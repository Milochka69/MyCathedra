using System.Windows;

namespace MyCathedra
{
    public partial class InputBox 
    {
        public InputBox(string question, string defaultAnswer = "")
        {
            InitializeComponent();
            LblQuestion.Content = question;
            TxtAnswer.Text = defaultAnswer;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public string Answer => TxtAnswer.Text;
    }
}
