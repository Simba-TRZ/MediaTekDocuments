using MediaTekDocuments.dal;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    public class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Crée un livre dans la bdd
        /// </summary>
        /// <param name="livre">L'objet Livre concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerLivre(Livre livre)
        {
            return access.CreerLivre(livre);
        }

        /// <summary>
        /// Modifie un livre dans la bdd
        /// </summary>
        /// <param name="livre">L'objet Livre concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierLivre(Livre livre)
        {
            return access.ModifierLivre(livre);
        }

        /// <summary>
        /// Supprime un livre dans la bdd
        /// </summary>
        /// <param name="livre">L'objet Livre concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerLivre(Livre livre)
        {
            return access.SupprimerLivre(livre);
        }

        /// <summary>
        /// Crée un DVD dans la bdd
        /// </summary>
        /// <param name="dvd">L'objet Dvd concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerDvd(Dvd dvd)
        {
            return access.CreerDvd(dvd);
        }

        /// <summary>
        /// Modifie un DVD dans la bdd
        /// </summary>
        /// <param name="dvd">L'objet Dvd concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierDvd(Dvd dvd)
        {
            return access.ModifierDvd(dvd);
        }

        /// <summary>
        /// Supprime un DVD dans la bdd
        /// </summary>
        /// <param name="dvd">L'objet Dvd concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerDvd(Dvd dvd)
        {
            return access.SupprimerDvd(dvd);
        }

        /// <summary>
        /// Crée une revue dans la bdd
        /// </summary>
        /// <param name="revue">L'objet Revue concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerRevue(Revue revue)
        {
            return access.CreerRevue(revue);
        }

        /// <summary>
        /// Modifie une revue dans la bdd
        /// </summary>
        /// <param name="revue">L'objet Revue concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierRevue(Revue revue)
        {
            return access.ModifierRevue(revue);
        }

        /// <summary>
        /// Supprime une revue dans la bdd
        /// </summary>
        /// <param name="revue">L'objet Revue concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerRevue(Revue revue)
        {
            return access.SupprimerRevue(revue);
        }

        /// <summary>
        /// getter sur la liste des étapes de suivi
        /// </summary>
        /// <returns>Liste d'objets Suivi</returns>
        public List<Suivi> GetAllSuivi()
        {
            return access.GetAllSuivi();
        }

        /// <summary>
        /// getter sur les commandes d'un livre ou DVD
        /// </summary>
        /// <param name="idLivreDvd">id du livre ou DVD concerné</param>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetCommandesLivreDvd(string idLivreDvd)
        {
            return access.GetCommandesLivreDvd(idLivreDvd);
        }

        /// <summary>
        /// Crée une commande de livre ou DVD dans la bdd
        /// </summary>
        /// <param name="commande">L'objet CommandeDocument concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerCommandeLivreDvd(CommandeDocument commande)
        {
            return access.CreerCommandeLivreDvd(commande);
        }

        /// <summary>
        /// Modifie le suivi d'une commande de livre ou DVD dans la bdd
        /// </summary>
        /// <param name="commande">L'objet CommandeDocument concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierSuiviCommande(CommandeDocument commande)
        {
            return access.ModifierSuiviCommande(commande);
        }

        /// <summary>
        /// Supprime une commande de livre ou DVD dans la bdd
        /// </summary>
        /// <param name="commande">L'objet CommandeDocument concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerCommandeLivreDvd(CommandeDocument commande)
        {
            return access.SupprimerCommandeLivreDvd(commande);
        }

        /// <summary>
        /// Retourne les abonnements d'une revue
        /// </summary>
        public List<Abonnement> GetAbonnementsRevue(string idRevue)
        {
            return access.GetAbonnementsRevue(idRevue);
        }

        /// <summary>
        /// Crée un abonnement (nouvel abonnement ou renouvellement)
        /// </summary>
        public bool CreerAbonnement(Abonnement abonnement)
        {
            return access.CreerAbonnement(abonnement);
        }

        /// <summary>
        /// Supprime un abonnement si aucun exemplaire n'y est rattaché
        /// </summary>
        public bool SupprimerAbonnement(Abonnement abonnement, List<Exemplaire> exemplairesRevue)
        {
            foreach (Exemplaire exemplaire in exemplairesRevue)
            {
                DateTime dateParution = exemplaire.DateAchat;
                DateTime dateCommande = DateTime.Parse(abonnement.DateCommande);
                DateTime dateFin = DateTime.Parse(abonnement.DateFinAbonnement);

                if (ParutionDansAbonnement(dateCommande, dateFin, dateParution))
                {
                    return false; // exemplaire rattaché → suppression interdite
                }
            }
            return access.SupprimerAbonnement(abonnement);
        }

        /// <summary>
        /// Retourne true si dateParution est comprise entre dateCommande et dateFinAbonnement
        /// Méthode testable unitairement (requise par la tâche)
        /// </summary>
        public bool ParutionDansAbonnement(DateTime dateCommande,
                                           DateTime dateFinAbonnement,
                                           DateTime dateParution)
        {
            return dateParution >= dateCommande && dateParution <= dateFinAbonnement;
        }

        /// <summary>
        /// Retourne les revues dont l'abonnement expire dans moins de 30 jours
        /// </summary>
        public List<Abonnement> GetAbonnementsExpiresBientot()
        {
            return access.GetAbonnementsExpiresBientot();
        }

        /// <summary>
        /// Retourne les exemplaires d'un livre ou DVD
        /// </summary>
        public List<Exemplaire> GetExemplairesLivreDvd(string idLivreDvd)
        {
            return access.GetExemplairesLivreDvd(idLivreDvd);
        }

        /// <summary>
        /// Modifie l'état d'un exemplaire
        /// </summary>
        public bool ModifierEtatExemplaire(Exemplaire exemplaire)
        {
            return access.ModifierEtatExemplaire(exemplaire);
        }

        /// <summary>
        /// Supprime un exemplaire
        /// </summary>
        public bool SupprimerExemplaire(Exemplaire exemplaire)
        {
            return access.SupprimerExemplaire(exemplaire);
        }

        /// <summary>
        /// Retourne tous les états
        /// </summary>
        public List<Categorie> GetAllEtats()
        {
            return access.GetAllEtats();
        }

        /// <summary>
        /// Retourne un utilisateur par son login
        /// </summary>
        public Utilisateur GetUtilisateur(string login)
        {
            return access.GetUtilisateur(login);
        }
    }
}