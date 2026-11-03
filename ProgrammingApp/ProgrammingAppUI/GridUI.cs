using ProgrammingApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgrammingAppUI
{
    internal class GridUI : Panel
    {
        private Grid grid;

        public GridUI()
        {
            this.BackColor = Color.FromArgb(82,82,82);
        }
        public void SetGrid(Grid grid)
        {
            this.grid = grid;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (grid == null) return;

            Graphics g = e.Graphics;
            Pen gridPen = new Pen(Color.Gray);
            int width = grid.cellWidth * grid.cols;
            int height = grid.cellHeight * grid.rows;
            int cellWidth = grid.cellWidth;
            int cellHeight = grid.cellHeight;

            for (int x = 0; x <= grid.cols; x++)
                g.DrawLine(gridPen, x * cellWidth, 0, x * cellWidth, height);

            for (int y = 0; y <= grid.rows; y++)
                g.DrawLine(gridPen, 0, y * cellHeight, width, y * cellHeight);

            // draw the player
            Player player = grid.GetPlayer();
            Brush playerBrush = new SolidBrush(Color.CornflowerBlue);

            if (player != null)
            {
                int x = player.position.X;
                int y = player.position.Y;
                int drawX = x * cellWidth;
                int drawY = y * cellHeight;

                g.FillEllipse(playerBrush, drawX + 20, drawY + 20, cellWidth - 40, cellHeight - 40);
            }
        }
    }
}
