using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginModerno
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RedondearEsquina(panel2, 30);
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contra = txtContra.Text;

            // Hardcoded username and password for demonstration purposes
            string validaUsuario = "usuario";
            string validaContra = "1234";

            if (usuario == validaUsuario && contra == validaContra)
            {
                MessageBox.Show("Inicio de sesión exitoso");
                // Perform any actions needed after successful login
                //Form1 form1 = new Form1();
                //Form2 form2 = new Form2();

                //// Hide Form1
                //form1.Hide();

                //// Show Form2
                //form2.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña invalido. Inténtalo de nuevo");
                // Clear the password field for another attempt
                txtUsuario.Clear();
                txtContra.Clear();
                txtUsuario.Focus(); 
            }

        }

        private void RedondearEsquina(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();

            control.Region = new Region(path);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtContra.Focus();
            }
        }

        private void txtContra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnIngresar.Focus();
        }
    }
}
