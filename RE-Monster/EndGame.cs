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
    public partial class EndGame : Form
    {
        SqlConnection sqlConnection;
        int _score;

        
        public EndGame()
        {
            InitializeComponent();
            
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            D.f1.Show();
        }

        public async void Zapis()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\79994\source\repos\RE-Monster\Database1.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);
            //@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";

            await sqlConnection.OpenAsync();

            SqlDataReader sqlDataReader = null;

            SqlCommand command = new SqlCommand("SELECT TOP(10) * FROM [TOP] ORDER BY [SCORE] DESC", sqlConnection);

            SqlCommand coomandSort = new SqlCommand("SELECT * FROM [TOP] ORDER BY [SCORE] DESC");

            try
            {
                sqlDataReader = await command.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                        listBox1.Items.Add(Convert.ToString(sqlDataReader["NAME"]) + "      " + Convert.ToString(sqlDataReader["SCORE"]));
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

        private async void EndGame_Load(object sender, EventArgs e)
        {
            label2.Text = Convert.ToString(_score);

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\79994\source\repos\RE-Monster\Database1.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlDataReader = null;

            SqlCommand command = new SqlCommand("SELECT TOP(10) * FROM [TOP] ORDER BY [SCORE] DESC", sqlConnection);

            SqlCommand coomandSort = new SqlCommand("SELECT * FROM [TOP] ORDER BY [SCORE] DESC");

            try
            {
                sqlDataReader = await command.ExecuteReaderAsync();

                while (await sqlDataReader.ReadAsync())
                {
                        listBox1.Items.Add(Convert.ToString(sqlDataReader["NAME"]) + "      " + Convert.ToString(sqlDataReader["SCORE"]));
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

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                SqlCommand command2 = new SqlCommand("INSERT INTO [TOP] (NAME, SCORE)VALUES(@NAME, @SCORE)", sqlConnection);

                SqlCommand coomandSort = new SqlCommand("SELECT * FROM [TOP] ORDER BY [SCORE] DESC");

                command2.Parameters.AddWithValue("NAME", textBox1.Text);

                command2.Parameters.AddWithValue("SCORE", _score);

                await command2.ExecuteNonQueryAsync();

                Score score = new Score();
                listBox1.Items.Clear();

                SqlDataReader sqlDataReader = null;

                SqlCommand command = new SqlCommand("SELECT TOP(10) * FROM [TOP] ORDER BY [SCORE] DESC", sqlConnection);

                try
                {
                    sqlDataReader = await command.ExecuteReaderAsync();

                    while (await sqlDataReader.ReadAsync())
                    {
                            score.listBox1.Items.Add(Convert.ToString(sqlDataReader["NAME"]) + "      " + Convert.ToString(sqlDataReader["SCORE"]));
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

                Zapis();
                button2.Enabled = false;
            }
        }
    }
}
