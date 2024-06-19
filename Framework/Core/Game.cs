using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Framework.Enums;

namespace Framework.Core
{
    public class Game : IGame
    {
        private List<GameObject> gameObjectsList;
        public event EventHandler onGameObjectAdded;

        private List<GameObject> playerFiresList;
        public event EventHandler onPlayerFires;
        public event EventHandler onPlayerFiresAdded;

        private List<GameObject> enemyFiresList;
        public event EventHandler onEnemyFiresAdded;

        public event EventHandler onProgressBarAdded;

        private List<CollisionClass> collisionsList;
        public event EventHandler onPlayerHealthDec;
        public event EventHandler onPlayerTouchingWall;
        public event EventHandler onPlayerTouchingEnergizer;

        public event EventHandler onPlayerFireTouchingEnemy;
        public event EventHandler onPlayerFireTouchingEnemy2;
        public event EventHandler onPlayerFireTouchingShootingEnemy;

        public event EventHandler onEnemyFireTouchingPlayer;

        public Game()
        {
            gameObjectsList = new List<GameObject>();
            playerFiresList = new List<GameObject>();
            enemyFiresList = new List<GameObject>();
            collisionsList = new List<CollisionClass>();
        }

        public void addGameObject(Image img, int top, int left, bool health, ObjectTypes type, IMovement movement)
        {
            GameObject ob = new GameObject(img, top, left, health, type, movement);
            
            if (ob.Type == ObjectTypes.playerFire)
            {
                playerFiresList.Add(ob);
                onPlayerFiresAdded?.Invoke(ob.Pb, EventArgs.Empty);
            }
            else if (ob.Type == ObjectTypes.enemyFire)
            {
                enemyFiresList.Add(ob);
                onEnemyFiresAdded?.Invoke(ob.Pb, EventArgs.Empty);
            }
            else if (ob.Type == ObjectTypes.player || ob.Type == ObjectTypes.shootingEnemy || ob.Type == ObjectTypes.enemy )
            {
                gameObjectsList.Add(ob);
                onGameObjectAdded?.Invoke(ob.Pb, EventArgs.Empty);
                if (health != false)
                {
                    onProgressBarAdded?.Invoke(ob.H, EventArgs.Empty);
                }
            }
        }

        public void addGameObject(PictureBox pb, ObjectTypes type, IMovement movement)
        {
            GameObject ob = new GameObject(pb, type, movement);
            if (ob.Type == ObjectTypes.wall)
            {
                gameObjectsList.Add(ob);
                onGameObjectAdded?.Invoke(ob.Pb, EventArgs.Empty);
            }
            if (ob.Type == ObjectTypes.energizer)
            {
                gameObjectsList.Add(ob);
                onGameObjectAdded?.Invoke(ob.Pb, EventArgs.Empty);
            }
        }

        public void update()
        {
            detectCollision();
            foreach (GameObject go in gameObjectsList)
            {
                go.move();
            }
            foreach (GameObject go in playerFiresList)
            {
                go.move();
            }
            foreach (GameObject go in enemyFiresList)
            {
                go.move();
            }
            
        }

        public void keyPressed(Keys keyCode)
        {
            foreach(GameObject go in gameObjectsList)
            {
                if (go.Movement.GetType() == typeof(Keyboard))
                {
                    Keyboard keyboardHandle = (Keyboard)go.Movement;
                    keyboardHandle.keyPressedByUser(keyCode);
                }
            }
            if (keyCode == Keys.Space)
            {
                foreach(GameObject go in gameObjectsList)
                {
                    if (go.Type == ObjectTypes.player)
                    {
                        onPlayerFires?.Invoke(go.Pb, EventArgs.Empty);
                    }
                }
            }
        }

        public void removePlayerFiresFromList(int width)
        {
            for(int x = 0; x < playerFiresList.Count; x++ )
            {
                if (playerFiresList[x].Pb.Left > width)
                {
                    playerFiresList.Remove(playerFiresList[x]);
                }
            }
        }

        public void removeEnemyFiresFromList(int height)
        {
            for (int x = 0; x < enemyFiresList.Count; x++)
            {
                if (enemyFiresList[x].Pb.Top > height)
                {
                    enemyFiresList.Remove(enemyFiresList[x]);
                }
            }
        }

        public PictureBox getShootingEnemy()
        {
            int index = 0;
            for(int x = 0; x < gameObjectsList.Count; x++)
            {
                if (gameObjectsList[x].Type == ObjectTypes.shootingEnemy)
                {
                    index = x;
                }
            }
            return gameObjectsList[index].Pb;
        }

        public ProgressBar getShootingEnemyHealth()
        {
            int index = 0;
            for (int x = 0; x < gameObjectsList.Count; x++)
            {
                if (gameObjectsList[x].Type == ObjectTypes.shootingEnemy)
                {
                    index = x;
                }
            }
            return gameObjectsList[index].H;
        }

        public PictureBox getPlayer()
        {
            int index = 0;
            for (int x = 0; x < gameObjectsList.Count; x++)
            {
                if (gameObjectsList[x].Type == ObjectTypes.player)
                {
                    index = x;
                }
            }
            return gameObjectsList[index].Pb;
        }

        public ProgressBar getPlayerHealthBar()
        {
            int index = 0;
            for (int x = 0; x < gameObjectsList.Count; x++)
            {
                if (gameObjectsList[x].Type == ObjectTypes.player)
                {
                    index = x;
                }
            }
            return gameObjectsList[index].H;
        }


        public void raisePlayerHealthDecAction(ProgressBar playerHealth)
        {
            onPlayerHealthDec?.Invoke(playerHealth, EventArgs.Empty);
        }

        public void raisePlayerTouchesWall(PictureBox wall)
        {
            onPlayerTouchingWall?.Invoke(wall, EventArgs.Empty);
        }

        public void raisePlayerTouchesEnergizer(PictureBox energizer)
        {
            onPlayerTouchingEnergizer?.Invoke(energizer, EventArgs.Empty);
        }

        public void raisePlayerFireTouchesShootingEnemy(PictureBox playerFire)
        {
            onPlayerFireTouchingShootingEnemy?.Invoke(playerFire, EventArgs.Empty);
            
        }

        public void raisePlayerTouchesEnemy(PictureBox playerFire, ProgressBar enemy)
        {
            onPlayerFireTouchingEnemy?.Invoke(playerFire, EventArgs.Empty);
            onPlayerFireTouchingEnemy2?.Invoke(enemy, EventArgs.Empty);
        }

        public void raiseEnemyFireTouchesPlayer(PictureBox enemyFire)
        {
            onEnemyFireTouchingPlayer?.Invoke(enemyFire, EventArgs.Empty);
        }

        public void detectCollision()
        {
            for (int x = 0; x < gameObjectsList.Count; x++)
            {
                for (int y = 0; y < gameObjectsList.Count; y++)
                {
                    if (gameObjectsList[x].Pb.Bounds.IntersectsWith(gameObjectsList[y].Pb.Bounds))
                    {
                        foreach(CollisionClass c in collisionsList)
                        {
                            if (gameObjectsList[x].Type == c.G1 && gameObjectsList[y].Type == c.G2)
                            {
                                c.Behaviour.performAction(this, c.G2, gameObjectsList[x], gameObjectsList[y]);
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < playerFiresList.Count; x++)
            {
                for (int y = 0; y < gameObjectsList.Count; y++)
                {
                    if (playerFiresList[x].Pb.Bounds.IntersectsWith(gameObjectsList[y].Pb.Bounds))
                    {
                        foreach (CollisionClass c in collisionsList)
                        {
                            if (playerFiresList[x].Type == c.G1 && gameObjectsList[y].Type == c.G2)
                            {
                                c.Behaviour.performAction(this, c.G2, playerFiresList[x], gameObjectsList[y]);
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < gameObjectsList.Count; x++)
            {
                for (int y = 0; y < enemyFiresList.Count; y++)
                {
                    if (gameObjectsList[x].Pb.Bounds.IntersectsWith(enemyFiresList[y].Pb.Bounds))
                    {
                        foreach (CollisionClass c in collisionsList)
                        {
                            if (gameObjectsList[x].Type == c.G1 && enemyFiresList[y].Type == c.G2)
                            {
                                c.Behaviour.performAction(this, c.G2, gameObjectsList[x], enemyFiresList[y]);
                            }
                        }
                    }
                }
            }
        }

        public void addCollision(CollisionClass c)
        {
            collisionsList.Add(c);
        }
    }
}
