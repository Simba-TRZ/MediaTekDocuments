using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;

namespace MediaTekDocuments.view
{
    public partial class FrmLogin : Form
    {
        private readonly FrmMediatekController controller;
        public Utilisateur UtilisateurConnecte { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
        }

        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            if (txbLogin.Text.Equals("") || txbPassword.Text.Equals(""))
            {
                MessageBox.Show("Login et mot de passe obligatoires", "Information");
                return;
            }
            Utilisateur utilisateur = controller.GetUtilisateur(txbLogin.Text);
            if (utilisateur == null)
            {
                MessageBox.Show("Login incorrect", "Erreur");
                return;
            }
            if (!BCrypt.Net.BCrypt.Verify(txbPassword.Text, utilisateur.Password))
            {
                MessageBox.Show("Mot de passe incorrect", "Erreur");
                return;
            }
            UtilisateurConnecte = utilisateur;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}