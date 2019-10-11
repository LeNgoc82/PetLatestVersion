using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PetsShop
{
    public partial class LoginForm : Form
    {

        SqlConnection sqlCon = new SqlConnection();
        SqlCommand command = new SqlCommand();
        public LoginForm()
        {
            InitializeComponent();
            sqlCon.ConnectionString = @"Data Source=USERMIC-LHQ35UF\SQLEXPRESS;Initial Catalog=Pets;Integrated Security=True";
        }

        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        private void btnLogin_Click(object sender, EventArgs e)
        {

            sqlCon.Open();
            command.Connection = sqlCon;
            command.CommandText = " select * from AccountLogin ";
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                if (txtName.Text.Equals(dataReader["AccountName"].ToString()) && txtPass.Text.Equals(dataReader["Pass"].ToString()))
                {
                    MessageBox.Show("Login Successfully!", "Congrates! ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Either Username or Password is incorrect! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Text = string.Empty;
                    txtPass.Text = string.Empty;
                }
                
            }
            sqlCon.Close();
        }
    }
}
