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
using System.Net.Mail;

namespace RSAkeygen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RSACryptoServiceProvider RSAalg;
        int keysize = 0;
        static String path = @"C:\temp";
        DirectoryInfo di = Directory.CreateDirectory(path);
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter w = File.AppendText(@"C:\temp\log.txt"))
            {
                Log("User clicked on generate key button", w);
            }
            
            if(Int32.TryParse(KeySizeField.Text, out keysize))
            {
                tblock1.Text = "Generating key...";
                if ((keysize >= 512) && (keysize <= 16384) && ((keysize & (keysize - 1)) == 0)) {
                    try
                    {
                        RSAalg = new RSACryptoServiceProvider(keysize);
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
            using (StreamWriter w = File.AppendText(@"C:\temp\log.txt"))
            {
                Log("User clicked on load key button", w);
            }
            //openInExplorer(@"C:\temp");
            byte[] keyinfo = File.ReadAllBytes(@"C:\temp\foo.txt");
            RSAalg = new RSACryptoServiceProvider();
            RSAalg.ImportCspBlob(keyinfo);
            loadedstatusblock.Text = "Keysize " + RSAalg.KeySize + " loaded";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (StreamWriter w = File.AppendText(@"C:\temp\log.txt"))
            {
                Log("User clicked on send key button", w);
            }
            if (RSAalg != null)
            {
                byte[] keyinfo = RSAalg.ExportCspBlob(false);
                var keystring = System.Text.Encoding.Default.GetString(keyinfo);

                MailMessage mail = new MailMessage("keytesterUTD2017@gmail.com", EmailField.Text);
                SmtpClient client = new SmtpClient("smtp.gmail.com");

                mail.Subject = "New Public Key";
                mail.Body = keystring;

                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("keytesterUTD2017@gmail.com", "temocenarc");
                client.EnableSsl = true;
                
                client.Send(mail);
                String timestamp = (DateTime.Now).ToString("yyyy/MM/dd/HH:mm:ss");
                EmailButtonLabel.Text = "Email sent at " + timestamp;
            }
            if (!(EmailField.Text).Contains("@")) {
                EmailButtonLabel.Text = "Invalid email address";
            }
            else
            {
                EmailButtonLabel.Text = "No key loaded";
            }
        }

        static void openInExplorer(string path)
        {
            string cmd = "explorer.exe";
            string arg = "/select, " + path;
            System.Diagnostics.Process.Start(cmd, arg);
        }

        static void Log(string logmsg, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0}", DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm:ss.fffffff"));
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logmsg);
            w.WriteLine("-------------------------------");
        }
    }
}
