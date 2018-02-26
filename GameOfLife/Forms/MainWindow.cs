// Game of Life //
// Main Window //
// ☻☺ トニー ☺☻ -- Y:2015年 M:11月 //

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace GameOfLife
{
	public partial class MainWindow : Form
	{
		#region << Fields >>


		/// <summary>
		/// Number of rows in the universe
		/// </summary>
		int numRows;
		/// <summary>
		/// Number of columns in the universe
		/// </summary>
		int numCols;
		/// <summary>
		/// Counter for the number of living cells in the current geeration
		/// </summary>
		int cellCount;
		/// <summary>
		/// Counter for the generations
		/// </summary>
		int generation;
		/// <summary>
		/// Seed used for randomizing cells
		/// </summary>
		int randomizeSeed;
		/// <summary>
		/// Width of the gridlines, positive if drawn under cells, negative if drawn over cells
		/// </summary>
		int gridWidth;
		/// <summary>
		/// Multidimensional array of bools to represent cells in the universe
		/// </summary>
		bool[,] cells;
		/// <summary>
		/// True if the universe wraps around, false if the universe is finite
		/// </summary>
		bool toroidal;
		/// <summary>
		/// True if gridlines should be shown, false if gridlines should be hidden
		/// </summary>
		bool showGrid;
		/// <summary>
		/// Color for the background
		/// </summary>
		Color backgroundColor;
		/// <summary>
		/// Color for the cells
		/// </summary>
		Color cellColor;
		/// <summary>
		/// Color for the grid
		/// </summary>
		Color gridColor;
		/// <summary>
		/// Brush used to draw the cells
		/// </summary>
		Brush cellBrush;
		/// <summary>
		/// Pen used to draw gridlines
		/// </summary>
		Pen gridPen;
		/// <summary>
		/// Timer used for simulation
		/// </summary>
		Timer timer;
		/// <summary>
		/// Random object used for randomizing cells
		/// </summary>
		Random rnd;
		/// <summary>
		/// Tool Window [DISABLED]
		/// </summary>
		ToolWindow tool; // [DISABLED]


		#endregion
		#region << Events >>


		// Universe Paint Event
		// Graphics Panel: Paint --> Paints the grid and the cells
		private void graphicsPanel_Paint(object sender, PaintEventArgs e)
		{
			// Reset living cells counter
			cellCount = 0;

			// if grid linewidth is negative, draw grid under the cells
			if (gridWidth < 0)
				// Show grid under cells
				DrawCellsOverGrid(e.Graphics);

			// if grid linewidth is not negative, draw grid over the cells
			else
				// Show grid over cells
				DrawGridOverCells(e.Graphics);

			// Update status bar information
			UpdateStatusBar();
		}
		// Clicking on cells
		// Graphics Panel: Mouse Down --> Clicking on a cell toggles it
		private void graphicsPanel_MouseDown(object sender, MouseEventArgs e)
		{
			// Ignore right clicks, reserved for context menu use
			if (MouseButtons.Right != e.Button)
			{
				// Calculate Cell Width and Height
				float cellWidth = (float)graphicsPanel.ClientSize.Width / numCols;
				float cellHeight = (float)graphicsPanel.ClientSize.Height / numRows;

				// Get position of the cell that was clicked on
				int x = (int)(e.X / cellWidth);
				int y = (int)(e.Y / cellHeight);

				// Make sure x and y are not out of bounds
				if (x < numCols && y < numRows)
				{
					// Toggle clicked cell
					cells[x, y] = !cells[x, y];

					// Invalidate graphics panel to redraw the universe
					graphicsPanel.Invalidate();
				}
			}
		}
		// Tick event for the simulation timer
		// Called by the Timer object (timer)
		void timer_Tick(object sender, EventArgs e)
		{
			// For every tick, call NextGen() to produce the next generation of cells
			NextGen();
		}

		// About
		// Menu: About...
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Show the About Box
			About about = new About();
			about.ShowDialog();
		}
		// Save File
		// Menu: File->Save... (Ctrl+S)
		// Tool Strip: Save File [Save Icon]
		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Create instance of SaveFileDialog
			SaveFileDialog dlg = new SaveFileDialog();

			// 1. Text File (*.txt) [Default]
			// 2. All Files (*.*)
			dlg.Filter = "Text File" + "|*.txt|"
						+ "All Files" + "|*.*";
			dlg.FilterIndex = 1;
			dlg.DefaultExt = "txt";

			// Show Save File Dialog
			if (DialogResult.OK == dlg.ShowDialog())
			{
				StreamWriter writer = new StreamWriter(dlg.FileName);

				int y = 0, x;
				// Save current universe to file
				for (; y < numRows; ++y)
				{
					for (x = 0; x < numCols; ++x)
						// If alive, 'O'
						// If dead, '.'
						writer.Write(cells[x, y] ? 'O' : '.');
					writer.Write('\n');
				}

				writer.Close();
			}
		}
		// Open File
		// Menu: File->Open... (Ctrl+O)
		// Tool Strip: Open File [Folder Icon]
		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Create instance of OpenFileDialog
			OpenFileDialog dlg = new OpenFileDialog();

			// 1. Text File (*.txt)
			// 2. Life Lexicon Plaintext File (*.cells)
			// 3. All Supported Files (*.txt, *.cells) [DEFAULT]
			// 4. All Files (*.*)
			dlg.Filter = "Text File" + "|*.txt|"
						+ "Life Lexicon Plaintext File" + "|*.cells|"
						+ "All Supported Files" + " (*.txt, *.cells)|*.txt;*.cells|"
						+ "All Files" + "|*.*";
			dlg.FilterIndex = 3;

			// Show Open File Dialog
			if (DialogResult.OK == dlg.ShowDialog())
			{
				// Clear the current universe
				ResetUniverse();

				// Create temporary variables for the new dimensions
				int universeWidth = 0;
				int universeHeight = 0;

				// Create temporary string for storing lines of text from file
				string s;

				// Create StreamReader for reading in file
				StreamReader reader;

				// Get file path and name from dialog
				string filename = dlg.FileName;

				// Open file to measure the size of the universe
				reader = new StreamReader(filename);

				// Get Size of file's universe
				while (true)
				{
					s = reader.ReadLine();
					if (null == s) // end of file
						break;
					if (0 == s.Length) // ignore empty lines
						continue;
					if ('!' != s[0]) // ignore commented lines
					{
						universeWidth = (s.Length > universeWidth) ? s.Length : universeWidth;
						++universeHeight;
					}
				}
				reader.Close();

				// Check if dimensions are valid
				if (0 < universeHeight && 0 < universeWidth)
				{
					// Check to see if file is a Life Lexicon Plaintext File (.cells)
					bool isCells = IsCells(filename);

					// startX, startY: top-left position in the
					// current universe where the file gets drawn
					// if .cells --> draw in the center
					// if .txt --> draw from top-left corner (0,0)
					int startX, startY;

					// If the file is a .cells file, do not resize the
					// universe unless the current universe is too small
					// to fit the file
					if (isCells)
					{
						// Allow at least 15 empty columns/rows on each side of the imported .cells file
						numCols = (numCols > (15 + universeWidth)) ? numCols : (15 + universeWidth);
						numRows = (numRows > (15 + universeHeight)) ? numRows : (15 + universeHeight);

						// Center image in the universe
						startX = (numCols >> 1) - (universeWidth >> 1);
						startY = (numRows >> 1) - (universeHeight >> 1);

						// Just making sure the start position isn't going out of bounds
						if (startX < 0) startX = 0;
						if (startY < 0) startY = 0;
					}
					// If the file is a .txt file, or any other type
					// of file, resize the current universe to
					// the dimensions of the file
					else
					{
						// Set dimensions equal to the file's dimensions
						numCols = universeWidth;
						numRows = universeHeight;

						// Image covers entire universe
						startX = 0;
						startY = 0;
					}

					// Make new universe with new dimensions
					cells = new bool[numCols, numRows];

					// Reopen file for reading universe data
					reader = new StreamReader(filename);

					// Start at the first row (y=0)
					int x, y = 0;
					while (true)
					{
						// read the file line by line
						s = reader.ReadLine();
						// break when you reach the end of file (line is null)
						if (null == s)
							break;
						// ignore empty lines (when string length is 0)
						if (0 == s.Length)
							continue;
						// ignore commented lines (lines that start with '!')
						if ('!' != s[0])
						{
							// iterate horizontally across each row
							for (x = 0; x < universeWidth; ++x)
							{
								// Prevent reading a character out of the string's range
								// in case user tries to open a universe from an invalid file
								if (s.Length <= x)
									break;

								// skip over invalid characters
								if ('O' != s[x] && '.' != s[x])
									continue;

								// set cells[x,y] to true if the character is 'O'
								cells[x + startX, y + startY] = 'O' == s[x];
							}
							// continue to the next row
							++y;
						}
					}

					reader.Close();

					// Invalidate the graphics panel to show the new universe
					graphicsPanel.Invalidate();
				}
			}
		}

		// New
		// Menu: File->New (Ctrl+N)
		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the ResetUniverse function to clear all the cells
			ResetUniverse();
		}
		// Clear
		// Menu: Do Something->Clear (F8)
		// Tool Strip: Clear [Red 'X' Icon]
		private void clearToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the ResetUniverse function to clear all the cells
			ResetUniverse();
		}
		// Next
		// Menu: Do Something->Next (F7)
		// Tool Strip: Next [DarkGreen Next Button]
		private void nextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the NextGen function to show the next generation of cells
			NextGen();
		}
		// Start
		// Menu: Do Something->Start (F5)
		// Tool Strip: Start [Green Play Button]
		private void startToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call EnableSimulation() to start/resume the simulation
			EnableSimulation();
		}
		// Pause
		// Menu: Do Something->Pause (F6)
		// Tool Strip: Pause [Orange Pause Button]
		private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call DisableSimulation() to stop the simulation
			DisableSimulation();
		}

		// Toggle Grid
		// Menu: Do Something->Toggle Grid (F9)
		// Tool Strip: Toggle Grid [Blue Grid Icon]
		// Context Menu: Show/Hide Grid
		private void toggleGridToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Turn the grid on/off
			showGrid = !showGrid;

			// Change text in the "Show/Hide Grid" item in the context menu
			showHideGridToolStripMenuItem.Text = showGrid ? "Hide &Grid" : "Show &Grid";

			// Invalidate the graphics panel to show the changes
			graphicsPanel.Invalidate();
		}

		// Options
		// Menu: Tools->Options... (Ctrl+,)
		// Context Menu: Options...
		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Pause the simulation
			DisableSimulation();

			// Create an instance of OptionsDialog
			OptionsDialog dlg = new OptionsDialog();

			// Pass in current settings
			dlg.Finite = !toroidal;
			dlg.NumberOfColumns = numCols;
			dlg.NumberOfRows = numRows;
			dlg.Interval = timer.Interval;
			dlg.Background = backgroundColor;
			dlg.GridColor = gridColor;
			dlg.CellsColor = cellColor;
			dlg.ShowGrid = showGrid;
			dlg.LineWidth = gridWidth;

			// Apply Button
			dlg.Apply += OptionsDlg_Apply;

			// Show the options dialog
			if (DialogResult.OK == dlg.ShowDialog())
			{
				// Get user's settings from the OptionsDialog
				toroidal = !dlg.Finite;
				numCols = dlg.NumberOfColumns;
				numRows = dlg.NumberOfRows;
				timer.Interval = dlg.Interval;
				backgroundColor = dlg.Background;
				gridColor = dlg.GridColor;
				cellColor = dlg.CellsColor;
				showGrid = dlg.ShowGrid;
				gridWidth = dlg.LineWidth;

				// Call ResizeUniverse() to apply changes in the dimensions
				ResizeUniverse();

				// Call UpdateColors() to apply new color settings
				UpdateColors();
			}
		}
		// Options Dialog Apply Button Event
		// Apply settings from the options dialog to the main window
		private void OptionsDlg_Apply(object sender, EventArgs e)
		{
			// Cast sender object to an instance of OptionsDialog
			OptionsDialog dlg = (OptionsDialog)sender;

			// Get user's settings from the OptionsDialog
			toroidal = !dlg.Finite;
			numCols = dlg.NumberOfColumns;
			numRows = dlg.NumberOfRows;
			timer.Interval = dlg.Interval;
			backgroundColor = dlg.Background;
			gridColor = dlg.GridColor;
			cellColor = dlg.CellsColor;
			showGrid = dlg.ShowGrid;
			gridWidth = dlg.LineWidth;

			// Call ResizeUniverse() to apply changes in the dimensions
			ResizeUniverse();

			// Call UpdateColors() to apply new color settings
			UpdateColors();
		}
		// Tool Window [DISABLED]
		// Menu: Tools->Tool Window (Ctrl+T)
		private void toolWindowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenToolWindow();
		}
		// Tool Window Close Event
		//
		private void Tool_FormClosed(object sender, FormClosedEventArgs e)
		{
			//When the tool window is closed, set tool to null
			tool = null;
		}
		// Main Window Close Event
		//
		private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
		{
			// When program closes, save the current settings

			// Get current settings
			Properties.Settings.Default.BackgroundColor = backgroundColor;
			Properties.Settings.Default.CellColor = cellColor;
			Properties.Settings.Default.GridColor = gridColor;
			Properties.Settings.Default.LineWidth = gridWidth;
			Properties.Settings.Default.NumCols = numCols;
			Properties.Settings.Default.NumRows = numRows;
			Properties.Settings.Default.TimerInterval = timer.Interval;
			Properties.Settings.Default.Toroidal = toroidal;
			Properties.Settings.Default.ShowGrid = showGrid;
			Properties.Settings.Default.RandomSeed = randomizeSeed;

			// Save out the settings
			Properties.Settings.Default.Save();
		}
		// Reset Settings and clear universe
		// Menu: Tools->Reset (Ctrl+R)
		private void resetToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Reset Settings to Defaults
			Properties.Settings.Default.Reset();

			// reset settings to defaults
			backgroundColor = Properties.Settings.Default.BackgroundColor;
			cellColor = Properties.Settings.Default.CellColor;
			gridColor = Properties.Settings.Default.GridColor;
			gridWidth = Properties.Settings.Default.LineWidth;
			numCols = Properties.Settings.Default.NumCols;
			numRows = Properties.Settings.Default.NumRows;
			timer.Interval = Properties.Settings.Default.TimerInterval;
			toroidal = Properties.Settings.Default.Toroidal;
			showGrid = Properties.Settings.Default.ShowGrid;
			randomizeSeed = Properties.Settings.Default.RandomSeed;

			// Reset everything else
			Reseed(randomizeSeed);
			UpdateColors();
			ResetUniverse();
		}


		// Toggle Toroidal Mode (MenuItem)
		// Menu: Do Something->Toggle Toroidal Mode (F10)
		private void toggleToroidalModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the toggleToroidal functions to toggle between Toroidal Mode and Finite Mode
			toggleToroidal();
		}
		// Toggle Toroidal Mode (Button)
		// Tool Strip: Toggle Toroidal Mode [Blue Torus Icon]
		private void toggleToroidalStripButton_Click(object sender, EventArgs e)
		{
			// Call the toggleToroidal functions to toggle between Toroidal Mode and Finite Mode
			toggleToroidal();
		}


		// Change Background Color
		// Context Menu: Change Color->Background...
		private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Create an instance of ColorDialog
			ColorDialog dlg = new ColorDialog();

			// Pass in the current background color
			dlg.Color = backgroundColor;

			// Show the color dialog
			if (DialogResult.OK == dlg.ShowDialog())
			{
				// Get user's setting and save it to the color variable (backgroundColor)
				backgroundColor = dlg.Color;

				// Call UpdateColors() to apply new color settings
				UpdateColors();
			}
		}
		// Change Grid Color
		// Context Menu: Change Color->Grid...
		private void gridToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Create an instance of ColorDialog
			ColorDialog dlg = new ColorDialog();

			// Pass in the current grid color
			dlg.Color = gridColor;

			// Show the color dialog
			if (DialogResult.OK == dlg.ShowDialog())
			{
				// Get user's setting and save it to the color variable (gridColor)
				gridColor = dlg.Color;

				// Call UpdateColors() to apply new color settings
				UpdateColors();
			}
		}
		// Change Cells Color
		// Context Menu: Change Color->Cells...
		private void cellsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Create an instance of ColorDialog
			ColorDialog dlg = new ColorDialog();

			// Pass in the current cell color
			dlg.Color = cellColor;

			// Show the color dialog
			if (DialogResult.OK == dlg.ShowDialog())
			{
				// Get user's setting and save it to the color variable (cellColor)
				cellColor = dlg.Color;

				// Call UpdateColors() to apply new color settings
				UpdateColors();
			}
		}


		// Randomize Cells (Random Seed)
		// Menu: Do Something->Randomize->Random Seed (F4)
		// Tool Strip: Randomize (Random Seed) [Shuffle Button]
		// Context Menu: Randomize->Random Seed
		private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the RandomizeCells function and pass in a new random seed
			// Seed a Random to the current time and .Next() it to get a new random seed
			RandomizeCells(new Random((int)DateTime.Now.Ticks).Next());
		}
		// Randomize Cells (Enter Seed)
		// Menu: Do Something->Randomize->Enter Seed... (F2)
		// Context Menu: Randomize->Enter Seed...
		private void enterSeedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Create an instance of SeedDialog
			SeedDialog dlg = new SeedDialog();

			// Pass in the current seed (use decimal for NumericUpDown)
			dlg.Seed = randomizeSeed;

			// Show the seed dialog
			if (DialogResult.OK == dlg.ShowDialog())
				// Call the RandomizeCells functions and pass in the new seed from user input
				RandomizeCells(dlg.Seed);
		}
		// Randomize Cells (Current Seed)
		// Menu: Do Something->Randomize->Current Seed (F3)
		// Context Menu: Randomize->Current Seed
		private void currentSeedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call RandomizeCells function and pass in the current seed (randomizeSeed)
			RandomizeCells(randomizeSeed);
		}


		// Add Column (MenuItem)
		// Menu: Move/Size->Add Column (Ctrl+Right)
		private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the addColumn function to add a new column to the right side of the universe
			addColumn();
		}
		// Remove Column (MenuItem)
		// Menu: Move/Size->Remove Column (Ctrl+Left)
		private void removeColumnToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the removeColumn function to delete the rightmost column from the universe
			removeColumn();
		}
		// Add Row (MenuItem)
		// Menu: Move/Size->Add Row (Ctrl+Up)
		private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the addRow function to add a new row to the bottom of the universe
			addRow();
		}
		// Remove Row (MenuItem)
		// Menu: Move/Size->Remove Row (Ctrl+Down)
		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the removeRow function to delete the bottom row from the universe
			removeRow();
		}

		// Add Column (Button)
		// Tool Strip: Add Column [Columns->Green Plus Button]
		private void addColumnToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the addColumn function to add a new column to the right side of the universe
			addColumn();
		}
		// Remove Column (Button)
		// Tool Strip: Remove Column [Columns->Red Minus Button]
		private void removeColumnToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the removeColumn function to delete the rightmost column from the universe
			removeColumn();
		}
		// Add Row (Button)
		// Tool Strip: Add Row [Rows->Green Plus Button]
		private void addRowToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the addRow function to add a new row to the bottom of the universe
			addRow();
		}
		// Remove Row (Button)
		// Tool Strip: Remove Row [Rows->Red Minus Button]
		private void removeRowToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the removeRow function to delete the bottom row from the universe
			removeRow();
		}


		// Move Up (MenuItem)
		// Menu: Move/Size->Move Up (Alt+Up)
		private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the translateUp function to move all the cells one space up
			translateUp();
		}
		// Move Down (MenuItem)
		// Menu: Move/Size->Move Down (Alt+Down)
		private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the translateDown function to move all the cells one space down
			translateDown();
		}
		// Move Left (MenuItem)
		// Menu: Move/Size->Move Left (Alt+Left)
		private void moveLeftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the translateLeft function to move all the cells one space to the left
			translateLeft();
		}
		// Move Right (MenuItem)
		// Menu: Move/Size->Move Right (Alt+Right)
		private void moveRightToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Call the translateRight function to move all the cells one space to the right
			translateRight();
		}

		// Move Up (Button)
		// Tool Strip: Move Up [Blue Up Arrow]
		private void moveUpToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the translateUp function to move all the cells one space up
			translateUp();
		}
		// Move Down (Button)
		// Tool Strip: Move Down [Blue Down Arrow]
		private void moveDownToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the translateDown function to move all the cells one space down
			translateDown();
		}
		// Move Left (Button)
		// Tool Strip: Move Left [Blue Left Arrow]
		private void moveLeftToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the translateLeft function to move all the cells one space to the left
			translateLeft();
		}
		// Move Right (Button)
		// Tool Strip: Move Right [Blue Right Arrow]
		private void moveRightToolStripButton_Click(object sender, EventArgs e)
		{
			// Call the translateRight function to move all the cells one space to the right
			translateRight();
		}


		#endregion
		#region << Methods >>


		/// <summary>
		/// UpdateColors: Call this function when the Color variables are changed
		/// (should also be called when gridlines change width)
		/// </summary>
		private void UpdateColors()
		{
			// Make a new SolidBrush for the cells using cellColor
			cellBrush = new SolidBrush(cellColor);

			// Make a new Pen for the gridlines using gridColor and the absolute value of gridWidth
			gridPen = new Pen(gridColor, 0 < gridWidth ? gridWidth : -gridWidth);

			// Change the backcolor of the graphics panel to the new bg color (backgroudColor)
			graphicsPanel.BackColor = backgroundColor;

			// Set backcolors of the contextmenu items to the new colors
			backgroundToolStripMenuItem.BackColor = backgroundColor;
			cellsToolStripMenuItem.BackColor = cellColor;
			gridToolStripMenuItem.BackColor = gridColor;

			// Set text of the contextmenu items colors to the backcolors' bitwise complement
			backgroundToolStripMenuItem.ForeColor = Color.FromArgb(~(backgroundColor.ToArgb()));
			cellsToolStripMenuItem.ForeColor = Color.FromArgb(~(cellColor.ToArgb()));
			gridToolStripMenuItem.ForeColor = Color.FromArgb(~(gridColor.ToArgb()));

			// Invalidate to redraw
			graphicsPanel.Invalidate();
		}
		/// <summary>
		/// UpdateStatusBar: Call this function when information displayed in the status bar changes
		/// </summary>
		private void UpdateStatusBar()
		{
			// Set text properties for all status labels
			GenerationStatusLabel.Text = "Generation: " + generation;
			DimensionsStatusLabel.Text = "Universe: " + numCols + "×" + numRows
											+ (toroidal ? " (Toroidal)" : " (Finite)");
			SeedStatusLabel.Text = 0 == randomizeSeed ? "Seed: Zero" : ("Seed: " + randomizeSeed);
			CellCountStatusLabel.Text = "Living Cells: " + cellCount;
			IntervalStatusLabel.Text = "Time Interval: " + timer.Interval + " ms";
		}


		/// <summary>
		/// NextGen: Creates the next generation of cells
		/// </summary>
		private void NextGen()
		{
			/*
			* Follows these four rules from Conway's Game of Life
			* 1. Any live cell with fewer than two live neighbours dies, as if caused by under-population.
			* 2. Any live cell with two or three live neighbours lives on to the next generation.
			* 3. Any live cell with more than three live neighbours dies, as if by over-population.
			* 4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
			* Source: Wikipedia -> Conway's Game of Life
			* Link: https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
			* Retrieved: November 9th, 2015 at 11:14 PM EST
			*/

			// increment the generation counter
			++generation;

			// Create temporary multidimensional array: nextGen
			bool[,] nextGen = new bool[numCols, numRows];

			// Create temp neighbor count variable
			int neighbors;

			int x = 0, y;

			// for all cells
			for (; x < numCols; ++x)
			{
				for (y = 0; y < numRows; ++y)
				{
					// Get number of neighbors around cells[x,y]
					// If toroidal, call GetNumNeighborsToroidal(x, y)
					// If finite, call GetNumNeighborsFinite(x, y)
					neighbors = toroidal ? GetNumNeighborsToroidal(x, y) : GetNumNeighborsFinite(x, y);

					// Let A: 2==neighbors (cell has two neighbors)
					// Let B: 3==neighbors (cell has three neighbors)
					// Let C: cells[x,y] (cell is alive)
					// Rule2: (A+B)(C) -> living cell stays alive
					// Rule4: (B)(~C) -> dead cell becomes new living cell
					// Alive iff [ ( (A+B)(C) ) + ( (B)(~C) ) ] = [ (A + B)C + B~C ]
					// = [ <(A + B)C> + B~C ] = [ <AC + BC> + B~C ] (Distributive Law) <(A + B)C> --> <AC + BC>
					// = [ AC + <BC + B~C> ] = [ AC + <B(C + ~C)> ] (Distributive Law) <BC + B~C> --> <B(C + ~C)>
					// = [ AC + B(<C + ~C>) ] = [ AC + B(<1>) ] (Negation Law) <C + ~C> --> <1>
					// = [ AC + <B(1)> ] = [ AC + <B> ] (Identity Law) <B(1)> --> <B>
					// = [ <AC + B> ] = [ <B + CA> ] (Commutative Law) <AC + B> --> <B + CA>
					// [ B + CA ] --> [ 3==neighbors || (cells[x,y] && 2==neighbors) ]
					if (3 == neighbors || cells[x, y] && 2 == neighbors) nextGen[x, y] = true;
				}
			}

			// Set universe equal to the new universe using its reference
			cells = nextGen;

			// Invalidate to redraw
			graphicsPanel.Invalidate();
		}
		/// <summary>
		/// RandomizeCells: Creates a randomized universe using a passed in seed
		/// </summary>
		/// <param name="_seed">Seed</param>
		private void RandomizeCells(int _seed)
		{
			// Pass the new seed to the Random object
			Reseed(_seed);

			// Turn off simulation and reset generation counter
			DisableSimulation();
			generation = 0;

			int x = 0, y;
			for (; x < numCols; ++x)
				for (y = 0; y < numRows; ++y)
					// Use rnd.Next(2) to make ~1/2 of the universe living cells
					// Use rnd.Next(3) to make ~1/3 of the universe living cells
					// Use rnd.Next(4) to make ~1/4 of the universe living cells
					// ...
					cells[x, y] = 0 == rnd.Next(3);

			// Invalidate to redraw
			graphicsPanel.Invalidate();
		}
		/// <summary>
		/// Reseed: Resets rnd, the Random object used for randomizing cells, using a new seed
		/// </summary>
		/// <param name="_seed">The new seed</param>
		private void Reseed(int _seed)
		{
			randomizeSeed = _seed;
			rnd = new Random(randomizeSeed);
			UpdateStatusBar();
		}
		/// <summary>
		/// ResetUniverse: Clears all cells, resets generation counter, and turns off the simulation
		/// </summary>
		private void ResetUniverse()
		{
			// Turn off simulation
			DisableSimulation();

			// Reset generation counter
			generation = 0;

			// Set universe (cells) to a new bool[,] (universe entirely of dead cells)
			cells = new bool[numCols, numRows];

			// Invalidate to redraw
			graphicsPanel.Invalidate();
		}


		/// <summary>
		/// GetNumNeighborsFinite: Count how many cells around cells[x,y] are alive in a finite universe
		/// </summary>
		/// <param name="x">X position of the location of the cell</param>
		/// <param name="y">Y position of the location of the cell</param>
		/// <returns>number of neighbors around the cell</returns>
		private int GetNumNeighborsFinite(int x, int y)
		{
			// Create temporary int values for up, down, left, and right
			int x_minus = x - 1; //left (W)
			int y_minus = y - 1; //up (N)
			int x_plus = x + 1; //right (E)
			int y_plus = y + 1; //down (S)

			// Create counter for living neighbors
			int neighbors = 0;

			// Top three neighbors (NW,N,NE)
			if (0 <= y_minus)
			{
				if (0 <= x_minus) if (cells[x_minus, y_minus]) ++neighbors; //top-left (x-,y-) [NW]
				if (x_plus < numCols) if (cells[x_plus, y_minus]) ++neighbors; //top-right (x+,y-) [NE]
				if (cells[x, y_minus]) ++neighbors; //top (x,y-) [N]
			}

			// Left and right neigbors (W,E)
			if (0 <= x_minus) if (cells[x_minus, y]) ++neighbors; //left (x-,y) [W]
			if (x_plus < numCols) if (cells[x_plus, y]) ++neighbors; //right (x+,y) [E]

			// Bottom three neighbors (SW,S,SE)
			if (y_plus < numRows)
			{
				if (0 <= x_minus) if (cells[x_minus, y_plus]) ++neighbors; //bottom-left (x-,y+) [SW]
				if (x_plus < numCols) if (cells[x_plus, y_plus]) ++neighbors; //bottom-right (x+,y+) [SE]
				if (cells[x, y_plus]) ++neighbors; //bottom (x,y+) [S]
			}

			return neighbors;
		}
		/// <summary>
		/// GetNumNeighborsToroidal: Count how many cells around cells[x,y] are alive in a toroidal universe
		/// </summary>
		/// <param name="x">X position of the location of the cell</param>
		/// <param name="y">Y position of the location of the cell</param>
		/// <returns>number of living neighbors around the cell</returns>
		private int GetNumNeighborsToroidal(int x, int y)
		{
			// Create temporary int values for up, down, left, and right
			int x_minus = x - 1; //left (W)
			int y_minus = y - 1; //up (N)
			int x_plus = x + 1; //right (E)
			int y_plus = y + 1; //down (S)

			// Wrap around
			if (x_minus < 0) x_minus = numCols - 1; //left of left column, wrap around to right side
			if (y_minus < 0) y_minus = numRows - 1; //above top row, wrap down to bottom row
			if (numCols <= x_plus) x_plus = 0; //right of right column, wrap around to left side
			if (numRows <= y_plus) y_plus = 0; //below bottom row, wrap up to top row

			// Create counter for living neighbors
			int neighbors = 0;

			// Check all eight neighbors
			if (cells[x_minus, y_minus]) ++neighbors; //top-left (x-,y-) [NW]
			if (cells[x, y_minus]) ++neighbors; //top (x,y-) [N]
			if (cells[x_plus, y_minus]) ++neighbors; //top-right (x+,y-) [NE]
			if (cells[x_plus, y]) ++neighbors; //right (x+,y) [E]
			if (cells[x_plus, y_plus]) ++neighbors; //bottom-right (x+,y+) [SE]
			if (cells[x, y_plus]) ++neighbors; //bottom (x,y+) [S]
			if (cells[x_minus, y_plus]) ++neighbors; //bottom-left (x-,y+) [SW]
			if (cells[x_minus, y]) ++neighbors; //left (x-,y) [W]

			return neighbors;
		}


		/// <summary>
		/// addColumn: Adds a new empty column to the right side of the universe
		/// </summary>
		private void addColumn()
		{
			++numCols;
			ResizeUniverse();
		}
		/// <summary>
		/// addRow: Adds a new empty row to the bottom of the universe
		/// </summary>
		private void addRow()
		{
			++numRows;
			ResizeUniverse();
		}
		/// <summary>
		/// removeColumn: Deletes and removes the rightmost column from the universe
		/// </summary>
		private void removeColumn()
		{
			if (1 < numCols) --numCols; // There must be at least 1 column in the universe
			ResizeUniverse();
		}
		/// <summary>
		/// removeRow: Deletes and removes the bottom row from the universe
		/// </summary>
		private void removeRow()
		{
			if (1 < numRows) --numRows; // There must be at least 1 row in the universe
			ResizeUniverse();
		}
		/// <summary>
		/// ResizeUniverse: Call this function when numRows changes or when numCols changes
		/// (Will also copy cells from old universe to new universe)
		/// </summary>
		private void ResizeUniverse()
		{
			// Create temporary universe
			bool[,] newCells = new bool[numCols, numRows];

			int x = 0, y;
			// Copy cells from current universe to temporary universe
			for (; x < numCols && x < cells.GetLength(0); ++x)
				for (y = 0; y < numRows && y < cells.GetLength(1); ++y)
					newCells[x, y] = cells[x, y];

			// Set universe equal to the new universe using its reference
			cells = newCells;

			// Disable row and column decrement buttons when numCols or numRows reaches 1
			if (numCols <= 1)
			{
				removeColumnToolStripButton.Enabled = false;
				removeColumnToolStripMenuItem.Enabled = false;
			}
			else if (!removeColumnToolStripButton.Enabled)
			{
				removeColumnToolStripButton.Enabled = true;
				removeColumnToolStripMenuItem.Enabled = true;
			}
			if (numRows <= 1)
			{
				removeRowToolStripButton.Enabled = false;
				removeRowToolStripMenuItem.Enabled = false;
			}
			else if (!removeRowToolStripButton.Enabled)
			{
				removeRowToolStripButton.Enabled = true;
				removeRowToolStripMenuItem.Enabled = true;
			}

			// Invalidate to redraw
			graphicsPanel.Invalidate();
		}


		/// <summary>
		/// translateDown: Moves all cells in the universe down one space, wraps around if toroidal
		/// </summary>
		private void translateDown()
		{
			// Create temporary cell universe
			bool[,] newCells = new bool[numCols, numRows];

			int x = 0, y = 1;

			// If a toroidal universe, copy the bottom row up to the top
			if (toroidal)
				for (; x < numCols; ++x)
					newCells[x, 0] = cells[x, numRows - 1];

			// Copy the rest of the rows, shifted down
			for (; y < numRows; ++y)
				for (x = 0; x < numCols; ++x)
					newCells[x, y] = cells[x, y - 1];

			// Set universe equal to the new universe using its reference
			cells = newCells;

			// Invalidate the graphics panel to redraw the universe
			graphicsPanel.Invalidate();
		}
		/// <summary>
		/// translateDown: Moves all cells in the universe up one space, wraps around if toroidal
		/// </summary>
		private void translateUp()
		{
			// Create temporary cell universe
			bool[,] newCells = new bool[numCols, numRows];

			int x = 0, y = 0;

			// If a toroidal universe, copy the top row down to the bottom
			if (toroidal)
				for (; x < numCols; ++x)
					newCells[x, numRows - 1] = cells[x, 0];

			// Copy the rest of the rows, shifted up
			for (; y < numRows - 1; ++y)
				for (x = 0; x < numCols; ++x)
					newCells[x, y] = cells[x, y + 1];

			// Set universe equal to the new universe using its reference
			cells = newCells;

			// Invalidate the graphics panel to redraw the universe
			graphicsPanel.Invalidate();
		}
		/// <summary>
		/// translateDown: Moves all cells in the universe right one space, wraps around if toroidal
		/// </summary>
		private void translateRight()
		{
			// Create temporary cell universe
			bool[,] newCells = new bool[numCols, numRows];

			int x = 1, y = 0;

			// If a toroidal universe, copy the right column around to the left
			if (toroidal)
				for (; y < numRows; ++y)
					newCells[0, y] = cells[numCols - 1, y];

			// Copy the rest of the columns, shifted right
			for (; x < numCols; ++x)
				for (y = 0; y < numRows; ++y)
					newCells[x, y] = cells[x - 1, y];

			// Set universe equal to the new universe using its reference
			cells = newCells;

			// Invalidate the graphics panel to redraw the universe
			graphicsPanel.Invalidate();
		}
		/// <summary>
		/// translateDown: Moves all cells in the universe left one space, wraps around if toroidal
		/// </summary>
		private void translateLeft()
		{
			// Create temporary cell universe
			bool[,] newCells = new bool[numCols, numRows];

			int x = 0, y = 0;

			// If a toroidal universe, copy the left column around to the right
			if (toroidal)
				for (; y < numRows; ++y)
					newCells[numCols - 1, y] = cells[0, y];

			// Copy the rest of the columns, shifted left
			for (; x < numCols - 1; ++x)
				for (y = 0; y < numRows; ++y)
					newCells[x, y] = cells[x + 1, y];

			// Set universe equal to the new universe using its reference
			cells = newCells;

			// Invalidate the graphics panel to redraw the universe
			graphicsPanel.Invalidate();
		}


		/// <summary>
		/// toggleToroidal: Switch between a toroidal universe and a finite universe
		/// </summary>
		private void toggleToroidal()
		{
			toroidal = !toroidal;
			UpdateStatusBar();
		}


		/// <summary>
		/// DisableSimulation: Turn off the automatic simulation
		/// </summary>
		private void DisableSimulation()
		{
			// Disable the simulation timer
			timer.Enabled = false;

			// (re)enable the Start button & menu item
			startToolStripButton.Enabled = true;
			startToolStripMenuItem.Enabled = true;

			// disable the Pause button & menu item
			pauseToolStripButton.Enabled = false;
			pauseToolStripMenuItem.Enabled = false;
		}
		/// <summary>
		/// EnableSimulation: Turn on the automatic simulation
		/// </summary>
		private void EnableSimulation()
		{
			// Enable timer to allow simulation to run
			timer.Enabled = true;

			// disable the Start button & menu item
			startToolStripButton.Enabled = false;
			startToolStripMenuItem.Enabled = false;

			// (re)enable the Pause button & menu item
			pauseToolStripButton.Enabled = true;
			pauseToolStripMenuItem.Enabled = true;
		}


		/// <summary>
		/// DrawCellsOverGrid: Call this draw function when (gridWidth less than 0)
		/// </summary>
		/// <param name="g">Graphics object from graphics panel paint event arguments [e.Graphics]</param>
		private void DrawCellsOverGrid(Graphics g)
		{
			// Calculate Cell Width and Height
			float cellWidth = (float)graphicsPanel.ClientSize.Width / numCols;
			float cellHeight = (float)graphicsPanel.ClientSize.Height / numRows;

			// Draw the grid before drawing the cells
			// Pass in Graphics object (g), width of a column (cellWidth), and height of a row (cellHeight)
			if (showGrid) DrawGrid(g, cellWidth, cellHeight);

			// Draw the living cells
			int x = 0, y;
			for (; x < numCols; ++x)
				for (y = 0; y < numRows; ++y)
					if (cells[x, y])
					{
						// Paint Solid Rectangle
						// brush: cellBrush
						// position: ( x*cellWidth , y*cellHeight )
						// size: ( cellWidth , cellHeight )
						g.FillRectangle(cellBrush, x * cellWidth, y * cellHeight, cellWidth, cellHeight);

						// Increment living cells counter
						++cellCount;
					}
		}
		/// <summary>
		/// DrawGridOverCells: Call this draw function when (gridWidth greater than or equals 0)
		/// </summary>
		/// <param name="g">Graphics object from graphics panel paint event arguments [e.Graphics]</param>
		private void DrawGridOverCells(Graphics g)
		{
			// Calculate Cell Width and Height
			float cellWidth = (float)graphicsPanel.ClientSize.Width / numCols;
			float cellHeight = (float)graphicsPanel.ClientSize.Height / numRows;

			// Draw the living cells
			int x = 0, y;
			for (; x < numCols; ++x)
				for (y = 0; y < numRows; ++y)
					if (cells[x, y])
					{
						// Paint Solid Rectangle
						// brush: cellBrush
						// position: ( x*cellWidth , y*cellHeight )
						// size: ( cellWidth , cellHeight )
						g.FillRectangle(cellBrush, x * cellWidth, y * cellHeight, cellWidth, cellHeight);

						// Increment living cells counter
						++cellCount;
					}

			// Draw the grid after drawing the cells
			// Pass in Graphics object (g), width of a column (cellWidth), and height of a row (cellHeight)
			if (showGrid && 0 != gridWidth) DrawGrid(g, cellWidth, cellHeight);
		}
		/// <summary>
		/// DrawGrid: Draw the grid of the universe
		/// </summary>
		/// <param name="g">Graphics object from graphics panel paint event arguments [e.Graphics]</param>
		/// <param name="columnWidth">Width of each column</param>
		/// <param name="rowHeight">Height of each row</param>
		private void DrawGrid(Graphics g, float columnWidth, float rowHeight)
		{
			float panelWidth = graphicsPanel.Width;
			float panelHeight = graphicsPanel.Height;

			// Draw vertical lines
			for (int c = 0; c <= numCols; ++c)
				g.DrawLine(gridPen, c * columnWidth, 0.0f, c * columnWidth, panelHeight);

			// Draw horizontal lines
			for (int r = 0; r <= numRows; ++r)
				g.DrawLine(gridPen, 0.0f, r * rowHeight, panelWidth, r * rowHeight);
		}

		/// <summary>
		/// IsCells: Check if file is a .cells file
		/// </summary>
		/// <param name="_filename">Filename</param>
		/// <returns>true if .cells, false otherwise</returns>
		static private bool IsCells(string _filename)
		{
			int filenameLength = _filename.Length;
			if (6 < filenameLength)
				if (('.' == _filename[filenameLength - 6]) &&
					('c' == _filename[filenameLength - 5] || 'C' == _filename[filenameLength - 5]) &&
					('e' == _filename[filenameLength - 4] || 'E' == _filename[filenameLength - 4]) &&
					('l' == _filename[filenameLength - 3] || 'L' == _filename[filenameLength - 3]) &&
					('l' == _filename[filenameLength - 2] || 'L' == _filename[filenameLength - 2]) &&
					('s' == _filename[filenameLength - 1] || 'S' == _filename[filenameLength - 1]))
					return true;
			return false;
		}

		/// <summary>
		/// OpenToolWindow: Open the tools window
		/// [DISABLED]
		/// </summary>
		private void OpenToolWindow()
		{
			if (tool == null)
			{
				tool = new ToolWindow();

				tool.FormClosed += Tool_FormClosed;

				tool.Show(this);
			}
		}


		#endregion

		// MainWindow Form initialization
		//
		public MainWindow()
		{
			InitializeComponent();

			// Initialize the universe (bool[,] cells) (default: 25×25)
			numRows = Properties.Settings.Default.NumRows;
			numCols = Properties.Settings.Default.NumCols;
			cells = new bool[numCols, numRows];

			// Seed the Random object to a seed (defualt: 0)
			randomizeSeed = Properties.Settings.Default.RandomSeed;
			rnd = new Random(randomizeSeed);

			// Initialize the Timer object for simulation, set Interval (default: 1)
			timer = new Timer();
			timer.Interval = Properties.Settings.Default.TimerInterval;
			DisableSimulation();
			timer.Tick += timer_Tick;

			// Initialize Color variables
			backgroundColor = Properties.Settings.Default.BackgroundColor; // default: Wheat (ARGB:#FFF5DEB3)
			cellColor = Properties.Settings.Default.CellColor; // default: Navy (ARGB:#FF000080)
			gridColor = Properties.Settings.Default.GridColor; // default: Red (ARGB:#FFFF0000)

			toroidal = Properties.Settings.Default.Toroidal; // Toroidal or Finite (default: Toroidal)
			gridWidth = Properties.Settings.Default.LineWidth; // Line Width (default: -2), negative value indicates lines are drawn underneath the cells
			showGrid = Properties.Settings.Default.ShowGrid; // Show the grid (default: true)
			showHideGridToolStripMenuItem.Text = showGrid ? "Hide &Grid" : "Show &Grid";

			// Set tool to null to indicate that it is inactive
			tool = null;

			cellCount = 0; // Start with 0 living cells
			generation = 0; // Start generation counter at 0

			ResizeUniverse();
			UpdateColors();
			UpdateStatusBar();
		}
	}
}

// ☻☺ トニー ☺☻ -- Y:2015年 M:11月 //
// TONY (Antonio V. Perez) //
// November 2015 //