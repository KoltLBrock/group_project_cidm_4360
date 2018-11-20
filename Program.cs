using System;
using System.Collections.Generic;
using System.Linq;
namespace skelecode
{
    class Program
    {
        static void Main(string[] args){
            //runtime for game state
            int runtime = 1;
            //the players (could be made modular, requires lots of new code)
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            //numPlayers could be modified/requires much more code
            int numPlayers = 2;
            //counts turns for debug purposes
            int turns = 0;
            //the deck object
            Deck playDeck = new Deck(numPlayers);
            //shuffles the deck before dealing
            playDeck.shuffle();
            //playDeck.displayshuffledDeck();
            //splits the deck up for the numbers of players
            List<List<string>> decks = playDeck.divideDeck(numPlayers);
            player1.setCards(decks.ElementAt(0));
            player2.setCards(decks.ElementAt(1));
            //shows cards the players have, for debug
            //player1.displayCards();
            //player2.displayCards();
            //List<string> playinglist = new List<string>{};
            //main game loop here 
            //compares the cards of the two players and runs them through the war rules
            while(runtime == 1){
                if(player1.getCards().Count() == 0 || player2.getCards().Count() == 0){
                    Console.WriteLine("The Game has ended.");
                    if(player1.getCards().Count() == 52){
                        Console.WriteLine("Player 1 won.");
                        Console.WriteLine(turns);
                        runtime = 0;
                        break;
                    }else if(player2.getCards().Count() == 52){
                        Console.WriteLine("Player 2 won.");
                        Console.WriteLine(turns);
                        runtime = 0;
                        break;
                    }else{
                        //debug stuff
                        Console.WriteLine("Error Early.");
                        player1.displayCards();
                        player2.displayCards();
                        Console.WriteLine(turns);
                        runtime = 0;
                        break;
                    }
                }
                turns++;
                string p1fullcard = player1.drawCard();
                string p2fullcard = player2.drawCard();
                int p1card = Convert.ToInt16(p1fullcard.Split(" ").First());
                int p2card = Convert.ToInt16(p2fullcard.Split(" ").First());
                if(p1card > p2card){
                    Console.WriteLine("Player 1 wins the battle!");
                    player1.removeCard(p1fullcard);
                    player2.removeCard(p2fullcard);
                    player1.addCard(p1fullcard);
                    player1.addCard(p2fullcard);
                    player1.shuffle();
                    player2.shuffle();
                }else if(p2card > p1card){
                    Console.WriteLine("Player 2 wins the battle!");
                    player1.removeCard(p1fullcard);
                    player2.removeCard(p2fullcard);
                    player2.addCard(p1fullcard);
                    player2.addCard(p2fullcard);
                    player1.shuffle();
                    player2.shuffle();
                }else if(p1card == p2card){
                    
                    Console.WriteLine("WAR!");
                    player1.removeCard(p1fullcard);
                    player2.removeCard(p2fullcard);
                    if(player1.getCards().Count() == 0 || player2.getCards().Count() == 0){
                        if(player1.getCards().Count() == 0)
                        Console.WriteLine("Player 1 has lost due to running out of cards.");
                        if(player2.getCards().Count() == 0)
                        Console.WriteLine("Player 2 has lost due to running out of cards.");
                        break;
                    }
                    string p1fullcardwar1 = player1.drawCard();
                    string p2fullcardwar1 = player2.drawCard();
                    int p1cardwar1 = Convert.ToInt16(p1fullcardwar1.Split(" ").First());
                    int p2cardwar1 = Convert.ToInt16(p2fullcardwar1.Split(" ").First());
                    if(p1cardwar1 > p2cardwar1){
                        Console.WriteLine("Player 1 won that war!");
                        player1.removeCard(p1fullcardwar1);player2.removeCard(p2fullcardwar1);
                        player1.addCard(p1fullcard);player1.addCard(p2fullcard);player1.addCard(p1fullcardwar1);player1.addCard(p2fullcardwar1);
                        player1.shuffle();
                        player2.shuffle();
                    }else if(p1cardwar1 < p2cardwar1){
                        Console.WriteLine("Player 2 won that war!");
                        player1.removeCard(p1fullcardwar1);player2.removeCard(p2fullcardwar1);
                        player2.addCard(p1fullcard);player2.addCard(p2fullcard);player2.addCard(p1fullcardwar1);player2.addCard(p2fullcardwar1);
                        player1.shuffle();
                        player2.shuffle();
                    }else{
                        Console.WriteLine("There are no winners in the game of war.");
                        player1.addCard(p1fullcard);//player1.addCard(p1fullcardwar1);
                        player2.addCard(p2fullcard);//player2.addCard(p2fullcardwar1);
                        player1.shuffle();
                        player2.shuffle();
                    }
                    //continue here
                }else{
                    Console.WriteLine("The Game has ended.");
                    if(player1.getCards().Count() == 52){
                        Console.WriteLine("Player 1 won.");
                        runtime = 0;
                    }else if(player2.getCards().Count() == 52){
                        Console.WriteLine("Player 2 won.");
                        runtime = 0;
                    }else{
                        Console.WriteLine("Error late.");
                        runtime = 0;
                    }
                }
            }
        }
    }
    //deck class
    class Deck{
        //starting deck with ordered 52 cards
        private static List<string> STARTINGDECK = new List<string>
        {"14 Ace of Spades","14 Ace of Hearts","14 Ace of Clubs","14 Ace of Diamonds"
        ,"2 of Spades","2 of Hearts","2 of Clubs","2 of Diamonds"
        ,"3 of Spades","3 of Hearts","3 of Clubs","3 of Diamonds"
        ,"4 of Spades","4 of Hearts","4 of Clubs","4 of Diamonds"
        ,"5 of Spades","5 of Hearts","5 of Clubs","5 of Diamonds"
        ,"6 of Spades","6 of Hearts","6 of Clubs","6 of Diamonds"
        ,"7 of Spades","7 of Hearts","7 of Clubs","7 of Diamonds"
        ,"8 of Spades","8 of Hearts","8 of Clubs","8 of Diamonds"
        ,"9 of Spades","9 of Hearts","9 of Clubs","9 of Diamonds"
        ,"10 of Spades","10 of Hearts","10 of Clubs","10 of Diamonds"
        ,"11 Jack of Spades","11 Jack of Hearts","11 Jack of Clubs","11 Jack of Diamonds"
        ,"12 Queen of Spades","12 Queen of Hearts","12 Queen of Clubs","12 Queen of Diamonds"
        ,"13 King of Spades","13 King of Hearts","13 King of Clubs","13 King of Diamonds"};
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
        //divides deck into chunks
        public List<List<string>> divideDeck(int playerNumber){
            int x = shuffledDeck.Count()/playerNumber;
            var list = new List<List<string>>();
            for (int i = 0; i < shuffledDeck.Count; i += 26)
                list.Add(shuffledDeck.GetRange(i, Math.Min(26, shuffledDeck.Count - i)));
                return list;
        }
        //for debug
        public void displayshuffledDeck(){
            foreach(string i in shuffledDeck){
                Console.WriteLine(i);
            }
        }
    }
    //player class
    class Player{
        private List<string> playerDeck = new List<string>();
        private string playerName;
        public Player(string name){
            playerName = name;
        }
        public void shuffle(){
            //this takes the starting deck and shuffles it, being unique each time
            playerDeck = playerDeck.OrderBy(x => Guid.NewGuid()).ToList();
        }
        public string drawCard(){
            return playerDeck.ElementAt(0);
        }
        public void setCards(List<string> list){
            playerDeck = list;
        }
        public void addCard(string card){
            playerDeck.Add(card);
        }
        public void removeCard(string card){
            playerDeck.Remove(card);
        }
        public void displayCards(){
            Console.WriteLine("Player's Cards");
            foreach(string i in playerDeck){
                Console.WriteLine(i);
            }
        }
        public List<string> getCards(){
            return playerDeck;
        }
    }
}