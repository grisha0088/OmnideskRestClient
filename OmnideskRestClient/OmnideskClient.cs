using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace OmnideskRestClient
{
    //Omnidesk REST API documentation: https://omnidesk.ru/api/introduction/intro

    public class OmnideskClient
    {
        private RestClient RestClient;

        public OmnideskClient(string baseUrl, string username, string password)
        {
            var baseApiUrl = new Uri(new Uri(baseUrl), "api/").ToString();
            RestClient = new RestClient(baseApiUrl);
            RestClient.Authenticator = new HttpBasicAuthenticator(username, password);
        }

        public List<Case> GetCases(int startFromPage, int count)
        {
            List<Case> issues = new List<Case>();

            if (count <= 0) return null;
            int page = startFromPage;
            int step = 100;
            while (count > 0)
            {
                count -= 100;
                if (count <= 0)
                {
                    step = count + 100;
                }
                var result = GetCasesAsString(page, step);
                page++;

                if(result == "{\n    \"case\": []\n}") return null;  
                JObject iss = JObject.Parse(result);
                foreach (JProperty issue in iss.Properties())
                {
                    if (issue.Name == "total_count") continue; // skip "total_count" property
                    var c = JsonConvert.DeserializeObject<Case>(issue.Value["case"].ToString());
                    issues.Add(c);
                }
                if (iss.Properties().Count() < 100)
                {
                    break;
                }
            }
            return issues;
        }
        private string GetCasesAsString(int page, int limit)
        {
            var request = new RestRequest("cases.json", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("limit", limit);
            request.AddParameter("page", page);
            //сортировка от новых к старым
            request.AddParameter("sort", "updated_at_desc");
            // execute the request
            var response = RestClient.Execute(request);
            return response.Content; // raw content as string
        }

        public Case GetCase(int id)
        {
            var request = new RestRequest("cases/" + id +".json", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            var response = RestClient.Execute(request);
            var caseContainer = JsonConvert.DeserializeObject<CaseContainer>(response.Content);
            return caseContainer.Case;
        }

        public List<Label> GetLables(int startFromPage, int count)
        {
            List<Label> lables = new List<Label>();
            if (count <= 0) return null;
            int step = 100;

            while (count > 0)
            {
                count -= 100;
                if (count <= 0)
                {
                    step = count + 100;
                }
                var result = GetLablesAsString(startFromPage, step);
                if (result == "{\"LableContainer\":\"\"}") return null;
                var lablelist = JsonConvert.DeserializeObject<LableList>(result);
                foreach (var lableContainer in lablelist.LableContainer)
                {
                    lables.Add(lableContainer.label);
                }
                startFromPage++;
                if (lablelist.LableContainer.Count < 100)
                {
                    break;
                }
            }
            return lables;
        }
        private string GetLablesAsString(int page, int limit)
        {
            var request = new RestRequest("labels.json", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("limit", limit);
            request.AddParameter("page", page);

            // execute the request
            var response = RestClient.Execute(request);
            return "{\"LableContainer\":" + response.Content + "}"; // raw content as string
        }

        public List<Staff> GetStaff(int startFromPage, int count)
        {
            List<Staff> lables = new List<Staff>();
            if (count <= 0) return null;
            int step = 100;

            while (count > 0)
            {
                count -= 100;
                if (count <= 0)
                {
                    step = count + 100;
                }
                var result = GetStaffAsString(startFromPage, step);
                if (result == "{\"StaffContainer\":\"\"}") return null;
                var stafflist = JsonConvert.DeserializeObject<StaffList>(result);
                foreach (var staffContainer in stafflist.StaffContainer)
                {
                    lables.Add(staffContainer.staff);
                }
                startFromPage++;
                if (stafflist.StaffContainer.Count < 100)
                {
                    break;
                }
            }
            return lables;
        }
        private string GetStaffAsString(int page, int limit)
        {
            var request = new RestRequest("staff.json", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("limit", limit);
            request.AddParameter("page", page);

            // execute the request
            var response = RestClient.Execute(request);
            return "{\"StaffContainer\":" + response.Content + "}"; // raw content as string
        }

        public List<Group> GetGroups(int startFromPage, int count)
        {
            List<Group> groups = new List<Group>();
            if (count <= 0) return null;
            int step = 100;

            while (count > 0)
            {
                count -= 100;
                if (count <= 0)
                {
                    step = count + 100;
                }
                var result = GetGroupsAsString(startFromPage, step);
                if (result == "{\"GroupContainer\":\"\"}") return null;
                var grouplist = JsonConvert.DeserializeObject<GroupList>(result);
                foreach (var groupContainer in grouplist.GroupContainer)
                {
                    groups.Add(groupContainer.group);
                }
                startFromPage++;
                if (grouplist.GroupContainer.Count < 100)
                {
                    break;
                }
            }
            return groups;
        }
        private string GetGroupsAsString(int page, int limit)
        {
            var request = new RestRequest("groups.json", Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("limit", limit);
            request.AddParameter("page", page);

            // execute the request
            var response = RestClient.Execute(request);
            return "{\"GroupContainer\":" + response.Content + "}"; // raw content as string
        }

    }

}
