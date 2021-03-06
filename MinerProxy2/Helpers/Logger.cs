﻿/* MinerProxy2 programmed by LostSoulfly.
   GNU General Public License v3.0 */

using System;

namespace MinerProxy2.Helpers
{
    internal static class Logging
    {
        private static string asciiLogo =
                            @"      __  __ _                 ____                      ____
     |  \/  (_)_ __   ___ _ __|  _ \ _ __ _____  ___   _|___ \
     | |\/| | | '_ \ / _ \ '__| |_) | '__/ _ \ \/ / | | | __) |
     | |  | | | | | |  __/ |  |  __/| | | (_) >  <| |_| |/ __/
     |_|  |_|_|_| |_|\___|_|  |_|   |_|  \___/_/\_\\__, |_____|
                                                   |___/       ";

        public static readonly object ConsoleBlockLock = new object();

        public static readonly object ConsoleColorLock = new object();

        public static string credits = "Programmed by LostSoulfly";

        public static void MinerProxyHeader()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;

            Console.WriteLine(asciiLogo + version);
            Console.WriteLine(credits + '\n');
        }
    }
}