using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using NLog;

namespace MediaTekDocuments.dal
{
    public class Access
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly string uriApi = ConfigurationManager.AppSettings["uriApi"];
        private static Access instance = null;
        private readonly ApiRest api = null;
        private const string GET = "GET";
        private const string POST = "POST";
        private const string PUT = "PUT";
        private const string DELETE = "DELETE";

        private Access()
        {
            try
            {
                string authenticationString = ConfigurationManager.AppSettings["authentification"];
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                logger.Fatal("Erreur initialisation API : " + e.Message);
                Environment.Exit(0);
            }
        }

        public static Access GetInstance()
        {
            if (instance == null)
                instance = new Access();
            return instance;
        }

        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre", null);
            return new List<Categorie>(lesGenres);
        }

        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon", null);
            return new List<Categorie>(lesRayons);
        }

        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public", null);
            return new List<Categorie>(lesPublics);
        }

        public List<Livre> GetAllLivres()
        {
            return TraitementRecup<Livre>(GET, "livre", null);
        }

        public List<Dvd> GetAllDvd()
        {
            return TraitementRecup<Dvd>(GET, "dvd", null);
        }

        public List<Revue> GetAllRevues()
        {
            return TraitementRecup<Revue>(GET, "revue", null);
        }

        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            String jsonIdDocument = ConvertToJson("id", idDocument);
            return TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument, null);
        }

        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire", "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("CreerExemplaire : " + ex.Message);
            }
            return false;
        }

        public bool CreerLivre(Livre livre)
        {
            String jsonLivre = JsonConvert.SerializeObject(livre);
            try
            {
                List<Livre> liste = TraitementRecup<Livre>(POST, "livre", "champs=" + jsonLivre);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("CreerLivre : " + ex.Message);
            }
            return false;
        }

        public bool ModifierLivre(Livre livre)
        {
            String jsonLivre = JsonConvert.SerializeObject(livre);
            try
            {
                List<Livre> liste = TraitementRecup<Livre>(PUT, "livre/" + livre.Id, "champs=" + jsonLivre);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("ModifierLivre : " + ex.Message);
            }
            return false;
        }

        public bool SupprimerLivre(Livre livre)
        {
            String jsonLivre = JsonConvert.SerializeObject(new { id = livre.Id });
            try
            {
                List<Livre> liste = TraitementRecup<Livre>(DELETE, "livre", "champs=" + jsonLivre);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("SupprimerLivre : " + ex.Message);
            }
            return false;
        }

        public bool CreerDvd(Dvd dvd)
        {
            String jsonDvd = JsonConvert.SerializeObject(dvd);
            try
            {
                List<Dvd> liste = TraitementRecup<Dvd>(POST, "dvd", "champs=" + jsonDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("CreerDvd : " + ex.Message);
            }
            return false;
        }

        public bool ModifierDvd(Dvd dvd)
        {
            String jsonDvd = JsonConvert.SerializeObject(dvd);
            try
            {
                List<Dvd> liste = TraitementRecup<Dvd>(PUT, "dvd/" + dvd.Id, "champs=" + jsonDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("ModifierDvd : " + ex.Message);
            }
            return false;
        }

        public bool SupprimerDvd(Dvd dvd)
        {
            String jsonDvd = JsonConvert.SerializeObject(new { id = dvd.Id });
            try
            {
                List<Dvd> liste = TraitementRecup<Dvd>(DELETE, "dvd", "champs=" + jsonDvd);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("SupprimerDvd : " + ex.Message);
            }
            return false;
        }

        public bool CreerRevue(Revue revue)
        {
            String jsonRevue = JsonConvert.SerializeObject(revue);
            try
            {
                List<Revue> liste = TraitementRecup<Revue>(POST, "revue", "champs=" + jsonRevue);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("CreerRevue : " + ex.Message);
            }
            return false;
        }

        public bool ModifierRevue(Revue revue)
        {
            String jsonRevue = JsonConvert.SerializeObject(revue);
            try
            {
                List<Revue> liste = TraitementRecup<Revue>(PUT, "revue/" + revue.Id, "champs=" + jsonRevue);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("ModifierRevue : " + ex.Message);
            }
            return false;
        }

        public bool SupprimerRevue(Revue revue)
        {
            String jsonRevue = JsonConvert.SerializeObject(new { id = revue.Id });
            try
            {
                List<Revue> liste = TraitementRecup<Revue>(DELETE, "revue", "champs=" + jsonRevue);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("SupprimerRevue : " + ex.Message);
            }
            return false;
        }

        public List<Suivi> GetAllSuivi()
        {
            return TraitementRecup<Suivi>(GET, "suivi", null);
        }

        public List<CommandeDocument> GetCommandesLivreDvd(string idLivreDvd)
        {
            String jsonId = ConvertToJson("id", idLivreDvd);
            return TraitementRecup<CommandeDocument>(GET, "commandedocument/" + jsonId, null);
        }

        public bool CreerCommandeLivreDvd(CommandeDocument commande)
        {
            var obj = new
            {
                id = commande.Id,
                dateCommande = commande.DateCommande,
                montant = commande.Montant,
                nbExemplaire = commande.NbExemplaire,
                idLivreDvd = commande.IdLivreDvd,
                idSuivi = commande.IdSuivi,
                suivi = commande.Suivi
            };
            String jsonCommande = JsonConvert.SerializeObject(obj, new CustomDateTimeConverter());
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(POST, "commandelivreDvd", "champs=" + jsonCommande);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("CreerCommandeLivreDvd : " + ex.Message);
            }
            return false;
        }

        public bool ModifierSuiviCommande(CommandeDocument commande)
        {
            String jsonCommande = JsonConvert.SerializeObject(new { idSuivi = commande.IdSuivi });
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(PUT, "commandedocument/" + commande.Id, "champs=" + jsonCommande);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("ModifierSuiviCommande : " + ex.Message);
            }
            return false;
        }

        public bool SupprimerCommandeLivreDvd(CommandeDocument commande)
        {
            String jsonCommande = JsonConvert.SerializeObject(new { id = commande.Id });
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(DELETE, "commande", "champs=" + jsonCommande);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("SupprimerCommandeLivreDvd : " + ex.Message);
            }
            return false;
        }

        public List<Abonnement> GetAbonnementsRevue(string idRevue)
        {
            String jsonId = ConvertToJson("id", idRevue);
            return TraitementRecup<Abonnement>(GET, "abonnement/" + jsonId, null);
        }

        public bool CreerAbonnement(Abonnement abonnement)
        {
            var obj = new
            {
                id = abonnement.Id,
                dateCommande = abonnement.DateCommande,
                montant = abonnement.Montant,
                dateFinAbonnement = abonnement.DateFinAbonnement,
                idRevue = abonnement.IdRevue
            };
            String jsonAbonnement = JsonConvert.SerializeObject(obj, new CustomDateTimeConverter());
            try
            {
                List<Abonnement> liste = TraitementRecup<Abonnement>(POST, "abonnement", "champs=" + jsonAbonnement);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("CreerAbonnement : " + ex.Message);
            }
            return false;
        }

        public bool SupprimerAbonnement(Abonnement abonnement)
        {
            var obj = new { id = abonnement.Id };
            String jsonAbonnement = JsonConvert.SerializeObject(obj);
            try
            {
                List<Abonnement> liste = TraitementRecup<Abonnement>(DELETE, "abonnement", "champs=" + jsonAbonnement);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("SupprimerAbonnement : " + ex.Message);
            }
            return false;
        }

        public List<Abonnement> GetAbonnementsExpiresBientot()
        {
            return TraitementRecup<Abonnement>(GET, "abonnementexpire", null);
        }

        private List<T> TraitementRecup<T>(String methode, String message, String parametres)
        {
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message, parametres);
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    logger.Error("code erreur = " + code + " message = " + (String)retour["message"]);
                }
            }
            catch (Exception e)
            {
                logger.Error("Erreur lors de l'accès à l'API : " + e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        private static String ConvertToJson(Object nom, Object valeur)
        {
            Dictionary<Object, Object> dictionary = new Dictionary<Object, Object> { { nom, valeur } };
            return JsonConvert.SerializeObject(dictionary);
        }

        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public List<Exemplaire> GetExemplairesLivreDvd(string idLivreDvd)
        {
            String jsonId = ConvertToJson("id", idLivreDvd);
            return TraitementRecup<Exemplaire>(GET, "exemplairellivredvd/" + jsonId, null);
        }

        public bool ModifierEtatExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(new { idEtat = exemplaire.IdEtat });
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(PUT, "exemplaire/" + exemplaire.Numero, "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("ModifierEtatExemplaire : " + ex.Message);
            }
            return false;
        }

        public bool SupprimerExemplaire(Exemplaire exemplaire)
        {
            var obj = new { id = exemplaire.Id, numero = exemplaire.Numero };
            String jsonExemplaire = JsonConvert.SerializeObject(obj);
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(DELETE, "exemplaire", "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                logger.Error("SupprimerExemplaire : " + ex.Message);
            }
            return false;
        }

        public List<Categorie> GetAllEtats()
        {
            List<Etat> lesEtats = TraitementRecup<Etat>(GET, "etat", null);
            return lesEtats.Select(e => (Categorie)e).ToList();
        }

        public Utilisateur GetUtilisateur(string login)
        {
            String jsonLogin = ConvertToJson("login", login);
            List<Utilisateur> lesUtilisateurs = TraitementRecup<Utilisateur>(GET, "utilisateur/" + jsonLogin, null);
            if (lesUtilisateurs != null && lesUtilisateurs.Count > 0)
                return lesUtilisateurs[0];
            return null;
        }
    }
}