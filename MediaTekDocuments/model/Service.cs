namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Service : représente le service d'un utilisateur
    /// </summary>
    public class Service
    {
        /// <summary>Identifiant du service</summary>
        public string Id { get; set; }

        /// <summary>Libellé du service</summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Constructeur : initialise les propriétés du service
        /// </summary>
        /// <param name="id">Identifiant du service</param>
        /// <param name="libelle">Libellé du service</param>
        public Service(string id, string libelle)
        {
            Id = id;
            Libelle = libelle;
        }
    }
}
