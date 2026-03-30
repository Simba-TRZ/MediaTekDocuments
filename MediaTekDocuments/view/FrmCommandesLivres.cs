using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Fenêtre de gestion des commandes de livres
    /// </summary>
    public partial class FrmCommandesLivres : Form
    {
        /// <summary>
        /// Contrôleur lié à ce formulaire
        /// </summary>
        private readonly FrmMediatekController controller;
        /// <summary>
        /// Liste des commandes du livre sélectionné
        /// </summary>
        private List<CommandeDocument> lesCommandes = new List<CommandeDocument>();
        /// <summary>
        /// Binding source pour la liste des commandes
        /// </summary>
        private readonly BindingSource bdgCommandesListe = new BindingSource();
        /// <summary>
        /// Binding source pour le combo suivi
        /// </summary>
        private readonly BindingSource bdgSuivi = new BindingSource();

        /// <summary>
        /// Constructeur : récupère le contrôleur
        /// </summary>
        /// <param name="controller">le contrôleur</param>
        public FrmCommandesLivres(FrmMediatekController controller)
        {
            InitializeComponent();
            this.controller = controller;
            RemplirComboSuivi();
        }

        /// <summary>
        /// Remplit le combo des étapes de suivi
        /// </summary>
        private void RemplirComboSuivi()
        {
            List<Suivi> lesSuivi = controller.GetAllSuivi();
            bdgSuivi.DataSource = lesSuivi;
            cbxSuivi.DataSource = bdgSuivi;
            cbxSuivi.DisplayMember = "Libelle";
            cbxSuivi.ValueMember = "Id";
            cbxSuivi.SelectedIndex = -1;
        }

        /// <summary>
        /// Recherche et affiche les commandes du livre saisi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRechercherLivre_Click(object sender, EventArgs e)
        {
            if (!txbNumeroLivre.Text.Equals(""))
            {
                lesCommandes = controller.GetCommandesLivreDvd(txbNumeroLivre.Text);
                RemplirCommandesListe(lesCommandes);
            }
            else
            {
                MessageBox.Show("Veuillez saisir un numéro de livre", "Information");
            }
        }

        /// <summary>
        /// Remplit le datagrid avec la liste des commandes
        /// </summary>
        /// <param name="commandes">liste des commandes</param>
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

        /// <summary>
        /// Sur la sélection d'une commande, affiche son suivi dans le combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Enregistre une nouvelle commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnValiderCommande_Click(object sender, EventArgs e)
        {
            if (txbNumeroLivre.Text.Equals(""))
            {
                MessageBox.Show("Veuillez saisir un numéro de livre", "Information");
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
                    txbNumeroLivre.Text,
                    "00001",
                    "En cours"
                );
                if (controller.CreerCommandeLivreDvd(commande))
                {
                    lesCommandes = controller.GetCommandesLivreDvd(txbNumeroLivre.Text);
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

        /// <summary>
        /// Modifie le suivi de la commande sélectionnée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            // Règles de gestion
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
                lesCommandes = controller.GetCommandesLivreDvd(txbNumeroLivre.Text);
                RemplirCommandesListe(lesCommandes);
                MessageBox.Show("Suivi modifié avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }

        /// <summary>
        /// Supprime la commande sélectionnée si elle n'est pas livrée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    lesCommandes = controller.GetCommandesLivreDvd(txbNumeroLivre.Text);
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