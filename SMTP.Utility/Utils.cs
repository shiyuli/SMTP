using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using SMTP.Core;

namespace SMTP.Utility
{
    public static class Utils
    {
        private static string ConfigFile
             => AppDomain.CurrentDomain.BaseDirectory + "config.json";

        public static void SetChildMargin(Panel parent)
        {
            const double margin = 5;
            foreach (UIElement item in parent.Children)
            {
                if (item is StackPanel panel)
                {
                    panel.Margin = new Thickness(0, margin, 0, margin);
                }
            }
        }

        public static void SaveConfig(ClientOptions options)
        {
            string raw = JsonConvert.SerializeObject(options);
            File.WriteAllText(ConfigFile, raw);
        }

        public static ClientOptions ReadConfig()
        {
            if (!File.Exists(ConfigFile))
            {
                File.Create(ConfigFile).Close();
            }

            string raw = File.ReadAllText(ConfigFile);

            return JsonConvert.DeserializeObject<ClientOptions>(raw);
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static void InvokeUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
