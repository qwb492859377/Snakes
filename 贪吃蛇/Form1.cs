using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 贪吃蛇 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        Snack snack;
        private void button1_Click(object sender, EventArgs e) {
        }

        private void Form1_Load(object sender, EventArgs e) {
            snack = new Snack(pictureBox1, 24, 16, 25);
            snack.Init();
        }

        int sta = 0, lsta = 1;  

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.Up: sta = 0; break;
                case Keys.Left: sta = 1; break;
                case Keys.Down: sta = 2; break;
                case Keys.Right: sta = 3; break;
            }

            if (snack.end) {
                lsta = 1;
                snack.Init();
                return;
            }

            if (!snack.end && timer1.Enabled == false && (e.KeyCode != Keys.Right)) {
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (snack.end) {
                timer1.Stop();
            }
            else {
                if (lsta == (sta + 2) % 4) {
                    sta = lsta;
                }
                lsta = sta;
                snack.Run(sta);
            }
        }
    }
}