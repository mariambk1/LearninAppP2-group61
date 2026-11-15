using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingAppUI
{
    internal class LoadProgramButton : Button
    {

        public LoadProgramButton()
        {
            this.BackColor = Color.DodgerBlue;
            this.Text = "Load Program";
            this.Font = new Font("Arial", 12, FontStyle.Bold);
            this.ForeColor = Color.White;
            this.Location = new Point(100, 100);
            this.Size = new Size(250, 80);

            this.MouseEnter += (sender, e) => { this.BackColor = Color.RoyalBlue; this.Cursor = Cursors.Hand; };
            this.MouseLeave += (sender, e) => this.BackColor = Color.DodgerBlue;
        }
    }
}
