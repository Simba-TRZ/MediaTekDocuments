using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        internal FrmMediatek()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
            try
            {
                AfficherAlerteAbonnements();
            }
            catch { }
        }

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Affiche une alerte si des abonnements expirent dans moins de 30 jours
        /// </summary>
        private void AfficherAlerteAbonnements()
        {
            List<Abonnement> abonnements = controller.GetAbonnementsExpiresBientot();
            if (abonnements != null && abonnements.Count > 0)
            {
                string message = "Les abonnements suivants expirent dans moins de 30 jours :\n\n";
                foreach (Abonnement abo in abonnements)
                {
                    message += $"• {abo.Titre} — fin le {abo.DateFinAbonnement}\n";
                }
                MessageBox.Show(message, "Alerte abonnements", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Onglet Livres
        private readonly BindingSource bdgLivresListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();
        private readonly BindingSource bdgExemplairesLivres = new BindingSource();
        private List<Exemplaire> lesExemplairesLivre = new List<Exemplaire>();
        private readonly BindingSource bdgEtats = new BindingSource();
        private List<Categorie> lesEtats = new List<Categorie>();

        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            lesEtats = controller.GetAllEtats(); // NOUVEAU
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            // NOUVEAU : remplir combo états
            bdgEtats.DataSource = lesEtats;
            cbxLivresEtat.DataSource = bdgEtats;
            cbxLivresEtat.DisplayMember = "Libelle";
            cbxLivresEtat.ValueMember = "Id";
            cbxLivresEtat.SelectedIndex = -1;
            RemplirLivresListeComplete();
        }

        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                    ChargerExemplairesLivre(livre.Id);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        private void DgvLivresExemplaires_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string col = dgvLivresExemplaires.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sorted = new List<Exemplaire>(lesExemplairesLivre);
            switch (col)
            {
                case "Numero":
                    sorted = lesExemplairesLivre.OrderBy(o => o.Numero).ToList();
                    break;
                case "DateAchat":
                    sorted = lesExemplairesLivre.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Etat":
                    sorted = lesExemplairesLivre.OrderBy(o => o.IdEtat).ToList();
                    break;
            }
            bdgExemplairesLivres.DataSource = sorted;
        }

        private void DgvLivresExemplaires_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresExemplaires.CurrentCell != null)
            {
                Exemplaire ex = (Exemplaire)bdgExemplairesLivres.List[bdgExemplairesLivres.Position];
                cbxLivresEtat.SelectedValue = ex.IdEtat;
            }
        }

        private void BtnLivresModifierEtat_Click(object sender, EventArgs e)
        {
            if (bdgExemplairesLivres.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner un exemplaire", "Information");
                return;
            }
            if (cbxLivresEtat.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner un état", "Information");
                return;
            }
            Exemplaire ex = (Exemplaire)bdgExemplairesLivres.Current;
            Etat etat = (Etat)cbxLivresEtat.SelectedItem;
            ex.IdEtat = etat.Id;
            if (controller.ModifierEtatExemplaire(ex))
            {
                Livre livre = (Livre)bdgLivresListe.Current;
                ChargerExemplairesLivre(livre.Id);
                MessageBox.Show("État modifié avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }

        private void BtnLivresSupprimerExemplaire_Click(object sender, EventArgs e)
        {
            if (bdgExemplairesLivres.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner un exemplaire", "Information");
                return;
            }
            Exemplaire ex = (Exemplaire)bdgExemplairesLivres.Current;
            if (MessageBox.Show("Confirmer la suppression de l'exemplaire n°" + ex.Numero + " ?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (controller.SupprimerExemplaire(ex))
                {
                    Livre livre = (Livre)bdgLivresListe.Current;
                    ChargerExemplairesLivre(livre.Id);
                    MessageBox.Show("Exemplaire supprimé avec succès", "Information");
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression", "Erreur");
                }
            }
        }

        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }

        /// <summary>
        /// Ajouter un livre
        /// </summary>
        private void BtnLivresAjouter_Click(object sender, EventArgs e)
        {
            if (txbLivresNumero.Text.Equals("") || txbLivresTitre.Text.Equals(""))
            {
                MessageBox.Show("Numéro et titre obligatoires", "Information");
                return;
            }
            Livre livre = (Livre)bdgLivresListe.Current;
            if (controller.CreerLivre(livre))
            {
                lesLivres = controller.GetAllLivres();
                RemplirLivresListeComplete();
                MessageBox.Show("Livre ajouté avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout", "Erreur");
            }
        }

        /// <summary>
        /// Modifier un livre
        /// </summary>
        private void BtnLivresModifier_Click(object sender, EventArgs e)
        {
            if (bdgLivresListe.Current == null)
            {
                MessageBox.Show("Aucun livre sélectionné", "Information");
                return;
            }
            Livre livre = (Livre)bdgLivresListe.Current;
            if (controller.ModifierLivre(livre))
            {
                lesLivres = controller.GetAllLivres();
                RemplirLivresListeComplete();
                MessageBox.Show("Livre modifié avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }

        /// <summary>
        /// Supprimer un livre
        /// </summary>
        private void BtnLivresSupprimer_Click(object sender, EventArgs e)
        {
            if (bdgLivresListe.Current == null)
            {
                MessageBox.Show("Aucun livre sélectionné", "Information");
                return;
            }
            Livre livre = (Livre)bdgLivresListe.Current;
            if (MessageBox.Show("Confirmer la suppression de " + livre.Titre + " ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (controller.SupprimerLivre(livre))
                {
                    lesLivres = controller.GetAllLivres();
                    RemplirLivresListeComplete();
                    MessageBox.Show("Livre supprimé avec succès", "Information");
                }
                else
                {
                    MessageBox.Show("Erreur : le livre a peut-être des exemplaires ou des commandes", "Erreur");
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de gestion des commandes du livre sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCommandesLivres_Click(object sender, EventArgs e)
        {
            if (bdgLivresListe.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner un livre", "Information");
                return;
            }
            FrmCommandesLivres frmCommandesLivres = new FrmCommandesLivres(controller);
            frmCommandesLivres.ShowDialog();
        }

        private void ChargerExemplairesLivre(string idLivre)
        {
            lesExemplairesLivre = controller.GetExemplairesLivreDvd(idLivre);
            bdgExemplairesLivres.DataSource = lesExemplairesLivre;
            dgvLivresExemplaires.DataSource = bdgExemplairesLivres;
            dgvLivresExemplaires.Columns["id"].Visible = false;
            dgvLivresExemplaires.Columns["photo"].Visible = false;
            dgvLivresExemplaires.Columns["idEtat"].Visible = false;
            dgvLivresExemplaires.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        #endregion

        #region Onglet Dvd
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private List<Dvd> lesDvd = new List<Dvd>();

        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            lesEtats = controller.GetAllEtats();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);
            bdgEtats.DataSource = lesEtats;
            cbxDvdEtat.DataSource = bdgEtats;
            cbxDvdEtat.DisplayMember = "Libelle";
            cbxDvdEtat.ValueMember = "Id";
            cbxDvdEtat.SelectedIndex = -1;
            RemplirDvdListeComplete();
        }

        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                    ChargerExemplairesDvd(dvd.Id);
                }
                catch { VideDvdZones(); }
            }
            else { VideDvdInfos(); }
        }

        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        private readonly BindingSource bdgExemplairesDvd = new BindingSource();
        private List<Exemplaire> lesExemplairesDvd = new List<Exemplaire>();
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }

        /// <summary>
        /// Ajouter un DVD
        /// </summary>
        private void BtnDvdAjouter_Click(object sender, EventArgs e)
        {
            if (txbDvdNumero.Text.Equals("") || txbDvdTitre.Text.Equals(""))
            {
                MessageBox.Show("Numéro et titre obligatoires", "Information");
                return;
            }
            Dvd dvd = (Dvd)bdgDvdListe.Current;
            if (controller.CreerDvd(dvd))
            {
                lesDvd = controller.GetAllDvd();
                RemplirDvdListeComplete();
                MessageBox.Show("DVD ajouté avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout", "Erreur");
            }
        }

        /// <summary>
        /// Modifier un DVD
        /// </summary>
        private void BtnDvdModifier_Click(object sender, EventArgs e)
        {
            if (bdgDvdListe.Current == null)
            {
                MessageBox.Show("Aucun DVD sélectionné", "Information");
                return;
            }
            Dvd dvd = (Dvd)bdgDvdListe.Current;
            if (controller.ModifierDvd(dvd))
            {
                lesDvd = controller.GetAllDvd();
                RemplirDvdListeComplete();
                MessageBox.Show("DVD modifié avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }

        /// <summary>
        /// Supprimer un DVD
        /// </summary>
        private void BtnDvdSupprimer_Click(object sender, EventArgs e)
        {
            if (bdgDvdListe.Current == null)
            {
                MessageBox.Show("Aucun DVD sélectionné", "Information");
                return;
            }
            Dvd dvd = (Dvd)bdgDvdListe.Current;
            if (MessageBox.Show("Confirmer la suppression de " + dvd.Titre + " ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (controller.SupprimerDvd(dvd))
                {
                    lesDvd = controller.GetAllDvd();
                    RemplirDvdListeComplete();
                    MessageBox.Show("DVD supprimé avec succès", "Information");
                }
                else
                {
                    MessageBox.Show("Erreur : le DVD a peut-être des exemplaires ou des commandes", "Erreur");
                }
            }
        }

        private void ChargerExemplairesDvd(string idDvd)
        {
            lesExemplairesDvd = controller.GetExemplairesLivreDvd(idDvd);
            bdgExemplairesDvd.DataSource = lesExemplairesDvd;
            dgvDvdExemplaires.DataSource = bdgExemplairesDvd;
            dgvDvdExemplaires.Columns["id"].Visible = false;
            dgvDvdExemplaires.Columns["photo"].Visible = false;
            dgvDvdExemplaires.Columns["idEtat"].Visible = false;
            dgvDvdExemplaires.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void DgvDvdExemplaires_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdExemplaires.CurrentCell != null)
            {
                Exemplaire ex = (Exemplaire)bdgExemplairesDvd.List[bdgExemplairesDvd.Position];
                cbxDvdEtat.SelectedValue = ex.IdEtat;
            }
        }

        private void DgvDvdExemplaires_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string col = dgvDvdExemplaires.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sorted = new List<Exemplaire>(lesExemplairesDvd);
            switch (col)
            {
                case "Numero":
                    sorted = lesExemplairesDvd.OrderBy(o => o.Numero).ToList();
                    break;
                case "DateAchat":
                    sorted = lesExemplairesDvd.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Etat":
                    sorted = lesExemplairesDvd.OrderBy(o => o.IdEtat).ToList();
                    break;
            }
            bdgExemplairesDvd.DataSource = sorted;
        }

        private void BtnDvdModifierEtat_Click(object sender, EventArgs e)
        {
            if (bdgExemplairesDvd.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner un exemplaire", "Information");
                return;
            }
            if (cbxDvdEtat.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner un état", "Information");
                return;
            }
            Exemplaire ex = (Exemplaire)bdgExemplairesDvd.Current;
            Etat etat = (Etat)cbxDvdEtat.SelectedItem;
            ex.IdEtat = etat.Id;
            if (controller.ModifierEtatExemplaire(ex))
            {
                Dvd dvd = (Dvd)bdgDvdListe.Current;
                ChargerExemplairesDvd(dvd.Id);
                MessageBox.Show("État modifié avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }

        private void BtnDvdSupprimerExemplaire_Click(object sender, EventArgs e)
        {
            if (bdgExemplairesDvd.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner un exemplaire", "Information");
                return;
            }
            Exemplaire ex = (Exemplaire)bdgExemplairesDvd.Current;
            if (MessageBox.Show("Confirmer la suppression de l'exemplaire n°" + ex.Numero + " ?",
                "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (controller.SupprimerExemplaire(ex))
                {
                    Dvd dvd = (Dvd)bdgDvdListe.Current;
                    ChargerExemplairesDvd(dvd.Id);
                    MessageBox.Show("Exemplaire supprimé avec succès", "Information");
                }
                else
                {
                    MessageBox.Show("Erreur lors de la suppression", "Erreur");
                }
            }
        }

        private void BtnCommandesDvd_Click(object sender, EventArgs e)
        {
            if (bdgDvdListe.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner un DVD", "Information");
                return;
            }
            FrmCommandesDvd frmCommandesDvd = new FrmCommandesDvd(controller);
            frmCommandesDvd.ShowDialog();
        }
        #endregion

        #region Onglet Revues
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private List<Revue> lesRevues = new List<Revue>();

        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }

        /// <summary>
        /// Ajouter une revue
        /// </summary>
        private void BtnRevuesAjouter_Click(object sender, EventArgs e)
        {
            if (txbRevuesNumero.Text.Equals("") || txbRevuesTitre.Text.Equals(""))
            {
                MessageBox.Show("Numéro et titre obligatoires", "Information");
                return;
            }
            Revue revue = (Revue)bdgRevuesListe.Current;
            if (controller.CreerRevue(revue))
            {
                lesRevues = controller.GetAllRevues();
                RemplirRevuesListeComplete();
                MessageBox.Show("Revue ajoutée avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de l'ajout", "Erreur");
            }
        }

        /// <summary>
        /// Modifier une revue
        /// </summary>
        private void BtnRevuesModifier_Click(object sender, EventArgs e)
        {
            if (bdgRevuesListe.Current == null)
            {
                MessageBox.Show("Aucune revue sélectionnée", "Information");
                return;
            }
            Revue revue = (Revue)bdgRevuesListe.Current;
            if (controller.ModifierRevue(revue))
            {
                lesRevues = controller.GetAllRevues();
                RemplirRevuesListeComplete();
                MessageBox.Show("Revue modifiée avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }

        /// <summary>
        /// Supprimer une revue
        /// </summary>
        private void BtnRevuesSupprimer_Click(object sender, EventArgs e)
        {
            if (bdgRevuesListe.Current == null)
            {
                MessageBox.Show("Aucune revue sélectionnée", "Information");
                return;
            }
            Revue revue = (Revue)bdgRevuesListe.Current;
            if (MessageBox.Show("Confirmer la suppression de " + revue.Titre + " ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (controller.SupprimerRevue(revue))
                {
                    lesRevues = controller.GetAllRevues();
                    RemplirRevuesListeComplete();
                    MessageBox.Show("Revue supprimée avec succès", "Information");
                }
                else
                {
                    MessageBox.Show("Erreur : la revue a peut-être des exemplaires", "Erreur");
                }
            }
        }

        /// <summary>
        /// Ouvre la fenêtre de gestion des abonnements de la revue sélectionnée
        /// </summary>
        private void BtnAbonnementsRevues_Click(object sender, EventArgs e)
        {
            if (bdgRevuesListe.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner une revue", "Information");
                return;
            }
            Revue revue = (Revue)bdgRevuesListe.Current;
            FrmAbonnements frmAbonnements = new FrmAbonnements(controller, revue);
            frmAbonnements.ShowDialog();
        }
        #endregion

        #region Onglet Parutions
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        const string ETATNEUF = "00001";

        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            lesEtats = controller.GetAllEtats();
            bdgEtats.DataSource = lesEtats;
            cbxParutionsEtat.DataSource = bdgEtats;
            cbxParutionsEtat.DisplayMember = "Libelle";
            cbxParutionsEtat.ValueMember = "Id";
            cbxParutionsEtat.SelectedIndex = -1;
            txbReceptionRevueNumero.Text = "";
        }

        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.Columns["photo"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
                if (dgvReceptionExemplairesListe.Columns["etat"] != null)
                    dgvReceptionExemplairesListe.Columns["etat"].DisplayIndex = 2;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        private void AfficheReceptionRevueInfos(Revue revue)
        {
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            AfficheReceptionExemplairesRevue();
        }

        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
                cbxParutionsEtat.SelectedValue = exemplaire.IdEtat;
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }

        private void BtnParutionsModifierEtat_Click(object sender, EventArgs e)
        {
            if (bdgExemplairesListe.Current == null)
            {
                MessageBox.Show("Veuillez sélectionner une parution", "Information");
                return;
            }
            if (cbxParutionsEtat.SelectedIndex < 0)
            {
                MessageBox.Show("Veuillez sélectionner un état", "Information");
                return;
            }
            Exemplaire ex = (Exemplaire)bdgExemplairesListe.Current;
            Etat etat = (Etat)cbxParutionsEtat.SelectedItem;
            ex.IdEtat = etat.Id;
            if (controller.ModifierEtatExemplaire(ex))
            {
                AfficheReceptionExemplairesRevue();
                MessageBox.Show("État modifié avec succès", "Information");
            }
            else
            {
                MessageBox.Show("Erreur lors de la modification", "Erreur");
            }
        }
        #endregion
    }
}