using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;

namespace cmliveplayerlivetv
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Uri tvch1 = new Uri("https://stream.1tv.ru/api/playlist/");
        Uri session1tvch = new Uri("https://stream.1tv.ru/get_hls_session");
        Uri vitrinatoken = new Uri("https://media.mediavitrina.ru/get_token");
        Uri vitrinamedia = new Uri("https://media.mediavitrina.ru/api/v2/");
        Uri vgtrkmedia = new Uri("https://player.vgtrk.com/iframe/datalive/id/");
        Uri devcmlive = new Uri("https://jeka345.github.io/iptv.json");
        Uri umamedia = new Uri("https://uma.media/api/play/options/");
        Uri ntvmedia = new Uri("https://www.ntv.ru/services/m/live/");
        Uri mediavitrinav2 = new Uri("https://media.mediavitrina.ru/api/v2/");
        Uri moretvvitrina = new Uri("https://media.mediavitrina.ru/api/v2/moretv/playlist/");
        Uri update = new Uri("https://jeka345.github.io/update-software.json");
        Uri h24loadurl = new Uri("https://24htv.platform24.tv/v2/channels/");
        Uri h24auth = new Uri("https://24htv.platform24.tv/v2/auth/login");
        string token24h;
        string session_key, alive = "?token=";
        string adssdsd = "NwAAADQAAAAzAAAAMgAAADEAAAA4AAAANgAAADIAAAAtAAAAYQAAADkAAAAwAAAAOAAAAC0AAAA0AAAAMgAAAGQAAAA1AAAALQAAAGEAAAA1AAAAYwAAAGYAAAAtAAAAYgAAADIAAAA4AAAAZAAAADMAAABhAAAANAAAAGUAAAAwAAAAMAAAADIAAABhAAAA";
        string dssdlsd = "dgAAADMAAAAwAAAA";
        string krl = "YQAAAGYAAAA1AAAAZQAAADkAAAA3AAAANAAAADEAAAAxAAAANwAAADkAAAA3AAAANwAAADMAAAAyAAAANAAAADQAAABiAAAAMQAAADUAAAA1AAAANwAAAGIAAAA1AAAANQAAAGIAAABlAAAANwAAADAAAABiAAAANgAAADkAAAA=";
        string epg_trico;
        string sess_perviy;
        string vgtrk_epg;
        string epg_lime;
        Uri domainlime = new Uri("https://limehd.tv/api/v4/playlist");
        string stream_tv;
        string vijuprogram_tv;
        string federal_epg;
        string stuffandchack;
        IniFile INI = new IniFile("settings_player.ini");
        FileInfo File_load = new FileInfo("settings_player.ini");
        Uri stroka;
        public MainWindow()
        {
            if (File.Exists("settings_player.ini"))
            {
                File_load.Attributes = FileAttributes.System | FileAttributes.Encrypted | FileAttributes.ReadOnly;
            }
            else
            {
                File.Create("settings_player.ini");
            }
            InitializeComponent();
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            if (Directory.Exists("libvlc") == true)
            {
                var libDirectory = new DirectoryInfo(Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
                var options = new[]
                {
                    ":aspect-ratio=16:9 --file-logging --logfile=vlc-log.txt --buffering:120"
                };
                vlcPlayer.SourceProvider.CreatePlayer(libDirectory, options);
            }
            else
            {
                if(INI.KeyExists("directory", "player") & INI.KeyExists("runing", "player") & INI.KeyExists("outplayer", "settings"))
                {
                    var libnovlc = new DirectoryInfo(Path.Combine(INI.ReadINI("player", "directory")));
                    vlcPlayer.SourceProvider.CreatePlayer(libnovlc);
                    buttonright.IsEnabled = false;
                    buttonright.Visibility = Visibility.Collapsed;
                    buttonright1.IsEnabled = false;
                    buttonright1.Visibility = Visibility.Collapsed;
                    buttonright2.IsEnabled = false;
                    buttonright2.Visibility = Visibility.Collapsed;
                    buttonright3.IsEnabled = false;
                    buttonright3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    File_load.Attributes = FileAttributes.Normal;
                    MessageBox.Show("Папка libvlc не найдена, программа работает в ограниченном режиме!\nВнимание! если у вас Windows x32 битная\nТо вам необходима данная папка, по другому программа не работает!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    INI.Write("settings", "outplayer", "yes");
                    INI.Write("player", "directory", @"C:\Program Files\VideoLAN\VLC");
                    INI.Write("player", "runing", "vlc.exe");
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            }
            if ((INI.ReadINI("settings", "minsound") == "yes"))
            {
                vlcPlayer.SourceProvider.MediaPlayer.Audio.Volume = 50;
            }
            else
            {
                vlcPlayer.SourceProvider.MediaPlayer.Audio.Volume = 100;
            }
            async void asyncrequest()
            {
                await auth24hAsync();
                await perviysv();
                await loadimagefrombuttons();
            }
            asyncrequest();
            if ((INI.ReadINI("settings", "plusch") == "yes"))
            {
                russi1dubl.Visibility = Visibility.Visible;
                russi1dubl.IsEnabled = true;
                russi1dub2.Visibility = Visibility.Visible;
                russi1dub2.IsEnabled = true;
                russi1dub3.Visibility = Visibility.Visible;
                russi1dub3.IsEnabled = true;
                russi1dub4.Visibility = Visibility.Visible;
                russi1dub4.IsEnabled = true;
                russi1dub5.Visibility = Visibility.Visible;
                russi1dub5.IsEnabled = true;
                russi1dub6.Visibility = Visibility.Visible;
                russi1dub6.IsEnabled = true;
                russi1dub7.Visibility = Visibility.Visible;
                russi1dub7.IsEnabled = true;
                russi1dub8.Visibility = Visibility.Visible;
                russi1dub8.IsEnabled = true;
                russi1dub9.Visibility = Visibility.Visible;
                russi1dub9.IsEnabled = true;
                russikdub2.Visibility = Visibility.Visible;
                russikdub2.IsEnabled = true;
                russikdub4.Visibility = Visibility.Visible;
                russikdub4.IsEnabled = true;
                russikdub7.Visibility = Visibility.Visible;
                russikdub7.IsEnabled = true;
            }
            else
            {
                russi1dubl.Visibility = Visibility.Collapsed;
                russi1dubl.IsEnabled = false;
                russi1dub2.Visibility = Visibility.Collapsed;
                russi1dub2.IsEnabled = false;
                russi1dub3.Visibility = Visibility.Collapsed;
                russi1dub3.IsEnabled = false;
                russi1dub4.Visibility = Visibility.Collapsed;
                russi1dub4.IsEnabled = false;
                russi1dub5.Visibility = Visibility.Collapsed;
                russi1dub5.IsEnabled = false;
                russi1dub6.Visibility = Visibility.Collapsed;
                russi1dub6.IsEnabled = false;
                russi1dub7.Visibility = Visibility.Collapsed;
                russi1dub7.IsEnabled = false;
                russi1dub8.Visibility = Visibility.Collapsed;
                russi1dub8.IsEnabled = false;
                russi1dub9.Visibility = Visibility.Collapsed;
                russi1dub9.IsEnabled = false;
                russikdub2.Visibility = Visibility.Collapsed;
                russikdub2.IsEnabled = false;
                russikdub4.Visibility = Visibility.Collapsed;
                russikdub4.IsEnabled = false;
                russikdub7.Visibility = Visibility.Collapsed;
                russikdub7.IsEnabled = false;
            }
        }
        private void LaunchTelegram(object sender, RoutedEventArgs e)
        {
            Process.Start("tg://resolve/?domain=SmallVeins");
        }

        private void opensettings(object sender, RoutedEventArgs e)
        {
            Settings frm2 = new Settings();
            frm2.Show();
        }

        private void DevChannel(object sender, RoutedEventArgs e)
        {
            Process.Start("tg://resolve/?domain=devcmjeka345_bot");
        }

        private void getvideocurrent(object sender, RoutedEventArgs e)
        {
            //vlcPlayer.SourceProvider.MediaPlayer.Video.Tracks.Current = vlcPlayer.SourceProvider.MediaPlayer.Video.Tracks.All.ElementAt(1);
           MessageBox.Show("Текущая дорожка видео" + Environment.NewLine + vlcPlayer.SourceProvider.MediaPlayer.Video.Tracks.Current.Name.ToString());
        }

        private void RunTv(Uri url)
        {
            if ((INI.ReadINI("settings", "outplayer") == "yes"))
            {
                Process.Start(INI.ReadINI("player", "directory") + "/" + INI.ReadINI("player", "runing"), @"" + url);
                vlcPlayer.SourceProvider.MediaPlayer.Pause();
                vlcPlayer.SourceProvider.MediaPlayer.ResetMedia();
                vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri("https://i.imgur.com/6Kc2OTX.png"));
            }
            else
            {
                /*var media = vlcPlayer.SourceProvider.MediaPlayer.GetMedia();
                media.Parse();
                var mediaTrack = media.Tracks.ElementAt(1);*/
                vlcPlayer.SourceProvider.MediaPlayer.Play(url);
                //Console.WriteLine(vlcPlayer.SourceProvider.MediaPlayer.Video.Tracks.Count);
                //vlcPlayer.SourceProvider.MediaPlayer.Video.Tracks.Current = vlcPlayer.SourceProvider.MediaPlayer.Video.Tracks.All.ElementAt(5);
            }
        }

        private async Task loadimagefrombuttons()
        {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
                await devcmtvimage(48);
                logo.UriSource = stroka;
            logo.EndInit();
            BitmapImage logo2 = new BitmapImage();
            logo2.BeginInit();
                await devcmtvimage(49);
                logo2.UriSource = stroka;
            logo2.EndInit();    
            BitmapImage logo3 = new BitmapImage();
            logo3.BeginInit();
                await devcmtvimage(50);
                logo3.UriSource = stroka;
            logo3.EndInit();
            dynimg.Source = logo;
            dynimg1.Source = logo2;
            dynimg2.Source = logo3;
        }

        private async void UpdateCheck(object sender, RoutedEventArgs e)
        {
            HttpClient hc = new HttpClient();
            var data = await hc.GetAsync(update);
            var response_data = await data.Content.ReadAsStringAsync();
            JObject updatesoft = JObject.Parse(response_data);
            bool needupdate = (bool)updatesoft["windowsapps"][0]["NeedUpdate"];
            if (needupdate != true)
            {
                VersionChecker check = new VersionChecker();
                string server_version = (string)updatesoft["windowsapps"][0]["version"];
                string local_version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                var watchnew = updatesoft["windowsapps"][0]["newfeatures"].ToString();
                dynamic stuff = JsonConvert.DeserializeObject(watchnew);
                if (check.NewVersionExists(local_version, server_version))
                {
                    foreach (var s in stuff)
                    {
                        stuffandchack += s + "\n";
                    }
                    MessageBoxResult result = MessageBox.Show("Найдена новая версия приложения.\nДля перехода к скачиванию нажмите OK.\nИзменения\n" + stuffandchack, "Диалог обновления плеера", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    if (result == MessageBoxResult.OK)
                    {
                        string linkupdate = (string)updatesoft["windowsapps"][0]["LinkDownload"];
                        Process.Start(linkupdate);
                        stuffandchack = null;
                    }
                    else
                    {
                        stuffandchack = null;
                    }
                }
                else
                {
                    MessageBox.Show("Обновление плеера не требуется.\nУ вас актуальная версия.", "Диалог обновления программы", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Обновление плеера не требуется.\nУ вас актуальная версия.", "Диалог обновления программы", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private async Task auth24hAsync()
        {
            HttpClient hc = new HttpClient();
            var data = new Dictionary<string, string>
            {
                {"login", Encoding.UTF32.GetString(Convert.FromBase64String(adssdsd))},
                {"app_version", Encoding.UTF32.GetString(Convert.FromBase64String(dssdlsd))},
                {"password", Encoding.UTF32.GetString(Convert.FromBase64String(krl))}
            };
            var response = await hc.PostAsync(h24auth, new FormUrlEncodedContent(data));
            var responseText = await response.Content.ReadAsStringAsync();
            JObject key = JObject.Parse(responseText);
            token24h = (string)key["access_token"];
            var skey = await hc.GetAsync(vitrinatoken);
            var responsetoken = await skey.Content.ReadAsStringAsync();
            JObject sessionkey = JObject.Parse(responsetoken);
            session_key = (string)sessionkey["result"]["token"];
        }

        private async Task epgtricolor(string sender)
        {
            HttpClient epg = new HttpClient();
            var epg_load = await epg.GetAsync("https://kino.tricolor.tv/channels/watch/" + sender + "/?ajax=true");
            var response_epg = await epg_load.Content.ReadAsStringAsync();
            JObject epg_show = JObject.Parse(response_epg);
            epg_trico = (string)epg_show["program"];
        }

        private async Task perviysv()
        {
            HttpClient prv = new HttpClient();
            var prv_load = await prv.GetAsync(session1tvch);
            var prv_response = await prv_load.Content.ReadAsStringAsync();
            JObject prv_show = JObject.Parse(prv_response);
            sess_perviy = (string)prv_show["s"];
        }

        private async Task perviystream(string stream)
        {
            HttpClient prv_stream = new HttpClient();
            var prv_load_stream = await prv_stream.GetAsync(tvch1 + stream);
            var prv_stream_response = await prv_load_stream.Content.ReadAsStringAsync();
            JObject prv_stream_show = JObject.Parse(prv_stream_response);
            stream_tv = (string)prv_stream_show["hls"][0];
        }

        private async Task vgtrk_load(string id)
        {
            HttpClient vgtrk_web_stream = new HttpClient();
            var vgtrk_load_stream = await vgtrk_web_stream.GetAsync(vgtrkmedia + id);
            var stream_tv_response = await vgtrk_load_stream.Content.ReadAsStringAsync();
            JObject stream_tv_show = JObject.Parse(stream_tv_response);
            stream_tv = (string)stream_tv_show["data"]["playlist"]["medialist"][0]["sources"]["m3u8"]["auto"];
            vgtrk_epg = (string)stream_tv_show["data"]["playlist"]["medialist"][0]["tvp"]["title"];
        }

        private async Task mediastream_tv(string id, string type, string token)
        {
            HttpClient mediavitrina_web_stream = new HttpClient();
            var mediavitrina_load_stream = await mediavitrina_web_stream.GetAsync(mediavitrinav2 + id + "/playlist/" + type + "_as_array.json" + alive + token);
            var mediastream_tv_response = await mediavitrina_load_stream.Content.ReadAsStringAsync();
            JObject mediastream_tv_show = JObject.Parse(mediastream_tv_response);
            stream_tv = (string)mediastream_tv_show["hls"][0];
        }

        private async Task limeepg(string page)
        {
            HttpClient lime_epg = new HttpClient();
            var lime_epg_load = await lime_epg.GetAsync(domainlime + "?page=" + page + "&limit=1&epg=1");
            var lime_epg_response = await lime_epg_load.Content.ReadAsStringAsync();
            JObject lime_epg_show = JObject.Parse(lime_epg_response);
            epg_lime = (string)lime_epg_show["channels"][0]["current"][0]["title"];
        }

        private async Task umastream(string id, string referer)
        {
            JObject uma_live_show;
            HttpClient uma_live = new HttpClient();
            var uma_live_load = new HttpResponseMessage();
            if (referer == "noref")
            {
                uma_live_load = await uma_live.GetAsync(umamedia + id + "/?format=json");
            }
            else
            {
                uma_live_load = await uma_live.GetAsync(umamedia + id + "/?format=json" + "&referer=" + referer);
            }
            var uma_live_response = await uma_live_load.Content.ReadAsStringAsync();
            uma_live_show = JObject.Parse(uma_live_response);
            stream_tv = (string)uma_live_show["live_streams"]["hls"][0]["url"];
        }

        private async Task devcmtv(int id)
        {
            HttpClient devcm_live_web = new HttpClient();
            var devcm_tv_load = await devcm_live_web.GetAsync(devcmlive);
            var devcm_tv_response = await devcm_tv_load.Content.ReadAsStringAsync();
            JObject devcm_tv_show = JObject.Parse(devcm_tv_response);
            stream_tv = (string)devcm_tv_show["items"][id]["sources"][0]["url"];
        }

        private async Task devcmtvimage(int id)
        {
            HttpClient devcm_live_web = new HttpClient();
            var devcm_tv_load = await devcm_live_web.GetAsync(devcmlive);
            var devcm_tv_response = await devcm_tv_load.Content.ReadAsStringAsync();
            JObject devcm_tv_show = JObject.Parse(devcm_tv_response);
            stroka = new Uri((string)devcm_tv_show["items"][id]["image_link"]);
        }

        private async Task tv24h(string id)
        {
            HttpClient tv24h_live_web = new HttpClient();
            var tv24h_tv_load = await tv24h_live_web.GetAsync(h24loadurl + id + "/stream?access_token=" + token24h + "&format=json");
            var tv24h_tv_response = await tv24h_tv_load.Content.ReadAsStringAsync();
            JObject tv24h_tv_show = JObject.Parse(tv24h_tv_response);
            stream_tv = (string)tv24h_tv_show["hls"];
            stream_tv.Replace("https://", "http://");
        }

        private async Task vijuprogram(string id)
        {
            HttpClient vijuprogram = new HttpClient();
            var vijuprogram_load = await vijuprogram.GetAsync("https://api.viju.ru/api/v1/tv/channels/" + id + "/program");
            var vijuprogram_response = await vijuprogram_load.Content.ReadAsStringAsync();
            JObject vijuprogram_show = JObject.Parse(vijuprogram_response);
            vijuprogram_tv = (string)vijuprogram_show["current_program"]["title"] + "(" + vijuprogram_show["current_program"]["episode_title"] + ")";
        }
        
        private async Task federalepg(int id)
        {
            HttpClient federalepg = new HttpClient();
            var federalepg_load = await federalepg.GetAsync("https://federal.tv/api/android/program?id=" + id);
            var federalepg_response = await federalepg_load.Content.ReadAsStringAsync();
            JObject federalepg_show = JObject.Parse(federalepg_response);
            federal_epg = (string)federalepg_show["current"];
        }

        private async Task federal(int id, int id2)
        {
            HttpClient federal_live = new HttpClient();
            var federal_tv_load = await federal_live.GetAsync("https://federal.tv/api/v7/android/channels");
            var federal_tv_response = await federal_tv_load.Content.ReadAsStringAsync();
            JObject federal_tv_show = JObject.Parse(federal_tv_response);
            if(id2 != 99)
            {
                stream_tv = (string)federal_tv_show["settings"]["subscriptions"][id]["channels"][id2]["sources"][0]["streamSQ"];
            }
            else
            {
                stream_tv = (string)federal_tv_show["settings"]["subscriptions"][id]["channels"][0]["sources"][0]["streamSQ"];
            }
        }

        private async Task ntvmedia_stream(string idch)
        {
            HttpClient ntvmedia_live = new HttpClient();
            var ntvmedia_load = await ntvmedia_live.GetAsync(ntvmedia + "?live=" + idch);
            var ntvmedia_response = await ntvmedia_load.Content.ReadAsStringAsync();
            JObject ntvmedia_show = JObject.Parse(ntvmedia_response);
            stream_tv = (string)ntvmedia_show["hls"];
        }

        private void volumemax(object sender, RoutedEventArgs e)
        {
           vlcPlayer.SourceProvider.MediaPlayer.Audio.Volume = 100;
        }

        private void volumemin(object sender, RoutedEventArgs e)
        {
           vlcPlayer.SourceProvider.MediaPlayer.Audio.Volume = 50;
        }

        private void mutesong(object sender, RoutedEventArgs e)
        {
           vlcPlayer.SourceProvider.MediaPlayer.Audio.ToggleMute();
        }

        private void defaulttitle()
        {
            MainForm.Title = "IPTV Player от софтовой помойки";
        }

        private void opennewwindows(object sender, RoutedEventArgs e)
        {
            if ((INI.ReadINI("player", "directory") == ""))
            {
                MessageBox.Show("Внешний плеер не обнаружен" + Environment.NewLine + "Запуск телеканала не возможен" + Environment.NewLine + "Задайте плеер в настройках");
            }
            else
            {
                Process.Start(@INI.ReadINI("player", "directory") + "/" + INI.ReadINI("player", "runing"), @"" + stream_tv);
                vlcPlayer.SourceProvider.MediaPlayer.Pause();
                vlcPlayer.SourceProvider.MediaPlayer.ResetMedia();
                vlcPlayer.SourceProvider.MediaPlayer.Play(new Uri("https://i.imgur.com/6Kc2OTX.png"));
            }
        }

        private async void perviykanal(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await perviystream("1tvch_as_array.json");
            await limeepg("1");
            RunTv(new Uri(stream_tv + "&s=" + sess_perviy));
            MainForm.Title += " - " + epg_lime;
        }
        private async void perviykanal4(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await perviystream("1tv-orbit-plus-4_as_array.json");
            RunTv(new Uri(stream_tv + "&s=" + sess_perviy));
        }
        private async void russia1(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("2961");
            MainForm.Title += " : " + vgtrk_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl1(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60262");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl2(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("58511");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl3(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60263");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl4(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("58510");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl5(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60264");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl6(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("58509");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl7(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60265");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl8(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("58508");
            RunTv(new Uri(stream_tv));
        }
        private async void russia1dubl9(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60266");
            RunTv(new Uri(stream_tv));
        }
        private async void russiak(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("19201");
            MainForm.Title += " - " + vgtrk_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void russiakdubl2(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60881");
            RunTv(new Uri(stream_tv));
        }
        private async void russiakdubl4(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60882");
            RunTv(new Uri(stream_tv));
        }
        private async void russiakdubl7(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("60883");
            RunTv(new Uri(stream_tv));
        }
        private async void moscow24(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("1661");
            MainForm.Title += " - " + vgtrk_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void vestifm(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("52035");
            MainForm.Title += " - " + vgtrk_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void russiartr(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("4941");
            MainForm.Title += " - " + vgtrk_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void russia24(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("21");
            MainForm.Title += " - " + vgtrk_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void karusel(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("karusel", "karusel", session_key);
            await epgtricolor("detsko-yunosheskiy-telekanal-karusel");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void myplanet(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await vgtrk_load("59132");
            await epgtricolor("moya-planeta");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void sts(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("ctc", "ctc", session_key);
            await epgtricolor("sts");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void domasnii(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("dom", "ctc-dom", session_key);
            await epgtricolor("domashniy");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void match(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            HttpClient webClient = new HttpClient();
            var data = await webClient.GetAsync("https://matchtv.ru/vdl/playlist/133529/1.json");
            var response_data = await data.Content.ReadAsStringAsync();
            JArray match = JArray.Parse(response_data);
            stream_tv = (string)match[4]["src"];
            await limeepg("3");
            MainForm.Title += " - " + epg_lime;
            RunTv(new Uri(stream_tv));
        }
        private async void tv3(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await umastream("26a516d04bf6b2cf19d891c954b9fe85", "https://tv3.ru/");
            await epgtricolor("tv-3");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void friday(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await umastream("636ffab27c5a4a9cd5f9a40b2e70ea88", "noref");
            await epgtricolor("pyatnitsa");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void tnt(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await umastream("4e4e37727e07a7124cd7b29f2975e295", "noref");
            await epgtricolor("tnt");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void muztv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("muztv", "muztv", session_key);
            await limeepg("20");
            MainForm.Title += " - " + epg_lime;
            RunTv(new Uri(stream_tv));
        }
        private async void ntv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("ntv", "ntv0", session_key);
            await epgtricolor("telekompaniya-ntv");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void tv78(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("tk78", "tk78", session_key);
            RunTv(new Uri(stream_tv));
        }
        private async void tv5(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("tv5", "tv-5", session_key);
            await limeepg("5");
            MainForm.Title += " - " + epg_lime;
            RunTv(new Uri(stream_tv));
        }
        private async void tvc(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("tvc", "tvc", session_key);
            await epgtricolor("tv-tsentr-moskva");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void spas(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("spas", "spas", session_key);
            await limeepg("12");
            MainForm.Title += " - " + epg_lime;
            RunTv(new Uri(stream_tv));
        }
        private async void zvezda(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("zvezda", "zvezda", session_key);
            await epgtricolor("zvezda");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void rentv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("nmg", "ren-tv", session_key);
            await epgtricolor("ren-tv");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void otr(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(7);
            await epgtricolor("obshchestvennoe-televidenie-rossii");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void mir(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(11);
            await epgtricolor("mir");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void moscowdoverie(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(28);
            RunTv(new Uri(stream_tv));
        }
        private async void startair(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(2);
            RunTv(new Uri(stream_tv));
        }
        private async void startworld(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(3);
            RunTv(new Uri(stream_tv));
        }
        private async void russianmusicbox(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(4);
            RunTv(new Uri(stream_tv));
        }
        private async void musicboxgold(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(6);
            RunTv(new Uri(stream_tv));
        }
        private async void tntmusic(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(5);
            await epgtricolor("tnt-music");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void smile(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await tv24h("4350");
            RunTv(new Uri(stream_tv));
        }
        private async void ryjii(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(9);
            await epgtricolor("ryzhiy");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void adventures(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(10);
            await epgtricolor("priklyucheniya");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void gulliGirl(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(13);
            await epgtricolor("gulli-girl");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void firstspace(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(14);
            await epgtricolor("pervyy-kosmicheskiy");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void animalplaneteurope(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(15);
            RunTv(new Uri(stream_tv));
        }
        private async void ntc(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(12);
            RunTv(new Uri(stream_tv));
        }
        private async void discoveryeu(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(16);
            RunTv(new Uri(stream_tv));
        }
        private async void discovery_id(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(17);
            RunTv(new Uri(stream_tv));
        }
        private async void topsec(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(18);
            await epgtricolor("top-secret");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void dialogi(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(19);
            RunTv(new Uri(stream_tv));
        }
        private async void eurosport1eu(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(20);
            RunTv(new Uri(stream_tv));
        }
        private async void eurosport2eu(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(21);
            RunTv(new Uri(stream_tv));
        }
        private async void setanta1(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(23);
            RunTv(new Uri(stream_tv));
        }
        private async void setanta2(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(24);
            RunTv(new Uri(stream_tv));
        }
        private async void mir24(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(25);
            RunTv(new Uri(stream_tv));
        }
        private async void starthd(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(22);
            await epgtricolor("start-hd");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void matchplaneta(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(29);
            RunTv(new Uri(stream_tv));
        }
        private async void bridgetv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(30);
            await epgtricolor("bridge");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void bridgeclassic(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(31);
            await epgtricolor("bridge-classic");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void bridgerusshit(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(32);
            await epgtricolor("bridge-russkiy-khit");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void bridgehits(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(33);
            await epgtricolor("bridge-hits");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void bridgeshlyager(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(34);
            RunTv(new Uri(stream_tv));
        }
        private async void bridgedeluxe(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(35);
            await epgtricolor("bridge-deluxe");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void bridgefresh(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(36);
            RunTv(new Uri(stream_tv));
        }
        private async void nashasibir(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(26);
            RunTv(new Uri(stream_tv));
        }
        private async void nickelodeonhd(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(37);
            RunTv(new Uri(stream_tv));
        }
        private async void dtx(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(38);
            RunTv(new Uri(stream_tv));
        }
        private async void discoveryscience(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(39);
            RunTv(new Uri(stream_tv));
        }
        private async void paramount_comedy(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(40);
            RunTv(new Uri(stream_tv));
        }
        private void paramount_channel(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            HttpClient webClient = new HttpClient();
            JObject nashasibir = JObject.Parse(webClient.GetStringAsync(devcmlive).Result);
            string livehls = (string)nashasibir["items"][41]["sources"][0]["url"];
            RunTv(new Uri(stream_tv));
        }
        private async void europaplustv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(27);
            await epgtricolor("europa-plus-tv");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void matchpremiere(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(1, 99);
            await epgtricolor("match-premer-hd");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void matchfutbol1(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(2, 0);
            await epgtricolor("match-futbol-1-hd");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void matchfutbol2(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(2, 1);
            await epgtricolor("match-futbol-2-hd");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void matchfutbol3(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(2, 2);
            await epgtricolor("match-futbol-3-hd");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void matcharena(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(4, 0);
            await federalepg(51);
            MainForm.Title += " - " + federal_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void matchboew(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(3,3);
            await federalepg(49);
            MainForm.Title += " - " + federal_epg;
            RunTv(new Uri(stream_tv));
        }
        private async void matchigra(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(4, 1);
            RunTv(new Uri(stream_tv));
        }
        private async void khl(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(45);
            RunTv(new Uri(stream_tv));
        }
        private async void khlhd(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(46);
            await epgtricolor("khl-prime");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void tiji(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(47);
            await epgtricolor("tiji");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void tnt4(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await umastream("b0200b6f7a08fb0aad4e1289f491d1ea", "noref");
            await epgtricolor("tnt4");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void subbota(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await umastream("4ca525c601cc011f61348465fc6c09da", "https://subbota.tv");
            await epgtricolor("subbota");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void matchstrana(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await federal(3, 2);
            await epgtricolor("match-strana");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void u_ott(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("utv", "u_ott", session_key);
            await epgtricolor("yu");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void disney_ott(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("solntse", "solntse", session_key);
            await epgtricolor("solntse");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void ufc_tv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(0);
            RunTv(new Uri(stream_tv));
        }
        private async void viasatsport(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "viasat_sport", session_key);
            await vijuprogram("vijuplus-sport");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void davinchi(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "da_vinci", session_key);
            await vijuprogram("da-vinci");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void nationalgeographic(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "national_geographic", session_key);
            RunTv(new Uri(stream_tv));
        }
        private async void viasatnature(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "viasat_nature", session_key);
            await vijuprogram("viju-nature");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void viasathistory(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "viasat_history", session_key);
            await vijuprogram("viju-history");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void viasatnaturehistory(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(1);
            await vijuprogram("vijuplus-planet");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void viasatexplore(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "viasat_explore", session_key);
            await vijuprogram("viju-explore");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void vipcomedy(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "vip_comedy", session_key);
            await vijuprogram("vijuplus-comedy");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void vipserial(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "vip_serial", session_key);
            await vijuprogram("vijuplus-serial");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void vipmegahit(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "vip_megahit", session_key);
            await vijuprogram("vijuplus-megahit");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void vippremiere(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "vip_premiere", session_key);
            await vijuprogram("vijuplus-premiere");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void tv1000ruskino(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "tv1000_russian_kino", session_key);
            await vijuprogram("viju-tv1000-russkoe");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void tv1000action(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "tv1000_action", session_key);
            await vijuprogram("viju-tv1000-action");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void tv1000(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "tv1000", session_key);
            await vijuprogram("viju-tv1000");
            MainForm.Title += " - " + vijuprogram_tv;
            RunTv(new Uri(stream_tv));
        }
        private async void ctckids(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "ctc_kids", session_key);
            await epgtricolor("sts-kids");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void fox(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "fox", session_key);
            RunTv(new Uri(stream_tv));
        }
        private async void foxlife(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "fox_life", session_key);
            RunTv(new Uri(stream_tv));
        }
        private async void fenikspluskino(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "feniks_kino", session_key);
            RunTv(new Uri(stream_tv));
        }
        private async void weapon(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "oruzhie", session_key);
            await epgtricolor("oruzhie");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void anekdottv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("moretv", "anekdot_tv", session_key);
            await epgtricolor("anekdot-tv");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void x22(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await umastream("dcab9b90a33239837c0f71682d6606da", "2x2tv.ru");
            await limeepg("23");
            MainForm.Title += " - " + epg_lime;
            RunTv(new Uri(stream_tv));
        }
        private async void ntvpravo(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await ntvmedia_stream("4982");
            RunTv(new Uri(stream_tv));
        }
        private async void ntvstyle(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await ntvmedia_stream("4981");
            RunTv(new Uri(stream_tv));
        }
        private async void ntvhit(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await ntvmedia_stream("4984");
            RunTv(new Uri(stream_tv));
        }
        private async void ntvserial(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await ntvmedia_stream("4983");
            RunTv(new Uri(stream_tv));
        }
        private async void che(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("che", "ctc-che", session_key);
            RunTv(new Uri(stream_tv));
        }
        private async void stslove(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await mediastream_tv("ctclove", "ctc-love", session_key);
            RunTv(new Uri(stream_tv));
        }
        private async void kinotv(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(42);
            await epgtricolor("kino-tv-hd");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }
        private async void tv360(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(43);
            RunTv(new Uri(stream_tv));
        }

        private async void tv360news(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(44);
            RunTv(new Uri(stream_tv));
        }
        private async void onehdmusic(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await tv24h("110");
            RunTv(new Uri(stream_tv));
        }
        private async void trash(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await tv24h("4830");
            await epgtricolor("trash");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }

        private async void jiviactivno(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await tv24h("5046");
            await epgtricolor("zhivi-aktivno");
            MainForm.Title += " - " + epg_trico;
            RunTv(new Uri(stream_tv));
        }

        private async void babytime(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(41);
            RunTv(new Uri(stream_tv));
        }

        private async void glazamiturista(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(48);
            RunTv(new Uri(stream_tv));
        }

        private async void jivyapriroda(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(49);
            RunTv(new Uri(stream_tv));
        }

        private async void nashasibiruhd(object sender, RoutedEventArgs e)
        {
            defaulttitle();
            await devcmtv(50);
            RunTv(new Uri(stream_tv));
        }

        private void NoRezine(object sender, SizeChangedEventArgs e)
        {
            MainForm.Width = 992;
        }
    }
}
