using Api.Core.Dtos;
using Api.Core.Dtos.Request;
using Api.Core.Dtos.Response;
using Api.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core.Admin
{
    public class AuthAdmin
    {
        private readonly string _authority;
        //private readonly string _token;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _namespace;
        public MyContext MyContext;


        public AuthAdmin(AppSettings appSettings, MyContext context)
        {
            _authority = appSettings.Authority;
            _clientId = appSettings.ClientId;
            _clientSecret = appSettings.ClientSecret;
            _namespace = appSettings.NameSpace;
            MyContext = context;
        }

        private RestRequest GetRequest(Method method)
        {
            var request = new RestRequest(method);
            var token = GetManagementAPIToken();
            request.AddHeader("authorization", $"Bearer {token}");
            return request;
        }

        private IRestResponse Execute(string path, Method method)
        {
            var client = new RestClient($"{_authority}{path}");
            var request = GetRequest(method);
            return client.Execute(request);
        }

        private IRestResponse Execute(string path, RestRequest request)
        {
            var client = new RestClient($"{_authority}{path}");
            return client.Execute(request);
        }

        public IRestResponse GetUser(string userId)
        {
            return Execute($"/api/v2/users/{userId}", Method.GET);
        }

        public IRestResponse GetUserProfile(string token)
        {
            var client = new RestClient($"{_authority}/userinfo");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"Bearer {token}");
            return client.Execute(request);
        }

        public IRestResponse GetAllUsers()
        {
             
            var client = new RestClient($@"{_authority}/api/v2/users");            
         
            var request = new RestRequest(Method.GET);
            var token = GetManagementAPIToken();

            request.AddHeader("authorization", "bearer " + token);
            var response  =  client.Execute(request);
            if (response.IsSuccessful)
            {
                List<AuthUser> users = JsonConvert.DeserializeObject<List<AuthUser>>(response.Content);
                foreach (AuthUser user in users) {
                    
                    user.user_metadata.roles = convertListToString(user.app_metadata  );
                     
                    

                }
                var serialized = JsonConvert.SerializeObject(users);
                response.Content = serialized;
                
            }

            return response;
        }

        private string convertListToString(AppMetadata app_metadata)
        {
            if (app_metadata == null)
            {
                return "";
            }
            var result = "";
            var first = "";
            foreach(string s in app_metadata.roles)
            {
                @result = result +  first + s ;
                @first = ", ";
            }
            return result;
        }

        public IRestResponse GetUserRoles(string token)
        {
            string userId = ExtractUserId(token);
            var client = new RestClient($"{_authority}/api/v2/users/{userId}/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", token);
            return  client.Execute(request);
        }

        private string ExtractUserId(string token)
        {
            var parts = token.Split('.');
            var decoded = Convert.FromBase64String(parts[2]);
             var part =  Encoding.UTF8.GetString(decoded);
              var jwt = JObject.Parse(part);
            return jwt.ToString();
        }

        public IRestResponse GetUsersByParent(string parentUserId)
        {
            return Execute($"/api/v2/users?q=user_metadata.parent_user_id={parentUserId}", Method.GET);
        }

        public IRestResponse GetUsersByCompany(int companyId)
        {
            return Execute($"/api/v2/users?q=user_metadata.company_id={companyId}", Method.GET);
        }

        public IRestResponse CreateUser(CreateUserRequest createUserRequest)
        {
            var request = GetRequest(Method.POST);
            request.AddParameter("application/json", JsonConvert.SerializeObject(createUserRequest), ParameterType.RequestBody);
            return Execute($"/api/v2/users", request);
        }

        public IRestResponse UpdateUser(UpdateUserRequest updateUserRequest, string userId)
        {
            var request = GetRequest(Method.PATCH);
            request.AddParameter("application/json", JsonConvert.SerializeObject(updateUserRequest), ParameterType.RequestBody);
            return Execute($"/api/v2/users/{userId}", request);
        }

        public IRestResponse BlockUser(BlockUserRequest blockUserRequest)
        {
            var request = GetRequest(Method.PATCH);
            var parameter = blockUserRequest.block ? "{\"blocked\":true}" : "{\"blocked\":false}";
            request.AddParameter("application/json", parameter, ParameterType.RequestBody);
            return Execute($"/api/v2/users/{blockUserRequest.user_id}", request);
        }

        public IRestResponse DeleteUser(string userId)
        {
            return Execute($"/api/v2/users/{userId}", Method.DELETE);
        }

        public Dtos.Common.Result FinishSignUp(Dtos.Compania company, string accessToken)
        {
            var result = new Dtos.Common.Result();

            var user = GetCurrentUser(accessToken);

            var compania = MyContext.Companias.Include(x => x.Licencias).FirstOrDefault(x => x.Id == company.Id);

            var companyUser = MyContext.Companias.FirstOrDefault(x => !x.Deleted && x.Enabled && x.AuthId == user.user_id);

            if (companyUser != null && companyUser.Id != compania.Id)
            {
                //usuario registrado, eliminación de compañía y licencia
                DeleteCompany(compania);

                result.HasErrors = true;
                result.Messages.Add("El usuario ya se encuentra registrado.");

                return result;
            }

            //usuario no registrado, habilitación de compañía, licencia y seteo de metadata
            EnableCompany(compania, user);

            var request = new UpdateUserRequest() { user_metadata = new UserMetadata() { company_id = company.Id, is_owner = true } };
            var resultUpdate = UpdateUser(request, user.user_id);

            return result;
        }

        private void DeleteCompany(Entities.Compania compania)
        {
            compania.Deleted = true;
            compania.DeleteDate = DateTime.Now;
            compania.DeleteBy = "admin";

            compania.Licencias[0].Deleted = true;
            compania.Licencias[0].DeleteDate = DateTime.Now;
            compania.Licencias[0].DeleteBy = "admin";

            MyContext.Update(compania);
            MyContext.SaveChanges();
        }

        private void EnableCompany(Entities.Compania compania, UsuarioAuth usuarioAuth)
        {
            compania.Enabled = true;
            compania.AuthId = usuarioAuth.user_id;
            compania.NombreUsuario = usuarioAuth.given_name;
            compania.ApellidoUsuario = usuarioAuth.family_name;
            compania.UpdateDate = DateTime.Now;
            compania.UpdatedBy = usuarioAuth.user_id;

            compania.Licencias[0].Enabled = true;
            compania.Licencias[0].Estado = 1;
            compania.Licencias[0].UpdateDate = DateTime.Now;
            compania.Licencias[0].UpdatedBy = usuarioAuth.user_id;

            MyContext.Update(compania);
            MyContext.SaveChanges();
        }

        public UsuarioAuth GetCurrentUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = (JwtSecurityToken)handler.ReadToken(token);

            var usuarioAuth = new UsuarioAuth()
            {
                user_id = jsonToken.Claims.FirstOrDefault(claim => claim.Type.Contains("example.com/email")).Value
            };          
            return usuarioAuth;
        }

        public string GetManagementAPIToken()
        {

            var client = new RestClient($"{_authority}/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded",
                                 $"grant_type=client_credentials&client_id={_clientId}&client_secret={_clientSecret}&audience={_authority}/api/v2/",
                                 ParameterType.RequestBody);

            var result = client.Execute(request);

            var response = JsonConvert.DeserializeObject<APIManagementTokenResponse>(result.Content);

            return response.access_token;
        }
    }
}
