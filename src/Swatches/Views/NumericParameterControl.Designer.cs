
namespace Notadesigner.Apps.Views
{
    partial class NumericParameterControl<T>
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
            this.ParameterInput = new System.Windows.Forms.NumericUpDown();
            this.SuspendLayout();
            // 
            // ParameterInput
            // 
            this.ParameterInput.AutoSize = true;
            this.ParameterInput.Location = new System.Drawing.Point(3, 26);
            this.ParameterInput.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ParameterInput.Name = "ParameterInput";
            this.ParameterInput.Size = new System.Drawing.Size(75, 23);
            this.ParameterInput.TabIndex = 0;
            this.ParameterInput.Text = "0";
            this.ParameterInput.Validating += new System.ComponentModel.CancelEventHandler(this.Parameter_Validating);
            this.ParameterInput.Validated += new System.EventHandler(this.Parameter_Validated);
            // 
            // ParameterControl
            // 
            this.Controls.Add(this.ParameterInput);
        }

        #endregion

        private System.Windows.Forms.NumericUpDown ParameterInput;
    }
}
