namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe représentant une commande de livre ou DVD
    /// </summary>
    public class CommandeDocument
    {
        /// <summary>Identifiant de la commande</summary>
        public string Id { get; set; }

        /// <summary>Date de la commande</summary>
        public string DateCommande { get; set; }

        /// <summary>Montant de la commande</summary>
        public double Montant { get; set; }

        /// <summary>Nombre d'exemplaires commandés</summary>
        public int NbExemplaire { get; set; }

        /// <summary>Identifiant du livre ou DVD concerné</summary>
        public string IdLivreDvd { get; set; }

        /// <summary>Identifiant de l'étape de suivi</summary>
        public string IdSuivi { get; set; }

        /// <summary>Libellé de l'étape de suivi</summary>
        public string Suivi { get; set; }

        /// <summary>
        /// Constructeur : initialise les propriétés de la commande document
        /// </summary>
        /// <param name="id">Identifiant de la commande</param>
        /// <param name="dateCommande">Date de la commande</param>
        /// <param name="montant">Montant de la commande</param>
        /// <param name="nbExemplaire">Nombre d'exemplaires commandés</param>
        /// <param name="idLivreDvd">Identifiant du livre ou DVD</param>
        /// <param name="idSuivi">Identifiant du suivi</param>
        /// <param name="suivi">Libellé du suivi</param>
        public CommandeDocument(string id, string dateCommande, double montant,
                                int nbExemplaire, string idLivreDvd, string idSuivi, string suivi)
        {
            Id = id;
            DateCommande = dateCommande;
            Montant = montant;
            NbExemplaire = nbExemplaire;
            IdLivreDvd = idLivreDvd;
            IdSuivi = idSuivi;
            Suivi = suivi;
        }
    }
}