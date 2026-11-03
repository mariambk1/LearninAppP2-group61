using ProgrammingApp;

namespace ProgrammingAppUI
{
    public partial class Form1 : Form
    {
        private Grid grid;
        private GridUI gridUI;
        public Form1()
        {
            InitializeComponent();
            InitializeComponentCustom();
            this.BackColor = Color.FromArgb(36, 36, 36);
            this.Text = "Programming App";
            grid = new Grid();
            gridUI.SetGrid(grid);
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeComponentCustom()
        {
            gridUI = new();
            gridUI.Location = new Point(1900, 200);
            gridUI.Size = new Size(600, 600);
            this.Controls.Add(gridUI);
        }
    }
}
