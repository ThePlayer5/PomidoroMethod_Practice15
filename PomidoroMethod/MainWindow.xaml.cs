using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PomidoroMethod
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private TimeSpan _MyTime = new TimeSpan(0, 0, 25);
        private int Minutes = 0;
        private int Seconds = 25;
        public ObservableCollection<TodoItem> Tasks { get; set; } = new ObservableCollection<TodoItem>();
        public MainWindow()
        {
            InitializeComponent();
            TaskListLb.ItemsSource = Tasks;
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TaskInputTb.Text))
            {
                Tasks.Add(new TodoItem
                {
                    Title = TaskInputTb.Text,
                    IsDone = false
                });
                TaskInputTb.Text = string.Empty;
                UpdateCounter();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateCounter();
        }

        private void DeleteTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is TodoItem item)
            {
                Tasks.Remove(item);
                UpdateCounter();
            }
        }
        public void UpdateCounter()
        {
            int counter = 0;
            foreach (var task in Tasks)
            {
                if (!task.IsDone)
                {
                    counter++;
                }
            }
            CounterTextTbl.Text = $"Осталось дел: {counter}";
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 25; i > 0; i--)
            {
                Seconds--;
            }
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public class TodoItem
    {
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
    public class DoneToTextDecorationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isDone = (bool)value;
            return isDone ? TextDecorations.Strikethrough : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
