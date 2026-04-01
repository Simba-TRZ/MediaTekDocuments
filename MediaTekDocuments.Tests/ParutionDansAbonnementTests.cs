using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocuments.Tests
{
    [TestClass]
    public class ParutionDansAbonnementTests
    {
        private bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
        {
            return dateParution >= dateCommande && dateParution <= dateFinAbonnement;
        }

        [TestMethod]
        public void DateParution_DansAbonnement_RetourneTrue()
        {
            DateTime dateCommande = new DateTime(2026, 1, 1);
            DateTime dateFin = new DateTime(2026, 12, 31);
            DateTime dateParution = new DateTime(2026, 6, 15);
            Assert.IsTrue(ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void DateParution_AvantAbonnement_RetourneFalse()
        {
            DateTime dateCommande = new DateTime(2026, 1, 1);
            DateTime dateFin = new DateTime(2026, 12, 31);
            DateTime dateParution = new DateTime(2025, 12, 31);
            Assert.IsFalse(ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void DateParution_ApresAbonnement_RetourneFalse()
        {
            DateTime dateCommande = new DateTime(2026, 1, 1);
            DateTime dateFin = new DateTime(2026, 12, 31);
            DateTime dateParution = new DateTime(2027, 1, 1);
            Assert.IsFalse(ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void DateParution_EgaleADateCommande_RetourneTrue()
        {
            DateTime dateCommande = new DateTime(2026, 1, 1);
            DateTime dateFin = new DateTime(2026, 12, 31);
            DateTime dateParution = new DateTime(2026, 1, 1);
            Assert.IsTrue(ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }

        [TestMethod]
        public void DateParution_EgaleADateFin_RetourneTrue()
        {
            DateTime dateCommande = new DateTime(2026, 1, 1);
            DateTime dateFin = new DateTime(2026, 12, 31);
            DateTime dateParution = new DateTime(2026, 12, 31);
            Assert.IsTrue(ParutionDansAbonnement(dateCommande, dateFin, dateParution));
        }
    }
}