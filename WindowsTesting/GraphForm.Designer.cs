﻿namespace WindowsTesting
{
    partial class GraphForm
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
            this.GraphBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // GraphBox
            // 
            this.GraphBox.Location = new System.Drawing.Point(12, 12);
            this.GraphBox.Name = "GraphBox";
            this.GraphBox.Padding = new System.Windows.Forms.Padding(0);
            this.GraphBox.Size = new System.Drawing.Size(260, 109);
            this.GraphBox.TabIndex = 0;
            this.GraphBox.TabStop = false;
            this.GraphBox.Text = "Kurs";
            this.GraphBox.Enter += new System.EventHandler(this.GraphBox_Enter);
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.GraphBox);
            this.Name = "GraphForm";
            this.Text = "GraphForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GraphBox;
    }
}