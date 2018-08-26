using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HackMTY2018_Residencias
{
    public partial class Form3 : Form
    {
        private int vid;
        private string[] datavec;

        public Form3(int id, string[] datavec)
        {
            InitializeComponent();
            CenterToScreen();
            this.MaximizeBox = false;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            richTextBox1.ReadOnly = true;
            vid = id;
            this.datavec = datavec;
            LoadInfo(vid);
        }

        private void LoadInfo(int id)
        {
            try
            {
                SqlConnection con = new SqlConnection("Server=tcp:hackmty18.database.windows.net, 1433;Initial Catalog=residencias;Persist Security Info=False;User ID = team7;Password = ZXC741asd852qwe963;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate = False;Connection Timeout=30;");
                con.Open();
                SqlDataReader read = new SqlCommand("SELECT empresa.emp_nombre, vacante.vac_carrera, vacante.vac_ubicacion, vacante.vac_descr FROM empresa JOIN vacante ON (empresa.emp_id = vacante.emp_id) WHERE vacante.vac_id = " + id, con).ExecuteReader();
                read.Read();
                textBox1.Text = read.GetString(0);
                textBox2.Text = read.GetString(1);
                textBox3.Text = read.GetString(2);
                richTextBox1.Text = read.GetString(3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Server=tcp:hackmty18.database.windows.net, 1433;Initial Catalog=residencias;Persist Security Info=False;User ID = team7;Password = ZXC741asd852qwe963;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate = False;Connection Timeout=30;");
                con.Open();
                SqlCommand com = new SqlCommand("INSERT INTO estudiante_vacante VALUES("+ datavec[0] +", "+ vid +")", con);
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Se enviará un correo con su información a la empresa para su futura revisión.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
