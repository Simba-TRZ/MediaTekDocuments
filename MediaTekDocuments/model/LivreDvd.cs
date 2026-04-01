namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier abstraite LivreDvd hérite de Document : regroupe les propriétés communes aux livres et dvd
    /// </summary>
    public abstract class LivreDvd : Document
    {
        /// <summary>
        /// Constructeur : initialise les propriétés communes aux livres et dvd
        /// </summary>
        /// <param name="id">Identifiant du document</param>
        /// <param name="titre">Titre du document</param>
        /// <param name="image">Chemin vers l'image</param>
        /// <param name="idGenre">Identifiant du genre</param>
        /// <param name="genre">Libellé du genre</param>
        /// <param name="idPublic">Identifiant du public</param>
        /// <param name="lePublic">Libellé du public</param>
        /// <param name="idRayon">Identifiant du rayon</param>
        /// <param name="rayon">Libellé du rayon</param>
        protected LivreDvd(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
        }
    }
}