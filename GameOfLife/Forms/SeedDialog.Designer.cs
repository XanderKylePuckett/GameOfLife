namespace GameOfLife
{
	partial class SeedDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeedDialog));
			this.seedNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.seedNumericUpDown)).BeginInit();
			this.SuspendLayout();
			// 
			// seedNumericUpDown
			// 
			resources.ApplyResources(this.seedNumericUpDown, "seedNumericUpDown");
			this.seedNumericUpDown.Maximum = new decimal(new int[] {
			2147483647,
			0,
			0,
			0});
			this.seedNumericUpDown.Minimum = new decimal(new int[] {
			-2147483648,
			0,
			0,
			-2147483648});
			this.seedNumericUpDown.Name = "seedNumericUpDown";
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// OkButton
			// 
			this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.OkButton, "OkButton");
			this.OkButton.Name = "OkButton";
			this.OkButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// SeedDialog
			// 
			this.AcceptButton = this.OkButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OkButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.seedNumericUpDown);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SeedDialog";
			this.ShowInTaskbar = false;
			((System.ComponentModel.ISupportInitialize)(this.seedNumericUpDown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown seedNumericUpDown;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button cancelButton;
	}
}