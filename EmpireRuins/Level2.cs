using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Framework.Core;
using Framework.Enums;

namespace EmpireRuins
{
    public partial class Level2 : Form
    {
        Game g;
        Random random = new Random();
        PictureBox shootingEnemy;
        bool enemyAlive = true;
        bool shootingEnemyAlive = true;

        public Level2()
        {
            InitializeComponent();
        }

        private void Level2_Load(object sender, EventArgs e)
        {
            Point boundary = new Point(this.Width, this.Height);

            g = new Game();

            //Event Handlers 
            g.onGameObjectAdded += new EventHandler(this.onGameObjectsAdded_Game);

            g.onPlayerFires += new EventHandler(onPlayerFires_Game);
            g.onPlayerFiresAdded += new EventHandler(onPlayerFiresAdded_Game);
            g.onEnemyFiresAdded += new EventHandler(onEnemyFiresAdded_Game);

            g.onProgressBarAdded += new EventHandler(onProgressBarAdded_Game);

            g.onPlayerHealthDec += new EventHandler(decPlayerHealth);
            g.onPlayerTouchingEnergizer += new EventHandler(onPlayerTouchingEnegizer);
            g.onPlayerTouchingWall += new EventHandler(onPlayerTouchingWall);

            g.onPlayerFireTouchingEnemy += new EventHandler(onPlayerFireTouchingEnemy);
            g.onPlayerFireTouchingEnemy2 += new EventHandler(onPlayerFireTouchingEnemy2);
            g.onPlayerFireTouchingShootingEnemy += new EventHandler(onPlayerFireTouchingShootingEnemy);

            g.onEnemyFireTouchingPlayer += new EventHandler(onEnemyFireTouchingPlayer);

            //adding design time added objects
            foreach (Control pb in this.Controls)
            {
                if (pb is PictureBox && (string)pb.Tag == "wall")
                {
                    g.addGameObject((PictureBox)pb, ObjectTypes.wall, new Static());
                }
                if (pb is PictureBox && (string)pb.Tag == "energizer")
                {
                    g.addGameObject((PictureBox)pb, ObjectTypes.energizer, new Static());
                }
            }

            //adding GameObjects
            g.addGameObject(EmpireRuins.Properties.Resources.char2, 230, 150, false, ObjectTypes.enemy, new Vertical(10, boundary, "Down"));
            g.addGameObject(EmpireRuins.Properties.Resources.char1, 350, 850, true, ObjectTypes.player, new Keyboard(15, boundary));
            g.addGameObject(EmpireRuins.Properties.Resources.char8, 350, 1150, true, ObjectTypes.shootingEnemy, new Vertical(10, boundary, "Up"));

            //adding Collisions 
            CollisionClass col1 = new CollisionClass(ObjectTypes.player, ObjectTypes.enemy, new PlayerCollision());
            CollisionClass col2 = new CollisionClass(ObjectTypes.player, ObjectTypes.shootingEnemy, new PlayerCollision());
            CollisionClass col3 = new CollisionClass(ObjectTypes.player, ObjectTypes.energizer, new PlayerCollision());
            CollisionClass col4 = new CollisionClass(ObjectTypes.player, ObjectTypes.wall, new PlayerCollision());
            g.addCollision(col1);
            g.addCollision(col2);
            g.addCollision(col3);
            g.addCollision(col4);

            CollisionClass col5 = new CollisionClass(ObjectTypes.playerFire, ObjectTypes.enemy, new PlayerFireCollisions());
            CollisionClass col6 = new CollisionClass(ObjectTypes.playerFire, ObjectTypes.shootingEnemy, new PlayerFireCollisions());
            g.addCollision(col5);
            g.addCollision(col6);

            CollisionClass col7 = new CollisionClass(ObjectTypes.player, ObjectTypes.enemyFire, new EnemyFireCollisions());
            g.addCollision(col7);

        }

        private void onGameObjectsAdded_Game(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            this.Controls.Add(pb);
        }

        private void onProgressBarAdded_Game(object sender, EventArgs e)
        {
            ProgressBar h = (ProgressBar)sender;
            this.Controls.Add(h);
        }

        private void onPlayerFiresAdded_Game(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            this.Controls.Add(pb);
        }

        private void onPlayerFires_Game(object sender, EventArgs e)
        {
            Point boundary = new Point(this.Width, this.Height);
            PictureBox pb = (PictureBox)sender;
            g.addGameObject(EmpireRuins.Properties.Resources.Icon18, (pb.Location.Y + (pb.Height / 2)), (pb.Location.X + (pb.Width / 2)), false, ObjectTypes.playerFire, new Shooting(10, boundary, "Right"));
        }

        private void onEnemyFiresAdded_Game(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            this.Controls.Add(pb);
        }

        private void decPlayerHealth(object sender, EventArgs e)
        {
            PictureBox p = g.getPlayer();
            ProgressBar h = (ProgressBar)sender;
            if (lblHealthNo.Text == "1")
            {
                if (h.Value > 0)
                {
                    h.Value = h.Value - 5;
                }
                if (h.Value == 0)
                {
                    lblHealthNo.Text = "0";
                    this.Controls.Remove(p);
                    p = null;
                    this.Controls.Remove(h);
                    h = null;
                    //gameover form
                }
            }
            else if (lblHealthNo.Text == "2")
            {
                if (h.Value > 0)
                {
                    h.Value = h.Value - 5;
                }
                if (h.Value == 0)
                {
                    h.Value = 100;
                    lblHealthNo.Text = "1";
                }
            }
            this.Controls.Add(h);
        }

        private void onPlayerTouchingEnegizer(object sender, EventArgs e)
        {
            PictureBox energizer = (PictureBox)sender;
            ProgressBar h = g.getPlayerHealthBar();
            this.Controls.Remove(energizer);
            if (h.Value < 100)
            {
                h.Value = 100;
            }
            this.Controls.Add(h);
        }

        private void onPlayerTouchingWall(object sender, EventArgs e)
        {
            PictureBox wall = (PictureBox)sender;
            this.Controls.Remove(wall);
            wall = null;
            calculateScore();
        }

        private void calculateScore()
        {
            int score = int.Parse(lblScoreNo.Text);
            score = score + 20;
            lblScoreNo.Text = score.ToString();
        }

        private void onPlayerFireTouchingEnemy(object sender, EventArgs e)
        {
            PictureBox playerFire = (PictureBox)sender;
            this.Controls.Remove(playerFire);
            playerFire = null;
            enemyAlive = false;
        }

        private void onPlayerFireTouchingEnemy2(object sender, EventArgs e)
        {
            PictureBox enemy = (PictureBox)sender;
            this.Controls.Remove(enemy);
            enemy = null;
        }

        private void onPlayerFireTouchingShootingEnemy(object sender, EventArgs e)
        {
            PictureBox playerFire = (PictureBox)sender;
            this.Controls.Remove(playerFire);
            playerFire = null;

            ProgressBar shootingEnemyHealth = g.getShootingEnemyHealth();
            PictureBox shootingEnemy = g.getShootingEnemy();
            if (shootingEnemyHealth.Value > 0)
            {
                shootingEnemyHealth.Value = shootingEnemyHealth.Value - 10;
            }
            if (shootingEnemyHealth.Value == 0)
            {
                this.Controls.Remove(shootingEnemy);
                this.Controls.Remove(shootingEnemyHealth);
                shootingEnemy = null;
                shootingEnemyHealth = null;
                shootingEnemyAlive = false;
            }
        }

        private void onEnemyFireTouchingPlayer(object sender, EventArgs e)
        {
            PictureBox enemyFire = (PictureBox)sender;
            this.Controls.Remove(enemyFire);

            PictureBox p = g.getPlayer();
            ProgressBar h = g.getPlayerHealthBar();
            if (lblHealthNo.Text == "1")
            {
                if (h.Value > 0)
                {
                    h.Value = h.Value - 5;
                }
                if (h.Value == 0)
                {
                    lblHealthNo.Text = "0";
                    this.Controls.Remove(p);
                    p = null;
                    this.Controls.Remove(h);
                    h = null;
                }
            }
            else if (lblHealthNo.Text == "2")
            {
                if (h.Value > 0)
                {
                    h.Value = h.Value - 5;
                }
                if (h.Value == 0)
                {
                    h.Value = 100;
                    lblHealthNo.Text = "1";
                }
            }
            this.Controls.Add(h);
        }

        private void GameTimer2_Tick(object sender, EventArgs e)
        {
            Point boundary = new Point(this.Width, this.Height);
            g.update();

            //generating random number for enemy fires
            int num = random.Next(1, 100);
            if (num % 15 == 0)
            {
                shootingEnemy = g.getShootingEnemy();
                g.addGameObject(EmpireRuins.Properties.Resources.Icon2, (shootingEnemy.Location.Y + (shootingEnemy.Height / 2)), (shootingEnemy.Location.X + (shootingEnemy.Width / 2)), false, ObjectTypes.enemyFire, new Shooting(10, boundary, "Left"));
            }

            //removing fires 
            g.removePlayerFiresFromList(this.Width);
            g.removeEnemyFiresFromList(this.Height);

            // game over condition
            if (lblHealthNo.Text == "0")
            {
                this.Close();
                LostForm form = new LostForm();
                form.Show();
            }

            //game won condition
            if (enemyAlive == false && shootingEnemyAlive == false)
            {
                this.Close();
                LastForm form = new LastForm();
                form.Show();
            }
        }

        private void Level2_KeyDown(object sender, KeyEventArgs e)
        {
            g.keyPressed(e.KeyCode);
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }
    }
}
