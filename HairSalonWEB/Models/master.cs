using System.ComponentModel.DataAnnotations;

namespace HairSalonWEB.Models
{
    public class master
    {
        [Key]
        public int master_code { get; set; }
        public string master_surname { get; set; }
        public string master_name { get; set; }
        public string master_patronymic { get; set; }
        public string master_gender { get; set; }
        public string master_phone_number { get; set; }
        public string master_status { get; set; }
        public string master_profile { get; set; }
        public int master_salary { get; set; }
        public string master_login { get; set; }
        public string master_password { get; set; }
    }
}
