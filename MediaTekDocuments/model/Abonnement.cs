namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Abonnement : représente un abonnement à une revue
    /// </summary>
    public class Abonnement
    {
        /// <summary>Identifiant de l'abonnement</summary>
        public string Id { get; set; }

        /// <summary>Date de commande de l'abonnement</summary>
        public string DateCommande { get; set; }

        /// <summary>Montant de l'abonnement</summary>
        public double Montant { get; set; }

        /// <summary>Date de fin de l'abonnement</summary>
        public string DateFinAbonnement { get; set; }

        /// <summary>Identifiant de la revue associée</summary>
        public string IdRevue { get; set; }

        /// <summary>Titre de la revue associée</summary>
        public string Titre { get; set; }

        /// <summary>
        /// Constructeur : initialise les propriétés de l'abonnement
        /// </summary>
        /// <param name="id">Identifiant de l'abonnement</param>
        /// <param name="dateCommande">Date de commande</param>
        /// <param name="montant">Montant de l'abonnement</param>
        /// <param name="dateFinAbonnement">Date de fin d'abonnement</param>
        /// <param name="idRevue">Identifiant de la revue</param>
        /// <param name="titre">Titre de la revue (optionnel)</param>
        public Abonnement(string id, string dateCommande, double montant,
                          string dateFinAbonnement, string idRevue, string titre = "")
        {
            Id = id;
            DateCommande = dateCommande;
            Montant = montant;
            DateFinAbonnement = dateFinAbonnement;
            IdRevue = idRevue;
            Titre = titre;
        }
    }
}