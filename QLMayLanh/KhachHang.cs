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
    public partial class KhachHang : Form
    {
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_KhachHang = new SqlDataAdapter();
        public KhachHang()
        {
            InitializeComponent();
        }
        public void LoadDataGridView_KHACHHANG()
        {
            string strSQL = "SELECT * FROM KHACH";
            ada_KhachHang = conn.getDataAdapter(strSQL, "KHACH");
            dataGridView1.DataSource = conn.DataSet.Tables["KHACH"];
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = conn.DataSet.Tables["KHACH"].Columns["MAKH"];
            conn.DataSet.Tables["KHACH"].PrimaryKey = primaryKey;
            dataGridView1.ReadOnly = true;
        }
        public void dataBindings(DataTable pTable)
        {
            txt_maKH.DataBindings.Clear();
            txt_tenKH.DataBindings.Clear();
            txt_maKH.DataBindings.Add("Text", pTable, "MAKH", true, DataSourceUpdateMode.Never);
            txt_tenKH.DataBindings.Add("Text", pTable, "TENKH", true, DataSourceUpdateMode.Never);
            txt_diachi.DataBindings.Add("Text", pTable, "DIACHI", true, DataSourceUpdateMode.Never);
            txt_DT.DataBindings.Add("Text", pTable, "DT", true, DataSourceUpdateMode.Never);
        }
        private void KhachHang_Load(object sender, EventArgs e)
        {
            LoadDataGridView_KHACHHANG();
            dataBindings(conn.DataSet.Tables["KHACH"]);
            txt_maKH.Enabled = false;
            txt_tenKH.Enabled = false;
            txt_diachi.Enabled = false;
            txt_DT.Enabled = false;
            btn_Xoa.Enabled = false;
            btn_Sua.Enabled = false;
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_maKH.Enabled = true;
            txt_tenKH.Enabled = true;
            txt_diachi.Enabled = true;
            txt_DT.Enabled = true;
            txt_maKH.Clear();
            txt_tenKH.Clear();
            txt_diachi.Clear();
            btn_Luu.Enabled = true;
            txt_DT.Clear();
            txt_maKH.Focus();
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn xóa khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo,
             MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.No)
                return;
            //Tiến hành kiểm tra và lưu dữ liệu
            string manv = txt_maKH.Text.Trim();
            try
            {
                //Kiểm tra khóa ngoại
                string strSQL = "SELECT count(*) FROM BIENLAI WHERE MAKH='" + manv + "'";
                if (conn.checkForExistence(strSQL))
                {
                    MessageBox.Show("Mã này đã tồn tại khóa ngoại trên bảng BIENLAI!");
                    return;
                }
                //Lưu cập nhật
                DataRow deleteRow = conn.DataSet.Tables["KHACH"].Rows.Find(manv);
                deleteRow.Delete();
                //Cập nhật dữ liệu xuống CSDL
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_KhachHang);
                ada_KhachHang.Update(conn.DataSet, "KHACH");
                btn_Luu.Enabled = false;
                txt_maKH.Enabled = txt_tenKH.Enabled = false;
                MessageBox.Show("Xóa thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!");
            }
            txt_maKH.Clear();
            txt_tenKH.Clear();
            txt_diachi.Clear();
            txt_DT.Clear();
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            txt_tenKH.Enabled = true;
            txt_diachi.Enabled = true;
            txt_DT.Enabled = true;
            btn_Luu.Enabled = true;
        }
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string makh = txt_maKH.Text.Trim();
            string tenkh = txt_tenKH.Text.Trim();
            string dc = txt_diachi.Text.Trim();
            string dt = txt_DT.Text.Trim();
            if (makh == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên. Vui lòng nhập !!!");
                txt_maKH.Focus();
                return;
            }
            else if (tenkh == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên. Vui lòng nhập !!!");
                txt_tenKH.Focus();
                return;
            }
            else if (dc == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ. Vui lòng nhập !!!");
                txt_diachi.Focus();
                return;
            }
            else if (dt == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập số điện thoại. Vui lòng nhập !!!");
                txt_DT.Focus();
                return;
            }
            try
            {
                if (txt_maKH.Enabled == true)
                {
                    DataRow existRow = conn.DataSet.Tables["KHACH"].Rows.Find(makh);
                    if (existRow != null)
                    {
                        MessageBox.Show("Mã này đã tồn tại!");
                        txt_maKH.Clear();
                        txt_maKH.Focus();
                        return;
                    }
                    DataRow newRow = conn.DataSet.Tables["KHACH"].NewRow();
                    newRow["MAKH"] = makh;
                    newRow["TENKH"] = tenkh;
                    newRow["DIACHI"] = dc;
                    newRow["DT"] = dt;
                    conn.DataSet.Tables["KHACH"].Rows.Add(newRow);
                }
                else
                {
                    DataRow updateRow = conn.DataSet.Tables["KHACH"].Rows.Find(makh);
                    updateRow["TENKH"] = tenkh;
                    updateRow["DIACHI"] = dc;
                    updateRow["DT"] = dt;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_KhachHang);
                ada_KhachHang.Update(conn.DataSet, "KHACH");

                btn_Luu.Enabled = false;
                txt_maKH.Enabled = txt_tenKH.Enabled = false;

                MessageBox.Show("thành công!");
            }
            catch
            {
                MessageBox.Show("không thành công!");
            }
            txt_maKH.Enabled = false;
            txt_tenKH.Enabled = false;
            txt_diachi.Enabled = false;
            txt_DT.Enabled = false;         
        }
        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain main = new FormMain();
            main.ShowDialog();
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {      
               this.Close();
        }
        private void KhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Ban co muon thoat", "Thoat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btn_Xoa.Enabled = btn_Sua.Enabled = true;
        }

    }
}
