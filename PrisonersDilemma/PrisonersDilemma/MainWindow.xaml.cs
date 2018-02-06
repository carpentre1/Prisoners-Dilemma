using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


/*Things needed:
 * DONE A reset button to erase the current scores
 * 
 * DONE A quick explanation of the prisoner's dilemma so the player understands the game, and then a way to test their knowledge after they've played the game
 *   The blank space off to the right will be where this is done
 * 
 * Documentation about the design process and testing (included as a word doc in the main folder)
 * Sources for where we got our research from
 * DONE Our names added to the UI 
 * */
namespace PrisonersDilemma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        int run = 0;

        bool playerSubmittedCoin = true;
        bool partnerSubmittedCoin = true;
        int playerCoins = 0;//the total amount of coins the player has accrued
        int partnerCoins = 0;

        int playerGained = 0;//the amount of coins the player gained last time the simulation ran
        int partnerGained = 0;

        string behavior = "random";//the behavior the AI will use for deciding contribution: random, always, never

        string TestChoice = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateInterface()//updates the labels showing how many coins have been earned and total coins
        {
            labelPlayerTotal.Content = playerCoins;
            labelPartnerTotal.Content = partnerCoins;

            if (playerGained < 0)//if you lost coins, don't show a + sign next to the amount
            {
                labelPlayerGained.Content = playerGained;
            }
            else
            {
                labelPlayerGained.Content = "+" + playerGained;
            }
            if (partnerGained < 0)//if they lost coins, don't show a + sign next to the amount
            {
                labelPartnerGained.Content = partnerGained;
            }
            else
            {
                labelPartnerGained.Content = "+" + partnerGained;
            }
        }

        private void RunSimulation()//runs once each time the player clicks either the "contribute" or "don't contribute" button
        {
            DecideContribution();//figure out whether the partner is submitting a coin based on his selected behavior
            if (playerSubmittedCoin && partnerSubmittedCoin)//both contributed
            {
                playerCoins += 2;
                partnerCoins += 2;

                playerGained = 2;
                partnerGained = 2;

                labelPlayerResult.Content = "You got 2 coins because both of you contributed.";
                labelPartnerResult.Content = "Your partner got 2 coins because both of you contributed.";

            }
            else if (!playerSubmittedCoin && !partnerSubmittedCoin)//both didn't contribute
            {
                playerCoins += 0;
                partnerCoins += 0;

                playerGained = 0;
                partnerGained = 0;

                labelPlayerResult.Content = "You got nothing because neither of you contributed!";
                labelPartnerResult.Content = "Your partner got nothing because neither of you contributed!";
            }
            else if (!playerSubmittedCoin && partnerSubmittedCoin)//player didn't contribute
            {
                playerCoins += 3;
                partnerCoins -= 1;

                playerGained = 3;
                partnerGained = -1;

                labelPlayerResult.Content = "You got 3 coins because you didn't contribute but your partner did.";
                labelPartnerResult.Content = "Your partner lost 1 coin because they contributed you didn't.";
            }
            else if (playerSubmittedCoin && !partnerSubmittedCoin)//partner didn't contribute
            {
                playerCoins -= 1;
                partnerCoins += 3;

                playerGained = -1;
                partnerGained = 3;

                labelPlayerResult.Content = "You lost 1 coin because you contributed but your partner didn't.";
                labelPartnerResult.Content = "Your partner got 3 coins because you contributed but your partner didn't.";
            }
            UpdateInterface();
        }

        private void DecideContribution()//uses the selected AI behavior to tell it whether to contribute
        {
            if (behavior == "random")
            {
                Random rnd = new Random();
                partnerSubmittedCoin = Convert.ToBoolean(rnd.Next(0, 2));
            }
            if (behavior == "always")
            {
                partnerSubmittedCoin = true;
            }
            if (behavior == "never")
            {
                partnerSubmittedCoin = false;
            }
        }

        private void buttonContribute_Click(object sender, RoutedEventArgs e)//player clicks the contribute button
        {
            playerSubmittedCoin = true;
            RunSimulation();
        }

        private void buttonDontContribute_Click(object sender, RoutedEventArgs e)//player clicks the "don't contribute" button
        {
            playerSubmittedCoin = false;
            RunSimulation();
        }

        private void radioRandom_Checked(object sender, RoutedEventArgs e)//when one of the radio buttons is selected, change the partner's behavior to match
        {
            behavior = "random";
        }

        private void radioAlways_Checked(object sender, RoutedEventArgs e)
        {
            behavior = "always";
        }

        private void radioNever_Checked(object sender, RoutedEventArgs e)
        {
            behavior = "never";
        }

        private void Button_Click(object sender, RoutedEventArgs e)// reset button clears all coin data
        {
            playerCoins = 0;
            partnerCoins = 0;
            labelPlayerTotal.Content = playerCoins;
            labelPartnerTotal.Content = partnerCoins;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)// I understand Prisoner's Dilemma button brings up dialogue for evaluating
        {
            I_understandButton.Opacity = 0;// hides the I understand button 
            Prisoners_Dilemma_textBox.Text = "You and your partner are captured for a crime. If you know that your partner is going to stay quiet what choice should you make so that is it the best choice for both of you?";
            
            StayQuietButton.Opacity = 100;// shows Stay quiet button
            Betray_Partner_Button.Opacity = 100;// shows Betray PartnerButton

        }

        private void Test()// Runs the test
        {


            if (run == 1)
            {
                switch (TestChoice)
                {
                    case "StayQuiet":
                        {
                            Prisoners_Dilemma_textBox.Text = "That is correct! If neither of you give up the other you each only serve 1 year, so 2 years in total for the group.";
                            
                            break;
                        }

                    case "BetrayPartner":
                        {
                            Prisoners_Dilemma_textBox.Text = "That is incorrect, please try again";
                            run--;
                            break;
                        }



                }// end of switch
            }// end of if
            if (run == 2)
            {
                
                switch (TestChoice)
                {
                    case "StayQuiet":
                        {
                            Prisoners_Dilemma_textBox.Text = "Incorrect, if your partner is going to give you up you will serve 3 years and your partner will serve 0. Please try again.";
                            run--;
                            break;
                        }

                    case "BetrayPartner":
                        {
                            Prisoners_Dilemma_textBox.Text = "Correct, if your partner is going to give you up then the best choice for you is to also give him up so that you only serve 2 years rather than 3.";

                            break;
                        }
                }
            
           


           

                



            }// end of if


            if (run == 3)
            {
                

                switch (TestChoice)
                {

                    case "StayQuiet":
                        {
                            Prisoners_Dilemma_textBox.Text = "Incorrect, if your partner isn't going to give you up you will serve 1 year. Please try again.";
                            run--;
                            break;
                        }

                    case "BetrayPartner":
                        {
                            Prisoners_Dilemma_textBox.Text = "Correct, if your partner isn't going to give you up the best choice for you is to also give him up so that you serve 0 years rather than 1.";

                            break;
                        }


                }// end of switch
            }


            //Prisoners_Dilemma_textBox.Text = "Congratulations you understand the prisoner's dilemma";
        }// end of Test

            private void StayQuietButton_Click(object sender, RoutedEventArgs e)
            {
            run++;
                TestChoice = "StayQuiet";
                Test();
            ContinueButton.Opacity = 100;
        }

            private void Button_Click_2(object sender, RoutedEventArgs e)
            {
            run++;
            TestChoice = "BetrayPartner";
                Test();
            ContinueButton.Opacity = 100;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            if (run == 1)
            {
                ContinueButton.Opacity = 0;
                Prisoners_Dilemma_textBox.Text = "If you know that your partner is going to give you up, what choice should you make so that is it the best choice for you?";
            }

            if (run == 2)
            {
                ContinueButton.Opacity = 0;
                Prisoners_Dilemma_textBox.Text = "If you know that your partner isn't going to give you up, what choice should you make so that is it the best choice for you?";
            }
        }
    }
    }

