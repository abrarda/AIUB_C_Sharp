using System;
using UniProject.Utilities;
using System.Windows.Forms;
using UniProject.Authentication.libs;
using UniProject.Database;
using MySql.Data.MySqlClient;

namespace UniProject.Authentication
{
    public partial class SignUp : BaseAuthForm
    {

        public SignUp()
        {
            InitializeComponent();
            DatabaseSetup.CreateDatabaseAndTables();
        }

        private void nav_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            Lib validator = new Lib();

            string userName = username_input.Text;
            string email = email_input.Text;
            string password = password_input.Text;

            var data = validator.Validate(userName, email, password, true);

            if (data.username == null || data.email == null || data.hashedPassword == null)
            {
                return;
            }

            StoreUserInDatabase(data.username, data.email, data.hashedPassword);
        }

        // store registered users data in database
        private void StoreUserInDatabase(string username, string email, string password)
        {
            using (var connection = db_config.GetConnection())
            {
                try
                {
                    connection.Open();

                    string sql = "INSERT INTO users (username, email, password_hash) VALUES (@username, @email, @hashedPassword)";

                    using (var cmd = new MySqlCommand(sql, connection))
                    {
                        // Ensure that the parameter name matches the SQL command
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@hashedPassword", password);

                        // Execute the command
                        cmd.ExecuteNonQuery();
                        MessageBoxUtility.ShowSuccessMessage("User registered successfully.");
                    }
                }
                catch (MySqlException ex)
                {
                    // Handle MySQL-specific exceptions
                    MessageBoxUtility.ShowErrorMessage("MySQL Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    MessageBoxUtility.ShowErrorMessage("Error: " + ex.Message);
                }
            }
        }


        private void close_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void back_to_signIn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormAnimation animation = new FormAnimation();

            animation.FadeOut(this, 300);
            this.Hide();

            signIn form = new signIn();
            animation.FadeIn(form, 300);
        }

        private void sign_up_form_Load(object sender, EventArgs e)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radious = 30;

            path.AddArc(0, 0, radious, radious, 180, 90);
            path.AddArc(this.Width - radious, 0, radious, radious, 270, 90);
            path.AddArc(this.Width - radious, this.Height - radious, radious, radious, 0, 90);
            path.AddArc(0, this.Height - radious, radious, radious, 90, 90);

            this.Region = new System.Drawing.Region(path);
        }

        private void nav_panel_MouseDown(object sender, MouseEventArgs e)
        {
            EnableDrag(nav_panel);
        }
    }
}
