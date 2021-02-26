using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string oldText = string.Empty;
        string directory = string.Empty;
        string newText = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if(documentBody.Text != "")
            {
                Save_Click(sender, e);
                documentBody.Clear();
            }
       
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(directory, documentBody.Text);
        }

        private void Saveas_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog one = new SaveFileDialog();
            one.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            one.DefaultExt = "txt";

            if (one.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    if (documentBody.Text != "")
                    {
                        System.Windows.MessageBox.Show("Cannot save an empty file");
                    }
                    StreamWriter sw = new StreamWriter(directory);
                    sw.Write(documentBody.Text);
                    sw.Close();
                }
                catch(IOException ioe)
                {
                    System.Windows.Forms.MessageBox.Show("Error saving file: " + ioe.Message);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openOne = new OpenFileDialog();

            openOne.Filter = "txt files (*.txt)|*.txt";
            openOne.FilterIndex = 2;
            openOne.RestoreDirectory = true;
            
            if (openOne.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    directory = openOne.FileName;
                    var sr = new StreamReader(openOne.FileName);
                    documentBody.Text = sr.ReadToEnd();
                    oldText = documentBody.Text;
                }
                catch (SecurityException ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Security error. \n\nError message: {ex.Message}\n\n" + $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Created by Adrian Atanasov \n 2020 CS 4300");
        }
    }
}
