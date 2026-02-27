using System;
using System.Collections.Generic;
using System.Linq;

namespace JocDeCruce
{
    public class Pachet
    {
        private List<Card> carti;
        private Random random;

        public Pachet()
        {
            carti = new List<Card>();
            random = new Random();
            InitializeazaPachet();
        }

        private void InitializeazaPachet()
        {
            int[] valori = { 2, 3, 4, 9, 10, 11 }; 
            
            foreach (Culoare culoare in Enum.GetValues(typeof(Culoare)))
            {
                foreach (int valoare in valori)
                {
                    carti.Add(new Card(culoare, valoare));
                }
            }
        }

        public void Amesteca()
        {
            carti = carti.OrderBy(c => random.Next()).ToList();
        }

        public List<List<Card>> Distribuie()
        {
            List<List<Card>> maini = new List<List<Card>>();
            
            for (int i = 0; i < 4; i++)
            {
                maini.Add(new List<Card>());
            }

            int index = 0;
            foreach (var carte in carti)
            {
                maini[index % 4].Add(carte);
                index++;
            }

            return maini;
        }
    }
}