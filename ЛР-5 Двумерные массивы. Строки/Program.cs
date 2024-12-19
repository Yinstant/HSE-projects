using System;
using System.Runtime.Intrinsics.Arm;

namespace lab1
{
    internal class Program
    {
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

        public static int[] ArrayInput(int L = 1) {
            int a;
            bool isConvert = false;
            string buf;
            string[] strArr;
            int[] arr = new int[L];

            do
            {
                buf = Console.ReadLine();
                strArr = buf.Split();
                if (strArr.Length != L) {
                    Console.WriteLine($"Неправильное количество введённых чисел. Введите ещё раз {L} чисел через пробел.");
                    continue;
                }
                for (int i = 0; i < L; i++) { 
                    isConvert = int.TryParse(strArr[i], out arr[i]);
                    if (!isConvert) {
                        Console.WriteLine("Введено не целое число в диапазоне типа int. Введите элементы строки ещё раз.");
                        break;
                    }
                }
            } while (strArr.Length != L || !isConvert);

            return arr;
        }

        public static void MakeUserMatrix(ref int[,] matrix) {
            
            for (int i = 0; i < matrix.GetLength(0); i++) {
                Console.WriteLine($"Введите {matrix.GetLength(1)} элементов строки матрицы через пробел:");
                int[] strMatrix = ArrayInput(matrix.GetLength(1));
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = strMatrix[j];
                }
            }
        }

        public static void MakeRandomMatrix(ref int[,] matrix)
        {
            Random rand = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rand.Next(int.MinValue, int.MaxValue - 1);
                }
            }
        }

        public static int[,] MakeMatrix() {

            const int MAXMEMORY = 2147483591;

            int L1, W1;

            Console.WriteLine("Введите неотрицательное число - количество строк в массве:");
            do
            {
                L1 = IntInput();
                if (L1 < 0)
                    Console.WriteLine("Ошибка ввода! Необходимо ввести неотрицательное число. Повторите попытку.");
                else if (L1 > MAXMEMORY) {
                    Console.WriteLine($"Матрица с таким количеством строк будет слишком большой. Введите число от 1 до {MAXMEMORY}. Повторите ввод.");
                }
            } while (L1 < 0 || L1 > MAXMEMORY);

            Console.WriteLine("Введите положиельное число - количество столбцов в массве:");
            do
            {
                W1 = IntInput();
                if (W1 < 0)
                    Console.WriteLine("Ошибка ввода! Необходимо ввести неотрицательное число. Повторите попытку.");
                else if (W1 == 0)
                    Console.WriteLine("Матрица с нуля столбцами не имеет смысла. Повторите ввод.");
                else if (W1 > MAXMEMORY / L1) {
                    Console.WriteLine($"Матрица с таким количеством столбцов будет слишком большой. Введите число от 1 до {(int)(MAXMEMORY / L1)}. Повторите ввод.");
                }
            } while (W1 <= 0 || W1 > MAXMEMORY / L1);

            int[,] matrix = new int[L1, W1];

            int formatChoose;

            Console.WriteLine("Выберите формат ввода:");
            Console.WriteLine("1) Ввод элементов в консоль");
            Console.WriteLine("2) Генерация случайных чисел");

            do {
                formatChoose = IntInput();
                if (formatChoose < 1 || formatChoose > 2) {
                    Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 2.");
                }
            } while (formatChoose < 1 || formatChoose > 2);

            if (formatChoose == 1)
            {
                MakeUserMatrix(ref matrix);
                Console.WriteLine("Массив был сформирован путём ввода элементов в консоль!\n");
            }
            else
            {
                MakeRandomMatrix(ref matrix);
                Console.WriteLine("Массив был сформирован путём генерации случайных чисел!\n");
            }
            
            return matrix;
        }


        public static void AddStrings(ref int[,] matrix) {

            const int MAXMEMORY = 2147483591;

            int K;

            Console.WriteLine("Введите целое положительное число - количество добавляемых строк:");
            do
            {
                K = IntInput();
                if (K < 0)
                    Console.WriteLine("Ошибка ввода! Необходимо ввести неотрицательное число. Повторите попытку.");
                else if (K == 0)
                    Console.WriteLine("Не добавлять ни одной строки не имеет смысла. Посторите ввод.");
                else if (K > (MAXMEMORY - matrix.Length) / matrix.GetLength(1))
                {
                    Console.WriteLine($"Матрица с таким количеством столбцов будет слишком большой. Введите число от 1 до {(MAXMEMORY - matrix.Length) / matrix.GetLength(1)}. Повторите ввод.");
                }
            } while (K <= 0 || K > (MAXMEMORY - matrix.Length) / matrix.GetLength(1));

            int[,] newMatrix = new int[matrix.GetLength(0) + K, matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            int[,] addMatrix = new int[K, matrix.GetLength(1)];

            int formatChoose;

            Console.WriteLine("Выберите формат ввода:");
            Console.WriteLine("1) Ввод элементов в консоль");
            Console.WriteLine("2) Генерация случайных чисел");

            do
            {
                formatChoose = IntInput();
                if (formatChoose < 1 || formatChoose > 2)
                {
                    Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 2.");
                }
            } while (formatChoose < 1 || formatChoose > 2);

            if (formatChoose == 1)
                MakeUserMatrix(ref addMatrix);
            else
                MakeRandomMatrix(ref addMatrix);

            for (int i = 0; i < K; i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i + matrix.GetLength(0), j] = addMatrix[i, j];
                }
            }

            matrix = newMatrix;

            Console.WriteLine($"В двумерный массив добавлены {K} строк!\n");
            return;
        }

        public static void Print(int[,] matrix) {
            if (matrix.GetLength(0) == 0) {
                Console.WriteLine("Двумерный массив - пустой.\n");
                return;
            }
            
            Console.WriteLine("Вывод двумерного массива:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }
                Console.Write("\n");
            }
            return;
        }

        public static void MakeUserRaggedArray(ref int[][] raggedArray)
        {
            const int MAXMEMORY = 2147483591;

            int W2;
            
            for (int i = 0; i < raggedArray.GetLength(0); i++)
            {
                Console.WriteLine($"Введите целое положительное число - количество столбцов строки {i + 1}:");
                do
                {
                    W2 = IntInput();
                    if (W2 < 0)
                        Console.WriteLine("Ошибка ввода! Необходимо ввести неотрицательное число. Повторите попытку.");
                    else if (W2 == 0)
                        Console.WriteLine("Рваный массив с нулём столбцов не имеет смысла. Посторите ввод.");
                    else if (raggedArray.Length > MAXMEMORY - W2)
                    {
                        Console.WriteLine($"Столбцов у строки слишком много. Введите число от 1 до {MAXMEMORY - raggedArray.Length}. Повторите ввод.");
                    }
                } while (W2 <= 0 || raggedArray.Length > MAXMEMORY - W2);

                Console.WriteLine($"Введите {W2} элементов строки двумерного массива через пробел:");
                int[] strMatrix = ArrayInput(W2);
                raggedArray[i] = new int[W2];
                for (int j = 0; j < W2; j++)
                {
                    raggedArray[i][j] = strMatrix[j];
                }
            }
        }

        public static void MakeRandomRaggedArray(ref int[][] raggedArray)
        {
            const int MAXMEMORY = 2147483591;

            int W2;
            Random rand = new Random();
            for (int i = 0; i < raggedArray.GetLength(0); i++)
            {

                Console.WriteLine($"Введите целое положительное число - количество столбцов строки {i + 1}:");
                do
                {
                    W2 = IntInput();
                    if (W2 < 0)
                        Console.WriteLine("Ошибка ввода! Необходимо ввести неотрицательное число. Повторите попытку.");
                    else if (W2 == 0)
                        Console.WriteLine("Рваный массив с нулём столбцов не имеет смысла. Посторите ввод.");
                    else if (raggedArray.Length > MAXMEMORY - W2)
                    {
                        Console.WriteLine($"Столбцов у строки слишком много. Введите число от 1 до {MAXMEMORY - raggedArray.Length}. Повторите ввод.");
                    }
                } while (W2 <= 0 || raggedArray.Length > MAXMEMORY - W2);

                raggedArray[i] = new int[W2];

                for (int j = 0; j < W2; j++)
                {
                    raggedArray[i][j] = rand.Next(int.MinValue, int.MaxValue - 1);
                }
            }
        }

        public static int[][] MakeRaggedArray() {
            const int MAXMEMORY = 2147483591;

            int L2;

            Console.WriteLine("Введите целое положительное число - количество строк в массве:");
            do
            {
                L2 = IntInput();
                if (L2 < 0)
                    Console.WriteLine("Ошибка ввода! Необходимо ввести неотрицательное число. Повторите попытку.");
                else if (L2 == 0)
                    Console.WriteLine("Рваный массив с нулём строк не имеет смысла. Посторите ввод.");
                else if (L2 > MAXMEMORY)
                {
                    Console.WriteLine($"Рваный массив с таким количеством строк будет слишком большой. Введите число от 1 до {MAXMEMORY}. Повторите ввод.");
                }
            } while (L2 <= 0 || L2 > MAXMEMORY);

            int[][] raggedArray = new int[L2][];

            int formatChoose;

            Console.WriteLine("Выберите формат ввода:");
            Console.WriteLine("1) Ввод элементов в консоль");
            Console.WriteLine("2) Генерация случайных чисел");

            do
            {
                formatChoose = IntInput();
                if (formatChoose < 1 || formatChoose > 2)
                {
                    Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 2.");
                }
            } while (formatChoose < 1 || formatChoose > 2);

            if (formatChoose == 1)
                MakeUserRaggedArray(ref raggedArray);
            else
                MakeRandomRaggedArray(ref raggedArray);

            Console.WriteLine("Рваный массив был успешно сформирован!\n");
            return raggedArray;
        }

        public static void PopString(ref int[][] raggedArr) {
            int num = -1;
            for (int i = 0; i < raggedArr.GetLength(0); i++) {
                foreach (int x in raggedArr[i]) {
                    if (x == 0) {
                        num = i;
                        break;
                    }
                }
                if (num != -1)
                    break;
            }
            if (num == -1) {
                Console.WriteLine("В массиве нет нулей. Ни одна строка не удалена!\n");
                return;
            }

            int[][] newRaggedArr = new int[raggedArr.GetLength(0) - 1][];

            for (int i = 0; i < num; i++) {
                newRaggedArr[i] = new int[raggedArr[i].Length];
                for (int j = 0; j < raggedArr[i].Length; j++) {
                    newRaggedArr[i][j] = raggedArr[i][j];
                }
            }

            for (int i = num + 1; i < raggedArr.GetLength(0); i++)
            {
                newRaggedArr[i - 1] = new int[raggedArr[i].Length];
                for (int j = 0; j < raggedArr[i].Length; j++)
                {
                    newRaggedArr[i - 1][j] = raggedArr[i][j];
                }
            }

            raggedArr = newRaggedArr;

            Console.WriteLine($"Была успешно удалена первая строка, содержащая ноль, с номером {num + 1}\n");
            return;
        }
        
        public static void Print(int[][] raggedArr) {
            Console.WriteLine("Вывод рваного массива:");
            for (int i = 0; i < raggedArr.GetLength(0); i++)
            {
                for (int j = 0; j < raggedArr[i].Length; j++)
                {
                    Console.Write($"{raggedArr[i][j]} ");
                }
                Console.Write("\n");
            }
            return;
        }


        public static bool IsIn(string mainStr, string chars) {
            for (int i = 0; i < mainStr.Length; i++) {
                for (int j = 0; j < chars.Length; j++) {
                    if (mainStr[i] == chars[j])
                        return true;
                }
            }
            return false;
        }

        public static bool IsIn(char mainChar, string chars)
        {
            for (int j = 0; j < chars.Length; j++)
            {
                if (mainChar == chars[j])
                    return true;
            }
            return false;
        }

        public static bool IsNeighboringMarks(string[] separator)
        {
            for (int i = 0; i < separator.Length; i++)
            {
                int countMarks = 0;
                for (int j = 0; j < separator[i].Length; j++) {
                    if (IsIn(separator[i][j], ".?!,:;"))
                        countMarks++;
                }
                if (countMarks > 1)
                    return true;
            }
            return false;
        }

        public static void SplitToWords(string text, ref string[] words, ref string[] separator)
        {
            for (int i = 0; i < text.Length; i++) {
                if (IsIn(text[i], ".?!,:; "))
                {
                    if (i == 0 || !IsIn(text[i - 1], ".?!,:; "))
                    {
                        Array.Resize(ref separator, separator.Length + 1);
                    }
                    separator[separator.Length - 1] = separator[separator.Length - 1] + text[i];
                }
                else {
                    if (i == 0) {
                        Array.Resize(ref separator, 1);
                        separator[0] = "";
                    }
                    if (i == 0 || IsIn(text[i - 1], ".?!,:; "))
                    {
                        Array.Resize(ref words, words.Length + 1);
                    }
                    words[words.Length - 1] = words[words.Length - 1] + text[i];
                }
            }

            if (words.Length + 1 != separator.Length) {
                Array.Resize(ref separator, separator.Length + 1);
                separator[separator.Length - 1] = "";
            }
        }

        public static void MakeUserString(ref string text, ref string[] words, ref string[] separator)
        {
            do {
                Array.Resize(ref separator, 0);
                Array.Resize(ref words, 0);

                Console.WriteLine("Введите строку символов в консоль:");
                text = Console.ReadLine();
                SplitToWords(text, ref words, ref separator);

                /*Console.WriteLine($"Длина words: {words.Length}");
                foreach (string str in words) {
                    Console.WriteLine(str + " ");
                }
                Console.WriteLine();

                Console.WriteLine($"Длина separator: {separator.Length}");
                foreach (string str in separator)
                {
                    Console.WriteLine(str + " ");
                }
                Console.WriteLine();*/

                if (words.Length == 0)
                {
                    Console.WriteLine("Строка должна содержать хотя бы одно слово!");
                    Console.WriteLine("Повторите ввод.");
                }
                else if (IsIn(separator[0], ".?!,:;"))
                {
                    Console.WriteLine("Перед первым словом не должно быть знака препинания!");
                    Console.WriteLine("Повторите ввод.");
                }
                else if (!IsIn(separator[separator.Length - 1], ".?!"))
                {
                    Console.WriteLine("В конце должна стоять точка, воклицательный знак или вопросительный знак!");
                    Console.WriteLine("Повторите ввод.");
                }
                else if (IsNeighboringMarks(separator)) {
                    Console.WriteLine("Два знака препинания должны быть разделены словами!");
                    Console.WriteLine("Повторите ввод.");
                }
            } while (IsIn(separator[0], ".?!,:;") || !IsIn(separator[separator.Length - 1], ".?!") || words.Length == 0 || IsNeighboringMarks(separator));
        }

        public static void MakeTestString(ref string text, ref string[] words, ref string[] separator)
        {
            string[] testStrings = {
                "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.",
                "static void PrintUpper stringinfo12346: WriteLine ToUpper info, 1234info.",
                "static void PrintUpper stringinfo12346: WriteLine ToUpper info, 1234info.if x>0 then sign=1; else if x<0 sign=-1; else sign=0.",
                "В лесу родилась елочка! В лесу она росла. Зимой и летом стройная, зеленая была!",
                "В лесу родилась елка! В лесу она росла. Зимой и летом была стройная, зеленая!",
                "В траве сидел кузнечик! Кузнечик не трогал козявок и дружил с мухом.",
                "Последняя тестовая строка. Просто, чтобы было.",
            };

            Random rand = new Random();

            int indexTest = rand.Next(0, testStrings.Length);

            text = testStrings[indexTest];
            SplitToWords(text, ref words, ref separator);
        }

        public static void MakeString(ref string text, ref string[] words, ref string[] separator) {
            int formatChoose;

            Console.WriteLine("Выберите формат ввода строки:");
            Console.WriteLine("1) Ввод строки в консоль");
            Console.WriteLine("2) Случайная тестовая строка из заранее сформированного массива строк");

            do
            {
                formatChoose = IntInput();
                if (formatChoose < 1 || formatChoose > 2)
                {
                    Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 2.");
                }
            } while (formatChoose < 1 || formatChoose > 2);

            if (formatChoose == 1)
            {
                MakeUserString(ref text, ref words, ref separator);
                Console.WriteLine("Вводимая строка прошла проверку и была успешно сформирована!");
            }
            else {
                MakeTestString(ref text, ref words, ref separator);
                Console.WriteLine("Строка была случайно выбрана из тестовых вариантов!");
            }
        }

        public static void CyclicShift(ref string str, ref string[] words, ref string[] separator) {
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Substring((i + 1) % words[i].Length, words[i].Length - (i + 1) % words[i].Length) + words[i].Substring(0, (i + 1) % words[i].Length);
            }
            str = "";
            for (int i = 0; i < words.Length; i++) {
                str += separator[i];
                str += words[i];
            }
            str += separator[separator.Length - 1];

            Console.WriteLine("Циклический сдвиг слов произведён!\n");
        }

        public static void Print(string str) {
            Console.WriteLine("Вывод строки символов:");
            Console.WriteLine(str);
            Console.WriteLine();
            return;
        }
        static void Main(string[] args)
        {
            const int MAXMEMORY = 2147483591;

            int mainChoose, moveChoose;
            
            int[,] matrix = { };

            int[][] raggedArr = { };

            string text = "";

            string[] words = { };
            string[] separator = { };

            do {
                Console.WriteLine("_______Главное меню_______");
                Console.WriteLine("Выберите интересующее вас задание:");
                Console.WriteLine("1) Работа с двумерными массивами");
                Console.WriteLine("2) Работа с рваными массивами");
                Console.WriteLine("3) Работа со строками");
                Console.WriteLine("4) Завершение программы");
                Console.WriteLine("\nВведите номер выбранного пункта:");

                do {
                    mainChoose = IntInput();
                    if (mainChoose < 1 || mainChoose > 4)
                        Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 4.");
                } while (mainChoose < 1 || mainChoose > 4);

                switch (mainChoose) {
                    case 1:
                        
                        Console.WriteLine("___Выбрана работа с двумерным массивом___");

                        do {
                            Console.WriteLine("Выберите доступные действия:");
                            Console.WriteLine("1) Сформировать новую матрицу");
                            Console.WriteLine("2) Добавить новые строки в конец матрицы");
                            Console.WriteLine("3) Вывести матрицу на экран");
                            Console.WriteLine("4) Выход в главное меню");
                            Console.WriteLine("\nВведите номер выбранного пункта:");

                            do
                            {
                                moveChoose = IntInput();
                                if (moveChoose < 1 || moveChoose > 4)
                                    Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 4.");
                            } while (moveChoose < 1 || moveChoose > 4);

                            switch (moveChoose) {
                                case 1:
                                    Console.WriteLine("Выбрано формирование двумерного массива!");
                                    matrix = MakeMatrix();
                                    break;
                                case 2:
                                    Console.WriteLine("Выбрано добавление строк в матрицу!");
                                    AddStrings(ref matrix);
                                    break;
                                case 3:
                                    Print(matrix);
                                    break;
                            }

                        } while (moveChoose != 4);
                        
                        break;
                    case 2:

                        Console.WriteLine("___Выбрана работа с рваным массивом___");

                        do
                        {
                            Console.WriteLine("Выберите доступные действия:");
                            Console.WriteLine("1) Сформировать новый рваный массив");
                            Console.WriteLine("2) Удалить первую строку, содержащую ноль");
                            Console.WriteLine("3) Вывести рваный массив на экран");
                            Console.WriteLine("4) Выход в главное меню");
                            Console.WriteLine("\nВведите номер выбранного пункта:");

                            do
                            {
                                moveChoose = IntInput();
                                if (moveChoose < 1 || moveChoose > 4)
                                    Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 4.");
                            } while (moveChoose < 1 || moveChoose > 4);

                            switch (moveChoose)
                            {
                                case 1:
                                    raggedArr = MakeRaggedArray();
                                    break;
                                case 2:
                                    PopString(ref raggedArr);
                                    break;
                                case 3:
                                    Print(raggedArr);
                                    break;
                            }

                        } while (moveChoose != 4);

                        break;
                    case 3:

                        Console.WriteLine("___Выбрана работа со строками___");

                        do
                        {
                            Console.WriteLine("Выберите доступные действия:");
                            Console.WriteLine("1) Сформировать новую строку");
                            Console.WriteLine("2) Выполнить циклический сдвиг слов");
                            Console.WriteLine("3) Вывести строку на экран");
                            Console.WriteLine("4) Выход в главное меню");
                            Console.WriteLine("\nВведите номер выбранного пункта:");

                            do
                            {
                                moveChoose = IntInput();
                                if (moveChoose < 1 || moveChoose > 4)
                                    Console.WriteLine("Число отсутствует в меню. Введите заново число от 1 до 4.");
                            } while (moveChoose < 1 || moveChoose > 4);

                            switch (moveChoose)
                            {
                                case 1:
                                    MakeString(ref text, ref words, ref separator);
                                    break;
                                case 2:
                                    CyclicShift(ref text, ref words, ref separator);
                                    break;
                                case 3:
                                    Print(text);
                                    break;
                            }

                        } while (moveChoose != 4);

                        break;
                }

            } while (mainChoose != 4);

            Console.WriteLine("Завершение работы программы!");
        }
    }
}