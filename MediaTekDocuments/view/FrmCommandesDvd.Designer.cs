namespace MediaTekDocuments.view
{
    partial class FrmCommandesDvd
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpRecherche = new System.Windows.Forms.GroupBox();
            this.btnRechercherDvd = new System.Windows.Forms.Button();
            this.txbNumeroDvd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCommandesListe = new System.Windows.Forms.DataGridView();
            this.grpNouvelleCommande = new System.Windows.Forms.GroupBox();
            this.btnValiderCommande = new System.Windows.Forms.Button();
            this.txbNbExemplaires = new System.Windows.Forms.TextBox();
            this.txbMontant = new System.Windows.Forms.TextBox();
            this.dtpDateCommande = new System.Windows.Forms.DateTimePicker();
            this.txbNumeroCommande = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grpSuivi = new System.Windows.Forms.GroupBox();
            this.btnSupprimerCommande = new System.Windows.Forms.Button();
            this.btnModifierSuivi = new System.Windows.Forms.Button();
            this.cbxSuivi = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpRecherche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommandesListe)).BeginInit();
            this.grpNouvelleCommande.SuspendLayout();
            this.grpSuivi.SuspendLayout();
            this.SuspendLayout();
            // grpRecherche
            this.grpRecherche.Controls.Add(this.btnRechercherDvd);
            this.grpRecherche.Controls.Add(this.txbNumeroDvd);
            this.grpRecherche.Controls.Add(this.label1);
            this.grpRecherche.Location = new System.Drawing.Point(12, 12);
            this.grpRecherche.Name = "grpRecherche";
            this.grpRecherche.Size = new System.Drawing.Size(860, 55);
            this.grpRecherche.TabIndex = 0;
            this.grpRecherche.TabStop = false;
            this.grpRecherche.Text = "Recherche DVD";
            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numéro de document :";
            // txbNumeroDvd
            this.txbNumeroDvd.Location = new System.Drawing.Point(150, 20);
            this.txbNumeroDvd.Name = "txbNumeroDvd";
            this.txbNumeroDvd.Size = new System.Drawing.Size(100, 20);
            this.txbNumeroDvd.TabIndex = 1;
            // btnRechercherDvd
            this.btnRechercherDvd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnRechercherDvd.Location = new System.Drawing.Point(260, 18);
            this.btnRechercherDvd.Name = "btnRechercherDvd";
            this.btnRechercherDvd.Size = new System.Drawing.Size(96, 22);
            this.btnRechercherDvd.TabIndex = 2;
            this.btnRechercherDvd.Text = "Rechercher";
            this.btnRechercherDvd.UseVisualStyleBackColor = true;
            this.btnRechercherDvd.Click += new System.EventHandler(this.BtnRechercherDvd_Click);
            // dgvCommandesListe
            this.dgvCommandesListe.AllowUserToAddRows = false;
            this.dgvCommandesListe.AllowUserToDeleteRows = false;
            this.dgvCommandesListe.AllowUserToResizeColumns = false;
            this.dgvCommandesListe.AllowUserToResizeRows = false;
            this.dgvCommandesListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCommandesListe.Location = new System.Drawing.Point(12, 80);
            this.dgvCommandesListe.MultiSelect = false;
            this.dgvCommandesListe.Name = "dgvCommandesListe";
            this.dgvCommandesListe.ReadOnly = true;
            this.dgvCommandesListe.RowHeadersVisible = false;
            this.dgvCommandesListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCommandesListe.Size = new System.Drawing.Size(860, 200);
            this.dgvCommandesListe.TabIndex = 1;
            this.dgvCommandesListe.SelectionChanged += new System.EventHandler(this.DgvCommandesListe_SelectionChanged);
            this.dgvCommandesListe.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvCommandesListe_ColumnHeaderMouseClick);
            // grpNouvelleCommande
            this.grpNouvelleCommande.Controls.Add(this.btnValiderCommande);
            this.grpNouvelleCommande.Controls.Add(this.txbNbExemplaires);
            this.grpNouvelleCommande.Controls.Add(this.txbMontant);
            this.grpNouvelleCommande.Controls.Add(this.dtpDateCommande);
            this.grpNouvelleCommande.Controls.Add(this.txbNumeroCommande);
            this.grpNouvelleCommande.Controls.Add(this.label5);
            this.grpNouvelleCommande.Controls.Add(this.label4);
            this.grpNouvelleCommande.Controls.Add(this.label3);
            this.grpNouvelleCommande.Controls.Add(this.label2);
            this.grpNouvelleCommande.Location = new System.Drawing.Point(12, 295);
            this.grpNouvelleCommande.Name = "grpNouvelleCommande";
            this.grpNouvelleCommande.Size = new System.Drawing.Size(860, 120);
            this.grpNouvelleCommande.TabIndex = 2;
            this.grpNouvelleCommande.TabStop = false;
            this.grpNouvelleCommande.Text = "Nouvelle commande";
            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Numéro de commande :";
            // txbNumeroCommande
            this.txbNumeroCommande.Location = new System.Drawing.Point(150, 20);
            this.txbNumeroCommande.Name = "txbNumeroCommande";
            this.txbNumeroCommande.Size = new System.Drawing.Size(100, 20);
            this.txbNumeroCommande.TabIndex = 1;
            // label3
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(6, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date de commande :";
            // dtpDateCommande
            this.dtpDateCommande.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateCommande.Location = new System.Drawing.Point(150, 48);
            this.dtpDateCommande.Name = "dtpDateCommande";
            this.dtpDateCommande.Size = new System.Drawing.Size(100, 20);
            this.dtpDateCommande.TabIndex = 3;
            // label4
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(6, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Montant :";
            // txbMontant
            this.txbMontant.Location = new System.Drawing.Point(150, 76);
            this.txbMontant.Name = "txbMontant";
            this.txbMontant.Size = new System.Drawing.Size(100, 20);
            this.txbMontant.TabIndex = 5;
            // label5
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(270, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Nombre d'exemplaires :";
            // txbNbExemplaires
            this.txbNbExemplaires.Location = new System.Drawing.Point(410, 20);
            this.txbNbExemplaires.Name = "txbNbExemplaires";
            this.txbNbExemplaires.Size = new System.Drawing.Size(100, 20);
            this.txbNbExemplaires.TabIndex = 7;
            // btnValiderCommande
            this.btnValiderCommande.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnValiderCommande.Location = new System.Drawing.Point(6, 90);
            this.btnValiderCommande.Name = "btnValiderCommande";
            this.btnValiderCommande.Size = new System.Drawing.Size(848, 22);
            this.btnValiderCommande.TabIndex = 8;
            this.btnValiderCommande.Text = "Valider la commande";
            this.btnValiderCommande.UseVisualStyleBackColor = true;
            this.btnValiderCommande.Click += new System.EventHandler(this.BtnValiderCommande_Click);
            // grpSuivi
            this.grpSuivi.Controls.Add(this.btnSupprimerCommande);
            this.grpSuivi.Controls.Add(this.btnModifierSuivi);
            this.grpSuivi.Controls.Add(this.cbxSuivi);
            this.grpSuivi.Controls.Add(this.label6);
            this.grpSuivi.Location = new System.Drawing.Point(12, 425);
            this.grpSuivi.Name = "grpSuivi";
            this.grpSuivi.Size = new System.Drawing.Size(860, 60);
            this.grpSuivi.TabIndex = 3;
            this.grpSuivi.TabStop = false;
            this.grpSuivi.Text = "Suivi de la commande sélectionnée";
            // label6
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Suivi :";
            // cbxSuivi
            this.cbxSuivi.FormattingEnabled = true;
            this.cbxSuivi.Location = new System.Drawing.Point(60, 20);
            this.cbxSuivi.Name = "cbxSuivi";
            this.cbxSuivi.Size = new System.Drawing.Size(150, 21);
            this.cbxSuivi.TabIndex = 1;
            // btnModifierSuivi
            this.btnModifierSuivi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnModifierSuivi.Location = new System.Drawing.Point(220, 18);
            this.btnModifierSuivi.Name = "btnModifierSuivi";
            this.btnModifierSuivi.Size = new System.Drawing.Size(150, 22);
            this.btnModifierSuivi.TabIndex = 2;
            this.btnModifierSuivi.Text = "Modifier le suivi";
            this.btnModifierSuivi.UseVisualStyleBackColor = true;
            this.btnModifierSuivi.Click += new System.EventHandler(this.BtnModifierSuivi_Click);
            // btnSupprimerCommande
            this.btnSupprimerCommande.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnSupprimerCommande.Location = new System.Drawing.Point(380, 18);
            this.btnSupprimerCommande.Name = "btnSupprimerCommande";
            this.btnSupprimerCommande.Size = new System.Drawing.Size(150, 22);
            this.btnSupprimerCommande.TabIndex = 3;
            this.btnSupprimerCommande.Text = "Supprimer";
            this.btnSupprimerCommande.UseVisualStyleBackColor = true;
            this.btnSupprimerCommande.Click += new System.EventHandler(this.BtnSupprimerCommande_Click);
            // FrmCommandesDvd
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 500);
            this.Controls.Add(this.grpSuivi);
            this.Controls.Add(this.grpNouvelleCommande);
            this.Controls.Add(this.dgvCommandesListe);
            this.Controls.Add(this.grpRecherche);
            this.Name = "FrmCommandesDvd";
            this.Text = "Gestion des commandes de DVD";
            this.grpRecherche.ResumeLayout(false);
            this.grpRecherche.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCommandesListe)).EndInit();
            this.grpNouvelleCommande.ResumeLayout(false);
            this.grpNouvelleCommande.PerformLayout();
            this.grpSuivi.ResumeLayout(false);
            this.grpSuivi.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox grpRecherche;
        private System.Windows.Forms.Button btnRechercherDvd;
        private System.Windows.Forms.TextBox txbNumeroDvd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvCommandesListe;
        private System.Windows.Forms.GroupBox grpNouvelleCommande;
        private System.Windows.Forms.Button btnValiderCommande;
        private System.Windows.Forms.TextBox txbNbExemplaires;
        private System.Windows.Forms.TextBox txbMontant;
        private System.Windows.Forms.DateTimePicker dtpDateCommande;
        private System.Windows.Forms.TextBox txbNumeroCommande;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox grpSuivi;
        private System.Windows.Forms.Button btnSupprimerCommande;
        private System.Windows.Forms.Button btnModifierSuivi;
        private System.Windows.Forms.ComboBox cbxSuivi;
        private System.Windows.Forms.Label label6;
    }
}