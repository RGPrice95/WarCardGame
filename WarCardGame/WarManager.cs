using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

/// <summary>
/// Author: Robert Price
/// The class which handles the game logic for the card game "War".
/// 
/// Class contains a player class, which will hold player names, and the players current deck of cards. Contains methods for altering player name,
/// and the players deck. "playerCards" variable should rarely be modified directly, and is only accessed directly once to initialize its value.
/// 
/// The only functions that need to be publically accessed for the WarManager class is "StartGame", which will set everything up and then begin the player turns, and "ResetGame" which offers to play again and re-calls "StartGame".
/// </summary>

namespace WarCardGame
{
    class Player
    {
        public List<Card> playerCards;
        private string playerName;

        public string PlayerName
        {
            get
            {
                return playerName;
            }
            set
            {
                playerName = value;
            }
        }

        public void ClearPlayerCards()
        {
            if (playerCards != null)
                playerCards.Clear();
        }

        public int PlayerCardCount
        {
            get
            {
                if (playerCards != null)
                    return playerCards.Count();

                return 0;
            }
        }

        public void AddToDeck(Card card)
        {
            playerCards.Add(card);
        }

        //Overload so we can add a range of cards
        public void AddToDeck(List<Card> cards)
        {
            playerCards.AddRange(cards);
        }

        //Method to handle drawing a card from the player hand
        //List added as a parameter to keep track of where cards are going
        public Card DrawCard(List<Card> drawTo)
        {
            //Nothing left, return null, add nothing to the list to draw too
            if (playerCards.Count <= 0)
                return null;

            Card toDraw = playerCards[0];
            playerCards.RemoveAt(0);
            drawTo.Add(toDraw);
            return toDraw;
        }
    }

    class WarManager
    {
        Player[] players = new Player[2];

        public WarManager()
        {
            //Initalize the players
            players[0] = new Player();
            players[1] = new Player();
        }

        public void StartGame()
        {            
            for(int i = 0; i < players.Length; i++)
            {
                //Ensure something was typed
                while (string.IsNullOrEmpty(players[i].PlayerName))
                {
                    //In case this is another playthrough, clear out the data
                    players[i].ClearPlayerCards();
                    players[i].PlayerName = "";

                    Console.WriteLine("Please enter a name for Player " + (i + 1));
                    players[i].PlayerName = Console.ReadLine();
                }
            }

            //Deck manager constructor will create a deck of 52 cards, and shuffle it
            DeckManager deckManager = new DeckManager();
            //Split the deck for each player
            deckManager.SplitDeck(out players[0].playerCards, out players[1].playerCards);

            //Being playing the game
            StartTurns();
        }

        void StartTurns()
        {
            //The pool of cards the winning player will get at the end of the turn, each players drawn cards are added to here
            List<Card> winnerPool = new List<Card>();
            //Used to automate game so player does not need to hit the key between every turn
            bool runToEnd = false;

            //Run a loop until either player runs out of cards
            while (players[0].PlayerCardCount > 0 && players[1].PlayerCardCount > 0)
            {
                Card playerOneDraw = players[0].DrawCard(winnerPool);
                Card playerTwoDraw = players[1].DrawCard(winnerPool);
                Console.WriteLine(string.Format("{0} plays:\t{1} plays:", players[0].PlayerName, players[1].PlayerName));
                MultiCardPrint(playerOneDraw, playerTwoDraw);

                while (playerOneDraw.cardValue == playerTwoDraw.cardValue)
                {
                    PrintWar();
                    //get the first two cards off each players hand into the pool
                    foreach (Player player in players)
                    {
                        for (int i = 0; i <= 1; i++)
                           player.DrawCard(winnerPool);
                    }

                    //Now pull the third draw, this is what will determine the winner
                    playerOneDraw = players[0].DrawCard(winnerPool);
                    playerTwoDraw = players[1].DrawCard(winnerPool);

                    //Someone ran out of cards - break out to outer loop
                    if (playerOneDraw == null || playerTwoDraw == null)
                        break;

                    Console.WriteLine(string.Format("{0} plays:\t{1} plays:", players[0].PlayerName, players[1].PlayerName));
                    //Offset the cards print position by the length of the player name - and tack on 7 to account for " plays:"
                    MultiCardPrint(playerOneDraw, playerTwoDraw);

                }

                //Shuffle the winnings each time - this helps prevent the game from going into stalemates
                ShuffleWinnings(ref winnerPool);

                if (playerOneDraw == null || playerTwoDraw == null)
                {
                    //Determine who gets the pot by checking who drew up nothing
                    Player winner = playerOneDraw == null ? players[1] : players[0];
                    Player loser = playerOneDraw == null ? players[0] : players[1];
                    Console.WriteLine(string.Format("{0} has run out of cards!", loser.PlayerName));
                    winner.AddToDeck(winnerPool);
                    break;
                }

                if (playerOneDraw.cardValue > playerTwoDraw.cardValue)
                {
                    Console.WriteLine(players[0].PlayerName + " wins the round");
                    players[0].AddToDeck(winnerPool);
                }
                else
                {
                    Console.WriteLine(players[1].PlayerName + " wins the round");
                    players[1].AddToDeck(winnerPool);
                }

                winnerPool.Clear();
                foreach (Player player in players)
                    Console.WriteLine(string.Format("{0} has {1} cards remaining", player.PlayerName, player.PlayerCardCount));

                Console.WriteLine("= = = = = = = = = = =");
                if (!runToEnd)
                {
                    TurnPause(ref runToEnd);
                }else
                {
                    /*
                     * Thread.Sleep is generally a bad idea, but for something simple like this application its fine.
                     * Ideally, if we had something with an involved UI, we would do waiting in an async method, and notify the main thread when completed
                     * so that the application does not lock up while waiting on something to be done.
                     */
                    Thread.Sleep(50);
                }
            }


            //End of Game - Determine Winner by seeing who has more than zero card in hand
            Player winningPlayer = players[0].PlayerCardCount > 0 ? players[0] : players[1];
            Console.WriteLine(string.Format("{0} wins with {1} cards!!", winningPlayer.PlayerName, winningPlayer.PlayerCardCount));
        }

        public void MultiCardPrint(Card cardA, Card cardB)
        {
            string[] a = cardA.GetAsciiCard();
            string[] b = cardB.GetAsciiCard();
            //For helping center the cards a bit more
            string offset = new string(' ', a[0].Length / 2);
            //Cards print value always share the same length, so we can just grab it from one of them
            for (int i = 0; i < a.Length; i++)
            {
                Console.Write(offset + a[i] + "\t" + offset + b[i] + "\n");
            }
        }

        //Just something fun to add some drama
        void PrintWar()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write('W');
            Thread.Sleep(250);
            Console.Write('A');
            Thread.Sleep(250);
            Console.Write('R');
            Thread.Sleep(250);
            Console.Write('!');
            Console.Write('\n');
            Thread.Sleep(250);
            Console.ResetColor();
        }

        Random rnd = new Random();
        void ShuffleWinnings(ref List<Card> winnerPool)
        {
            int cardCount = winnerPool.Count;
            while (cardCount > 1)
            {
                cardCount--;
                int rndIndex = rnd.Next(cardCount + 1);
                Card drawnCard = winnerPool[rndIndex];
                winnerPool[rndIndex] = winnerPool[cardCount];
                winnerPool[cardCount] = drawnCard;
            }
        }

        void TurnPause(ref bool RunToEnd)
        {
            Console.WriteLine("Press Any Key To Continue.\nPress Enter To Run Game To End");
            var pressed = Console.ReadKey();
            if (pressed.Key == ConsoleKey.Enter)
                RunToEnd = true;
        }


        public bool ResetGame()
        {
            Console.WriteLine("Press Any Key To Exit.\nPress Enter To Play Again");
            
            players[0] = new Player();
            players[1] = new Player();

            var pressed = Console.ReadKey();
            if (pressed.Key == ConsoleKey.Enter)
            {
                return true;
            }

            return false;
        }
    }
}
