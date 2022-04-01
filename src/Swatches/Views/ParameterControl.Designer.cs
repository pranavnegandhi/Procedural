namespace Notadesigner.Apps.Views
{
    partial class ParameterControl<TChangeEventArgs>
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
        
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ParameterLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ParameterLabel
            // 
            this.ParameterLabel.AutoSize = true;
            this.ParameterLabel.Location = new System.Drawing.Point(3, 7);
            this.ParameterLabel.Name = "ParameterLabel";
            this.ParameterLabel.Size = new System.Drawing.Size(38, 15);
            this.ParameterLabel.TabIndex = 0;
            this.ParameterLabel.Text = "label1";
            // 
            // ParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParameterLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.Name = "ParameterControl";
            this.Size = new System.Drawing.Size(90, 50);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ParameterLabel;
    }
}