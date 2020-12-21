using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackWpf.Model
{
    class Card
    {
        private string _rank;
        private int _value;
        
        private string _suit;


        public Card(string _rank, int _value, string _suit)
        {
            Rank = _rank;
            Value = _value;
            Suit = _suit;
        }

        public Card()
        {

        }
        public string Rank 
        {
            get { return _rank; }
            set { _rank = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Suit
        {
            get { return _suit; }
            set { _suit = value; }
        }

        public Card PrintCard()
        {
            Random rand = new Random();
            string[] rank = new string[] { "A", "K", "Q", "J", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
            string[] suits = new string[] { "H", "C", "D", "S" };

            
            int indexRank = rand.Next(0, rank.Length);
            int indexSuit = rand.Next(0, suits.Length);

            string r = rank[indexRank];
            string s = suits[indexSuit];

            int value = 0;
            if (r.Equals("A") || r.Equals("K") || r.Equals("Q") || r.Equals("J"))
            {
                value = ConvertRank(r);
            }
            else
            {
                value = Convert.ToInt32(r.ToString());
            }

            Card card = new Card(r, value, s);

            return card;
            
        }

        public int ConvertRank(string rank)
        {
            int value = 0;
            if (rank.Equals("A"))
            {
                value = 11;
                
            }
            else
            {
                value = 10;
                
            }
            return value;
            
        }


    }
}
