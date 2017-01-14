using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedTree
{
    class Node
    {
        public Node left;
        public bool leftThread;
        public int info;
        public bool rightThread;
        public Node right; 

        public Node(int i)
        {
            info = i;
            left = null;
            right = null;
            leftThread = true;
            rightThread = true;
        }
    }
}
