using System;

namespace OmnideskRestClient
{
    public class Group
    {
        public int group_id { get; set; }
        public string group_title { get; set; }
        public string group_from_name { get; set; }
        public string group_signature { get; set; }
        public bool active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
