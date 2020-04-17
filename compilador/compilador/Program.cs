using System;
using System.Collections.Generic;
using System.Linq;

namespace compilador
{
    class Program
    {
        private static string VariableA { get; set; }
        private static string VariableB { get; set; }
        private static string VariableC { get; set; }
        static void Main(string[] args)
        {
            //Pode ser executado por CLI
            //Basta localizar a pasta bin, ou executar o ex. passando os parametros
            //EX: "1;10;12" "((a b +)(b c +) *)"
            //A primeira parte se refere as variáveis, A, B, respectivamente,
            //A segunda parte se refere a expressão.
            //Lembrando que é permitido apenas 2 repeticoes de variáveis na expressão.

            var expression = string.Empty;
            var isConsole = false;

            if (!args.Any())
            {
                Console.WriteLine("Digite uma expressão. EX: ((a b +)(b c +) *)");
                expression = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine("Os valores devem ser numéricos");

                Console.WriteLine("Digite o valor para A");
                VariableA = Console.ReadLine();

                Console.WriteLine("Digite o valor para B");
                VariableB = Console.ReadLine();

                Console.WriteLine("Digite o valor para C");
                VariableC = Console.ReadLine();

                isConsole = true;
            }
            else
            {
                var variables = args[0].Split(";");

                VariableA = variables[0];
                VariableB = variables[1];
                VariableC = variables[2];

                expression = args[1];
            }

            expression = expression.ToUpper();

            if (!ExpressionIsValid(expression))
            {
                Console.WriteLine($"Input {expression} Não é válido");
                return;
            }

            Console.Clear();

            Console.WriteLine();
            Console.WriteLine($"Expressão => {expression}");
            Console.WriteLine($"A={VariableA}, B={VariableB}, C={VariableC}");
            Console.WriteLine();

            char[] sp = new char[] { ' ', '\t', '(', ')' };

            var a = expression.Split(sp, StringSplitOptions.RemoveEmptyEntries);

            Stack<string> pilha = new Stack<string>(expression.Split(sp, StringSplitOptions.RemoveEmptyEntries));

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
                Console.WriteLine();
                Console.WriteLine("|vish, Deu erro");
                Console.WriteLine("|Verifique se o input está correto, deve ser esse padrão ((1 2 +)(1 2 +) *)");
                Console.WriteLine("|Verifique se não teve divisão por zero");
                Console.WriteLine("|Tente novamente");
                if (isConsole)
                    Main(args);
            }

            Console.WriteLine($"**FIM**");
            Console.ReadKey();
        }


        private static bool ExpressionIsValid(string expression)
        {
            if (expression is null)
                return false;
            if (expression.Split("A").Length > 3 || expression.Split("B").Length > 3 || expression.Split("B").Length > 3)
                return false;

            return true;
        }

        private static double processaPilha(Stack<string> pilha)
        {
            var op = pilha.Pop();
            double x, y;

            switch (op)
            {
                case "A":
                    op = VariableA;
                    break;
                case "B":
                    op = VariableB;
                    break;
                case "C":
                    op = VariableC;
                    break;
            }

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