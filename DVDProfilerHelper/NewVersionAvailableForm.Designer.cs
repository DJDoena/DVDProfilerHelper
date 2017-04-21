namespace DoenaSoft.DVDProfiler.DVDProfilerHelper
{
    partial class NewVersionAvailableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewVersionAvailableForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CurrentVersionTextBox = new System.Windows.Forms.TextBox();
            this.NewVersionTextBox = new System.Windows.Forms.TextBox();
            this.LinkLabel = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // CurrentVersionTextBox
            // 
            resources.ApplyResources(this.CurrentVersionTextBox, "CurrentVersionTextBox");
            this.CurrentVersionTextBox.Name = "CurrentVersionTextBox";
            this.CurrentVersionTextBox.ReadOnly = true;
            // 
            // NewVersionTextBox
            // 
            resources.ApplyResources(this.NewVersionTextBox, "NewVersionTextBox");
            this.NewVersionTextBox.Name = "NewVersionTextBox";
            this.NewVersionTextBox.ReadOnly = true;
            // 
            // LinkLabel
            // 
            resources.ApplyResources(this.LinkLabel, "LinkLabel");
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.TabStop = true;
            this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkLabelLinkClicked);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // NewVersionAvailableForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LinkLabel);
            this.Controls.Add(this.NewVersionTextBox);
            this.Controls.Add(this.CurrentVersionTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewVersionAvailableForm";
            this.Load += new System.EventHandler(this.OnNewVersionAvailableFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CurrentVersionTextBox;
        private System.Windows.Forms.TextBox NewVersionTextBox;
        private System.Windows.Forms.LinkLabel LinkLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}