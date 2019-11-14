using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainReaction
{
    class Node
    {

        char direction;
        List<char> directions = new List<char>(new char[]{ 'd', 'L', 'r', '7' }); //up+left, up+right, down+right, down+left
        string name;
        List<string> paired; //order of pairs = 0=UP, 1=Down, 2=Left, 3=Right
        List<Node> pairedNodes;

        public char GetDirection()
        {
            return direction;
        }

        public List<string> GetPairedStrings()
        {
            return paired;
        }

        public List<Node> GetPairedNodes()
        {
            return pairedNodes;
        }

        public Node()
        {
            name = "00";
            direction = '0';
        }

        public Node(string id, Random rng)
        {
            direction = directions[rng.Next(4)];
            name = id;
            paired = new List<string>();
            pairedNodes = new List<Node>();
            StringBuilder toAdd = new StringBuilder();
            if (id[0] != 'A')
            {
                toAdd.Append((char)(id[0] - 1));
                toAdd.Append(id[1]);
                paired.Add(toAdd.ToString());
                toAdd.Clear();
            }
            else
                paired.Add("00");
            if (id[0] != 'I')
            {
                toAdd.Append((char)(id[0] + 1));
                toAdd.Append(id[1]);
                paired.Add(toAdd.ToString());
                toAdd.Clear();
            }
            else
                paired.Add("00");
            if (id[1] != '1')
            {
                toAdd.Append(id[0]);
                toAdd.Append((char)(id[1] - 1));
                paired.Add(toAdd.ToString());
                toAdd.Clear();
            }
            else
                paired.Add("00");
            if (id[1] != '9')
            {
                toAdd.Append(id[0]);
                toAdd.Append((char)(id[1] + 1));
                paired.Add(toAdd.ToString());
                toAdd.Clear();
            }
            else
                paired.Add("00");
        }

        public List<string> Move()
        {
            List<string> toCheck = new List<string>();
            direction = directions[(directions.IndexOf(direction) + 1) % 4];
            if (direction == 'd')
            {
                if (pairedNodes[0].GetDirection() == 'r' || pairedNodes[0].GetDirection() == '7')
                    toCheck.Add(paired[0]);
                if (pairedNodes[2].GetDirection() == 'L' || pairedNodes[2].GetDirection() == 'r')
                    toCheck.Add(paired[2]);
            }
            else if (direction == 'L')
            {
                if (pairedNodes[0].GetDirection() == 'r' || pairedNodes[0].GetDirection() == '7')
                    toCheck.Add(paired[0]);
                if (pairedNodes[3].GetDirection() == 'd' || pairedNodes[3].GetDirection() == '7')
                    toCheck.Add(paired[3]);
            }
            else if (direction == 'r')
            {
                if (pairedNodes[1].GetDirection() =='d' || pairedNodes[1].GetDirection() == 'L')
                    toCheck.Add(paired[1]);
                if (pairedNodes[3].GetDirection() == 'd' || pairedNodes[3].GetDirection() == '7')
                    toCheck.Add(paired[3]);
            }
            else if (direction == '7')
            {
                if (pairedNodes[1].GetDirection() == 'd' || pairedNodes[1].GetDirection() == 'L')
                    toCheck.Add(paired[1]);
                if (pairedNodes[2].GetDirection() == 'L' || pairedNodes[2].GetDirection() == 'r')
                    toCheck.Add(paired[2]);
            }
            return toCheck;
        }

        public void PairNode(Node n)
        {
            pairedNodes.Add(n);
        }
    }
}
