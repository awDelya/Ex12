using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My_methods;

namespace SortMas2
{
    class Program
    {
        private static double[] rndMas, vozMas, ubvMas;
        private static int rndSrv = 0, vozSrv = 0, ubvSrv = 0;
        private static int rndPer = 0, vozPer = 0, ubvPer = 0;
        private static Random rnd = new Random();
        private static bool sort = false;
        private static int Menu()
        {
            Color.Print("\n Выберите пункт меню", ConsoleColor.Yellow);
            Color.Print("\n\n 1) Создать массив" +
                        "\n\n 2) Отсортировать массив" +
                        "\n\n 3) Напечатать массив" +
                        "\n\n 4) Выход", ConsoleColor.Cyan);
            Color.Print("\n\n Цифра: ", ConsoleColor.Black, ConsoleColor.White);
            return Number.Check(1, 4);
        }
        private static int ChooseSort()
        {
            Console.Clear();
            Color.Print("\n Выберите пункт меню", ConsoleColor.Yellow);
            Color.Print("\n\n 1) Блочная сортировка" +
                        "\n\n 2) Быстрая сортировка" +
                        "\n\n 3) Назад", ConsoleColor.Cyan);
            Color.Print("\n\n Цифра: ", ConsoleColor.Black, ConsoleColor.White);
            return Number.Check(1, 3);
        }
        private static bool CreateMas()
        {
            Console.Clear();
            Color.Print("\n Сколько эллементов в массиве: ", ConsoleColor.Yellow);
            int kol = Number.Check(1, int.MaxValue);
            rndMas = new double[kol];
            vozMas = new double[kol];
            ubvMas = new double[kol];
            Console.Clear();
            if (Text.HowAdd() == 1)
            {
                for (int i = 0; i < rndMas.Length; ++i)
                {
                    Console.Clear();
                    Color.Print("\n Введите " + (i + 1) + " число: ", ConsoleColor.Yellow);
                    rndMas[i] = Number.Check(-99, 100);
                }
            }
            else
            {
                for (int i = 0; i < rndMas.Length; ++i)
                    rndMas[i] = rnd.Next(-99, 100);
            }
            for (int i = 0; i < vozMas.Length; ++i)
                vozMas[i] = rndMas[i];
            for (int i = 0; i < ubvMas.Length; ++i)
                ubvMas[i] = rndMas[i];
            Array.Sort(vozMas);
            Array.Sort(ubvMas);
            Array.Reverse(ubvMas);
            return true;
        }
        private static void PrintMas()
        {
            Console.Clear();
            if (!sort)
            {
                Color.Print("\n Рандомный массив выглядит так: \n\n", ConsoleColor.Magenta);
                for (int i = 0; i < rndMas.Length; ++i)
                    Color.Print(" " + rndMas[i], ConsoleColor.Cyan);
                Color.Print("\n Колличество сравнений: " + rndSrv + ", Колличество перестановок: " + rndPer + "\n", ConsoleColor.Yellow);
                Color.Print("\n Возрастающий массив выглядит так: \n\n", ConsoleColor.Magenta);
                for (int i = 0; i < vozMas.Length; ++i)
                    Color.Print(" " + vozMas[i], ConsoleColor.Cyan);
                Color.Print("\n Колличество сравнений: " + vozSrv + ", Колличество перестановок: " + vozPer + "\n", ConsoleColor.Yellow);
                Color.Print("\n Убывающий массив выглядит так: \n\n", ConsoleColor.Magenta);
                for (int i = 0; i < ubvMas.Length; ++i)
                    Color.Print(" " + ubvMas[i], ConsoleColor.Cyan);
                Color.Print("\n Колличество сравнений: " + ubvSrv + ", Колличество перестановок: " + ubvPer + "\n", ConsoleColor.Yellow);
            }
            else
            {
                Color.Print("\n Отсортированный рандомный массив выглядит так: \n\n", ConsoleColor.Magenta);
                for (int i = 0; i < rndMas.Length; ++i)
                    Color.Print(" " + rndMas[i], ConsoleColor.Cyan);
                Color.Print("\n Колличество сравнений: " + rndSrv + ", Колличество перестановок: " + rndPer + "\n", ConsoleColor.Yellow);
                Color.Print("\n Отсортированный возрастающий массив выглядит так: \n\n", ConsoleColor.Magenta);
                for (int i = 0; i < vozMas.Length; ++i)
                    Color.Print(" " + vozMas[i], ConsoleColor.Cyan);
                Color.Print("\n Колличество сравнений: " + vozSrv + ", Колличество перестановок: " + vozPer + "\n", ConsoleColor.Yellow);
                Color.Print("\n Отсортированный убывающий массив выглядит так: \n\n", ConsoleColor.Magenta);
                for (int i = 0; i < ubvMas.Length; ++i)
                    Color.Print(" " + ubvMas[i], ConsoleColor.Cyan);
                Color.Print("\n Колличество сравнений: " + ubvSrv + ", Колличество перестановок: " + ubvPer + "\n", ConsoleColor.Yellow);
            }
        }
        private static void BucketSort(double[] a, ref int sravnenie, ref int perestonovka)
        {
            // Примем, что количество корзин равно количеству элементов в массиве-источнике.
            // Тогда:
            // массив корзин
            List<double>[] aux = new List<double>[a.Length];

            // каждую корзину проинициализировать
            for (int i = 0; i < aux.Length; ++i)
                aux[i] = new List<double>();

            // найти диапазон значений в массиве-источнике
            double minValue = a[0];
            double maxValue = a[0];

            for (int i = 1; i < a.Length; ++i)
            {
                if (a[i] < minValue)
                {
                    minValue = a[i];
                    sravnenie++;
                }
                else if (a[i] > maxValue)
                {
                    maxValue = a[i];
                    sravnenie++;
                }
            }

            // эта величина будет использоваться a.Length раз, поэтому имеет смысл её сохранить.
            double numRange = maxValue - minValue;

            for (int i = 0; i < a.Length; ++i)
            {
                // вычисление индекса корзины
                int bcktIdx = (int)Math.Floor((a[i] - minValue) / numRange * (aux.Length - 1));

                // добавление элемента в соответствующую корзину
                aux[bcktIdx].Add(a[i]);
                perestonovka++;
            }

            // сортировка корзин. Здесь я, для упрощения себе писанины, использую библиотечную сортировку
            for (int i = 0; i < aux.Length; ++i)
            {
                aux[i].Sort();
                sravnenie++;
                perestonovka++;
            }
            // собираем отсортированные элементы обратно в массив-источник
            int idx = 0;

            for (int i = 0; i < aux.Length; ++i)
            {
                for (int j = 0; j < aux[i].Count; ++j)
                {
                    a[idx++] = aux[i][j];
                    perestonovka++;
                }
            }
        }
        private static void ForBucketSort()
        {
            BucketSort(rndMas, ref rndSrv, ref rndPer);
            BucketSort(vozMas, ref vozSrv, ref vozPer);
            BucketSort(ubvMas, ref ubvSrv, ref ubvPer);
        }
        private static void QuickSort(double[] arr, long first, long last, ref int sravnenie, ref int perestonovka)
        {
            double p = arr[(last - first) / 2 + first];
            double temp;
            long i = first, j = last;
            while (i <= j)
            {
                while (arr[i] < p && i <= last) { sravnenie++;  ++i; }
                while (arr[j] > p && j >= first) { sravnenie++;  --j; }
                if (i <= j)
                {
                    temp = arr[i];
                    perestonovka++;
                    arr[i] = arr[j];
                    perestonovka++;
                    arr[j] = temp;
                    perestonovka++;
                    ++i; --j;
                }
            }
            if (j > first) QuickSort(arr, first, j, ref sravnenie, ref perestonovka);
            if (i < last) QuickSort(arr, i, last, ref sravnenie, ref perestonovka);
        }
        private static void ForQuickSort()
        {
            QuickSort(rndMas, 0, rndMas.Length - 1, ref rndSrv, ref rndPer);
            QuickSort(vozMas, 0, vozMas.Length - 1, ref vozSrv, ref vozPer);
            QuickSort(ubvMas, 0, ubvMas.Length - 1, ref ubvSrv, ref ubvPer);
        }
        static void Main()
        {
            bool ok = false;
            Again:
            Console.Clear();
            switch (Menu())
            {
                case 1:
                    ok = CreateMas();
                    sort = false;
                    rndPer = 0;
                    rndSrv = 0;
                    vozPer = 0;
                    vozSrv = 0;
                    ubvPer = 0;
                    ubvSrv = 0;
                    Color.Print("\n Созданно!", ConsoleColor.Green);
                    Text.GoBackMenu();
                    goto Again;
                case 2:
                    bool ok2 = false;
                    if (ok && !sort)
                    {
                        for (int i = 0; i < rndMas.Length; ++i)
                            try
                            {
                                if (rndMas[i] != rndMas[i + 1])
                                    ok2 = true;
                            }
                            catch (IndexOutOfRangeException) { }
                        if (ok2)
                        {
                            sort = true;
                            switch(ChooseSort())
                            {
                                case 1:
                                    ForBucketSort();
                                    break;
                                case 2:
                                    ForQuickSort();
                                    break;
                                case 3:
                                    sort = false;
                                    break;
                            }
                            Color.Print("\n Отсортировано!", ConsoleColor.Green);
                        }
                        else
                        {
                            Color.Print("\n Массив состоит из повторяющихся элементов, сортировка невозможна!", ConsoleColor.Red);
                        }
                    }
                    else
                    {
                        Color.Print("\n Создайте новый массив!", ConsoleColor.Red);
                    }
                    Text.GoBackMenu();
                    goto Again;
                case 3:
                    if (ok)
                    {
                        PrintMas();
                    }
                    else
                    {
                        Color.Print("\n Создайте новый массив!", ConsoleColor.Red);
                    }
                    Text.GoBackMenu();
                    goto Again;
                case 4:
                    break;
            }
        }
    }
}
