namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe représentant une étape de suivi de commande
    /// </summary>
    public class Suivi
    {
        /// <summary>
        /// Identifiant du suivi
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Libellé du suivi
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Suivi(string id, string libelle)
        {
            Id = id;
            Libelle = libelle;
        }

        /// <summary>
        /// Retourne le libellé pour l'affichage dans les combos
        /// </summary>
        public override string ToString()
        {
            return Libelle;
        }
    }
}