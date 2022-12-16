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

namespace Maks
{
    /// <summary>
    /// Логика взаимодействия для StudentMenu.xaml
    /// </summary>
    public partial class StudentMenu : Window
    {
        DatabaseEntities db = new DatabaseEntities();
        public StudentMenu()
        {
            InitializeComponent();
            pn.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 1).ToList();
            vt.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 2).ToList();
            sr.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 3).ToList();
            ct.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 4).ToList();
            pt.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 5).ToList();
            sb.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 6).ToList();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.L)
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                Close();
            }
        }
    }
}
