namespace Linked_ID.Scripts.Forms
{
    partial class TypeID
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypeID));
            this.Main_Label = new System.Windows.Forms.Label();
            this.B_Show = new System.Windows.Forms.Button();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Main_Label
            // 
            this.Main_Label.AutoSize = true;
            this.Main_Label.Location = new System.Drawing.Point(9, 14);
            this.Main_Label.Name = "Main_Label";
            this.Main_Label.Size = new System.Drawing.Size(176, 13);
            this.Main_Label.TabIndex = 0;
            this.Main_Label.Text = "ID - (Use semicolon for multiple ID\'s)";
            // 
            // B_Show
            // 
            this.B_Show.Location = new System.Drawing.Point(12, 67);
            this.B_Show.Name = "B_Show";
            this.B_Show.Size = new System.Drawing.Size(88, 23);
            this.B_Show.TabIndex = 1;
            this.B_Show.Text = "Show";
            this.B_Show.UseVisualStyleBackColor = true;
            // 
            // B_OK
            // 
            this.B_OK.Location = new System.Drawing.Point(133, 67);
            this.B_OK.Name = "B_OK";
            this.B_OK.Size = new System.Drawing.Size(88, 23);
            this.B_OK.TabIndex = 2;
            this.B_OK.Text = "OK";
            this.B_OK.UseVisualStyleBackColor = true;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(225, 67);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(88, 23);
            this.B_Cancel.TabIndex = 3;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(301, 20);
            this.textBox1.TabIndex = 4;
            // 
            // TypeID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 106);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.B_Show);
            this.Controls.Add(this.Main_Label);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TypeID";
            this.Text = "Select Linked Element by ID";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Main_Label;
        private System.Windows.Forms.Button B_Show;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.TextBox textBox1;
    }
}