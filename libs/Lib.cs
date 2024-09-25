using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UniProject.Utilities;

namespace UniProject.Authentication.libs
{
    public class Lib
    {
        // Validate username, email, and password based on the context (isSignUp or login)
        public (string username, string email, string hashedPassword) Validate(string username, string email, string password, bool isSignUp)
        {
            if (isSignUp)
            {
                string usernameError = IsValidUsername(username);
                if (usernameError != null)
                {
                    return (null, null, null); // Return null if username validation fails
                }
            }

            string emailError = IsValidEmail(email);
            if (emailError != null)
            {
                return (null, null, null); // Return null if email validation fails
            }

            string hashedPassword = IsValidPassword(password);
            if (hashedPassword == null)
            {
                return (null, null, null); // Return null if password validation fails
            }

            return (username, email, hashedPassword); // Return valid username, email, and hashed password
        }

        // Validate username for sign-up
        private string IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBoxUtility.ShowErrorMessage("Username cannot be empty.");
                return "Username cannot be empty."; // Return an error message
            }
            if (username.Length < 4)
            {
                MessageBoxUtility.ShowErrorMessage("Username must be at least 4 characters long.");
                return "Username must be at least 4 characters long."; // Return an error message
            }
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9]+$"))
            {
                MessageBoxUtility.ShowErrorMessage("Username can only contain alphanumeric characters.");
                return "Username can only contain alphanumeric characters."; // Return an error message
            }

            // Username is valid
            return null;
        }

        // Validate email format
        private string IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern))
            {
                MessageBoxUtility.ShowErrorMessage("Invalid email format.");
                return "Invalid email format."; // Return an error message
            }
            return null; // Email is valid
        }

        // Validate password
        private string IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBoxUtility.ShowErrorMessage("Password cannot be empty.");
                return null; // Indicate failure
            }
            if (password.Length < 6)
            {
                MessageBoxUtility.ShowErrorMessage("Password must be at least 6 characters long.");
                return null; // Indicate failure
            }
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                MessageBoxUtility.ShowErrorMessage("Password must contain at least one uppercase letter.");
                return null; // Indicate failure
            }
            if (!Regex.IsMatch(password, @"[0-9]"))
            {
                MessageBoxUtility.ShowErrorMessage("Password must contain at least one number.");
                return null; // Indicate failure
            }

            return HashPassword(password); // Return the hashed password if valid
        }

        // Hash password using SHA256
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the password string to a byte array and compute the hash
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convert byte to hexadecimal
                }
                return builder.ToString(); // Return the hashed password as a string
            }
        }
        
        // Method to verify the password
        public bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            // Hash the entered password
            string hashedEnteredPassword = HashPassword(enteredPassword);

            // Compare with the stored hashed password
            return hashedEnteredPassword.Equals(storedHashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
}