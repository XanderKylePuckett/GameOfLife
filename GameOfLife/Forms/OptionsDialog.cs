// Game of Life //
// Options Dialog //
// ☻☺ トニー ☺☻ -- Y:2015年 M:11月 //

using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class OptionsDialog : Form
    {
        // EventHandler for Apply button
        //
        public event EventHandler Apply;

        // OptionsDialog Form initialization
        //
        public OptionsDialog()
        {
            InitializeComponent();

            PreviewGraphicsPanel.Invalidate();
        }

        #region << Properties >>
        // Finite Mode
        // Radio Buttons: Toroidal, Finite
        public bool Finite
        {
            get
            {
                return !radioButtonToroidal.Checked;
            }
            set
            {
                if (value)
                    radioButtonFinite.Checked = true;
                else
                    radioButtonToroidal.Checked = true;
            }
        }
        // Rows
        // NumericUpDown: [1]->[1000]
        public int NumberOfRows
        {
            get
            {
                return (int)numericUpDownRows.Value;
            }
            set
            {
                if ((decimal)value >= numericUpDownRows.Maximum)
                {
                    numericUpDownRows.Value = numericUpDownRows.Maximum;
                }
                else if ((decimal)value <= numericUpDownRows.Minimum)
                {
                    numericUpDownRows.Value = numericUpDownRows.Minimum;
                }
                else
                {
                    numericUpDownRows.Value = (decimal)value;
                }
            }
        }
        // Columns
        // NumericUpDown: [1]->[1000]
        public int NumberOfColumns
        {
            get
            {
                return (int)numericUpDownColumns.Value;
            }
            set
            {
                if ((decimal)value >= numericUpDownColumns.Maximum)
                {
                    numericUpDownColumns.Value = numericUpDownColumns.Maximum;
                }
                else if ((decimal)value <= numericUpDownColumns.Minimum)
                {
                    numericUpDownColumns.Value = numericUpDownColumns.Minimum;
                }
                else
                {
                    numericUpDownColumns.Value = (decimal)value;
                }
            }
        }
        // Time Interval
        // NumericUpDown: [1]->[500]
        public int Interval
        {
            get
            {
                return (int)numericUpDownInterval.Value;
            }
            set
            {
                if ((decimal)value >= numericUpDownInterval.Maximum)
                {
                    numericUpDownInterval.Value = numericUpDownInterval.Maximum;
                }
                else if ((decimal)value <= numericUpDownInterval.Minimum)
                {
                    numericUpDownInterval.Value = numericUpDownInterval.Minimum;
                }
                else
                {
                    numericUpDownInterval.Value = (decimal)value;
                }
            }
        }
        // Background Color
        // B Button, Using Backcolor, Opens Color Dialog
        public Color Background
        {
            get
            {
                return buttonBackgroundColor.BackColor;
            }
            set
            {
                buttonBackgroundColor.BackColor = value;
                buttonBackgroundColor.ForeColor = Color.FromArgb(~(value.ToArgb()));
            }
        }
        // Grid Color
        // G Button, Using Backcolor, Opens Color Dialog
        public Color GridColor
        {
            get
            {
                return buttonGridColor.BackColor;
            }
            set
            {
                buttonGridColor.BackColor = value;
                buttonGridColor.ForeColor = Color.FromArgb(~(value.ToArgb()));
            }
        }
        // Cell Color
        // C Button, Using Backcolor, Opens Color Dialog
        public Color CellsColor
        {
            get
            {
                return buttonCellColor.BackColor;
            }
            set
            {
                buttonCellColor.BackColor = value;
                buttonCellColor.ForeColor = Color.FromArgb(~(value.ToArgb()));
            }
        }
        // Show Grid
        // Checkbox
        public bool ShowGrid
        {
            get
            {
                return checkBoxShowGrid.Checked;
            }
            set
            {
                checkBoxShowGrid.Checked = value;
            }
        }
        // Gridline Width
        // NumericUpDown: [-5]->[5]
        public int LineWidth
        {
            get
            {
                return (int)numericUpDownLineWidth.Value;
            }
            set
            {
                if ((decimal)value >= numericUpDownLineWidth.Maximum)
                {
                    numericUpDownLineWidth.Value = numericUpDownLineWidth.Maximum;
                }
                else if ((decimal)value <= numericUpDownLineWidth.Minimum)
                {
                    numericUpDownLineWidth.Value = numericUpDownLineWidth.Minimum;
                }
                else
                {
                    numericUpDownLineWidth.Value = (decimal)value;
                }
            }
        }
        #endregion

        #region << Events >>
        // Show Color Dialog to change the background color setting
        private void buttonBackgroundColor_Click(object sender, EventArgs e)
        {
            // Create an instance of ColorDialog
            ColorDialog dlg = new ColorDialog();

            // Pass in the button's backcolor
            dlg.Color = buttonBackgroundColor.BackColor;

            // Show the color dialog
            if (DialogResult.OK == dlg.ShowDialog())
            {
                // Get user's setting and save it to the button's backcolor property
                buttonBackgroundColor.BackColor = dlg.Color;

                // Set button's forecolor to its backcolor's bitwise complement
                buttonBackgroundColor.ForeColor = Color.FromArgb(~(dlg.Color.ToArgb()));

                // Invalidate preview panel to redraw preview
                PreviewGraphicsPanel.Invalidate();
            }
        }
        // Show Color Dialog to change the grid color setting
        private void buttonGridColor_Click(object sender, EventArgs e)
        {
            // Create an instance of ColorDialog
            ColorDialog dlg = new ColorDialog();

            // Pass in the button's backcolor
            dlg.Color = buttonGridColor.BackColor;

            // Show the color dialog
            if (DialogResult.OK == dlg.ShowDialog())
            {
                // Get user's setting and save it to the button's backcolor property
                buttonGridColor.BackColor = dlg.Color;

                // Set button's forecolor to its backcolor's bitwise complement
                buttonGridColor.ForeColor = Color.FromArgb(~(dlg.Color.ToArgb()));

                // Invalidate preview panel to redraw preview
                PreviewGraphicsPanel.Invalidate();
            }
        }
        // Show Color Dialog to change the cell color setting
        private void buttonCellColor_Click(object sender, EventArgs e)
        {
            // Create an instance of ColorDialog
            ColorDialog dlg = new ColorDialog();

            // Pass in the button's backcolor
            dlg.Color = buttonCellColor.BackColor;

            // Show the color dialog
            if (DialogResult.OK == dlg.ShowDialog())
            {
                // Get user's setting and save it to the button's backcolor property
                buttonCellColor.BackColor = dlg.Color;

                // Set button's forecolor to its backcolor's bitwise complement
                buttonCellColor.ForeColor = Color.FromArgb(~(dlg.Color.ToArgb()));

                // Invalidate preview panel to redraw preview
                PreviewGraphicsPanel.Invalidate();
            }
        }
        // Restore all settings to defaults
        private void defaultsButton_Click(object sender, EventArgs e)
        {
            // Finite Mode
            Finite = false;
            // Rows
            NumberOfRows = 25;
            // Columns
            NumberOfColumns = 25;
            // Time Interval
            Interval = 1;
            // Background Color
            Background = Color.Wheat;
            // Grid Color
            GridColor = Color.Red;
            // Cell Color
            CellsColor = Color.Navy;
            // Show Grid
            ShowGrid = true;
            // Gridline Width
            LineWidth = -2;

            // Invalidate to redraw
            PreviewGraphicsPanel.Invalidate();
        }
        // Apply Settings
        private void applyButton_Click(object sender, EventArgs e)
        {
            if (Apply != null)
                Apply(this, EventArgs.Empty);
        }
        // When Show Grid setting changes, Invalidate the preview panel to redraw preview
        private void checkBoxShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            PreviewGraphicsPanel.Invalidate();
        }
        // When Line Width setting changes, Invalidate the preview panel to redraw preview
        private void numericUpDownLineWidth_ValueChanged(object sender, EventArgs e)
        {
            PreviewGraphicsPanel.Invalidate();
        }
        // Paint event for preview panel
        private void PreviewGraphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            // Preview Panel, 4x4 grid, apply color settings and gridline settings
            // panel dimensions: 120px x 120px
            // cell dimensions: 30px x 30px
            // Preview this universe:
            // . . O .
            // . O O .
            // . O . .
            // O O . .
            // (2,0), (2,1), (1,1), (1,2), (1,3), (0,3)

            float panelWidth = PreviewGraphicsPanel.Width;
            float panelHeight = PreviewGraphicsPanel.Height;
            float cellWidth = panelWidth / 4.0f;
            float cellHeight = panelHeight / 4.0f;

            Graphics g = e.Graphics;

            // Make a new SolidBrush for the cells using CellsColor
            Brush cellBrush = new SolidBrush(CellsColor);

            // Change the backcolor of the graphics panel to the new bg color (Background)
            PreviewGraphicsPanel.BackColor = Background;

            int x, y = 0;

            // negative LineWidth --> cells over grid
            if (LineWidth < 0)
            {
                // Draw Grid
                if (ShowGrid)
                {
                    // Make a new Pen for the gridlines using GridColor and the absolute value of LineWidth
                    Pen gridPen = new Pen(GridColor, -LineWidth);

                    for (x = 0; x <= 4; ++x)
                        g.DrawLine(gridPen, x * cellWidth, 0.0f, x * cellWidth, panelHeight);
                    for (; y <= 4; ++y)
                        g.DrawLine(gridPen, 0.0f, y * cellHeight, panelWidth, y * cellHeight);
                }

                // Draw cells
                // (2,0), (2,1), (1,1), (1,2), (1,3), (0,3)
                x = 2; y = 0;
                g.FillRectangle(cellBrush, x * cellWidth, y++ * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x-- * cellWidth, y * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x * cellWidth, y++ * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x * cellWidth, y++ * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x-- * cellWidth, y * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x * cellWidth, y * cellHeight, cellWidth, cellHeight);

            }
            // positive LineWidth --> grid over cells
            else
            {
                // Draw cells
                // (2,0), (2,1), (1,1), (1,2), (1,3), (0,3)
                x = 2;
                g.FillRectangle(cellBrush, x * cellWidth, y++ * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x-- * cellWidth, y * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x * cellWidth, y++ * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x * cellWidth, y++ * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x-- * cellWidth, y * cellHeight, cellWidth, cellHeight);
                g.FillRectangle(cellBrush, x * cellWidth, y * cellHeight, cellWidth, cellHeight);

                // Draw Grid
                if (ShowGrid && 0 != LineWidth)
                {
                    // Make a new Pen for the gridlines using GridColor and the absolute value of LineWidth
                    Pen gridPen = new Pen(GridColor, LineWidth);

                    for (; x <= 4; ++x)
                        g.DrawLine(gridPen, x * cellWidth, 0.0f, x * cellWidth, panelHeight);
                    for (y = 0; y <= 4; ++y)
                        g.DrawLine(gridPen, 0.0f, y * cellHeight, panelWidth, y * cellHeight);
                }
            }
        }
        #endregion
    }
}

// ☻☺ トニー ☺☻ -- Y:2015年 M:11月 //
// TONY (Antonio V. Perez) //
// November 2015 //