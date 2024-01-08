using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheExitorDesktop
{
    /// <summary>
    /// Třída pro algoritmus vlny
    /// </summary>
    public class Wave
    {
        List<Point> path;
        int[,] arr;
        int[] vectors;
        Queue<Point> pos; // pozice bodů vlny     

        Point curr, start, finish, finPoint;
        int x, y, id;

        public Wave()
        {
            path = new List<Point>();
            pos = new Queue<Point>();
            vectors = new int[] { -1, 1, -1, 1 };
        }

        /// <summary>
        /// Najde cestu z bodu A do bodu B
        /// </summary>
        /// <param name="source"></param>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public List<Point> FindPath(int[,] source, Point start, Point finish)
        {
            this.start = start;
            this.finish = finish;
            path.Clear();
            pos.Clear();
            arr = new int[source.GetLength(0), source.GetLength(1)];
            Array.Copy(source, arr, source.Length);
            pos.Enqueue(start);

            DoWave();

            bool loop = true;
            while (loop)
            {
                x = curr.X;
                y = curr.Y;
                loop = false;
                for (int k = 0; k < vectors.Length; k++)
                {
                    if (k < 2)
                        x += vectors[k];
                    else y += vectors[k];

                    if (x >= 0 && x < arr.GetLength(0) && y >= 0 && y < arr.GetLength(1) && arr[x, y] == id - 1)
                    {
                        path.Add(new Point(x, y));
                        curr = new Point(x, y);
                        id = arr[x, y];
                        if (IsNearFinish(new Point(x, y), start))
                        {
                            path.Add(finPoint);
                            loop = false;
                        }
                        else
                            loop = true;

                        break;

                    }
                    x = curr.X;
                    y = curr.Y;
                }
            }

            return path;
        }

        /// <summary>
        /// Rozšíří vlnu a nalezne volná místa okolo podle zadaného rozsahu
        /// </summary>
        /// <param name="start"></param>
        /// <param name="range">Rozsah šíření</param>
        /// <returns></returns>
        public List<Point> FindPlaces(Point start, int range)
        {
            return new List<Point>();
        }

        private void DoWave()
        {
            while (pos.Count > 0)
            {
                curr = pos.Dequeue();
                x = curr.X;
                y = curr.Y;
                for (int k = 0; k < vectors.Length; k++)
                {
                    if (k <= 1)
                        x += vectors[k];
                    else y += vectors[k];
                  
                    if (x >= 0 && x < arr.GetLength(0) && y >= 0 && y < arr.GetLength(1) && arr[x, y] == 0 && (x != start.X || y != start.Y))
                    {
                        id = arr[curr.X, curr.Y] + 1;
                        pos.Enqueue(new Point(x, y));
                        arr[x, y] = id;
                      
                        // nalezen cíl
                        if (IsNearFinish(new Point(x, y), finish))
                        {
                            id++;
                            curr = new Point(finPoint.X, finPoint.Y);                          
                            pos.Clear();
                            arr[finPoint.X, finPoint.Y] = id;
                            break;
                        }
                    }
                    x = curr.X;
                    y = curr.Y;
                }
            }
        }

        private bool IsNearFinish(Point pos, Point fin)
        {
            int px = 0;
            int py = 0;

            for (int i = 0; i < vectors.Length; i++)
            {
                px = pos.X;
                py = pos.Y;
                if (i < 2)
                    px += vectors[i];
                else py += vectors[i];

                if (px == fin.X && py == fin.Y)
                {
                    finPoint = new Point(px, py);
                    return true;
                }
            }

            return false;
        }


    }
}
