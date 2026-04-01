using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class RevueTests
    {
        [TestMethod]
        public void Constructeur_ProprietesSontCorrectementInitialisees()
        {
            Revue revue = new Revue("3", "Titre Revue", "image.jpg",
                                   "idGenre1", "Presse", "idPublic1", "Adultes",
                                   "idRayon1", "Magazines", "MS", 52);

            Assert.AreEqual("3", revue.Id);
            Assert.AreEqual("Titre Revue", revue.Titre);
            Assert.AreEqual("MS", revue.Periodicite);
            Assert.AreEqual(52, revue.DelaiMiseADispo);
            Assert.AreEqual("Presse", revue.Genre);
            Assert.AreEqual("Adultes", revue.Public);
        }
    }
}