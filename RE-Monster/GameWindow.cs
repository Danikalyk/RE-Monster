using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RE_Monster
{
    public partial class GameWindow : Form
    {
        int knight_1 = 10;
        int knight_2 = 20;
        int knight_3 = 25;

        string enemy;

        int record = 0;

        Random rand = new Random();

        private DateTime tl1;
        private DateTime tl2;

        int player_health = 10;
        //объявление необходимых переменных
        public GameWindow()
        {
            InitializeComponent();

            timer1.Start();
            timer2.Start();

            Knight();

            EnemyDie();

            PlayerDie();

            label4.Text = "Побеждено: " + Convert.ToString(record);

            tl1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            tl2 = tl1.AddMinutes((double)5);
            tl2 = tl2.AddSeconds((double)0);//создание переменных для таймера

            label2.Text = Convert.ToString(player_health);
        }


        public void PlayerDie()
        {
            EndGame eg = new EndGame();
            if (player_health <= 0)
            {
                timer1.Enabled = false;
                timer2.Enabled = false;
                eg.Score = record;
                eg.ShowDialog();
            }
        }//действие при смерти персонажа

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (enemy == "standart")
            {
                knight_1 -= 4;
                label3.Text = Convert.ToString(knight_1);
            }
            if (enemy == "assasin")
            {
                knight_2 -= 4;
                label3.Text = Convert.ToString(knight_2);
            }
            if (enemy == "berserk")
            {
                knight_3 -= 4;
                label3.Text = Convert.ToString(knight_3);
            }

            EnemyDie();
        }//способность #3

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Fight();
        }//игровой процесс

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (enemy == "standart")
            {
                knight_1 -= 2;
                label3.Text = Convert.ToString(knight_1);
            }
            if (enemy == "assasin")
            {
                knight_2 -= 2;
                label3.Text = Convert.ToString(knight_2);
            }
            if (enemy == "berserk")
            {
                knight_3 -= 2;
                label3.Text = Convert.ToString(knight_3);
            }

            EnemyDie();
        }//способность #1

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (enemy == "standart")
            {
                knight_1 -= 3;
                label3.Text = Convert.ToString(knight_1);
            }
            if (enemy == "assasin")
            {
                knight_2 -= 3;
                label3.Text = Convert.ToString(knight_2);
            }
            if (enemy == "berserk")
            {
                knight_3 -= 3;
                label3.Text = Convert.ToString(knight_3);
            }

            EnemyDie();
        }//сопособность #2

        private void GameWindow_Load(object sender, EventArgs e)
        {
            EndGame eg = new EndGame();
            eg.Score = record;
        }

        public void Knight()
        {
            int valian = rand.Next(0, 5);

            if (valian==0||valian==1||valian==2)
            {
                pictureBox3.Image = Properties.Resources._002_1;
                label3.Text = Convert.ToString(knight_1);
                enemy = "standart";
            }
            if(valian==3||valian==4)
            {
                pictureBox3.Image = Properties.Resources._002_2;
                label3.Text = Convert.ToString(knight_2);
                enemy = "assasin";
            }
            if(valian==5)
            {
                pictureBox3.Image = Properties.Resources._002_3;
                label3.Text = Convert.ToString(knight_3);
                enemy = "berserk";
            }
        }//вывод врагов

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        public void Fight()
        {
            if(enemy=="standart")
            {
                knight_1--;
                label3.Text = Convert.ToString(knight_1);
            }
            if(enemy=="assasin")
            {
                knight_2--;
                label3.Text = Convert.ToString(knight_2);
            }
            if(enemy=="berserk")
            {
                knight_3--;
                label3.Text = Convert.ToString(knight_3);
            }

            EnemyDie();
        }//игровая логика

        public void EnemyDie()
        {
            if(knight_1 <= 0||knight_2 <= 0||knight_3 <= 0)
            {
                Knight();
                knight_1 = 10;
                knight_2 = 20;
                knight_3 = 25;

                record++;
                label4.Text = "Побеждено: " + Convert.ToString(record);

                player_health += 10;
                label2.Text = Convert.ToString(player_health);
            }
        }//действие при смерти врага

        private void timer1_Tick(object sender, EventArgs e)
        {
            EndGame eg = new EndGame();

            tl2 = tl2.AddSeconds(-1);
            if (tl2.Minute < 9)
                label1.Text = "0" + tl2.Minute.ToString() + ":";
            else
                label1.Text = tl2.Minute.ToString() + ":";

            if (tl2.Second < 9)
                label1.Text += "0" + tl2.Second.ToString();
            else
                label1.Text += tl2.Second.ToString();

            if (Equals(tl1, tl2))
            {
                timer1.Enabled = false;
                if (MessageBox.Show("Время истекло", "Таймер", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    eg.Score = record;
                    eg.ShowDialog();

                    this.Hide();
                }
            }
        }//игровой таймер

        private void timer2_Tick(object sender, EventArgs e)
        {
            player_health--;
            label2.Text = Convert.ToString(player_health);

            PlayerDie();
        }//атаки врагов

        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            D.f1.Show();

            timer1.Stop();
            timer2.Stop();
        }//действие при закрытии формы

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space)
            {
                Fight();
            }
        }
    }
}
