using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace breakingout_game_csh
{
    public partial class Form1 : Form
    {
        bool goLeft;
        bool goRight;
        bool isGameOver;

        int score;
        int ballx;
        int bally;
        int playerSpeed;
        Random rnd = new Random();
        PictureBox[] blockArray;

        public Form1()
        {
            InitializeComponent();
            PlaceBlocks();
           // setupGame();
        }
        private void setupGame()
        {
            isGameOver = false;
            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            txtScore.Text = "Score: " + score;
            ball.Left = 376;
            ball.Top = 328;
            player.Left = 347;
            gameTimer.Start();
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void PlaceBlocks()

        {
            blockArray = new PictureBox[20];
            int a = 0;
            int top = 50;
            int left = 60;
            for (int i = 0; i < blockArray.Length; i++)
            {
                blockArray[i] = new PictureBox();
                blockArray[i].Height = 23;
                blockArray[i].Width = 100;
                blockArray[i].Tag = "blocks";
                blockArray[i].BackColor = Color.White;
                if (a == 5)
                {
                    top = top + 50;
                    left = 60;
                    a = 0;
                }
                if (a < 5)
                {
                    a++;
                    blockArray[i].Left = left;
                    blockArray[i].Top = top;
                    this.Controls.Add(blockArray[i]);
                    left = left + 130;
                }
            }
            setupGame();
        }
        private void removeBlocks()
        {
            foreach (PictureBox x in blockArray)
            {
                this.Controls.Remove(x);
            }
        }
        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            if (goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left < 588)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;
            if (ball.Left < 0 || ball.Left > 685)
            {
                ballx = -ballx;
            }
            if (ball.Top < 0)
            {
                bally = -bally;
            }
            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                 ball.Top = 362;
                bally = rnd.Next(5, 12) * -1;
                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(5, 12);
                }
            }
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox && (string)x.Tag == "blocks")
                    {
                        if (ball.Bounds.IntersectsWith(x.Bounds))
                        {
                            score += 1;
                            bally = -bally;
                            this.Controls.Remove(x);
                        }
                    }
                }
                if (score == 20)
                {
                    gameOver("\n***You Win*** Press Enter to Play Again");
                }
                if (ball.Top > 580)
                {
                    gameOver("\nYou Lose!! Press Enter to try again");
                }
            
        }
        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + " " + message;
        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
           if (e.KeyCode == Keys.Enter && isGameOver == true)
            {
                removeBlocks();
                PlaceBlocks();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ball_Click(object sender, EventArgs e)
        {

        }
    }
    
}
