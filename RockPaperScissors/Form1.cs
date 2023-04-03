using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using System.Drawing.Imaging;

/// <summary>
/// A rock, paper, scissors game that utilizes basic methods
/// for repetitive tasks.
/// </summary>

namespace RockPaperScissors
{
    public partial class Form1 : Form
    {
        string playerChoice, cpuChoice;

        int choicePause = 1000;
        int outcomePause = 3000;

        Random randGen = new Random();

        SoundPlayer jabPlayer = new SoundPlayer(Properties.Resources.jabSound);
        SoundPlayer gongPlayer = new SoundPlayer(Properties.Resources.gong);


        int[] winLossTie = new int[] { 0, 0, 0 };
        Image[] winLossTieImages = new Image[] { Properties.Resources.winTrans, Properties.Resources.loseTrans, Properties.Resources.tieTrans };

        string[] moveList = new string[] { "Rock", "Paper", "Scissors" };
        Image[] imageList = new Image[] { Properties.Resources.rock168x280, Properties.Resources.paper168x280, Properties.Resources.scissors168x280 };
        public Form1()
        {
            InitializeComponent();
        }

        private void rockButton_Click(object sender, EventArgs e)
        {
            playerMakesChoice(0);
        }

        private void paperButton_Click(object sender, EventArgs e)
        {
            playerMakesChoice(1);
        }

        private void scissorsButton_Click(object sender, EventArgs e)
        {
            playerMakesChoice(2);
        }

        void playerMakesChoice(int choiceNum)
        {
            /// TODO Set the playerchoice value, show the appropriate image,
            /// play a sound, wait for a second; repeat for the computer turn 
            //PLAYER TURN
            playerChoice = moveList[choiceNum];
            playerImage.BackgroundImage = imageList[choiceNum];

            jabPlayer.Play();

            Wait(choicePause);

            //CPU TURN
            int cpuChoiceNum = randGen.Next(0, 2);

            cpuChoice = moveList[cpuChoiceNum];
            cpuImage.BackgroundImage = imageList[cpuChoiceNum];

            jabPlayer.Play();

            Wait(choicePause);

            //EVALUATION
            if (playerChoice == cpuChoice) { OutcomeIs(2); }
            else if ((playerChoice == "Rock" && cpuChoice == "Paper")|| (playerChoice == "Paper" && cpuChoice == "Scissors")||(playerChoice == "Scissors" && cpuChoice == "Rock"))
            { OutcomeIs(1); }
            else { OutcomeIs(0); }
        
             Wait(outcomePause);
            cpuImage.BackgroundImage = null;
            playerImage.BackgroundImage = null;
            resultImage.BackgroundImage = null;
        }

        void Wait(int time) 
        {
            Refresh();
            Thread.Sleep(time);
        }

        void OutcomeIs(int outcome) 
        {
            gongPlayer.Play();
            winLossTie[outcome]++;
            resultImage.BackgroundImage = winLossTieImages[outcome];
        }
    }
}