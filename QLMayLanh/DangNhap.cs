using System;
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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btn_DN_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=NIDOL\SQLEXPRESS;Initial Catalog=QLBANHANG;Integrated Security=True");
            try
            {
                conn.Open();
                string tk = txt_tenDN.Text;
                string mk = txt_MK.Text;
                string sql = "select * from DANGNHAP where TENDN='" + tk + "' and MK='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FormMain formMain = new FormMain();
                    formMain.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu hoặc tên đăng nhập. Không thể đăng nhập !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng nhập thất bại !!!");
            }
        }

        private void DangNhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Ban co muon thoat", "Thoat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void cb_Show_CheckedChanged(object sender, System.EventArgs e)
        {
            if (cb_Show.Checked == true)
            {
                txt_MK.UseSystemPasswordChar = false;
                var checkbox = (CheckBox)sender;
                cb_Show.Text = "Hide password";
            }
            else
            {
                txt_MK.UseSystemPasswordChar = true;
                cb_Show.Text = "Show password";
            }
        }

        private void btn_DN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_DN.PerformClick();
            }
        }
        private void btn_Thoat_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        private void DangNhap_Load(object sender, System.EventArgs e)
        {
            txt_tenDN.Focus();
        }
    }
}
