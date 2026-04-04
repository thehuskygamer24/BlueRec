using System;
using server;
using System.IO;
using ws;
using api;
using System.Net;
using System.Diagnostics;
using vaultgamesesh;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace start
{
    class Program
    {
        static void Main()
        {
            Setup.setup();
            goto Tutorial;

        Tutorial:
            if (Setup.firsttime == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Title = "BlueRec Intro";
                Console.WriteLine("Welcome to BlueRec " + appversion + "!");
                Console.WriteLine("Is this your first time using BlueRec?");
                Console.WriteLine("Yes or No (Y, N)");
                string readline22 = Console.ReadLine();

                if (readline22 == "y" || readline22 == "Y")
                {
                    Console.Clear();
                    Console.Title = "BlueRec Tutorial";
                    Console.WriteLine("In that case, welcome to BlueRec!");
                    Console.WriteLine("BlueRec is server software that emulates the old servers of previous RecRoom versions.");
                    Console.WriteLine("To use BlueRec, you'll need to have builds aswell!");
                    Console.WriteLine("To download builds, either go to the builds channel or use the links below:" + Environment.NewLine);
                    Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Update/builds.txt"));
                    Console.WriteLine("Download a build and press any key to continue:");
                    Console.ReadKey();

                    Console.Clear();
                    Console.WriteLine("Now that you have a build, what you're going to do is as follows:" + Environment.NewLine);
                    Console.WriteLine("1. Unzip the build");
                    Console.WriteLine("2. Start the server by pressing 5 on the main menu");
                    Console.WriteLine("3. Run Recroom_Release.exe from the folder." + Environment.NewLine);
                    Console.WriteLine("Press any key to go to the main menu:");
                    Console.ReadKey();

                    Console.Clear();
                    goto Start;
                }
                else
                {
                    Console.Clear();
                    goto Start;
                }
            }
            else
            {
                goto Start;
            }

        Start:
            Console.Title = "BlueRec Startup Menu";
            string State = new WebClient().DownloadString("https://raw.githubusercontent.com/thehuskygamer24/BlueRec/refs/heads/master/Download/state.txt");
             if (State.Trim().ToLower() == "closed";
                 {
                     goto ServerClose;
                 }
                        
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("BlueRec - Old Rec Room Server For 2017. (Version: " + appversion + ")");
            Console.WriteLine("Created by orangecreeper245671");
            // Console.WriteLine("Download source code here: https://github.com/recroom2016/OpenRec");
           // Console.WriteLine("Discord: https://discord.gg/daC8QUhnFP" + Environment.NewLine);

            if (!(new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Download/version.txt").Contains(appversion)))
            {
                Console.WriteLine("This version of BlueRec is outdated. Install the latest version: BlueRec " +
                new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Download/version.txt"));
            }

            Console.WriteLine("(1) Updates And Announcements");
            Console.WriteLine("(2) Settings");
            Console.WriteLine("(3) Discord Server")
            Console.WriteLine("(4) Profile");
            Console.WriteLine("(5) Builds");
            Console.WriteLine("(6) Start Server");
        
                

            string readline = Console.ReadLine();

            if (readline == "1")
            {
                Console.Title = "BlueRec Changelog";
                Console.Clear();
                Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Download/changelog.txt"));
                Console.WriteLine("Press any key to continue:");
                Console.ReadKey();
                Console.Clear();
                goto Start;
            }

            if (readline == "2")
            {
                Console.Clear();
                goto Settings;
            }

        Settings:
            Console.Title = "BlueRec Settings Menu";
            Console.WriteLine("(1) Private Rooms: " + File.ReadAllText("SaveData\\App\\privaterooms.txt"));
            Console.WriteLine("(2) Custom Room Downloader");
            Console.WriteLine("(3) Reset SaveData");
            Console.WriteLine("(4) Go Back");

            string readline4 = Console.ReadLine();

            if (readline4 == "1")
            {
                if (File.ReadAllText("SaveData\\App\\privaterooms.txt") == "Disabled")
                    File.WriteAllText("SaveData\\App\\privaterooms.txt", "Enabled");
                else
                    File.WriteAllText("SaveData\\App\\privaterooms.txt", "Disabled");

                Console.Clear();
                Console.WriteLine("Success!");
                goto Settings;
            }
            else if (readline4 == "2")
            {
                Console.Title = "BlueRec Custom Room Downloader";
                Console.Clear();
                Console.WriteLine("Type the room name:");
                string roomname = Console.ReadLine();

                try
                {
                    string text = new WebClient().DownloadString("https://rooms.rec.net/rooms?name=" + roomname + "&include=297");
                    CustomRooms.RoomDecode(text);
                    Console.WriteLine("Success!");
                }
                catch
                {
                    Console.WriteLine("Failed to download room...");
                }

                goto Settings;
            }
            else if (readline4 == "3")
            {
                string[] files =
                {
                    "SaveData\\avatar.txt","SaveData\\avataritems.txt","SaveData\\equipment.txt",
                    "SaveData\\consumables.txt","SaveData\\gameconfigs.txt","SaveData\\storefronts2.txt",
                    "SaveData\\Profile\\username.txt","SaveData\\Profile\\level.txt","SaveData\\Profile\\userid.txt",
                    "SaveData\\myrooms.txt","SaveData\\settings.txt","SaveData\\App\\privaterooms.txt",
                    "SaveData\\App\\facefeaturesadd.txt","SaveData\\profileimage.png",
                    "SaveData\\App\\firsttime.txt","SaveData\\avataritems2.txt"
                };

                foreach (var file in files)
                {
                    if (File.Exists(file)) File.Delete(file);
                }

                Console.WriteLine("Success!");
                Setup.setup();
                goto Settings;
            }
            else if (readline4 == "4")
            {
                Console.Clear();
                goto Start;
            }

            if (readline == "3")
            {
                Console.Clear();
                goto Profile;
            }

        Profile:
            Console.Title = "BlueRec Profile Menu";
            Console.WriteLine("(1) Change Username");
            Console.WriteLine("(2) Change Profile Image");
            Console.WriteLine("(3) Change Level");
            Console.WriteLine("(4) Profile Downloader");
            Console.WriteLine("(5) Go Back");

            string readline3 = Console.ReadLine();

            if (readline3 == "1")
            {
                Console.WriteLine("Current Username: " + File.ReadAllText("SaveData\\Profile\\username.txt"));
                Console.WriteLine("New Username:");
                string newusername = Console.ReadLine();
                File.WriteAllText("SaveData\\Profile\\username.txt", newusername);

                Console.WriteLine("Success!");
                goto Profile;
            }
            else if (readline3 == "3")
            {
                Console.WriteLine("Current Level: " + File.ReadAllText("SaveData\\Profile\\level.txt"));
                Console.WriteLine("New Level:");
                string newlevel = Console.ReadLine();
                File.WriteAllText("SaveData\\Profile\\level.txt", newlevel);

                Console.WriteLine("Success!");
                goto Profile;
            }
            else if (readline3 == "5")
            {
                Console.Clear();
                goto Start;
            }

            if (readline == "4")
            {
                Console.Title = "BlueRec Build Downloads";
                Console.Clear();
                Console.WriteLine(new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Update/builds.txt"));
                Console.ReadKey();
                goto Start;
            }

            if (readline == "5")
            {
                Console.Title = "BlueRec Version Select";
                Console.WriteLine("Select version (2016, 2017, 2018):");

                string readline2 = Console.ReadLine();

                if (readline2 == "2016")
                {
                    version = "2016";
                    Console.WriteLine("Version Selected: 2016");
                    new APIServer();
                    new WebSocket();
                }
                else if (readline2 == "2017")
                {
                    version = "2017";
                    Console.WriteLine("Version Selected: 2017");
                    new APIServer();
                    new WebSocket();
                }
                else if (readline2 == "2018")
                {
                    version = "2018";
                    new APIServer();
                    new WebSocket();
                }

                Console.WriteLine(msg);
            }
        }

        public static string msg = "Server running. Start your build now.";
        public static string version = "";
        public static string appversion = "0.6.9";
        public static bool bannedflag = false;
        ServerClose:
            Console.Title = "Server Failed";
            Console.ForegroundColor = Console.Color.Red;
            Console.Writeline("Blue Rec Is Offline Or Under Maintenance Please Come Back Later");
    }
}
