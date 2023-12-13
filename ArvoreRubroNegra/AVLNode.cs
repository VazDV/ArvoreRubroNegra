namespace ArvoreRubroNegra
{
    public class AVLNode
    {
        public int Key, Height;
        public AVLNode Left, Right;

        public AVLNode(int key)
        {
            Key = key;
            Height = 1;
        }
    }
}
