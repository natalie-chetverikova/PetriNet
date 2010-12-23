using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using PetriNets;
using System.Collections.Generic;

public class TableHandler : Form
{
    private int col;
    private int row;
    private int choice;
    private List<Transition> arr_trans;
    private List<Position> arr_pos;
    private DataGridView grid;

    /*
     * choice = 1  - матрица входов
     * choice = 2  - матрица выходов
     * choice = 3  - маркировка
     * arr_pos - список вершин
     * arr_trans - список переходов
     */
    public TableHandler(List<Position> arr_pos, List<Transition> arr_trans, int choice)
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

    //выбирает какую матрицу генерить
    private void TableHandler_Load(System.Object sender, System.EventArgs e)
    {
        SetGridView();
        switch (choice) 
        {
                //матрица Ди
            case 1:
                {                    
                    this.Text = "Матрица входов ";
                    //this.Text += " " + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height + " " + System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                            
                    if (col != 0)
                    {
                    for (int j = 0; j < row; j++)
                    {                        
                        string[] row0 = new string[col];

                        for (int i = 0; i < col; i++)
                        {
                            if (((Position)arr_pos[i]).DictOfIn.ContainsKey(arr_trans[j]))
                            {
                                row0[i] = "" + ((Position)arr_pos[i]).DictOfIn[arr_trans[j]];                                
                            }
                            else
                            {
                                row0[i] = "0";
                            }                            
                        }
                        
                        grid.Rows.Add(row0);
                        grid.Rows[j].HeaderCell.Value = ((Transition)arr_trans[j]).Name;
                    }
                    }
                    break;
                }

            case 2:
                {
                    this.Text = "Матрица выходов";

                    if (col != 0)
                    {
                        for (int j = 0; j < row; j++)
                        {
                            string[] row0 = new string[col];

                            for (int i = 0; i < col; i++)
                            {
                                if (((Transition)arr_trans[j]).DictOfIn.ContainsKey(arr_pos[i]))
                                {
                                    row0[i] = "" + ((Transition)arr_trans[j]).DictOfIn[arr_pos[i]];
                                }
                                else
                                {
                                    row0[i] = "0";
                                }
                            }
                            grid.Rows.Add(row0);
                            grid.Rows[j].HeaderCell.Value = ((Transition)arr_trans[j]).Name;
                        }
                    }
                    break;
                }
            case 3:
                {
                    this.Text = "Маркировка";

                    grid.RowHeadersVisible = false;
                    string[] row0 = new string[col];
                    if (col != 0)
                    {
                        for (int i = 0; i < col; i++)
                        {
                            row0[i] = "" + ((Position)arr_pos[i]).Tokens;
                        }

                        grid.Rows.Add(row0);
                    }                    
                    
                    break; 
                }
        }
    }
    //общие параметры внешнего вида для табличек
    private void SetGridView()
    {
        grid.ColumnCount = col;
        
        for (int i = 0; i < col; i++)
        {
            grid.Columns[i].HeaderText = ((Position)arr_pos[i]).Name;
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
        // TableHandler
        // 
        this.AutoScroll = true;
        this.AutoSize = true;
        this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.ClientSize = new System.Drawing.Size(292, 266);
        this.Name = "TableHandler";
        this.ResumeLayout(false);

    }   
}