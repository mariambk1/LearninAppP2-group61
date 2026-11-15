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

        public void ViewProgram(ICommandProgram program)
        {
            if (program == null)
                return;

            System.Text.StringBuilder stringBuilder = new();

            foreach (var command in program.Commands)
                stringBuilder.AppendLine(command.ToString());

            this.Text = stringBuilder.ToString();
        }

        public  CommandProgram ParseProgramText(string rawText)
        {
            string[] lines = rawText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            if (lines.Length == 0 || string.IsNullOrWhiteSpace(lines[0]))
            {
                throw new FormatException("The program is empty.");
            }

            string programName = lines[0].Trim();
            int lineIndex = 1;

            FileImport importer = new();
            List<ICommand> commands = importer.ReadCommands(lines, ref lineIndex, 0);

            return new CommandProgram(programName, commands);
        }
    }
}
