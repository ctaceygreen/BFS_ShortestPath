using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {

            int[][] mat = new int[9][] {
                new int[]{ 1, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                new int[]{ 1, 0, 1, 0, 1, 1, 1, 0, 1, 1 },
                new int[]{ 1, 1, 1, 0, 1, 1, 0, 1, 0, 1 },
                new int[]{ 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
                new int[]{ 1, 1, 1, 0, 1, 1, 1, 0, 1, 0 },
                new int[]{ 1, 0, 1, 1, 1, 1, 0, 1, 0, 0 },
                new int[]{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                new int[]{ 1, 0, 1, 1, 1, 1, 0, 1, 1, 1 },
                new int[]{ 1, 1, 0, 0, 0, 0, 1, 0, 0, 1 } 
            };

            Point source = new Point { x = 0, y = 0 };
            Point dest = new Point { x = 3, y = 4 };

            int distance = BFS(mat, source, dest);

            Console.WriteLine($"Shortest path is {distance}");
            Console.ReadLine();

        }

        // check whether given cell (row, col) is a valid 
        // cell or not. 
        static bool isValid(int row, int col, int maxRow, int maxCol)
        {
            // return true if row number and column number 
            // is in range 
            return (row >= 0) && (row < maxRow) &&
                   (col >= 0) && (col < maxCol);
        }

        public static int BFS(int[][] matrix, Point source, Point dest)
        {
            // These arrays are used to get row and column 
            // numbers of 4 neighbours of a given cell 
            int[] neighbourRowNum = { -1, 0, 0, 1 };
            int[] neighbourColNum = { 0, -1, 1, 0 };

            int maxRow = matrix.GetLength(0);
            int maxCol = matrix[0].Length;

            //Check that source and dest are a 1 in the matrix (they can be accessed)
            if (matrix[source.x][source.y] != 1 || matrix[dest.x][dest.y] != 1)
            {
                return -1;
            }

            bool[][] visited = new bool[matrix.GetLength(0)][];
            for(int i = 0; i < maxRow; i++)
            {
                visited[i] = new bool[10];
            }
            visited[source.x][source.y] = true; //Start at source

            //Create a queue for BFS
            Queue<QueueNode> queue = new Queue<QueueNode>();

            //Add source to queue to start off
            queue.Enqueue(new QueueNode { point = source, Distance = 0 });

            //Do BFS from source cell
            while(queue.Count > 0)
            {
                //Get next in the queue
                QueueNode node = queue.Dequeue();

                Point point = node.point;

                //If we have reached the destination then return the distance
                if(point.x == dest.x && point.y == dest.y)
                {
                    return node.Distance;
                }

                //Look at each direction of the 4 potentials from this point
                for(int i = 0; i < 4; i++)
                {
                    int row = point.x + neighbourRowNum[i];
                    int col = point.y + neighbourColNum[i];

                    //If cell can be accessed (1) and hasn't been visited and is a valid cell in the matrix
                    if (isValid(row, col, maxRow, maxCol) && matrix[row][col] == 1 && !visited[row][col])
                    {
                        visited[row][col] = true;
                        QueueNode neighbourNode = new QueueNode { point = new Point { x = row, y = col }, Distance = node.Distance + 1 };
                        queue.Enqueue(neighbourNode);
                    }

                }

            }

            //If we are here then we never met the destination so return -1
            return -1;
        }
    }
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    public class QueueNode
    {
        public Point point { get; set; }
        public int Distance { get; set; }
    }
}
