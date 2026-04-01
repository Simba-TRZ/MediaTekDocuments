using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class DvdTests
    {
        [TestMethod]
        public void Constructeur_ProprietesSontCorrectementInitialisees()
        {
            Dvd dvd = new Dvd("2", "Titre DVD", "image.jpg", 120, "Réalisateur Test",
                              "Synopsis test", "idGenre1", "Action",
                              "idPublic1", "Adultes", "idRayon1", "Cinéma");

            Assert.AreEqual("2", dvd.Id);
            Assert.AreEqual("Titre DVD", dvd.Titre);
            Assert.AreEqual("Réalisateur Test", dvd.Realisateur);
            Assert.AreEqual(120, dvd.Duree);
            Assert.AreEqual("Synopsis test", dvd.Synopsis);
            Assert.AreEqual("Action", dvd.Genre);
            Assert.AreEqual("Adultes", dvd.Public);
            Assert.AreEqual("Cinéma", dvd.Rayon);
        }
    }
}