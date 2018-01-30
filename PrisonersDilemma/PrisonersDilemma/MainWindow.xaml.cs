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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunSimulation()
        {
            Random rnd = new Random();
            partnerSubmittedCoin = Convert.ToBoolean(rnd.Next(0, 2));
            if(playerSubmittedCoin && partnerSubmittedCoin)//both contributed
            {
                playerCoins += 2;
                partnerCoins += 2;
                labelPlayerResult.Content = "You got 2 coins because both of you contributed.";
                labelPartnerResult.Content = "Your partner got 2 coins because both of you contributed.";

            }
            else if(!playerSubmittedCoin && !partnerSubmittedCoin)//both didn't contribute
            {
                playerCoins += 0;
                partnerCoins += 0;
                labelPlayerResult.Content = "You got nothing because neither of you contributed!";
                labelPartnerResult.Content = "Your partner got nothing because neither of you contributed!";
            }
            else if (!playerSubmittedCoin && partnerSubmittedCoin)//player didn't contribute
            {
                playerCoins += 3;
                partnerCoins -= 1;
                labelPlayerResult.Content = "You got 3 coins because you didn't contribute but your partner did.";
                labelPartnerResult.Content = "Your partner lost 1 coin because they contributed you didn't.";
            }
            else if (playerSubmittedCoin && !partnerSubmittedCoin)//partner didn't contribute
            {
                playerCoins -= 1;
                partnerCoins += 3;
                labelPlayerResult.Content = "You lost 1 coin because you contributed but your partner didn't.";
                labelPartnerResult.Content = "Your partner got 3 coins because you contributed but your partner didn't.";
            }
        }

        private void buttonContribute_Click(object sender, RoutedEventArgs e)
        {
            playerSubmittedCoin = true;
            RunSimulation();
        }

        private void buttonDontContribute_Click(object sender, RoutedEventArgs e)
        {
            playerSubmittedCoin = false;
            RunSimulation();
        }
    }
}
