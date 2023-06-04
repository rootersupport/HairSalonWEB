using System.ComponentModel.DataAnnotations;

namespace HairSalonWEB.Models
{
    public class procedures
    {
        [Key]
        public int procedure_code { get; set; }
        public string procedure_name { get; set; }
        public string procedure_price { get; set; }
        public int procedure_time { get; set; }
    }
}
