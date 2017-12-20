using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace IndZad
{
    class MyCollection
    {

        public static ArrayList NewCollection(int i)
        {

            SqlConnection sqlConnection;
            string connectionString = @" Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\IndZad - копия\IndZad\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            SqlDataReader sqlReader = null;
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [Teachers]", sqlConnection);
            ArrayList arr = new ArrayList();

            sqlReader = command.ExecuteReader(); //считывает
            int w = 0;
            while (++w < i)
            {
                sqlReader.Read();

                arr.Add(Convert.ToString(sqlReader["Имя"]));
            }

            return arr;
        }


        public static void RemoveElementMyCollection(int i, int j, ref ArrayList arr)
        {
            arr.RemoveRange(i, j);
        }
        public static void AddElementInMyCollection(int i, ref ArrayList arr)
        {
            SqlConnection sqlConnection;
            string connectionString = @" Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\IndZad - копия\IndZad\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            SqlDataReader sqlReader = null;
            sqlConnection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [Teachers]", sqlConnection);

            sqlReader = command.ExecuteReader(); //считывает

            int w = 0;
         


            while (++w < i)
            {
                sqlReader.Read();
                arr.Add(Convert.ToString(sqlReader["Имя"]));
            }

        }

        public static void WriteMyCollection(ArrayList arr)
        {
            foreach (string a in arr)
                Console.Write("{0}\t", a);
            Console.WriteLine("\n");
        }

    }

}
