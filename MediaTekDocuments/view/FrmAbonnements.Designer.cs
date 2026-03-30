namespace MediaTekDocuments.view
{
    partial class FrmAbonnements
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Contrôles
            this.txbNumero = new System.Windows.Forms.TextBox();
            this.txbTitre = new System.Windows.Forms.TextBox();
            this.txbPeriodicite = new System.Windows.Forms.TextBox();
            this.dgvAbonnements = new System.Windows.Forms.DataGridView();
            this.grpSaisie = new System.Windows.Forms.GroupBox();
            this.txbNouvelId = new System.Windows.Forms.TextBox();
            this.txbMontant = new System.Windows.Forms.TextBox();
            this.dtpDateCommande = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFin = new System.Windows.Forms.DateTimePicker();
            this.btnAjouter = new System.Windows.Forms.Button();
            this.btnSupprimer = new System.Windows.Forms.Button();
            // Labels
            this.lblNumero = new System.Windows.Forms.Label();
            this.lblTitre = new System.Windows.Forms.Label();
            this.lblPeriodicite = new System.Windows.Forms.Label();
            this.lblListe = new System.Windows.Forms.Label();
            this.lblNouvelId = new System.Windows.Forms.Label();
            this.lblMontant = new System.Windows.Forms.Label();
            this.lblDateCommande = new System.Windows.Forms.Label();
            this.lblDateFin = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAbonnements)).BeginInit();
            this.grpSaisie.SuspendLayout();
            this.SuspendLayout();

            // lblNumero
            this.lblNumero.AutoSize = true;
            this.lblNumero.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNumero.Location = new System.Drawing.Point(12, 15);
            this.lblNumero.Text = "Numéro revue :";

            // txbNumero
            this.txbNumero.Location = new System.Drawing.Point(150, 12);
            this.txbNumero.ReadOnly = true;
            this.txbNumero.Size = new System.Drawing.Size(100, 20);

            // lblTitre
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTitre.Location = new System.Drawing.Point(12, 40);
            this.lblTitre.Text = "Titre :";

            // txbTitre
            this.txbTitre.Location = new System.Drawing.Point(150, 37);
            this.txbTitre.ReadOnly = true;
            this.txbTitre.Size = new System.Drawing.Size(350, 20);

            // lblPeriodicite
            this.lblPeriodicite.AutoSize = true;
            this.lblPeriodicite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblPeriodicite.Location = new System.Drawing.Point(12, 65);
            this.lblPeriodicite.Text = "Périodicité :";

            // txbPeriodicite
            this.txbPeriodicite.Location = new System.Drawing.Point(150, 62);
            this.txbPeriodicite.ReadOnly = true;
            this.txbPeriodicite.Size = new System.Drawing.Size(150, 20);

            // lblListe
            this.lblListe.AutoSize = true;
            this.lblListe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblListe.Location = new System.Drawing.Point(12, 95);
            this.lblListe.Text = "Liste des abonnements :";

            // dgvAbonnements
            this.dgvAbonnements.AllowUserToAddRows = false;
            this.dgvAbonnements.AllowUserToDeleteRows = false;
            this.dgvAbonnements.AllowUserToResizeColumns = false;
            this.dgvAbonnements.AllowUserToResizeRows = false;
            this.dgvAbonnements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAbonnements.Location = new System.Drawing.Point(12, 113);
            this.dgvAbonnements.MultiSelect = false;
            this.dgvAbonnements.Name = "dgvAbonnements";
            this.dgvAbonnements.ReadOnly = true;
            this.dgvAbonnements.RowHeadersVisible = false;
            this.dgvAbonnements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAbonnements.Size = new System.Drawing.Size(760, 200);
            this.dgvAbonnements.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvAbonnements_ColumnHeaderMouseClick);

            // grpSaisie
            this.grpSaisie.Location = new System.Drawing.Point(12, 325);
            this.grpSaisie.Size = new System.Drawing.Size(760, 130);
            this.grpSaisie.Text = "Nouvel abonnement";
            this.grpSaisie.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);

            // lblNouvelId
            this.lblNouvelId.AutoSize = true;
            this.lblNouvelId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNouvelId.Location = new System.Drawing.Point(6, 25);
            this.lblNouvelId.Text = "N° commande :";

            // txbNouvelId
            this.txbNouvelId.Location = new System.Drawing.Point(150, 22);
            this.txbNouvelId.Size = new System.Drawing.Size(100, 20);

            // lblDateCommande
            this.lblDateCommande.AutoSize = true;
            this.lblDateCommande.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDateCommande.Location = new System.Drawing.Point(6, 52);
            this.lblDateCommande.Text = "Date commande :";

            // dtpDateCommande
            this.dtpDateCommande.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateCommande.Location = new System.Drawing.Point(150, 49);
            this.dtpDateCommande.Size = new System.Drawing.Size(100, 20);

            // lblMontant
            this.lblMontant.AutoSize = true;
            this.lblMontant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMontant.Location = new System.Drawing.Point(270, 25);
            this.lblMontant.Text = "Montant (€) :";

            // txbMontant
            this.txbMontant.Location = new System.Drawing.Point(400, 22);
            this.txbMontant.Size = new System.Drawing.Size(100, 20);

            // lblDateFin
            this.lblDateFin.AutoSize = true;
            this.lblDateFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblDateFin.Location = new System.Drawing.Point(270, 52);
            this.lblDateFin.Text = "Date fin abonnement :";

            // dtpDateFin
            this.dtpDateFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFin.Location = new System.Drawing.Point(430, 49);
            this.dtpDateFin.Size = new System.Drawing.Size(100, 20);
            this.dtpDateFin.Value = System.DateTime.Now.AddYears(1);

            // btnAjouter
            this.btnAjouter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnAjouter.Location = new System.Drawing.Point(6, 95);
            this.btnAjouter.Size = new System.Drawing.Size(160, 25);
            this.btnAjouter.Text = "Enregistrer";
            this.btnAjouter.Click += new System.EventHandler(this.BtnAjouter_Click);

            // btnSupprimer
            this.btnSupprimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSupprimer.Location = new System.Drawing.Point(172, 95);
            this.btnSupprimer.Size = new System.Drawing.Size(160, 25);
            this.btnSupprimer.Text = "Supprimer";
            this.btnSupprimer.Click += new System.EventHandler(this.BtnSupprimer_Click);

            // Ajout des contrôles dans grpSaisie
            this.grpSaisie.Controls.Add(this.lblNouvelId);
            this.grpSaisie.Controls.Add(this.txbNouvelId);
            this.grpSaisie.Controls.Add(this.lblDateCommande);
            this.grpSaisie.Controls.Add(this.dtpDateCommande);
            this.grpSaisie.Controls.Add(this.lblMontant);
            this.grpSaisie.Controls.Add(this.txbMontant);
            this.grpSaisie.Controls.Add(this.lblDateFin);
            this.grpSaisie.Controls.Add(this.dtpDateFin);
            this.grpSaisie.Controls.Add(this.btnAjouter);
            this.grpSaisie.Controls.Add(this.btnSupprimer);

            // FrmAbonnements
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 470);
            this.Controls.Add(this.lblNumero);
            this.Controls.Add(this.txbNumero);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.txbTitre);
            this.Controls.Add(this.lblPeriodicite);
            this.Controls.Add(this.txbPeriodicite);
            this.Controls.Add(this.lblListe);
            this.Controls.Add(this.dgvAbonnements);
            this.Controls.Add(this.grpSaisie);
            this.Name = "FrmAbonnements";
            this.Text = "Gestion des abonnements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            ((System.ComponentModel.ISupportInitialize)(this.dgvAbonnements)).EndInit();
            this.grpSaisie.ResumeLayout(false);
            this.grpSaisie.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Déclarations
        private System.Windows.Forms.TextBox txbNumero;
        private System.Windows.Forms.TextBox txbTitre;
        private System.Windows.Forms.TextBox txbPeriodicite;
        private System.Windows.Forms.DataGridView dgvAbonnements;
        private System.Windows.Forms.GroupBox grpSaisie;
        private System.Windows.Forms.TextBox txbNouvelId;
        private System.Windows.Forms.TextBox txbMontant;
        private System.Windows.Forms.DateTimePicker dtpDateCommande;
        private System.Windows.Forms.DateTimePicker dtpDateFin;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Label lblNumero;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.Label lblPeriodicite;
        private System.Windows.Forms.Label lblListe;
        private System.Windows.Forms.Label lblNouvelId;
        private System.Windows.Forms.Label lblMontant;
        private System.Windows.Forms.Label lblDateCommande;
        private System.Windows.Forms.Label lblDateFin;
    }
}