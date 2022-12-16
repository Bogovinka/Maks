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
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        DatabaseEntities db = new DatabaseEntities();
        public AdminMenu()
        {
            InitializeComponent();
            pn.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 1).ToList();
            vt.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 2).ToList();
            sr.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 3).ToList();
            ct.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 4).ToList();
            pt.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 5).ToList();
            sb.ItemsSource = db.Lesson.Where(x => x.DayWeek_id == 6).ToList();
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            cb.ItemsSource = db.Items.ToList();
        }
        ComboBox cb = null;
        int id;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cb = (ComboBox)sender;
            id = Convert.ToInt32(cb.Tag);
            Lesson less = db.Lesson.Where(x => x.ID == id).FirstOrDefault();
            less.Items_id = Convert.ToInt32(cb.SelectedValue);
            db.SaveChanges();
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
