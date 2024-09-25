using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniProject.Utilities
{
    public class FormAnimation
    {
        public async void FadeOut(Form form, int duration)
        {
            for (int i = 100; i >= 0; i -= 5)
            {
                form.Opacity = i / 100.0; // Set the form's opacity
                await Task.Delay(duration / 20); // Adjust the delay to control speed
            }
        }

        public async void FadeIn(Form form, int duration)
        {
            form.Opacity = 0; // Start with form completely transparent
            form.Show(); // Show the form
            for (int i = 0; i <= 100; i += 5)
            {
                form.Opacity = i / 100.0; // Increase opacity
                await Task.Delay(duration / 20); // Adjust the delay to control speed
            }
        }
    }
}
