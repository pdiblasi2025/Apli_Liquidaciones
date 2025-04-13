using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.Admin
{
    public  class PermisosAdmin
    {

        private string _token;
        private string _domain;
        private string _audience;
        private string _audienceMod;




        public PermisosAdmin(string token, string domain,string audience )
        {
            this._token = "bearer " + token;
            this._domain = domain;
            this._audience =   audience ;  
            this._audienceMod = "https%3A%2F%2F" +  audience.Substring(8); ;
        }

        public List<Dtos.TablePermissions> GetPermissions()
        {
             
            var client = new RestClient($@"{_domain}/api/v2/resource-servers/{_audienceMod}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);

            IRestResponse response = client.Execute(request);

            var permisosDto = JsonConvert.DeserializeObject< Dtos.PermisosDto >(response.Content);

            var table = new List<Dtos.TablePermissions>();

            foreach (Dtos.Scope sc in permisosDto.scopes )
            {
                table.Add(new Dtos.TablePermissions()
                {
                    descripcion = sc.description,
                 
                    id = sc.value,
                    nombre = sc.description,
                    state = "Habilitar", 

                }) ;
            }
            return table;
        }

        public List<Dtos.TablePermissions>  GetPermissionsFromRole(string roleId)
        {
            var client = new RestClient($@"{_domain}/api/v2/roles/{roleId}/permissions");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);

            IRestResponse permisosFromRole = client.Execute(request);

            var permissionsFromRole = JsonConvert.DeserializeObject<List<Dtos.PermissionsFromRole>>(permisosFromRole.Content);

            var allPermissions = this.GetPermissions();
 


            foreach(Dtos.TablePermissions t in allPermissions)
            {
                if (permissionsFromRole.Where(x => x.permission_name == t.id).FirstOrDefault() != null)
                {
                    t.state = "Deshabilitar";
                }

                 
             
            }

            return allPermissions;
             
        }

        public Dtos.AssignPermissionToRol convertAssignDtos(Dtos.PreviousPermission dto)
        {
            Dtos.AssignPermissionToRol newDto = new Dtos.AssignPermissionToRol();
            newDto.permissions = new List<Dtos.permissionsAsigned>();

            foreach (string s in dto.permissions)
            {
                newDto.permissions.Add(new Dtos.permissionsAsigned()
                {
                    permission_name = s,
                    resource_server_identifier = _audience,
                });
            }

            return newDto;
        }

        public List<Dtos.TablePermissions> AssignPermissionToRol(string rolId, Dtos.PreviousPermission dto)
        {


            var newDto = convertAssignDtos(dto);


            var client = new RestClient($@"{_domain}/api/v2/roles/{rolId}/permissions");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", _token);

            //json content
            request.AddHeader("content-type", "application/json");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter($"application/json", JsonConvert.SerializeObject(newDto), ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                throw new System.Exception("Error Deshabilitando Permiso");
            }

            return this.GetPermissionsFromRole(rolId);
        }

        public List<Dtos.TablePermissions>  UnassignPermissionToRol(string rolId, Dtos.PreviousPermission dto) 
        {
            var newDto = convertAssignDtos(dto);

            var client = new RestClient($@"{_domain}/api/v2/roles/{rolId}/permissions");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("authorization", _token);

            //json content
            request.AddHeader("content-type", "application/json");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter($"application/json", JsonConvert.SerializeObject(newDto), ParameterType.RequestBody);

            

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                throw new System.Exception("Error Deshabilitando Permiso");
            }

            return this.GetPermissionsFromRole(rolId); 

        }
    }
}