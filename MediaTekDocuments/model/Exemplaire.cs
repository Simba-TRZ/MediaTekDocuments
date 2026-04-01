using System;
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Exemplaire (exemplaire d'une revue)
    /// </summary>
    public class Exemplaire
    {
        /// <summary>Numéro de l'exemplaire</summary>
        public int Numero { get; set; }

        /// <summary>Chemin vers la photo de l'exemplaire</summary>
        public string Photo { get; set; }

        /// <summary>Date d'achat de l'exemplaire</summary>
        public DateTime DateAchat { get; set; }

        /// <summary>Identifiant de l'état de l'exemplaire</summary>
        public string IdEtat { get; set; }

        /// <summary>Identifiant du document associé</summary>
        public string Id { get; set; }

        /// <summary>
        /// Constructeur : initialise les propriétés de l'exemplaire
        /// </summary>
        /// <param name="numero">Numéro de l'exemplaire</param>
        /// <param name="dateAchat">Date d'achat</param>
        /// <param name="photo">Chemin vers la photo</param>
        /// <param name="idEtat">Identifiant de l'état</param>
        /// <param name="idDocument">Identifiant du document associé</param>
        public Exemplaire(int numero, DateTime dateAchat, string photo, string idEtat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.IdEtat = idEtat;
            this.Id = idDocument;
        }
    }
}