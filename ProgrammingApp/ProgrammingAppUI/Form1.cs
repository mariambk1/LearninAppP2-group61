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
        private ToolStripMenuItem importExercise;
        private TextBox traceOutput;
        private System.Windows.Forms.Timer animationTimer;

        public Form1()
        {
            InitializeComponent();
            InitializeComponentCustom();
            this.BackColor = Color.FromArgb(36, 36, 36);
            this.Text = "Programming App";
            grid = new Grid();
            gridUI.SetGrid(grid);
            this.WindowState = FormWindowState.Maximized;
            LayoutControls();
            this.Resize += (sender, e) => LayoutControls();
            animationTimer = new();
            animationTimer.Interval = 500;
            animationTimer.Tick += AnimationTimer_Tick;
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
            metricsButton.Click += MetricsButton_Click;
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

            Size buttonSize = new Size(180, 50);

            loadProgramButton.Size = buttonSize;
            runButton.Size = buttonSize;
            metricsButton.Size = buttonSize;

            loadProgramButton.Location = new Point(20, 20);
            runButton.Location = new Point(220, 20);
            metricsButton.Location = new Point(420, 20);

            importExercise = new("Load Exercise...");
            importExercise.Click += ImportExercise_Click;

            loadProgramMenu.Items.Add(new ToolStripSeparator());
            loadProgramMenu.Items.Add(importExercise);

            traceOutput = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                BackColor = programEditor.BackColor,
                ForeColor = programEditor.ForeColor,
                Font = programEditor.Font
            };

            this.Controls.Add(traceOutput);
        }

        private void LayoutControls()
        {
            int margin = 20;
            int top = 200;
            int availableWidth = Math.Max(100, ClientSize.Width - 3 * margin);
            int availableHeight = Math.Max(100, ClientSize.Height - top - margin);
            int editorWidth = availableWidth / 2;
            int traceHeight = availableHeight / 3;
            int editorHeight = availableHeight - traceHeight - margin;

            programEditor.SetBounds(
                margin,
                top,
                editorWidth,
                editorHeight);

            gridUI.SetBounds(
                editorWidth + 2 * margin,
                top,
                availableWidth - editorWidth,
                availableHeight);
            traceOutput.SetBounds(
                margin,
                top + editorHeight + margin,
                editorWidth,
                traceHeight);
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(programEditor.Text))
            {
                MessageBox.Show("Please load or type a program first.");
                return;
            }

            grid.Reset();
            traceOutput.Clear();

            ICommandProgram currentProgram = programEditor.ParseProgramText(programEditor.Text);
            grid.GetTrace().Clear();
            traceOutput.Clear();
            currentProgram.RunProgram(grid);

            traceOutput.Text = string.Join(Environment.NewLine, grid.GetTrace()) + Environment.NewLine + Environment.NewLine + grid.GetFinalState();

            grid.StartAnimation();
            animationTimer.Start();
            
            gridUI.Invalidate();
        }

        private void MetricsButton_Click(object sender, EventArgs e)
        {
            ICommandProgram currentProgram =
                programEditor.ParseProgramText(programEditor.Text);
            Metrics metrics = new(currentProgram.Commands);

            MessageBox.Show(
                $"Commands: {metrics.CommandCount}\n" +
                $"Repeat commands: {metrics.RepeatCount}\n" +
                $"Maximum nesting: {metrics.MaxNestingLevel}",
                "Program metrics");
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
            using OpenFileDialog dialog = new();

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            program = new FileImport().ImportFromTextFile(dialog.FileName);
            programEditor.ViewProgram(program);
        }

        public void ImportExercise_Click(object sender, EventArgs e)
        {
            using OpenFileDialog dialog = new();
            dialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                grid.LoadExercise(dialog.FileName);
                gridUI.SetGrid(grid);

                MessageBox.Show("Exercise map loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading exercise file:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (grid.IsAnimating)
            {
                grid.StepAnimation();

                gridUI.Invalidate();
            }
            else
            {
                animationTimer.Stop();
            }
        }
    }
}
