using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace 贪吃蛇 {
    public class Draw {
        int w;
        PictureBox p;
        Graphics g; 

        public Draw(PictureBox _p, int c) {
            p = _p; w = c;
            Bitmap bmp = new Bitmap(p.Width, p.Height);
            g = Graphics.FromImage((Image)bmp);
            g.Clear(Color.White);

            p.Image = bmp;
            p.Refresh();
        }

        public void ClearAll(){
            g.Clear(Color.White);
            p.Refresh();
        }
        public void DrawSquare(int i,int j){
            int x = w * (i - 1), y = w * (j - 1);
            SolidBrush brush = new SolidBrush(Color.BlueViolet);

            g.FillRectangle(brush, new Rectangle(x, y, w, w));
            g.DrawRectangle(new Pen(Color.DarkGray, 1), new Rectangle(x, y, w, w));
            p.Refresh();
        }
        public void DrawCycle(int i, int j) {
            int x = w * (i - 1), y = w * (j - 1);
            SolidBrush brush = new SolidBrush(Color.Green);

            g.FillPie(brush, x, y, w, w, 0, 360);
            p.Refresh();
        }
        public void Clear(int i, int j) {
            int x = w * (i - 1), y = w * (j - 1);
            SolidBrush brush = new SolidBrush(Color.White);

            g.FillRectangle(brush, new Rectangle(x, y, w, w));
            g.DrawRectangle(new Pen(Color.White, 1), new Rectangle(x, y, w, w));
            p.Refresh();
        }
    }

    struct Point {
        public int x, y;
        public Point(int _x, int _y) {
            x = _x; y = _y;
        }
    }

    public class Snack {
        PictureBox p;
        int m, n, w;
        Draw draw;

        public bool end = false;
        Point food = new Point();
        LinkedList<Point> L=new LinkedList<Point>();
        bool[,] vis;
        int score;

        public Snack(PictureBox _p, int _m, int _n, int _w) {
            p = _p; m = _m; n = _n; w = _w;
            draw = new Draw(p, w);
        }

        public void NewFood() {
            int x, y;
            Random ran = new Random();
            do {
                x = ran.Next(1, m);
                y = ran.Next(1, n);
            } while (vis[x,y]);
            food.x = x;
            food.y = y;
            draw.DrawCycle(x, y);
        }

        public void Init() {
            draw.ClearAll();

            score = 0;
            end = false;
            vis = new bool[m + 1, n + 1];

            L.Clear();
            L.AddLast(new Point(10, 10));
            L.AddLast(new Point(11, 10));
            L.AddLast(new Point(12, 10));

            draw.DrawSquare(10, 10);
            draw.DrawSquare(11, 10);
            draw.DrawSquare(12, 10);

            vis[10, 10] = vis[11, 10] = vis[12, 10] = true;
            NewFood();
        }

        public void Run(int sta) {
            int[,] dist = new int[4, 2] { { 0, -1 }, { -1, 0 }, { 0, 1 }, { 1, 0 } };//上左下右

            Point fp = L.First(), lp = L.Last();
            fp.x += dist[sta, 0];
            fp.y += dist[sta, 1];

            if (fp.x < 1 || fp.y < 1 || fp.x > m || fp.y > n || vis[fp.x,fp.y]) {
                GameEnd();
                return;
            }

            draw.DrawSquare(fp.x, fp.y);
            vis[fp.x, fp.y] = true;
            L.AddFirst(fp);

            if (fp.x == food.x && fp.y == food.y) {
                score++;
                NewFood();
            }
            else {
                draw.Clear(lp.x, lp.y);
                vis[lp.x, lp.y] = false;
                L.RemoveLast();
            }
        }

        public void GameEnd() {
            end = true;
            MessageBox.Show("游戏结束,得分：" + score + "分");
        }
    }
}
