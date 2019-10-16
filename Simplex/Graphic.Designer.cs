namespace Simplex
{
    partial class Graphic
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.drawp = new System.Windows.Forms.Panel();
            this.drawpic = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.result_text = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.n = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.X2 = new System.Windows.Forms.TextBox();
            this.X1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.drawp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawpic)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(266, 77);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(166, 17);
            this.checkBox1.TabIndex = 29;
            this.checkBox1.Text = " Графическое отображение";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(438, 77);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(283, 45);
            this.trackBar1.TabIndex = 27;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(700, 390);
            this.vScrollBar1.Maximum = 1200;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(21, 258);
            this.vScrollBar1.TabIndex = 28;
            this.vScrollBar1.Value = 600;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // drawp
            // 
            this.drawp.AutoScroll = true;
            this.drawp.AutoScrollMinSize = new System.Drawing.Size(587, 237);
            this.drawp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.drawp.Controls.Add(this.pictureBox1);
            this.drawp.Controls.Add(this.drawpic);
            this.drawp.Location = new System.Drawing.Point(2, 390);
            this.drawp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.drawp.MaximumSize = new System.Drawing.Size(1110, 1100);
            this.drawp.Name = "drawp";
            this.drawp.Size = new System.Drawing.Size(695, 258);
            this.drawp.TabIndex = 26;
            // 
            // drawpic
            // 
            this.drawpic.BackColor = System.Drawing.SystemColors.Window;
            this.drawpic.Location = new System.Drawing.Point(6, 0);
            this.drawpic.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.drawpic.Name = "drawpic";
            this.drawpic.Size = new System.Drawing.Size(657, 0);
            this.drawpic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.drawpic.TabIndex = 13;
            this.drawpic.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.result_text);
            this.panel2.Location = new System.Drawing.Point(242, 126);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(455, 260);
            this.panel2.TabIndex = 25;
            // 
            // result_text
            // 
            this.result_text.AutoSize = true;
            this.result_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.result_text.Location = new System.Drawing.Point(3, 0);
            this.result_text.Name = "result_text";
            this.result_text.Size = new System.Drawing.Size(26, 16);
            this.result_text.TabIndex = 9;
            this.result_text.Text = "  x  ";
            this.result_text.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(38, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "  X1      X2     Знак     B";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(2, 126);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 260);
            this.panel1.TabIndex = 22;
            // 
            // n
            // 
            this.n.AccessibleDescription = " ";
            this.n.AccessibleName = "x1";
            this.n.Location = new System.Drawing.Point(342, 53);
            this.n.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.n.Name = "n";
            this.n.ReadOnly = true;
            this.n.Size = new System.Drawing.Size(47, 20);
            this.n.TabIndex = 21;
            this.n.Text = "3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Количество ограничений (без x1, x2 >= 0)";
            // 
            // X2
            // 
            this.X2.Location = new System.Drawing.Point(416, 19);
            this.X2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.X2.Name = "X2";
            this.X2.ReadOnly = true;
            this.X2.Size = new System.Drawing.Size(47, 20);
            this.X2.TabIndex = 19;
            this.X2.Text = "40";
            // 
            // X1
            // 
            this.X1.AccessibleDescription = " ";
            this.X1.AccessibleName = "x1";
            this.X1.Location = new System.Drawing.Point(342, 19);
            this.X1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.X1.Name = "X1";
            this.X1.ReadOnly = true;
            this.X1.Size = new System.Drawing.Size(47, 20);
            this.X1.TabIndex = 18;
            this.X1.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Коэффициэнты целевой функции F(x1,x2)";
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(2, 650);
            this.hScrollBar1.Maximum = 1200;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(685, 19);
            this.hScrollBar1.TabIndex = 30;
            this.hScrollBar1.Value = 600;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(6, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(683, 258);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // Graphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 678);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.drawp);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.n);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.X2);
            this.Controls.Add(this.X1);
            this.Controls.Add(this.label1);
            this.Name = "Graphic";
            this.Text = "Graphic";
            this.Load += new System.EventHandler(this.Graphic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.drawp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawpic)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.Panel drawp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label result_text;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox n;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox X2;
        private System.Windows.Forms.TextBox X1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.PictureBox drawpic;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}