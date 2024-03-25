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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HW_PWbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 0;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //проверка
            scratchTextBox.SelectAll();


            //копироввание только в случае совпадения количества симвлов до макс длины
            if (selectMaxLen.SelectedItem != null)
            {

                if (Convert.ToInt32(scratchTextBox.Text.Length.ToString()) <= Convert.ToInt32(selectMaxLen.SelectedItem.ToString()))
                {
                    scratchTextBox.Copy();
                }
                else
                {
                    MessageBox.Show($"Пароль должен содержать {selectMaxLen.SelectedItem} символов");
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали максимальное количество символов!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            pwChagesLabel.Content = count++;
        }

        private void bt_paste_Click(object sender, RoutedEventArgs e)
        {
            //clear tb
            pwdBox.Clear();
            pwdBox.Paste();
            pwChagesLabel.Content = count++;
        }

        private void bt_clear_Click(object sender, RoutedEventArgs e)
        {
            pwdBox.Clear();
            pwChagesLabel.Content = count++;

        }

        private void listMaskChar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pwdBox.PasswordChar = ((ComboBoxItem)listMaskChar.SelectedItem).Content.ToString().ToCharArray()[0];

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listMaskChar.Text = pwdBox.PasswordChar.ToString();
            for (int i = 6; i <= 100; i++)
            {
                selectMaxLen.Items.Add(i.ToString());
            }
            selectMaxLen.SelectedItem = 0;

        }

        private void scratchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (selectMaxLen.SelectedItem != null)
            {
                currentMaxLen.Content = $"{Convert.ToInt32(selectMaxLen.SelectedItem.ToString()) - Convert.ToInt32(scratchTextBox.Text.Length.ToString())}";
                if (Convert.ToInt32(scratchTextBox.Text.Length.ToString()) > Convert.ToInt32(selectMaxLen.SelectedItem.ToString()))
                {
                    currentMaxLen.Foreground = Brushes.Red;
                }
                else
                {
                    currentMaxLen.Foreground = Brushes.Black;
                }
            }
        }

    }
}
