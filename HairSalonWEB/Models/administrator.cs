using System.ComponentModel.DataAnnotations;

namespace HairSalonWEB.Models
{
    public class administrator
    {
        [Key]
        public int admin_id { get; set; }
        public int company_code { get; set; }
        public string admin_surname { get; set; }
        public string admin_name { get; set; }
        public string admin_patronymic { get; set; }
        public string admin_gender { get; set; }
        public string admin_phone_number { get; set; }
        public string admin_status { get; set; }
        public string admin_login { get; set; }
        public string admin_password { get; set; }
    }
}
