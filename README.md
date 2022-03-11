# WarCardGame
Console application which simulates the card game "War", written entirely in C#.

Contains three classes outside of the entry point, while one is specific to this card game (War), the DeckManager and Card classes enable this code to be fairly scalable in supporting more than just War. 

<b>DeckManager.cs:</b>
This class will generate a standard 52 deck of cards (No Jokers) for you comprised of objects created from Card.cs. 
The constructor for this class will generate the cards using "InitializeDeck", in this method you can alter the values for face-cards depending on what game you are playing.
Additionally, it will shuffle the list of cards it makes after the deck is created using the "ShuffleDeck" method, so you may begin using it for any game.

<b>Card.cs:</b>
Contains any relevant information for playing cards so they may be re-used for any card game. The constructor for this class will set the playing cards value for the game, as well as its suit and what type of face card it is.
Additionally, this class has two methods for getting it as a string value, the "ToString" method has been overriden to just print the details of the card, while printing the array of "GetAsciiCard"
will allow you to have a visual representation of the card.

<b>WarManager.cs</b>
Contains all the game logic for the card game "War". Has a self-contained Player class, that keeps track of the players name, and what cards they are using. Despite being publically exposed, the player cards list should not be modified directly outside of intializing it (currently done using the "SplitDeck" method in DeckManager.cs). The methods used for accessing player cards in this class will help keep track of where cards should go in the event they are removed from the players hand.
The constructor for this class will initalize two players. To begin the game, use the publically accessible "StartGame" method, which will allow you to enter player names, and then begin the match using the "StartTurns" method.





