using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheExitorDesktop
{   
    /// <summary>
    /// Generátor bludiště
    /// </summary>
    class MazeGenerator
    {
        int[,] pole;
        int[] vectors;
        int sizeX;
        int sizeY;    
        List<Point> position;
        public List<Point> Base { get; private set; }

        Random rnd = new Random();

        public bool IsDebug { private get; set; }


        public MazeGenerator()
        {
            rnd = new Random();
            position = new List<Point>();
            Base = new List<Point>();           
            vectors = new int[] { 1, 0, -1, 0, 1 };
            IsDebug = false;
        }

        public int[,] Generate(int width, int height)
        {

            pole = new int[width, height];
            sizeX = pole.GetLength(0);
            sizeY = pole.GetLength(1);
            position.Clear();          
            PrepareBase(); // připraví základní pozice 
            Put(Base[rnd.Next(0, Base.Count)], rnd.Next(0, Base.Count));

            return pole;
        }

        private bool Put(Point put,int n)
        {
        
            int index =rnd.Next(0,4);
            int sX = vectors[index];
            int sY = vectors[index + 1]; 
            int pX = put.X;
            int pY = put.Y;

            for (int p = 0; p < n; p++)
            {
                if (pX >= 0 && pX < pole.GetLength(0) && pY >= 0 && pY < pole.GetLength(1))
                {
                    if (pole[pX, pY] == 2)
                        Base.Remove(new Point(pX, pY));
                    else if (pole[pX, pY] == -1)
                        break;
                    pole[pX, pY] = -1;
                }
                else break;
                pX += sX;
                pY += sY;
            }        
            if (Base.Count == 0)
                return true;
            else
                return Put(Base[rnd.Next(0, Base.Count)], rnd.Next(2,9));
        }

        private void PrepareBase()
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (x == 0 || x == sizeX - 1 || y == 0 || y == sizeY - 1)
                    { pole[x, y] = -1; }// základní zeď
                    else if (x % 2 == 0 && y % 2 == 0 && !(x == 0 || x == sizeX - 2 || y == 0 || y == sizeY - 2))
                    { pole[x, y] = 2; Base.Add(new Point(x, y)); }

                }

            }
        }

    }
}
