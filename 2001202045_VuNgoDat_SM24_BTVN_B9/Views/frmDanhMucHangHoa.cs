using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2001202045_VuNgoDat_SM24_BTVN_B9.Views
{
    public partial class frmDanhMucHangHoa : Form
    {
        DataSet ds_Hang;
        Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();           
        DataColumn[] key = new DataColumn[1];
        SqlDataAdapter da;

        public frmDanhMucHangHoa()
        {
            InitializeComponent();
            //Thêm dữ liệu vào combobox
            cboMaCL.DataSource = DAL.Modules.DBHelper.Instance.GetDataSet("select * from chatlieu", "chatlieu").Tables[0];
            cboMaCL.DisplayMember = "MaChatLieu";
            cboMaCL.ValueMember = "TenChatLieu";
            
            //Lưu dictionary chatlieu
            DataTable tmpCLList = DAL.Modules.DBHelper.Instance.GetDataSet("select * from chatlieu", "chatlieu").Tables[0];
            foreach (DataRow row in tmpCLList.Rows)
            {
                keyValuePairs.Add(row.ItemArray[0].ToString(), row.ItemArray[1].ToString());
            }
            da = DAL.Modules.DBHelper.Instance.GetAdapter("select * from hang");
            

            
        }
        public void ClearInputValue()
        {
            this.Controls.OfType<TextBox>().ToList().ForEach(
                x => x.Clear()
                );
            this.Controls.OfType<RichTextBox>().ToList().ForEach(
                x => x.Clear()
                );
            this.Controls.OfType<ComboBox>().ToList().ForEach(
                x => x.Text = ""
                ); ;
            //foreach (Control ctr in panel1.Controls)
            //{
            //    ctr.Text = "";
            //}
        }
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow dataRow = ds_Hang.Tables[0].NewRow();
                dataRow.ItemArray = new object[] { txtMH.Text, txtTen.Text, cboMaCL.Text, txtSL.Text, txtDGNhap.Text, txtDGBan.Text };

                if (!ds_Hang.Tables[0].Rows.Contains(dataRow.ItemArray[0].ToString()))
                {
                    ds_Hang.Tables[0].Rows.Add(dataRow);
                    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                    da.Update(ds_Hang, "hang");
                    ClearInputValue();
                }
                else
                {
                    MessageBox.Show(string.Format($"Mã hàng [{txtMH.Text.Trim()}] đã trùng, vui lòng nhập lại!"), "Hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception)
            {
                return;
            }
            

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            dgvMain.ClearSelection();
            ClearInputValue();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            DataRow dr = ds_Hang.Tables[0].Rows.Find(txtMH.Text);
            if (dr != null)
            {
                dr.ItemArray = new object[] { txtMH.Text, txtTen.Text, cboMaCL.Text, txtSL.Text, txtDGNhap.Text, txtDGBan.Text, rtxtAnh.Text, rtxtGhiChu.Text };
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds_Hang, "hang");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataRow dr = ds_Hang.Tables[0].Rows.Find(txtMH.Text);
            if (dr != null)
            {
                dr.Delete();
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds_Hang, "hang");
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All File (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                rtxtAnh.Text = openFileDialog1.FileName;
                picBox.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void frmDanhMucHangHoa_Load(object sender, EventArgs e)
        {
            LoadGird();
            dgvMain.ClearSelection();
            ClearInputValue();
        }
        private void dgvMain_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //return;
        }

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //LoadGird();
        }
        public void DataBinding(DataTable dataTable)
        {
            //Thêm khóa chính
            key[0] = ds_Hang.Tables["hang"].Columns[0];
            ds_Hang.Tables["hang"].PrimaryKey = key;

            dgvMain.DataSource = ds_Hang.Tables[0];

            rtxtGhiChu.DataBindings.Add("Text", dataTable, dataTable.Columns["GhiChu"].ToString());
            rtxtAnh.DataBindings.Add("Text", dataTable, dataTable.Columns["Hinh"].ToString());
            txtMH.DataBindings.Add("Text", dataTable, dataTable.Columns["MaHang"].ToString());
            txtTen.DataBindings.Add("Text", dataTable, dataTable.Columns["TenHang"].ToString());
            txtSL.DataBindings.Add("Text", dataTable, dataTable.Columns["SoLuong"].ToString());
            txtDGNhap.DataBindings.Add("Text", dataTable, dataTable.Columns["DonGiaNhap"].ToString());
            txtDGBan.DataBindings.Add("Text", dataTable, dataTable.Columns["DonGiaBan"].ToString());
            cboMaCL.DataBindings.Add("Text", dataTable, dataTable.Columns["MaChatLieu"].ToString());

        }
        public void ClearDataBiding()
        {
            panel1.Controls.Cast<Control>().ToList().ForEach(
                x => x.DataBindings.Clear()
                );
        }
        public void LoadGird()
        {
            ClearDataBiding();
            ds_Hang = DAL.Modules.DBHelper.Instance.GetDataSet("select * from hang", "hang");
            DataBinding(ds_Hang.Tables[0]);
        }
    }
}
