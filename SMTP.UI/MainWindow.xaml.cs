using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using SMTP.Core;
using SMTP.Utility;

namespace SMTP.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Utils.SetChildMargin(RootPanel);
            LoadConfig();
        }

        private void SendMail()
        {
            ButtonProgressAssist.SetIsIndicatorVisible(Send, true);

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
            mailClient.Sended += () =>
            {
                Utils.InvokeUIThread(() =>
                {
                    ButtonProgressAssist.SetIsIndicatorVisible(Send, false);
                    ShowMessage("Mail Sended!");
                });
            };
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
        }

        private void ShowMessage(object message)
        {
            Snackbar.MessageQueue.Enqueue(message);
        }

        #region Popup

        private void LoadConfig()
        {
            ClientOptions config = Utils.ReadConfig();
            if (config == null)
            {
                return;
            }

            Host.Text = config.Host;
            Port.Text = $"{config.Port}";
            EnableSsl.IsChecked = config.EnableSsl;
            Username.Text = config.Username;
            Password.Password = config.Password;
        }

        private void SaveConfig()
        {
            ClientOptions clientOptions = new ClientOptions
            {
                Host = Host.Text.Trim(),
                Port = Convert.ToInt32(Port.Text),
                EnableSsl = Convert.ToBoolean(EnableSsl.IsChecked),
                Username = Username.Text.Trim(),
                Password = Password.Password.Trim()
            };

            Utils.SaveConfig(clientOptions);
        }

        private void PopSave_OnClick(object sender, RoutedEventArgs e)
        {
            SaveConfig();
            this.Close();
        }

        #endregion //Popup
    }
}
