using System;
using System.Collections.Generic;
using System.Linq;
/* ==============================================================
 ==== Classes are below marked by large "=" areas similar to this
 ================================================================
 */
namespace skelecode
{
    class Program
    {
        static void Main(string[] args){
            //runtime for game state
            int runtime = 1;
            //option for turn-by-turn or automatic resolution
            int turnByTurn = 0;
            //the players (could be made modular, requires code rewrite in many places)
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
            /*===============================================================================================
            ============                           MAIN MENU                        =========================
            =================================================================================================
             */
            Console.WriteLine("*====================================================================================================================================*");
            Console.WriteLine("Welcome to the War card game, but virtual!");
            Console.WriteLine("You can play in 1 turn at a time with turn by turn mode, or you can get through the hassle quickly with automatic mode.");
            while(true){
                //main menu, allows user to change options of game, and start game
                int optionInput = 0;
                Console.WriteLine("*====================================================================================================================================*");
                Console.WriteLine("Type the number of the option you want: \n");
                Console.WriteLine("1. Play the game.");
                if(turnByTurn == 0)
                    Console.WriteLine("2. Change game mode to turn by turn.");
                if(turnByTurn == 1)
                    Console.WriteLine("2. Change game mode to automatic.");
                // catches potential user invald entry
                Console.Write("&GameMaster&: ");
                try{
                    optionInput = Convert.ToInt16(Console.ReadLine());
                }
                catch(Exception){
                    Console.WriteLine("Please enter numbers.");
                    optionInput = 0;
                }
                //this structure sets actions based on options
                if(optionInput == 0){
                    Console.WriteLine("Something went wrong or you entered an invalid number. Try Again.");
                }else if(optionInput == 2 && turnByTurn == 0){
                    turnByTurn = 1;
                    Console.WriteLine("Turn by turn mode enabled.");
                }else if(optionInput == 2 && turnByTurn == 1){
                    turnByTurn = 0;
                    Console.WriteLine("Turn by turn mode disabled.");
                }else if(optionInput == 1){
                    break;
                }else{
                    Console.WriteLine("Something went wrong or you entered an invalid number. Please Try Again.");
                }
            }
            //this writeline signifies the switch from the option loop to the game loop
            Console.WriteLine("The first shots has been fired:");
            /* ============================================================================================================
            ===================================        GAME LOOP                ===========================================
            ===============================================================================================================
             */
            //main game loop here 
            //compares the cards of the two players and runs them through the war rules
            while(runtime == 1){
                //this if statement catches players who have no cards, in which case the other player wins,
                //if there is a missing card for some reason the 'error early' happens
                if(player1.getCards().Count() == 0 || player2.getCards().Count() == 0){
                    Console.WriteLine("The Game has ended.");
                    if(player1.getCards().Count() == 52){
                        //player 1 win case
                        Console.WriteLine("Player 1 won.");
                        Console.WriteLine(turns);
                        runtime = 0;
                        break;
                    }else if(player2.getCards().Count() == 52){
                        //player 2 win case
                        Console.WriteLine("Player 2 won.");
                        Console.WriteLine(turns);
                        runtime = 0;
                        break;
                    }else{
                        //debug stuff (missing cards??)
                        Console.WriteLine("Error Early (this shouldnt't happen).");
                        player1.displayCards();
                        player2.displayCards();
                        Console.WriteLine(turns);
                        runtime = 0;
                        break;
                    }
                }
                //counts turns (average number of turns is around 700, this leads to a very long game in real life)
                turns++;
                //the players draw their cards and they are compared for the result of the war
                string p1fullcard = player1.drawCard();
                string p2fullcard = player2.drawCard();
                int p1card = Convert.ToInt16(p1fullcard.Split(" ").First());
                int p2card = Convert.ToInt16(p2fullcard.Split(" ").First());
                string throwAway = null;
                if(turnByTurn == 1){
                    throwAway = Convert.ToString(Console.ReadKey());
                }
                if(p1card > p2card){
                    //player 1 recieves player 2's card
                    Console.WriteLine("Player 1 wins the battle!");
                    player1.removeCard(p1fullcard);
                    player2.removeCard(p2fullcard);
                    player1.addCard(p1fullcard);
                    player1.addCard(p2fullcard);
                    player1.shuffle();
                    player2.shuffle();
                }else if(p2card > p1card){
                    //player 2 recieves player 1's card
                    Console.WriteLine("Player 2 wins the battle!");
                    player1.removeCard(p1fullcard);
                    player2.removeCard(p2fullcard);
                    player2.addCard(p1fullcard);
                    player2.addCard(p2fullcard);
                    player1.shuffle();
                    player2.shuffle();
                }else if(p1card == p2card){
                    // war is when the players cards match 
                    Console.WriteLine("WAR!");
                    if(turnByTurn == 1){
                        throwAway = Convert.ToString(Console.ReadKey());
                    }
                    player1.removeCard(p1fullcard);
                    player2.removeCard(p2fullcard);
                    if(player1.getCards().Count() == 0 || player2.getCards().Count() == 0){
                        //if one of the players runs out of cards due to a war, they lose the war due to insufficient support
                        //and thus they get outflanked
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
                        //player 1 recieves the cards used in the war
                        Console.WriteLine("Player 1 won that war!");
                        player1.removeCard(p1fullcardwar1);player2.removeCard(p2fullcardwar1);
                        player1.addCard(p1fullcard);player1.addCard(p2fullcard);player1.addCard(p1fullcardwar1);player1.addCard(p2fullcardwar1);
                        player1.shuffle();
                        player2.shuffle();
                    }else if(p1cardwar1 < p2cardwar1){
                        //player 2 recieves the cards used in the war
                        Console.WriteLine("Player 2 won that war!");
                        player1.removeCard(p1fullcardwar1);player2.removeCard(p2fullcardwar1);
                        player2.addCard(p1fullcard);player2.addCard(p2fullcard);player2.addCard(p1fullcardwar1);player2.addCard(p2fullcardwar1);
                        player1.shuffle();
                        player2.shuffle();
                    }else{
                        //returns cards to players because the war was tied
                        Console.WriteLine("There are no winners in the game of war. (Cards returned)");
                        player1.addCard(p1fullcard);//player1.addCard(p1fullcardwar1);
                        player2.addCard(p2fullcard);//player2.addCard(p2fullcardwar1);
                        player1.shuffle();
                        player2.shuffle();
                    }
                    //continue here
                }else{
                    Console.WriteLine("The Game has ended.");
                    if(player1.getCards().Count() == 52){
                        //player 1 win case
                        Console.WriteLine("Player 1 won.");
                        runtime = 0;
                    }else if(player2.getCards().Count() == 52){
                        //player 2 win case
                        Console.WriteLine("Player 2 won.");
                        runtime = 0;
                    }else{
                        //debug
                        Console.WriteLine("Error late. (this shouldn't happen)");
                        runtime = 0;
                    }
                }
            }
        }
    }
    //deck class
    /*
    ===================================================================================================
    =========                                 DECK CLASS                                ===============
    ===================================================================================================
     */
    class Deck{
        //starting deck with ordered 52 cards
        //values from the front of the strings are used for gameplay purposes
        //STARTINGDECK is constant and should not be modified directly
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
        //the shuffled deck
        private List<string> shuffledDeck = new List<string>{};
        Random value = new Random();
        public void shuffle(){
            //this takes the starting deck and shuffles it, being unique each time
            shuffledDeck = STARTINGDECK.OrderBy(x => Guid.NewGuid()).ToList();
        }
        private int playerNumber;
        //constructor, code could be modified in other locations to accept more players
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
        //for debug/lists the shuffled deck
        public void displayshuffledDeck(){
            foreach(string i in shuffledDeck){
                Console.WriteLine(i);
            }
        }
    }
    //player class
    /*
    =======================================================================================
    ======                           PLAYER CLASS                               ===========
    =======================================================================================
     */
    class Player{
        //this is the player's deck
        private List<string> playerDeck = new List<string>();
        private string playerName;
        //constructor
        public Player(string name){
            playerName = name;
        }
        public void shuffle(){
            //this takes the starting deck and shuffles it, being unique each time
            playerDeck = playerDeck.OrderBy(x => Guid.NewGuid()).ToList();
        }
        //draws a card from the players deck from position 0 of the list
        //position 0 is always a different card because the deck is required to be shuffled every turn
        public string drawCard(){
            return playerDeck.ElementAt(0);
        }
        // sets the players deck to the one dealt to them
        public void setCards(List<string> list){
            playerDeck = list;
        }
        // adds a card to the players deck
        public void addCard(string card){
            playerDeck.Add(card);
        }
        // removes card from players deck
        public void removeCard(string card){
            playerDeck.Remove(card);
        }
        //prints a list of players cards for debug purposes
        public void displayCards(){
            Console.WriteLine("Player's Cards");
            foreach(string i in playerDeck){
                Console.WriteLine(i);
            }
        }
        //gets list of players cards for game
        public List<string> getCards(){
            return playerDeck;
        }
    }
}