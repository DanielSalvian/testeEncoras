using System;
using System.Collections.Generic;

public class InfiniteCoins
{
    /**
     * A função abaixo calcula todas as combinações possíveis de moedas (quarters, dimes, nickels e pennies)
     * que somam o valor n em centavos.
     * - A função começa com a chamada para 'MakeChangeRecursive', que é recursiva e tenta todas as combinações
     *   de moedas possíveis.
     * - Quando uma combinação válida é encontrada (que soma o valor n), ela é adicionada ao conjunto.
     * - O conjunto é retornado no final, contendo todas as combinações únicas.
     */
    public static HashSet<int[]> MakeChange(int n)
    {
        // Cria um HashSet para armazenar as combinações únicas
        HashSet<int[]> result = new HashSet<int[]>(new IntArrayComparer());

        // Chama a função recursiva para explorar todas as combinações possíveis
        MakeChangeRecursive(n, 0, new int[4], result);

        return result;
    }

    /**
     * Função recursiva para gerar todas as combinações possíveis de moedas
     */
    private static void MakeChangeRecursive(int amountLeft, int coinIndex, int[] currentCombination, HashSet<int[]> result)
    {
        // Caso base: se o valor restante for 0, adicionamos a combinação ao resultado
        if (amountLeft == 0)
        {
            result.Add((int[])currentCombination.Clone());
            return;
        }

        // Caso recursivo: tentamos usar diferentes quantidades de moedas a partir do índice coinIndex
        if (coinIndex < 4)
        {
            // Calcula o número máximo de moedas do tipo 'coinIndex' que podem ser usadas
            int maxCoins = amountLeft / GetCoinValue(coinIndex);

            // Tentamos de 0 até maxCoins moedas do tipo coinIndex
            for (int count = 0; count <= maxCoins; count++)
            {
                currentCombination[coinIndex] = count;
                MakeChangeRecursive(amountLeft - count * GetCoinValue(coinIndex), coinIndex + 1, currentCombination, result);
            }

            // Limpa o valor atual para explorar outras opções
            currentCombination[coinIndex] = 0;
        }
    }

    // Função para obter o valor de uma moeda baseado no índice
    private static int GetCoinValue(int index)
    {
        switch (index)
        {
            case 0: return 25; // quarter
            case 1: return 10; // dime
            case 2: return 5;  // nickel
            case 3: return 1;  // penny
            default: throw new ArgumentException("Índice de moeda inválido");
        }
    }

    /**
     * Comparador para garantir que o HashSet armazene combinações únicas de int[].
     * O método Equals e GetHashCode são usados para garantir que as combinações sejam tratadas como únicas
     * baseado no conteúdo dos arrays (e não no endereço de memória).
     */
    public class IntArrayComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x == null || y == null)
                return false;

            // Verifica se ambos os arrays têm os mesmos valores em cada posição
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                    return false;
            }
            return true;
        }

        public int GetHashCode(int[] obj)
        {
            // Gera um código hash com base nos valores do array
            int hash = 17;
            foreach (int value in obj)
            {
                hash = hash * 23 + value;
            }
            return hash;
        }
    }

    // Exemplo de uso
    public static void Main()
    {
        int n = 12;
        HashSet<int[]> combinations = MakeChange(n);
        
        // Imprime o número total de maneiras de representar n centavos
        Console.WriteLine($"Número total de maneiras de representar {n} centavos: {combinations.Count}");
        
        // Imprime cada combinação no formato desejado
        foreach (var combination in combinations)
        {
            Console.WriteLine($"[{combination[0]}, {combination[1]}, {combination[2]}, {combination[3]}]");
        }
    }
}
