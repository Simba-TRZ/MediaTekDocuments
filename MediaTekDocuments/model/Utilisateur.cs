namespace MediaTekDocuments.model
{
    public class Utilisateur
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string IdService { get; set; }
        public string Service { get; set; }
        public Utilisateur(string id, string login, string password, string idService)
        {
            Id = id;
            Login = login;
            Password = password;
            IdService = idService;
        }
    }
}