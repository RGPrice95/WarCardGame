using System;
using System.Linq;

/// <summary>
/// Author: Robert Price
/// The main card class - this holds all the data for a playing card within a standard 52 card deck (No Jacks)
/// Cards can be printed in two ways, using the overriden "ToString" method, which will print out the cards information - 
/// or by printing the "AsciiGraphic" line by line for a graphical representation.
/// 
/// All values for each card are set in the constructor, so that in cases between games, such as War or Blackjack, the
/// numerical value of the card can be altered to suit the game
/// </summary>

namespace WarCardGame
{    
    enum FaceCard
    {
        Number,
        King,
        Queen,
        Jack,
        Ace
    }

    enum Suit
    {
        Heart,
        Club,
        Diamond,
        Spade
    }

    class Card
    {
        private int m_cardValue;
        private string[] m_asciiGraphic;
        private Suit m_suit;
        private FaceCard m_faceCard;

        public Card(int cardValue, Suit suit, FaceCard faceCard)
        {
            m_cardValue = cardValue;
            m_suit = suit;
            m_faceCard = faceCard;
            SetAsciiValue();
        }

        public string[] GetAsciiCard()
        {
            return m_asciiGraphic;
        }

        public int cardValue
        {
            get
            {
                return m_cardValue;
            }
        }

        public override string ToString()
        {
            if(m_faceCard != FaceCard.Number)
            {
                return string.Format("{0} of {1}'s", m_faceCard, m_suit);
            }
            else
            {
                return string.Format("{0} of {1}'s", m_cardValue, m_suit);
            }
        }

        public void PrintFaceCard()
        {
            for (int i = 0; i < m_asciiGraphic.Length; i++)
                Console.WriteLine(m_asciiGraphic[i]);
        }


        public void SetAsciiValue()
        {
            //FaceVal must be a string because we have double-digit numbers in some cases
            string faceVal = "";
            char suitVal = '?';
            switch (m_faceCard)
            {
                case FaceCard.Ace:
                    faceVal = "A";
                    break;
                case FaceCard.Jack:
                    faceVal = "J";
                    break;
                case FaceCard.King:
                    faceVal = "K";
                    break;
                case FaceCard.Queen:
                    faceVal = "Q";
                    break;
                case FaceCard.Number:
                    faceVal = m_cardValue.ToString();
                    break;
            }

            switch (m_suit)
            {
                case Suit.Club:
                    suitVal = '♣';
                    break;
                case Suit.Diamond:
                    suitVal = '♦';
                    break;
                case Suit.Heart:
                    suitVal = '♥';
                    break;
                case Suit.Spade:
                    suitVal = '♠';
                    break;

            }

            if (m_cardValue == 1)
            {
                m_asciiGraphic = new string[]                    
                {
                      "-----------",
                      "|V        |",                   
                      "|         |",
                      "|         |",
                      "|    S    |",
                      "|         |",
                      "|         |",
                      "|        V|",
                      "-----------"
                };
            }
            if (m_cardValue == 2)
            {
                m_asciiGraphic = new string[]
                {
                      "-----------" ,
                      "|V        |" ,
                      "|    S    |" ,
                      "|         |" ,
                      "|         |" ,
                      "|         |" ,
                      "|    S    |" ,
                      "|        V|" ,
                      "-----------"
                  };
            }
            if (m_cardValue == 3)
            {
                m_asciiGraphic = new string[]
                {
                     "-----------" ,
                     "|V        |" ,
                     "|    S    |" ,
                     "|         |" ,
                     "|    S    |" ,
                     "|         |" ,
                     "|    S    |" ,
                     "|        V|" ,
                     "-----------" 
                };
            }
            if (m_cardValue == 4)
            {
                m_asciiGraphic = new string[]
                {
                    "-----------" ,
                    "|V        |" ,
                    "|  S   S  |" ,
                    "|         |" ,
                    "|         |" ,
                    "|         |" ,
                    "|  S   S  |" ,
                    "|        V|" ,
                    "-----------"
                };
            }
            if (m_cardValue == 5)
            {
                m_asciiGraphic = new string[]
                {
                    "-----------" ,
                    "|V        |" ,
                    "|  S   S  |" ,
                    "|         |" ,
                    "|    S    |" ,
                    "|         |" ,
                    "|  S   S  |" ,
                    "|        V|" ,
                    "-----------"
                };
            }
            if (m_cardValue == 6)
            {
                m_asciiGraphic = new string[]
                {
                    "-----------" ,
                    "|V        |" ,
                    "|  S   S  |" ,
                    "|         |" ,
                    "|  S   S  |" ,
                    "|         |" ,
                    "|  S   S  |" ,
                    "|        V|" ,
                    "-----------"

                };
            }
            if (m_cardValue == 7)
            {
                m_asciiGraphic = new string[]
                {
                    "-----------" ,
                    "|V        |" ,
                    "|  S   S  |" ,
                    "|    S    |" ,
                    "|  S   S  |" ,
                    "|         |" ,
                    "|  S   S  |" ,
                    "|        V|" ,
                    "-----------"
                };
            }
            if (m_cardValue == 8)
            {
                m_asciiGraphic = new string[]
                {
                    "-----------" ,
                    "|V        |" ,
                    "|  S   S  |" ,
                    "|    S    |" ,
                    "|  S   S  |" ,
                    "|    S    |" ,
                    "|  S   S  |" ,
                    "|        V|" ,
                    "-----------"
                };
            }
            if (m_cardValue == 9)
            {
                m_asciiGraphic = new string[]
                {
                    "-----------" ,
                    "|V        |" ,
                    "|  S S S  |" ,
                    "|         |" ,
                    "|  S S S  |" ,
                    "|         |" ,
                    "|  S S S  |" ,
                    "|        V|" ,
                    "-----------"
                };
            }
            if (m_cardValue >= 10)
            {
                //Have to account for double digits by removing a space - But only applicable if card is numerical
                if (m_faceCard == FaceCard.Number)
                {
                    m_asciiGraphic = new string[]
{
                    "-----------" ,
                    "|V       |" ,
                    "|   S S   |" ,
                    "|    S    |" ,
                    "| S S S S |" ,
                    "|    S    |" ,
                    "|   S S   |" ,
                    "|       V|" ,
                    "-----------"
};
                }
                else
                {
                    m_asciiGraphic = new string[]
                    {
                    "-----------" ,
                    "|V        |" ,
                    "|   S S   |" ,
                    "|    S    |" ,
                    "| S S S S |" ,
                    "|    S    |" ,
                    "|   S S   |" ,
                    "|        V|" ,
                    "-----------"
                    };
                }
            }

            //Replace the characters so it looks nice 
            m_asciiGraphic = m_asciiGraphic.Select(s => s.Replace("V", faceVal)).ToArray();
            m_asciiGraphic = m_asciiGraphic.Select(s => s.Replace('S', suitVal)).ToArray();
        }
    }
}
