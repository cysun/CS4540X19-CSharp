using System;

namespace DelegateExample
{
    public delegate int BinaryOp(int op1, int op2);

    class Program
    {
        static int Add1(int a, int b)
        {
            Console.WriteLine("Add1: {0}+{1}={2}", a, b, a + b);
            return a + b;
        }

        int Add2(int a, int b)
        {
            Console.WriteLine("Add2: {0}+{1}={2}", a, b, a + b);
            return a + b;
        }

        static void Main(string[] args)
        {
            BinaryOp bop = new BinaryOp(Program.Add1);
            Program p = new Program();
            bop += new BinaryOp(p.Add2);
            bop(10, 20);

            Func<int, int, int> bop2 = Program.Add1;
            bop2 += p.Add2;
            bop2(11, 22);

            Func<int, int, int> bop3 = delegate (int a, int b)
             {
                 Console.WriteLine("Add3: {0}+{1}={2}", a, b, a + b);
                 return a + b;
             };
            bop3 += (a, b) =>
            {
                Console.WriteLine("Add4: {0}+{1}={2}", a, b, a + b);
                return a + b;
            };
            bop3(12, 23);
        }
    }
}
