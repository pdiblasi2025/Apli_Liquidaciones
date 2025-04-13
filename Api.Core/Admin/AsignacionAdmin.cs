using Api.Core.Controllers;
using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.Admin
{
    internal class AsignacionAdmin
    {
        private string _token;
        private string _domain;
        private RoleAdmin _roleAdmin;

        public AsignacionAdmin(string token, string domain)
        {
            this._token = "bearer " + token;
            this._domain = domain;

            _roleAdmin = new RoleAdmin(_token, _domain);
        }


        public IRestResponse GetRolesFromUser(string userId)
        {
            var client = new RestClient($@"{_domain}/api/v2/users/{userId}/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);

            //json content 

            IRestResponse response = client.Execute(request);

            return response;
        }


        public List<Dtos.TableRoles> GetRolesFromUserTable(string userId)
        {
            var rolesResponse = this.getRoles();
            var  allRoles =  JsonConvert.DeserializeObject<List<DtoRole>>(rolesResponse.Content);
          
            var client = new RestClient($@"{_domain}/api/v2/users/{userId}/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);

            //json content 

            IRestResponse response = client.Execute(request);
             var rolesInUsers = JsonConvert.DeserializeObject<List<DtoRole>>(response.Content);
            List<Dtos.TableRoles> tableRoles = new List<Dtos.TableRoles>();
            foreach (DtoRole role in allRoles)
            {
                tableRoles.Add(new TableRoles()
                {
                    id = role.id,
                    nombre = role.name,
                    descripcion = role.description,
                    state = rolesInUsers.Where(x => x.name == role.name ).FirstOrDefault()!=null ? "Deshabilitar" : "Habilitar",

                });

            }

                return tableRoles;
        }

        public IRestResponse AsignRoleToUser(AsignRoleToUserAuth0 dto,string userId )
        {
            var client = new RestClient($@"{_domain}/api/v2/users/{userId}/roles");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", _token);

            //json content
            request.AddHeader("content-type", "application/json");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter($"application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);

            return response;
        }

        public  IRestResponse UnasignRoleToUser(AsignRoleToUserAuth0 dto  , string userId)
        {
            var client = new RestClient($@"{_domain}/api/v2/users/{userId}/roles");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("authorization", _token);

            //json content
            request.AddHeader("content-type", "application/json");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter($"application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse getRoles()
        {

            var client = new RestClient($@"{_domain}/api/v2/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);
 


            IRestResponse response = client.Execute(request);

            return response;
        }

        public List<Dtos.DtoRole> getRolesCombo()
        {

            var client = new RestClient($@"{_domain}/api/v2/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);



            IRestResponse response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<List<Dtos.DtoRole>>(response.Content);

           foreach(DtoRole role in result)
            {
                role.text = role.name;
                role.value = role.id;
            }

            return result;
        }

        public IRestResponse GetUsers()
        {
            var client = new RestClient($@"{_domain}/api/v2/users");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}