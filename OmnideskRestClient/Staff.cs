using System;

namespace OmnideskRestClient
{
    public class Staff
    {
        public int staff_id { get; set; }
        public string staff_email { get; set; }
        public string staff_full_name { get; set; }
        public string staff_signature { get; set; }
        public string thumbnail { get; set; }
        public bool active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
