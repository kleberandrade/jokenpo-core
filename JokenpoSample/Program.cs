using Jokenpo;
using System;

namespace JokenpoSample
{
    public class Program
    {
        private static int match = 0;
        private static bool isJoker = false;
        private static IJokenpoHand player = new JokenpoHand();
        private static IJokenpoHand enemy = new JokenpoHandAI();

        private static void Menu()
        {
            isJoker = false;

            while (true)
            {
                Console.WriteLine("Match game: #{0}", (++match));
                Console.WriteLine("Choose your hand");
                Console.WriteLine("1 : Rock");
                Console.WriteLine("2 : Paper");
                Console.WriteLine("3 : Scissor");
                Console.WriteLine("4 : Lizard");
                Console.WriteLine("5 : Spock");

                if (isJoker)
                    Console.WriteLine("Let's play correctly joker...");

                Console.Write("What your hand [1-5]: ");
                string line = Console.ReadLine();
                int option;
                if (!int.TryParse(line, out option))
                {
                    isJoker = true;
                    Console.Clear();
                    continue;
                }

                if (option < 1 || option > 5)
                {
                    isJoker = true;
                    Console.Clear();
                    continue;
                }

                player.Hand = (JokenpoHandType)(option - 1);
                break;
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Menu();

                ((JokenpoHandAI)enemy).Next();

                Console.WriteLine("\nRock-Paper-Scissor-Lizard-Spock");
                Console.WriteLine("\nPlayer ({0}) x ({1}) Enemy", player.Hand, enemy.Hand);

                int result = JokenpoRules.Compare(player, enemy);
                if (result == 0)
                    Console.WriteLine("Draw.");
                else if (result < 0)
                    Console.WriteLine("You Lose!");
                else
                    Console.WriteLine("You Win!");


                Console.Write("\nPlay again [y/n]? ");

                if (Console.ReadLine().ToLower().Equals("n"))
                    break;
               

                Console.Clear();
            }

            Console.WriteLine("Thanks.");
            Console.ReadKey();
        }
    }
}
