namespace Cryptage
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.checkBox_room_name = new System.Windows.Forms.CheckBox();
            this.checkBox_room_finish = new System.Windows.Forms.CheckBox();
            this.checkBox_walls = new System.Windows.Forms.CheckBox();
            this.checkBox_doors = new System.Windows.Forms.CheckBox();
            this.checkBox_furniture = new System.Windows.Forms.CheckBox();
            this.checkBox_add_furniture = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectAll = new System.Windows.Forms.Button();
            this.Unselect_All = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(248, 317);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(161, 317);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // checkBox_room_name
            // 
            this.checkBox_room_name.AutoSize = true;
            this.checkBox_room_name.Location = new System.Drawing.Point(20, 43);
            this.checkBox_room_name.Name = "checkBox_room_name";
            this.checkBox_room_name.Size = new System.Drawing.Size(249, 17);
            this.checkBox_room_name.TabIndex = 9;
            this.checkBox_room_name.Text = "Crypter les noms et numéros des pièces (XXXX)";
            this.checkBox_room_name.UseVisualStyleBackColor = true;
            // 
            // checkBox_room_finish
            // 
            this.checkBox_room_finish.AutoSize = true;
            this.checkBox_room_finish.Location = new System.Drawing.Point(20, 65);
            this.checkBox_room_finish.Name = "checkBox_room_finish";
            this.checkBox_room_finish.Size = new System.Drawing.Size(204, 17);
            this.checkBox_room_finish.TabIndex = 11;
            this.checkBox_room_finish.Text = "Crypter les finitions des pièces (XXXX)";
            this.checkBox_room_finish.UseVisualStyleBackColor = true;
            // 
            // checkBox_walls
            // 
            this.checkBox_walls.AutoSize = true;
            this.checkBox_walls.Location = new System.Drawing.Point(20, 115);
            this.checkBox_walls.Name = "checkBox_walls";
            this.checkBox_walls.Size = new System.Drawing.Size(193, 17);
            this.checkBox_walls.TabIndex = 12;
            this.checkBox_walls.Text = "Remplacer les murs limites de pièce";
            this.checkBox_walls.UseVisualStyleBackColor = true;
            // 
            // checkBox_doors
            // 
            this.checkBox_doors.AutoSize = true;
            this.checkBox_doors.Location = new System.Drawing.Point(20, 137);
            this.checkBox_doors.Name = "checkBox_doors";
            this.checkBox_doors.Size = new System.Drawing.Size(128, 17);
            this.checkBox_doors.TabIndex = 13;
            this.checkBox_doors.Text = "Remplacer les portes ";
            this.checkBox_doors.UseVisualStyleBackColor = true;
            // 
            // checkBox_furniture
            // 
            this.checkBox_furniture.AutoSize = true;
            this.checkBox_furniture.Location = new System.Drawing.Point(20, 162);
            this.checkBox_furniture.Name = "checkBox_furniture";
            this.checkBox_furniture.Size = new System.Drawing.Size(126, 17);
            this.checkBox_furniture.TabIndex = 14;
            this.checkBox_furniture.Text = "Remplacer le mobilier";
            this.checkBox_furniture.UseVisualStyleBackColor = true;
            // 
            // checkBox_add_furniture
            // 
            this.checkBox_add_furniture.AutoSize = true;
            this.checkBox_add_furniture.Location = new System.Drawing.Point(20, 213);
            this.checkBox_add_furniture.Name = "checkBox_add_furniture";
            this.checkBox_add_furniture.Size = new System.Drawing.Size(222, 17);
            this.checkBox_add_furniture.TabIndex = 16;
            this.checkBox_add_furniture.Text = "Ajout de mobilier aléatoire dans les pièces";
            this.checkBox_add_furniture.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBox_room_name);
            this.groupBox1.Controls.Add(this.checkBox_add_furniture);
            this.groupBox1.Controls.Add(this.checkBox_room_finish);
            this.groupBox1.Controls.Add(this.checkBox_furniture);
            this.groupBox1.Controls.Add(this.checkBox_doors);
            this.groupBox1.Controls.Add(this.checkBox_walls);
            this.groupBox1.Location = new System.Drawing.Point(12, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(311, 247);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options de cryptage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 193);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Géométrie - ajout :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 95);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Géométrie - remplacement par des éléments génériques :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Informations du modèle :";
            // 
            // SelectAll
            // 
            this.SelectAll.Location = new System.Drawing.Point(89, 18);
            this.SelectAll.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(106, 24);
            this.SelectAll.TabIndex = 17;
            this.SelectAll.Text = "Tout sélectionner";
            this.SelectAll.UseVisualStyleBackColor = true;
            this.SelectAll.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // Unselect_All
            // 
            this.Unselect_All.Location = new System.Drawing.Point(205, 18);
            this.Unselect_All.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Unselect_All.Name = "Unselect_All";
            this.Unselect_All.Size = new System.Drawing.Size(118, 24);
            this.Unselect_All.TabIndex = 18;
            this.Unselect_All.Text = "Ne rien sélectionner";
            this.Unselect_All.UseVisualStyleBackColor = true;
            this.Unselect_All.Click += new System.EventHandler(this.Unselect_All_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(337, 354);
            this.Controls.Add(this.Unselect_All);
            this.Controls.Add(this.SelectAll);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cryptage du modèle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox checkBox_room_name;
        private System.Windows.Forms.CheckBox checkBox_room_finish;
        private System.Windows.Forms.CheckBox checkBox_walls;
        private System.Windows.Forms.CheckBox checkBox_doors;
        private System.Windows.Forms.CheckBox checkBox_furniture;
        private System.Windows.Forms.CheckBox checkBox_add_furniture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SelectAll;
        private System.Windows.Forms.Button Unselect_All;
    }
}