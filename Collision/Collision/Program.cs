using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
/*
 *  Collide
 *  Eine Simulation im 2-dimensionalen Raum
 *  Aufgabenverteilung
 *  a) Hueber Noemi
 *  b) Gross Patrick
 *  c) Zöschg Thomas
 *  d) Weiss Lukas
 * 
 * 2020 TFO-Meran
 */

namespace ConsoleApplication1
{
    class Program
    {
        const int seite = 50;
        static int[,] feld = new int[seite, seite];

        

        class einer
        {
            // Private Eigenschaften

            // Öffentliche Eigenschaften
            public int posx, posy;
            public ConsoleColor farbe;

            // Konstruktor

            Random randomGenerator = new Random();  

            /// <summary>
            /// Erstellt & Initialisiert a nuies Objekt mit zufàlliger Position und Forb
            /// </summary>
            public einer()
            {
                do
                {
                    //Neue Position generieren
                    posx = randomGenerator.Next(1, seite);
                    posy = randomGenerator.Next(1, seite);

                } while (feld[posx, posy] != 0); //Wiederholen, falls Feld besetzt

                feld[posx, posy] = 1; // Feld besetzen

                farbe = (ConsoleColor)randomGenerator.Next(16); // Farbe generieren
                
            }
            //Private Methoden

            // Thomas Zöschg
            private void hide()
            {
                int cursposX, cursposY;
                
                // Speicher die aktuelle Cursorposition
                cursposX = Console.CursorLeft;
                cursposY = Console.CursorTop;

                // Setzt die Cursorposition auf das gewünschte objekt
                Console.SetCursorPosition(posx, posy);
                // Überschreibt das Objekt mit einer leertaste
                Console.Write(" ");
                
                // Setzt die ursprüngliche Cursorposition
                Console.SetCursorPosition(cursposX, cursposY);   
            }

            // Thomas Zöschg
            private void show()
            {
                int cursposX, cursposY;
                // Speichert die originale Textfarbe
                ConsoleColor originalcolor = Console.ForegroundColor;
                
                // Speicher die aktuelle Cursorposition
                cursposX = Console.CursorLeft;
                cursposY = Console.CursorTop;

                // Setzt die Schriftfarbe auf eine ausgewhälte farbe
                Console.ForegroundColor = farbe;
                // Setzt Cursorposition auf gewünschten Ort
                Console.SetCursorPosition(posx, posy);
                // Schreibt 0 auf den gewünschten Ort
                Console.Write("0");

                // Setzt die Textfarbe wieder auf das Originale
                Console.ForegroundColor = originalcolor;
                // Setzt die ursprüngliche Cursorposition
                Console.SetCursorPosition(cursposX, cursposY);
            }

            // Thomas Zöschg
            private void collide()
            {   
                int cursposX, cursposY;
                int newcursX ,newcursY;
                // Speichert die originale Textfarbe
                ConsoleColor originalcolor = Console.ForegroundColor;

                // Erstellt zufallszahlen für eine neue position
                Random ZufallsZahl = new Random();
                do
                {
                    newcursX = ZufallsZahl.Next(1, seite - 1);
                    newcursY = ZufallsZahl.Next(1, seite - 1);
                    // Prüft ob es die neue position noch nicht gibt 
                } while (feld[newcursX, newcursY] != 1);

                // Speichert neue zufällige position in feld
                feld[newcursX, newcursY] = 1;

                // Merkt sich die aktuelle Curserposition
                cursposX = Console.CursorLeft;
                cursposY = Console.CursorTop;

                 // Setzt die Schriftfarbe auf eine ausgewhälte farbe
                Console.ForegroundColor = farbe;
                // Setzt die Cursorposition auf gewünschtes Objekt
                Console.SetCursorPosition(posx, posy);
                // Schreibt C für collision auf die Cursorposition
                Console.Write("C");

                // Setzt die Cursorposition auf neue zufallsort
                Console.SetCursorPosition(newcursX, newcursY);
                // Schreibt 0 auf den gewünschten Ort
                Console.Write("0");

                // Setzt die Textfarbe wieder auf das Originale
                Console.ForegroundColor = originalcolor;
                // Setzt die Cursorposition wo es vorher war
                Console.SetCursorPosition(cursposX, cursposY);
            }

            //Öffentliche Methoden
            
            //Gross Patrick
            public void Move()
            {
                Random ZufallsZahl = new Random();
                int Richtung = ZufallsZahl.Next(1, 4);

                //springt in Methode "hide", zum Löschen des Objekts
                hide();

                //schreibt eine 0 auf die Position, wo es sich gerade befand
                feld[posx, posy] = 0;

                #region Position festlegen
                //Neue Position festlegen
                //Eine Zelle hoch
                if (Richtung == 1)
                {
                    //Schaut ob es sich bereits ganz oben befindet
                    if (posy == 0)  //wenn, dann geht es einfach eine Zelle in die andere Richtung(runter)
                    {
                        posy++;
                    }
                    else  //wenn nicht, dann ganz normal eine Zelle nach oben
                    {
                        posy--;
                    }
                }
                //Eine Zelle runter
                if (Richtung == 2)
                {
                    //Schaut ob es sich bereits ganz unten befindet
                    if (posy == seite) //wenn, dann geht es einfach eine Zelle in die andere Richtung(hoch)
                    {
                        posy--;
                    }
                    else  //wenn nicht, einfach ganz normal eine Zelle nach unten
                    {
                        posy++;
                    }
                }
                //Eine Zelle nach links
                if (Richtung == 3)
                {
                    //Schaut ob es sich bereits ganz links befindet
                    if (posx == 0)  //wenn, dann geht es einfach eine Zelle in die andere Richtung(rechts)
                    {
                        posx++;
                    }
                    else  //wenn nicht, einfach ganz normal eine Zelle nach links
                    {
                        posx--;
                    }            
                    
                }
                //Eine Zelle nach rechts
                if (Richtung == 4)
                {
                    //Schaut ob es sich bereits ganz rechts befindet
                    if (posx == seite)//wenn, dann geht es einfach eine Zelle in die andere Richtung(links)
                    {
                        posx--;
                    }
                    else  //wenn nicht, einfach ganz normal eine Zelle nach rechts
                    {
                        posx++;
                    }                    
                }
                else
                { }
                #endregion

                //Schaut ob sich auf dieser Position bereits ein Objekt befindet oder nicht
                if (feld[posx, posy] == 0)  //wenn nicht, dann zeige es auf dieser Position an, und schreibe auf dieser Position eine 1
                {
                    //springt in Methode "show", zum Anzeigen des Objekts
                    show();

                    //schreibt eine 1 auf die Position, wo es sich aktuell befindet
                    feld[posx, posy] = 1;
                }
                else  //wenn doch, dann springe in die Methode collide
                {
                    //springt in Methode "collide", um eine Kollision anzuzeigen und eine neue Position festzulegen
                    collide();
                }
                //Ende Methode "Move"
            }

            
        }

        

        static void Main(string[] args)
        {
            Console.WindowWidth = seite * 2;
            Console.WindowHeight = seite;
            Console.Clear();
            Random ZG = new Random();
            int Anzahl = ZG.Next(1, 6);
            einer[] meineEiner = new einer[Anzahl];
            for (int i = 0; i < Anzahl; i++)
            {
                meineEiner[i] = new einer();
            }
            Console.CursorVisible = false;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < Anzahl; j++)
                {
                   meineEiner[j].Move();
                }
                System.Threading.Thread.Sleep(10);

            }
        }

        //Aufgabe D, Weiss Lukas

        static bool SaveConfig(int Anzahl)
        {
            string Pfad = @"c:\tmp\config.ini";
            string Inhalt = "";
            bool gespeichert = false;

            StreamWriter sw = new StreamWriter(Pfad);
            sw.WriteLine(Anzahl);
            sw.Close();

            using (StreamReader sr = File.OpenText(Pfad))
            {
                Inhalt = sr.ReadLine();
            }

            if (Anzahl == Convert.ToInt16(Inhalt))
            {
                gespeichert = true;
            }

            return gespeichert;
        }

        static bool LoadConfig(ref int Anzahl)
        {
            string Pfad = @"c:\tmp\config.ini";
            string Inhalt = "";
            bool keinfehler = false;

            if (File.Exists(Pfad))
            {
                using (StreamReader sr = File.OpenText(Pfad))
                {
                    Inhalt = sr.ReadLine();
                }

                if (Inhalt == " " || Inhalt == "0" || Inhalt == null)
                {
                    Anzahl = 0;
                    keinfehler = false;
                }
                else
                {
                    try
                    {
                        Anzahl = Convert.ToInt16(Inhalt);
                        keinfehler = true;
                    }
                    catch (FormatException)
                    {
                        Anzahl = 0;
                        keinfehler = false;
                    }
                }
            }
            else
            {
                Anzahl = 0;
                keinfehler = false;
            }
            return keinfehler;
        }

    }
}
