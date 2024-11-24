using System;

namespace lab1
{
    internal class Program
    {
        //Функция обмена переменных
        public static void Swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }

        //Функция обработки ввода целых чисел
        public static int IntInput()
        {
            int a;
            bool isConvert;
            string buf;
            do
            {
                Console.WriteLine("Введите целое число");
                buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out a);
                if (!isConvert)
                {
                    Console.WriteLine("Ошибка ввода. Повторите свой ввод.");
                }
            } while (!isConvert);
            return a;
        }

        //Функция генерации случайного массива
        public static void MakeRandomArray(int L, ref int[] arr)
        {
            Random rand = new Random();
            for (int i = 0; i < L; i++)
            {
                arr[i] = rand.Next(int.MinValue, int.MaxValue - 1);
            }
        }

        //Функция создания пользовательского массива
        public static void MakeUserArray(int L, ref int[] arr)
        {
            Console.WriteLine("Выбран ввод массива с клавиатуры");
            for (int i = 0; i < L; i++)
            {
                arr[i] = IntInput();
            }
        }

        //Функция вывода элементов массива
        public static void PrintArray(int[] arr)
        {
            if (arr.Length == 0) {
                Console.WriteLine("Массив пустой");
                return;
            }
            Console.WriteLine("Вывод массива:");
            foreach (int x in arr)
            {
                Console.Write($"{x} ");
            }
            Console.Write("\n");
        }

        //Функция удаления нечётных элементов из массива
        public static int[] PopOdd(int[] arr)
        {
            int cnt = 0;
            foreach (int x in arr)
            {
                if (x % 2 == 0)
                    cnt++;
            }
            int[] newArr = new int[cnt];
            int ind = 0;
            foreach (int x in arr)
            {
                if (x % 2 == 0)
                {
                    newArr[ind] = x;
                    ind++;
                }
            }
            return newArr;
        }

        //Функция добавления элементов в указанное место массива
        public static int[] AddElements(int L, int[] arr, int N, int[] addArr, int K)
        {
            int[] newArr = new int[L + N];
            for (int i = 0; i < K; i++)
            {
                newArr[i] = arr[i];
            }
            for (int i = 0; i < N; i++)
            {
                newArr[i + K] = addArr[i];
            }
            for (int i = K; i < L; i++)
            {
                newArr[i + N] = arr[i];
            }
            return newArr;
        }

        //Функция перестановки элементов массива указанным способом
        public static int[] ToPermute(int L, int[] arr)
        {
            int[] newArr = new int[L];

            int ind = 0;
            for (int i = 0; i < L; i++)
            {
                if (arr[i] > 0)
                {
                    newArr[ind] = arr[i];
                    ind++;
                }
            }
            ind = L - 1;
            for (int i = L - 1; i >= 0; i--)
            {
                if (arr[i] < 0)
                {
                    newArr[ind] = arr[i]; ;
                    ind--;
                }
            }

            return newArr;
        }

        //Функция линейного поиска элемента по его ключу
        public static int LineSearch(int L, int[] arr, int val)
        {
            for (int i = 0; i < L; i++)
            {
                if (arr[i] == val)
                {
                    Console.WriteLine($"Количество сравнений линейного поиска: {i + 1}");
                    return i + 1;
                }
            }
            return -1;
        }

        //Функция реализации сортировки методом пузырька
        public static void BubbleSort(int L, ref int[] arr)
        {
            for (int i = 0; i < L; i++)
            {
                for (int j = 0; j < L - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                        Swap(ref arr[j], ref arr[j + 1]);
                }
            }
        }

        //Функция реализации сортировки слиянием
        public static int[] MergeSort(int left, int right, int[] arr)
        {
            if (left == right)
            {
                int[] baseArr = { arr[left] };
                return baseArr;
            }
            int middle = (left + right) / 2;
            int[] leftArr = MergeSort(left, middle, arr);
            int[] rightArr = MergeSort(middle + 1, right, arr);

            int[] newArr = new int[right - left + 1];
            int leftPointer = 0, rightPointer = 0, ind = 0;
            while (leftPointer + rightPointer < right - left + 1)
            {
                if (rightPointer == rightArr.Length || leftPointer != leftArr.Length && leftArr[leftPointer] <= rightArr[rightPointer])
                {
                    newArr[ind] = leftArr[leftPointer];
                    ind++;
                    leftPointer++;
                }
                else
                {
                    newArr[ind] = rightArr[rightPointer];
                    ind++;
                    rightPointer++;
                }
            }
            return newArr;
        }

        //Функция выбора метода сортировки
        public static void Sorting(int L, ref int[] arr)
        {
            Console.WriteLine("Выберите способ сортировки:");
            Console.WriteLine("1) Пузырьковая сортировка (Bubble sort)");
            Console.WriteLine("2) Сортировка слиянием (Merge sort)");

            int choice;

            do
            {
                choice = IntInput();
                if (choice > 2 || choice < 1)
                {
                    Console.WriteLine("Такого номера в меню нет. Введите 1 или 2");
                }
            } while (choice > 2 || choice < 1);

            switch (choice)
            {
                case 1:
                    BubbleSort(L, ref arr);
                    Console.WriteLine("Массив отсортирован сортировкой пузырьком!");
                    break;
                case 2:
                    arr = MergeSort(0, L - 1, arr);
                    Console.WriteLine("Массив отсортирован сортировкой слиянием!");
                    break;
            }
        }

        //Функция проверки массива на отсортированность в неубывающем порядке
        public static bool IsSorted(int L, int[] arr)
        {
            for (int i = 0; i < L - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                    return false;
            }
            return true;
        }

        //Функция бинарного поиска элемента по его ключу в отсортированном массиве
        public static int BinarySearch(int L, int[] arr, int val)
        {
            int leftPointer = -1, rightPointer = L, middlePointer;
            int steps = 0;
            while (leftPointer + 1 < rightPointer)
            {
                steps++;
                middlePointer = (leftPointer + rightPointer) / 2;
                if (arr[middlePointer] >= val)
                    rightPointer = middlePointer;
                else
                    leftPointer = middlePointer;
            }
            Console.WriteLine($"Количество сравнений бинарного поиска: {steps}");
            if (arr[rightPointer] == val)
                return rightPointer + 1;
            else
                return -1;
        }

        static void Main(string[] args)
        {
            //Приветствие пользователя
            Console.WriteLine("Привет! Это программа для работы с массивом.");
            Console.WriteLine("Для начала нам нужно массив создать.");

            //Ввод пользователем длины массива
            int L;

            do {
                Console.WriteLine("Введите натуральное число - длину массива.");
                L = IntInput();
                if (L < 0) {
                    Console.WriteLine("Длина массива не может быть отрицательной. Повторите свой ввод.");
                }
            } while (L < 0);

            //Создаём пустой массив указанного размера
            int[] arr = new int[L];

            //Выбор способа формирования массива
            Console.WriteLine("Отлично! Теперь вы можете выбрать способ формирования массива.");
            Console.WriteLine("1) Автоматическое заполнение случайными числами");
            Console.WriteLine("2) Ручной ввод элементов с клавиатуры в консоль");
            Console.WriteLine("Для выбора нужного вам способа введите соответствующий ему номер:");

            int choice;

            do
            {
                choice = IntInput();
                if (choice > 2 || choice < 1)
                {
                    Console.WriteLine("Такого номера в меню нет. Введите 1 или 2");
                }
            } while (choice > 2 || choice < 1);

            //Формирование массива указанным способом
            switch (choice)
            {
                case 1:
                    MakeRandomArray(L, ref arr);
                    break;
                case 2:
                    MakeUserArray(L, ref arr);
                    break;
            }

            int val, index;

            Console.WriteLine("Поздравляем, массив успешно создан!");
            Console.WriteLine("Теперь вы можете совершать над ним действия, описанные в меню.\n");

            //Главное меню действий
            do
            {
                Console.WriteLine("\nМеню выбора действий:");
                Console.WriteLine("0) Завершить программу");
                Console.WriteLine("1) Вывести массив на экран");
                Console.WriteLine("2) Удалить нечётные элементы");
                Console.WriteLine("3) Добавить элементы в массив");
                Console.WriteLine("4) Переставить положительные элементы в начало, а отрицательные - в конец");
                Console.WriteLine("5) Линейный поиск элемента по ключу");
                Console.WriteLine("6) Отсортировать массив");
                Console.WriteLine("7) Бинарный поиск элементов по ключу (ВАЖНО! Массив должен быть отсортирован!)");

                //Выбор действия
                do
                {
                    choice = IntInput();
                    if (choice > 7 || choice < 0)
                    {
                        Console.WriteLine("Такого номера в меню нет. Введите число от 0 до 7");
                    }
                } while (choice > 7 || choice < 0);

                switch (choice)
                {
                    case 1:
                        PrintArray(arr); // вывод массива в консоль
                        break;
                    case 2:
                        arr = PopOdd(arr); // удаление нечётных элементов
                        L = arr.Length;
                        Console.WriteLine("Нечётные элементы удалены");
                        break;
                    case 3:
                        int N, K;

                        // Ввод количества добавляемых элементов
                        do
                        {
                            Console.WriteLine("Введите натуральное число - количеcтво добавляемых элементов.");
                            N = IntInput();
                            if (N < 0)
                            {
                                Console.WriteLine("Количеcтво добавляемых элементов не может быть отрицательным. Повторите свой ввод.");
                            }
                            else if (N == 0)
                            {
                                Console.WriteLine("Не добавлять ни одного элемента - это скучно. Повторите свой ввод.");
                            }
                        } while (N <= 0);

                        Console.WriteLine("Отлично! Теперь нам нужно узнать после какого элемента нам нужно вставить наши элементы.");

                        // Ввод номера опорного элемента для вставки
                        do
                        {
                            Console.WriteLine("Введите натуральное число - номер элемента массива");
                            K = IntInput();
                            if (K < 0 || K > L)
                            {
                                Console.WriteLine($"Элемента с заданным номером не существует. Введите число от 0 до {L}");
                            }
                        } while (K < 0 || K > L);

                        Console.WriteLine("Прекрасно! Теперь введите добавляемые числа.");

                        // Формирование массива добавляемых чисел
                        int[] addArr = new int[N];

                        for (int i = 0; i < N; i++)
                        {
                            addArr[i] = IntInput();
                        }

                        arr = AddElements(L, arr, N, addArr, K); // добавление элементов в массив
                        L = arr.Length;

                        Console.WriteLine("Элементы успешно добавлены в массив!");
                        break;
                    case 4:
                        arr = ToPermute(L, arr); // перестановка элементов массива
                        Console.WriteLine("Перестановка успешно совершена!");
                        break;
                    case 5:
                        Console.WriteLine("Для поиска элемента введите искомое значение");
                        val = IntInput();
                        index = LineSearch(L, arr, val); // линейный поиск элемента

                        if (index == -1) // если index = -1, значит искомого элемента нет
                        {
                            Console.WriteLine("Элемента с таким ключом в массиве нет!");
                        }
                        else
                        {
                            Console.WriteLine($"Номер первого элемента с данным ключом: {index}");
                        }
                        break;
                    case 6:
                        Sorting(L, ref arr); // сортировка массива
                        break;
                    case 7:
                        // Предварительная проверка отсортированности массива
                        if (!IsSorted(L, arr))
                        {
                            Console.WriteLine("Ваш массив не отсотирован! Бинарный поиск невозможен!");
                            Console.WriteLine("Пожалуйста, сначала отсортируйте массив, а затем повторите свой запрос.");
                            break;
                        }

                        Console.WriteLine("Для поиска элемента введите искомое значение");
                        val = IntInput();
                        index = BinarySearch(L, arr, val); // бинарный поиск элемента
                        if (index == -1) // если index = -1, значит искомого элемента нет
                        {
                            Console.WriteLine("Элемента с таким ключом в массиве нет!");
                        }
                        else
                        {
                            Console.WriteLine($"Номер первого элемента с данным ключом: {index}");
                        }
                        break;
                }
            } while (choice != 0);

            // Завершение работы
            Console.WriteLine("Завершение работы");
            Console.WriteLine("Спасибо, что воспользовались этой программой!");
        }
    }
}