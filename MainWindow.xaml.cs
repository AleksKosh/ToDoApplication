using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApplication;

namespace ToDoApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<Tasks> TasksList = new ObservableCollection<Tasks>();

        public MainWindow()
        {
            InitializeComponent();

            ToDoListBox.ItemsSource = TasksList;
            ToDoListBox.DisplayMemberPath = "Name";

        }

        private void ToDoListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Tasks? selected = ToDoListBox.SelectedItem as Tasks;
            if (selected != null)
            {
                MessageBox.Show(selected.Description);

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewTasksWindow window = new NewTasksWindow();
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (window.ShowDialog() == true)
            {
                Tasks newTask = window.Result;
                TasksList.Add(newTask);
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int Index = ToDoListBox.SelectedIndex;
            if (Index != -1)
            {
                TasksList.RemoveAt(Index);
            }
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            int Index = ToDoListBox.SelectedIndex;
            if (Index != -1)
            {
                TasksList[Index].IsCompleted = true;
            }
        }

        private void allRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ToDoListBox.ItemsSource = TasksList;
        }

        private void NotCompletedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Tasks> filtered = new ObservableCollection<Tasks>();

            for (int i = 0; i < TasksList.Count; i++)
            {
                Tasks current = TasksList[i];
                if (current.IsCompleted == false)
                {
                    filtered.Add(current);
                }
                ToDoListBox.ItemsSource = filtered;
            }
        }

        private void CompletedRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Tasks> completedtasks = new ObservableCollection<Tasks>();
            for (int i = 0; i < TasksList.Count; i++)
            {
                Tasks current = TasksList[i];
                if (current.IsCompleted == true)
                {
                    completedtasks.Add(current);
                }
                ToDoListBox.ItemsSource = completedtasks;
            }
        }
  
          string fileName = "data.bin";
        private void Window_Closed(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream file=File.OpenWrite(fileName);
            formatter.Serialize(file, TasksList);
            file.Close();
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (File.Exists(fileName));
                {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream file = File.OpenRead(fileName);
                if (file.Length!=0)
                
                TasksList = formatter.Deserialize(file) as ObservableCollection<Tasks>;
                file.Close();

                ToDoListBox.ItemsSource = TasksList;


            }
           

        }
        
    } }
