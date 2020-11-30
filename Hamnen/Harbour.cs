using System;
using System.Collections.Generic;
using System.Text;

namespace Hamnen
{
    public class Harbour
    {
        const string POWERBOAT = "powerboat";
        const string SAILBOAT = "sailboat";
        const string CARGOSHIP = "cargoship";
        Boat[] harbour = new Boat[25];
        List<string> boats = new List<string>();
        int rejectedBoats = 0;

        // Looping method that shows the harbour filled with boats
        public void ShowHarbour()
        {
            boats.Add(POWERBOAT);
            boats.Add(SAILBOAT);
            boats.Add(CARGOSHIP);

            bool keepGoing = true;
            
            do
            {
                int emptyMoorings = 0;

                foreach (Boat boat in harbour)
                {
                    if (boat != null && boat.DaysCounter == 0)
                    {
                        for (int i = 0; i < harbour.Length; i++)
                        {
                            if (harbour[i] == boat)
                                harbour[i] = null;
                        }
                    }
                }
                
                FillHarbourWithBoats();

                // Check if boat has stayed in harbour for as long as its DaysCounter, if so, replace boat with empty mooring
                int mooring = 1;

                // Count rejected boats that the harbour did not had enough space for
                foreach (Boat b in harbour)
                {
                    if (b is null)
                        emptyMoorings++;
                }

                //Make sure the counter works as intented with a dictionary to count the key instead of multiple
                //rows with same id in the list
                Dictionary<string, Boat> guests = new Dictionary<string, Boat>();
                foreach (Boat boat in harbour)
                {
                    if (boat != null && !guests.ContainsKey(boat.Id))
                    {
                        guests.Add(boat.Id, boat);
                    }
                };
                foreach (Boat boat in guests.Values)
                {
                    boat.DaysCounter--;
                }
                
                Console.WriteLine(
$@"                  Hamnen  
Antal avvisade båtar = { rejectedBoats}
Antal tomma kajplatser = { emptyMoorings}

Tryck Enter för att uppdatera hamnen till nästa dag.

Plats           Båttyp          Nr       ");

                foreach (Boat boat in harbour)
                {
                    if (boat == null)
                    {
                        Console.WriteLine($"{mooring}\t\tTom Plats");
                    }
                    else
                    {
                        Console.WriteLine($"{mooring}\t\t{boat.BoatType}\t{boat.Id}");
                    }
                    mooring++;
                };

                Console.ReadKey();
                Console.Clear();
            }

            while (keepGoing == true);

        }

        // Method to fill harbour with boats 
        public void FillHarbourWithBoats()
        {
            List<string> randomBoats = Boat.GetFiveRandomBoats(boats);
            foreach (var boat in randomBoats)
            {
                bool fillOneMooring = false;
                if (boat == POWERBOAT)
                {
                    Boat powerboat = new Boat { BoatType = "Motorbåt", Size = 1 };
                    powerboat.Id = "M-" + Boat.RandomBoatId();
                    powerboat.DaysCounter = 3;

                    for (int i = 0; i < harbour.Length-powerboat.Size; i++)
                    {
                        if (harbour[i] == null)
                        {
                            harbour[i] = powerboat;

                            fillOneMooring = true;
                        }
                        if (fillOneMooring == true)
                            break;
                    }
                    if (fillOneMooring == false)
                    {
                        rejectedBoats++;
                    }
                }
                else if (boat == SAILBOAT)
                {
                    Boat sailboat = new Boat { BoatType = "Segelbåt", Size = 2 };
                    sailboat.Id = "S-" + Boat.RandomBoatId();
                    sailboat.DaysCounter = 4;

                    for (int i = 0; i < harbour.Length-sailboat.Size; i++)
                    {
                        if (harbour[i] == null &&
                            harbour[i + 1] == null)
                        {
                            harbour[i] = sailboat;
                            harbour[i + 1] = sailboat;

                            fillOneMooring = true;
                        }
                        if (fillOneMooring == true)
                            break;
                    }
                    if (fillOneMooring == false)
                    {
                        rejectedBoats++;
                    }
                }
                else if (boat == CARGOSHIP)
                {
                    Boat cargoShip = new Boat { BoatType = "Fraktbåt", Size = 4 };
                    cargoShip.Id = "L-" + Boat.RandomBoatId();
                    cargoShip.DaysCounter = 6;

                    for (int i = 0; i < harbour.Length-cargoShip.Size; i++)
                    {
                        if (harbour[i] == null &&
                            harbour[i + 1] == null &&
                            harbour[i + 2] == null &&
                            harbour[i + 3] == null)
                        {
                            harbour[i] = cargoShip;
                            harbour[i + 1] = cargoShip;
                            harbour[i + 2] = cargoShip;
                            harbour[i + 3] = cargoShip;

                            fillOneMooring = true;
                        }
                        if (fillOneMooring == true)
                            break;
                    }
                    if (fillOneMooring == false)
                    {
                        rejectedBoats++;
                    }
                }
            }
        }
    }
}
