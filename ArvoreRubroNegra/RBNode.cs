namespace ArvoreRubroNegra
{
    public enum Color
    {
        Red,
        Black
    }
    public class RBNode
    {
        public int Key;
        public RBNode Parent, Left, Right;
        public Color NodeColor;

        public RBNode(int key)
        {
            Key = key;
            NodeColor = Color.Red; // Por padrão, os nós são vermelhos ao serem inseridos
        }
    }
}
