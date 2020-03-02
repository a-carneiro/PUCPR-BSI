using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace compilador
{
    class Program
    {
        static void Main(string[] args)
        {
            //var equations = args[0];
            //var equations = "((12+)(12+)*)";
            // var equations = "(1 0 /)";
            var input = "((1 2 +)(1 2 +) *)";
            //var input = args[0];

            if (input == null)
            {
                Console.WriteLine($"Input não pode ser nulo");
                return;
            }

            Console.WriteLine($"Expressão => {input}");

            char[] sp = new char[] { ' ', '\t', '(', ')' };

            var a = input.Split(sp, StringSplitOptions.RemoveEmptyEntries);

            Stack<string> pilha = new Stack<string>(input.Split(sp, StringSplitOptions.RemoveEmptyEntries));

            if (pilha.Count == 0) return;

            try
            {
                var result = processaPilha(pilha);

                if (pilha.Count != 0) 
                    throw new Exception();

                Console.WriteLine($"RESULT => {result}");
            }
            catch (Exception e) 
            { 
                Console.WriteLine("|vish, Deu erro");
                Console.WriteLine("|Verifique se o input está correto, deve ser esse padrão ((1 2 +)(1 2 +) *)");
                Console.WriteLine("|Verifique se não teve divisão por zero");
                Console.WriteLine("|Tente novamente");
            }

            Console.WriteLine($"**FIM**");
            Console.ReadKey();
        }

        private static double processaPilha(Stack<string> pilha)
        {
            var op = pilha.Pop();
            double x, y;

            if (!Double.TryParse(op, out x))
            {
                y = processaPilha(pilha); x = processaPilha(pilha);
                switch (op)
                {
                    case "+":
                        x += y;
                        break;
                    case "-":
                        x -= y;
                        break;
                    case "*":
                        x *= y;
                        break;
                    case "/":
                        if (y == 0)
                        {
                            Console.WriteLine("***DIVISÃO POR ZERO***");
                            throw new Exception();
                        }
                        x /= y;
                        break;
                    default:
                        throw new Exception();
                }
            }
            return x;
        }
    }
}
