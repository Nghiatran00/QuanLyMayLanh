using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace QLMayLanh
{
    public partial class HangHoaTimKiem : Form
    {
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_Hang = new SqlDataAdapter();
        public HangHoaTimKiem()
        {
            InitializeComponent();
        }
        public void LoadDataGridView_HANG()
        {
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM HANG");
            dataGridView1.ReadOnly = true;
        }
        public void KeyWord()
        {
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM HANG where MAHANG = '" + txt_Tim.Text + "'");
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM HANG where TENHANG like N'%" + txt_Tim.Text + "%'");
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM HANG where MACHATLIEU = '" + txt_Tim.Text + "'");
        }
        private void HangHoaTimKiem_Load(object sender, EventArgs e)
        {
            LoadDataGridView_HANG();
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            KeyWord();
        }
        private void btn_hienthi_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            LoadDataGridView_HANG();
        }
        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain main = new FormMain();
            main.ShowDialog();
        }
    }
}
