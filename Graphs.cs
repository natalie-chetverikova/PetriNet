using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PetriNets
{
    public partial class Graphs : Form
    {
        List<string[]> wl;
        List<String> names = new List<string>();
        List<Point> nodes = new List<Point>();
        int dist;
        private const int radius = 20;
        bool down = false;
        int selected = -1;
        public Graphs(List<Position> arr_pos, List<Transition> arr_trans)
        {
            InitializeComponent();
            
            wl = generateList(arr_pos, arr_trans);
            createNodes();
            //this.Size = new Size(2 * dist + 300, 2 * dist + 300);
            panel1.Size = this.Size;
        //    panel1.BackColor = Color.Silver;
            this.SizeChanged += new EventHandler(Graphs_SizeChanged);
            this.panel1.MouseDown += new MouseEventHandler(Graphs_MouseDown);
            this.panel1.MouseUp += new MouseEventHandler(Graphs_MouseUp);
            this.panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
        //    this.SetScrollState(1, true);
            
        }

        void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.X + radius < this.Width && e.Y + radius < this.Height && down && selected > -1)
            {
                nodes[selected] = new Point(e.X - radius, e.Y - radius);
                panel1.Invalidate();
            }
        }

        void Graphs_MouseUp(object sender, MouseEventArgs e)
        {
            if (selected > -1)
            {
                nodes[selected] = new Point(e.X - radius, e.Y - radius);
                selected = -1;
                down = false;
            }
        }

        void Graphs_MouseDown(object sender, MouseEventArgs e)
        {
            down = true;
            int i = -1;
            foreach (Point p in nodes)
            {
                i++;
                if (e.X - p.X < 2 * radius && e.X > p.X && Math.Abs(e.Y - p.Y) < 2*radius && e.Y > p.Y)
                {
                    selected = i;
                    break;
                }
                selected = -1;
            } 
            panel1.Invalidate();
        }

        void Graphs_SizeChanged(object sender, EventArgs e)
        {
            panel1.Size = this.Size;
           // panel1.Size = new Size(2 * dist + 100, 2 * dist + 100);
            createNodes();
            panel1.Invalidate();

        }
      
        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
                int n = names.Count;
                Pen p = new Pen(Brushes.Salmon, 2);
                p.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

                Font f = new Font(FontFamily.GenericSerif, 8, FontStyle.Bold);
                Brush fbrush = Brushes.Crimson;
                Pen mpen = new Pen(Brushes.LimeGreen, 2);
                Pen spen = new Pen(Brushes.Violet, 2);
                Pen tpen = new Pen(Brushes.Yellow, 2);
                for (int i = 0; i < n; i++)
                {

                    e.Graphics.DrawEllipse((i==selected)?spen:mpen, nodes[i].X, nodes[i].Y, radius * 2, radius * 2);
                    e.Graphics.DrawString("M" + names[i], f, fbrush, nodes[i].X + radius - 8, nodes[i].Y + radius - 5);
                }
                foreach (String[] s in wl)
                {
                    Point from = nodes[Int32.Parse(s[0])];
                    Point to = nodes[Int32.Parse(s[1])];
                    String name = s[2];
                    Point n1 = new Point(from.X + radius,from.Y + radius);
                    Point n2 = new Point(to.X + radius,to.Y + radius);

                    int d =(int) Math.Sqrt((n2.X - n1.X) * (n2.X - n1.X) + (n2.Y - n1.Y) * (n2.Y - n1.Y));
                    if (d == 0) d = 1;
                    if (d == radius) d = radius + 1;
                    n1.X = n1.X - (n1.X - n2.X) * radius / d;
                    n1.Y = n1.Y - (n1.Y - n2.Y) * radius / d;

                    n2.X = n2.X - (n2.X - n1.X) * radius / (d - radius);
                    n2.Y = n2.Y - (n2.Y - n1.Y) * radius / (d - radius);
                    Point arrlength = n2;
        
                    arrlength.X = n2.X - (n2.X - n1.X) * radius / (d - radius);
                    arrlength.Y = n2.Y - (n2.Y - n1.Y) * radius / (d - radius);
                    int dx = n2.X - arrlength.X;
                    int dy = n2.Y - arrlength.Y;
                    Point B = arrlength;
                    Point C = arrlength;
                    B.X = n2.X - (int)(dx * 0.866 - dy * 0.5);
                    B.Y = n2.Y - (int)(dx * 0.5 + dy * 0.866);
                    C.X = n2.X - (int)(dx * 0.866 + dy * 0.5);
                    C.Y = n2.Y - (int)(- dx * 0.5 + dy * 0.866);

                    e.Graphics.DrawLine(tpen, n1, n2);
                    e.Graphics.DrawLine(tpen, B, n2);
                    e.Graphics.DrawLine(tpen, C, n2);
                    e.Graphics.DrawString(name, f, fbrush, (n1.X + n2.X) / 2, (n1.Y + n2.Y) / 2);
                }

              //  e.Graphics.DrawLine(Pens.Black, Width / 2, 0, Width / 2, Height);
              //  e.Graphics.DrawLine(Pens.Black, 0, Height / 2, Width, Height / 2);
        }

        private void createNodes()
        {
            nodes = new List<Point>();
            foreach (String [] s in wl)
            {
                if (!names.Contains(s[0])) names.Add(s[0]);
                if (!names.Contains(s[1])) names.Add(s[1]);
            }
            int n = names.Count;
            if (n > 0)
            {
                dist = (int)((2 * n * radius + 10) / Math.PI);
                int fstep = 360 / n;

                Point center = new Point(this.Width / 2, this.Height / 2);

                for (int fi = 0; fi < 360; fi += fstep)
                {
                    int x = center.X + (int)(dist * Math.Cos(fi * Math.PI / 180)) - radius;
                    int y = center.Y + (int)(dist * Math.Sin(fi * Math.PI / 180)) - radius;
                    if (x < 0) { x = radius; }
                    if (y < 0) { y = radius; }
                    if (x > this.Width) { x = Width - 2 * radius; }
                    if (y > this.Height) { y = Height - 4 * radius; }
                    nodes.Add(new Point(x, y));
                }
            }
        }

        private List<string[]> generateList(List<Position> arr_pos, List<Transition> arr_trans)
        {
            List<string[]> mmt = new List<string[]>();
            int[] initmark = new int[arr_pos.Count];
            int[] currmark = new int[arr_pos.Count];
            int[] newmark = new int[arr_pos.Count];
            List<int[]> list_mark = new List<int[]>();
            int[,] di = new int[arr_trans.Count(), arr_pos.Count()];
            int[,] dq = new int[arr_trans.Count, arr_pos.Count];
            bool choice = true;
            bool end = false;
            int mark_ptr = 1;
            //начальная маркировка
            for (int i = 0; i < arr_pos.Count; i++)
            {
                initmark[i] = ((Position)arr_pos[i]).Tokens;
            }
            list_mark.Add(initmark);

            currmark = new int[arr_pos.Count];

            for (int i = 0; i < arr_pos.Count; i++)
            {
                currmark[i] = initmark[i];
            }
            //Ди
            for (int j = 0; j < arr_trans.Count; j++)
            {
                for (int i = 0; i < arr_pos.Count; i++)
                {
                    if (((Position)arr_pos[i]).DictOfIn.ContainsKey(arr_trans[j]))
                    {
                        di[j, i] = ((Position)arr_pos[i]).DictOfIn[arr_trans[j]];
                    }
                    else
                    {
                        di[j, i] = 0;
                    }
                }
            }
            //Дкью
            for (int j = 0; j < arr_trans.Count; j++)
            {
                for (int i = 0; i < arr_pos.Count; i++)
                {
                    if (((Transition)arr_trans[j]).DictOfIn.ContainsKey(arr_pos[i]))
                    {
                        dq[j, i] = ((Transition)arr_trans[j]).DictOfIn[arr_pos[i]];
                    }
                    else
                    {
                        dq[j, i] = 0;
                    }
                }
            }

            //начало обхода
            while (!end)
            {
                //начало нового яруса, ветки

                for (int j = 0; j < arr_trans.Count; j++)
                {
                    choice = false;
                    for (int i = 0; i < arr_pos.Count; i++)
                    {
                        if (currmark[i] * di[j, i] != 0 && currmark[i] >= di[j, i])
                        {
                            choice = true;
                        }
                    }

                    //если переход произойдет
                    if (choice)
                    {
                        //новая маркировка
                        for (int q = 0; q < arr_pos.Count; q++)
                        {
                            newmark[q] = currmark[q] - di[j, q] + dq[j, q];
                        }

                        //this.Text += " /curm : " + currmark[0] + " " + currmark[1] + " --> ";
                        //this.Text += "newm : " + newmark[0] + " " + newmark[1] + " / ";

                        //добавляем новую маркировку
                        bool containsMark = true;
                        foreach (int[] mark in list_mark)
                        {
                            containsMark = true;
                            for (int i = 0; i < mark.Length; i++)
                            {
                                containsMark &= (mark[i] == newmark[i]);
                            }
                            if (containsMark) { break; }
                        }

                        if (!containsMark)
                        {
                            int[] buf = new int[newmark.Length];
                            for (int i = 0; i < buf.Length; i++)
                            {
                                buf[i] = newmark[i];
                            }
                            list_mark.Add(buf);
                        }
                        else
                        {
                        }
                        //========
                        string[] buff = new string[3];
                        bool equals = true;

                        for (int q = 0; q < list_mark.Count; q++)
                        {
                            equals = true;
                            for (int i = 0; i < currmark.Length; i++)
                            {
                                equals &= ((list_mark.ElementAt(q))[i] == currmark[i]);
                            }
                            if (equals) { buff[0] = "" + q; break; }
                        }

                        for (int q = 0; q < list_mark.Count; q++)
                        {
                            equals = true;
                            for (int i = 0; i < currmark.Length; i++)
                            {
                                equals &= ((list_mark.ElementAt(q))[i] == newmark[i]);
                            }
                            if (equals) { buff[1] = "" + q; break; }
                        }
                        buff[2] = arr_trans[j].Name;
                        mmt.Add(buff);
                        //=========
                    }
                }

                try
                {
                    for (int q = 0; q < currmark.Length; q++)
                    {
                        currmark[q] = (list_mark[mark_ptr])[q];
                    }

                    mark_ptr++;

                }
                catch (Exception)
                {
                    end = true;
                }
                //if (tier == 10) break;
            };

            return mmt;
        }
    }
}
