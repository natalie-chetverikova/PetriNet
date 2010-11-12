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
        private int col;
        private int row;
        private int choice;
        private List<Transition> arr_trans;
        private List<Position> arr_pos;
        private DataGridView grid;

        public Tree(List<Position> arr_pos, List<Transition> arr_trans, int choice)
    {
        this.arr_pos = arr_pos;
        this.arr_trans = arr_trans;
        row = arr_trans.Count;
        col = arr_pos.Count;
        this.choice = choice;
       //это надо делать если 123
                    grid = new DataGridView();
                    this.Load += new EventHandler(TableHandler_Load);
       //авот это если 4
        //кто все эти цифры спустя 3 дня не помню даже я
    }

        private void TableHandler_Load(System.Object sender, System.EventArgs e)
        {
            SetGridView();
        }

        //общие параметры внешнего вида для табличек
        private void SetGridView()
        {
            grid.ColumnCount = col;

            for (int i = 0; i < col; i++)
            {
                grid.Columns[i].HeaderText = string.Concat("p", "" + ((Position)arr_pos[i]).ID);
            }
            grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;

            grid.AllowUserToAddRows = false;
            //grid.AutoSize = true;

            this.Controls.Add(grid);
            this.AutoSize = true;

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
    }
}
