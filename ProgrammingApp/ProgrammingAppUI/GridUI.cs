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
            this.ResizeRedraw = true;
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
            int cellWidth = Math.Max(1, (ClientSize.Width - 1) / grid.cols);
            int cellHeight = Math.Max(1, (ClientSize.Height - 1) / grid.rows);
            int width = cellWidth * grid.cols;
            int height = cellHeight * grid.rows;

            for (int y = 0; y < grid.rows; y++)
            {
                for (int x = 0; x < grid.cols; x++)
                {
                    int drawX = x * cellWidth;
                    int drawY = y * cellHeight;

                    if (grid.HasExercise())
                    {
                        char cell = grid.GetCell(x, y);
                        if (cell == '+')
                        {
                            // draw wall
                            Brush wallBrush = new SolidBrush(Color.FromArgb(50, 50, 50));
                            g.FillRectangle(wallBrush, drawX, drawY, cellWidth, cellHeight);
                        }
                        else if (cell == 'x')
                        {
                            // draw end target as an X
                            Pen targetBrush = new Pen(Color.FromArgb(46, 139, 87), 4);
                            float padding = cellWidth * 0.25f;

                            g.DrawLine(targetBrush,
                                        drawX + padding,
                                        drawY + padding,
                                        drawX + cellWidth - padding,
                                        drawY + cellHeight - padding);

                            g.DrawLine(targetBrush,
                                drawX + padding,
                                drawY + cellHeight - padding,
                                drawX + cellWidth - padding,
                                drawY + padding);
                        }
                    }

                    g.DrawRectangle(gridPen, drawX, drawY, cellWidth, cellHeight);
                }
            }
            // draw the player
            Player player = grid.GetPlayer();
            Brush playerBrush = new SolidBrush(Color.CornflowerBlue);

            if (player != null)
            {
                int x = player.position.X;
                int y = player.position.Y;
                int drawX = x * cellWidth;
                int drawY = y * cellHeight;

                g.FillEllipse(playerBrush, drawX + cellWidth * 0.2f, drawY + cellHeight * 0.2f, cellWidth * 0.6f, cellHeight * 0.6f);
            }
        }
    }
}
