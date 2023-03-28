using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace cmliveplayerlivetv
{
    public class IniFile
    {
        private string Path;

        public IniFile(string IniPath)
        {
            Path = new FileInfo(IniPath).FullName.ToString();
        }

        public string ReadINI(string Section, string Key)
        {
            var RetVal = new StringBuilder(255);
            IniFileHelpers.GetPrivateProfileString(Section, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Section, string Key, string Value)
        {
            IniFileHelpers.WritePrivateProfileString(Section, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Section, Key, null);
        }

        public void DeleteSection(string Section = null)
        {
            Write(Section, null, null);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return ReadINI(Section, Key).Length > 0;
        }
    }
}
