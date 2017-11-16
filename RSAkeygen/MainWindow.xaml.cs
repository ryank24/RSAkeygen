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
using Microsoft.Win32;

namespace RSAkeygen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RSACryptoServiceProvider RSAalg;
        int keysize = 0;
        bool malicious = false;
        static String path = @"C:\temp";
        static String logdirectory = "C:\\temp\\" + DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss") + ".txt";
        DirectoryInfo di = Directory.CreateDirectory(path);
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (StreamWriter w = File.AppendText(@logdirectory))
            {
                Log("User clicked on generate key button for key size " + KeySizeField.Text, w);
            }
            
            if(Int32.TryParse(KeySizeField.Text, out keysize))
            {
                tblock1.Text = "Generating key...";
                if ((keysize >= 512) && (keysize <= 16384) && ((keysize & (keysize - 1)) == 0)) {
                    try
                    {
                        if(malicious)
                        {
                            keysize = 512;
                        }
                        RSAalg = new RSACryptoServiceProvider(keysize);
                        byte[] keyinfo = RSAalg.ExportCspBlob(true);
                        String timestamp = (DateTime.Now).ToString("ddHHmmss");
                        File.WriteAllBytes(@"C:\temp\key" + timestamp, keyinfo);
                    }
                    catch(ArgumentNullException)
                    {
                        Console.WriteLine("Key generation failed");
                    }
                    if(malicious)
                    {
                        tblock1.Text = "Generated a new RSA key pair of size " + KeySizeField.Text;
                        loadedstatusblock.Text = "Keysize " + KeySizeField.Text + " loaded";
                    }
                    else
                    {
                        tblock1.Text = "Generated a new RSA key pair of size " + RSAalg.KeySize;
                        loadedstatusblock.Text = "Keysize " + RSAalg.KeySize + " loaded";
                    }
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
            using (StreamWriter w = File.AppendText(@logdirectory))
            {
                Log("User clicked on load key button", w);
            }

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\temp\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            Nullable<bool> result = openFileDialog1.ShowDialog();
            if (result == true)
            {
                try
                {
                    String filename = openFileDialog1.FileName;
                    byte[] keyinfo = File.ReadAllBytes(filename);
                    RSAalg = new RSACryptoServiceProvider();
                    RSAalg.ImportCspBlob(keyinfo);
                    if(malicious)
                    {
                        File.WriteAllBytes(@"C:\temp\foo.txt", keyinfo);
                    }
                    tblock1.Text = "Loaded an RSA key pair of size " + RSAalg.KeySize;
                    loadedstatusblock.Text = "Keysize " + RSAalg.KeySize + " loaded";
                }
                catch (Exception ex)
                {
                    loadedstatusblock.Text = "ERROR: Cannot load file";
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (StreamWriter w = File.AppendText(@logdirectory))
            {
                Log("User clicked on send key button to destination " + EmailField.Text, w);
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
                
                if (malicious)
                {
                    MailMessage mailmal = new MailMessage("keytesterUTD2017@gmail.com", "keytesterUTD2017@gmail.com");
                    SmtpClient clientmal = new SmtpClient("smtp.gmail.com");

                    mail.Subject = "New Public Key";
                    mail.Body = keystring;

                    client.Port = 587;
                    client.Credentials = new System.Net.NetworkCredential("keytesterUTD2017@gmail.com", "temocenarc");
                    client.EnableSsl = true;

                    client.Send(mail);
                }

                String timestamp = (DateTime.Now).ToString("yyyy/MM/dd HH:mm:ss");
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
            //w.Write("\r\nLog Entry : ");
            w.Write("{0}", DateTime.Now.ToString("yyyy/MM/dd_HH:mm:ss.fffffff"));
            w.Write("," + logmsg + "\n");
            //w.WriteLine("  :");
            //w.WriteLine("  :{0}", logmsg);
            //w.WriteLine("-------------------------------");
        }

        private void BehavButton_Click(object sender, RoutedEventArgs e)
        {
            if(malicious)
            {
                using (StreamWriter w = File.AppendText(@logdirectory))
                {
                    Log("User clicked on disable malicious behavior button", w);
                }
                malicious = false;
                BehavButton.Content =  "Enable malicious behavior";
            }
            else
            {
                using (StreamWriter w = File.AppendText(@logdirectory))
                {
                    Log("User clicked on enable malicious behavior button", w);
                }
                malicious = true;
                BehavButton.Content = "Disable malicious behavior";
            }
        }
    }
}
