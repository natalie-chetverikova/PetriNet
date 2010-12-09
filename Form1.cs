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
//blablabla
        private int[,] drawing_Field;
        private int sc = 10;
        private Rectangle rect;
        private List<Position> arr_pos;
        private List<Transition> arr_trans;
        int field_Size;
        int move;
        bool redraw = false;
        bool istomove = false;
        private int[] el_num;
        //int x, y;
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
        Brush fishkabrush = Brushes.Yellow;
        //Edited
        int selected = 0;
        Point selected_point = new Point(1,1);

        public Form1()
        {
            InitializeComponent();
            
            field_Size = Math.Max(this.splitContainer1.Panel2.Width/sc + 10, this.splitContainer1.Panel2.Height/sc + 10);
            clean_field();
            
            this.splitContainer1.Panel2.MouseDown += new MouseEventHandler(Panel2_MouseDown);
            this.splitContainer1.Panel2.MouseUp += new MouseEventHandler(Panel2_MouseUp);
            this.splitContainer1.Panel2.MouseMove += new MouseEventHandler(Panel2_MouseMove);
            this.SizeChanged += new EventHandler(Form1_SizeChanged);
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
            this.Text = "Petri Panda Project";
            //текстбокс для переименовывания нодов
            nodname.Visible = false;

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

        void Form1_SizeChanged(object sender, EventArgs e)
        {
            field_Size = Math.Max(this.splitContainer1.Panel2.Width / sc + 10, this.splitContainer1.Panel2.Height / sc + 10);
            drawing_Field = new int[field_Size, field_Size];
            updateTable();
            context.MaximumBuffer = new Size(this.splitContainer1.Panel2.Width + 1, this.splitContainer1.Panel2.Height + 1);
            grafx = context.Allocate(this.splitContainer1.Panel2.CreateGraphics(), new Rectangle(0, 0, this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height));
            grafx.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height));
            DrawToBuffer(grafx.Graphics);
            this.splitContainer1.Panel2.Invalidate();
        }

        void Panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // move = 0;
            int x = e.X / sc;
            int y = e.Y / sc;
            istomove = false;
            if (drawing_Field[x, y] > 10)
            {
                arr_pos[indexpair[drawing_Field[x, y]]].Tokens++;
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
            if (drawing_Field[x, y] > 10 && e.Button.Equals(MouseButtons.Right) && arr_pos[indexpair[drawing_Field[x, y]]].Tokens > 0)
            {
                arr_pos[indexpair[drawing_Field[x, y]]].Tokens--;
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
                            //news
                            Position p = new Position(el_num[val]);
                            p.Location = new Point(x, y); 
                            arr_pos.Add(p);
                            indexpair.Add(el_num[val], p.ID);
                            //
                            istomove = true;
                            break;
                        }
                    case 2:
                        {
                            el_num[val] -= 2;
                            move = el_num[val];
                            //news
                            Transition t = new Transition(el_num[val]);
                            t.Location = new Point(x, y);
                            arr_trans.Add(t);
                            indexpair.Add(el_num[val], t.ID);
                            //
                            istomove = true;
                            break;
                        }
                    case 3:
                        {
                            el_num[val] -= 2;
                            move = el_num[val];
                            //
                            Transition t = new Transition(el_num[val]);
                            t.Location = new Point(x, y);
                            arr_trans.Add(t);
                            indexpair.Add(el_num[val], t.ID);
                            //
                            istomove = true;
                            break;
                        }
                    case 4:
                        {
                            if (move < 0 || move > 10)
                            {
                                temp = e.Location;
                                el_con_points.Add(new Dictionary<Point, Point>());
                               drlin = true;
                            }
                            break;
                        }
                }
            }
            if (drawing_Field[x, y] ==0)
            {
                bool sel = false;
                foreach (Dictionary<Point, Point>   dline  in el_con_points)
                {
                    foreach (KeyValuePair<Point, Point> kvpline in dline)
                    {
                        Point p1 = lines.mo(kvpline.Key);
                        Point p2 = lines.mo(kvpline.Value);
                        if (x >= (Math.Min(p1.X, p2.X) - 1)  && x <= (Math.Max(p1.X, p2.X) + 1))
                        {
                            if( y >= (Math.Min(p1.Y, p2.Y) - 1) && y <= (Math.Max(p1.Y, p2.Y) + 1))
                            {
                                selected_point = kvpline.Key; sel = true;
                            }
                        }
                    }
                }
                if (!sel) selected_point = new Point(1, 1);
            }
            
        }

        void Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            int xm = e.X / sc;
            int ym = e.Y / sc;
            if (down && xm > 0 && ym > 0 && !buttons[4].Focused)
            {
                if (move != 0 && move != 1)
                {   //news
                    if (move > 10)
                        arr_pos[indexpair[move]].Location = new Point(xm, ym);
                    else
                        arr_trans[indexpair[move]].Location = new Point(xm, ym);

                 
                    clearline();
                    updateTable();
                    reconnect_nodes(move);
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
        //ИСПРАВИТЬ ЭТОТ МЕТОД
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

        private void reconnect_nodes(int which)
        {//on the base of matrixes recreate connections between nodes
            if (which > 10)
            {
                foreach (int line in arr_pos[indexpair[which]].Lin)
                {
                    Object from = arr_pos[indexpair[which]], to = null;
                    foreach (Transition t in arr_trans)
                    {
                        if (t.Lin.Contains(line))
                        {
                            to = t;
                        }
                    }
                    if (!((Position)from).DictOfIn.ContainsKey((Transition)to))
                    {
                        Object t = from;
                        from = to;
                        to = t;
                        lines.connect_two_points(((Transition)from).Location, ((Position)to).Location);

                    }
                    else
                    {
                        lines.connect_two_points(((Position)from).Location, ((Transition)to).Location);
                    }
                    el_con_points[line] = lines.Points;
                }

            }
            else
            {
                foreach (int i in arr_trans[indexpair[which]].Lin)
                {
                    Object from = arr_trans[indexpair[which]], to = null;
                    foreach (Position p in arr_pos)
                    {
                        if (p.Lin.Contains(i))
                        {
                            to = p;
                        }
                    }

                    if (!((Transition)from).DictOfIn.ContainsKey((Position)to))
                    {
                        Object t = from;
                        from = to;
                        to = t;
                        lines.connect_two_points(((Position)from).Location, ((Transition)to).Location);

                    }
                    else
                    {
                        lines.connect_two_points(((Transition)from).Location, ((Position)to).Location);

                    }
                    el_con_points[i] = lines.Points;
                }
            }
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
                //news
                if (move > 10)
                    arr_pos[indexpair[move]].Location = new Point(x, y);
                else
                    arr_trans[indexpair[move]].Location = new Point(x, y);

                updateTable();
                DrawToBuffer(grafx.Graphics);
                if (!buttons[0].Focused && !redraw)
                   this.splitContainer1.Panel2.Invalidate(new Rectangle(sc * (x - 1), sc * (y - 2), sc * 7, sc * 7));
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
                  
                    //news
                    if (move > 10 && drawing_Field[x, y] < 0)
                       drawline(arr_pos[indexpair[move]].Location, arr_trans[indexpair[drawing_Field[x, y]]].Location, line_counter);
                    else if (move < 0 && drawing_Field[x, y] > 10)
                        drawline(arr_trans[indexpair[move]].Location, arr_pos[indexpair[drawing_Field[x, y]]].Location, line_counter);

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
          //  e.Graphics.DrawString("" + field_Size, Font, Brushes.Red, 100, 100);
       
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
//news
            foreach (Position p in arr_pos)
            {
                 for (int i = p.Location.X; i < p.Location.X + 3; i++)
                        for (int j = p.Location.Y; j < p.Location.Y + 3; j++)
                        {
                            // if(drawing_Field[i, j] == 0)
                            drawing_Field[i, j] = p.Fieldnumber;
                        }
            }

            foreach (Transition t in arr_trans)
            {
                 for (int i = t.Location.Y; i < t.Location.Y + 5; i++)
                    {
                        drawing_Field[t.Location.X, i] = t.Fieldnumber;
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
            clearline();
            el_con_points.Add(new Dictionary<Point, Point>());
            line_counter++;
            if (drawing_Field[from.X, from.Y] > 10 && drawing_Field[to.X, to.Y] < 0)
            {
                ((Position)arr_pos[indexpair[drawing_Field[from.X, from.Y]]]).addIn(arr_trans[indexpair[drawing_Field[to.X, to.Y]]]);
                arr_pos[indexpair[drawing_Field[from.X, from.Y]]].Lin.Add(line_counter);
                arr_trans[indexpair[drawing_Field[to.X, to.Y]]].Lin.Add(line_counter);
             }

            if (drawing_Field[from.X, from.Y] < 0 && drawing_Field[to.X, to.Y] > 10)
            {
                ((Transition)arr_trans[indexpair[drawing_Field[from.X, from.Y]]]).addIn(arr_pos[indexpair[drawing_Field[to.X, to.Y]]]);
                arr_trans[indexpair[drawing_Field[from.X, from.Y]]].Lin.Add(line_counter);
                arr_pos[indexpair[drawing_Field[to.X, to.Y]]].Lin.Add(line_counter);
            }
            reconnect_nodes(drawing_Field[from.X, from.Y]);   
            //reconnect_nodes(0);   
        }
        
        private void clean_field()
        {
            
            drawing_Field = new int[field_Size, field_Size];
            el_con_points = new List<Dictionary<Point, Point>>();
            el_con_points.Add(new Dictionary<Point, Point>());
            lines = new Lines(sc);

            indexpair = new Dictionary<int, int>();
            arr_pos = new List<Position>();
            arr_trans = new List<Transition>();

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
            savedialog.Filter = "petri nets files (*.pni)|*.pni|All flles(*.*)|*.*";
            savedialog.FilterIndex = 1;
            savedialog.RestoreDirectory = true;
            savedialog.FileName = "PetriNetsProject";

            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(savedialog.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryFormatter bf = new BinaryFormatter();
                List<Object> serializedinfo = new List<Object>();
                serializedinfo.Add(arr_pos);
                serializedinfo.Add(arr_trans);
                serializedinfo.Add(indexpair);
                serializedinfo.Add(drawing_Field);
                serializedinfo.Add(el_con_points);
                serializedinfo.Add(el_num);
                serializedinfo.Add(line_counter);
                if (arr_trans.Count != 0) { serializedinfo.Add(Transition.Id_cntr); } //вот это не работает а надо

                bf.Serialize(fs, serializedinfo);

                fs.Close();
            }
        }

        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog savedialog = new OpenFileDialog();
            //savedialog.InitialDirectory = ".";
            savedialog.Filter = "petri nets files (*.pni)|*.pni|All flles(*.*)|*.*";
            savedialog.FilterIndex = 1;
            savedialog.RestoreDirectory = true;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(savedialog.FileName, FileMode.Open, FileAccess.Read);
                BinaryFormatter bf = new BinaryFormatter();
                List<Object> serializedinfo = new List<Object>();
                serializedinfo = (List<Object>)bf.Deserialize(fs);
                fs.Close();
                clean_field();
                arr_pos = (List<Position>)serializedinfo[0];
                arr_trans = (List<Transition>)serializedinfo[1];
                indexpair = (Dictionary<int, int>)serializedinfo[2];
                drawing_Field = (int[,])serializedinfo[3];
                el_con_points = (List<Dictionary<Point, Point>>)serializedinfo[4];
                el_num = (int[])serializedinfo[5];
                line_counter = (int)serializedinfo[6];
                if (arr_trans.Count != 0) { Transition.Id_cntr = (int)serializedinfo[7]; }//и это тоже не работает
                updateTable();
                lines.Field_matrix = drawing_Field;
                foreach (Position p in arr_pos)
                {
                    reconnect_nodes(p.Fieldnumber);
                }
                DrawToBuffer(grafx.Graphics);
                this.splitContainer1.Panel2.Invalidate();
                //this.Text += arr_pos[0].Tokens;

                //вооот здесь нужно перерисовать все
            }               
        }        

        private void clean_Click(object sender, EventArgs e)
        {
            if (arr_trans.Count != 0) { Transition.Id_cntr = 0; }
            clean_field();
            DrawToBuffer(grafx.Graphics);
            this.splitContainer1.Panel2.Invalidate();
        }

        private void DrawToBuffer(Graphics g)
        {

            grafx.Graphics.FillRectangle(Brushes.White, 0, 0, this.splitContainer1.Panel2.Width, this.splitContainer1.Panel2.Height);
          //  Test part
            //for (int i = 0; i < field_Size; i++)
            //    for (int j = 0; j < field_Size; j++)
            //    {
            //        if (drawing_Field[i, j] != 0)
            //            g.DrawString("" + drawing_Field[i, j], Font, Brushes.Blue, sc * i, sc * j);
            //    }
            // g.DrawString("" + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width, Font, Brushes.Blue, sc * 20, sc * 20);   
            bool txtbox = false;
                                   
            foreach (Position p in arr_pos)
                    {
                        
                        g.DrawEllipse((p.Fieldnumber == selected) ? new Pen(sel_br, 2) : new Pen(Brushes.Black, 2), sc * p.Location.X, sc * p.Location.Y, sc * 3, sc * 3);
                        switch (p.Tokens)
                        {
                            case 0: break;
                            case 1:
                                {
                                    g.FillEllipse(fishkabrush, sc * p.Location.X + sc, sc * p.Location.Y + sc, sc, sc);
                                    g.DrawEllipse(new Pen(Brushes.Black, 1), sc * p.Location.X + sc, sc * p.Location.Y + sc, sc, sc);
                                    break;
                                }
                            case 2:
                                {
                                    g.FillEllipse(fishkabrush, sc * p.Location.X + sc/2, sc * p.Location.Y + sc, sc, sc);
                                    g.DrawEllipse(new Pen(Brushes.Black, 1), sc * p.Location.X + sc / 2, sc * p.Location.Y + sc, sc, sc);
                                    g.FillEllipse(fishkabrush, sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y + sc, sc, sc);
                                    g.DrawEllipse(new Pen(Brushes.Black, 1), sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y + sc, sc, sc);
                                    //g.DrawArc(new Pen(Brushes.Salmon, 3), sc * p.Location.X + sc/2, sc * p.Location.Y + sc/ 2 + 2, sc * 2, sc *2, 20f, 140f);
                                    //g.DrawLine(new Pen(Brushes.Black), sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y, sc * p.Location.X + sc / 2, sc * p.Location.Y - sc);
                                    //g.DrawLine(new Pen(Brushes.Black), sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y, sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y - sc);
                                    //g.DrawLine(new Pen(Brushes.Black), sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y, sc * p.Location.X + 5 * sc / 2, sc * p.Location.Y - sc);
                                    break;
                                }
                            case 3:
                                {
                                    g.FillEllipse(fishkabrush, sc * p.Location.X + sc / 2, sc * p.Location.Y + sc /2 + 1, sc, sc);
                                    g.DrawEllipse(new Pen(Brushes.Black, 1), sc * p.Location.X + sc / 2, sc * p.Location.Y + sc /2 + 1, sc, sc);
                                    g.FillEllipse(fishkabrush, sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y + sc /2 + 1, sc, sc);
                                    g.DrawEllipse(new Pen(Brushes.Black, 1), sc * p.Location.X + 3 * sc / 2, sc * p.Location.Y + sc /2 + 1, sc, sc);
                                    g.FillEllipse(fishkabrush, sc * p.Location.X + sc, sc * p.Location.Y + 3 * sc / 2 + 1, sc, sc);
                                    g.DrawEllipse(new Pen(Brushes.Black, 1), sc * p.Location.X + sc, sc * p.Location.Y + 3*sc/2 + 1, sc, sc);
                                    break;
                                }
                            default: g.DrawString("" + p.Tokens, Font, (p.Fieldnumber == selected) ? Brushes.HotPink : Brushes.Black, sc * (p.Location.X + 1), sc * (p.Location.Y + 1));
                                break;
                        }
                        g.DrawString(p.Name, Font, (p.Fieldnumber == selected) ? Brushes.HotPink : Brushes.Black, sc * (p.Location.X + 1), (int)(sc * (p.Location.Y - 1.5)));
                        //высвечивает имя кружка при нажатии
                        if (p.Fieldnumber == selected)
                        {
                            txtbox = true;
                            this.nodname.Text = p.Name;
                            this.nodname.Visible = true;
                            this.nodname.Focus();
                        }  
                //
            
            }
           
                    foreach (Transition t in arr_trans)
                    {
                        if (t.Fieldnumber % (-2) == -1)
                        {
                            g.FillRectangle((t.Fieldnumber == selected) ? sel_br : Brushes.Black, sc * t.Location.X, sc * t.Location.Y, sc, sc * 5);
                        }
                        else
                        {
                            g.DrawRectangle((t.Fieldnumber == selected) ? new Pen(sel_br, 2) : new Pen(Brushes.Black, 2), sc * t.Location.X, sc * t.Location.Y, sc, sc * 5);
                        }
                        g.DrawString(t.Name, Font, (t.Fieldnumber == selected) ? Brushes.HotPink : Brushes.Black, sc * (t.Location.X), (int)(sc * (t.Location.Y - 1.5)));
                        //высвечивает имя кирпича при нажатии
                        if (t.Fieldnumber == selected)
                        {
                            this.nodname.Text = t.Name;
                            txtbox = true;
                            this.nodname.Visible = true;
                            this.nodname.Focus();
                        }
                        //скрыть текстбокс если нод не выбран
                        if (!txtbox) { this.nodname.Visible = false; }
                        //
                       
                    }
                    foreach (Dictionary<Point, Point> lin in el_con_points)
                    {
                        foreach (KeyValuePair<Point, Point> points in lin)
                        {
                           // Pen p = new Pen(Brushes.Green, 2);
                            bool flag = (points.Key == selected_point); // (Math.Abs(points.Value.X - selected_point.X) < 20) && (Math.Abs(points.Value.Y - selected_point.Y) < 20);   
                            if (points.Value.X != 0)
                                g.DrawLine(flag ? new Pen(sel_br, 2) : new Pen(Brushes.Green, 2), points.Key, points.Value);
                           
                            //Console.Write("From: {0:d} : {1:d} To: {2:d} : {3:d} \n", points.Key.X, points.Key.Y, points.Value.X, points.Value.Y);
                        }
                    }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void t_Click(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (selected != 0)
            {
                if (selected > 10)
                {
                    Position p = arr_pos[indexpair[selected]];
                    arr_pos.Remove(p);
                    int i = 0;
                    foreach (Position pos in arr_pos)
                    {
                        pos.ID = i;
                        pos.Tokens = arr_pos[i++].Tokens;
                    }
                    indexpair.Remove(selected);
                    foreach (Position pos in arr_pos)
                    {
                        indexpair[pos.Fieldnumber] = pos.ID;
                    }
                    Position.Id_cntr--;
                    //el_num[1]--;
                   // el_pos.
                }

                if (selected < 0)
                {
                    Transition t = arr_trans[indexpair[selected]];
                    arr_trans.Remove(t);
                    int i = 0;
                    foreach (Transition trans in arr_trans)
                    {
                        trans.ID = i;
                    }
                    indexpair.Remove(selected);
                    foreach (Transition trans in arr_trans)
                    {
                        indexpair[trans.Fieldnumber] = trans.ID;
                    }
                    Position.Id_cntr--;
                    //el_num[1]--;
                    // el_pos.
                }
            }

            DrawToBuffer(grafx.Graphics);
            this.splitContainer1.Panel2.Invalidate();
           
        }

        private void graph_Click(object sender, EventArgs e)
        {
            //new Graphs(arr_pos, arr_trans).ShowDialog(this);
        }

        private void tree_Click_1(object sender, EventArgs e)
        {
            new Tree(arr_pos, arr_trans).ShowDialog(this);
        }

        private void changename(Graphics g, string newname)
        {
            
        }

        private void rename_Click(object sender, EventArgs e)
        {
            if (selected != 0)
            {
                if (selected > 10)
                {
                    Position p = arr_pos[indexpair[selected]];
                    p.Name = this.nodname.Text;
                    grafx.Graphics.DrawString(p.Name, Font, (p.Fieldnumber == selected) ? Brushes.HotPink : Brushes.Black, sc * (p.Location.X + 1), (int)(sc * (p.Location.Y - 1.5)));
                }

                if (selected < 0)
                {
                   Transition t = arr_trans[indexpair[selected]];
                   t.Name = this.nodname.Text;
                   grafx.Graphics.DrawString(t.Name, Font, (t.Fieldnumber == selected) ? Brushes.HotPink : Brushes.Black, sc * (t.Location.X), (int)(sc * (t.Location.Y - 1.5)));
                }
            }
            DrawToBuffer(grafx.Graphics);
            this.splitContainer1.Panel2.Invalidate();
        }

        }
}

