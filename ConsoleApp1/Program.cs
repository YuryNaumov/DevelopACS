using System;
using System.IO;
using MathNet.Numerics.LinearAlgebra;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1.43*x = 19.8*y''' + 114.6*y'' + 43.9*y' + y
            //
            // y0  = y
            // Система диф. уравнений
            // y0' = y1
            // y1' = y2
            // y2' = (-114.6*y2 - 43.9*y1 - y0 + 1.43*x) / 19.8

            int i = 0;
            double h = 0.001;

            double x = readX();
            Console.WriteLine("x = {0}\n", x);

            Vector<double> _Y = Vector<double>.Build.Dense(3, 0);
            Vector<double> _F = Vector<double>.Build.Dense(3);

            while (i < 100) //true
            {
                _F[0] = _Y[1];
                _F[1] = _Y[2];
                _F[2] = (1.43 * x - 114.6 * _Y[2] - 43.9 * _Y[1] - _Y[0]) / 19.8;
                _Y = _Y + h * _F;

                Console.WriteLine("Итерация = {0}:\n{1}", i, _Y);

                i++;
            }

            bool status = writeY(_Y);

            Console.WriteLine(status);

            double readX()
            {
                FileStream file = new FileStream("D:\\Projects\\Sharp\\DevelopACS\\IN.txt", FileMode.Open);
                StreamReader reader = new StreamReader(file);
                double x_ = Convert.ToDouble(reader.ReadToEnd());
                reader.Close();

                return x_;
            }

            bool writeY(Vector<double> Y)
            {
                FileStream file = new FileStream("D:\\Projects\\Sharp\\DevelopACS\\OUT.txt", FileMode.Open);
                StreamWriter writer = new StreamWriter(file);
                string s = Y[0].ToString() + "\n" + Y[1].ToString() + "\n" + Y[2].ToString() + "\n";
                writer.Write(s);
                writer.Close();

                return true;
            }
        }
    }
}
