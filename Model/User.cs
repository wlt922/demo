namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime birthdate { get; set; }
        public bool is_active { get; set; }

        //public bool is_admin { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}
