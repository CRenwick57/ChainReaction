using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRENTestApp
{
    class ChainReaction
    {

        Dictionary<string, int> locations;
        List<Node> board;
        List<int> toMove;
        List<string> willMove;
        int score;
        int hiscore;
        char[] columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H','I' };
        char[] rows = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public ChainReaction()
        {
            int position = 0;
            score = 0;
            hiscore = 0;
            Random rng = new Random();
            board = new List<Node>();
            locations = new Dictionary<string, int>();
            toMove = new List<int>();
            willMove = new List<string>();
            StringBuilder id = new StringBuilder();
            for (int i = 0; i < columns.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    id.Clear();
                    id.Append(columns[i]);
                    id.Append(rows[j]);
                    Node n = new Node(id.ToString(), rng);
                    board.Add(n);
                    locations.Add(id.ToString(), position);
                    position++;
                }
            }
            PairNodes();

        }

        private void PairNodes()
        {
            foreach (Node n in board)
            {
                List<string> pairedStrings = n.GetPairedStrings();
                foreach (string id in pairedStrings)
                {
                    if (!id.Equals("00"))
                        n.PairNode(board[locations[id]]);
                    else
                        n.PairNode(new Node());
                }
            }
        }

        private void Display()
        {
            for (int i = 0; i < 81; i += 9)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i + j].GetDirection());
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void StartMove(string click)
        {
            toMove.Add(locations[click]);
            MakeMove();
        }

        private void MakeMove()
        {
            int index;
            while (toMove.Count > 0)
            {
                foreach (int i in toMove)
                {
                    willMove.Extend(board[i].Move());
                    score++;
                }
                toMove.Clear();
                foreach (string s in willMove)
                {
                    index = locations[s];
                    if (!toMove.Contains(index))
                    {
                        toMove.Add(index);
                    }
                }
                willMove.Clear();
                Display();
                System.Threading.Thread.Sleep(100);
            }
        }

        public void Play()
        {
            string click = string.Empty;
            Display();
            while (click != "00")
            {
                Console.WriteLine("Pick a position:");
                click = Console.ReadLine();
                if (locations.Keys.Contains(click))
                {
                    score = 0;
                    StartMove(click);
                    if (score > hiscore)
                    {
                        if (hiscore != 0)
                            Console.WriteLine("New High Score!");
                        hiscore = score;
                    }
                    Console.WriteLine(score);
                }
            }
        }

    }
}
