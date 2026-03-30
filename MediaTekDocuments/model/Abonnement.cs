namespace MediaTekDocuments.model
{
    public class Abonnement
    {
        public string Id { get; set; }
        public string DateCommande { get; set; }
        public double Montant { get; set; }
        public string DateFinAbonnement { get; set; }
        public string IdRevue { get; set; }
        public string Titre { get; set; }

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