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
    public partial class KhachHangTimKiem : Form
    {
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_KhachHang = new SqlDataAdapter();
        public KhachHangTimKiem()
        {
            InitializeComponent();
        }
        public void LoadDataGridView_KHACHHANG()
        {
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM KHACH");
            dataGridView1.ReadOnly = true;
        }
        public void KeyWord()
        {
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM KHACH where MAKH = '" + txt_tim.Text + "'");
            dataGridView1.DataSource = conn.getDataTable("SELECT * FROM KHACH where TENKH like N'%" + txt_tim.Text + "%'");
        }
        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain main = new FormMain();
            main.ShowDialog();
        }
        private void KhachHangTimKiem_Load(object sender, EventArgs e)
        {
            LoadDataGridView_KHACHHANG();
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            KeyWord();
        }
        private void btn_hienthi_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView1.DataSource).Rows.Clear();
            LoadDataGridView_KHACHHANG();
        }
    }
}
