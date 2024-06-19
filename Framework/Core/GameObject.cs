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
    public class GameObject
    {
        private PictureBox pb;
        private ObjectTypes type;
        private ProgressBar h;
        private IMovement movement;

        public GameObject(Image img, int top , int left, bool health, ObjectTypes type, IMovement movement)
        {
            Pb = new PictureBox();
            Pb.Image = img;
            Pb.Width = img.Width;
            Pb.Height = img.Height;
            Pb.BackColor = Color.Transparent;
            Pb.Top = top;
            Pb.Left = left;
            this.movement = movement;
            this.Type = type;
            if (health == true)
            {
                H = new ProgressBar();
                H.Left = Pb.Left;
                H.Top = Pb.Height;
                H.Value = 100;
                H.Width = Pb.Width;
                H.Height = 10;
            }
        }

        public GameObject(PictureBox pb, ObjectTypes type, IMovement movement)
        {
            Pb = pb;
            this.movement = movement;
            this.Type = type;
        }

        public PictureBox Pb { get => pb; set => pb = value; }
        public ObjectTypes Type { get => type; set => type = value; }
        public ProgressBar H { get => h; set => h = value; }
        internal IMovement Movement { get => movement; set => movement = value; }

        public void move()
        {
            this.pb.Location = Movement.move(this.pb.Location);
            if (this.H != null)
            {
                H.Left = this.pb.Location.X ;
                H.Top = this.pb.Location.Y + pb.Height;
            }
        }
    }
}
