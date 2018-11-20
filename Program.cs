using System;
using System.Collections.Generic;
using System.Linq;
namespace skelecode
{
    class Program
    {
        static void Main(string[] args)
        {
            int numPlayers = 2;
            Deck playDeck = new Deck(numPlayers);
            playDeck.shuffle();
            playDeck.displayshuffledDeck();

        }
    }
    class Deck{
        //starting deck with ordered 52 cards
        private static List<string> STARTINGDECK = new List<string>
        {"Ace of Spades","Ace of Hearts","Ace of Clubs","Ace of Diamonds"
        ,"2 of Spades","2 of Hearts","2 of Clubs","2 of Diamonds"
        ,"3 of Spades","3 of Hearts","3 of Clubs","3 of Diamonds"
        ,"4 of Spades","4 of Hearts","4 of Clubs","4 of Diamonds"
        ,"5 of Spades","5 of Hearts","5 of Clubs","5 of Diamonds"
        ,"6 of Spades","6 of Hearts","6 of Clubs","6 of Diamonds"
        ,"7 of Spades","7 of Hearts","7 of Clubs","7 of Diamonds"
        ,"8 of Spades","8 of Hearts","8 of Clubs","8 of Diamonds"
        ,"9 of Spades","9 of Hearts","9 of Clubs","9 of Diamonds"
        ,"10 of Spades","10 of Hearts","10 of Clubs","10 of Diamonds"
        ,"Jack of Spades","Jack of Hearts","Jack of Clubs","Jack of Diamonds"
        ,"Queen of Spades","Queen of Hearts","Queen of Clubs","Queen of Diamonds"
        ,"King of Spades","King of Hearts","King of Clubs","King of Diamonds"};
        private List<string> shuffledDeck = new List<string>{};
        Random value = new Random();
        public void shuffle(){
            //this takes the starting deck and shuffles it, being unique each time
            shuffledDeck = STARTINGDECK.OrderBy(x => Guid.NewGuid()).ToList();
        }
        private int playerNumber;
        public Deck(int numPlayers){
            playerNumber = numPlayers;
        }
        public void divideDeck(int playerNumber, List<string> shuffledDeck){

        }
        public void displayshuffledDeck(){
            foreach(string i in shuffledDeck){
                Console.WriteLine(i);
            }
        }
    }
    class Player{
        private List<string> playerDeck = new List<string>();
        private string playerName;
        public Player(string name){
            playerName = name;
        }
        public void addCard(string card){
            playerDeck.Add(card);
        }
        public void displayCards(){
            foreach(string i in playerDeck){
                Console.WriteLine(i);
            }
        }
    }
}