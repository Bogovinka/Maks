using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
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
using Point = System.Drawing.Point;

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

        private void save_Click(object sender, RoutedEventArgs e)
        {
            snapshotHandler.Savesnapshot(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle);
        }
    }
    public sealed class snapshotHandler
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int m_left;
            public int m_top;
            public int m_right;
            public int m_bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        public static void Savesnapshot(IntPtr handle_)
        {
            RECT windowRect = new RECT();
            GetWindowRect(handle_, ref windowRect);

            Int32 width = windowRect.m_right - windowRect.m_left;
            Int32 height = windowRect.m_bottom - windowRect.m_top;
            Point topLeft = new Point(windowRect.m_left, windowRect.m_top);

            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);
            g.CopyFromScreen(topLeft, new Point(0, 0), new System.Drawing.Size(width, height));
            SaveFileDialog sD = new SaveFileDialog();
            if (sD.ShowDialog() == true)
            {
                b.Save(sD.FileName + ".jpg", ImageFormat.Jpeg);
            }
        }
    }
}
