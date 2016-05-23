using System.ComponentModel.DataAnnotations;

namespace OmnideskRestClient
{
    public class Label
    {
        [Key]
        public int label_id { get; set; }
        public string label_title { get; set; }
    }
}
