using ProgrammingApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingAppUI
{
    internal class ProgramEditor : TextBox
    {
        public ProgramEditor()
        {
            this.BackColor = Color.FromArgb(45, 45, 45);
            this.ForeColor = Color.White;
            this.Font = new("Arial", 10.5f, FontStyle.Regular);
            this.Location = new Point(500, 110);
            this.Size = new Size(1200, 1000);
            this.Multiline = true;
            this.ScrollBars = ScrollBars.Vertical;
        }

        public void ViewProgram(CommandProgram program)
        {
            if (program == null)
                return;

            StringBuilder text = new();
            AppendCommands(program.Commands, text, 0);
            Text = text.ToString();
        }

        private void AppendCommands(
            IEnumerable<ICommand> commands,
            StringBuilder text,
            int depth)
        {
            string indent = new string(' ', depth * 4);

            foreach (ICommand command in commands)
            {
                switch (command)
                {
                    case MoveCommand move:
                        text.AppendLine($"{indent}Move {move.Steps}");
                        break;

                    case TurnCommand turn:
                        text.AppendLine($"{indent}Turn {turn.Direction}");
                        break;

                    case RepeatCommand repeat:
                        text.AppendLine($"{indent}Repeat {repeat.Times} times");
                        AppendCommands(repeat.Commands, text, depth + 1);
                        break;

                    case RepeatUntilCommand until:
                        text.AppendLine($"{indent}RepeatUntil {until.Condition}");
                        AppendCommands(until.Commands, text, depth + 1);
                        break;
                }
            }
        }
        public  CommandProgram ParseProgramText(string rawText)
        {
            string[] lines = rawText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            if (lines.Length == 0 || string.IsNullOrWhiteSpace(lines[0]))
            {
                throw new FormatException("The program is empty.");
            }

            string programName = lines[0].Trim();
            int lineIndex = 0;

            FileImport importer = new();
            List<ICommand> commands = importer.ReadCommands(lines, ref lineIndex, 0);

            return new CommandProgram(programName, commands);
        }

        // pressing tab inserts 4 spaces
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                int selectionStart = this.SelectionStart;

                this.SelectedText = "    ";

                this.SelectionStart = selectionStart + 4;

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
