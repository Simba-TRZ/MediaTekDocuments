namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Etat (état d'usure d'un document)
    /// </summary>
    public class Etat : Categorie
    {
        public Etat(string id, string libelle) : base(id, libelle)
        {
        }
    }
}