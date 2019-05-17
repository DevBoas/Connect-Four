using Connect_Four.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Connect_Four
{
    public partial class Form1 : Form
    {
        private int[][] jaggedArray3 =
        {
            new int[] { 0, 0, 0, 0, 0, 0 }, // stack 1
            new int[] { 0, 0, 0, 0, 0, 0 }, // stack 2
            new int[] { 0, 0, 0, 0, 0, 0 }, // stack 3
            new int[] { 0, 0, 0, 0, 0, 0 }, // stack 4
            new int[] { 0, 0, 0, 0, 0, 0 }, // stack 5
            new int[] { 0, 0, 0, 0, 0, 0 }, // stack 6
            new int[] { 0, 0, 0, 0, 0, 0 }, // stack 7
        };

        PictureBox ball = null;
        int loc = -1;
        int lastoffset = 0;

        public Form1()
        {
            InitializeComponent();
            CreateBall();
        }

        private void CreateBall()
        {
            PictureBox newBall = new PictureBox();
            newBall.SizeMode = PictureBoxSizeMode.AutoSize;
            newBall.Image = (Image)Resources.ResourceManager.GetObject("Red");
            newBall.MouseClick += PictureBox1_MouseClick;
            newBall.Visible = false;

            ball = newBall;
            pictureBox1.Controls.Add(newBall);
            if ((loc == -1) || (CanPlace(loc) == -2))
            {
                Debug.WriteLine("Not appear");
                return;
            }
            newBall.Visible = true;
            newBall.Location = new Point((pictureBox1.Location.X - lastoffset) + (loc * 100), pictureBox1.Location.Y - 20);
                
            
        }

        private int CanPlace(int c)
        {
            if (c == -1 || (c != -1) && (c > 6))
                return -1;
            //Debug.WriteLine(c.ToString());
            for (int i = 0; i < jaggedArray3[c].Length; i++)
            {
                if (jaggedArray3[c][i] == 0)

                    return i;
            }
            return -2;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = (sender as Control);
            int x = 0;
            int y = 0;
            if (c.Name =="pictureBox1")
            {
                x = pictureBox1.Location.X;
                y = pictureBox1.Location.Y;
            }
            
            if (e.Location.X >= pictureBox1.Location.X && e.Location.X <= pictureBox1.Location.X + pictureBox1.Size.Width)
                loc = (e.Location.X) / 100; 
            else
                loc = -1;
            if (CanPlace(loc) == -2 && ball != null)
            {
                ball.Visible = false;
                return;
            }

            if (loc != -1 && ball != null)
            {
                ball.Visible = true;
                int offset = 0;
                if (loc == 0 || loc == 1)
                    offset = 1;
                else if (loc == 2 || loc == 3 || loc == 4 )
                    offset = 2;
                else if (loc == 6 || loc == 5)
                    offset = 3;
                lastoffset = offset;
                ball.Location = new Point((pictureBox1.Location.X - offset) + (loc * 100), pictureBox1.Location.Y - 20);
            }
            else if (ball != null)
                ball.Visible = false;
            label1.Text = "loc "+ loc.ToString() + " MousePos X" + (e.Location.X + x).ToString() + " Y" + (e.Location.Y + y).ToString();
        }

        public static void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private int InsertBall(int where)
        {
            for (int i = 0; i < jaggedArray3[where].Length; i++)
            {
                if (jaggedArray3[where][i] == 0)
                {
                    jaggedArray3[where][i] = 1;
                    return i+1;
                }
            }
            return -1;
        }
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (ball == null)
                return;
            int height = InsertBall(loc);
            if (height == -1)
                return;
            PictureBox KeepBall = ball;
            int offset = 0;
           
            ball = null;
            offset = (height - 1) * 4;
            while (KeepBall.Location.Y < (pictureBox1.Size.Height - (KeepBall.Size.Height * height) - (14 * height) - offset))
            {
                KeepBall.Location = new Point(KeepBall.Location.X, KeepBall.Location.Y + 1);
                wait(1);
            }
            CreateBall();
        }
    }
}