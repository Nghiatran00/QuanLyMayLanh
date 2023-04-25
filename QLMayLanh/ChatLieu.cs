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
    public partial class ChatLieu : Form
    {
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_ChatLieu = new SqlDataAdapter();
        public ChatLieu()
        {
            InitializeComponent();
        }
        //Load dữ liệu lên
        public void LoadDataGridView_CHATLIEU()
        {
            string strSQL = "SELECT * FROM CHATLIEU";
            ada_ChatLieu = conn.getDataAdapter(strSQL, "CHATLIEU");
            dataGridView1.DataSource = conn.DataSet.Tables["CHATLIEU"];
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = conn.DataSet.Tables["CHATLIEU"].Columns["MACHATLIEU"];
            conn.DataSet.Tables["CHATLIEU"].PrimaryKey = primaryKey;
            dataGridView1.ReadOnly = true;
        }
        public void dataBindings(DataTable pTable)
        {
            txt_maCL.DataBindings.Clear();
            txt_tenCL.DataBindings.Clear();
            txt_maCL.DataBindings.Add("Text", pTable, "MACHATLIEU", true, DataSourceUpdateMode.Never);
            txt_tenCL.DataBindings.Add("Text", pTable, "TENCHATLIEU", true, DataSourceUpdateMode.Never);
        }
        private void ChatLieu_Load(object sender, EventArgs e)
        {
            LoadDataGridView_CHATLIEU();
            dataBindings(conn.DataSet.Tables["CHATLIEU"]);
            txt_maCL.Enabled = false;
            txt_tenCL.Enabled = false;
            btn_Xoa.Enabled = false;
            btn_Sua.Enabled = false;
        }
        // Làm Mới
        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_maCL.Enabled = true;
            txt_tenCL.Enabled = true;
            txt_maCL.Clear();
            txt_tenCL.Clear();
            txt_maCL.Focus();
        }
        // Xóa
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn xóa chất liệu này không?", "Thông báo", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.No)
                return;
            //Tiến hành kiểm tra và lưu dữ liệu
            string machatlieu = txt_maCL.Text.Trim();
            try
            {
                //Kiểm tra khóa ngoại
                string strSQL = "SELECT count(*) FROM HANG WHERE MACHATLIEU='" + machatlieu + "'";
                if (conn.checkForExistence(strSQL))
                {
                    MessageBox.Show("Chất liệu này đã tồn tại khóa ngoại trên bảng HANG!");
                    return;
                }
                //Lưu cập nhật
                DataRow deleteRow = conn.DataSet.Tables["CHATLIEU"].Rows.Find(machatlieu);
                deleteRow.Delete();
                //Cập nhật dữ liệu xuống CSDL
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_ChatLieu);
                ada_ChatLieu.Update(conn.DataSet, "CHATLIEU");
                btn_Luu.Enabled = false;
                txt_maCL.Enabled = txt_tenCL.Enabled = false;
                MessageBox.Show("Xóa thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!");
            }
        }
        // Sửa
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            btn_Luu.Enabled = true;
            txt_tenCL.Enabled = true;
            txt_tenCL.Focus();
        }
        //Thêm
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string machatlieu = txt_maCL.Text.Trim();
            string tenchatlieu = txt_tenCL.Text.Trim();
            if (machatlieu == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã chất liệu. Vui lòng nhập !!!");
                txt_maCL.Focus();
                return;
            }
            else if (tenchatlieu == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu. Vui lòng nhập !!!");
                txt_tenCL.Focus();
                return;
            }
            try
            {
                if (txt_maCL.Enabled == true)
                {
                    DataRow existRow = conn.DataSet.Tables["CHATLIEU"].Rows.Find(machatlieu);
                    if (existRow != null)
                    {
                        MessageBox.Show("Mã này đã tồn tại!");
                        txt_maCL.Clear();
                        txt_maCL.Focus();
                        return;
                    }
                    DataRow newRow = conn.DataSet.Tables["CHATLIEU"].NewRow();
                    newRow["MACHATLIEU"] = machatlieu;
                    newRow["TENCHATLIEU"] = tenchatlieu;
                    conn.DataSet.Tables["CHATLIEU"].Rows.Add(newRow);
                }
                else
                {
                    DataRow updateRow = conn.DataSet.Tables["CHATLIEU"].Rows.Find(machatlieu);
                    updateRow["TENCHATLIEU"] = tenchatlieu;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_ChatLieu);
                ada_ChatLieu.Update(conn.DataSet, "CHATLIEU");

                btn_Luu.Enabled = false;
                txt_maCL.Enabled = txt_tenCL.Enabled = false;

                MessageBox.Show("thành công!");
            }
            catch
            {
                MessageBox.Show("không thành công!");
            }
        }
        // Quay Lại
        private void btn_Quaylai_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain formMain = new FormMain();
            formMain.ShowDialog();
        }
        // Đóng Form
        private void ChatLieu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Ban co muon thoat", "Thoat", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
        // Thoát
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
                this.Close();
        }
        // Chọn dữ liệu để xóa hoặc sửa
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btn_Xoa.Enabled = btn_Sua.Enabled = true;
        }

    }
}
