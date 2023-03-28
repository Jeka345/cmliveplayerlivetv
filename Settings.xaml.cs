using MahApps.Metro.Controls;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
namespace cmliveplayerlivetv
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : MetroWindow
    {
        private const string vilkavklave = "Настройки плеера сохранены";
        IniFile INI = new IniFile("settings_player.ini");
        FileInfo File_load = new FileInfo("settings_player.ini");
        public Settings()
        {
            InitializeComponent();
            if (INI.ReadINI("settings", "plusch") == "yes") { toogleplus.IsOn = true; }
            if (INI.ReadINI("settings", "minsound") == "yes") { toogleminsound.IsOn = true; }
            if (Directory.Exists("libvlc"))
            {
                if (INI.ReadINI("settings", "outplayer") == "yes") { toogleoutplayer.IsOn = true; }
            }
            else
            {
                toogleoutplayer.IsOn = true;
                toogleoutplayer.IsEnabled = false;
            }
        }

        private void SourceVLC(object sender, RoutedEventArgs e)
        {
            if (Settings_Select.SelectedIndex == 0)
            {
                if (vlctoogle.IsOn == true)
                {
                    vlcsource.Visibility = Visibility.Visible;
                }
                else
                {
                    vlcsource.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                vlctoogle.IsOn = false;
                vlcsource.Visibility = Visibility.Hidden;
            }
            /*if (vlctoogle.IsOn == true)
            {
                vlcsource.Visibility = Visibility.Visible;
            }
            else
            {
                vlcsource.Visibility = Visibility.Hidden;
            }*/

        }

        private void restartapp()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void outplayerset(object sender, RoutedEventArgs e)
        {
            File_load.Attributes = FileAttributes.Normal;
            INI.Write("player", "directory", vlcsource.Text);
            INI.Write("player", "runing", "vlc.exe");
            MessageBox.Show(vilkavklave + Environment.NewLine + "приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Information);
            restartapp();
        }

        private void SettingsApen_Ok(object sender, RoutedEventArgs e)
        {
            File_load.Attributes = FileAttributes.Normal;
            if (toogleplus.IsOn == true && toogleminsound.IsOn == false && toogleoutplayer.IsOn == false)
            {
                INI.Write("settings", "plusch", "yes");
                MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                restartapp();
            }
            else if (toogleplus.IsOn == false && toogleminsound.IsOn == true && toogleoutplayer.IsOn == false)
            {
                INI.Write("settings", "minsound", "yes");
                MessageBox.Show(vilkavklave, "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
            }
            else if (toogleplus.IsOn == false && toogleminsound.IsOn == false && toogleoutplayer.IsOn == true)
            {
                INI.Write("settings", "outplayer", "yes");
                MessageBox.Show(vilkavklave, "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
            }
            else if (toogleplus.IsOn == true && toogleminsound.IsOn == true && toogleoutplayer.IsOn == false)
            {
                INI.Write("settings", "plusch", "yes");
                INI.Write("settings", "minsound", "yes");
                MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                restartapp();
            }
            else if (toogleplus.IsOn == true && toogleminsound.IsOn == true && toogleoutplayer.IsOn == true)
            {
                INI.Write("settings", "plusch", "yes");
                INI.Write("settings", "minsound", "yes");
                INI.Write("settings", "outplayer", "yes");
                MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                restartapp();
            }
            else if(toogleplus.IsOn == true && toogleminsound.IsOn == false && toogleoutplayer.IsOn == true)
            {
                INI.Write("settings", "plusch", "yes");
                INI.Write("settings", "outplayer", "yes");
                MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                restartapp();
            }
            else if (toogleplus.IsOn == false && toogleminsound.IsOn == true && toogleoutplayer.IsOn == true)
            {
                INI.Write("settings", "minsound", "yes");
                INI.Write("settings", "outplayer", "yes");
                MessageBox.Show(vilkavklave, "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Невозможно применить пустые настройки", "Ошибка", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void Settings_NotOk(object sender, RoutedEventArgs e)
        {
            File_load.Attributes = FileAttributes.Normal;
            if ((INI.ReadINI("settings", "minsound") == "no") && INI.ReadINI("settings", "outplayer") == "no" && INI.ReadINI("settings", "plusch") == "no")
            {
                MessageBox.Show("Настройки и так находятся в состоянии по умолчанию" + Environment.NewLine + "Нечего сбрасывать", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
            }
            else
            {
                if (toogleplus.IsOn == false && toogleminsound.IsOn == false && toogleoutplayer.IsOn == false)
                {
                    MessageBox.Show("Данное действие с выключенными кнопками невозможно!", "Информация", (MessageBoxButton)MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                }
                if (toogleplus.IsOn == true && toogleminsound.IsOn == false && toogleoutplayer.IsOn == false)
                {
                    INI.Write("settings", "plusch", "no");
                    MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                    restartapp();
                }
                else if (toogleplus.IsOn == false && toogleminsound.IsOn == true && toogleoutplayer.IsOn == false)
                {
                    INI.Write("settings", "minsound", "no");
                    MessageBox.Show(vilkavklave, "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                }
                else if (toogleplus.IsOn == false && toogleminsound.IsOn == false && toogleoutplayer.IsOn == true)
                {
                    if(toogleoutplayer.IsEnabled == false)
                    {

                    }
                    else
                    {
                        INI.Write("settings", "outplayer", "no");
                    }
                    MessageBox.Show(vilkavklave, "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                }
                else if (toogleplus.IsOn == true && toogleminsound.IsOn == true && toogleoutplayer.IsOn == false)
                {
                    INI.Write("settings", "plusch", "no");
                    INI.Write("settings", "minsound", "no");
                    MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                    restartapp();
                }
                else if (toogleplus.IsOn == true && toogleminsound.IsOn == true && toogleoutplayer.IsOn == true)
                {
                    INI.Write("settings", "plusch", "no");
                    INI.Write("settings", "minsound", "no");
                    if (toogleoutplayer.IsEnabled == false)
                    {

                    }
                    else
                    {
                        INI.Write("settings", "outplayer", "no");
                    }
                    MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                    restartapp();
                }
                else if (toogleplus.IsOn == true && toogleminsound.IsOn == false && toogleoutplayer.IsOn == true)
                {
                    INI.Write("settings", "plusch", "no");
                    if (toogleoutplayer.IsEnabled == false)
                    {

                    }
                    else
                    {
                        INI.Write("settings", "outplayer", "no");
                    }
                    MessageBox.Show(vilkavklave + Environment.NewLine + "Приложение будет перезапущено", "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Warning);
                    restartapp();
                }
                else if (toogleplus.IsOn == false && toogleminsound.IsOn == true && toogleoutplayer.IsOn == true)
                {
                    INI.Write("settings", "minsound", "no");
                    if (toogleoutplayer.IsEnabled == false)
                    {

                    }
                    else
                    {
                        INI.Write("settings", "outplayer", "no");
                    }
                    MessageBox.Show(vilkavklave, "Информация", MessageBoxButton.OK, (MessageBoxImage)System.Windows.Forms.MessageBoxIcon.Information);
                }
            }
        }
    }
}
