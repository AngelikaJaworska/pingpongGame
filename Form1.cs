using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pingpong
{
    public partial class GameForm : Form
    {
        private int x1;
        private int x2;
        private int x3;

        private int y1;
        private int y2;
        private int y3;

        public Bitmap face1;
        public Bitmap face2;
        public Bitmap ball;

        //after ball speed change, change also faces speeds!

        private const int SIZE_FACE = 60; //size of faces
        private const int SIZE_BALL = 40; //size of the ball

        private const int SPEED_Y = 5; //speed of the ball
        public int speed_top = SPEED_Y;
        public int speed_left = 8;

        private const int SPEED_FACE = 10; //speeds of faces

        public int score_face1 = 0; //score
        public int score_face2 = 0;

        public GameForm()
        {
            InitializeComponent();

            SetNewDimension();

            face1 = new Bitmap("face1.jpg");
            face2 = new Bitmap("face2.jpg");
            ball = new Bitmap("ball.png");
            
        }

        public void SetNewDimension()
        {
            x1 = 30;
            y1 = 200;

            x2 = 590;
            y2 = 200;

            x3 = 300;
            y3 = 200;
        }
                      
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W)
            {
                y1 -= SPEED_FACE;
            }

            if (e.KeyCode == Keys.S)
            {
                y1 += SPEED_FACE;
            }

            if (e.KeyCode == Keys.Up)
            {
                y2 -= SPEED_FACE;
            }

            if (e.KeyCode == Keys.Down)
            {
                y2 += SPEED_FACE;
            }
            
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Check();
            BallMove();
            
            e.Graphics.DrawImage(face1, x1, y1, SIZE_FACE, SIZE_FACE);
            e.Graphics.DrawImage(face2, x2, y2, SIZE_FACE, SIZE_FACE);
            e.Graphics.DrawImage(ball, x3, y3, SIZE_BALL, SIZE_BALL);
                           
            this.Invalidate();
        }

        public void BallMove()
        {
            x3 += speed_left; //set the speed of the ball
            y3 += speed_top;


            if (x3 >= x2 - (SIZE_FACE / 2) && y3 >= y2 - (SIZE_FACE / 2) && y3 <= y2 + (SIZE_FACE / 2) && speed_left > 0) //collision with player blue
            {
                if (y3 < y2 + (SIZE_FACE / 3)) //upside
                {
                    speed_top = -SPEED_Y;
                }
                else if (y3 == y2 + ((2 * SIZE_FACE) / 3)) //middle
                {
                    speed_top = 0;
                }
                else if (y3 < y2 + SIZE_FACE) //downside    //sign change >
                {
                    speed_top = SPEED_Y;
                }
                speed_left = -speed_left; //negative - will go left
               
            }

            if (x3 <= x1 + (SIZE_FACE / 2) && y3 >= y1 - (SIZE_FACE / 2) && y3 <= y1 + (SIZE_FACE / 2) && speed_left < 0) //collision with player yellow
            {
                if (y3 < y1 + (SIZE_FACE / 3)) 
                {
                    speed_top = -SPEED_Y;
                }
                else if (y3 == y1 + ((2 * SIZE_FACE) / 3)) 
                {
                    speed_top = 0;
                }
                else if (y3 < y1 + SIZE_FACE)    //sign change >
                {
                    speed_top = SPEED_Y;
                }
                speed_left = -speed_left;  //possitive - will go right

            }

            if(x3 > 650 || x3 < 0)  
            {
                if (y3 < 0) //top
                {
                    speed_top = -speed_top;
                    speed_left = -speed_left;
                }

                if (y3 > 420) //bottom
                {
                    speed_top = -speed_top;
                    speed_left = -speed_left;
                }

                else //ball run out of the playground
                {
                    if(x3 < 0) 
                    {
                        score_face1 += 1;
                        SetNewDimension();
                        MessageBox.Show("Player blue wins! Score: " + score_face2.ToString() + ":" + score_face1.ToString());
                    }
                    else
                    {
                        score_face2 += 1;
                        SetNewDimension();
                        MessageBox.Show("Player yellow wins! Score: " + score_face2.ToString() + ":" + score_face1.ToString());
                    }
                    
                }
            }

            else
            {
                if (y3 < 0)
                {
                    speed_top = -speed_top;
                }

                if (y3 > 420)
                {
                    speed_top = -speed_top;
                }
            }
           

        }

        public void Check() //avoid runing out faces from playground
        {
            if(y1 < 0)
            {
                y1 = 0;
            }

            if(y1 > 400)
            {
              y1 = 400;
            }

            if(y2 < 0)
            {
                y2 = 0;
            }

            if(y2 > 400)
            {
               y2 = 400;
            }

        }


       
    }
}
