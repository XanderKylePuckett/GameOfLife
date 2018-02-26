// Game of Life //
// Seed Dialog //
// ☻☺ トニー ☺☻ -- Y:2015年 M:11月 //

using System.Windows.Forms;

namespace GameOfLife
{
	public partial class SeedDialog : Form
	{
		// SeedDialog Form initialization
		//
		public SeedDialog()
		{
			InitializeComponent();
		}

		#region << Properties >>
		// Seed
		// NumericUpDown: [-2147483648]->[2147483647]
		public int Seed
		{
			get
			{
				return (int)seedNumericUpDown.Value;
			}
			set
			{
				seedNumericUpDown.Value = value;
			}
		}
		#endregion
	}

}

// ☻☺ トニー ☺☻ -- Y:2015年 M:11月 //
// TONY (Antonio V. Perez) //
// November 2015 //