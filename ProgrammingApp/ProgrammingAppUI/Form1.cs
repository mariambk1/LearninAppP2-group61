using ProgrammingApp;

namespace ProgrammingAppUI
{
    public partial class Form1 : Form
    {
        private Grid grid;
        private GridUI gridUI;
        private ProgramEditor programEditor;
        private ICommandProgram program;
        private RunButton runButton;
        private MetricsButton metricsButton;
        private LoadProgramButton loadProgramButton;
        private ContextMenuStrip loadProgramMenu;
        private ToolStripMenuItem basicProgram;
        private ToolStripMenuItem advancedProgram;
        private ToolStripMenuItem expertProgram;
        private ToolStripMenuItem importProgram;

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
            this.Controls.Add(gridUI);

            programEditor = new();
            this.Controls.Add(programEditor);

            runButton = new();
            runButton.Click += RunButton_Click; 
            this.Controls.Add(runButton);

            metricsButton = new();
            this.Controls.Add(metricsButton);

            loadProgramButton = new();
            loadProgramButton.Click += LoadProgramButton_Click;
            this.Controls.Add(loadProgramButton);

            loadProgramMenu = new();

            // dropdown menu items
            basicProgram = new("Basic");
            basicProgram.Click += LoadBasic_Click;

            advancedProgram = new("Advanced");
            advancedProgram.Click += LoadAdvanced_Click;

            expertProgram = new("Expert");
            expertProgram.Click += LoadExpert_Click;

            importProgram = new("Import file");
            importProgram.Click += ImportProgram_Click;

            loadProgramMenu.Items.Add(basicProgram);
            loadProgramMenu.Items.Add(advancedProgram);
            loadProgramMenu.Items.Add(expertProgram);
            loadProgramMenu.Items.Add(importProgram);

        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (program == null)
            {
                MessageBox.Show("Please load or type a program first.");
                return;
            }

            string text = programEditor.Text;
            ICommandProgram currentProgram = programEditor.ParseProgramText(text);

            currentProgram.RunProgram(grid);
            gridUI.Invalidate();
        }

        public void LoadProgramButton_Click(object sender, EventArgs e)
        {
            loadProgramMenu.Show(loadProgramButton, new Point(0, loadProgramButton.Height));
        }

        public void LoadBasic_Click(object sender, EventArgs e)
        {
            program = ProgramExamples.GetRandom(ObjectType.Basic);
            programEditor.ViewProgram(program);
        }

        public void LoadAdvanced_Click(object sender, EventArgs e)
        {
            program = ProgramExamples.GetRandom(ObjectType.Advanced);
            programEditor.ViewProgram(program);
        }

        public void LoadExpert_Click(object sender, EventArgs e)
        {
            program = ProgramExamples.GetRandom(ObjectType.Expert);
            programEditor.ViewProgram(program);
        }

        public void ImportProgram_Click(object sender, EventArgs e)
        {
            program = CommandLineInteraction.ImportFile();
        }
    }
}
