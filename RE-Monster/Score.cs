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

namespace RE_Monster
{
    public partial class Score : Form
    {
        SqlConnection sqlConnection;

        int _score;

        public Score()
        {
            InitializeComponent();
        }

        private async void Score_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\79994\source\repos\RE-Monster\Database1.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlDataReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [TOP]",sqlConnection);

            SqlCommand coomandSort = new SqlCommand("SELECT * FROM [TOP] ORDER BY [SCORE] DESC");

            try
            {
                sqlDataReader = await command.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlDataReader["Id"]) + "      " + Convert.ToString(sqlDataReader["NAME"]) + "      " + Convert.ToString(sqlDataReader["SCORE"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                {
                    sqlDataReader.Close();
                }
            }
        }
    }
}
