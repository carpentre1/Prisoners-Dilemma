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
 * A reset button to erase the current scores
 * A couple different AI behaviors the player can choose from such as "always contributes", "never contributes", "contributes until you don't". These will allow the player to see the
 *   difference in results. It would probably be best to make each behavior unnamed so the player has to figure out what they do by experimenting. Right now the AI chooses randomly.
 * 
 * Documentation about the design process and testing (included as a word doc in the main folder)
 * Sources for where we got our research from
 * Our names added to the UI
 * */
namespace PrisonersDilemma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool playerSubmittedCoin = true;
        bool partnerSubmittedCoin = true;
        int playerCoins = 0;//the total amount of coins the player has accrued
        int partnerCoins = 0;

        int playerGained = 0;//the amount of coins the player gained last time the simulation ran
        int partnerGained = 0;

        string behavior = "random";//the behavior the AI will use for deciding contribution: random, always, never

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
    }
}
