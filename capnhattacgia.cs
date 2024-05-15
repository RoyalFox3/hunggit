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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace WindowsFormsApp14
{
    public partial class capnhattacgia : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-P2M7CEL\\SQLEXPRESS01;Initial Catalog=\"QL Thuvien\";Integrated Security=True;Encrypt=False");
        public capnhattacgia()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void load_Tacgia()
        {
            //B1: Kết nối đến Db
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-P2M7CEL\\SQLEXPRESS01;Initial Catalog=\"QL Thuvien\";Integrated Security=True;Encrypt=False");
            if (con.State == ConnectionState.Closed)
                con.Open();
            //B2: tạo đối tượng command để thực hiện láy toàn độ dl trong bảng tacgia
            String sql = "Select * From TacGia";
            SqlCommand cmd = new SqlCommand(sql, con);
            //B3: tạo đối tượng dataAdapter để lấy kq từ command
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            //B4: tạo đối tượng dataTable để lấy dl từ dataAdapter
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            //B5: hiển thị dataTable(bảng) lên điều khiển datagridview
            dgvTacgia.DataSource = tb;
            dgvTacgia.Refresh();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Lay du lieu tu cac dieu khien dua vao bien 
            string mtg = txtMatacgia.Text.Trim();
            string ttg = txtTentacgia.Text.Trim();
            string ns = dtpNgaysinh.Text.Trim();
            string gt = cbGioitinh.SelectedItem.ToString();
            string dt = txtDienthoai.Text.Trim();
            string mail = txtEmail.Text.Trim();
            string dc = txtDiachi.Text.Trim();
            //Kiểm tra trùng mã
            if (checktrungmatg(mtg))
            {
                MessageBox.Show("Trùng mã tác giả");
                return;
            }
            //Kiểm tra nã tg rỗng
            if (mtg == "")
            {
                MessageBox.Show(" Mã tác giả không được trống");
                return;
            }
            //B1: Ket noi den database
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-P2M7CEL\\SQLEXPRESS01;Initial Catalog=\"QL Thuvien\";Integrated Security=True;Encrypt=False");
            if (con.State == ConnectionState.Closed)
                con.Open();

            //B2: Tao cau lenh sql them moi
            string sql = "Insert Tacgia Values(@matg,@tentg,@gioitinh,@ngaysinh,@dienthoai,@mail,@diachi)";

            //B3: Tao doi tuong Command de thuc hien cau lenh SQL
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@matg", SqlDbType.NVarChar, 50).Value = mtg;
            cmd.Parameters.Add("@tentg", SqlDbType.NVarChar, 50).Value = ttg;
            cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar, 50).Value = gt;
            cmd.Parameters.Add("@ngaysinh", SqlDbType.Date, 50).Value = ns;
            cmd.Parameters.Add("@dienthoai", SqlDbType.NVarChar, 50).Value = dt;
            cmd.Parameters.Add("@mail", SqlDbType.NVarChar, 50).Value = mail;
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 50).Value = dc;

            //B4: Thuc thi cau lenh
            cmd.ExecuteNonQuery();
            cmd.Dispose();

            //B5: Dong ket noi 
            con.Close();

            //hien thi hop thoai thong bao thanh cong
            MessageBox.Show("Thêm mới thành công !");
        }

        private void dtpNgaysinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbGioitinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void capnhattacgia_Load(object sender, EventArgs e)
        {
            load_Tacgia();

        }


        private void dgvTacgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            txtMatacgia.Text = dgvTacgia.Rows[i].Cells[0].Value.ToString();
            txtTentacgia.Text = dgvTacgia.Rows[i].Cells[1].Value.ToString();
            cbGioitinh.Text = dgvTacgia.Rows[i].Cells[2].Value.ToString();
            //dtpNgaysinh.Value = DateTime.Parse(dgvTacgia.Rows[i].Cells["Ngaysinh"].Value.ToString());
            txtDienthoai.Text = dgvTacgia.Rows[i].Cells[4].Value.ToString();
            txtEmail.Text = dgvTacgia.Rows[i].Cells[5].Value.ToString();
            txtDiachi.Text = dgvTacgia.Rows[i].Cells[6].Value.ToString();
        }

        private bool checktrungmatg(String matg)
        {
            //Nếu trùn mã tác giả hàm checktrungmatg sẽ trả về giá trị true ngược lại

            // Lại hàm checktrungmatg sẽ trả về giá trị false

            //b1: kết nối đến 
            if (con.State == ConnectionState.Closed)
                con.Open();
            //b2: tạo đối tượng command để ktra matg có trùng không
            String sql = "Select count(*) From tacgia where matg='" + matg + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            int kq = (int)cmd.ExecuteScalar();
            cmd.Dispose();
            if (kq > 0) return true;
            else return false;  
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Lay du lieu tu cac dieu khien dua vao bien 
            string mtg = txtMatacgia.Text.Trim();
            string ttg = txtTentacgia.Text.Trim();
            string ns = dtpNgaysinh.Text.Trim();
            string gt = cbGioitinh.SelectedItem.ToString();
            string dt = txtDienthoai.Text.Trim();
            string mail = txtEmail.Text.Trim();
            string dc = txtDiachi.Text.Trim();
            //B2; ket noi den db
            if (con.State == ConnectionState.Closed)
                con.Open();
            //B3 Tao doi tuong command de thuc hien
            String sql = "Update Tacgia Set Tentg=@tentg,Ngaysinh=@ngaysinh,Gioitinh=@gioitinh," +
                "Dienthoai=@dienthoai,Email=@email,Diachi=@diachi Where Matg=@matg";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add("@matg", SqlDbType.NVarChar, 50).Value = mtg;
            cmd.Parameters.Add("@tentg", SqlDbType.NVarChar, 50).Value = ttg;
            cmd.Parameters.Add("@ngaysinh", SqlDbType.Date).Value = ns;
            cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar, 50).Value = gt;
            cmd.Parameters.Add("@dienthoai", SqlDbType.NVarChar, 50).Value = dt;
            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 50).Value = mail;
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 50).Value = dc;
            cmd.ExecuteNonQuery();
            MessageBox.Show("Sửa thành công!");

            load_Tacgia();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mtg=txtMatacgia.Text.Trim();
            //B2 ket noi den db
            if (con.State == ConnectionState.Closed)
                con.Open();
            //B3 Tao doi tuong command de xoa theo ma tac gia
            string sql = "Delete from Tacgia Where Matg='" + mtg + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            MessageBox.Show("Xoá thành công ! ");

            load_Tacgia();
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string mtg=txtMatacgia1.Text.Trim();
            string ttg=txtTentacgia1.Text.Trim();   
            string dt=txtDienthoai1.Text.Trim();   
            string gt=cbGioitinh1.SelectedText.Trim();

            if (con.State == ConnectionState.Closed)
                con.Open();

            String Sql = "Select * From Tacgia Where Matg like '%" + mtg + "%' and Tentg like N'%" + ttg + "%' and DienThoai like '%" + dt + "%' and GioiTinh like N'%" + gt + "%'";
            SqlCommand cmd = new SqlCommand(Sql, con);
            cmd.ExecuteNonQuery();
           // Tao doi tuong dataAdapter de lay kq tu command
           SqlDataAdapter   da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            // Tao doi tuong datatable de lay du lieu tu dataadapter
            DataTable tb = new DataTable();
            da.Fill(tb);
            cmd.Dispose();
            con.Close();
            //b6: Hiển thị DataTable lên DataGridView
            dgvTacgia.DataSource = tb;
            dgvTacgia.Refresh();


        }

        private void btnxuatfile_Click(object sender, EventArgs e)
        {

        }
    }
}


