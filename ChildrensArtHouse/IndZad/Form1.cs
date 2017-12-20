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
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace IndZad
{
    enum Sections { Drawing = 1, Dancing = 2, Modeling = 3, Plush_toys = 4 }; //перечисление
    public partial class Form1 : Form
    {

        SqlConnection sqlConnection;
        List<Teachers> teachers = new List<Teachers>();
        List<Teachers> teachers1 = new List<Teachers>();
        List<clubs> Clubs = new List<clubs>();
        List<TheHouse_for_arts_for_children> TheHouse_for_arts = new List<TheHouse_for_arts_for_children>();
        string url = @" Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\GitFolder\ChildrensArtHouse\IndZad\Database1.mdf;Integrated Security=True";
        int counter;
        List<string> list1;
       FileStream fs;


        public Form1()
        {
            InitializeComponent();
        }


        private async void button3_Click(object sender, EventArgs e)
        {
            #region Teachers

            if (label12.Visible)
                label12.Visible = false;

            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
               

                try
                {
                    List<Diplom> diplom = new List<Diplom>();
                    Random rand = new Random();

                    Date data1 = new Date(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox11.Text), Convert.ToInt32(textBox12.Text));
                    Teachers teac = new Teachers(textBox3.Text, textBox2.Text);
                    SqlCommand command = new SqlCommand("INSERT INTO [Teachers](Имя, Фамилия, День, Месяц, Год)VALUES(@Имя, @Фамилия, @День, @Месяц, @Год)", sqlConnection);
                    command.Parameters.AddWithValue("Имя", textBox2.Text);
                    command.Parameters.AddWithValue("Фамилия", textBox3.Text);
                    command.Parameters.AddWithValue("День", Convert.ToInt32(textBox1.Text));
                    command.Parameters.AddWithValue("Месяц", Convert.ToInt32(textBox11.Text));
                    command.Parameters.AddWithValue("Год", Convert.ToInt32(textBox12.Text));
                    await command.ExecuteNonQueryAsync();
                  
                    teachers1.Add(new Teachers(textBox2.Text, textBox3.Text, Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox1.Text)));
                    Diplom dip = new Diplom();
                    diplom.Add(new Diplom(textBox2.Text, textBox3.Text, data1, dip.Check()));
                    bool a = dip.HasHighEducation;

                    richTextBox3.Clear();
                    richTextBox3.AppendText("\n" + "Руководитель с " + "именем " + textBox3.Text + " фамилией " + textBox2.Text + " и датой рождения " + data1.Show() + " успешно добавлен!" + "\n");
                    if (dip.Check() == true)
                        richTextBox3.AppendText("высшее образование имеется");
                    else richTextBox3.AppendText("высшего образования нет");

                }
                catch (Exception r)
                {

                    richTextBox3.Text = r.Message;
                }

            }

            else
            {
                label12.Visible = true;
                label12.Text = "Имя и фамилия должны\n быть заполнены!";

            }

          
        }
            #endregion

        private async void Form1_Load(object sender, EventArgs e)
        {
           
            this.teachersTableAdapter1.Fill(this.database1DataSet2.Teachers);
            comboBox1.DataSource = Enum.GetValues(typeof(Sections));
            string connectionString = url;
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync(); //открыли соединение БД

            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Teachers]", sqlConnection);
         
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
              

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "      " + Convert.ToString(sqlReader["Имя"]) + "    " + Convert.ToString(sqlReader["Фамилия"] + "    " + Convert.ToString(sqlReader["День"]) + "        " + Convert.ToString(sqlReader["Месяц"]) + "        " + Convert.ToString(sqlReader["Год"])));
                    teachers1.Add(new Teachers(Convert.ToString(sqlReader["Имя"]), Convert.ToString(sqlReader["Фамилия"]), Convert.ToInt32(sqlReader["День"]), Convert.ToInt32(sqlReader["Месяц"]), Convert.ToInt32(sqlReader["Год"])));
                  
                        counter++;
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
            }

            SqlCommand command1 = new SqlCommand("SELECT * FROM [Kruzhki]", sqlConnection);
            SqlDataReader sqlReader1 = null;

            try
            {
                sqlReader1 = await command1.ExecuteReaderAsync();


                while (await sqlReader1.ReadAsync())
                {
                   
                    Clubs.Add(new clubs(Convert.ToString(sqlReader1["Название_кружка"]), Convert.ToInt32(sqlReader1["Цена"]), Convert.ToInt32(sqlReader1["Колво_занятий"]), Convert.ToInt32(sqlReader1["Колво_учеников"]), Convert.ToString(sqlReader1["Руководитель"]), Convert.ToString(sqlReader1["Секция"])));
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            finally
            {
                if (sqlReader1 != null)
                    sqlReader1.Close();
            }
        }

        #region
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }
        #endregion
        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {


            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Teachers]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync(); //считывает

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["id"]) + "      " + Convert.ToString(sqlReader["Имя"]) + "    " + Convert.ToString(sqlReader["Фамилия"] + "    " + Convert.ToString(sqlReader["День"]) + "    " + Convert.ToString(sqlReader["Месяц"]) + "    " + Convert.ToString(sqlReader["Год"])));
                    teachers1.Add(new Teachers(Convert.ToString(sqlReader["Имя"]), Convert.ToString(sqlReader["Фамилия"]), Convert.ToInt32(sqlReader["День"]), Convert.ToInt32(sqlReader["Месяц"]), Convert.ToInt32(sqlReader["Год"])));
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
            }

            string sql = "SELECT * FROM [Teachers]";
            string connectionString = url;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                // перебор всех таблиц
                foreach (DataTable dt in ds.Tables)
                {
                  
                    list1 = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<string>(2)).ToList();
                    foreach (object cell in list1)
                    {
                        richTextBox3.AppendText(cell.ToString() + Environment.NewLine);
                        Console.WriteLine(cell);
                    }

                    comboBox2.DataSource = list1;
                }
            }
        }


        private async void button6_Click(object sender, EventArgs e)
        {

            if (label12.Visible)
                label12.Visible = false;

            if (!string.IsNullOrEmpty(textBox10.Text) && !string.IsNullOrWhiteSpace(textBox10.Text))
            {
                SqlCommand command = new SqlCommand("DELETE FROM [Teachers] WHERE [Id]=@Id", sqlConnection);

                command.Parameters.AddWithValue("Id", textBox10.Text);

                try
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch
                {

                    richTextBox3.Text = "Id должен быть цифрой";
                }
            }
            else
            {
                label12.Visible = true;
                label12.Text = "ID должен быть заполнен!";
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            richTextBox3.Visible = true;
            if (label14.Visible)
                label14.Visible = false;

            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) &&
                !string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text) &&
                !string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text) &&
                !string.IsNullOrEmpty(textBox7.Text) && !string.IsNullOrWhiteSpace(textBox7.Text))
            {

                SqlCommand command = new SqlCommand("INSERT INTO [Kruzhki](Название_кружка, Секция, Руководитель, Цена, Колво_учеников, Колво_занятий)VALUES(@Название_кружка, @Секция, @Руководитель, @Цена, @Колво_учеников, @Колво_занятий)", sqlConnection);

                command.Parameters.AddWithValue("Название_кружка", textBox4.Text);
                command.Parameters.AddWithValue("Секция", comboBox1.Text);
                command.Parameters.AddWithValue("Руководитель", comboBox2.Text);
                command.Parameters.AddWithValue("Цена", Convert.ToInt32(textBox5.Text));
                command.Parameters.AddWithValue("Колво_учеников", Convert.ToInt32(textBox6.Text));
                command.Parameters.AddWithValue("Колво_занятий", Convert.ToInt32(textBox7.Text));
                richTextBox5.Visible = true;
                Clubs.Add(new clubs(textBox4.Text, Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox7.Text), Convert.ToInt32(textBox6.Text), comboBox2.Text, comboBox1.Text));
                Obobschenie<string, string, string, float, int, int> obj2 = new Obobschenie<string, string, string, float, int, int>(textBox4.Text, comboBox1.Text, comboBox2.Text, Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox7.Text));
                richTextBox5.AppendText(obj2.objectsType1());
              
                await command.ExecuteNonQueryAsync();

            }

            else
            {
                label14.Visible = true;
                label14.Text = "Все поля должны быть заполнены!";

            }

        }

        private async void button5_Click(object sender, EventArgs e)
        {
            #region
            // чтобы создать несколько домов детского творчества без смс и регистраций 
            //// string connectionString = @" Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\IndZad\Database1.mdf;Integrated Security=True";

            // //sqlConnection = new SqlConnection(connectionString);
            // //await sqlConnection.OpenAsync(); //открыли соединение БД

            // //SqlDataReader sqlReader = null;
            //    if (label15.Visible)
            //        label15.Visible = false;
            //    if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
            //       !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
            //    {

            //        SqlCommand command = new SqlCommand("INSERT INTO [TheHouse](Название, Адрес)VALUES(@Название, @Адрес)", sqlConnection);


            //        command.Parameters.AddWithValue("Название", textBox8.Text);
            //        command.Parameters.AddWithValue("Адрес", textBox9.Text);

            //        TheHouse_for_arts.Add(new TheHouse_for_arts_for_children(textBox8.Text, textBox9.Text));

            //        await command.ExecuteNonQueryAsync();


            //    }

            //    else
            //    {
            //        label15.Visible = true;
            //        label15.Text = "Все поля должны быть заполнены!";

            //    }

            //}
            #endregion
            string connectionString = url;
            int i = 0;
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync(); //открыли соединение БД

            SqlDataReader sqlReader = null;
            if (label15.Visible)
                label15.Visible = false;
            if (!string.IsNullOrEmpty(textBox8.Text) && !string.IsNullOrWhiteSpace(textBox8.Text) &&
               !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text))
            {
                SqlCommand command1 = new SqlCommand("SELECT * FROM [TheHouse] ", sqlConnection);

                sqlReader = await command1.ExecuteReaderAsync(); //считывает

                while (await sqlReader.ReadAsync())
                {

                    i = Convert.ToInt32(sqlReader["id"]);
                 
                }
                if (i >= 1)
                {
                    richTextBox4.Text = "Дом детского творчества уже зарегистрирован!";
                }
                else
                {
                    sqlReader.Close();
                    SqlCommand command = new SqlCommand("INSERT INTO [TheHouse](Название, Адрес)VALUES(@Название, @Адрес)", sqlConnection);


                    command.Parameters.AddWithValue("Название", textBox8.Text);
                    command.Parameters.AddWithValue("Адрес", textBox9.Text);

                    TheHouse_for_arts.Add(new TheHouse_for_arts_for_children(textBox8.Text, textBox9.Text));

                    await command.ExecuteNonQueryAsync();


                }
            }

            else
            {
                label15.Visible = true;
                label15.Text = "Все поля должны быть заполнены!";

            }
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBox1.SelectedIndex == 0))
            {
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
            }
            if ((comboBox1.SelectedIndex == 1))
            {
                pictureBox3.Visible = true;
                pictureBox2.Visible = false;
                pictureBox4.Visible = false;
                pictureBox5.Visible = false;
            }
            if ((comboBox1.SelectedIndex == 2))
            {
                pictureBox4.Visible = true;
                pictureBox2.Visible = false;
                pictureBox3.Visible = false;
                pictureBox5.Visible = false;
            }
            if ((comboBox1.SelectedIndex == 3))
            {
                pictureBox5.Visible = true;
                pictureBox2.Visible = false;
                pictureBox4.Visible = false;
                pictureBox3.Visible = false;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            TheHouse_for_arts_for_children the_house = new TheHouse_for_arts_for_children();
            string connectionString = url;
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync(); //открыли соединение БД

            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Kruzhki]", sqlConnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync(); //считывает

                while (await sqlReader.ReadAsync())
                {
                    double t = Convert.ToInt32(sqlReader["Цена"]);

                    richTextBox1.AppendText(Convert.ToString(sqlReader["id"]) + ")" + "Название кружка-" + Convert.ToString(sqlReader["Название_кружка"]) + "\n" + " Секция-" + Convert.ToString(sqlReader["Секция"] + "\n" + " Руководитель-" + Convert.ToString(sqlReader["Руководитель"]) + "\n" + " Цена-" + Math.Round(t,2) + " грн/занятие \n" + " Кол-во учеников-" + Convert.ToString(sqlReader["Колво_учеников"]) + "\n" + " Кол-во занятий-" + Convert.ToString(sqlReader["Колво_занятий"]) + "\n_______________________\n"));
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
            }
        }


        private void действующиеКружкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_kruzhki form_kruzkki = new Form_kruzhki();
            form_kruzkki.ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            TheHouse_for_arts_for_children the_house = new TheHouse_for_arts_for_children();
            string connectionString = url;
            sqlConnection = new SqlConnection(connectionString);
            await sqlConnection.OpenAsync(); //открыли соединение БД
            string i;
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM TheHouse, Kruzhki ", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                //считывает

                while (await sqlReader.ReadAsync())
                {
                    i = Convert.ToString(sqlReader["Id"]);
                    #region
                    // richTextBox2.Text = Convert.ToString(i);
                    // Console.WriteLine(Convert.ToString(i));
                    //if (string.IsNullOrEmpty(i))                    
                    // richTextBox2.AppendText("Не зарегистрирован ДДТ");
                    //  if (string.IsNullOrEmpty(richTextBox2.Text) && string.IsNullOrWhiteSpace(richTextBox2.Text)) { richTextBox2.AppendText("Не зарегистрирован ДДТ"); }
                    // else
                    #endregion

                    richTextBox2.AppendText(Convert.ToString("Название кружка-" + Convert.ToString(sqlReader["Название_кружка"]) + "\n" + " Кол-во учеников-" + Convert.ToString(sqlReader["Колво_учеников"]) + "\n" + "Адрес дома творчества-" + Convert.ToString(sqlReader["Адрес"] + "\n_______________________\n")));

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
            }
            #region
            //    SqlDataReader sqlReader1 = null;
            //    SqlCommand command1 = new SqlCommand("SELECT * FROM TheHouse", sqlConnection);
            //    try
            //    {
            //        sqlReader1 = await command1.ExecuteReaderAsync();
            //        while (await sqlReader1.ReadAsync())
            //        {
            //            richTextBox1.AppendText(Convert.ToString("adress" + Convert.ToString(sqlReader1["Адрес"] + "\n_______________________\n")));
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            //    }
            //    finally
            //    {
            //        if (sqlReader1 != null)
            //            sqlReader1.Close();
            //    }
            #endregion
        }

        private void button7_Click(object sender, EventArgs e)
        {


            foreach (Teachers cell in teachers1)
            {
                richTextBox1.AppendText(cell.name + Environment.NewLine);
            }
         
                if (teachers1[1] == teachers1[2])
                {
                   
                    MessageBox.Show("Первый и второй учитель имеюют одинаковые имена и фамилии");
                }
                else
                    MessageBox.Show("Первый и второй учитель имеюют разные имена и фамилии");
                
              
            }
         
        

        private void button8_Click(object sender, EventArgs e)
        {
            ArrayList Coll = MyCollection.NewCollection(Convert.ToInt32(textBox14.Text));
            richTextBox3.AppendText("Исходная коллекция: ");
            foreach (string a in Coll)
            {
                richTextBox3.AppendText(a);
            }
            richTextBox3.AppendText("\n");
            MyCollection.WriteMyCollection(Coll);
            int y = Convert.ToInt32(textBox14.Text);
            int u = Convert.ToInt32(textBox13.Text);
            int c = y - 1 - u;
            MyCollection.RemoveElementMyCollection(c, Convert.ToInt32(textBox13.Text), ref Coll);
            richTextBox3.AppendText("Коллекция после удаления предпоследних двух элементов: ");
            foreach (string a in Coll)
            {
                richTextBox3.AppendText(a);
            }
            richTextBox3.AppendText("\n");
            MyCollection.WriteMyCollection(Coll);

            // Добавим еще несколько элементов
            MyCollection.AddElementInMyCollection(3, ref Coll);
            richTextBox3.AppendText("Добавили 2 элементa: ");
            foreach (string a in Coll)
            {
                richTextBox3.AppendText(a);
            }
            richTextBox3.AppendText("\n");
            MyCollection.WriteMyCollection(Coll);


        }

        private void button9_Click(object sender, EventArgs e)
        {
            ArrayList objectList = new ArrayList() { 1, 2, "string", 'c', 2.0f };

            object obj = 45.8;

            objectList.Add(obj);
            objectList.Add("string2");
            objectList.RemoveAt(0); // удаление первого элемента
            foreach (object o in objectList)
            {
                richTextBox3.AppendText(Convert.ToString(o) + "\n");
            }
            richTextBox3.AppendText("Общее число элементов коллекции: " + objectList.Count);
            // обобщенная коллекция List
            List<string> countries = new List<string>() { "Украина", "США", "Великобритания", "Китай" };
            countries.Add("Франция");
            countries.RemoveAt(1); // удаление второго элемента
            foreach (string s in countries)
            {
                richTextBox3.AppendText(s + "\n");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (Clubs[Convert.ToInt32(textBox15.Text)] < Clubs[Convert.ToInt32(textBox16.Text)])
                {
                    MessageBox.Show("В " + (textBox15.Text)+" группе меньше учеников");
                 
                }
                else
                    MessageBox.Show("В " + (textBox15.Text) + " группе больше учеников. В " + (textBox15.Text) + " группе " + Convert.ToString(Clubs[Convert.ToInt32(textBox15.Text)].NumberOfPupils) + " учеников, а во " + (textBox16.Text) + " группе " + Convert.ToString(Clubs[Convert.ToInt32(textBox16.Text)].NumberOfPupils) + " учеников");
              
            }
            catch
            {

                MessageBox.Show("Проверьте введенные данные");
            };


              
        }

        private void button11_Click(object sender, EventArgs e)
        {
            richTextBox3.Clear();
            BinaryFormatter formatter = new BinaryFormatter();
            using ( fs = new FileStream("teachers.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, teachers1);
                MessageBox.Show("Сохранено");
            }
           
            using  ( fs = new FileStream("teachers.dat", FileMode.OpenOrCreate))
{
    List<Teachers> newPerson = (List<Teachers>)formatter.Deserialize(fs);
    foreach (Teachers p in newPerson)
    {
        richTextBox3.AppendText("Десериализация:");
        richTextBox3.AppendText("Имя:" + p.Name + "Фамилия" + p.Surname + "\n");
    }    
}
        }
    }
}



#region
//DBCC CHECKIDENT (Teachers, RESEED, 0) //скрипт 
#endregion

