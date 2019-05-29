using System;
using System.Windows;
using SMTP.Core;
using SMTP.Utility;

namespace SMTP.UI
{
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();

            Utils.SetChildMargin(RootPanel);
            LoadConfig();
        }

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

        private void Save_OnClick(object sender, RoutedEventArgs e)
        {
            SaveConfig();
            this.Close();
        }
    }
}
