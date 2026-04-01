namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Etat (état d'usure d'un document) hérite de Categorie
    /// </summary>
    public class Etat : Categorie
    {
        /// <summary>
        /// Constructeur : initialise les propriétés de l'état
        /// </summary>
        /// <param name="id">Identifiant de l'état</param>
        /// <param name="libelle">Libellé de l'état</param>
        public Etat(string id, string libelle) : base(id, libelle)
        {
        }
    }
}