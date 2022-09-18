using System;

namespace determinant
{
    internal class Program
    {
        
        static int[,] InitMatrix()
    {
        // создание матрицы
        int[,] array = { };
        Console.WriteLine("\nВыберете способ формирования Двумерного Массива:\n1. ДСЧ.\n2. Ручной ввод элементов.");
        int choise = 1;//InputInt32("", 1, 2); // режим инициализации матрицы
        switch (choise)
        {
            case 1: // дсч
                array = RandomMatrix();
                break;
            case 2: // ввод
                //array = InputMatrix();
                break;
        }
        Console.WriteLine($"Двумерный Массив {array.GetLength(0)}*{array.GetLength(1)} сформирован.");
        return array;
    }
    
        static int[,] RandomMatrix()
        {
            //случайная матрица
            int lines = 3;//new Random().Next(3, 5);
            int[,] matrix = new int[lines, lines];
            for (int i = 0; i < lines; i++)//перебор строк
                for (int j = 0; j < lines; j++)//перебор элементов 
                    matrix[i, j] = new Random().Next(-10, 10);//генерация элемента
            return matrix;
        }
        
        static void PrintMatrix(int[,] matrix, string msg = "")
        {
            //печать матрицы
            Console.WriteLine(msg);
            if (!IsEmpty(matrix))
                for (int i = 0; i < matrix.GetLength(0); i++)//перебор строк
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)//перебор элементов
                        Console.Write($"{matrix[i, j],4}");
                    Console.WriteLine();
                }
            else Console.WriteLine("Пустой массив!");
        }

        static bool IsEmpty(int[,] matrix)
        {
            //false if IS empty, true if IS NOT empty
            if (matrix == null || matrix.Length == 0) return true;
            else return false;
        }
        
        static double[,] InitMatrixD()
        {
            // создание матрицы
            double[,] array = { };
            //Console.WriteLine("\nВыберете способ формирования Двумерного Массива:\n1. ДСЧ.\n2. Ручной ввод элементов.");
            int choise = 1;//InputInt32("", 1, 2); // режим инициализации матрицы
            switch (choise)
            {
                case 1: // дсч
                    array = RandomMatrixD();
                    break;
                case 2: // ввод
                    //array = InputMatrix();
                    break;
            }
            Console.WriteLine($"Двумерный Массив {array.GetLength(0)}*{array.GetLength(1)} сформирован.");
            return array;
        }
    
        static double[,] RandomMatrixD()
        {
            //случайная матрица
            int lines = 3;//new Random().Next(3, 5);
            double[,] matrix = new double[lines, lines];
            for (int i = 0; i < lines; i++)//перебор строк
            for (int j = 0; j < lines; j++)//перебор элементов 
                matrix[i, j] = new Random().Next(-10, 10);//генерация элемента
            return matrix;
        }
        
        static void PrintMatrixD(double[,] matrix, string msg = "")
        {
            //печать матрицы
            Console.WriteLine(msg);
            if (!IsEmpty(matrix))
                for (int i = 0; i < matrix.GetLength(0); i++)//перебор строк
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)//перебор элементов
                        Console.Write($"{matrix[i, j],4}");
                    Console.WriteLine();
                }
            else Console.WriteLine("Пустой массив!");
        }

        static bool IsEmpty(double[,] matrix)
        {
            //false if IS empty, true if IS NOT empty
            if (matrix == null || matrix.Length == 0) return true;
            else return false;
        }

        static int Determinant(int[,] matrix)
        {
                //recursive by Laplas aproach
                if (matrix.GetLength(0) == 1)
                    return matrix[0, 0];
                if (matrix.GetLength(0) == 2)
                    return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
                //return matrix[0,0] * SmallerMatrix(matrix)
                int sign = 1, det=0;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    sign = 1;
                    if (i % 2 == 1)
                        sign = -1;
                    det += (sign * matrix[0, i] * Determinant(SmallerMatrix(matrix, i)));
                }

                return det;
        }
        
        static int[,] SmallerMatrix(int[,] matrix, int i)
        {
            int size = matrix.GetLength(1);
            int[,] smallMatrix = new int[size - 1,size - 1];//размер >1

            int a=0, b=0;
            for (int j = 0; j < size; j++)
            {
                if (j==0)
                    continue;
                for (int k = 0; k < size; k++)
                {
                    if (k == i)
                        continue;
                    smallMatrix[a, b++] = matrix[j, k];
                }

                a++;
                b = 0;
            }

            return smallMatrix;
        }
        
        
        
        
        static double[,] SmallerMatrixGauss(double[,] matrix)
        {
            // минор размерностью на 1 меньше
            int size = matrix.GetLength(1);
            double[,] smallMatrix = new double[size - 1,size - 1];//размер >1

            int a=0, b=0;
            for (int j = 1; j < size; j++)
            {
                for (int k = 1; k < size; k++)
                {
                    smallMatrix[a, b++] = matrix[j, k];
                }

                a++;
                b = 0;
            }

            return smallMatrix;
        }

        static int sign = 1;//знак детерминанта
        
        static double[,] GaussMatrix(double[,] matrix)
        {
            if (matrix.GetLength(0) == 1) 
                return matrix; // возврат матрица 1*1


            double mult;//множитель
            if (matrix[0, 0] == 0)
                matrix = SwapLines(matrix, 0);
            for (int i = 1; i < matrix.GetLength(1); i++)//перебор строк 
            {
                if (matrix[i, 0] != 0)
                {
                    mult = matrix[i, 0] / matrix[0, 0];
                    matrix[i, 0] = 0;
                    for (int j = 1; j < matrix.GetLength(0); j++)//перебор элементов в строке
                    {
                        matrix[i, j] -= matrix[0, j] * mult;
                    }
                }
                else
                {
                    matrix = SwapLines(matrix, i);
                    i--;
                }
                
            }
            GaussMatrix(SmallerMatrixGauss(matrix));
            return matrix;
        }


        static double[,] SwapLines(double[,] matrix, int a)
        {
            // a to notnull
            int notNull=-1;
            double temp=0;

            if (matrix.GetLength(0) < 2)//нечего свопать
                return matrix;
            
            for (int i = 1; i < matrix.GetLength(0); i++)//поиск ненулевого элемента в столбце
                if (matrix[i, 0] != 0)
                {
                    notNull = i;
                    break;
                }

            //if (notNull == -1)//ненулевых элементов нет
            //    return matrix;//зациклим?) ага)

            for (int i = 0; i < matrix.GetLength(0); i++)//своп линий
            {
                temp = matrix[a, i];
                matrix[a, i] = matrix[notNull, i];
                matrix[notNull, i] = temp;
            }

            sign *= -1;//изменяем знак детерминанта при обмене строк
            return matrix;
        }

        public static double CountDeterminant(double[,] matrix)
        {
            //перемножение элементов главной диагонали
            double res = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                res *= matrix[i, i];
            }

            return res * sign;
        }   
        
        public static void Main(string[] args)
        {
            //лаплас
            //int[,] matrix = new int[,]{{5, 2, -1, 6}, {3, 4, -1, 5},{0, 0, 0, 1},{-1, 2, -2, 0}};
            //int[,] matrix = InitMatrix();
            //PrintMatrix(matrix);
            
            //Console.WriteLine(Determinant(matrix));
            
            //гаус
            //double[,] matrixD = new double[,]{{5, 2, -1, 6}, {3, 4, -1, 5},{0, 0, 0, 1},{-1, 2, -2, 0}};
            double[,] matrixD = InitMatrixD();
            PrintMatrixD(matrixD);
            double[,] matrixDupd = GaussMatrix(matrixD);
            
            PrintMatrixD(matrixDupd);
            //PrintMatrixD(SwapLines(matrixD,2));
            
            Console.WriteLine(CountDeterminant(matrixDupd));
        }
    }
}