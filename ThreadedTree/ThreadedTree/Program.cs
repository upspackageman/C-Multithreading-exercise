using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedTree
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadBinaryTree tree = new ThreadBinaryTree();
            int x, select;
            while (true)
            {
                Console.WriteLine("1. Insert a new node");
                Console.WriteLine("2. Delete a node");
                Console.WriteLine("3. Inorder Teraversal");
                Console.WriteLine("4. Exit");

                Console.Write("Enter your selection: ");

                select = Convert.ToInt32(Console.ReadLine());
                if (select == 4)
                    break;

                switch (select)
                {
                    case 1:
                        Console.Write("Enter the key to be inserted: ");
                        x = Convert.ToInt32(Console.ReadLine());
                        tree.insert(x);
                        break;
                    case 2:
                        Console.Write("Enter the key to be deleted: ");
                        x = Convert.ToInt32(Console.ReadLine());
                        tree.delete(x);
                        break;
                    case 3:
                        tree.Inorder();
                        break;
                    default:
                        Console.WriteLine("Not within parameter");
                        break;


                }

            }
        }
    }
}
