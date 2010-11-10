using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PetriNets
{
    class Lines
    {
        int[,] field_matrix;
        Dictionary<Point, Point> points;
        public Dictionary<Point, Point> Points
        { get { return points; } }




        public int[,] Field_matrix
        { set { field_matrix = value; }
          get { return field_matrix; }
        }
        int sc;
        public int Scale
        { set { sc = value; } }

        public Lines(int scale)
        {
            sc = scale;
        }
        


        public bool connect_two_points(Point from, Point to)
        {

            int ot = 20;
            Point[] inp, outp;
            points = new Dictionary<Point, Point>();
            

            //Линия из кружка в кирпич
            if (field_matrix[from.X, from.Y] > 10)
            {
                //Кружок левее кирпича
                if (to.X < from.X)
                { inp = output(to, true); }
                else //Кружок правее кирпича
                { inp = input(to, true); }

                outp = output(from, false);

                int f = 0, t = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (field_matrix[inp[i].X / sc - ((to.X < from.X) ? 0 : 1), inp[i].Y / sc] == 0)
                    {
                        f = i % 3;
                        t = i;
                        break;
                    }
                }

                if (to.Y + 1 == from.Y) //На одной высоте
                {
                    inp = input(to, true);
                    one_height(inp, outp, ot, f, t);
                    if (f == 0)
                    {
                      field_matrix[outp[f].X / sc, outp[f].Y / sc] = 1;
                    }
                    else
                 {
                        field_matrix[outp[f].X / sc + 1, outp[f].Y / sc] = 1;
                    }
                }
                else                    //Не на одной высоте
                {
                    if ((to.Y - from.Y) > 2) //Кружок выше
                    {
                        for (int i = 6; i < 9; i++)
                        {
                            if (field_matrix[outp[i].X / sc, (outp[i].Y) / sc + (i == 6 ? 0 : 1)] == 0)
                            {
                                f = i;
                                break;
                            }
                        }
                        field_matrix[outp[f].X / sc, outp[f].Y / sc + (f == 6 ? 0 : 1)] = 1;
                    }
                    else
                        if ((to.Y - from.Y) < -2) // Кружок ниже
                        {
                            for (int i = 3; i < 6; i++)
                            {
                                if (field_matrix[outp[i].X / sc, outp[i].Y / sc - 1] == 0)
                                {
                                    f = i;
                                    break;
                                }
                            }

                            field_matrix[outp[f].X / sc, outp[f].Y / sc - 1] = 1;
                        }

                    if (from.X > to.X) ot = -ot;
                    dif_height(inp, outp, ot, f, t);
                }


                field_matrix[inp[t].X / sc - ((to.X < from.X) ? ((to.Y + 1 == from.Y) ? 1 : 0) : 1), inp[t].Y / sc] = 1;

            }
            else //Из кирпича в кружок
            {
                //inp - кружок
                //outp - кирпич

                inp = input(to, false);
                outp = output(from, true); 



                int f = 0, t = 0;
                for (int i = 0; i < 5; i++)
                {
                    if (field_matrix[outp[i].X / sc - ((to.X > from.X) ? 0 :((to.Y > from.Y)?0:2)), outp[i].Y / sc] == 0)
                    {
                        f = i;
                        t = i%3;
                        break;
                    }
                }

                if (to.Y == from.Y + 1) //На одной высоте
                {

                    one_height(inp, outp, ot, f%3, t);
                    field_matrix[outp[f].X / sc, outp[f].Y / sc] = 1;

                }
                else                    //Не на одной высоте
                {

                    if (to.X < from.X) //Кружок левее кирпича
                    { outp = input(from, true); }

                    field_matrix[outp[f].X / sc - ((to.X > from.X) ? 0 : 1), outp[f].Y / sc] = 1;  
                   // if (from.X > to.X) ot = -ot;
                      dif_height_back(inp, outp, ot, f, t);
                   // one_height(inp, outp, ot, f % 3, t % 3);
                }
                field_matrix[inp[t].X / sc - (f == 0 ? 1 : 0), inp[t].Y / sc] = 1;
            }



            return true;
        }

        //Check x-axis
        private bool testX(Point from, Point to)
        {
            bool free = true;
            int xf = Math.Min(from.X, to.X) / sc;
            int xt = Math.Max(from.X, to.X) / sc;
            for (int i = xf; i < xt; i++)
            {
                if (field_matrix[i, from.Y / sc] != 0) free = false;
            }
            return free;
        }

        //Check y-axis
        private bool testY(Point from, Point to)
        {
            bool free = true;
            int yf = Math.Min(from.Y, to.Y) / sc;
            int yt = Math.Max(from.Y, to.Y) / sc;
            for (int i = yf; i < yt; i++)
            {
                if (field_matrix[from.X / sc, i] != 0) free = false;
            }
            return free;
        }

        private Point m(Point p)
        {
            return new Point((int)((p.X + 0.5) * sc), (int)((p.Y + 0.5) * sc));
        }

        public Point mo(Point p)
        {
            return new Point((int)(p.X / sc), (int)(p.Y / sc));
        }

        private Point mvh(Point p, double distancever, double distancehor)
        {
            return new Point((int)((p.X + 0.5 + distancehor) * sc), (int)((p.Y + 0.5 + distancever) * sc));
        }

        //On one height
        private void one_height(Point []  inp, Point [] outp, int ot, int f, int t) 
        {
            Point a = new Point(outp[f].X + ot, outp[f].Y);
            Point b = new Point(inp[t].X - ot, inp[t].Y);
            if (!testX(a, b))
            {
                Point a0 = new Point(outp[f].X + ot, outp[f].Y);
                Point b0 = new Point(inp[t].X - ot, inp[t].Y);
//Сюда тот цикл со сменой знака
                for (int i = 0; i < field_matrix.GetLength(0); i++)
                {
                    if (testX(a, b)) break;
                    a.Y += (int)Math.Pow(-1, i) * (i + 1) * sc;
                    b.Y += (int)Math.Pow(-1, i) * (i + 1) * sc;

                }

                for (int i = 0; i < field_matrix.GetLength(0); i++)
                {
                    if (testY(a0, a)) break;
                    a0.X += (int)Math.Pow(-1, i) * (i + 1) * sc;
                    a.X += (int)Math.Pow(-1, i) * (i + 1) * sc;

                }

                for (int i = 0; i < field_matrix.GetLength(0); i++)
                {
                    if (testY(b0, b)) break;
                    b0.X += (int)Math.Pow(-1, i) * (i + 1) * sc;
                    b.X += (int)Math.Pow(-1, i) * (i + 1) * sc;

                }
                points.Add(outp[f], a0);
                points.Add(a0, a);
                points.Add(a, b);
                points.Add(b, b0);
                points.Add(b0, inp[t]);
                
            }
            else
            {
                points.Add(outp[f], inp[t]);
            }

            int ar = 5;
            points.Add(new Point(inp[t].X - ar, inp[t].Y - ar), inp[t]);
            points.Add(new Point(inp[t].X - ar, inp[t].Y + ar), inp[t]);
        }

        //On dif height
        private void dif_height(Point[] inp, Point[] outp, int ot, int f, int t)
        {
            Point a = new Point(outp[f].X, inp[t].Y);
            Point b = new Point(inp[t].X - ot, inp[t].Y);
            if (!testX(a, b) || !testY(outp[f],a))
            {
                Point b0 = new Point(inp[t].X - ot, inp[t].Y);
                Point a0 = new Point(outp[f].X, outp[f].Y - ot);
                Point a1 = a0;

                //And here
                    for (int i = 0; i < field_matrix.GetLength(0); i++)
                    {
                        if (testX(a, b)) break;
                        a.Y +=(int)Math.Pow(-1, i) * (i + 1) * sc;
                        b.Y +=(int)Math.Pow(-1, i) * (i + 1) * sc;
                        
                    }

                    for (int i = 0; i < field_matrix.GetLength(0); i++)
                    {
                        if (testY(a0, a)) break;
                        a0.X += (int)Math.Pow(-1, i) * (i + 1) * sc;
                        a.X += (int)Math.Pow(-1, i) * (i + 1) * sc;
                        
                    }

                    for (int i = 0; i < field_matrix.GetLength(0); i++)
                    {
                        if (testY(b0, b)) break;
                        b0.X += (int)Math.Pow(-1, i) * (i + 1) * sc;
                        b.X += (int)Math.Pow(-1, i) * (i + 1) * sc;

                    }

               try
              {
                    points.Add(outp[f], a1);
                   if (!a1.Equals(a0)) points.Add(a1, a0);
                   points.Add(a0, a);
                    points.Add(a, b);
                   if(!b.Equals(b0)) points.Add(b, b0);
                    points.Add(b0, inp[t]);
                }
               catch (Exception) {  }
            }
            else
            {
                points.Add(outp[f], a);
                points.Add(a, inp[t]);
            }

            int ar = 5;
            if (inp[t].X < outp[f].X) ar = -ar;
            points.Add(new Point(inp[t].X - ar, inp[t].Y - ar), inp[t]);
            points.Add(new Point(inp[t].X - ar, inp[t].Y + ar), inp[t]);
        }

        private void dif_height_back(Point[] inp, Point[] outp, int ot, int f, int t)
        {
           // if (inp[t].X < outp[f].X) ot = -ot; 
            Point a = new Point(inp[t].X - ot, outp[f].Y);
            Point b = new Point(inp[t].X - ot, inp[t].Y);
            if (!testX(outp[f], a))
            {
                Point b0 = b;
                Point b1 = b0;
                Point a0 = new Point(outp[f].X - ot, outp[f].Y);
                Point a1 = a0;

                    for (int i = 0; i < field_matrix.GetLength(0); i++)
                    {
                        if (testX(a, a0)) break;
                        a.Y += (int)Math.Pow(-1, i) * (i + 1) * sc;
                        a0.Y += (int)Math.Pow(-1, i) * (i + 1) * sc;
                    }

                    for (int i = 0; i < field_matrix.GetLength(0); i++)
                    {
                        if (testY(a, b)) break;
                        a.X += (int)Math.Pow(-1, i) * (i + 1) * sc;
                        b.X += (int)Math.Pow(-1, i) * (i + 1) * sc;

                        for (int j = 0; j < field_matrix.GetLength(0); j++)
                        {
                            if (testX(b0, b)) break;
                            b0.Y += (int)Math.Pow(-1, j) * (j + 1) * sc;
                            b.Y += (int)Math.Pow(-1, j) * (j + 1) * sc;
                        }
                    }
                points.Add(outp[f], a1);
                if (!a1.Equals(a0)) points.Add(a1, a0);
                points.Add(a0, a);
                points.Add(a, b);
                if (!b.Equals(b0)) points.Add(b, b0);
                if (!b0.Equals(b1)) points.Add(b0, b1);
                points.Add(b1, inp[t]);
            }
            else
            {
                try
                {
                    points.Add(outp[f], a);
                    points.Add(a, b);
                    points.Add(b, inp[t]);
                }
                catch (Exception)
                {
                    
                }
            }

            int ar = 5;
            points.Add(new Point(inp[t].X - ar, inp[t].Y - ar), inp[t]);
            points.Add(new Point(inp[t].X - ar, inp[t].Y + ar), inp[t]);
        }

        private Point[] input(Point p, bool time)
        {

            if (time)
            {
                Point[] timeleft = new Point[5];
                timeleft[0] = mvh(p, 2, -0.5);
                timeleft[1] = mvh(p, 1, -0.5);
                timeleft[2] = mvh(p, 3, -0.5);
                timeleft[3] = mvh(p, 0, -0.5);
                timeleft[4] = mvh(p, 4, -0.5);
                return timeleft;
            }
            else
            {
                Point[] pos = new Point[3];
                pos[0] = mvh(p, 1, - 0.5);
                pos[1] = mvh(p, 0, - 0.7);
                pos[2] = mvh(p, 2, - 0.7);
                return pos;
            }
        }

        private Point [] output(Point p, bool time)
        {
            if (time)
            {
                Point[] timeright = new Point[5];
                timeright[0] = mvh(p, 2, 0.5);
                timeright[1] = mvh(p, 1, 0.5);
                timeright[2] = mvh(p, 3, 0.5);
                timeright[3] = mvh(p, 0, 0.5);
                timeright[4] = mvh(p, 4, 0.5);
                return timeright;
            }
            else
            {
                Point[] pos = new Point[9];
                //право
                pos[0] = mvh(p, 1, 3 - 0.5);
                pos[1] = mvh(p, 0, 3 - 0.7);
                pos[2] = mvh(p, 2, 3 - 0.7);
                //верх
                pos[3] = mvh(p, -0.5, 1);
                pos[4] = mvh(p, -0.7, 0);
                pos[5] = mvh(p, -0.7, 2);
                //низ
                pos[6] = mvh(p, 3 - 0.5, 1);
                pos[7] = mvh(p, 3 - 0.7, 2);
                pos[8] = mvh(p, 3 - 0.7, 0);
                return pos;
            }

        }

    }
}
