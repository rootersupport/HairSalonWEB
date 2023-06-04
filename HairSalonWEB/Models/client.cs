using System.ComponentModel.DataAnnotations;

namespace HairSalonWEB.Models
{
    public class client
    {
        [Key]
        public int client_code { get; set; }
        public string client_name { get; set; }
        public string client_surname { get; set; }
        public string client_patronymic { get; set; }
        public string client_gender { get; set; }
        public string client_phone_number { get; set; }
        public string client_login { get; set; }
        public string client_password { get; set; }
    }
}
