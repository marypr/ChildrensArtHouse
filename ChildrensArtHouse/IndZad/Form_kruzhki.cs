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
using System.Data.OleDb;

namespace IndZad
{
    public partial class Form_kruzhki : Form
    {

        SqlConnection sqlConnection;
        public Form_kruzhki()
        {
            InitializeComponent();
        }

        private async void Form_kruzhki_Load(object sender, EventArgs e)
        {

            string connectionString = @" Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\GitFolder\ChildrensArtHouse\IndZad\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync(); //открыли соединение БД

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Kruzhki]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync(); //считывает

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "      " + Convert.ToString(sqlReader["Название_кружка"]) + "    " + Convert.ToString(sqlReader["Секция"] + "    " + Convert.ToString(sqlReader["Руководитель"]) + "        " + Convert.ToString(sqlReader["Цена"]) + "        " + Convert.ToString(sqlReader["Колво_учеников"]) + "     " +Convert.ToString(sqlReader["Колво_занятий"]) ));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }}


        private async void button1_Click(object sender, EventArgs e)
        {
  
           
            if (label2.Visible)
                label2.Visible = false;

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Kruzhki] WHERE [Id]=@Id", sqlConnection);
                command.Parameters.AddWithValue("Id", textBox1.Text);
                await command.ExecuteNonQueryAsync();

            }
            else
            {
                label2.Visible = true;
                label2.Text = "ID должен быть заполнен!";
            }
        }
    }}
    


