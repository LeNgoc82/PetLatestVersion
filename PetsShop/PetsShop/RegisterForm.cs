using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PetsShop
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            View();
        }

        
        SqlConnection con = new SqlConnection(@"Data Source=USERMIC-LHQ35UF\SQLEXPRESS;Initial Catalog = Pets; Integrated Security = True");

        // Bản chất của cái view này thực ra là truy vấn.=> cho xem dạng bảng. 
        // trong btn Addnew, Update, Delete, query = null. 
        // nếu query=null,tức không truyền gì => nó sẽ được gán với cái lệnh trong if => View thôi
        //còn nếu truyền cho nó 1 chuỗi truy vấn cụ thể thì nó thực hiện truy vấn theo yêu cầu query mà ta truyền vào => ứng dụng vào Search
        private void View(string query = null)
        {
            if(string.IsNullOrEmpty(query))
            {
                query = "select * from PetInfor";
            }
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvPet.DataSource = dt;
            con.Close();
        }
       
        private void ThucThiPhiTruyVan(string cmd)
        {
            con.Open();
           
            SqlCommand sc = new SqlCommand(cmd, con);
            sc.CommandType = CommandType.Text;
            sc.ExecuteNonQuery();
            con.Close();
        }
        int currentId = -1;// ko cho hien Id
        private void dgvPet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            currentId = int.Parse(dgvPet.CurrentRow.Cells[0].Value.ToString()); 
            txtName.Text = dgvPet.CurrentRow.Cells[1].Value.ToString();
            cbGender.Text = dgvPet.CurrentRow.Cells[2].Value.ToString();
            txtAge.Text = dgvPet.CurrentRow.Cells[3].Value.ToString();
            txtOrigin.Text = dgvPet.CurrentRow.Cells[4].Value.ToString();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string cmd = string.Format("insert into PetInfor values ('{0}','{1}',{2},'{3}')", txtName.Text, cbGender.Text, txtAge.Text, txtOrigin.Text);
            ThucThiPhiTruyVan(cmd);
            MessageBox.Show(" Add new object successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            View();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string cmd = string.Format("update PetInfor set  PetName ='{0}',Gennder='{1}',Age= {2},Origin='{3}' where PetId={4}", txtName.Text, cbGender.Text, txtAge.Text, txtOrigin.Text, currentId);
            ThucThiPhiTruyVan(cmd);
            MessageBox.Show(" Updated successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            View();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you want to delete this object?", "Delele item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string cmd = string.Format("delete from PetInfor where PetId ={0}", currentId);
                ThucThiPhiTruyVan(cmd);
                MessageBox.Show(" Delete successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                View();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // móe mấy cái dấu nháy phải viết liền vào. ko đc press space, ko thì nó chạy đếch ra đâu >.< 
            View("select * from PetInfor where PetName like '%" + txtSearch.Text + "%'");
        }
         
        //đặc trưng cơ bản nhất của Text là TextChange
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // móe mấy cái dấu nháy phải viết liền vào. ko đc press space, ko thì nó chạy đếch ra đâu >.< 
            View("select * from PetInfor where PetName like '%"+ txtSearch.Text +"%'");
        }
    }
}
