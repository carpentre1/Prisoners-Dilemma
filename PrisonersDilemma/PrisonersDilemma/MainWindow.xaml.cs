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
        bool playerStayedSilent = true;
        bool partnerStayedSilent = true;
        int playerYears = 0;//years in prison
        int partnerYears = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonSilent_Click(object sender, RoutedEventArgs e)
        {
            playerStayedSilent = true;
            RunSimulation();
        }

        private void buttonRat_Click(object sender, RoutedEventArgs e)
        {
            playerStayedSilent = false;
            RunSimulation();
        }
        private void RunSimulation()
        {
            Random rnd = new Random();
            partnerStayedSilent = Convert.ToBoolean(rnd.Next(0, 2));
            if(playerStayedSilent && partnerStayedSilent)//both silent
            {
                playerYears += 1;
                partnerYears += 1;
                labelPlayerResult.Content = "You got 1 year in prison because both of you stayed silent.";
                labelPartnerResult.Content = "Your partner got 1 year in prison because both of you stayed silent.";

            }
            else if(!playerStayedSilent && !partnerStayedSilent)//both ratted
            {
                playerYears += 2;
                partnerYears += 2;
                labelPlayerResult.Content = "You got 2 years in prison because you both ratted each other out!";
                labelPartnerResult.Content = "Your partner got  2 years in prison because you both ratted each other out!";
            }
            else if (!playerStayedSilent && partnerStayedSilent)//player ratted
            {
                playerYears += 0;
                partnerYears += 3;
                labelPlayerResult.Content = "You got 0 years in prison because you ratted out your partner and he stayed silent.";
                labelPartnerResult.Content = "Your partner got 3 years in prison because you ratted him out but he stayed silent.";
            }
            else if (playerStayedSilent && !partnerStayedSilent)//partner ratted
            {
                playerYears += 3;
                partnerYears += 0;
                labelPlayerResult.Content = "You got 3 years in prison because your partner ratted you out but you stayed silent.";
                labelPartnerResult.Content = "Your partner got 0 years in prison because he ratted you out but you stayed silent.";
            }
        }
    }
}
