namespace GameOfLife
{
	partial class OptionsDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.groupBoxBoundaries = new System.Windows.Forms.GroupBox();
			this.radioButtonFinite = new System.Windows.Forms.RadioButton();
			this.radioButtonToroidal = new System.Windows.Forms.RadioButton();
			this.groupBoxDimensions = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownColumns = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
			this.groupBoxTimer = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
			this.groupBoxGridLines = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.numericUpDownLineWidth = new System.Windows.Forms.NumericUpDown();
			this.checkBoxShowGrid = new System.Windows.Forms.CheckBox();
			this.groupBoxColors = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.buttonCellColor = new System.Windows.Forms.Button();
			this.buttonGridColor = new System.Windows.Forms.Button();
			this.buttonBackgroundColor = new System.Windows.Forms.Button();
			this.defaultsButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.PreviewGraphicsPanel = new GameOfLife.GraphicsPanel();
			this.groupBoxBoundaries.SuspendLayout();
			this.groupBoxDimensions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
			this.groupBoxTimer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
			this.groupBoxGridLines.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineWidth)).BeginInit();
			this.groupBoxColors.SuspendLayout();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// groupBoxBoundaries
			// 
			this.groupBoxBoundaries.Controls.Add(this.radioButtonFinite);
			this.groupBoxBoundaries.Controls.Add(this.radioButtonToroidal);
			resources.ApplyResources(this.groupBoxBoundaries, "groupBoxBoundaries");
			this.groupBoxBoundaries.Name = "groupBoxBoundaries";
			this.groupBoxBoundaries.TabStop = false;
			// 
			// radioButtonFinite
			// 
			resources.ApplyResources(this.radioButtonFinite, "radioButtonFinite");
			this.radioButtonFinite.Name = "radioButtonFinite";
			this.radioButtonFinite.TabStop = true;
			this.radioButtonFinite.UseVisualStyleBackColor = true;
			// 
			// radioButtonToroidal
			// 
			resources.ApplyResources(this.radioButtonToroidal, "radioButtonToroidal");
			this.radioButtonToroidal.Checked = true;
			this.radioButtonToroidal.Name = "radioButtonToroidal";
			this.radioButtonToroidal.TabStop = true;
			this.radioButtonToroidal.UseVisualStyleBackColor = true;
			// 
			// groupBoxDimensions
			// 
			this.groupBoxDimensions.Controls.Add(this.label2);
			this.groupBoxDimensions.Controls.Add(this.label1);
			this.groupBoxDimensions.Controls.Add(this.numericUpDownColumns);
			this.groupBoxDimensions.Controls.Add(this.numericUpDownRows);
			resources.ApplyResources(this.groupBoxDimensions, "groupBoxDimensions");
			this.groupBoxDimensions.Name = "groupBoxDimensions";
			this.groupBoxDimensions.TabStop = false;
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// numericUpDownColumns
			// 
			resources.ApplyResources(this.numericUpDownColumns, "numericUpDownColumns");
			this.numericUpDownColumns.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.numericUpDownColumns.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownColumns.Name = "numericUpDownColumns";
			this.numericUpDownColumns.Value = new decimal(new int[] {
			25,
			0,
			0,
			0});
			// 
			// numericUpDownRows
			// 
			resources.ApplyResources(this.numericUpDownRows, "numericUpDownRows");
			this.numericUpDownRows.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.numericUpDownRows.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownRows.Name = "numericUpDownRows";
			this.numericUpDownRows.Tag = "";
			this.numericUpDownRows.Value = new decimal(new int[] {
			25,
			0,
			0,
			0});
			// 
			// groupBoxTimer
			// 
			this.groupBoxTimer.Controls.Add(this.label3);
			this.groupBoxTimer.Controls.Add(this.numericUpDownInterval);
			resources.ApplyResources(this.groupBoxTimer, "groupBoxTimer");
			this.groupBoxTimer.Name = "groupBoxTimer";
			this.groupBoxTimer.TabStop = false;
			// 
			// label3
			// 
			resources.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			// 
			// numericUpDownInterval
			// 
			resources.ApplyResources(this.numericUpDownInterval, "numericUpDownInterval");
			this.numericUpDownInterval.Maximum = new decimal(new int[] {
			500,
			0,
			0,
			0});
			this.numericUpDownInterval.Minimum = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.numericUpDownInterval.Name = "numericUpDownInterval";
			this.numericUpDownInterval.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			// 
			// groupBoxGridLines
			// 
			this.groupBoxGridLines.Controls.Add(this.label6);
			this.groupBoxGridLines.Controls.Add(this.label5);
			this.groupBoxGridLines.Controls.Add(this.label4);
			this.groupBoxGridLines.Controls.Add(this.numericUpDownLineWidth);
			this.groupBoxGridLines.Controls.Add(this.checkBoxShowGrid);
			resources.ApplyResources(this.groupBoxGridLines, "groupBoxGridLines");
			this.groupBoxGridLines.Name = "groupBoxGridLines";
			this.groupBoxGridLines.TabStop = false;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			// 
			// label5
			// 
			resources.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			// 
			// label4
			// 
			resources.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			// 
			// numericUpDownLineWidth
			// 
			resources.ApplyResources(this.numericUpDownLineWidth, "numericUpDownLineWidth");
			this.numericUpDownLineWidth.Maximum = new decimal(new int[] {
			5,
			0,
			0,
			0});
			this.numericUpDownLineWidth.Minimum = new decimal(new int[] {
			5,
			0,
			0,
			-2147483648});
			this.numericUpDownLineWidth.Name = "numericUpDownLineWidth";
			this.numericUpDownLineWidth.Value = new decimal(new int[] {
			2,
			0,
			0,
			-2147483648});
			this.numericUpDownLineWidth.ValueChanged += new System.EventHandler(this.numericUpDownLineWidth_ValueChanged);
			// 
			// checkBoxShowGrid
			// 
			resources.ApplyResources(this.checkBoxShowGrid, "checkBoxShowGrid");
			this.checkBoxShowGrid.Checked = true;
			this.checkBoxShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxShowGrid.Name = "checkBoxShowGrid";
			this.checkBoxShowGrid.UseVisualStyleBackColor = true;
			this.checkBoxShowGrid.CheckedChanged += new System.EventHandler(this.checkBoxShowGrid_CheckedChanged);
			// 
			// groupBoxColors
			// 
			this.groupBoxColors.Controls.Add(this.label9);
			this.groupBoxColors.Controls.Add(this.label8);
			this.groupBoxColors.Controls.Add(this.label7);
			this.groupBoxColors.Controls.Add(this.buttonCellColor);
			this.groupBoxColors.Controls.Add(this.buttonGridColor);
			this.groupBoxColors.Controls.Add(this.buttonBackgroundColor);
			resources.ApplyResources(this.groupBoxColors, "groupBoxColors");
			this.groupBoxColors.Name = "groupBoxColors";
			this.groupBoxColors.TabStop = false;
			// 
			// label9
			// 
			resources.ApplyResources(this.label9, "label9");
			this.label9.Name = "label9";
			// 
			// label8
			// 
			resources.ApplyResources(this.label8, "label8");
			this.label8.Name = "label8";
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.label7.Name = "label7";
			// 
			// buttonCellColor
			// 
			this.buttonCellColor.BackColor = System.Drawing.Color.Black;
			this.buttonCellColor.ForeColor = System.Drawing.Color.White;
			resources.ApplyResources(this.buttonCellColor, "buttonCellColor");
			this.buttonCellColor.Name = "buttonCellColor";
			this.buttonCellColor.UseVisualStyleBackColor = false;
			this.buttonCellColor.Click += new System.EventHandler(this.buttonCellColor_Click);
			// 
			// buttonGridColor
			// 
			this.buttonGridColor.BackColor = System.Drawing.Color.Black;
			this.buttonGridColor.ForeColor = System.Drawing.Color.White;
			resources.ApplyResources(this.buttonGridColor, "buttonGridColor");
			this.buttonGridColor.Name = "buttonGridColor";
			this.buttonGridColor.UseVisualStyleBackColor = false;
			this.buttonGridColor.Click += new System.EventHandler(this.buttonGridColor_Click);
			// 
			// buttonBackgroundColor
			// 
			this.buttonBackgroundColor.BackColor = System.Drawing.Color.Black;
			this.buttonBackgroundColor.ForeColor = System.Drawing.Color.White;
			resources.ApplyResources(this.buttonBackgroundColor, "buttonBackgroundColor");
			this.buttonBackgroundColor.Name = "buttonBackgroundColor";
			this.buttonBackgroundColor.UseVisualStyleBackColor = false;
			this.buttonBackgroundColor.Click += new System.EventHandler(this.buttonBackgroundColor_Click);
			// 
			// defaultsButton
			// 
			resources.ApplyResources(this.defaultsButton, "defaultsButton");
			this.defaultsButton.Name = "defaultsButton";
			this.defaultsButton.UseVisualStyleBackColor = true;
			this.defaultsButton.Click += new System.EventHandler(this.defaultsButton_Click);
			// 
			// applyButton
			// 
			resources.ApplyResources(this.applyButton, "applyButton");
			this.applyButton.Name = "applyButton";
			this.applyButton.UseVisualStyleBackColor = true;
			this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
			// 
			// label10
			// 
			resources.ApplyResources(this.label10, "label10");
			this.label10.Name = "label10";
			// 
			// PreviewGraphicsPanel
			// 
			this.PreviewGraphicsPanel.BackColor = System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.PreviewGraphicsPanel, "PreviewGraphicsPanel");
			this.PreviewGraphicsPanel.Name = "PreviewGraphicsPanel";
			this.PreviewGraphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewGraphicsPanel_Paint);
			// 
			// OptionsDialog
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.PreviewGraphicsPanel);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.applyButton);
			this.Controls.Add(this.defaultsButton);
			this.Controls.Add(this.groupBoxColors);
			this.Controls.Add(this.groupBoxGridLines);
			this.Controls.Add(this.groupBoxTimer);
			this.Controls.Add(this.groupBoxDimensions);
			this.Controls.Add(this.groupBoxBoundaries);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsDialog";
			this.ShowInTaskbar = false;
			this.groupBoxBoundaries.ResumeLayout(false);
			this.groupBoxBoundaries.PerformLayout();
			this.groupBoxDimensions.ResumeLayout(false);
			this.groupBoxDimensions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
			this.groupBoxTimer.ResumeLayout(false);
			this.groupBoxTimer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
			this.groupBoxGridLines.ResumeLayout(false);
			this.groupBoxGridLines.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineWidth)).EndInit();
			this.groupBoxColors.ResumeLayout(false);
			this.groupBoxColors.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.GroupBox groupBoxBoundaries;
		private System.Windows.Forms.GroupBox groupBoxDimensions;
		private System.Windows.Forms.GroupBox groupBoxTimer;
		private System.Windows.Forms.GroupBox groupBoxGridLines;
		private System.Windows.Forms.RadioButton radioButtonFinite;
		private System.Windows.Forms.RadioButton radioButtonToroidal;
		private System.Windows.Forms.NumericUpDown numericUpDownColumns;
		private System.Windows.Forms.NumericUpDown numericUpDownRows;
		private System.Windows.Forms.NumericUpDown numericUpDownInterval;
		private System.Windows.Forms.NumericUpDown numericUpDownLineWidth;
		private System.Windows.Forms.CheckBox checkBoxShowGrid;
		private System.Windows.Forms.GroupBox groupBoxColors;
		private System.Windows.Forms.Button buttonCellColor;
		private System.Windows.Forms.Button buttonGridColor;
		private System.Windows.Forms.Button buttonBackgroundColor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button defaultsButton;
		private System.Windows.Forms.Button applyButton;
		private GraphicsPanel PreviewGraphicsPanel;
	}
}