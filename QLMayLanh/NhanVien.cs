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
    public partial class NhanVien : Form
    {
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_NhanVien = new SqlDataAdapter();
        public NhanVien()
        {
            InitializeComponent();
        }
        // Load dữ liệu lên 
        public void LoadDataGridView_NHANVIEN()
        {
            string strSQL = "SELECT * FROM NHANVIEN";
            ada_NhanVien = conn.getDataAdapter(strSQL, "NHANVIEN");
            dataGridView1.DataSource = conn.DataSet.Tables["NHANVIEN"];
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = conn.DataSet.Tables["NHANVIEN"].Columns["MANV"];
            conn.DataSet.Tables["NHANVIEN"].PrimaryKey = primaryKey;
            dataGridView1.ReadOnly = true;
        }
        public void dataBindings(DataTable pTable)
        {
            txt_maNV.DataBindings.Clear();
            txt_tenNV.DataBindings.Clear();
            txt_maNV.DataBindings.Add("Text", pTable, "MANV", true, DataSourceUpdateMode.Never);
            txt_tenNV.DataBindings.Add("Text", pTable, "TENNV", true, DataSourceUpdateMode.Never);
            comboBox1.DataBindings.Add("Text", pTable, "GIOITINH", true, DataSourceUpdateMode.Never);
            txt_chucvu.DataBindings.Add("Text", pTable, "CHUCVU", true, DataSourceUpdateMode.Never);
            txt_phongban.DataBindings.Add("Text", pTable, "PHONGBAN", true, DataSourceUpdateMode.Never);
            txt_hocvan.DataBindings.Add("Text", pTable, "HOCVAN", true, DataSourceUpdateMode.Never);
            txt_cmnd.DataBindings.Add("Text", pTable, "CMND", true, DataSourceUpdateMode.Never);
            txt_tongiao.DataBindings.Add("Text", pTable, "TONGIAO", true, DataSourceUpdateMode.Never);
            txt_diachi.DataBindings.Add("Text", pTable, "DIACHI", true, DataSourceUpdateMode.Never);
            txt_bhyt.DataBindings.Add("Text", pTable, "BHYT", true, DataSourceUpdateMode.Never);
            txt_email.DataBindings.Add("Text", pTable, "EMAIL", true, DataSourceUpdateMode.Never);
            txt_quoctich.DataBindings.Add("Text", pTable, "QUOCTICH", true, DataSourceUpdateMode.Never);
            txt_dantoc.DataBindings.Add("Text", pTable, "DANTOC", true, DataSourceUpdateMode.Never);
            txt_trangthai.DataBindings.Add("Text", pTable, "TRANGTHAI", true, DataSourceUpdateMode.Never);
            txt_DT.DataBindings.Add("Text", pTable, "DIENTHOAI", true, DataSourceUpdateMode.Never);
            dateTimePicker1.DataBindings.Add("Text", pTable, "NGAYSINH", true, DataSourceUpdateMode.Never);
        }
        private void NhanVien_Load(object sender, EventArgs e)
        {
            LoadDataGridView_NHANVIEN();
            dataBindings(conn.DataSet.Tables["NHANVIEN"]);
            txt_maNV.Enabled = false;
            txt_tenNV.Enabled = false;
            txt_diachi.Enabled = false;
            dateTimePicker1.Enabled = false;
            txt_DT.Enabled = false;
            comboBox1.Enabled = false;
            btn_Xoa.Enabled = false;
            btn_Sua.Enabled = false;
        }
        // Làm Mới
        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_maNV.Enabled = true;
            txt_tenNV.Enabled = true;
            txt_diachi.Enabled = true;
            dateTimePicker1.Enabled = true;
            txt_DT.Enabled = true;
            comboBox1.Enabled = true;
            btn_Luu.Enabled = true;
            txt_maNV.Clear();
            txt_tenNV.Clear();
            txt_diachi.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txt_DT.Clear();
            comboBox1.Text = null;
            txt_maNV.Focus();
        }
        // Xóa
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn xóa nhân viên này không?", "Thông báo", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.No)
                return;
            //Tiến hành kiểm tra và lưu dữ liệu
            string manv = txt_maNV.Text.Trim();
            try
            {
                string strSQL = "SELECT count(*) FROM BIENLAI WHERE MANV='" + manv + "'";
                if (conn.checkForExistence(strSQL))
                {
                    MessageBox.Show("Nhân viên này đã tồn tại khóa ngoại trên bảng BIENLAI!");
                    return;
                }
                //Lưu cập nhật
                DataRow deleteRow = conn.DataSet.Tables["NHANVIEN"].Rows.Find(manv);
                deleteRow.Delete();

                //Cập nhật dữ liệu xuống CSDL
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_NhanVien);
                ada_NhanVien.Update(conn.DataSet, "NHANVIEN");

                btn_Luu.Enabled = false;
                txt_maNV.Enabled = txt_tenNV.Enabled = false;

                MessageBox.Show("Xóa thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!");
            }
            txt_maNV.Clear();
            txt_tenNV.Clear();
            txt_diachi.Clear();
            txt_diachi.Enabled = txt_DT.Enabled = false;
            dateTimePicker1.Text = null;
            txt_DT.Clear();
            comboBox1.Text = null;
        }
        // Sửa
        private void btn_Sua_Click_1(object sender, EventArgs e)
        {
            btn_Luu.Enabled = true;
            txt_tenNV.Enabled = true;
            txt_diachi.Enabled = true;
            dateTimePicker1.Enabled = true;
            txt_DT.Enabled = true;
            txt_tenNV.Focus();
            comboBox1.Enabled = true;
        }
        // Lưu
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string manv = txt_maNV.Text.Trim();
            string tennv = txt_tenNV.Text.Trim();
            string gt = comboBox1.Text.Trim();
            string dc = txt_diachi.Text.Trim();
            string dt = txt_DT.Text.Trim();
            string ngsinh = dateTimePicker1.Text.Trim();
            if (manv == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên. Vui lòng nhập !!!");
                txt_maNV.Focus();
                return;
            }
            else if (tennv == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên. Vui lòng nhập !!!");
                txt_tenNV.Focus();
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
            else if (ngsinh == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập ngày sinh. Vui lòng nhập !!!");
                dateTimePicker1.Focus();
                return;
            }
            try
            {
                if (txt_maNV.Enabled == true)
                {
                    DataRow existRow = conn.DataSet.Tables["NHANVIEN"].Rows.Find(manv);
                    if (existRow != null)
                    {
                        MessageBox.Show("Mã này đã tồn tại!");
                        txt_maNV.Clear();
                        txt_maNV.Focus();
                        return;
                    }
                    DataRow newRow = conn.DataSet.Tables["NHANVIEN"].NewRow();
                    newRow["MANV"] = manv;
                    newRow["TENNV"] = tennv;
                    newRow["GIOITINH"] = comboBox1.Text;
                    newRow["DIACHI"] = dc;
                    newRow["DT"] = dt;
                    newRow["NGAYSINH"] = ngsinh;
                    conn.DataSet.Tables["NHANVIEN"].Rows.Add(newRow);
                }
                else
                {
                    DataRow updateRow = conn.DataSet.Tables["NHANVIEN"].Rows.Find(manv);
                    updateRow["TENNV"] = tennv;
                    updateRow["GIOITINH"] = gt;
                    updateRow["DIACHI"] = dc;
                    updateRow["DT"] = dt;
                    updateRow["NGAYSINH"] = ngsinh;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_NhanVien);
                ada_NhanVien.Update(conn.DataSet, "NHANVIEN");
                btn_Luu.Enabled = false;
                txt_maNV.Enabled = txt_tenNV.Enabled = false;
                MessageBox.Show("thành công!");
            }
            catch
            {
                MessageBox.Show("không thành công!");
            }
            txt_maNV.Enabled = false;
            txt_tenNV.Enabled = false;
            txt_diachi.Enabled = false;
            dateTimePicker1.Enabled = false;
            txt_DT.Enabled = false;
            comboBox1.Enabled = false;
        }
        // Quay lại
        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain main = new FormMain();
            main.ShowDialog();
        }
        // Thoát
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
                this.Close();
        }
        // Đóng Form
        private void NhanVien_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btn_Them_Click_1(object sender, EventArgs e)
        {
            txt_maNV.Enabled = true;
            txt_tenNV.Enabled = true;
            txt_diachi.Enabled = true;
            dateTimePicker1.Enabled = true;
            txt_DT.Enabled = true;
            comboBox1.Enabled = true;
            btn_Luu.Enabled = true;
            txt_maNV.Clear();
            txt_tenNV.Clear();
            txt_diachi.Clear();
            dateTimePicker1.Value = DateTime.Now;
            txt_DT.Clear();
            comboBox1.Text = null;
            txt_maNV.Focus();
        }

        private void btn_Luu_Click_1(object sender, EventArgs e)
        {
            string manv = txt_maNV.Text.Trim();
            string tennv = txt_tenNV.Text.Trim();
            string gt = comboBox1.Text.Trim();
            string dc = txt_diachi.Text.Trim();
            string dt = txt_DT.Text.Trim();
            string ngsinh = dateTimePicker1.Text.Trim();
            if (manv == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên. Vui lòng nhập !!!");
                txt_maNV.Focus();
                return;
            }
            else if (tennv == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên nhân viên. Vui lòng nhập !!!");
                txt_tenNV.Focus();
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
            else if (ngsinh == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập ngày sinh. Vui lòng nhập !!!");
                dateTimePicker1.Focus();
                return;
            }
            try
            {
                if (txt_maNV.Enabled == true)
                {
                    DataRow existRow = conn.DataSet.Tables["NHANVIEN"].Rows.Find(manv);
                    if (existRow != null)
                    {
                        MessageBox.Show("Mã này đã tồn tại!");
                        txt_maNV.Clear();
                        txt_maNV.Focus();
                        return;
                    }
                    DataRow newRow = conn.DataSet.Tables["NHANVIEN"].NewRow();
                    newRow["MANV"] = manv;
                    newRow["TENNV"] = tennv;
                    newRow["GIOITINH"] = comboBox1.Text;
                    newRow["DIACHI"] = dc;
                    newRow["DT"] = dt;
                    newRow["NGAYSINH"] = ngsinh;
                    conn.DataSet.Tables["NHANVIEN"].Rows.Add(newRow);
                }
                else
                {
                    DataRow updateRow = conn.DataSet.Tables["NHANVIEN"].Rows.Find(manv);
                    updateRow["TENNV"] = tennv;
                    updateRow["GIOITINH"] = gt;
                    updateRow["DIACHI"] = dc;
                    updateRow["DT"] = dt;
                    updateRow["NGAYSINH"] = ngsinh;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_NhanVien);
                ada_NhanVien.Update(conn.DataSet, "NHANVIEN");
                btn_Luu.Enabled = false;
                txt_maNV.Enabled = txt_tenNV.Enabled = false;
                MessageBox.Show("thành công!");
            }
            catch
            {
                MessageBox.Show("không thành công!");
            }
            txt_maNV.Enabled = false;
            txt_tenNV.Enabled = false;
            txt_diachi.Enabled = false;
            dateTimePicker1.Enabled = false;
            txt_DT.Enabled = false;
            comboBox1.Enabled = false;
        }

        private void btn_Xoa_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            btn_Luu.Enabled = true;
            txt_tenNV.Enabled = true;
            txt_diachi.Enabled = true;
            dateTimePicker1.Enabled = true;
            txt_DT.Enabled = true;
            txt_tenNV.Focus();
            comboBox1.Enabled = true;
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            btn_Xoa.Enabled = btn_Sua.Enabled = true;
        }
    }
}
