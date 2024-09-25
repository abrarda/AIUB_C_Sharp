using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UniProject.Authentication;
using UniProject.Authentication.libs;
using UniProject.Utilities;

namespace UniProject
{
    public partial class signIn : BaseAuthForm
    {
        public signIn()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void emailIcon_Click(object sender, EventArgs e)
        {

        }
        
        // create new acc button
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormAnimation animation = new FormAnimation();

            animation.FadeOut(this, 200);
            this.Hide();

            SignUp form = new SignUp();
            animation.FadeIn(form, 200);
        }

        // login btn
        private void button3_Click(object sender, EventArgs e)
        {
            Lib validator = new Lib();

            string email = emaiInput.Text;
            string password = passwordInput.Text;

            var data = validator.Validate(null, email, password, false);

            if (data.email == null || data.hashedPassword == null) // Check if validation failed
            {
                return; // Exit if validation fails
            }

            MessageBoxUtility.ShowSuccessMessage(data.email + " " + data.hashedPassword);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signIn_Load(object sender, EventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radious = 30;

            path.AddArc(0, 0, radious, radious, 180, 90);
            path.AddArc(this.Width - radious, 0, radious, radious, 270, 90);
            path.AddArc(this.Width - radious, this.Height - radious, radious, radious, 0, 90);
            path.AddArc(0, this.Height - radious, radious, radious, 90, 90);

            this.Region = new System.Drawing.Region(path);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            EnableDrag(nav_panel);
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
