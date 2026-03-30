using System;
using System.Windows.Forms;
using MediaTekDocuments.view;
using MediaTekDocuments.model;

namespace MediaTekDocuments
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmLogin frmLogin = new FrmLogin();
            if (frmLogin.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Utilisateur utilisateur = frmLogin.UtilisateurConnecte;
                if (utilisateur.IdService == "00002")
                {
                    MessageBox.Show("Vos droits ne sont pas suffisants pour accéder à cette application.", "Accès refusé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Application.Run(new FrmMediatek(utilisateur));
            }
        }
    }
}