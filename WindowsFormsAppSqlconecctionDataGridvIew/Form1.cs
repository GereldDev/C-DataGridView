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

namespace WindowsFormsAppSqlconecctionDataGridvIew
{
    public partial class Form1 : Form
    {
        private SqlConnection conexion  = new SqlConnection("server=GERALDTITAN\\SQLEXPRESS01 ; database=base1 ; integrated security = true");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //leer registros para leer bases de datos 
            conexion.Open();
            string sql = "select documento,nombre,sueldo  from empleados";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader r = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while(r.Read())
            {
                dataGridView1.Rows.Add(r["documento"].ToString(), r["nombre"].ToString(), r["sueldo"].ToString());
            }
            conexion.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //select comando select y where como consultar especifica
            conexion.Open();
            string sql = $"select documento,nombre,sueldo  from empleados where sueldo>{textBox1.Text}";
            SqlCommand comando = new SqlCommand(sql, conexion);
            SqlDataReader r = comando.ExecuteReader();
            dataGridView1.Rows.Clear();
            while (r.Read())
            {
                dataGridView1.Rows.Add(r["documento"].ToString(), r["nombre"].ToString(), r["sueldo"].ToString());
            }
            conexion.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string sql = "delete from empleados where nombre=@nombre";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@nombre", SqlDbType.Char).Value = textBox2.Text;
            int cant = comando.ExecuteNonQuery();
            textBox2.Text = "";
            MessageBox.Show("Cantidad de Registro borrado: " + cant.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string sql = "insert into empleados(documento,nombre,sueldo) values(@documento,@nombre,@sueldo)";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.Add("@documento", SqlDbType.Char).Value= textBox4.Text;
            comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBox3.Text;
            comando.Parameters.Add("@sueldo", SqlDbType.Float).Value = textBox5.Text;
            comando.ExecuteNonQuery();
            textBox4.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            MessageBox.Show("Insert OK");
            conexion.Close();

        }
    }
}
