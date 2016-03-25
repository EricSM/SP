﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoggleClient
{
    public partial class View : Form
    {
        public Model model;

        public View()
        {
            InitializeComponent();
            model = new Model();

            //random info
            textBoxPlayerName.Text = GetRandomString();
            Random r = new Random();
            int rInt = r.Next(10, 100); //for ints
            textBoxTime.Text = "15";//rInt.ToString();
        }

        private void buttonJoinGame_Click(object sender, EventArgs e)
        {            
            Task task = new Task(() => 
            {
                model.CreateUser(textBoxPlayerName.Text, textBoxServer.Text);
                model.JoinGame(int.Parse(textBoxTime.Text), textBoxServer.Text);
           
            });

            task.Start();
            timer1.Enabled = true;

            buttonJoinGame.Enabled = false;
            buttonCancel.Enabled = true;
            textBoxServer.ReadOnly = textBoxPlayerName.ReadOnly = textBoxTime.ReadOnly = true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //check if the game is not pending you cant cancel
            buttonCancel.Enabled = false;

            model.CancelJoinRequest(textBoxServer.Text);

           // timer1.Enabled = false;
            if (model.GameState == "")
            {
                this.Close();
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            model.PlayWordRequest(textBoxWord.Text,  textBoxServer.Text);

            timer1.Enabled = true;
        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path;
        }
        private void endgame()
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            model.GameStatus(false, textBoxServer.Text);

            labelStatus.Text = model.GameState;
            labelTime.Text = model.TimeLeft.ToString();
            labelPlayer1.Text = model.Player1;
            labelPlayer2.Text = model.Player2;

            if (model.TimeLeft == 0)
            {
                model.GameStatus(false, textBoxServer.Text);
                Console.WriteLine("Times up");
                //MessageBox.Show(model.Player1WordsPlayed.ToString());
                timer1.Enabled = false;
            }
            try
            {
                textBoxPlayer1Score.Text = model.Player1Score.ToString();
                textBoxPlayer2Score.Text = model.Player2Score.ToString();

                if (model.Board.Length > 0)
                {
                    Dice1.Text = model.Board[0].ToString();
                    Dice2.Text = model.Board[1].ToString();
                    Dice3.Text = model.Board[2].ToString();
                    Dice4.Text = model.Board[3].ToString();
                    Dice5.Text = model.Board[4].ToString();
                    Dice6.Text = model.Board[5].ToString();
                    Dice7.Text = model.Board[6].ToString();
                    Dice8.Text = model.Board[7].ToString();
                    Dice9.Text = model.Board[8].ToString();
                    Dice10.Text = model.Board[9].ToString();
                    Dice11.Text = model.Board[10].ToString();
                    Dice12.Text = model.Board[11].ToString();
                    Dice13.Text = model.Board[12].ToString();
                    Dice14.Text = model.Board[13].ToString();
                    Dice15.Text = model.Board[14].ToString();
                    Dice16.Text = model.Board[15].ToString();

                }
            }
            catch
            {

            }
        }
    }
}
