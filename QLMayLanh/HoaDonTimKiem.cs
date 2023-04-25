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
    public partial class HoaDonTimKiem : Form
    {
        public HoaDonTimKiem()
        {
            InitializeComponent();
        }
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_BienLai = new SqlDataAdapter();
        public void LoadDataGridView_BIENLAI()
        {
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM BIENLAI");
            dataGridView1.ReadOnly = true;
        }
        public void KeyWord()
        {
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM BIENLAI where MAHD = '" + txt_Tim.Text + "'");
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM BIENLAI where MAKH = '" + txt_Tim.Text + "'");
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM BIENLAI where MANV = '" + txt_Tim.Text + "'");
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM BIENLAI where MAHANG = '" + txt_Tim.Text + "'");
        }
        private void HoaDonTimKiem_Load(object sender, EventArgs e)
        {
            LoadDataGridView_BIENLAI();
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            KeyWord();
        }
        private void btn_hienthi_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            LoadDataGridView_BIENLAI();
            txt_Tim.Clear();
        }
        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain main = new FormMain();
            main.ShowDialog();
        }
    }
}
