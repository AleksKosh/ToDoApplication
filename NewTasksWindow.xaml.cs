using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDoApplication
{
    /// <summary>
    /// Логика взаимодействия для NewTasksWindow.xaml
    /// </summary>
    public partial class NewTasksWindow : Window
    {
        public Tasks Result { get; set; }

        public NewTasksWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text != "")
            {
                Tasks t = new Tasks();
                t.Name = NameTextBox.Text;
                t.IsCompleted = IsCompletedCheckBox.IsChecked.Value;
                t.Description = DescriptionTextBox.Text;
                Result = t;

                DialogResult = true;
            }
            else
            {
                NameTextBox.Background = Brushes.Red;
            }
        }

       
    }
}
