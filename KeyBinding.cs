using System.Windows.Input;

namespace cmliveplayerlivetv
{
    public class BoolKeyBinding : KeyBinding
    {
        public bool Parameter
        {
            get { return (bool)CommandParameter; }
            set { CommandParameter = value; }
        }
    }
}
