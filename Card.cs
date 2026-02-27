namespace JocDeCruce
{
    public enum Culoare
    {
        Rosu,
        Duba,
        Verde,
        Ghinda
    }

    public class Card
    {
        public Culoare Culoare { get; set; }
        public int Valoare { get; set; } 

        public Card(Culoare culoare, int valoare)
        {
            Culoare = culoare;
            Valoare = valoare;
        }

        public int Puncte()
        {
            return Valoare switch
            {
                11 => 11,  
                10 => 10,  
                4 => 4,    
                3 => 3,    
                2 => 2,    
                9 => 0,    
                _ => 0
            };
        }

        public override string ToString()
        {
            string valoareStr = Valoare == 11 ? "As" : Valoare.ToString();
            return $"{valoareStr} de {Culoare}";
        }
    }
}