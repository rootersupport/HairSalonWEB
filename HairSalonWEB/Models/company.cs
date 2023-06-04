using System.ComponentModel.DataAnnotations;

namespace HairSalonWEB.Models
{
    public class company
    {
        [Key]
        public int company_code { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public string company_phone_number { get; set; }
        public string director_full_name { get; set; }
    }
}
