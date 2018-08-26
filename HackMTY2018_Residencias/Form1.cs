using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HackMTY2018_Residencias
{
    public partial class Form1 : Form
    {
        private Form2 _f2;
        private Form4 _f4;

        public Form1()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
            this.MaximizeBox = false;
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_f2 == null || _f2.IsDisposed)
            {
                SqlConnection con = new SqlConnection("Server=tcp:hackmty18.database.windows.net, 1433;Initial Catalog=residencias;Persist Security Info=False;User ID = team7;Password = ZXC741asd852qwe963;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate = False;Connection Timeout=30;");
                con.Open();
                try
                {
                    SqlDataReader read = new SqlCommand("SELECT * FROM estudiante WHERE est_email = '" + textBox1.Text + "' AND est_pwd = '" + textBox2.Text + "'", con).ExecuteReader();
                    if (read.HasRows)
                    {
                        read.Read();
                        string[] datavec = new string[read.FieldCount];
                        for (int i = 0; i < read.FieldCount; i++)
                            datavec[i] = read.GetValue(i).ToString();
                        Form1 _this = this;
                        _f2 = new Form2(datavec, ref _this);
                        _f2.Show();
                        con.Close();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario indicado.\nVerifique sus credenciales, por favor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox1.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_f4 == null || _f4.IsDisposed)
            {
                _f4 = new Form4();
                _f4.Show();
            }
        }
    }
}
