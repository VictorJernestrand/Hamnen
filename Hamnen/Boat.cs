using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hamnen
{
    public class Boat
    {
        public string Id { get; set; }
        public string BoatType { get; set; }
        public int Size { get; set; }
        public int DaysCounter { get; set; }

        // Method to create a list of 5 random boats
        public static List<string> GetFiveRandomBoats(List<string> boats)
        {
            int numberofBoats = 0;
            // Instantiate list of boats.
            List<string> harbour = new List<string>();

            // Populate list of boats with 5 random boats.
            while (numberofBoats < 5)
            {
                Random rnd = new Random();
                string s = boats[rnd.Next(boats.Count)];
                harbour.Add(s);
                numberofBoats++;
            }
            return harbour;
        }
        // Give every boats a uniqe id of 3 random alphabetical letters
        public static string RandomBoatId()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 3)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
