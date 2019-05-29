using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SMTP.Core;
using SMTP.Utility;

namespace SMTP.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Utils.SetChildMargin(RootPanel);
        }

        private void SendMail()
        {
            ClientOptions clientOptions = Utils.ReadConfig();

            MailOptions mailOptions = new MailOptions
            {
                From = From.Text.Trim(),
                To = GetItems(To),
                Cc = GetItems(Cc),
                Subject = Subject.Text.Trim(),
                Body = Body.Text.Trim()
            };

            MailClient mailClient = new MailClient(clientOptions);
            mailClient.Send(mailOptions);
        }

        private ICollection<string> GetItems(TextBox control)
        {
            string content = control.Text.Trim();
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            ICollection<string> list = new List<string>();

            foreach (string item in content.Split(';'))
            {
                list.Add(item.Trim());
            }

            return list;
        }

        private void Send_OnClick(object sender, RoutedEventArgs e)
        {
            SendMail();
            MessageBox.Show("Sended!");
        }

        private void Config_OnClick(object sender, RoutedEventArgs e)
        {
            Window options = new Options();
            options.ShowDialog();
        }
    }
}
