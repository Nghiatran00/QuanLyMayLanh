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
    public partial class HoaDonBan : Form
    {
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_NhanVien = new SqlDataAdapter();
        public HoaDonBan()
        {
            InitializeComponent();
        }
        public void LoadDataGridView_NHANVIEN()
        {
            string strSQL = "SELECT * FROM BIENLAI";
            ada_NhanVien = conn.getDataAdapter(strSQL, "BIENLAI");
            dataGridView1.DataSource = conn.DataSet.Tables["BIENLAI"];
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = conn.DataSet.Tables["BIENLAI"].Columns["MAHD"];
            conn.DataSet.Tables["BIENLAI"].PrimaryKey = primaryKey;
            dataGridView1.ReadOnly = true;
        }
        public void dataBindings(DataTable pTable)
        {
            txt_maHD.DataBindings.Add("Text", pTable, "MAHD", true, DataSourceUpdateMode.Never);
            dateTimePicker1.DataBindings.Add("Text", pTable, "NGAYBAN", true, DataSourceUpdateMode.Never);
            cbb_maNV.DataBindings.Add("Text", pTable, "MANV", true, DataSourceUpdateMode.Never);
            txt_SL.DataBindings.Add("Text", pTable, "SOLUONG", true, DataSourceUpdateMode.Never);
            cbb_maKH.DataBindings.Add("Text", pTable, "MAKH", true, DataSourceUpdateMode.Never);
            cbb_maMH.DataBindings.Add("Text", pTable, "MAHANG", true, DataSourceUpdateMode.Never);
            txt_DG.DataBindings.Add("Text", pTable, "DONGIA", true, DataSourceUpdateMode.Never);
            txt_GG.DataBindings.Add("Text", pTable, "GIAMGIA", true, DataSourceUpdateMode.Never);
            txt_TT.DataBindings.Add("Text", pTable, "THANHTIEN", true, DataSourceUpdateMode.Never);
        }
        private void HoaDonBan_Load(object sender, EventArgs e)
        {
            LoadDataGridView_NHANVIEN();
            dataBindings(conn.DataSet.Tables["BIENLAI"]);
            //loadCombobox1();
            //loadCombobox2();
            //loadCombobox3();
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
            cbb_maNV.Enabled = cbb_maKH.Enabled = cbb_maMH.Enabled = false;
            txt_maHD.Enabled = dateTimePicker1.Enabled = txt_SL.Enabled = txt_DG.Enabled = txt_GG.Enabled = txt_TT.Enabled = false;
            dataGridView1.Enabled = true;
        }
        //public void loadCombobox1()
        //{
        //    string tb = "select MANV from NHANVIEN";
        //    ada_NhanVien = conn.getDataAdapter(tb, "NHANVIEN");
        //    DataTable dt = new DataTable();
        //    ada_NhanVien.Fill(dt);
        //    cbb_maNV.DisplayMember = "MANV";
        //    cbb_maNV.DataSource = dt;
        //}
        //public void loadCombobox2()
        //{
        //    string tb = "select MAKH from KHACH";
        //    ada_NhanVien = conn.getDataAdapter(tb, "KHACH");
        //    DataTable dt = new DataTable();
        //    ada_NhanVien.Fill(dt);
        //    cbb_maKH.DisplayMember = "MAKH";
        //    cbb_maKH.DataSource = dt;
        //}
        //public void loadCombobox3()
        //{
        //    string tb = "select MAHANG from HANG";
        //    ada_NhanVien = conn.getDataAdapter(tb, "HANG");
        //    DataTable dt = new DataTable();
        //    ada_NhanVien.Fill(dt);
        //    cbb_maMH.DisplayMember = "MAHANG";
        //    cbb_maMH.DataSource = dt;
        //}
        private void btn_lammoi_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = groupBox2.Enabled = true;
            txt_maHD.Enabled = dateTimePicker1.Enabled = txt_SL.Enabled = txt_DG.Enabled = txt_GG.Enabled = txt_TT.Enabled = true;
            cbb_maNV.Enabled = true;
            cbb_maKH.Enabled = true;
            cbb_maMH.Enabled = true;
            btn_them.Enabled = true;
            txt_maHD.Clear();
            txt_SL.Clear();
            txt_GG.Clear();
            txt_DG.Clear();
            txt_TT.Clear();
            cbb_maNV.Text = null;
            cbb_maKH.Text = null;
            cbb_maMH.Text = null;
            txt_maHD.Focus();
        }
        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn hủy đơn hàng này không?", "Thông báo", MessageBoxButtons.YesNo,
          MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.No)
                return;
            //Tiến hành kiểm tra và lưu dữ liệu
            string mahd = txt_maHD.Text.Trim();
            try
            {
                //Lưu cập nhật
                DataRow deleteRow = conn.DataSet.Tables["BIENLAI"].Rows.Find(mahd);
                deleteRow.Delete();
                //Cập nhật dữ liệu xuống CSDL
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_NhanVien);
                ada_NhanVien.Update(conn.DataSet, "BIENLAI");
                MessageBox.Show("Xóa thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!");
            }
        }
        private void btn_sua_Click(object sender, EventArgs e)
        {
            btn_them.Enabled = true;
            groupBox2.Enabled = true;
            groupBox1.Enabled = true;
            txt_maHD.Enabled = false;
            txt_SL.Focus();
            cbb_maNV.Enabled = cbb_maKH.Enabled = cbb_maMH.Enabled = false;
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            string mahd = txt_maHD.Text.Trim();
            string ngayban = dateTimePicker1.Text.Trim();
            string manv = cbb_maNV.Text.Trim();
            string makh = cbb_maKH.Text.Trim();
            string mahang = cbb_maMH.Text.Trim();
            string soluong = txt_SL.Text.Trim();
            string dongia = txt_DG.Text.Trim();
            string giamgia = txt_GG.Text.Trim();
            string thanhtien = txt_TT.Text.Trim();
            if (mahd == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã hóa đơn. Vui lòng nhập !!!");
                txt_maHD.Focus();
                return;
            }
            else if (soluong == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập số lượng. Vui lòng nhập !!!");
                txt_SL.Focus();
                return;
            }
            else if (manv == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã nhân viên. Vui lòng nhập !!!");
                cbb_maNV.Focus();
                return;
            }
            else if (makh == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã khách hàng. Vui lòng nhập !!!");
                cbb_maKH.Focus();
                return;
            }
            else if (mahang == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã hàng hóa. Vui lòng nhập !!!");
                cbb_maMH.Focus();
                return;
            }          
            try
            {
                if (txt_maHD.Enabled == true)
                {
                    DataRow existRow = conn.DataSet.Tables["BIENLAI"].Rows.Find(mahd);
                    if (existRow != null)
                    {
                        MessageBox.Show("Mã này đã tồn tại!");
                        txt_maHD.Clear();
                        txt_maHD.Focus();
                        return;
                    }
                    DataRow newRow = conn.DataSet.Tables["BIENLAI"].NewRow();
                    newRow["MAHD"] = mahd;
                    newRow["NGAYBAN"] = ngayban;
                    newRow["MANV"] = manv;            
                    newRow["MAKH"] = makh;                   
                    newRow["MAHANG"] = mahang;               
                    newRow["SOLUONG"] = soluong;
                    newRow["DONGIA"] = dongia;
                    newRow["GIAMGIA"] = giamgia;
                    newRow["THANHTIEN"] = thanhtien;
                    conn.DataSet.Tables["BIENLAI"].Rows.Add(newRow);
                }
                else
                {
                    DataRow updateRow = conn.DataSet.Tables["BIENLAI"].Rows.Find(mahd);
                    updateRow["NGAYBAN"] = ngayban;
                    updateRow["SOLUONG"] = soluong;
                    updateRow["DONGIA"] = dongia;
                    updateRow["GIAMGIA"] = giamgia;
                    updateRow["THANHTIEN"] = thanhtien;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_NhanVien);
                ada_NhanVien.Update(conn.DataSet, "BIENLAI");
                MessageBox.Show(" thành công!");
            }
            catch
            {
                MessageBox.Show("không thành công!");
            }
            groupBox1.Enabled = groupBox2.Enabled = false;
        }
        private void btn_quaylai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain main = new FormMain();
            main.ShowDialog();
        }
        private void btn_Tinh_Click(object sender, EventArgs e)
        {
            double sl = double.Parse(txt_SL.Text);
            double giamgia = double.Parse(txt_GG.Text);
            double dongia = double.Parse(txt_DG.Text);
            double thanhtien = sl * dongia - (sl * dongia * (giamgia / 100));
            txt_TT.Text = thanhtien.ToString();
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn đóng chương trình không?", "Thông báo", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
                Close();
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btn_xoa.Enabled = true;
            btn_sua.Enabled = true;
        }
    }
}
