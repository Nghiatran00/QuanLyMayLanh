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
using System.IO;

namespace QLMayLanh
{
    public partial class HangHoa : Form
    {
        KetNoi conn = new KetNoi();
        SqlDataAdapter ada_HangHoa = new SqlDataAdapter();
        public HangHoa()
        {
            InitializeComponent();
        }
        public void LoadDataGridView_HANGHOA()
        {
            string strSQL = "SELECT * FROM HANG";
            ada_HangHoa = conn.getDataAdapter(strSQL, "HANG");
            dataGridView1.DataSource = conn.DataSet.Tables["HANG"];
            DataColumn[] primaryKey = new DataColumn[1];
            primaryKey[0] = conn.DataSet.Tables["HANG"].Columns["MAHANG"];
            conn.DataSet.Tables["HANG"].PrimaryKey = primaryKey;
            dataGridView1.ReadOnly = true;
        }
        public void dataBindings(DataTable pTable)
        {
            txt_maH.DataBindings.Add("Text", pTable, "MAHANG", true, DataSourceUpdateMode.Never);
            txt_tenH.DataBindings.Add("Text", pTable, "TENHANG", true, DataSourceUpdateMode.Never);
            comboBox1.DataBindings.Add("Text", pTable, "MACHATLIEU", true, DataSourceUpdateMode.Never);
            txt_SL.DataBindings.Add("Text", pTable, "SOLUONG", true, DataSourceUpdateMode.Never);
            txt_DGN.DataBindings.Add("Text", pTable, "DONGIANHAP", true, DataSourceUpdateMode.Never);
            txt_DGB.DataBindings.Add("Text", pTable, "DONGIABAN", true, DataSourceUpdateMode.Never);
            txt_GC.DataBindings.Add("Text", pTable, "GHICHU", true, DataSourceUpdateMode.Never);
        }
        private void HangHoa_Load(object sender, EventArgs e)
        {
            LoadDataGridView_HANGHOA();
            dataBindings(conn.DataSet.Tables["HANG"]);
            txt_maH.Enabled = txt_tenH.Enabled = txt_SL.Enabled = txt_DGN.Enabled = txt_DGB.Enabled = txt_GC.Enabled = false;
            comboBox1.Enabled = false;
            comboBox1.Text = null;
            btn_Xoa.Enabled = false;
            btn_Sua.Enabled = false;
            txt_maH.Clear();
            txt_tenH.Clear();
            txt_SL.Clear();
            txt_DGN.Clear();
            txt_DGB.Clear();
            txt_GC.Clear();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            txt_maH.Enabled = txt_tenH.Enabled = txt_SL.Enabled = txt_DGN.Enabled = txt_DGB.Enabled = txt_GC.Enabled = true;
            comboBox1.Enabled = true;
            txt_maH.Clear();
            txt_tenH.Clear();
            txt_SL.Clear();
            txt_DGN.Clear();
            txt_DGB.Clear();
            txt_GC.Clear();
            comboBox1.Text = null;
            txt_maH.Focus();
        }
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.No)
                return;
            //Tiến hành kiểm tra và lưu dữ liệu
            string manv = txt_maH.Text.Trim();
            try
            {
                //Kiểm tra khóa ngoại
                string strSQL = "SELECT count(*) FROM BIENLAI WHERE MAHANG='" + manv + "'";
                if (conn.checkForExistence(strSQL))
                {
                    MessageBox.Show("Mã hàng này đã tồn tại khóa ngoại trên bảng BIENLAI!");
                    return;
                }
                //Lưu cập nhật
                DataRow deleteRow = conn.DataSet.Tables["HANG"].Rows.Find(manv);
                deleteRow.Delete();

                //Cập nhật dữ liệu xuống CSDL
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_HangHoa);
                ada_HangHoa.Update(conn.DataSet, "HANG");
                btn_Luu.Enabled = false;
                txt_maH.Enabled = txt_tenH.Enabled = false;
                MessageBox.Show("Xóa thành công!");
            }
            catch
            {
                MessageBox.Show("Xóa không thành công!");
            }
            txt_maH.Clear();
            txt_tenH.Clear();
            txt_SL.Clear();
            txt_DGN.Clear();
            txt_DGB.Clear();
            txt_GC.Clear();
            comboBox1.Text = null;
        }
        private void btn_Sua_Click(object sender, EventArgs e)
        {
            btn_Luu.Enabled = true;          
            txt_tenH.Enabled = txt_SL.Enabled = comboBox1.Enabled = true;
            txt_DGN.Enabled = txt_DGB.Enabled = txt_GC.Enabled = true;
            txt_tenH.Focus();
            comboBox1.Enabled = true;
        }
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            string mahang = txt_maH.Text.Trim();
            string tenhang = txt_tenH.Text.Trim();
            string mcl = comboBox1.Text.Trim();
            string sl = txt_SL.Text.Trim();
            string dgn = txt_DGN.Text.Trim();
            string dgb = txt_DGB.Text.Trim();
            string gc = txt_GC.Text.Trim();
            if (mahang == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã hàng. Vui lòng nhập !!!");
                txt_maH.Focus();
                return;
            }
            else if (tenhang == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập tên hàng. Vui lòng nhập !!!");
                txt_tenH.Focus();
                return;
            }
            else if (mcl == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập mã chất liệu. Vui lòng nhập !!!");
                comboBox1.Focus();
                return;
            }
            else if (sl == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập số lượng. Vui lòng nhập !!!");
                txt_SL.Focus();
                return;
            }
            else if (dgn == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập giá nhập. Vui lòng nhập !!!");
                txt_DGN.Focus();
                return;
            }
            else if (dgb == string.Empty)
            {
                MessageBox.Show("Bạn chưa nhập giá bán. Vui lòng nhập !!!");
                txt_DGB.Focus();
                return;
            }
            else if (gc == string.Empty)
            {
                MessageBox.Show("Chưa ghi chú. Vui lòng bổ sung !!!");
                txt_GC.Focus();
                return;
            }
            try
            {
                if (txt_maH.Enabled == true)
                {
                    DataRow existRow = conn.DataSet.Tables["HANG"].Rows.Find(mahang);
                    if (existRow != null)
                    {
                        MessageBox.Show("Mã này đã tồn tại!");
                        txt_maH.Clear();
                        txt_maH.Focus();
                        return;
                    }
                    DataRow newRow = conn.DataSet.Tables["HANG"].NewRow();
                    newRow["MAHANG"] = mahang;
                    newRow["TENHANG"] = tenhang;
                    newRow["MACHATLIEU"] = comboBox1.Text;
                    newRow["SOLUONG"] = sl;
                    newRow["DONGIANHAP"] = dgn;
                    newRow["DONGIABAN"] = dgb;
                    newRow["GHICHU"] = gc;
                    conn.DataSet.Tables["HANG"].Rows.Add(newRow);
                }
                else
                {
                    DataRow updateRow = conn.DataSet.Tables["HANG"].Rows.Find(mahang);
                    updateRow["TENHANG"] = tenhang;
                    updateRow["MACHATLIEU"] = mcl;
                    updateRow["SOLUONG"] = sl;
                    updateRow["DONGIANHAP"] = dgn;
                    updateRow["DONGIABAN"] = dgb;
                    updateRow["GHICHU"] = gc;
                }
                SqlCommandBuilder cb = new SqlCommandBuilder(ada_HangHoa);
                ada_HangHoa.Update(conn.DataSet, "HANG");
                btn_Luu.Enabled = false;
                txt_maH.Enabled = txt_tenH.Enabled = false;
                txt_SL.Enabled = txt_DGN.Enabled = txt_DGB.Enabled = txt_GC.Enabled = false;
                MessageBox.Show("thành công!");
            }
            catch
            {
                MessageBox.Show("không thành công!");
            }          
            txt_maH.Enabled = txt_tenH.Enabled = txt_SL.Enabled = comboBox1.Enabled = false;
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
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            btn_Xoa.Enabled = true;
            btn_Sua.Enabled = true;
        }


    }
}
