using System;
using System.Collections.Generic;
using System.Threading;

namespace goddamnedsnakes
{
    class Program
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                global.score = 0;
                global.blue = true;


                Console.SetWindowSize(50, 23);
                Console.CursorVisible = false;
                box();
                head pp = new head(21, 10, "right", 1, new List<int> { }, new List<int> { });
                List<List<int>> berryXY = new List<List<int>> { };

                while (true)
                {
                    if (Collison(pp))
                    {
                        break;
                    }
                    DHead(pp);
                    DBody(pp);

                    Berry(pp, berryXY);
                    CheckDir(pp);
                    move(pp);
                    Berrycollect(pp, berryXY);
                
                    Thread.Sleep(150);
                }
                Console.Clear();
                box();
                Console.SetCursorPosition(14, 8);
                Console.Write("L + ratio");
                Console.SetCursorPosition(14, 9);
                Console.Write("score: " + global.score);
                Console.SetCursorPosition(14, 11);
                Console.Write("Retry\t\tExit");

                bool retry = true;
                while (true)
                {
                

                    if (retry)
                    {
                        Console.SetCursorPosition(14, 12);
                        Console.Write("_____\t\t      ");
                    }
                    else if (!retry)
                    {
                        Console.SetCursorPosition(14, 12);
                        Console.Write("     \t\t____");
                    }

                    var keys = Console.ReadKey(true).Key;
                    if (keys == ConsoleKey.D && retry)
                    {
                        retry = false;
                    }
                    else if (keys == ConsoleKey.A && !retry)
                    {
                        retry = true;
                    }
                    else if (keys == ConsoleKey.Enter && retry)
                    {
                        break;
                    }
                    else if (keys == ConsoleKey.Enter && !retry)
                    {
                        Console.SetCursorPosition(0, 21);
                        Environment.Exit(0);
                    }
                }

            }




        }

        static class global
        {
            public static int score = 0;
            public static bool blue = true;
        }


        public static void Berrycollect(head snake, List<List<int>> berrylist)
        {
            foreach (List<int> berry in berrylist.ToArray())
            {
                if (snake.X == berry[0] && snake.Y == berry[1])
                {
                    berrylist.Remove(berry);
                    snake.Lbod += 1;
                    global.score += 1;
                }
            }
        }

        public static void Berry(head bf, List<List<int>> L)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                var rand = new Random();
                int xx = rand.Next(1, 48);
                int yy = rand.Next(1, 21);
                List<int> tempxy = new List<int> { xx, yy };

                if (L.Count >= 5)
                {
                    break;
                }
                if (!BerryCol(bf, xx, yy) && berrycheck(L, tempxy)) //checks body collison and berry collison
                {
                    Console.SetCursorPosition(xx, yy);
                    Console.Write("O");
                    L.Add(tempxy);
                }



            }
        }

        public static bool berrycheck(List<List<int>> berrylist, List<int> berrycoords)
        {
            foreach (List<int> x in berrylist)
            {

                if (x[0] == berrycoords[0] && x[1] == berrycoords[1])
                {
                    return false;
                }
            }
                return true;
        }

        public static bool Collison(head dv) //checks for lose condition
        {
            if (dv.X < 1 || dv.Y < 1 || dv.X > 47 || dv.Y > 20) // check for box hit
            {
                return true;
            }
            else if (BodyCol(dv))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool BodyCol(head ds) //check for body hit
        {
            for (int i = 0; i < ds.BodX.Count - 1 ; i++)
            {
                if (ds.BodX[i] == ds.X && ds.BodY[i] == ds.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool BerryCol(head ds, int x, int y ) //check for berry spawn
        {
            List<int> templist = new List<int> { x, y };
            for (int i = 0; i < ds.BodX.Count; i++)
            {
                if (ds.BodX[i] == templist[0] && ds.BodY[i] == templist[1])
                {
                    return true;
                }
            }
            return false;
        }


        public static void DBody(head mom)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (mom.BodX.Count > mom.Lbod)
            {
                Console.SetCursorPosition(mom.BodX[0], mom.BodY[0]);
                Console.Write(" ");
                mom.BodX.RemoveAt(0);
                mom.BodY.RemoveAt(0);
            }
            for (int i = 0; i < mom.BodY.Count; i++)
            {
                Console.SetCursorPosition(mom.BodX[i], mom.BodY[i]);
                Console.Write("█");
            }
            if (global.blue)
            {
                Console.SetCursorPosition(20, 10);
                Console.Write(" ");
                global.blue = false;
            }
        }

        public static void DHead(head mom) //draws head
        {
            try
            {
                Console.SetCursorPosition(mom.X, mom.Y);
                Console.Write("█");
                if (mom.BodY.Count != 0)
                {
                    Console.SetCursorPosition(mom.BodX[mom.BodX.Count - 1], mom.BodY[mom.BodY.Count - 1]);
                    Console.Write(" ");
                }
            }
            catch
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("wdfghbidfvhbihbi");
            }
        }
        public static head CheckDir(head xy) // changes head object direction on keypress
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.D && xy.dir != "left")
                {
                    xy.dir = "right";
                }
                else if (key == ConsoleKey.A && xy.dir != "right")
                {
                    xy.dir = "left";
                }
                else if (key == ConsoleKey.W && xy.dir != "down")
                {
                    xy.dir = "up";
                }
                else if (key == ConsoleKey.S && xy.dir != "up")
                {
                    xy.dir = "down";
                }
            }
            return xy;
        }

        public static head move(head sa) //moves head
        {
            sa.BodX.Add(sa.X);      //adds current position to list
            sa.BodY.Add(sa.Y);
            if (sa.dir == "right")
            {
                sa.X += 1;
            }
            else if (sa.dir == "left")
            {
                sa.X -= 1;
            }
            else if (sa.dir == "down")
            {
                sa.Y += 1;
            }
            else if (sa.dir == "up")
            {
                sa.Y -= 1;
            }
            
            //Console.SetCursorPosition(0, 10); //testing
            //for (int i = 0; i < sa.BodX.Count; i++) 
            //{
            //    Console.WriteLine(sa.BodX[i] + " " + sa.BodY[i]);
            //}
            return sa;
        }

        public static void box() //draws bounding box
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("+■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■+");
            for (var i = 0; i < 20; i++)
            {
                Console.WriteLine("█                                               █");
            }
            Console.WriteLine("+■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■+");
        }

    }
}
