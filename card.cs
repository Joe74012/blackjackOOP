using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blackjackOOP
{
    public enum Rank 
    { 
        ACE = 1,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING
    }

    public enum Suit 
    { 
        HEARTS,
        CLUBS,
        DIAMONDS,
        SPADES
    }
    public class Card
    {
        private Rank rank;
        private int cardValue;
        private Suit suit;
        private bool isfacedown;

        public int Value
        {
            get {
                switch(rank)
                {
                    case Rank.TWO:
                        cardValue = (int)2;
                        break;
                    case Rank.JACK:
                    case Rank.QUEEN:
                    case Rank.KING:
                        Value = 10;
                        break;
                }
                if (rank == Rank.TWO)
                {
                    cardValue = 2;
                }
                return cardValue; 
            }
            set {
                if(value < 5)
                {
                    this.cardValue = value;
                }
            }
        }

        public Card(Rank rank, Suit suit) 
        { 
            this.rank = rank;
            this.suit = suit;
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
