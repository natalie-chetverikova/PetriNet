﻿namespace PetriNets
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.di = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.dq = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.markup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.open = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.delete = new System.Windows.Forms.ToolStripButton();
            this.graph = new System.Windows.Forms.ToolStripButton();
            this.tree = new System.Windows.Forms.ToolStripButton();
            this.P = new System.Windows.Forms.Button();
            this.t = new System.Windows.Forms.Button();
            this.tt = new System.Windows.Forms.Button();
            this.connector = new System.Windows.Forms.Button();
            this.cursor = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.modelate = new System.Windows.Forms.ToolStripButton();
            this.drawing = new System.Windows.Forms.ToolStripButton();
            this.step = new System.Windows.Forms.ToolStripButton();
            this.go = new System.Windows.Forms.ToolStripButton();
            this.faraway = new System.Windows.Forms.ToolStripButton();
            this.current_step = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.max_steps = new System.Windows.Forms.ToolStripTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.reset = new System.Windows.Forms.ToolStripButton();
            this.stop = new System.Windows.Forms.ToolStripButton();
            this.faster = new System.Windows.Forms.ToolStripButton();
            this.slower = new System.Windows.Forms.ToolStripButton();
            this.test = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modelate,
            this.toolStripSeparator12,
            this.toolStripSeparator11,
            this.di,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.dq,
            this.toolStripSeparator5,
            this.toolStripSeparator3,
            this.markup,
            this.toolStripSeparator4,
            this.toolStripSeparator6,
            this.save,
            this.toolStripSeparator7,
            this.toolStripSeparator8,
            this.open,
            this.toolStripSeparator9,
            this.toolStripSeparator10,
            this.delete,
            this.graph,
            this.tree});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // di
            // 
            this.di.Image = ((System.Drawing.Image)(resources.GetObject("di.Image")));
            this.di.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.di.Name = "di";
            this.di.Size = new System.Drawing.Size(38, 22);
            this.di.Text = "DI";
            this.di.ToolTipText = "Матрица входов";
            this.di.Click += new System.EventHandler(this.di_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // dq
            // 
            this.dq.Image = ((System.Drawing.Image)(resources.GetObject("dq.Image")));
            this.dq.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.dq.Name = "dq";
            this.dq.Size = new System.Drawing.Size(44, 22);
            this.dq.Text = "DQ";
            this.dq.ToolTipText = "Матрица выходов";
            this.dq.Click += new System.EventHandler(this.dq_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // markup
            // 
            this.markup.Image = ((System.Drawing.Image)(resources.GetObject("markup.Image")));
            this.markup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markup.Name = "markup";
            this.markup.Size = new System.Drawing.Size(44, 22);
            this.markup.Text = "M0";
            this.markup.ToolTipText = "Маркировка";
            this.markup.Click += new System.EventHandler(this.markup_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // save
            // 
            this.save.Image = ((System.Drawing.Image)(resources.GetObject("save.Image")));
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(23, 22);
            this.save.ToolTipText = "Сохранить проект";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // open
            // 
            this.open.Image = ((System.Drawing.Image)(resources.GetObject("open.Image")));
            this.open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(23, 22);
            this.open.ToolTipText = "Открыть проект";
            this.open.Click += new System.EventHandler(this.open_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // delete
            // 
            this.delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(23, 22);
            this.delete.Text = "DeletButton";
            this.delete.ToolTipText = "Удалить объект";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // graph
            // 
            this.graph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.graph.Image = ((System.Drawing.Image)(resources.GetObject("graph.Image")));
            this.graph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.graph.Name = "graph";
            this.graph.Size = new System.Drawing.Size(23, 22);
            this.graph.Text = "Дракула";
            this.graph.Click += new System.EventHandler(this.graph_Click);
            // 
            // tree
            // 
            this.tree.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tree.Image = ((System.Drawing.Image)(resources.GetObject("tree.Image")));
            this.tree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(23, 22);
            this.tree.Text = "Бревно";
            this.tree.Click += new System.EventHandler(this.tree_Click_1);
            // 
            // P
            // 
            this.P.Cursor = System.Windows.Forms.Cursors.Hand;
            this.P.Image = ((System.Drawing.Image)(resources.GetObject("P.Image")));
            this.P.Location = new System.Drawing.Point(1, 56);
            this.P.Name = "P";
            this.P.Size = new System.Drawing.Size(55, 55);
            this.P.TabIndex = 1;
            this.P.Tag = "1";
            this.P.UseVisualStyleBackColor = true;
            // 
            // t
            // 
            this.t.Cursor = System.Windows.Forms.Cursors.Hand;
            this.t.Image = ((System.Drawing.Image)(resources.GetObject("t.Image")));
            this.t.Location = new System.Drawing.Point(1, 109);
            this.t.Name = "t";
            this.t.Size = new System.Drawing.Size(55, 55);
            this.t.TabIndex = 2;
            this.t.Tag = "2";
            this.t.Text = "t";
            this.t.UseVisualStyleBackColor = true;
            this.t.Click += new System.EventHandler(this.t_Click);
            // 
            // tt
            // 
            this.tt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tt.Image = ((System.Drawing.Image)(resources.GetObject("tt.Image")));
            this.tt.Location = new System.Drawing.Point(1, 161);
            this.tt.Name = "tt";
            this.tt.Size = new System.Drawing.Size(55, 55);
            this.tt.TabIndex = 3;
            this.tt.Tag = "3";
            this.tt.UseVisualStyleBackColor = true;
            // 
            // connector
            // 
            this.connector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connector.Image = ((System.Drawing.Image)(resources.GetObject("connector.Image")));
            this.connector.Location = new System.Drawing.Point(1, 214);
            this.connector.Name = "connector";
            this.connector.Size = new System.Drawing.Size(55, 55);
            this.connector.TabIndex = 4;
            this.connector.Tag = "4";
            this.connector.UseVisualStyleBackColor = true;
            // 
            // cursor
            // 
            this.cursor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cursor.Image = ((System.Drawing.Image)(resources.GetObject("cursor.Image")));
            this.cursor.Location = new System.Drawing.Point(1, 3);
            this.cursor.Name = "cursor";
            this.cursor.Size = new System.Drawing.Size(55, 55);
            this.cursor.TabIndex = 0;
            this.cursor.Tag = "0";
            this.cursor.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(1, 278);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 55);
            this.button2.TabIndex = 5;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.clean_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.cursor);
            this.splitContainer1.Panel1.Controls.Add(this.connector);
            this.splitContainer1.Panel1.Controls.Add(this.tt);
            this.splitContainer1.Panel1.Controls.Add(this.t);
            this.splitContainer1.Panel1.Controls.Add(this.P);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.splitContainer1.Panel1MinSize = 55;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(784, 312);
            this.splitContainer1.SplitterDistance = 57;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawing,
            this.step,
            this.go,
            this.stop,
            this.faraway,
            this.reset,
            this.current_step,
            this.toolStripLabel2,
            this.max_steps,
            this.faster,
            this.slower,
            this.test});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(784, 25);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            this.toolStrip2.Visible = false;
            // 
            // modelate
            // 
            this.modelate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.modelate.Image = ((System.Drawing.Image)(resources.GetObject("modelate.Image")));
            this.modelate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.modelate.Name = "modelate";
            this.modelate.Size = new System.Drawing.Size(23, 22);
            this.modelate.Text = "Прогонялка";
            this.modelate.Click += new System.EventHandler(this.modelate_Click);
            // 
            // drawing
            // 
            this.drawing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawing.Image = ((System.Drawing.Image)(resources.GetObject("drawing.Image")));
            this.drawing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawing.Name = "drawing";
            this.drawing.Size = new System.Drawing.Size(23, 22);
            this.drawing.Text = "Рисовалка";
            this.drawing.Click += new System.EventHandler(this.drawing_Click);
            // 
            // step
            // 
            this.step.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.step.Image = ((System.Drawing.Image)(resources.GetObject("step.Image")));
            this.step.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.step.Name = "step";
            this.step.Size = new System.Drawing.Size(23, 22);
            this.step.Text = "Шаг";
            this.step.Click += new System.EventHandler(this.step_Click);
            // 
            // go
            // 
            this.go.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.go.Image = ((System.Drawing.Image)(resources.GetObject("go.Image")));
            this.go.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(23, 22);
            this.go.Text = "Старт";
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // faraway
            // 
            this.faraway.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.faraway.Image = ((System.Drawing.Image)(resources.GetObject("faraway.Image")));
            this.faraway.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.faraway.Name = "faraway";
            this.faraway.Size = new System.Drawing.Size(23, 22);
            this.faraway.Text = "До конца";
            this.faraway.Click += new System.EventHandler(this.faraway_Click);
            // 
            // current_step
            // 
            this.current_step.AutoSize = false;
            this.current_step.Name = "current_step";
            this.current_step.Size = new System.Drawing.Size(55, 22);
            this.current_step.Text = "Шаг: ";
            this.current_step.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(19, 22);
            this.toolStripLabel2.Text = "из";
            // 
            // max_steps
            // 
            this.max_steps.Name = "max_steps";
            this.max_steps.Size = new System.Drawing.Size(40, 25);
            this.max_steps.TextChanged += new System.EventHandler(this.max_steps_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // reset
            // 
            this.reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reset.Image = ((System.Drawing.Image)(resources.GetObject("reset.Image")));
            this.reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(23, 22);
            this.reset.Text = "Сбросить";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // stop
            // 
            this.stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stop.Image = ((System.Drawing.Image)(resources.GetObject("stop.Image")));
            this.stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(23, 22);
            this.stop.Text = "Стоп";
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // faster
            // 
            this.faster.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.faster.Image = ((System.Drawing.Image)(resources.GetObject("faster.Image")));
            this.faster.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.faster.Name = "faster";
            this.faster.Size = new System.Drawing.Size(23, 22);
            this.faster.Text = "Быстрее";
            this.faster.Click += new System.EventHandler(this.faster_Click);
            // 
            // slower
            // 
            this.slower.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.slower.Image = ((System.Drawing.Image)(resources.GetObject("slower.Image")));
            this.slower.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.slower.Name = "slower";
            this.slower.Size = new System.Drawing.Size(23, 22);
            this.slower.Text = "Медленнее";
            this.slower.Click += new System.EventHandler(this.slower_Click);
            // 
            // test
            // 
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(26, 22);
            this.test.Text = "test";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 362);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton di;
        private System.Windows.Forms.ToolStripButton dq;
        private System.Windows.Forms.ToolStripButton markup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.Button P;
        private System.Windows.Forms.Button t;
        private System.Windows.Forms.Button tt;
        private System.Windows.Forms.Button connector;
        private System.Windows.Forms.Button cursor;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton delete;
        private System.Windows.Forms.ToolStripButton graph;
        private System.Windows.Forms.ToolStripButton tree;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton modelate;
        private System.Windows.Forms.ToolStripButton drawing;
        private System.Windows.Forms.ToolStripButton step;
        private System.Windows.Forms.ToolStripButton go;
        private System.Windows.Forms.ToolStripButton faraway;
        private System.Windows.Forms.ToolStripLabel current_step;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox max_steps;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton stop;
        private System.Windows.Forms.ToolStripButton reset;
        private System.Windows.Forms.ToolStripButton faster;
        private System.Windows.Forms.ToolStripButton slower;
        private System.Windows.Forms.ToolStripLabel test;

    }
}

