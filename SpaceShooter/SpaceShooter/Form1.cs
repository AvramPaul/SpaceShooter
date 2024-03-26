using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace SpaceShooter
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer gameMedia;
        WindowsMediaPlayer shootMedia;
        WindowsMediaPlayer explosion;
        WindowsMediaPlayer PlayerDead;

        PictureBox[] stars;
        int backgroundspeed;

        Random rnd;
        int playerspeed;

        PictureBox[] Munitions;
        int munitionspeed;

        PictureBox[] Enemies;
        int enemiesspeed;

        PictureBox[] EnemiesMunition;
        int enemiesmunitionspeed;

        int score;
        int level;
        int dificulty;
        bool pause;
        bool gameIsOver;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pause = false;
            gameIsOver = false;
            score = 0;
            level = 1;
            dificulty = 9;


            backgroundspeed = 1;
            stars = new PictureBox[10];
            playerspeed = 4;
            rnd = new Random();
            munitionspeed = 20;
            Munitions = new PictureBox[3];
            enemiesspeed = 4;
            EnemiesMunition = new PictureBox[10];
            enemiesmunitionspeed = 4;

            Image munition = Image.FromFile(@"asserts/munition.png");
            gameMedia = new WindowsMediaPlayer();
            shootMedia = new WindowsMediaPlayer();
            explosion = new WindowsMediaPlayer();
            PlayerDead = new WindowsMediaPlayer();

            gameMedia.URL = "songs\\FishSpinning.mp3";
            shootMedia.URL = "songs\\shoot.mp3";
            explosion.URL = "songs\\Death.mp3";
            PlayerDead.URL = "songs\\PlayerDead.mp3";
            gameMedia.settings.setMode("loop", true);
            gameMedia.settings.volume = 5;
            shootMedia.settings.volume = 1;
            explosion.settings.volume = 15;
            PlayerDead.controls.stop();
            Image enemie1 = Image.FromFile("asserts\\E1.png");
            Image enemie2 = Image.FromFile("asserts\\E2.png");
            Image enemie3 = Image.FromFile("asserts\\E3.png");
            Image enemie4 = Image.FromFile("asserts\\E4.png");
            Image enemie5 = Image.FromFile("asserts\\E5.png");
            Image enemie6 = Image.FromFile("asserts\\E6.png");
            Image boss1 = Image.FromFile("asserts\\Boss1.png");
            Image boss2 = Image.FromFile("asserts\\Boss2.png");

            Enemies = new PictureBox[10];

            for(int i=0; i<Enemies.Length; i++)
            {
                Enemies[i] = new PictureBox();
                Enemies[i].Size = new Size(60, 60);
                Enemies[i].SizeMode = PictureBoxSizeMode.Zoom;
                Enemies[i].BorderStyle = BorderStyle.None;
                Enemies[i].Visible = false;
                this.Controls.Add(Enemies[i]);
                Enemies[i].Location = new Point(rnd.Next(10, 400), -50);
            }
            Enemies[0].Image = boss1;
            Enemies[1].Image = enemie2;
            Enemies[2].Image = enemie3;
            Enemies[3].Image = enemie1;
            Enemies[4].Image = boss1;
            Enemies[5].Image = boss2;
            Enemies[6].Image = enemie4;
            Enemies[7].Image = enemie6;
            Enemies[8].Image = enemie5;
            Enemies[9].Image = boss1;

            for (int i=0; i<Munitions.Length; i++)
            {
                Munitions[i] = new PictureBox();
                Munitions[i].Size = new Size(8, 8);
                Munitions[i].Image = munition;
                Munitions[i].SizeMode = PictureBoxSizeMode.StretchImage;
                Munitions[i].BorderStyle = BorderStyle.None;
                this.Controls.Add(Munitions[i]);
            }
            for(int i=0; i<stars.Length; i++)
            {
                stars[i] = new PictureBox();
                stars[i].BorderStyle = BorderStyle.None;
                stars[i].Location = new Point(rnd.Next(20, 580), rnd.Next(10, 400));
                
                if(i % 5 == 0)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Wheat;
                }else if(i%2 == 0)
                {
                    stars[i].Size = new Size(2, 2);
                    stars[i].BackColor = Color.Wheat;
                }else
                {
                    stars[i].Size = new Size(3, 3);
                    stars[i].BackColor = Color.Wheat;
                }

                this.Controls.Add(stars[i]);
            }
            for(int i=0; i<EnemiesMunition.Length; i++)
            {
                EnemiesMunition[i] = new PictureBox();
                EnemiesMunition[i].Size = new Size(20, 20);
                EnemiesMunition[i].Visible = true;
                EnemiesMunition[i].BorderStyle = BorderStyle.None;
                EnemiesMunition[i].Image = Image.FromFile("asserts\\enemie_munition.png");
                EnemiesMunition[i].SizeMode = PictureBoxSizeMode.StretchImage;
                int x = rnd.Next(0, 10);
                EnemiesMunition[i].Location = new Point(Enemies[x].Location.X + 10, Enemies[x].Location.Y + 20);
                this.Controls.Add(EnemiesMunition[i]);
            }
            

            gameMedia.controls.play();
        }

        private void MoveBgTimer_Tick(object sender, EventArgs e)
        {
            for(int i=0; i<stars.Length; i++)
            {
                if(i%5 == 0)
                {
                    stars[i].Top += backgroundspeed;
                    if (stars[i].Top >= this.Height)
                    {
                        stars[i].Top = -stars[i].Height;
                    }
                }else  if(i%2 == 0)
                {
                    stars[i].Top += backgroundspeed + 2;
                    if(stars[i].Top >= this.Height)
                    {
                        stars[i].Top = -stars[i].Height;
                    }
                }else{
                    stars[i].Top += backgroundspeed + 3;
                    if (stars[i].Top >= this.Height)
                    {
                        stars[i].Top = -stars[i].Height;
                    }
                }
            }
        }

        private void LeftMoveTimer_Tick(object sender, EventArgs e)
        {
            if(Player.Left > 10)
            {
                Player.Left -= playerspeed;
            }
        }

        private void RightMoveTimer_Tick(object sender, EventArgs e)
        {
            if(Player.Right < 430)
            {
                Player.Left += playerspeed;
            }
        }

        private void UpMoveTimer_Tick(object sender, EventArgs e)
        {
            if(Player.Top > 10)
            {
                Player.Top -= playerspeed;
            }
        }

        private void DownMoveTimer_Tick(object sender, EventArgs e)
        {
            if(Player.Top < 320)
            {
                Player.Top += playerspeed;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(!pause)
            {
                if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
                {
                    DownMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
                {
                    UpMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
                {
                    LeftMoveTimer.Start();
                }
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
                {
                    RightMoveTimer.Start();
                }
            }
            
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            RightMoveTimer.Stop();
            UpMoveTimer.Stop();
            LeftMoveTimer.Stop();
            DownMoveTimer.Stop();

            if(e.KeyCode == Keys.Space)
            {
                if(!gameIsOver)
                {
                    if(pause)
                    {
                        StartTimers();
                        label1.Visible = false;
                        gameMedia.controls.play();
                        pause = false;
                    }
                    else
                    {
                        label1.Location = new Point(this.Width / 2 - 120, 150);
                        label1.Text = "PAUSED";
                        label1.Visible = true;
                        gameMedia.controls.pause();
                        StopTimers();
                        pause = true;
                    }
                }
            }
        }

        private void MoveMunitionTimer_Tick(object sender, EventArgs e)
        {
            shootMedia.controls.play();
            for(int i=0; i<Munitions.Length; i++)
            {
                if(Munitions[i].Top > 0)
                {
                    Munitions[i].Visible = true;
                    Munitions[i].Top -= munitionspeed;

                    Collision();
                }
                else
                {
                    Munitions[i].Visible = false;
                    Munitions[i].Location = new Point(Player.Location.X + 20, Player.Location.Y - i * 30);
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveEnemie(Enemies, enemiesspeed);
        }

        private void MoveEnemie(PictureBox[] array, int speed)
        {
            for(int i=0; i<array.Length; i++)
            {
                array[i].Visible = true;
                array[i].Top += speed;

                if(array[i].Top > this.Height)
                {
                    array[i].Location = new Point(rnd.Next(10, 400), -50);
                }
            }
        }

        private void Collision()
        {
            for(int i=0; i<Enemies.Length; i++)
            {
                if(  (Munitions[0].Bounds.IntersectsWith(Enemies[i].Bounds)) ||
                        (Munitions[1].Bounds.IntersectsWith(Enemies[i].Bounds)) ||
                        (Munitions[2].Bounds.IntersectsWith(Enemies[i].Bounds)) )
                {
                    score += 1;
                    scorelbl.Text = (score < 10) ? "0" + score.ToString() : score.ToString();

                    if (score % 30 == 0)
                    {
                        level += 1;
                        levellbl.Text = (level < 10) ? "0" + level.ToString() : level.ToString();

                        if (enemiesspeed <= 10 && enemiesmunitionspeed <= 10 && dificulty >= 0)
                        {
                            dificulty--;
                            enemiesspeed++;
                            enemiesmunitionspeed++;
                        }
                        if (level == 10)
                        {
                            GameOver("WELL DONE!");
                        }
                    }
                    explosion.controls.play();
                    Enemies[i].Location = new Point(rnd.Next(10, 400), -100);
                }

                if(Player.Bounds.IntersectsWith(Enemies[i].Bounds))
                {
                    PlayerDead.settings.volume = 20;
                    PlayerDead.controls.play();
                    Player.Visible = false;
                    GameOver("YOURE DEAD");
                }
            }
        }

        private void GameOver(String str)
        {
            label1.Text = str;
            label1.Location = new Point(80, 50);
            label1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            gameIsOver = true;

            gameMedia.controls.stop();
            StopTimers();
        }

        private void StopTimers()
        {
            MoveBgTimer.Stop();
            MoveEnemieTimer.Stop();
            MoveMunitionTimer.Stop();
            EnemiesMunitiontimer.Stop();
        }

        private void StartTimers()
        {
            MoveBgTimer.Start();
            MoveEnemieTimer.Start();
            MoveMunitionTimer.Start();
            EnemiesMunitiontimer.Start();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            for(int i=0; i< (EnemiesMunition.Length - dificulty); i++)
            {
                if (EnemiesMunition[i].Top < this.Height) 
                {
                    EnemiesMunition[i].Visible = true;
                    EnemiesMunition[i].Top += enemiesmunitionspeed;

                    CollisionWithEnemiesMunition();
                }
                else
                {
                    EnemiesMunition[i].Visible = false;
                    int x = rnd.Next(0, 10);
                    EnemiesMunition[i].Location = new Point(Enemies[x].Location.X + 10 , Enemies[x].Location.Y + 20);
                }
            }
        }

        private void CollisionWithEnemiesMunition()
        {
            for(int i=0; i<EnemiesMunition.Length; i++)
            {
                if(EnemiesMunition[i].Bounds.IntersectsWith(Player.Bounds))
                {
                    EnemiesMunition[i].Visible = false;
                    PlayerDead.settings.volume = 30;
                    PlayerDead.controls.play();
                    Player.Visible = false;
                    GameOver("GameOver");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
            Form1_Load(e, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        private void scorelbl_Click(object sender, EventArgs e)
        {

        }
    }
}
