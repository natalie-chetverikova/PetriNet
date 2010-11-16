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
        private int col;
        private int row;
        private int[] initmark;
        private int[,] di;
        private int[,] dq;
        private int[,] dr;
        List<int[]> dict_mark;

        public Tree(List<Position> arr_pos, List<Transition> arr_trans)
    {
        this.arr_pos = arr_pos;
        this.arr_trans = arr_trans;
        col = arr_pos.Count;
        row = arr_trans.Count;
        grid = new DataGridView();
        dict_mark = new  List<int[]>();
        //начальная маркировка и матрицы
        init_comp();
        dict_mark.Add(initmark);
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
            //ярус
            int tier = 1;
            //ветка
            int branch = 1;
            //текущая маркировка
            int[] currmark = initmark;
            int[] newmark = new int[arr_pos.Count];
            int mark_ctr = 0;
            string[] gridrow = new string[gridcol];
            

            //while(!end)
            //{
                
                //начало нового яруса, ветки
               
                for (int j = 0; j < arr_trans.Count; j++)
                {
                    choice = false;                    
                    for (int i = 0; i < arr_pos.Count; i++)
                    {
                        this.Text += " " + 3;
                        if (arr_pos[i].Tokens * di[j, i] != 0 && arr_pos[i].Tokens > di[j, i]) 
                        {
                            choice = true; 
                        }
                    }
                    newmark.Initialize();

                    //если переход произойдет
                    if (choice)
                    {
                        this.Text += "choice= " + choice;
                        //новая маркировка
                        for (int m = 0; m < arr_pos.Count; m++)
                        {
                            this.Text += " " + 4;
                            for (int n = 0; n < arr_trans.Count; n++)
                            {
                                this.Text += " " + 5;
                                newmark[m] -= dr[n,m] * dq[j,n];
                            }
                            newmark[m] += currmark[m];
                        }
                        //добавляем новую маркировку
                        if (!dict_mark.Contains(newmark))
                        {
                            this.Text += " " + 6;
                            dict_mark.Add(newmark);
                                                     
                            gridrow[0] = "" + tier;
                            gridrow[1] = "" + branch;
                            foreach (int i in currmark)
                            {
                                gridrow[2] += ""+i+", ";
                            }
                            gridrow[3] = arr_trans[j].Name;
                            foreach (int i in newmark)
                            {
                                gridrow[4] += "" + i + ", ";
                            }
                            newmark.Initialize();
                            gridrow[5] = "type";
                            grid.Rows.Add(gridrow);
                            branch++;
                        }

                    }

                    choice = false;

                }
                tier++;
                mark_ctr++;
                end = true;
                if (mark_ctr < dict_mark.Count)
                {
                    this.Text += " " + 7;
                    currmark = dict_mark[mark_ctr];
                    end = false;
                }
            //};

        }

        //общие параметры внешнего вида для табличек
        private void SetGridView()
        {
            //headers
            grid.ColumnCount = gridcol;

            grid.Columns[0].HeaderText = "Номер яруса";
            grid.Columns[1].HeaderText = "Номер ветви";
            grid.Columns[2].HeaderText = "Исходная маркировка";
            grid.Columns[3].HeaderText = "Переход";
            grid.Columns[4].HeaderText = "Новая маркировка";
            grid.Columns[5].HeaderText = "Класификация";

            grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            grid.RowHeadersVisible = false;
            grid.AllowUserToAddRows = false;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);

            grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;

            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            grid.GridColor = Color.Black;

            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.Dock = DockStyle.Fill;
            grid.AutoSize = true;
            grid.MaximumSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
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
