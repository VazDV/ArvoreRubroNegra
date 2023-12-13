using System.Xml.Linq;

namespace ArvoreRubroNegra
{
    public class RBTree
    {
        private RBNode root;
        private RBNode nil; // Nó nulo, representando um nó folha

        public RBTree()
        {
            nil = new RBNode(-1);
            nil.NodeColor = Color.Black;
            root = nil;
        }

        public void Insert(int key)
        {
            RBNode node = new RBNode(key);
            RBNode current = root;
            RBNode parent = null;

            while (current != nil)
            {
                parent = current;

                if (node.Key < current.Key)
                    current = current.Left;
                else
                    current = current.Right;
            }

            node.Parent = parent;

            if (parent == null)
                root = node;
            else if (node.Key < parent.Key)
                parent.Left = node;
            else
                parent.Right = node;

            node.Left = nil;
            node.Right = nil;
            node.NodeColor = Color.Red;

            FixInsert(node);
        }

        private void FixInsert(RBNode node)
        {
            while (node != null && node.Parent != null && node.Parent.NodeColor == Color.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    RBNode uncle = node.Parent.Parent.Right;

                    if (uncle.NodeColor == Color.Red)
                    {
                        node.Parent.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }

                        if (node != null && node.Parent != null)
                        {
                            node.Parent.NodeColor = Color.Black;
                            if (node.Parent.Parent != null)
                                node.Parent.Parent.NodeColor = Color.Red;
                            RightRotate(node.Parent.Parent);
                        }
                    }
                }
                else
                {
                    RBNode uncle = node.Parent.Parent.Left;

                    if (uncle.NodeColor == Color.Red)
                    {
                        node.Parent.NodeColor = Color.Black;
                        uncle.NodeColor = Color.Black;
                        node.Parent.Parent.NodeColor = Color.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }

                        if (node != null && node.Parent != null)
                        {
                            node.Parent.NodeColor = Color.Black;
                            if (node.Parent.Parent != null)
                                node.Parent.Parent.NodeColor = Color.Red;
                            LeftRotate(node.Parent.Parent);
                        }
                    }
                }
            }

            if (node != null && node.Parent == null)
                root.NodeColor = Color.Black;
        }

        private void LeftRotate(RBNode x)
        {
            RBNode y = x.Right;
            x.Right = y.Left;

            if (y.Left != nil)
                y.Left.Parent = x;

            y.Parent = x.Parent;

            if (x.Parent == null)
                root = y;
            else if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;

            y.Left = x;
            x.Parent = y;
        }

        private void RightRotate(RBNode y)
        {
            RBNode x = y.Left;
            y.Left = x.Right;

            if (x.Right != nil)
                x.Right.Parent = y;

            x.Parent = y.Parent;

            if (y.Parent == null)
                root = x;
            else if (y == y.Parent.Left)
                y.Parent.Left = x;
            else
                y.Parent.Right = x;

            x.Right = y;
            y.Parent = x;
        }

        public void Delete(int key)
        {
            RBNode node = Search(key);

            if (node == null || node == nil)
            {
                Console.WriteLine("Node not found.");
                return;
            }

            RBNode temp = node;
            Color originalColor = temp.NodeColor;
            RBNode x;

            if (node.Left == nil)
            {
                x = node.Right;
                Transplant(node, node.Right);
            }
            else if (node.Right == nil)
            {
                x = node.Left;
                Transplant(node, node.Left);
            }
            else
            {
                temp = Minimum(node.Right);

                if (temp == null || temp == nil)
                {
                    Console.WriteLine("Error: temp is null or nil.");
                    return;
                }

                originalColor = temp.NodeColor;
                x = temp.Right;

                if (temp.Parent == node)
                    x.Parent = temp;
                else
                {
                    Transplant(temp, temp.Right);
                    temp.Right = node.Right;
                    temp.Right.Parent = temp;
                }

                Transplant(node, temp);
                temp.Left = node.Left;
                temp.Left.Parent = temp;
                temp.NodeColor = node.NodeColor;
            }

            if (originalColor == Color.Black)
                FixDelete(x);
        }


        private void FixDelete(RBNode x)
        {
            while (x != root && x.NodeColor == Color.Black)
            {
                if (x == x.Parent.Left)
                {
                    RBNode w = x.Parent.Right;

                    if (w.NodeColor == Color.Red)
                    {
                        w.NodeColor = Color.Black;
                        x.Parent.NodeColor = Color.Red;
                        LeftRotate(x.Parent);
                        w = x.Parent.Right;
                    }

                    if (w.Left.NodeColor == Color.Black && w.Right.NodeColor == Color.Black)
                    {
                        w.NodeColor = Color.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Right.NodeColor == Color.Black)
                        {
                            w.Left.NodeColor = Color.Black;
                            w.NodeColor = Color.Red;
                            RightRotate(w);
                            w = x.Parent.Right;
                        }

                        w.NodeColor = x.Parent.NodeColor;
                        x.Parent.NodeColor = Color.Black;
                        w.Right.NodeColor = Color.Black;
                        LeftRotate(x.Parent);
                        x = root;
                    }
                }
                else
                {
                    RBNode w = x.Parent.Left;

                    if (w.NodeColor == Color.Red)
                    {
                        w.NodeColor = Color.Black;
                        x.Parent.NodeColor = Color.Red;
                        RightRotate(x.Parent);
                        w = x.Parent.Left;
                    }

                    if (w.Right.NodeColor == Color.Black && w.Left.NodeColor == Color.Black)
                    {
                        w.NodeColor = Color.Red;
                        x = x.Parent;
                    }
                    else
                    {
                        if (w.Left.NodeColor == Color.Black)
                        {
                            w.Right.NodeColor = Color.Black;
                            w.NodeColor = Color.Red;
                            LeftRotate(w);
                            w = x.Parent.Left;
                        }

                        w.NodeColor = x.Parent.NodeColor;
                        x.Parent.NodeColor = Color.Black;
                        w.Left.NodeColor = Color.Black;
                        RightRotate(x.Parent);
                        x = root;
                    }
                }
            }

            x.NodeColor = Color.Black;
        }

        private void Transplant(RBNode u, RBNode v)
        {
            if (u == null)
                return; // Adiciona verificação para evitar NullReferenceException

            if (u.Parent == null)
                root = v;
            else if (u == u.Parent.Left)
                u.Parent.Left = v;
            else
                u.Parent.Right = v;

            if (v != null) // Adiciona verificação para evitar NullReferenceException
                v.Parent = u.Parent;
        }

        public RBNode Search(int key)
        {
            return Search(root, key);
        }

        private RBNode Search(RBNode node, int key)
        {
            if (node == null || node == nil || key == node.Key)
                return node;

            if (key < node.Key)
                return Search(node.Left, key);

            return Search(node.Right, key);
        }

        public RBNode Minimum(RBNode node)
        {
            if (node == null || node == nil)
                return nil;

            while (node.Left != nil)
                node = node.Left;

            return node;
        }

        public void InOrderTraversal(RBNode node)
        {
            if (node != nil)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Key + "(" + node.NodeColor + ") ");
                InOrderTraversal(node.Right);
            }
        }

        public void PrintTree()
        {
            InOrderTraversal(root);
            Console.WriteLine();
        }
        public int SearchCount(int key)
        {
            return SearchCount(root, key);
        }

        private int SearchCount(RBNode node, int key)
        {
            if (node == null || node == nil)
                return 0;

            if (key < node.Key)
                return SearchCount(node.Left, key);
            else if (key > node.Key)
                return SearchCount(node.Right, key);
            else
            {
                // Encontrou o valor na árvore, agora conta quantas vezes ele aparece
                int count = 1;

                // Conta na subárvore esquerda
                count += SearchCount(node.Left, key);

                // Conta na subárvore direita
                count += SearchCount(node.Right, key);

                return count;
            }
        }
    }

}
