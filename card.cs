using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using blackjackOOP.Enums;

namespace blackjackOOP
{
    public class Card
    {
        private Rank rank;
        private Suit suit;
        private bool isfacedown;

        public int Value
        {
            get
            {
                switch (rank)
                {
                    case Rank.JACK:
                    case Rank.QUEEN:
                    case Rank.KING:
                        return 10;

                    case Rank.ACE:
                        return 11; // later kun je 1 of 11 maken

                    default:
                        return (int)rank;
                }
            }
        }

        public Card(Rank rank, Suit suit)
        {
            this.rank = rank;
            this.suit = suit;
            this.isfacedown = false;
        }



        public void Flip()
        {

        }

        public override string ToString()
        {
            return rank.ToString() + " OF " + suit.ToString();
        }
    }
}
