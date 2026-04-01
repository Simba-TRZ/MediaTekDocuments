namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Utilisateur : représente un utilisateur de l'application
    /// </summary>
    public class Utilisateur
    {
        /// <summary>Identifiant de l'utilisateur</summary>
        public string Id { get; set; }

        /// <summary>Login de l'utilisateur</summary>
        public string Login { get; set; }

        /// <summary>Mot de passe de l'utilisateur</summary>
        public string Password { get; set; }

        /// <summary>Identifiant du service de l'utilisateur</summary>
        public string IdService { get; set; }

        /// <summary>Libellé du service de l'utilisateur</summary>
        public string Service { get; set; }

        /// <summary>
        /// Constructeur : initialise les propriétés de l'utilisateur
        /// </summary>
        /// <param name="id">Identifiant de l'utilisateur</param>
        /// <param name="login">Login de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur</param>
        /// <param name="idService">Identifiant du service</param>
        public Utilisateur(string id, string login, string password, string idService)
        {
            Id = id;
            Login = login;
            Password = password;
            IdService = idService;
        }
    }
}