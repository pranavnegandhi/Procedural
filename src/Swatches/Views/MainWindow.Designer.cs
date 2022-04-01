
namespace Notadesigner.Apps.Views
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainWindowLayout = new System.Windows.Forms.TableLayoutPanel();
            this.startButton = new System.Windows.Forms.Button();
            this.effectsList = new System.Windows.Forms.ListBox();
            this.effectOptionsLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.effectView = new System.Windows.Forms.PictureBox();
            this.mainWindowLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.effectView)).BeginInit();
            this.SuspendLayout();
            // 
            // mainWindowLayout
            // 
            this.mainWindowLayout.ColumnCount = 2;
            this.mainWindowLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.mainWindowLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainWindowLayout.Controls.Add(this.startButton, 1, 1);
            this.mainWindowLayout.Controls.Add(this.effectsList, 0, 0);
            this.mainWindowLayout.Controls.Add(this.effectOptionsLayout, 1, 0);
            this.mainWindowLayout.Controls.Add(this.effectView, 1, 2);
            this.mainWindowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainWindowLayout.Location = new System.Drawing.Point(0, 0);
            this.mainWindowLayout.Name = "mainWindowLayout";
            this.mainWindowLayout.RowCount = 3;
            this.mainWindowLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.mainWindowLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainWindowLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainWindowLayout.Size = new System.Drawing.Size(1232, 780);
            this.mainWindowLayout.TabIndex = 0;
            // 
            // startButton
            // 
            this.startButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.startButton.Location = new System.Drawing.Point(223, 203);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "&Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClickHandler);
            // 
            // effectsList
            // 
            this.effectsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectsList.ItemHeight = 15;
            this.effectsList.Location = new System.Drawing.Point(10, 10);
            this.effectsList.Margin = new System.Windows.Forms.Padding(10);
            this.effectsList.Name = "effectsList";
            this.mainWindowLayout.SetRowSpan(this.effectsList, 3);
            this.effectsList.Size = new System.Drawing.Size(200, 836);
            this.effectsList.TabIndex = 0;
            // 
            // effectOptionsLayout
            // 
            this.effectOptionsLayout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.effectOptionsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectOptionsLayout.Location = new System.Drawing.Point(223, 3);
            this.effectOptionsLayout.Name = "effectOptionsLayout";
            this.effectOptionsLayout.Size = new System.Drawing.Size(1006, 194);
            this.effectOptionsLayout.TabIndex = 1;
            this.effectOptionsLayout.Text = "Options";
            // 
            // effectView
            // 
            this.effectView.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.effectView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.effectView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.effectView.Location = new System.Drawing.Point(223, 233);
            this.effectView.Name = "effectView";
            this.effectView.Size = new System.Drawing.Size(1006, 620);
            this.effectView.TabIndex = 0;
            this.effectView.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 780);
            this.Controls.Add(this.mainWindowLayout);
            this.Name = "MainWindow";
            this.Text = "Shades Playground";
            this.Resize += new System.EventHandler(this.MainWindowResizeHandler);
            this.mainWindowLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.effectView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainWindowLayout;
        private System.Windows.Forms.ListBox effectsList;
        private System.Windows.Forms.FlowLayoutPanel effectOptionsLayout;
        private System.Windows.Forms.PictureBox effectView;
        private System.Windows.Forms.Button startButton;
    }
}

