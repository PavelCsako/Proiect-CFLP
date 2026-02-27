using System;
using System.Collections.Generic;
using System.Linq;

namespace JocDeCruce
{
    public class Jucator
    {
        public string Nume { get; set; }
        public List<Card> Mana { get; set; }
        public int Licitatie { get; set; }

        public Jucator(string nume)
        {
            Nume = nume;
            Mana = new List<Card>();
            Licitatie = 0;
        }

        public void AfiseazaMana()
        {
            Console.WriteLine($"\n{Nume} are urmatoarele carti:");
            for (int i = 0; i < Mana.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {Mana[i]}");
            }
        }

        public void Liciteaza()
        {
            Console.Write($"{Nume}, cate puncte licitezi? (0-4): ");
            int licitatie; 
            
            while (!int.TryParse(Console.ReadLine(), out licitatie) || licitatie < 0 || licitatie > 4)
            {
                Console.Write("Introdu un numar de la 0 la 4: ");
            }
            
            Licitatie = licitatie;
        }
        
        public Card JoacaCarte(Culoare? tromf, Culoare? culoareCeruta)
        {
            AfiseazaMana();
            Console.Write($"{Nume}, alege o carte (1-{Mana.Count}): ");
            
            int alegere;
            while (!int.TryParse(Console.ReadLine(), out alegere) || alegere < 1 || alegere > Mana.Count)
            {
                Console.Write($"Alege un număr valid (1-{Mana.Count}): ");
            }

            Card carteAleasa = Mana[alegere - 1];
            Mana.RemoveAt(alegere - 1);
            return carteAleasa;
        }
    }
}