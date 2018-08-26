using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace HackMTY2018_Residencias
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            CenterToScreen();
            textBox2.UseSystemPasswordChar = true;
            textBox3.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains('@'))
            {
                MessageBox.Show("Ingrese una dirección de correo válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBox2.Text != textBox3.Text)
            {
                MessageBox.Show("Las contraseñas introducidas no coinciden.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlConnection con = new SqlConnection("Server=tcp:hackmty18.database.windows.net, 1433;Initial Catalog=residencias;Persist Security Info=False;User ID = team7;Password = ZXC741asd852qwe963;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate = False;Connection Timeout=30;");
            con.Open();
            try
            {
                SqlCommand com = new SqlCommand(string.Format("INSERT INTO estudiante (est_email, est_nombre, est_escuela, est_carrera, est_pwd, est_ruta_cv) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', 'N/A')",
                    textBox1.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox2.Text),
                    con);
                if (com.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Registro correcto.", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    con.Close();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
    }
}
