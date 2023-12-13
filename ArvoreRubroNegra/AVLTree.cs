using System.Xml.Linq;

namespace ArvoreRubroNegra
{
    class AVLTree
    {
        private AVLNode root;
        private AVLNode nil;
        public AVLTree()
        {
            nil = new AVLNode(0);
            nil.Height = 0;
            nil.Left = nil.Right = null;

            root = null;
        }
        public int Height(AVLNode node)
        {
            if (node == null || node == nil)
                return 0;
            return node.Height;
        }

        public int BalanceFactor(AVLNode node)
        {
            if (node == null || node == nil)
                return 0;
            return Height(node.Left) - Height(node.Right);
        }

        public AVLNode RightRotate(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;
        }

        public AVLNode LeftRotate(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;
        }

        public AVLNode Insert(AVLNode node, int key)
        {
            if (node == null)
                return new AVLNode(key);

            if (key < node.Key)
                node.Left = Insert(node.Left, key);
            else if (key > node.Key)
                node.Right = Insert(node.Right, key);
            else
                return node; // Duplicates not allowed

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = BalanceFactor(node);

            // Left Left Case
            if (balance > 1 && key < node.Left.Key)
                return RightRotate(node);

            // Right Right Case
            if (balance < -1 && key > node.Right.Key)
                return LeftRotate(node);

            // Left Right Case
            if (balance > 1 && key > node.Left.Key)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right Left Case
            if (balance < -1 && key < node.Right.Key)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public void Insert(int key)
        {
            root = Insert(root, key);
        }

        public AVLNode MinValueNode(AVLNode node)
        {
            AVLNode current = node;

            while (current.Left != null)
                current = current.Left;

            return current;
        }

        public AVLNode Delete(AVLNode root, int key)
        {
            if (root == null)
                return root;

            if (key < root.Key)
                root.Left = Delete(root.Left, key);
            else if (key > root.Key)
                root.Right = Delete(root.Right, key);
            else
            {
                if ((root.Left == null) || (root.Right == null))
                {
                    AVLNode temp = null;
                    if (temp == root.Left)
                        temp = root.Right;
                    else
                        temp = root.Left;

                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else
                        root = temp;
                }
                else
                {
                    AVLNode temp = MinValueNode(root.Right);
                    root.Key = temp.Key;
                    root.Right = Delete(root.Right, temp.Key);
                }
            }

            if (root == null)
                return root;

            root.Height = 1 + Math.Max(Height(root.Left), Height(root.Right));

            int balance = BalanceFactor(root);

            // Left Left Case
            if (balance > 1 && BalanceFactor(root.Left) >= 0)
                return RightRotate(root);

            // Left Right Case
            if (balance > 1 && BalanceFactor(root.Left) < 0)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }

            // Right Right Case
            if (balance < -1 && BalanceFactor(root.Right) <= 0)
                return LeftRotate(root);

            // Right Left Case
            if (balance < -1 && BalanceFactor(root.Right) > 0)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            return root;
        }

        public void Delete(int key)
        {
            root = Delete(root, key);
        }

        public void InOrderTraversal(AVLNode node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Key + " ");
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

        private int SearchCount(AVLNode node, int key)
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
                if (node.Left != null)
                    count += SearchCount(node.Left, key);

                // Conta na subárvore direita
                if (node.Right != null)
                    count += SearchCount(node.Right, key);

                return count;
            }
        }
    }
}
