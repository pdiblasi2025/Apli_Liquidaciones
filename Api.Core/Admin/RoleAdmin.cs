using Api.Core.Controllers;
using Api.Core.Dtos;
using Api.Core.Dtos.Common;
using Api.Core.Dtos.Filter;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.Admin
{
    public class RoleAdmin  
    {
        private string _token;

        private string _domain;

        public RoleAdmin(string token, string domain)
        {
            if (!token.Contains("bearer"))
            {
                this._token = "bearer " + token;
            }
            else
            {
                this._token = token;
            }
            this._domain = domain  ;
        }

        public  IRestResponse Create(Dtos.CreateRole dto )
        {
            var client = new RestClient($@"{_domain}/api/v2/roles");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", _token);
            
            //json content
            request.AddHeader("content-type", "application/json");            
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter( $"application/json",  JsonConvert.SerializeObject(dto) , ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse Update   (string roleId,UpdateRole dto)
        {
            var client = new RestClient($@"{_domain}/api/v2/roles/{roleId}");
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("authorization", _token);

            //json content
            request.AddHeader("content-type", "application/json");
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter($"application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);


            IRestResponse response = client.Execute(request);

            return response;
        }

        public IRestResponse Delete(string roleId)
        {
            var client = new RestClient($@"{_domain}/api/v2/roles/{roleId}");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("authorization", _token);

            IRestResponse response = client.Execute(request);

            return response;
        }

        public virtual List<GetRoleOuput> GetAll()
        {
            var client = new RestClient($@"{_domain}/api/v2/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);
            
            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<GetRoleOuput>>(response.Content);
        }

        public virtual List<GetRoleOuput> GetRolesByUser(string userId)
        {
            var client = new RestClient($@"{_domain}/api/v2/users/{userId}/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);

            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<GetRoleOuput>>(response.Content);
        }

        public virtual PagedListResponse<GetRoleOuput> BasicQuery(FilterRole filter)
        {
            var client = new RestClient($@"{_domain}/api/v2/roles");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", _token);
            
            IRestResponse response = client.Execute(request);

            var listOfRecs = JsonConvert.DeserializeObject<List<GetRoleOuput>>(response.Content);

            if (!string.IsNullOrEmpty(filter.MultiColumnSearchText))
            {
              listOfRecs =  listOfRecs.Where(x=>x.name.Contains(filter.MultiColumnSearchText)|| x.description.Contains(filter.MultiColumnSearchText)).ToList();
            }

            return new PagedListResponse<GetRoleOuput>
            {
                Count = listOfRecs.Count(),
                Data = listOfRecs
            };
        }

    }
}