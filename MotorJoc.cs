using System;
using System.Collections.Generic;
using System.Linq;

namespace JocDeCruce
{
    public class MotorJoc
    {
        private List<Jucator> jucatori;
        private Culoare? tromf;
        private int jucatorCurent;
        private int[] puncteEchipe; 

        public MotorJoc()
        {
            jucatori = new List<Jucator>
            {
                new Jucator("Jucator 1"),
                new Jucator("Jucator 2"),
                new Jucator("Jucator 3"),
                new Jucator("Jucator 4")
            };
            puncteEchipe = new int[2];
            tromf = null;
            jucatorCurent = 0;
        }

        public void Start()
        {
            Console.WriteLine(" JOC DE CRUCE \n");
            Console.WriteLine("Echipa 1: Jucator 1 & Jucator 3");
            Console.WriteLine("Echipa 2: Jucator 2 & Jucator 4\n");

            Pachet pachet = new Pachet();
            pachet.Amesteca();
            List<List<Card>> maini = pachet.Distribuie();

            for (int i = 0; i < 4; i++)
            {
                jucatori[i].Mana = maini[i];
            }

            Console.WriteLine("\nDISTRIBUIRE CARTI:");
            foreach (var jucator in jucatori)
            {
                jucator.AfiseazaMana();
            }

            FaceLicitatie();

            for (int tura = 1; tura <= 6; tura++)
            {
                Console.WriteLine($"\n TURA {tura} ");
                JoacaTura();
            }

            AfiseazaRezultat();
        }

        private int echipaCareLiciteaza = -1; 
        private int puncteMareLicitate = 0;

        private void FaceLicitatie()
        {
            Console.WriteLine("\n LICITATIE ");
            
            foreach (var jucator in jucatori)
            {
                jucator.Liciteaza();
            }

            int maxLicitatie = jucatori.Max(j => j.Licitatie);
            var castigatorLicitatie = jucatori.First(j => j.Licitatie == maxLicitatie);
            jucatorCurent = jucatori.IndexOf(castigatorLicitatie);

            echipaCareLiciteaza = jucatorCurent % 2;
            puncteMareLicitate = maxLicitatie;

            Console.WriteLine($"\n{castigatorLicitatie.Nume} a castigat licitatia cu {maxLicitatie} puncte mari!");
            Console.WriteLine($"Echipa {echipaCareLiciteaza + 1} trebuie sa facă cel putin {puncteMareLicitate * 33} puncte.");
            Console.WriteLine("Prima carte jucată va stabili tromful.");
        }

        private void JoacaTura()
        {
            List<Card> cartiJucate = new List<Card>();
            List<int> ordineJucatori = new List<int>();
            Culoare culoareCeruta = Culoare.Rosu; 

            for (int i = 0; i < 4; i++)
            {
                int indexJucator = (jucatorCurent + i) % 4;
                Jucator jucator = jucatori[indexJucator];

                Console.WriteLine($"\n--- {jucator.Nume} ---");
                Card carte = jucator.JoacaCarte(tromf, i == 0 ? null : culoareCeruta);
                
                if (tromf == null && i == 0)
                {
                    tromf = carte.Culoare;
                    Console.WriteLine($"\n TROMF STABILIT: {tromf} ");
                }

                if (i == 0)
                {
                    culoareCeruta = carte.Culoare;
                }

                cartiJucate.Add(carte);
                ordineJucatori.Add(indexJucator);
                
                Console.WriteLine($"{jucator.Nume} a jucat: {carte}");
            }

            int indexCastigator = DeterminaCastigator(cartiJucate, culoareCeruta);
            int jucatorCastigator = ordineJucatori[indexCastigator];

            Console.WriteLine($"\n {jucatori[jucatorCastigator].Nume} castiga tura.");

            int puncteTura = cartiJucate.Sum(c => c.Puncte());
            int echipaCastigatoare = jucatorCastigator % 2;
            puncteEchipe[echipaCastigatoare] += puncteTura;

            Console.WriteLine($"Puncte in tura: {puncteTura}");
            Console.WriteLine($"Echipa {echipaCastigatoare + 1} primeste punctele.");

            jucatorCurent = jucatorCastigator;
        }

        private int DeterminaCastigator(List<Card> carti, Culoare culoareCeruta)
        {
            int indexCastigator = 0;
            Card carteCastigatoare = carti[0];

            for (int i = 1; i < carti.Count; i++)
            {
                if (EsteMaiBuna(carti[i], carteCastigatoare, culoareCeruta))
                {
                    carteCastigatoare = carti[i];
                    indexCastigator = i;
                }
            }

            return indexCastigator;
        }

        private bool EsteMaiBuna(Card carte1, Card carte2, Culoare culoareCeruta)
        {
            bool carte1EsteTromf = carte1.Culoare == tromf;
            bool carte2EsteTromf = carte2.Culoare == tromf;

            if (carte1EsteTromf && !carte2EsteTromf)
                return true;
            if (!carte1EsteTromf && carte2EsteTromf)
                return false;

            if (carte1EsteTromf && carte2EsteTromf)
                return carte1.Valoare > carte2.Valoare;

            bool carte1CuloareCorecta = carte1.Culoare == culoareCeruta;
            bool carte2CuloareCorecta = carte2.Culoare == culoareCeruta;

            if (carte1CuloareCorecta && !carte2CuloareCorecta)
                return true;
            if (!carte1CuloareCorecta && carte2CuloareCorecta)
                return false;

            if (carte1CuloareCorecta && carte2CuloareCorecta)
                return carte1.Valoare > carte2.Valoare;

            return false;
        }

        private void AfiseazaRezultat()
        {
            Console.WriteLine("\n\n REZULTAT FINAL: ");
            Console.WriteLine($"Echipa 1 (Jucator 1 & Jucator 3): {puncteEchipe[0]} puncte");
            Console.WriteLine($"Echipa 2 (Jucator 2 & Jucator 4): {puncteEchipe[1]} puncte");

            int puncteNecesare = puncteMareLicitate * 33;
            int puncteEchipaCareLiciteaza = puncteEchipe[echipaCareLiciteaza];
            
            Console.WriteLine($"\nEchipa {echipaCareLiciteaza + 1} a licitat {puncteMareLicitate} puncte mari ({puncteNecesare} puncte)");
            Console.WriteLine($"Echipa {echipaCareLiciteaza + 1} a făcut {puncteEchipaCareLiciteaza} puncte");
            
            if (puncteEchipaCareLiciteaza >= puncteNecesare)
            {
                Console.WriteLine($" Echipa {echipaCareLiciteaza + 1} si-a indeplinit licitatia!");
                
                if (puncteEchipe[0] > puncteEchipe[1])
                    Console.WriteLine("\n ECHIPA 1 CASTIGA!");
                else if (puncteEchipe[1] > puncteEchipe[0])
                    Console.WriteLine("\n ECHIPA 2 CASTIGA!");
                else
                    Console.WriteLine("\n EGALITATE!");
            }
            else
            {
                Console.WriteLine($" Echipa {echipaCareLiciteaza + 1} NU si-a indeplinit licitația!");
                
                int echipaAdversa = echipaCareLiciteaza == 0 ? 1 : 0;
                Console.WriteLine($"\n ECHIPA {echipaAdversa + 1} CASTIGA AUTOMAT!");
            }
        }
    }
}