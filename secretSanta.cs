using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2Day3
{

    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 1000; i++)
            {
                GetSecretSantas();
                Console.Clear();
            }
           
        }

        private static void GetSecretSantas()
        {
            List<Pairing> gifters = new List<Pairing>()
            {
                new Pairing() {Giftor = "Daniel" },
                new Pairing() {Giftor = "Brandon" },
                new Pairing() {Giftor = "Aaron" },
                new Pairing() {Giftor = "Mike" },
                new Pairing() {Giftor = "David" },
                new Pairing() {Giftor = "Scott" },
                new Pairing() {Giftor = "Jason" },
            };


            int numberOfPeople = gifters.Count();

            while (gifters.Any(x => x.Receipent == null))
            {
                var firstPerson = gifters.First(x => x.Receipent == null);
                var firstRandomName = gifters
                                        .Where(x => x.Giftor != firstPerson.Giftor) // make sure they don't pick themselves
                                        .Where(x => !gifters.Any(y => y.Receipent == x.Giftor)) // filter out ones that have names already used.
                                        .OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                //means only one person left and we need to swap.
                if (firstRandomName == null)
                {
                    var swap = gifters.Where(x => x.Giftor != firstPerson.Giftor).First();
                    firstPerson.Receipent = swap.Receipent;
                    swap.Receipent = null;
                }
                else
                {
                    firstPerson.Receipent = firstRandomName.Giftor;
                }
            }

            gifters.ForEach(x => Console.WriteLine("P1: {0}, P2 {1}", x.Giftor, x.Receipent));

            Console.ReadLine();
        }
    }

    internal class Pairing
    {
        public string Giftor { get; set; }
        public string Receipent { get; set; }
    }
}
