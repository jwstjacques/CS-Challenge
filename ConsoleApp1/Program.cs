using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1 {
    class Program {
        static string joke = "";
        static char key;

        static ConsolePrinter printer = new ConsolePrinter ();

        static async Task Main (string[] args) {
            int counter = 0;

            Console.Clear ();
            printer.Value ("************************************").ToString ();
            printer.Value ("*  Welcome to the Joke Company!    *").ToString ();
            printer.Value ("*  Would you like to hear a joke?  *").ToString ();
            printer.Value ("*  Press y for yes! : )            *").ToString ();
            printer.Value ("*  Press n for no!  : (            *").ToString ();
            printer.Value ("************************************").ToString ();

            Boolean yesOrNo = false;
            Boolean exit = false;

            while (!yesOrNo) {
                GetEnteredKey (Console.ReadKey ());

                if (key == 'y') {
                    yesOrNo = true;
                } else if (key == 'n') {
                    return;
                } else {
                    Console.WriteLine ("Press y for yes, n for no");
                }
            }

            while (!exit) {
                // To prevent hitting rate limit on api
                if (counter == 10) {
                    printer.Value ("\nThat's enough jokes for you today!").ToString ();
                    return;
                }

                Console.Clear ();
                printer.Value ("********************************************").ToString ();
                printer.Value ("*  What would you like:                    *").ToString ();
                printer.Value ("*  Press r for Random Chuck Norris joke    *").ToString ();
                printer.Value ("*  Press f for a joke with a Fake name     *").ToString ();
                printer.Value ("*  Press x to exit                         *").ToString ();
                printer.Value ("********************************************").ToString ();

                GetEnteredKey (Console.ReadKey ());

                if (key == 'r') {
                    await GetRandomJoke (false);
                } else if (key == 'f') {
                    await GetRandomJoke (true);
                } else if (key == 'x') {
                    printer.Value ("\nSee ya!!").ToString ();
                    exit = true;
                    return;
                }

                counter++;
                printer.Value ('\n' + joke).ToString ();
                printer.Value ("\nPress Any Key to Continue...").ToString ();
                GetEnteredKey (Console.ReadKey ());
            }
        }

        private static void GetEnteredKey (ConsoleKeyInfo consoleKeyInfo) {
            switch (consoleKeyInfo.Key) {
                case ConsoleKey.F:
                    key = 'f';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.X:
                    key = 'x';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
            }
        }

        /// <summary>Retreives a joke from the supplied URL and name replaces when given a true argument</summary>
        /// <param name="useFakeName">A boolean to determine whether to call GetName()</param>
        private static async Task GetRandomJoke (Boolean withFakeName = false) {
            string formattedJoke = await JsonFeed.GetRandomJoke ();

            if (withFakeName) {
                dynamic response = await GetName ();

                // Replace first and last name separately as some jokes use first and/or last name separately
                // ex. They don't call him Chuck.... or ... Mr. Norris.
                string firstName = response["name"];
                string lastName = response["surname"];

                // Replace does not play nice with direct object property
                formattedJoke = Regex.Replace (formattedJoke, "(?i)Chuck", firstName);
                formattedJoke = Regex.Replace (formattedJoke, "(?i)Norris", lastName);

                // Accommodate for female names, and his/her gender designation in the joke
                if (response["gender"] == "female") {
                    formattedJoke = formattedJoke.Replace (" He ", " She ");
                    formattedJoke = formattedJoke.Replace (" he ", " she ");
                    formattedJoke = formattedJoke.Replace (" His ", " Her ");
                    formattedJoke = formattedJoke.Replace (" his ", " her ");
                    formattedJoke = formattedJoke.Replace (" Him ", " Her ");
                    formattedJoke = formattedJoke.Replace (" him ", " her ");
                }
            }

            joke = formattedJoke;
        }

        /// <summary>
        /// Async get method to retrieve names from uinames.com
        /// </summary>
        /// <returns>A name object</returns>
        private static async Task<dynamic> GetName () {
            return await JsonFeed.Getname ();
        }
    }
}