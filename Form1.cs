using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace PetriNets
{
    [Serializable]
    public partial class Form1 : Form
    {
        private int[,] drawing_Field;
        private int sc = 10;
        private Rectangle rect;
        private List<int> positions;// = new List<int>();
        private List<Position> arr_pos;
        private List<Transition> arr_trans;
        int field_Size;
        int move;
        bool redraw = false;
        bool istomove = false;
        private int[] el_num;
        //int x, y;
        private System.Collections.Generic.Dictionary<int, Point> el_pos;
        private List<Dictionary<Point, Point>> el_con_points;
        //private System.Collections.DictionaryEntry el_pos_entry = new System.Collections.DictionaryEntry();
        bool down = false;
        int line_counter = 0;
        bool drlin = false;
        Button[] buttons;
        private Dictionary<int, int> indexpair;
        private Lines lines;
        private Point temp;

        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;

        Brush sel_br = Brushes.Indigo;
        //Edited
        int selected = 0;

        public Form1()
        {
            InitializeComponent();
            clear_field();
            
            this.splitContainer1.Panel2.MouseDown += new MouseEventHandler(Panel2_MouseDown);
            this.splitContainer1.Panel2.MouseUp += new MouseEventHandler(Panel2_MouseUp);
            this.splitContainer1.Panel2.MouseMove += new MouseEventHandler(Panel2_MouseMove);
            //    this.splitContainer1.Panel2.MouseEnter += new EventHandler(Panel2_MouseEnter);
            this.splitContainer1.Panel2.MouseDoubleClick += new MouseEventHandler(Panel2_MouseDoubleClick);
            rect.X = 0;
            rect.Y = 0;
            rect.Height = 20;
            rect.Width = 20;
            //pen = new Pen(Brushes.Black, 2);
            buttons = new Button[5];
            buttons[0] = this.cursor;
            buttons[1] = this.P;
            buttons[2] = this.t;
            buttons[3] = this.tt;
            buttons[4] = this.connector;
            el_num = new int[5];
            el_num[1] = 10;
            el_num[2] = -1;
            el_num[3] = -2;
            this.Text = "Petri Nets Project";

            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(this.splitContainer1.Panel2.Width + 1, this.splitContainer1.Panel2.Height + 1);
            grafx = context.Allocate(this.splitContainer1.Panel2.CreateGraphics(), new Rectangle(0, 0, this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height));
            grafx.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height));
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
            //del
            //drawing_Field = new int[field_Size, field_Size];
            //el_pos = new System.Collections.Generic.Dictionary<int, Point>();
            //el_con = new List<KeyValuePair<int, int>>();            
            //el_con_points = new List<Dictionary<Point, Point>>();             
            //lines = new Lines(sc);
        }

        void Panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // move = 0;
            int x = e.X / sc;
            int y = e.Y / sc;
            istomove = false;
            if (drawing_Field[x, y] > 10)
            {
                positions[drawing_Field[x, y] - 11]++;
                ((Position)arr_pos[drawing_Field[x, y] - 11]).Tokens++;
            }
        }

        //   void Panel2_MouseEnter(object sender, EventArgs e)
        //   {
        //       throw new NotImplementedException();
        //    }
        void Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            
            int x = e.X / sc;
            int y = e.Y / sc;
            if (drawing_Field[x, y] > 10 && e.Button.Equals(MouseButtons.Right) && positions[drawing_Field[x, y] - 11] > 0)
            {
                positions[drawing_Field[x, y] - 11]--;
                ((Position)arr_pos[drawing_Field[x, y] - 11]).Tokens--;
                move = 0;
            }
            else
            {
                down = true;
                int val = 0;
                move = drawing_Field[x, y];
                selected = move; //edited
                for (int i = 0; i < 5; i++)
                {
                    if (buttons[i].Focused) val = i;
                }
                switch (val)
                {
                    case 1:
                        {
                            el_num[val]++;
                            move = el_num[val];
                            positions.Add(0);
                            el_pos.Add(el_num[val], new Point(x, y));
                            //
                            arr_pos.Add(new Position(el_num[val] - 11));
                            indexpair.Add(el_num[val], ((Position)arr_pos.Last()).ID);
                            //
                            istomove = true;
                            break;
                        }
                    case 2:
                        {
                            el_num[val] -= 2;
                            move = el_num[val];
                            el_pos.Add(el_num[val], new Point(x, y));
                            //
                            arr_trans.Add(new Transition(el_num[val]));
                            indexpair.Add(el_num[val], ((Transition)arr_trans.Last()).ID);
                            //
                            istomove = true;
                            break;
                        }
                    case 3:
                        {
                            el_num[val] -= 2;
                            move = el_num[val];
                            el_pos.Add(el_num[val], new Point(x, y));
                            //
                            arr_trans.Add(new Transition(el_num[val]));
                            indexpair.Add(el_num[val], ((Transition)arr_trans.Last()).ID);
                            //
                            istomove = true;
                            break;
                        }
                    case 4:
                        {
                            if (move < 0 || move > 10)
                            {
                               // line_counter++;
                                temp = e.Location;
                                el_con_points.Add(new Dictionary<Point, Point>());
                                // el_con_points[line_counter].Add(new Point((el_pos[move].X + 3 )*sc, (el_pos[move].Y + 1) *sc),  new Point(0,0));
                                drlin = true;
                            }
                            break;
                        }
                }
            }

        }

        void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            int xm = e.X / sc;
            int ym = e.Y / sc;
            if (down && xm > 0 && ym > 0 && !buttons[4].Focused)
            {
                if (move != 0)
                {
                    el_pos[move] = new Point(xm, ym);
                    //  updateTable(); //ХЗ че изменилось когда убрал, если чет не работает то скорее всего бага здесь
                    // this.splitContainer1.Panel2.Invalidate(new Rectangle(sc * (Math.Min(xm, x) - 3), sc * (Math.Min(ym, y) - 3), sc * (Math.Abs(xm - x) + 2) , sc * (Math.Abs(ym - y))+2));

                    clearline();
                    reconnect_nodes();
                    DrawToBuffer(grafx.Graphics);
                    this.splitContainer1.Panel2.Invalidate();
                    istomove = true;
                    redraw = true;
                }
            }
            if (buttons[4].Focused && drlin && down)
            {
                el_con_points[0] = new Dictionary<Point, Point>();
                try
                {
                    el_con_points[0].Add(temp, new Point(e.Location.X, temp.Y));
                    el_con_points[0].Add(new Point(e.Location.X, temp.Y), e.Location);
                    DrawToBuffer(grafx.Graphics);
                    this.splitContainer1.Panel2.Invalidate();
                }
                catch (Exception)
                {

                }
                istomove = false;
            }
        }


        private void clearline()
        {
            /*        foreach(KeyValuePair<Point,Point> dic in el_con_points[line])
            {
                drawing_Field[lines.mo(dic.Key).X, lines.mo(dic.Key).Y] = 0;
                drawing_Field[lines.mo(dic.Value).X - 1, lines.mo(dic.Value).Y] = 0;
                //drawing_Field[lines.mo(dic.Value).X, lines.mo(dic.Value).Y] = 0;
            }*/
            for (int i = 0; i < field_Size; i++)
                for (int j = 0; j < field_Size; j++)
                {
                    if (drawing_Field[i, j] == 1)
                        drawing_Field[i, j] = 0;
                }
        }
        
        private void reconnect_nodes()
        {
            line_counter = 1;
            el_con_points.Clear();
            //    el_con_points = new List<Dictionary<Point,Point>>();
            el_con_points.Add(new Dictionary<Point, Point>());
            foreach (Position pos in arr_pos)
            {
                foreach (KeyValuePair<Transition, int> inp in pos.DictOfIn)
                {
                    Transition p = inp.Key;
                    int count = inp.Value;
                    while (count-- > 0)
                    {
                        lines.connect_two_points(el_pos[pos.ID + 11], el_pos[p.Fieldnumber]);
                        el_con_points.Add(new Dictionary<Point, Point>());
                        el_con_points[line_counter++] = lines.Points;
                    }
                }
            }

            foreach (Transition tr in arr_trans)
            {
                foreach (KeyValuePair<Position, int> inp in tr.DictOfIn)
                {
                    Position p = inp.Key;
                    int count = inp.Value;
                    while (count-- > 0)
                    {
                        lines.connect_two_points(el_pos[tr.Fieldnumber], el_pos[p.ID + 11]);
                        el_con_points.Add(new Dictionary<Point, Point>());
                        el_con_points[line_counter++] = lines.Points;
                    }
                }
            }
            //on the base of matrixes recreate connections between nodes
        }

        void Panel2_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           // bgc.Invalidate();
            el_con_points[0] = new Dictionary<Point, Point>();
            int x = e.X / sc;
            int y = e.Y / sc;
            if (istomove)
            {

                int test = 0;
                if (move > 0) { test = 1; }
                if (move < 0) { test = 2; }
                bool free = test_free(x, y, test);
                int step_fi = 1;
                int x0 = x;
                int y0 = y;
                int ro = 1;
                while (!free && move != 0)
                {
                    // if (step_fi < 1) step_fi = 1;
                    for (int fi = 0; fi < 360; fi += step_fi)
                    {
                        x0 = x + (int)(ro * Math.Cos(fi * Math.PI / 180));
                        y0 = y + (int)(ro * Math.Sin(fi * Math.PI / 180));
                        if (test_free(x0, y0, test))
                        {
                            x = x0;
                            y = y0;
                            free = true;
                            //el_pos[move] = new Point(x, y);
                            break;
                        }
                        else
                        {
                            x0 = x;
                            y0 = y;
                        }
                    }
                    ro++;

                }
                el_pos[move] = new Point(x, y);
                updateTable();
                DrawToBuffer(grafx.Graphics);
                if (!buttons[0].Focused && !redraw)
                   this.splitContainer1.Panel2.Invalidate(new Rectangle(sc * (x - 1), sc * (y - 1), sc * 7, sc * 7));
                else
                {
                    this.splitContainer1.Panel2.Invalidate();
                    redraw = true;
                }
            }
            else
            {
                if (buttons[4].Focused && drlin && (drawing_Field[x, y] < 0 || drawing_Field[x, y] > 10))
                {
                    // el_con_points[line_counter][new Point((el_pos[move].X + 3) * sc, (el_pos[move].Y + 1) * sc)] = e.Location;
                    drawline(el_pos[move], el_pos[drawing_Field[x, y]], line_counter);
                    drlin = false;
                }
                DrawToBuffer(grafx.Graphics);
                this.splitContainer1.Panel2.Invalidate();
            }
            down = false;
            move = 0;
            istomove = false;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e) //changed
        {
         //   System.Drawing.Drawing2D.GraphicsPath p = new System.Drawing.Drawing2D.GraphicsPath();
            grafx.Render(e.Graphics);
        }
        
        public void updateTable()
        {
            for (int i = 0; i < field_Size; i++)
                for (int j = 0; j < field_Size; j++)
                {
                    if (drawing_Field[i, j] != 1)
                    {
                        drawing_Field[i, j] = 0;
                    }
                }

            foreach (KeyValuePair<int, Point> kvp in el_pos)
            {
                if (kvp.Key > 10)
                {
                    for (int i = kvp.Value.X; i < kvp.Value.X + 3; i++)
                        for (int j = kvp.Value.Y; j < kvp.Value.Y + 3; j++)
                        {
                            // if(drawing_Field[i, j] == 0)
                            drawing_Field[i, j] = kvp.Key;
                        }
                }
                else if (kvp.Key < 0)
                {

                    for (int i = kvp.Value.Y; i < kvp.Value.Y + 5; i++)
                    {
                        drawing_Field[kvp.Value.X, i] = kvp.Key;
                    }
                }
            }
        }

        private bool test_free(int x, int y, int obj)
        {
            bool flag = true;
            switch (obj)
            {
                case 1:
                    {
                        if (x < 2 || y < 2 || x > field_Size || y > field_Size) return false;
                        for (int i = x - 1; i <= x + 3; i++)
                            for (int j = y - 1; j <= y + 3; j++)
                            {
                                if (drawing_Field[i, j] != 0 && drawing_Field[i, j] != move && drawing_Field[i, j] !=1) flag = false;
                            }

                        break;
                    }
                case 2:
                    {
                        if (y < 2 || y > field_Size) return false;
                        for (int j = y - 1; j <= y + 5; j++)
                        {
                            if (drawing_Field[x, j] != 0 && drawing_Field[x, j] != move) flag = false;
                        }
                        break;
                    }
                case 3:
                    {
                        if (drawing_Field[x, y] != 0 && drawing_Field[x, y] != move) flag = false;
                        break;
                    }
            }
            return flag;
        }

        private void drawline(Point from, Point to, int linenum)
        {
            lines.Field_matrix = drawing_Field;
            //int[] path = new int[2];
            //path[0] = drawing_Field[from.X, from.Y];
            //path[1] = drawing_Field[to.X, to.Y];
            //bool flag = (path[0] > 10 && path[1] > 10) || (path[0] < 0 && path[1] < 0);
            //if (!from.Equals(to) && !flag && lines.connect_two_points(from, to))
            //{
            //    el_con_points[linenum] = lines.Points;
            //}
            //
            if (drawing_Field[from.X, from.Y] > 10 && drawing_Field[to.X, to.Y] < 0)
            {
                ((Position)arr_pos[drawing_Field[from.X, from.Y] - 11]).addIn(arr_trans[indexpair[drawing_Field[to.X, to.Y]]]);
            }

            if (drawing_Field[from.X, from.Y] < 0 && drawing_Field[to.X, to.Y] > 10)
            {
                ((Transition)arr_trans[indexpair[drawing_Field[from.X, from.Y]]]).addIn(arr_pos[drawing_Field[to.X, to.Y] - 11]);
            }
            clearline();
            reconnect_nodes();
                      
        }
        
        private void clear_field()
        {
            
            drawing_Field = new int[field_Size, field_Size];
            el_pos = new System.Collections.Generic.Dictionary<int, Point>();
            el_con_points = new List<Dictionary<Point, Point>>();
            el_con_points.Add(new Dictionary<Point, Point>());
            lines = new Lines(sc);

            indexpair = new Dictionary<int, int>();
            arr_pos = new List<Position>();
            arr_trans = new List<Transition>();
            positions = new List<int>();

            el_num = new int[5];
            el_num[1] = 10;
            el_num[2] = -1;
            el_num[3] = -2;
            line_counter = 0;
        }

        private void di_Click(object sender, EventArgs e)
        {
            new TableHandler(arr_pos, arr_trans, 1).ShowDialog(this);
        }

        private void dq_Click(object sender, EventArgs e)
        {
            new TableHandler(arr_pos, arr_trans, 2).ShowDialog(this);
        }

        private void markup_Click(object sender, EventArgs e)
        {
            new TableHandler(arr_pos, arr_trans, 3).ShowDialog(this);
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog savedialog = new SaveFileDialog();
            //savedialog.InitialDirectory = " . ";
            savedialog.Filter = "petri net files (*.pni)|*.pni|All flles(*.*)|*.*";
            savedialog.FilterIndex = 1;
            savedialog.RestoreDirectory = true;
            savedialog.FileName = "PetriNetsProject";

            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(savedialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                List<Object> serializedinfo = new List<Object>();
                serializedinfo.Add(arr_pos);//+
                serializedinfo.Add(arr_trans);//+
                serializedinfo.Add(indexpair);
                //serializedinfo.Add(drawing_Field);
                serializedinfo.Add(el_con_points);
                serializedinfo.Add(el_pos);
                //serializedinfo.Add(positions);//-
                serializedinfo.Add(el_num);//+
                serializedinfo.Add(line_counter);//+
                

                bf.Serialize(fs, serializedinfo);

                fs.Close();
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog savedialog = new OpenFileDialog();
            //savedialog.InitialDirectory = ".";
            savedialog.Filter = "petri net files (*.pni)|*.pni|All flles(*.*)|*.*";
            savedialog.FilterIndex = 1;
            savedialog.RestoreDirectory = true;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(savedialog.FileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                List<Object> serializedinfo = new List<Object>();
                serializedinfo = (List<Object>)bf.Deserialize(fs);
                fs.Close();
                clear_field();
                arr_pos = (List<Position>)serializedinfo[0];
                arr_trans = (List<Transition>)serializedinfo[1];
                drawing_Field = (int[,])serializedinfo[3];
                el_con_points = (List<Dictionary<Point, Point>>)serializedinfo[4];
                el_pos = (Dictionary<int, Point>)serializedinfo[5];

                el_num = (int[])serializedinfo[7];
                line_counter = (int)serializedinfo[8];
                
                fs.Close();
                //index
                ((Transition)arr_trans[0]).setIndex(arr_trans.Count);
                //indexpair
                for (int i = 0; i < arr_pos.Count; i++)
                {
                    indexpair.Add(arr_pos[i].ID + 11, arr_pos[i].ID);
                }
                for (int i = 0; i < arr_trans.Count; i++)
                {
                    indexpair.Add(arr_trans[i].getIndex(), arr_trans[i].ID);
                }
                //positions
                for (int i = 0; i < arr_pos.Count; i++)
                {
                    positions.Add(arr_pos[i].Tokens);
                }
                //drawing field
                //подумать сделать

                this.splitContainer1.Panel2.Invalidate();
            }               
        }        

        private void clear_Click(object sender, EventArgs e)
        {
            if (arr_trans.Count != 0) { arr_trans[0].setIndex(0); }
            clear_field();
            DrawToBuffer(grafx.Graphics);
            this.splitContainer1.Panel2.Invalidate();
        }

        private void DrawToBuffer(Graphics g)
        {

            grafx.Graphics.FillRectangle(Brushes.White, 0, 0, this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height);
            //Test part
            //for (int i = 0; i < field_Size; i++)
            //    for (int j = 0; j < field_Size; j++)
            //    {
            //        if (drawing_Field[i, j] != 0)
            //            e.Graphics.DrawString("" + drawing_Field[i, j], Font, Brushes.Blue, sc * i, sc * j);
            //    }
            // e.Graphics.DrawString("" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width, Font, Brushes.Blue, sc * 20, sc * 20);   
            foreach (KeyValuePair<int, Point> kvp in el_pos)
            {
                if (kvp.Key > 10)
                {
                    g.DrawEllipse((kvp.Key == selected) ? new Pen(sel_br, 2) : new Pen(Brushes.Black, 2), sc * kvp.Value.X, sc * kvp.Value.Y, sc * 3, sc * 3);
                    g.DrawString("" + positions[kvp.Key - 11], Font, (kvp.Key == selected) ? Brushes.HotPink : Brushes.Black, sc * (kvp.Value.X + 1), sc * (kvp.Value.Y + 1));
                }
                else if (kvp.Key % (-2) == -1)
                {
                    g.FillRectangle((kvp.Key == selected) ? sel_br : Brushes.Black, sc * kvp.Value.X, sc * kvp.Value.Y, sc, sc * 5);
                }
                else if (kvp.Key < 0 && kvp.Key % (-2) == 0)
                {
                    g.DrawRectangle((kvp.Key == selected) ? new Pen(sel_br, 2) : new Pen(Brushes.Black, 2), sc * kvp.Value.X, sc * kvp.Value.Y, sc, sc * 5);
                }
            }
            foreach (Dictionary<Point, Point> lin in el_con_points)
            {
                foreach (KeyValuePair<Point, Point> points in lin)
                {
                    Pen p = new Pen(Brushes.Green, 2);
                    if (points.Value.X != 0)
                        g.DrawLine(p, points.Key, points.Value);
                    //Console.Write("From: {0:d} : {1:d} To: {2:d} : {3:d} \n", points.Key.X, points.Key.Y, points.Value.X, points.Value.Y);
                }
            }
        }

        private void tree_Click(object sender, EventArgs e)
        {

        }

        private void rename_Click(object sender, EventArgs e)
        {
            //переименовать при удалении
            for (int i = 0; i < arr_pos.Count; i++)
            {
                ((Position)arr_pos[i]).ID = i;
            }
            for (int i = 0; i < arr_trans.Count; i++)
            {
                ((Transition)arr_trans[i]).ID = i;
            }
            ((Transition)arr_trans[0]).setIndex(arr_trans.Count);
        }
    }
}

