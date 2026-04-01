namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Dvd hérite de LivreDvd : contient des propriétés spécifiques aux dvd
    /// </summary>
    public class Dvd : LivreDvd
    {
        /// <summary>
        /// Durée du dvd en minutes
        /// </summary>
        public int Duree { get; }

        /// <summary>
        /// Réalisateur du dvd
        /// </summary>
        public string Realisateur { get; }

        /// <summary>
        /// Synopsis du dvd
        /// </summary>
        public string Synopsis { get; }

        /// <summary>
        /// Constructeur : initialise les propriétés du dvd
        /// </summary>
        /// <param name="id">Identifiant du dvd</param>
        /// <param name="titre">Titre du dvd</param>
        /// <param name="image">Image du dvd</param>
        /// <param name="duree">Durée en minutes</param>
        /// <param name="realisateur">Réalisateur du dvd</param>
        /// <param name="synopsis">Synopsis du dvd</param>
        /// <param name="idGenre">Identifiant du genre</param>
        /// <param name="genre">Libellé du genre</param>
        /// <param name="idPublic">Identifiant du public</param>
        /// <param name="lePublic">Libellé du public</param>
        /// <param name="idRayon">Identifiant du rayon</param>
        /// <param name="rayon">Libellé du rayon</param>
        public Dvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.Duree = duree;
            this.Realisateur = realisateur;
            this.Synopsis = synopsis;
        }
    }
}