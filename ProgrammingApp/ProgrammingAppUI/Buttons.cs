using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingAppUI
{
    internal class RunButton : Button
    {
        public RunButton()
        {
            this.BackColor = Color.Green;
            this.Text = "Run";
            this.Font = new Font("Arial", 12, FontStyle.Bold);
            this.ForeColor = Color.White;
            this.Location = new Point(900, 1200);
            this.Size = new Size(200, 80);

            this.MouseEnter += (sender, e) => { this.BackColor = Color.DarkGreen; this.Cursor = Cursors.Hand; };
            this.MouseLeave += (sender, e) => this.BackColor = Color.Green;
        }
    }

    internal class MetricsButton : Button
    {
        public MetricsButton()
        {
            this.BackColor = Color.DodgerBlue;
            this.Text = "Metrics";
            this.Font = new Font("Arial", 12, FontStyle.Bold);
            this.ForeColor = Color.White;
            this.Location = new Point(1150, 1200);
            this.Size = new Size(200, 80);

            this.MouseEnter += (sender, e) => { this.Cursor = Cursors.Hand; this.BackColor = Color.RoyalBlue; };
            this.MouseLeave += (sender, e) => this.BackColor = Color.DodgerBlue; 

        }
    }
}
