using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PetriNets
{
   
    class Tree : Form
    {        
        private List<Transition> arr_trans;
        private List<Position> arr_pos;
        private DataGridView grid;
        private int gridcol = 6;
        //наальная маркировка
        private int[] initmark;
        private int[,] di;
        private int[,] dq;
        private int[,] dr;
        //маркировка, ярус
        private Dictionary<int[], int> dict_mark;
        //ярус
        private int tier = 1;
        int[] currmark;
        int[] newmark;
        private List<int[]> mmt;
        private List<int[]> mrks;
        
        
        public Tree(List<Position> arr_pos, List<Transition> arr_trans)
    {
        this.arr_pos = arr_pos;
        this.arr_trans = arr_trans;
        grid = new DataGridView();
        dict_mark = new Dictionary<int[], int>();
        //начальная маркировка и матрицы
        init_comp();           
        dict_mark.Add(initmark,0);

            this.Load += new EventHandler(TableHandler_Load);

    }
        private void TableHandler_Load(System.Object sender, System.EventArgs e)
        {
            //общие параметры внешнего вида для табличек
            SetGridView();
            //произойдет ли переход
            bool choice = true;
            //конец обхода
            bool end = false;            
            //ветка
            int branch = 1;
            //текущая маркировка
            currmark = new int[arr_pos.Count];
            //
            for (int i = 0; i < arr_pos.Count; i++)
            {
                currmark[i] = initmark[i];
            }
            //новая маркировка
            newmark = new int[arr_pos.Count];
            //счетчик маркировок, 1 - потому что корневую записали в конструкторе
            int mark_ctr = 1;
            //указатель на след маркировку
            int mark_ptr = 1;

            /*
             * 0 - tier
             * 1 - branch
             * 2 - currmark
             * 3 - trans
             * 4 - newmark
             * 5 - type
             * grid.Rows[0].HeaderCell.Value = mark_name
             */
            string[] gridrow = new string[gridcol];
            
            //корневая вершина
            gridrow[0] = "" + 0;
            gridrow[1] = "" + 1;
            gridrow[2] = "";
            gridrow[3] = "";
            foreach (int i in initmark)
            {
                gridrow[4] += "" + i + ", ";
            }
            gridrow[5] = "корневая";
            grid.Rows.Add(gridrow);
            grid.Rows[0].HeaderCell.Value = "M0";

            //начало обработки дерева

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
                            newmark[q] = currmark[q] - di[j,q] + dq[j,q];
                        }
                       
                        //this.Text += " /curm : " + currmark[0] + " " + currmark[1] + " --> ";
                        //this.Text += "newm : " + newmark[0] + " " + newmark[1] + " / ";

                        //добавляем новую маркировку
                        bool qw = !containsKey(dict_mark, newmark);

                        //--------
                        gridrow[2] = "";
                        gridrow[4] = "";

                        gridrow[0] = "" + tier;
                        gridrow[1] = "" + branch;
                        foreach (int i in currmark)
                        {
                            gridrow[2] += "" + i + ", ";
                        }
                        gridrow[3] = arr_trans[j].Name;
                        foreach (int i in newmark)
                        {
                            gridrow[4] += "" + i + ", ";
                        }
                        grid.Rows[mark_ctr].HeaderCell.Value = "M" + getmarkNumber(dict_mark, newmark);
                        mark_ctr++;
                        branch++;
                        //-------
                        if (!containsKey(dict_mark, newmark))
                        {
                            //dict_mark.Add(mark_ctr,newmark);
                            addmark(dict_mark, tier+1, newmark);
                            gridrow[5] = "промежуточная";
                        }
                        else
                        {
                           gridrow[5] = "конечная";  
                        }
                        grid.Rows.Add(gridrow);
                        //м-м-т для графа
                        mkListsforGraph(currmark, newmark, j);

                    }
                }

                try{
                    for (int q = 0; q < arr_pos.Count; q++)
                    {
                        currmark[q] = (dict_mark.ElementAt(mark_ptr).Key)[q];
                    }
                    tier = dict_mark.ElementAt(mark_ptr).Value;
                    mark_ptr++;                 
                    branch = 1;
                }
                catch(Exception)
                {
                    end = true;
                }
                //if (tier == 10) break;
            };
            //список маркировок для графа
            mrks = dict_mark.Keys.ToList();

        }

        //список: маркировка-маркировка-переход для графа
        //переходы по ид-шнику
        private void mkListsforGraph(int[] currmark, int[] newmark, int j)
        {
            int[] buf = new int[3];
            buf[0] = getmarkNumber(dict_mark, currmark);
            buf[1] = getmarkNumber(dict_mark, newmark);
            buf[2] = j;
            mmt.Add(buf);
        }

        //добавление маркировки в словарь
        private void addmark(Dictionary<int[], int> dict_mark, int tier, int[] newmark)
        {
            int[] buf = new int[newmark.Count()];
            for (int i = 0; i < buf.Count(); i++)
            {
                buf[i] = newmark[i];
            }
            dict_mark.Add(buf,tier);
        }

        //содержит ли словарь маркировку
        private bool containsKey(Dictionary<int[], int> dict_mark, int[] newmark)
        {
            bool equals = true;
            foreach (int[] mark in dict_mark.Keys)
            {
                equals = true;
                for (int i = 0; i < mark.Length; i++)
                {
                    equals &= (mark[i]==newmark[i]);                    
                }
                if (equals) { return true; }                
            }
            return false;
        }

        //вытащить номер маркировки из словаря
        private int getmarkNumber(Dictionary<int[], int> dict_mark, int[] newmark)
        {
             bool equals = true;
             for (int j = 0; j < dict_mark.Count; j++)
             {
                 equals = true;
                 for (int i = 0; i < dict_mark.ElementAt(j).Key.Length; i++)
                 {
                     equals &= (dict_mark.ElementAt(j).Key[i] == newmark[i]);
                 }
                 if (equals) { return j; }
             }
                return 100;
        }

        //общие параметры внешнего вида для табличек
        private void SetGridView()
        {
            //headers
            grid.ColumnCount = gridcol;
            this.Text = "Дерево достижимости";

            grid.Columns[0].HeaderText = "Номер яруса";
            grid.Columns[1].HeaderText = "Номер ветви";
            grid.Columns[2].HeaderText = "Исходная маркировка";
            grid.Columns[3].HeaderText = "Переход";
            grid.Columns[4].HeaderText = "Новая маркировка";
            grid.Columns[5].HeaderText = "Классификация";

            grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            
            grid.AllowUserToAddRows = false;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);

            grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grid.GridColor = Color.Black;

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.Dock = DockStyle.Fill;
            grid.AutoSize = true;
            Size si = new System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width / 2);
            //grid.MaximumSize = si;
            grid.Size = si;// = si;
            grid.ScrollBars = System.Windows.Forms.ScrollBars.Both;

            this.Controls.Add(grid);
            this.AutoSize = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Tree
            // 
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Name = "Tree";
            this.ResumeLayout(false);

        }

        //начальная маркировка и матрицы
        private void init_comp()
        {
            //initial markup
            initmark = new int[arr_pos.Count];
            for (int i = 0; i < arr_pos.Count; i++)
            {
                initmark[i] = ((Position)arr_pos[i]).Tokens;
            }

            //di
            di = new int[arr_trans.Count, arr_pos.Count];
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

            //dq
            dq = new int[arr_trans.Count, arr_pos.Count];
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

            //dr
            dr = new int[arr_trans.Count, arr_pos.Count];
            for (int j = 0; j < arr_trans.Count; j++)
            {
                for (int i = 0; i < arr_pos.Count; i++)
                {  
                        dr[j, i] = dq[j, i] - di[j, i];
                }
            }
                        }
    }
}