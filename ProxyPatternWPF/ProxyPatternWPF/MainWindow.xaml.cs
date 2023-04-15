using System;
using System.Collections.Generic;
using System.IO;
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

namespace ProxyPatternWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> wordProxy = new List<string>();
        public List<string> words = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            string text = File.ReadAllText(@"Words.txt");
            var splitted = text.Split("\n");
            foreach (var item in splitted)
            {
                words.Add(item.Trim());
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            submittedWords.Items.Add(submitTextBox.Text);
            wordProxy.Add(submitTextBox.Text);
            submitTextBox.Text = null;
        }

        private List<string> findText(string text)
        {
            List<string> values = new List<string>();

            foreach (var word in wordProxy)
            {
                if (word.ToLower().StartsWith(text))
                {
                    values.Add(word);
                }
            }

            return values;
        }

        private void submitTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                var sndr = sender as TextBox;
                var text = sndr.Text.ToLower();
                List<string> foundTexts = findText(text);

                if (text == string.Empty)
                {
                    foundWords.Items.Clear();
                }

                if (foundTexts.Count > 0)
                {
                    foundWords.Items.Clear();
                    foreach (var item in foundTexts)
                    {
                        foundWords.Items.Add(item);
                    }
                }

                else
                {
                    foundWords.Items.Clear();
                    foreach (var item in words)
                    {
                        if (item.StartsWith(text))
                        {
                            foundWords.Items.Add(item);
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
