using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Author: Robert Price
/// The main deck class - Creates a standard 52 deck of playing cards, using the "Card" class
/// 
/// No extra methods need to be called in this class - as the constructor will both initalize the deck of cards, and set their values
/// and additionally shuffle the deck once it is created using the "ShuffleDeck" method.
/// 
/// For different card games, card values can be altered in the "Initalize Deck" method using the "Card" constructors parameters.
/// 
/// </summary>

namespace WarCardGame
{
    //52 Card in a Deck
    //No Joker Cards
    //4 suits - 13 cards per suit
    //2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace
    //Hearts, Clubs, Dimaonds Spades

    //Should look into making this a singleton(?) - Maybe not, could want multiple decks for some reason or another for re-usability
    class DeckManager
    {
        List<Card> Deck = new List<Card>();


        public DeckManager()
        {
            InitializeDeck();
            ShuffleDeck();
        }

        void InitializeDeck()
        {
            //Iterate over each suit option, add the card options for each suit into the deck
            //Use a switch to account for face cards and their values - The values from lowest to highest are: 2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, A
            foreach (var suit in Enum.GetValues(typeof(Suit)))
            {
                for(int i = 1; i <= 13; i++)
                {
                    Card deckCard = null;
                    switch (i)
                    {
                        case 1:
                            deckCard = new Card(14, (Suit)suit, FaceCard.Ace);
                            break;
                        case 11:
                            deckCard = new Card(11, (Suit)suit, FaceCard.Jack);
                            break;
                        case 12:
                            deckCard = new Card(12, (Suit)suit, FaceCard.Queen);
                            break;
                        case 13:
                            deckCard = new Card(13, (Suit)suit, FaceCard.King);
                            break;
                        default:
                            deckCard = new Card(i, (Suit)suit, FaceCard.Number);
                            break;
                    }
                    Deck.Add(deckCard);
                }
            }
        }

        Random rnd = new Random();

        void ShuffleDeck()
        {

            int cardCount = Deck.Count;
            while (cardCount > 1)
            {
                cardCount--;
                int rndIndex = rnd.Next(cardCount + 1);
                Card drawnCard = Deck[rndIndex];
                Deck[rndIndex] = Deck[cardCount];
                Deck[cardCount] = drawnCard;
            }
        }

        public void SplitDeck(out List<Card> firstHalf, out List<Card> secondHalf)
        {
            firstHalf = Deck.Take(Deck.Count / 2).ToList();
            secondHalf = Deck.Skip(Deck.Count / 2).ToList();
            
            //Clear out the deck, all cards were taken
            Deck.Clear();
        }

    }
}
