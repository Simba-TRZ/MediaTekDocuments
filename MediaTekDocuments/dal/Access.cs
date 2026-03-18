using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;

namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = "http://10.192.22.254:8888/rest_mediatekdocuments/";
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// méthode HTTP pour update
        /// </summary>
        private const string PUT = "PUT";
        /// <summary>
        /// méthode HTTP pour delete
        /// </summary>
        private const string DELETE = "DELETE";

        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            String authenticationString;
            try
            {
                authenticationString = "admin:adminpwd";
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if (instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre", null);
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon", null);
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public", null);
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne tous les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre", null);
            return lesLivres;
        }

        /// <summary>
        /// Retourne tous les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd", null);
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue", null);
            return lesRevues;
        }

        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            String jsonIdDocument = convertToJson("id", idDocument);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument, null);
            return lesExemplaires;
        }

        /// <summary>
        /// Ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Ajoute un livre en base de données
        /// </summary>
        /// <param name="livre">livre à insérer</param>
        /// <returns>true si l'insertion a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie un livre en base de données
        /// </summary>
        /// <param name="livre">livre à modifier</param>
        /// <returns>true si la modification a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un livre en base de données
        /// </summary>
        /// <param name="livre">livre à supprimer</param>
        /// <returns>true si la suppression a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Ajoute un DVD en base de données
        /// </summary>
        /// <param name="dvd">dvd à insérer</param>
        /// <returns>true si l'insertion a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie un DVD en base de données
        /// </summary>
        /// <param name="dvd">dvd à modifier</param>
        /// <returns>true si la modification a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un DVD en base de données
        /// </summary>
        /// <param name="dvd">dvd à supprimer</param>
        /// <returns>true si la suppression a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Ajoute une revue en base de données
        /// </summary>
        /// <param name="revue">revue à insérer</param>
        /// <returns>true si l'insertion a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie une revue en base de données
        /// </summary>
        /// <param name="revue">revue à modifier</param>
        /// <returns>true si la modification a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime une revue en base de données
        /// </summary>
        /// <param name="revue">revue à supprimer</param>
        /// <returns>true si la suppression a pu se faire</returns>
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api
        /// </summary>
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
                    Console.WriteLine("code erreur = " + code + " message = " + (String)retour["message"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors de l'accès à l'API : " + e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Convertit en json un couple nom/valeur
        /// </summary>
        private String convertToJson(Object nom, Object valeur)
        {
            Dictionary<Object, Object> dictionary = new Dictionary<Object, Object>();
            dictionary.Add(nom, valeur);
            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// </summary>
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
    }
}