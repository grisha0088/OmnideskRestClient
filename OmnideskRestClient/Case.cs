  using System;
using System.Collections.Generic;

namespace OmnideskRestClient
{
    public class Case
    {
        public int case_id { get; set; }
        public string case_number { get; set; }
        public string subject { get; set; }
        public int user_id { get; set; }
        public int staff_id { get; set; }
        public int group_id { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string channel { get; set; }
        public string recipient { get; set; }
        public bool deleted { get; set; }
        public bool spam { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public CustomFields custom_fields { get; set; }
        public List<int> labels { get; set; }
    }
}