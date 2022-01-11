using System.Collections;

namespace ForExamAlgorithms
{
    public static class Sorting
    {
        // Сортировка пузырьком. Сложность O(n^2). Меняются соседние элементы
        public static void BubbleSort(int[] arr)
        {
            int temp;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j + 1] < arr[j])
                    {
                        temp = arr[j + 1];
                        arr[j + 1] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }
        // Сортировка вставками. Сложность O(n^2). Элемент протаскивается влево до своего места
        public static void InsertionSort(int[] arr)
        {
            for (var i = 1; i < arr.Length; i++)
            {
                var key = arr[i];
                var j = i;
                while (j > 1 && arr[j - 1] > key)
                {
                    Swap(ref arr[j - 1], ref arr[j]);
                    j--;
                }

                arr[j] = key;
            }

            void Swap(ref int e1, ref int e2)
            {
                var temp = e1;
                e1 = e2;
                e2 = temp;
            }
        }
        // Сортировка выбором. Сложность O(n^2). Наименьший элемент перемещается в отсортированную последовательность
        public static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < arr.Length; j++)
                    if (arr[j] < arr[min])
                        min = j;

                int temp = arr[i];
                arr[i] = arr[min];
                arr[min] = temp;
            }
        }
        // Шейкерная сортировка. Сложность O(n^2). Наибольшие и наименьшие элементы протаскиваются в конец и начало, границы сортировки сужаются
        public static void ShakerSort(int[] arr)
        {
            int left = 0,
                right = arr.Length - 1;

            while (left < right)
            {
                for (int i = left; i < right; i++)
                {
                    if (arr[i] > arr[i + 1])
                        Swap(ref arr[i], ref arr[i + 1]);
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    if (arr[i - 1] > arr[i])
                        Swap(ref arr[i - 1], ref arr[i]);
                }
                left++;
            }

            void Swap(ref int e1, ref int e2)
            {
                var temp = e1;
                e1 = e2;
                e2 = temp;
            }
        }
        // Сортировка Шелла. Сложность O(n^2). Элементы сортируются с уменьшающимся расстоянием между ними
        public static void ShellSort(int[] arr)
        {
            var d = arr.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < arr.Length; i++)
                {
                    var j = i;
                    while (j >= d && arr[j - d] > arr[j])
                    {
                        Swap(ref arr[j], ref arr[j - d]);
                        j -= d;
                    }
                }
                d /= 2;
            }

            void Swap(ref int a, ref int b)
            {
                var t = a;
                a = b;
                b = t;
            }
        }
        // Алгоритм бинарного поиска. O(log2 n). В два раза сужаем область поиска.
        public static int BinarySearch(int[] arr, int key, int leftEdge, int rightEdge)
        {
            int mid = (leftEdge + rightEdge) / 2;
            if (arr[mid] == key)
                return mid;
            return arr[mid] < key ? BinarySearch(arr, key, mid + 1, rightEdge) : BinarySearch(arr, key, leftEdge, mid - 1);
        }
        // Быстрая сортировка. O(n*log2 n). Массив разбивается на левый и правый от опорного элемента
        public static void QuickSort(int[] array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
                return;

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            int Partition(int[] array, int minIndex, int maxIndex)
            {
                var pivot = minIndex - 1;
                for (var i = minIndex; i <= maxIndex; i++)
                {
                    if (array[i] < array[maxIndex] || i == maxIndex)
                    {
                        pivot++;
                        Swap(ref array[pivot], ref array[i]);
                    }
                }
                return pivot;
            }
            void Swap(ref int x, ref int y)
            {
                var t = x;
                x = y;
                y = t;
            }
        }
        // Внешняя сортировка.
        public static void SortByFiles(string pathToSort)
        {
            int numOfLines = 0;
            using (var reader = new StreamReader(pathToSort))
                while (reader.ReadLine() != null)
                    numOfLines++;

            int runTo = (int)Math.Ceiling(Math.Log(numOfLines, 2));

            for (int i = 0; i < runTo; i++)
                TakeElements((int)Math.Pow(2, i));


            void TakeElements(int switching)
            {
                //Наполнение временных файлов
                #region
                var writerA = new StreamWriter("A.txt");
                var writerB = new StreamWriter("B.txt");

                int counter = 0;
                bool writeToA = true;
                using (var reader = new StreamReader(pathToSort))
                {
                    string? line = reader.ReadLine();
                    while (line != null)
                    {
                        if (counter % switching == 0)
                            writeToA = !writeToA;

                        if (writeToA)
                            writerA.WriteLine(line);
                        else
                            writerB.WriteLine(line);
                        counter++;
                        line = reader.ReadLine();
                    }
                }
                writerB.Dispose();
                writerA.Dispose();
                #endregion
                //Сбрасывание в файл
                File.Create(pathToSort).Dispose();
                using (var writer = new StreamWriter(pathToSort))
                {
                    var readerA = new StreamReader("A.txt");
                    var readerB = new StreamReader("B.txt");

                    while (!readerA.EndOfStream || !readerB.EndOfStream)
                    {
                        PopTo(readerA, readerB, switching);
                    }

                    readerA.Dispose();
                    readerB.Dispose();

                    void PopTo(StreamReader readerA, StreamReader readerB, int popFromFile)
                    {
                        int stepsA = 0, stepsB = 0;
                        string? a_line = readerA.ReadLine();
                        string? b_line = readerB.ReadLine();
                        while ((stepsA < popFromFile || stepsB < popFromFile) && (a_line != null || b_line != null))
                        {
                            int compare;
                            if (a_line == null) compare = 1;
                            else if (b_line == null) compare = -1;
                            else compare = int.Parse(a_line).CompareTo(int.Parse(b_line));

                            if (compare < 0 || compare == 0)
                            {
                                writer.WriteLine(a_line);
                                stepsA++;
                                a_line = stepsA >= popFromFile ? null : readerA.ReadLine();
                            }
                            if (compare > 0 || compare == 0)
                            {
                                writer.WriteLine(b_line);
                                stepsB++;
                                b_line = stepsB >= popFromFile ? null : readerB.ReadLine();
                            }
                        }
                    }
                }
            }
        }
        // Сортировка двоичным деревом. Сложность O(n*log2 n). Элементы располагаются деревом, слева < число, справа >= число
        private class TreeNode
        {
            public TreeNode(int num) => number = num;
            int number;
            TreeNode? left;
            TreeNode? right;
            internal void Insert(int num)
            {
                if (num < number)
                {
                    if (left == null)
                        left = new TreeNode(num);
                    else
                        left.Insert(num);
                }
                else
                {
                    if (right == null)
                        right = new TreeNode(num);
                    else
                        right.Insert(num);
                }
            }
            internal List<int> Parse(List<int> array)
            {
                if (left != null)
                    left.Parse(array);
                array.Add(number);
                if (right != null)
                    right.Parse(array);
                return array;
            }
        }
        public static void TreeSort(int[] arr)
        {
            var enumerator = arr.GetEnumerator();
            enumerator.MoveNext();
            var root = new TreeNode((int)enumerator.Current);
            while (enumerator.MoveNext())
                root.Insert((int)enumerator.Current);
            Array.Copy(root.Parse(new List<int>()).ToArray(), arr, arr.Length);
        }
        // ABC-сортировка.
        public static ICollection<string> ABSSort(ICollection<string> words, int rank = 0)
        {
            if (words.Count <= 1)
                return words;

            var square = new Dictionary<char, List<string>>(62);
            var result = new List<string>();
            int shortWordsCounter = 0;
            foreach (var word in words)
            {
                if (rank < word.Length)
                {
                    if (square.ContainsKey(word[rank]))
                        square[word[rank]].Add(word);
                    else
                        square.Add(word[rank], new List<string> { word });
                }
                else
                {
                    result.Add(word);
                    shortWordsCounter++;
                }
            }
            if (shortWordsCounter == words.Count)
                return words;

            for (char i = 'А'; i <= 'я'; i++)
            {
                if (square.ContainsKey(i))
                {
                    foreach (var word in ABSSort(square[i], rank + 1))
                        result.Add(word);
                }
            }
            return result;
        }
        // Radix sort. Сортировка чисел с помощью сдвига
        public static void RadixSort(int[] arr)
        {
            int i, j;
            int[] tmp = new int[arr.Length];
            for (int shift = sizeof(int) * 8 - 1; shift >= 0; shift--)
            {
                j = 0;
                for (i = 0; i < arr.Length; i++)
                {
                    bool move = (arr[i] << shift) >= 0;
                    if (shift == 0 ? !move : move)
                        arr[i - j] = arr[i];
                    else
                        tmp[j++] = arr[i];
                }
                Array.Copy(tmp, 0, arr, arr.Length - j, j);
            }
        }
    }


    // Ячейка для хэш-таблиц
    internal class Cell<TKey, TValue>
    {
        internal TValue value;
        internal TKey key;
        internal Cell(TKey key, TValue value)
        {
            this.value = value;
            this.key = key;
        }
        public override string ToString() => $"key {key}, value {value}";
    }
    // Хэш-таблица с разрешением коллизий методом цепочек
    public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly LinkedList<Cell<TKey, TValue>>[] buckets;
        public MyDictionary(int dictLenght = 200)
        {
            buckets = new LinkedList<Cell<TKey, TValue>>[dictLenght];
            for (int i = 0; i < buckets.Length; i++)
                buckets[i] = new LinkedList<Cell<TKey, TValue>>();
        }

        // Add, Remove pairs
        public void Add(TKey key, TValue value)
        {
            if (IsHaveValue(key)) throw new Exception("This key yet contains in dictionary");
            var cell = GetList(key);
            cell.AddFirst(new Cell<TKey, TValue>(key, value));
        }
        public void Remove(TKey key)
        {
            if (!IsHaveValue(key)) throw new Exception("This key not contains in dictionary");
            var cell = GetList(key);
            var pair = GetKeyValuePair(key);
            cell.Remove(pair);
        }

        // Get, Set value
        public void Set(TKey key, TValue value) => GetKeyValuePair(key).value = value;
        public TValue Get(TKey key) => GetKeyValuePair(key).value;
        public TValue this[TKey key]
        {
            get => Get(key);
            set => Set(key, value);
        }
        public bool IsHaveValue(TKey key)
        {
            var list = GetList(key);
            foreach (var cell in list)
                if (cell.key?.Equals(key) ?? false)
                    return true;
            return false;
        }

        // Find key value pairs
        private Cell<TKey, TValue> GetKeyValuePair(TKey key)
        {
            var list = GetList(key);
            foreach (var cell in list)
                if (cell.key?.Equals(key) ?? false)
                    return cell;
            // if key is not found
            throw new Exception("Key not found");
        }
        private LinkedList<Cell<TKey, TValue>> GetList(TKey key)
        {
            int hash = key?.GetHashCode() ?? throw new Exception("Key mustn't be null");
            int bucketNum = (hash & 0x7fffffff) % buckets.Length;
            return buckets[bucketNum];
        }

        // Interface
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in buckets)
                foreach (var pair in bucket)
                    yield return new KeyValuePair<TKey, TValue>(pair.key, pair.value);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    // Хэш-таблица с разрешением коллизий методом открытой адресации
    public class MyDictionary2<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private readonly Cell<TKey, TValue>[] buckets;
        public MyDictionary2(int dictLenght = 200) => buckets = new Cell<TKey, TValue>[dictLenght];

        // Add, Remove pairs
        public void Add(TKey key, TValue value)
        {
            if (IsHaveValue(key)) throw new Exception("This key yet contains in dictionary or hash-map overfilled");
            GetKeyValuePair(key, out int ind);
            buckets[ind] = new Cell<TKey, TValue>(key, value);
        }
        public void Remove(TKey key)
        {
            if (!IsHaveValue(key)) throw new Exception("This key not contains in dictionary");
            GetKeyValuePair(key, out int ind);
            buckets[ind] = null;
        }

        // Get, Set value
        public void Set(TKey key, TValue value)
        {
            GetKeyValuePair(key).value = IsHaveValue(key) ? value : throw new Exception("Key not found");
        }
        public TValue Get(TKey key)
        {
            return IsHaveValue(key) ? GetKeyValuePair(key).value : throw new Exception("Key not found");
        }

        public TValue this[TKey key]
        {
            get => Get(key);
            set => Set(key, value);
        }
        public bool IsHaveValue(TKey key)
        {
            try
            {
                return GetKeyValuePair(key) != null;
            }
            catch
            {
                return false;
            }
        }

        // Find key value pairs
        private Cell<TKey, TValue> GetKeyValuePair(TKey key, out int index)
        {
            int hash = key?.GetHashCode() ?? throw new Exception("Key mustn't be null");
            int bucketNum = (hash & 0x7fffffff) % buckets.Length;

            int offsetCounter = 0;
            while(offsetCounter < buckets.Length)
            {
                if (buckets[bucketNum]?.key?.Equals(key) ?? true)   // либо пустой, либо равный ключу
                {
                    index = bucketNum;
                    return buckets[bucketNum];
                }
                bucketNum = (bucketNum + 1) % buckets.Length;
                offsetCounter++;
            }
            throw new Exception("Key not found");
        }
        private Cell<TKey, TValue> GetKeyValuePair(TKey key) => GetKeyValuePair(key, out int _);
        // Interface
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var pair in buckets)
                yield return new KeyValuePair<TKey, TValue>(pair.key, pair.value);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }




    // Двусвязный список
    public class Sequence<T> : IEnumerable<T>
    {
        protected class Node<TValue>
        {
            internal Node<TValue> next, previous;
            internal TValue value;
            public Node(TValue value, Node<TValue> next = null, Node<TValue> previous = null)
            {
                this.value = value;
                this.next = next;
                this.previous = previous;
            }
        }
        protected Node<T> head, tail;
        public int Count { get; private set; }
        protected void AddLast(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
                head = node;
            else
            {
                tail.next = node;
                node.previous = tail;
            }
            tail = node;
            Count++;
        }
        protected void AddFirst(T data)
        {
            Node<T> node = new Node<T>(data);
            Node<T> temp = head;
            node.next = temp;
            head = node;
            if (Count == 0)
                tail = head;
            else
                temp.previous = node;
            Count++;
        }
        protected T PopFirst()
        {
            if (Count == 0) throw new Exception();
            T val = head.value;
            head = head.next;
            Count--;
            return val;
        }
        public void Clear()
        {
            head = tail = null;
            Count = 0;
        }
        public T this[int index]
        {
            get 
            {
                if (index >= Count) throw new IndexOutOfRangeException();
                int counter = 0;
                foreach(var node in this)
                {
                    if (counter == index)
                        return node;
                    counter++;
                }
                throw new Exception();
            }
            set 
            {
                if (index >= Count) throw new IndexOutOfRangeException();
                var current = head;
                for (int i = 1; i < index; i++)
                    current = current.next;
                current.value = value;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            var current = head;
            while(current != null)
            {
                yield return current.value;
                current = current.next;
            }  
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    // Стек на основе списка
    public class MyStack<T> : Sequence<T>
    {
        public void Push(T value) => AddFirst(value);
        public T Pop() => PopFirst();
        public T Peek() => this[0];
    }
    // Очередь на основе списка
    public class MyQueue<T> : Sequence<T>
    {
        public void Enqueue(T value) => AddLast(value);
        public T Dequeue() => PopFirst();
        public T Peek() => this[0];
    }



    // Класс вершины, которая образует граф без веса ребер
    public class Node
    {
        public List<Node> fromMe = new();    // Входящие и выходящие вершины
    }
    // Класс вершины, которая образует граф с весом ребер. Вершина знает сколько стоит дойти ребра, на которое она указывает
    public class WeightNode
    {
        public List<WeightNode> fromMe = new();
        readonly Dictionary<WeightNode, int> edgeWeights = new();
        public void SetWeight(WeightNode node, int newWeight) => edgeWeights[node] = newWeight;
        public int GetWeight(WeightNode node) => edgeWeights[node];
    }
    public static class Graph
    {
        // Обход в ширину с помощью очереди
        public static Node[] BreadFirstAlgorithm(Node start)
        {
            var visited = new List<Node> { start };
            var queue = new Queue<Node>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var neighbours = node.fromMe;
                foreach (var n in neighbours)
                    if (!visited.Contains(n))
                    {
                        queue.Enqueue(n);
                        visited.Add(n);
                    }
            }
            return visited.ToArray();
        }
        // Обход в глубину с помощью стека.
        public static Node[] DeapthFirstAlgorithm(Node start)
        {
            var visited = new List<Node> { start };
            var stack = new Stack<Node>();
            stack.Push(start);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                var neighbours = node.fromMe;
                foreach (var n in neighbours)
                    if (!visited.Contains(n))
                    {
                        stack.Push(n);
                        visited.Add(n);
                    }
            }
            return visited.ToArray();
        }
        // Дейкстра. 
        public static void AlgorithmDeikstra(WeightNode start, WeightNode end, out int minCost, out WeightNode[] minPath)
        {
            var minCostGetThere = new Dictionary<WeightNode, int>();
            var allChecked = new List<WeightNode>();
            minCostGetThere.Add(start, 0);
            RecSearch(start);
            minCost = minCostGetThere[end];
            minPath = null;
            
            void RecSearch(WeightNode current)
            {
                foreach (var next in current.fromMe)
                {
                    if (!allChecked.Contains(next))
                    {
                        if (!minCostGetThere.ContainsKey(next))
                            minCostGetThere.Add(next, minCostGetThere[current] + current.GetWeight(next));
                        else if (minCostGetThere[current] + current.GetWeight(next) < minCostGetThere[next])
                            minCostGetThere[next] = minCostGetThere[current] + current.GetWeight(next);
                    }
                }
                allChecked.Add(current);
                foreach (var next in current.fromMe.OrderBy(x => current.GetWeight(x)))
                {
                    if (!allChecked.Contains(next))
                        RecSearch(next);
                }
            }
        }
    }



    // Класс ребра, которое знает две соединяющиеся вершины и вес
    public class Edge
    {
        public int node1, node2, weight;
        public Edge(int node1, int node2, int weight)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.weight = weight;
        }
        public override string ToString() => $"{node1} {node2}, {weight}";
    }
    public static class TreeGraphMethods
    {
        // Алгоритм Краскала. Обходим все отсортированные по весу ребра, которые не образуют цикл, ребра записываем в образуемые множества. 
        public static List<Edge> Kruskals(ICollection<Edge> edges)
        {
            var nodes = new List<int>();                        // ищем все вершины
            foreach (var edge in edges)
            {
                if (!nodes.Contains(edge.node1)) nodes.Add(edge.node1);
                if (!nodes.Contains(edge.node2)) nodes.Add(edge.node2);
            }

            var sortedByWeight = edges.OrderBy(e => e.weight);
            var nodeSets = new Dictionary<int, int>();          // множества которые хранят номера вершин
            var resultEdges = new List<Edge>();
            foreach (var edge in sortedByWeight)
            {
                int node1 = edge.node1, node2 = edge.node2;
                if (nodeSets.ContainsKey(node1))
                {
                    if (nodeSets.ContainsKey(node2))
                    {
                        if (nodeSets[node1] == nodeSets[node2]) // две вершины из одного множества - будет цикл
                            continue;
                        else
                        {                                       // соединяем два множества в одно
                            var nodesOfSecondSet = nodeSets.Where(x => x.Value == nodeSets[node2]).Select(x => x.Key);
                            foreach (var node in nodesOfSecondSet)
                                nodeSets[node] = nodeSets[node1];
                        }
                    }
                    else
                    {
                        nodeSets.Add(node2, nodeSets[node1]);
                    }
                }
                else
                {
                    if (nodeSets.ContainsKey(node2))
                        nodeSets.Add(node1, nodeSets[node2]);
                    else
                    {
                        int newSet = 0;                         // ни в одном множестве нет этих вершин - создаем новое множество
                        while (nodeSets.ContainsValue(newSet))   // ищем уникальный номер для нового множества
                            newSet++;
                        nodeSets.Add(node1, newSet);
                        nodeSets.Add(node2, newSet);
                    }
                }
                resultEdges.Add(edge);                          // все проверки пройдены и ребро может быть добавлено в ответ

                var sets = nodeSets.GroupBy(x => x.Value);
                foreach (var set in sets)
                    if (set.Count() == nodes.Count)
                        return resultEdges;
            }
            throw new Exception();
        }
        // Алгоритм Эль Примо. Выбираем случайно начальную вершину, затем идем по наименьшему по весу ребру, которое не образует цикл, до того как не обойдем все ноды
        public static List<Edge> Prima(Edge[] edges)
        {
            var nodes = new List<int>();                        // ищем все вершины
            foreach (var edge in edges)
            {
                if (!nodes.Contains(edge.node1)) nodes.Add(edge.node1);
                if (!nodes.Contains(edge.node2)) nodes.Add(edge.node2);
            }

            List<int> visitedNodes = new();
            visitedNodes.Add(edges[0].node1);
            IOrderedEnumerable<Edge> toVisit = edges.Where(x => x.node1 == visitedNodes[0] || x.node2 == visitedNodes[0]).OrderBy(x => x.weight);
            List<Edge> resultEdges = new();
            while(visitedNodes.Count < nodes.Count)
            {
                if (toVisit.FirstOrDefault() == null) throw new Exception();
                Edge minEdge = toVisit.First();
                resultEdges.Add(minEdge);
                if (visitedNodes.Contains(minEdge.node1))
                    visitedNodes.Add(minEdge.node2);
                else
                    visitedNodes.Add(minEdge.node1);
                toVisit = edges.Where(x => visitedNodes.Contains(x.node1) ^ visitedNodes.Contains(x.node2)).OrderBy(x => x.weight);
            }
            return resultEdges;
        }
    }



}