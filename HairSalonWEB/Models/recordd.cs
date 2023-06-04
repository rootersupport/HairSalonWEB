using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HairSalonWEB.Models
{
    public class recordd
    {
        [Key]
        public int record_code { get; set; }

        [ForeignKey("company")]
        public int company_code { get; set; }

        [ForeignKey("client")]
        public int client_code { get; set; }

        [ForeignKey("master")]
        public int master_code { get; set; }

        [ForeignKey("procedures")]
        public int procedure_code { get; set; }
        public DateTime record_time { get; set; }
    }
}
