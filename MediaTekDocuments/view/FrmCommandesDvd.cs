using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;

namespace MediaTekDocuments.view
{
    public partial class FrmCommandesDvd : Form
    {
        private readonly FrmMediatekController controller;
        private List<CommandeDocument> lesCommandes = new List<CommandeDocument>();
        private readonly BindingSource bdgCommandesListe = new BindingSource();
        private readonly BindingSource bdgSuivi = new BindingSource();

        public FrmCommandesDvd(FrmMediatekController controller)
        {
            InitializeComponent();
            this.controller = controller;
            RemplirComboSuivi();
        }

        private void RemplirComboSuivi()
        {
            List<Suivi> lesSuivi = controller.GetAllSuivi();
            bdgSuivi.DataSource = lesSuivi;
            cbxSuivi.DataSource = bdgSuivi;
            cbxSuivi.DisplayMember = "Libelle";
            cbxSuivi.ValueMember = "Id";
            cbxSuivi.SelectedIndex = -1;
        }

        private void BtnRechercherDvd_Click(object sender, EventArgs e)
        {
            if (!txbNumeroDvd.Text.Equals(""))
            {
                lesCommandes = controller.GetCommandesLivreDvd(txbNumeroDvd.Text);
                RemplirCommandesListe(lesCommandes);
            }
            else
            {
                MessageBox.Show("Veuillez saisir un numéro de DVD", "Information");
            }
        }

        private void RemplirCommandesListe(List<CommandeDocument> commandes)
        {
            bdgCommandesListe.DataSource = commandes;
            dgvCommandesListe.DataSource = bdgCommandesListe;
            dgvCommandesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            if (dgvCommandesListe.Columns.Contains("IdLivreDvd"))
                dgvCommandesListe.Columns["IdLivreDvd"].Visible = false;
            if (dgvCommandesListe.Columns.Contains("IdSuivi"))
                dgvCommandesListe.Columns["IdSuivi"].Visible = false;
        }

        private void DgvCommandesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCommandesListe.CurrentCell != null)
            {
                try
                {
                    CommandeDocument commande = (CommandeDocument)bdgCommandesListe.List[bdgCommandesListe.Position];
                    cbxSuivi.SelectedValue = commande.IdSuivi;
                }
                catch { }
            }
        }

        private void DgvCommandesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandesListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "DateCommande":
                    sortedList = lesCommandes.OrderByDescending(o => o.DateCommande).ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandes.OrderBy(o => o.Montant).ToList();
                    break;
                case "NbExemplaire":
                    sortedList = lesCommandes.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Suivi":
                    sortedList = lesCommandes.OrderBy(o => o.Suivi).ToList();
                    break;
            }
            if (sortedList.Count > 0)
                RemplirCommandesListe(sortedList);
        }

        private void BtnValiderCommande_Click(object sender, EventArgs e)
        {
            if (txbNumeroDvd.Text.Equals(""))
            {
                MessageBox.Show("Veuillez saisir un numéro de DVD", "Information");
                return;
            }
            if (txbNumeroCommande.Text.Equals("") || txbMontant.Text.Equals("") || txbNbExemplaires.Text.Equals(""))
            {
                MessageBox.Show("Tous les champs sont obligatoires", "Information");
                return;
            }
            try
            {
                CommandeDocument commande = new CommandeDocument(
                    txbNumeroCommande.Text,
                    dtpDateCommande.Value.ToString("yyyy-MM-dd"),
                    double.Parse(txbMontant.Text, System.Globalization.CultureInfo.InvariantCulture),
                    int.Parse(txbNbExemplaires.Text),
                    txbNumeroDvd.Text,
                    "00001",
                    "En cours"
                );
                if (controller.CreerCommandeLivreDvd(commande))
                {
                    lesCommandes = controller.GetCommandesLivreDvd(txbNumeroDvd.Text);
                    RemplirCommandesListe(lesCommandes);
                    MessageBox.Show("Commande enregistrée avec succès", "Information");
                    txbNumeroCommande.Text = "";
                    txbMontant.Text = "";
                    txbNbExemplaires.Text = "";
                }
                else
                {
                    MessageBox.Show("Erreur lors de l'enregistrement", "Erreur");
                }
            }
            catch
            {
                MessageBox.Show("Montant et nombre d'exemplaires doivent être numériques", "Erreur");
            }
        }

        private void BtnModifierSuivi_Click(object sender, EventArgs e)
        {
            if (bdgCommandesListe.Current == null)
            {
                MessageBox.Show("Aucune commande sélectionnée", "Information");
                return;
            }
            if (cbxSuivi.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner une étape de suivi", "Information");
                return;
            }
            CommandeDocument commande = (CommandeDocument)bdgCommandesListe.Current;
            Suivi nouveauSuivi = (Suivi)cbxSuivi.SelectedItem;

            if ((commande.IdSuivi == "00003" || commande.IdSuivi == "00004") &&
                (nouveauSuivi.Id == "00001" || nouveauSuivi.Id == "00002"))
            {
                MessageBox.Show("Une commande livrée ou réglée ne peut pas revenir à une étape précédente", "Erreur");
                return;
            }
            if (nouveauSuivi.Id == "00004" && commande.IdSuivi != "00003")
            {
                MessageBox.Show("Une commande ne peut être réglée que si elle est livrée", "Erreur");
                return;
            }

            commande.IdSuivi = nouveauSuivi.Id;
            if (controller.ModifierSuiviCommande(commande))
            {
                lesCommandes = controller.GetCommandesLivreDvd(txbNumeroDvd.Text);
                RemplirCommandesListe(lesCommandes);
                MessageBox.Show("Suivi modifié avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }

        private void BtnSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (bdgCommandesListe.Current == null)
            {
                MessageBox.Show("Aucune commande sélectionnée", "Information");
                return;
            }
            CommandeDocument commande = (CommandeDocument)bdgCommandesListe.Current;
            if (commande.IdSuivi == "00003" || commande.IdSuivi == "00004")
            {
                MessageBox.Show("Impossible de supprimer une commande livrée ou réglée", "Erreur");
                return;
            }
            if (MessageBox.Show("Confirmer la suppression ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (controller.SupprimerCommandeLivreDvd(commande))
                {
                    lesCommandes = controller.GetCommandesLivreDvd(txbNumeroDvd.Text);
                    RemplirCommandesListe(lesCommandes);
                    MessageBox.Show("Commande supprimée avec succès", "Information");
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression", "Erreur");
                }
            }
        }
    }
}