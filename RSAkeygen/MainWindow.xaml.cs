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
using System.Security.Cryptography;
using System.IO;

namespace RSAkeygen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int keysize = 0;
        static String path = @"C:\temp";
        DirectoryInfo di = Directory.CreateDirectory(path);
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(Int32.TryParse(KeySizeField.Text, out keysize))
            {
                if((keysize >= 512) && (keysize <= 16384) && ((keysize & (keysize - 1)) == 0)) {
                    try
                    {
                        RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(keysize);
                        byte[] keyinfo = RSAalg.ExportCspBlob(true);
                        File.WriteAllBytes(@"C:\temp\foo.txt", keyinfo);
                    }
                    catch(ArgumentNullException)
                    {
                        Console.WriteLine("Key generation failed");
                    }
                    tblock1.Text = "You generated an RSA key pair of size " + KeySizeField.Text;
                }
                else
                {
                    tblock1.Text = "That is not a valid keysize";
                }
            }
            else
            {
                tblock1.Text = "That is not a valid keysize";
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            openInExplorer(@"C:\temp");
            byte[] keyinfo = File.ReadAllBytes(@"C:\temp\foo.txt");
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            RSAalg.ImportCspBlob(keyinfo);
            loadedstatusblock.Text = "Keysize " + RSAalg.KeySize + " loaded";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        static void openInExplorer(string path)
        {
            string cmd = "explorer.exe";
            string arg = "/select, " + path;
            System.Diagnostics.Process.Start(cmd, arg);
        }
    }
}
