﻿namespace Haxee.MQTTConsumer.Services
{
    public class MenuService
    {
        public static int MainMenu()
        {
            Console.Clear();

            DrawService.DrawLogo();

            DrawService.DrawMainMenu();

            int option = 0;
            List<int> validOptions = new List<int> { 1, 2, 3, 4, 5 };

            while (!HelperService.ValidMenuOption(validOptions, option))
            {
                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                if (!HelperService.ValidMenuOption(validOptions, option))
                {
                    DrawService.DrawBonk();
                    DrawService.DrawErrorOption(validOptions);
                }

                DrawService.DrawMainMenu();
            }

            return option;
        }

        public static void CurrentSetup(CurrentYear currentYear)
        {
            Console.Clear();
            DrawService.ShowCurrentSettings(currentYear);

            Console.WriteLine("\n[0] Back");

            int option = -1;
            List<int> validOptions = new List<int> { 0 };

            while (!HelperService.ValidMenuOption(validOptions, option))
            {
                int.TryParse(Console.ReadLine(), out option);

                Console.Clear();

                if (!HelperService.ValidMenuOption(validOptions, option))
                {
                    DrawService.DrawBonk();
                    DrawService.DrawErrorOption(validOptions);
                }

                DrawService.ShowCurrentSettings(currentYear);

                Console.WriteLine("\n[0] Back");
            }
        }

        public static void SetupHiFi()
        {
            bool validSetup = false;
            while(!validSetup)
            {
                Console.Clear();
                Console.WriteLine($"Hi-Fi Ralley SETUP");
                Console.Write("Year:\n   ");
                string y = Console.ReadLine() ?? String.Empty;

                Console.Write("Broker IP:\n   ");
                string ip = Console.ReadLine() ?? String.Empty;

                Console.Write("Broker port:\n   ");
                string port = Console.ReadLine() ?? String.Empty;

                Console.Write("Client name:\n   ");
                string name = Console.ReadLine() ?? String.Empty;

                Console.Write("Topic:\n   ");
                string topic = Console.ReadLine() ?? String.Empty;
            }
        }
    }
}
