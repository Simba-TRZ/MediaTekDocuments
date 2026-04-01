using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class LivreTests
    {
        [TestMethod]
        public void Constructeur_ProprietesSontCorrectementInitialisees()
        {
            Livre livre = new Livre("1", "Titre Test", "image.jpg", "123456789",
                                   "Auteur Test", "Collection Test",
                                   "idGenre1", "Roman", "idPublic1", "Adultes",
                                   "idRayon1", "Littérature");

            Assert.AreEqual("1", livre.Id);
            Assert.AreEqual("Titre Test", livre.Titre);
            Assert.AreEqual("image.jpg", livre.Image);
            Assert.AreEqual("123456789", livre.Isbn);
            Assert.AreEqual("Auteur Test", livre.Auteur);
            Assert.AreEqual("Collection Test", livre.Collection);
            Assert.AreEqual("Roman", livre.Genre);
            Assert.AreEqual("Adultes", livre.Public);
            Assert.AreEqual("Littérature", livre.Rayon);
        }
    }
}