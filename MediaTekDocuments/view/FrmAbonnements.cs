using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.view
{
    public partial class FrmAbonnements : Form
    {
        private readonly FrmMediatekController controller;
        private readonly Revue revue;
        private readonly BindingSource bdgAbonnements = new BindingSource();
        private List<Abonnement> lesAbonnements = new List<Abonnement>();

        public FrmAbonnements(FrmMediatekController controller, Revue revue)
        {
            InitializeComponent();
            this.controller = controller;
            this.revue = revue;
            txbNumero.Text = revue.Id;
            txbTitre.Text = revue.Titre;
            txbPeriodicite.Text = revue.Periodicite;
            ChargerAbonnements();
        }

        private void ChargerAbonnements()
        {
            lesAbonnements = controller.GetAbonnementsRevue(revue.Id);
            bdgAbonnements.DataSource = lesAbonnements;
            dgvAbonnements.DataSource = bdgAbonnements;
            dgvAbonnements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (dgvAbonnements.Columns.Contains("IdRevue"))
                dgvAbonnements.Columns["IdRevue"].Visible = false;
            if (dgvAbonnements.Columns.Contains("Id"))
                dgvAbonnements.Columns["Id"].Visible = false;
        }

        private void DgvAbonnements_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string col = dgvAbonnements.Columns[e.ColumnIndex].HeaderText;
            List<Abonnement> sorted = new List<Abonnement>(lesAbonnements);
            switch (col)
            {
                case "Id":
                    sorted.Sort((a, b) => string.Compare(a.Id, b.Id));
                    break;
                case "DateCommande":
                    sorted.Sort((a, b) => string.Compare(a.DateCommande, b.DateCommande));
                    sorted.Reverse();
                    break;
                case "Montant":
                    sorted.Sort((a, b) => a.Montant.CompareTo(b.Montant));
                    break;
                case "DateFinAbonnement":
                    sorted.Sort((a, b) => string.Compare(a.DateFinAbonnement, b.DateFinAbonnement));
                    sorted.Reverse();
                    break;
            }
            bdgAbonnements.DataSource = sorted;
        }

        private void BtnAjouter_Click(object sender, EventArgs e)
        {
            if (txbNouvelId.Text.Equals("") || txbMontant.Text.Equals(""))
            {
                MessageBox.Show("Numéro de commande et montant obligatoires", "Information");
                return;
            }
            if (!double.TryParse(txbMontant.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double montant))
            {
                MessageBox.Show("Le montant doit être un nombre", "Information");
                return;
            }
            if (dtpDateFin.Value.Date <= dtpDateCommande.Value.Date)
            {
                MessageBox.Show("La date de fin doit être postérieure à la date de commande", "Information");
                return;
            }
            Abonnement abonnement = new Abonnement(
                txbNouvelId.Text,
                dtpDateCommande.Value.ToString("yyyy-MM-dd"),
                montant,
                dtpDateFin.Value.ToString("yyyy-MM-dd"),
                revue.Id
            );
            if (controller.CreerAbonnement(abonnement))
            {
                ChargerAbonnements();
                ViderSaisie();
                MessageBox.Show("Abonnement ajouté avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout", "Erreur");
            }
        }

        private void BtnSupprimer_Click(object sender, EventArgs e)
        {
            if (bdgAbonnements.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner un abonnement", "Information");
                return;
            }
            Abonnement abonnement = (Abonnement)bdgAbonnements.Current;
            if (MessageBox.Show("Confirmer la suppression ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                List<Exemplaire> exemplaires = controller.GetExemplairesRevue(revue.Id);
                if (controller.SupprimerAbonnement(abonnement, exemplaires))
                {
                    ChargerAbonnements();
                    MessageBox.Show("Abonnement supprimé avec succès", "Information");
                }
                else
                {
                    MessageBox.Show("Suppression impossible : des exemplaires sont rattachés à cet abonnement", "Erreur");
                }
            }
        }

        private void ViderSaisie()
        {
            txbNouvelId.Text = "";
            txbMontant.Text = "";
            dtpDateCommande.Value = DateTime.Now;
            dtpDateFin.Value = DateTime.Now.AddYears(1);
        }
    }
}