using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            #region Init_components
            Simplex simplex = new Simplex(2, 3);
            simplex.dataGridView4.Rows[0].Cells[0].Value = "10";
            simplex.dataGridView4.Rows[0].Cells[1].Value = "40";
            simplex.dataGridView4.Rows[0].Cells[2].Value = "0";

            var targetCell = new DataGridViewComboBoxCell();
            targetCell.Items.AddRange("max", "min");
            (targetCell as DataGridViewComboBoxCell).Value = (targetCell as DataGridViewComboBoxCell).Items[1];
            simplex.dataGridView4.Rows[0].Cells[3] = targetCell;

            simplex.dataGridView3.Rows[0].Cells[0].Value = "10";
            simplex.dataGridView3.Rows[0].Cells[1].Value = "14";
            var cell0 = new DataGridViewComboBoxCell();
            cell0.Items.AddRange("≥", "=", "≤");
            (cell0 as DataGridViewComboBoxCell).Value = (cell0 as DataGridViewComboBoxCell).Items[0];
            simplex.dataGridView3.Rows[0].Cells[2] = cell0;
            simplex.dataGridView3.Rows[0].Cells[3].Value = "100";

            simplex.dataGridView3.Rows[1].Cells[0].Value = "6";
            simplex.dataGridView3.Rows[1].Cells[1].Value = "20";
            var cell1 = new DataGridViewComboBoxCell();
            cell1.Items.AddRange("≥", "=", "≤");
            (cell1 as DataGridViewComboBoxCell).Value = (cell1 as DataGridViewComboBoxCell).Items[0];
            simplex.dataGridView3.Rows[1].Cells[2] = cell1;
            simplex.dataGridView3.Rows[1].Cells[3].Value = "180";

            simplex.dataGridView3.Rows[2].Cells[0].Value = "1";
            simplex.dataGridView3.Rows[2].Cells[1].Value = "0";
            var cell2 = new DataGridViewComboBoxCell();
            cell2.Items.AddRange("≥", "=", "≤");
            (cell2 as DataGridViewComboBoxCell).Value = (cell2 as DataGridViewComboBoxCell).Items[2];
            simplex.dataGridView3.Rows[2].Cells[2] = cell2;
            simplex.dataGridView3.Rows[2].Cells[3].Value = "10";
            #endregion
            simplex.dataGridView3.ReadOnly = true;
            simplex.dataGridView4.ReadOnly = true;
            simplex.Initialize();
            simplex.Calculate();
            simplex.ShowDialog();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphic graphic = new Graphic();
            graphic.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphic gr = new Graphic();
            Excel ex = new Excel();
            gr.Export(ex, "test");
            ex.Visible = true;
            gr.Dispose();
        }
    }
}
