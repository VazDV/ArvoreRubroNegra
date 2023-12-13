using ArvoreRubroNegra;
using System.Diagnostics;
class Program
{
    static void Main()
    {
        AVLTree avlTree = new AVLTree();
        RBTree redBlackTree = new RBTree();
        Stopwatch avlStopwatch = new Stopwatch();
        Stopwatch redBlackStopwatch = new Stopwatch();
        Stopwatch avlStopwatchPRINT = new Stopwatch();
        Stopwatch redBlackStopwatchPRINT = new Stopwatch();

        // Caminho para o arquivo contendo os números
        string filePath = "D:\\ArvoreRubroNegra\\ArvoreRubroNegra\\dados100_mil.txt";

        try
        {
            // Lê os números do arquivo
            string[] lines = File.ReadAllLines(filePath);

            // Preenchimento da AVL Tree
            avlStopwatch.Start();
            foreach (string line in lines)
            {
                string[] numbers = line.Trim('[', ']').Split(',');
                foreach (string number in numbers)
                {
                    int num = int.Parse(number.Trim());
                    avlTree.Insert(num);
                }
            }
            avlStopwatch.Stop();

            // Preenchimento da Red-Black Tree
            redBlackStopwatch.Start();
            foreach (string line in lines)
            {
                string[] numbers = line.Trim('[', ']').Split(',');
                foreach (string number in numbers)
                {
                    int num = int.Parse(number.Trim());
                    redBlackTree.Insert(num);
                }
            }
            redBlackStopwatch.Stop();

            // Exibe as árvores após a inserção inicial
            Console.WriteLine("AVL Tree após inserção inicial:");

            avlStopwatchPRINT.Start();
            avlTree.PrintTree();
            avlStopwatchPRINT.Stop();

            Console.WriteLine("\nRed-Black Tree após inserção inicial:");
            redBlackStopwatchPRINT.Start();
            redBlackTree.PrintTree();
            redBlackStopwatchPRINT.Stop();

            // Sorteio aleatório de outros 50.000 números entre -9999 e 9999
            Random random = new Random();
            for (int i = 0; i < 50000; i++)
            {
                int randomNumber = random.Next(-9999, 10000);

                if (randomNumber % 3 == 0)
                {
                    // Caso o número sorteado seja múltiplo de 3, inserir na árvore
                    avlTree.Insert(randomNumber);
                    redBlackTree.Insert(randomNumber);
                }
                else if (randomNumber % 5 == 0)
                {
                    // Caso o número sorteado seja múltiplo de 5, remover da árvore
                    avlTree.Delete(randomNumber);
                    redBlackTree.Delete(randomNumber);
                }
                else
                {
                    // Caso não seja nem múltiplo de 3 nem de 5, contar quantas vezes aparece na árvore
                    int countInAVL = avlTree.SearchCount(randomNumber);

                    int countInRedBlack = redBlackTree.SearchCount(randomNumber);

                    Console.WriteLine($"Número: {randomNumber}, AVL Tree: {countInAVL}, Red-Black Tree: {countInRedBlack}");
                }
            }

            // Imprime o tempo de execução
            Console.WriteLine($"\n\n\nTempo para preencher AVL Tree: {avlStopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Tempo para imprimir AVL Tree: {avlStopwatchPRINT.ElapsedMilliseconds} ms");
            Console.WriteLine($"\n\nTempo para preencher Red-Black Tree: {redBlackStopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Tempo para imprimir Red-Black Tree: {redBlackStopwatchPRINT.ElapsedMilliseconds} ms");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }
    }
}
