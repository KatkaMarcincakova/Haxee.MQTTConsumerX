namespace Haxee.MQTTConsumer.Services
{
    public class HelperService
    {
        public static bool ValidMenuOption(List<int> validOptions, int option)
        {
            foreach (int o in validOptions)
                if (o == option)
                    return true;

            return false;
        }
    }
}
