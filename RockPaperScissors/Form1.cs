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

        //Holds information about the Win, Loss, Tie states:
        int[] winLossTieScoreCounter = new int[] { 0, 0, 0 };
        string[] winLossTieWords = new string[] { "Wins:", "Losses:", "Ties:" };
        Image[] winLossTieImages = new Image[] { Properties.Resources.winTrans, Properties.Resources.loseTrans, Properties.Resources.tieTrans };

        //Keeps track of labels 
        List<Label> labels = new List<Label>();

        //Holds information about the Rock, Paper, Scissors moves:
        string[] moveList = new string[] { "Rock", "Paper", "Scissors" };
        Image[] imageList = new Image[] { Properties.Resources.rock168x280, Properties.Resources.paper168x280, Properties.Resources.scissors168x280 };
        public Form1()
        {
            InitializeComponent();
            //Add labels to keep track of in order WIN, LOSS, TIE
            labels.Add(winsLabel);
            labels.Add(lossesLabel);
            labels.Add(tiesLabel);
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
            //PLAYER TURN
            //set the playerChoice value, show the appropriate image
            playerChoice = moveList[choiceNum];
            playerImage.BackgroundImage = imageList[choiceNum];

            //play a sound, wait for choicePause
            jabPlayer.Play();
            Wait(choicePause);

            //CPU TURN
            //generate cpuChoiceNumber
            int cpuChoiceNum = randGen.Next(0, 2);

            //set the cpuChoice value, show the appropriate image
            cpuChoice = moveList[cpuChoiceNum];
            cpuImage.BackgroundImage = imageList[cpuChoiceNum];

            //play a sound, wait for choicePause
            jabPlayer.Play();
            Wait(choicePause);

            //EVALUATION
            //if both signs match the outcome is a tie, trigger a tie:
            if (playerChoice == cpuChoice) { OutcomeIs(2); }

            //all possibilities of the the outcome being a loss trigger a loss:
            else if ((playerChoice == "Rock" && cpuChoice == "Paper") || (playerChoice == "Paper" && cpuChoice == "Scissors") || (playerChoice == "Scissors" && cpuChoice == "Rock"))
            { OutcomeIs(1); }

            //if its not the last two, it must be a win, trigger a win:
            else { OutcomeIs(0); }

            //Wait the outcomePause length, and reset for the next match.
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
            //Play sound
            gongPlayer.Play();

            //Update the appropriate score (add one)
            winLossTieScoreCounter[outcome]++;

            //Background image shows appropriate condition, appropriate label updates its score.
            resultImage.BackgroundImage = winLossTieImages[outcome];
            labels[outcome].Text = $"{winLossTieWords[outcome]} {winLossTieScoreCounter[outcome]}";
        }
    }
}