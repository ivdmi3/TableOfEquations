using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MathWorker
{
    public class EquationParser
    {
        private const int STRING_BUIDER_MAX_CAPACITY = 16;
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";
        private const string ERROR_PARENTHESIS = "Mismatched parentheses";

        private static string[] RESULT_ERROR_PARENTHESIS = new string[] { ERROR_PARENTHESIS };

        private readonly static HashSet<char> TokenSeparators;
        private readonly static Dictionary<char, OperatorInfo> OperatorInfos;
        private readonly static HashSet<string> OperatorChecker;

        private delegate decimal BinaryOperator(decimal operand1, decimal operand2);
        private readonly static Dictionary<char, BinaryOperator> operations;

        private Dictionary<string, double> ConstantsDicitionary;
        private HashSet<string> ConstantsNames;
        private bool HasConstatsDictionary = false;

        private delegate bool CheckFloat(char c);
        private CheckFloat CheckFloatDelegate;

        private delegate string ReplaceFloatDelimeter(string token);
        private ReplaceFloatDelimeter ReplaceFloatDelimeterDelegate;

        static EquationParser()
        {
            TokenSeparators = new HashSet<char> { '(', ')', '+', '*', '/', '^' };
            OperatorInfos = new Dictionary<char, OperatorInfo>()
            {
                { '+', new OperatorInfo(1, Associativity.Left) },
                { '-', new OperatorInfo(1, Associativity.Left) },
                { '*', new OperatorInfo(2, Associativity.Left) },
                { '/', new OperatorInfo(2, Associativity.Left) },
                { '^', new OperatorInfo(3, Associativity.Right) }
            };
            OperatorChecker = new HashSet<string> { "+", "-", "*", "/", "^" };
            operations = new Dictionary<char, BinaryOperator>()
            {
                { '+', (op1,op2) => op2+op1},
                { '-', (op1,op2) => op2-op1},
                { '*', (op1,op2) => op2*op1},
                { '/', (op1,op2) => op2/op1},
                { '^', (op1,op2) => (decimal) Math.Pow(Convert.ToDouble(op2), Convert.ToDouble(op1))}
            };
        }

        #region Implementation
        private string InternalCalculate(string input)
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(ShuntingYardAlgorithm(input.Substring(1)));
            string str = queue.Dequeue();
            while (queue.Count >= 0)
            {
                if (!OperatorChecker.Contains(str))
                {
                    stack.Push(str);
                    if (queue.Count == 0) break;
                    str = queue.Dequeue();
                }
                else
                {
                    decimal result = 0;
                    try
                    {
                        decimal a = Convert.ToDecimal(stack.Pop());//, CultureInfo.InvariantCulture);
                        decimal b = Convert.ToDecimal(stack.Pop());//, CultureInfo.InvariantCulture);
                        result = operations[str[0]](a, b);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    stack.Push(result.ToString());
                    if (queue.Count > 0)
                        str = queue.Dequeue();
                    else
                        break;
                }

            }
            return stack.Pop();
        }
        private IEnumerable<string> TokenIterator(string input)
        {
            int index = 0;
            bool IsPreviousCharSeparator = true;
            StringBuilder sb = new StringBuilder(STRING_BUIDER_MAX_CAPACITY);
            while (index < input.Length)
            {
                sb.Clear();
                char curChar = input[index];
                sb.Append(input[index]);
                if (curChar == '-')
                {
                    if (IsPreviousCharSeparator)
                    {
                        if (char.IsDigit(input[index + 1]))
                            for (int i = index + 1; i < input.Length && CheckFloatDelegate(input[i]); i++)
                                sb.Append(input[i]);
                        else if (char.IsLetter(input[index + 1]))
                            for (int i = index + 1; i < input.Length && (char.IsLetter(input[i]) || char.IsDigit(input[i])); i++)
                                sb.Append(input[i]);
                        IsPreviousCharSeparator = false;
                    }
                    else
                        IsPreviousCharSeparator = true;
                }
                else if (!TokenSeparators.Contains(curChar))
                {
                    if (char.IsDigit(curChar))
                        for (int i = index + 1; i < input.Length && CheckFloatDelegate(input[i]); i++)
                            sb.Append(input[i]);
                    else if (char.IsLetter(curChar))
                        for (int i = index + 1; i < input.Length && (char.IsLetter(input[i]) || char.IsDigit(input[i])); i++)
                            sb.Append(input[i]);
                    IsPreviousCharSeparator = false;
                }
                else
                    IsPreviousCharSeparator = true;

                yield return sb.ToString();
                index += sb.Length;
            }
        }

        private IEnumerable<string> TokenIterator2(string input)
        {
            int index = 0;
            bool IsPreviousCharSeparator = true;
            while (index < input.Length)
            {
                string token = string.Empty;
                if (input[index] == '-')
                {
                    if (IsPreviousCharSeparator)
                    {
                        if (char.IsDigit(input[index + 1]))
                            token = '-' + MakeNumericToken(index + 1, input);
                        else if (char.IsLetter(input[index + 1]))
                            token = '-' + MakeTextToken(index + 1, input);
                        IsPreviousCharSeparator = false;
                    }
                    else
                    {
                        token = char.ToString('-');
                        IsPreviousCharSeparator = true;
                    }                                            
                }
                else if (!TokenSeparators.Contains(input[index]))
                {
                    if (char.IsDigit(input[index]))
                        token = MakeNumericToken(index, input);
                    else if (char.IsLetter(input[index]))
                        token = MakeTextToken(index, input);
                    IsPreviousCharSeparator = false;
                }
                else
                {
                    token = char.ToString(input[index]);
                    IsPreviousCharSeparator = true;
                }

                index += token.Length;
                yield return token;                
            }
        }

        private string MakeNumericToken(int startIndex, string input)
        {
            StringBuilder sb = new StringBuilder(STRING_BUIDER_MAX_CAPACITY);
            for (int i = startIndex; i < input.Length && CheckFloatDelegate(input[i]); i++)
                sb.Append(input[i]);
            return sb.ToString();
        }

        private string MakeTextToken(int startIndex, string input)
        {
            StringBuilder sb = new StringBuilder(STRING_BUIDER_MAX_CAPACITY);
            for (int i = startIndex; i < input.Length && (char.IsLetter(input[i]) || char.IsDigit(input[i])); i++)
                sb.Append(input[i]);
            return sb.ToString();
        }


        private IEnumerable<string> ShuntingYardAlgorithm(string input)
        {
            Queue<string> outputQueue = new Queue<string>();
            Stack<string> StackOperators = new Stack<string>();

            foreach (string token in TokenIterator(input))
            {
                decimal dValue;
                string value = token;
                string signPrefix = string.Empty;
                if (token.Length > 1 && token.StartsWith("-"))
                {
                    value = token.Substring(1);
                    signPrefix = "-";
                }
                if (decimal.TryParse(ReplaceFloatDelimeterDelegate(value), out dValue))
                    outputQueue.Enqueue(signPrefix + dValue.ToString());
                else if (HasConstatsDictionary && ConstantsNames.Contains(value))
                    outputQueue.Enqueue(signPrefix + ConstantsDicitionary[value].ToString());
                else if (OperatorChecker.Contains(value))
                {
                    while (StackOperators.Count > 0
                        && !StackOperators.Peek().Equals(LEFT_PARENTHESIS)
                        && (OperatorInfos[StackOperators.Peek()[0]].Precedence > OperatorInfos[value[0]].Precedence
                        || (OperatorInfos[StackOperators.Peek()[0]].Precedence == OperatorInfos[value[0]].Precedence && OperatorInfos[value[0]].Associativity == Associativity.Left)))
                        outputQueue.Enqueue(StackOperators.Pop());
                    StackOperators.Push(value);
                }
                else if (value.Equals(LEFT_PARENTHESIS))
                    StackOperators.Push(value);
                else if (value.Equals(RIGHT_PARENTHESIS))
                {
                    while (StackOperators.Count > 0 && !StackOperators.Peek().Equals(LEFT_PARENTHESIS))
                        outputQueue.Enqueue(StackOperators.Pop());

                    if (StackOperators.Count == 0)
                        return RESULT_ERROR_PARENTHESIS; //throw new ArithmeticException("Mismatched parentheses");

                    if (StackOperators.Peek().Equals(LEFT_PARENTHESIS))
                        StackOperators.Pop();
                }
            }

            while (StackOperators.Count > 0)
            {
                string top = StackOperators.Pop();
                if (top == LEFT_PARENTHESIS || top == RIGHT_PARENTHESIS) return RESULT_ERROR_PARENTHESIS; //throw new ArithmeticException("Mismatched parentheses");
                outputQueue.Enqueue(top);
            }

            return outputQueue;
        }
        #endregion

        #region Public API
        public EquationParser()
        {
            SetRuLocale();
        }

        public void SetRuLocale()
        {
            CheckFloatDelegate = (char c) => char.IsDigit(c) || c == ',';
            ReplaceFloatDelimeterDelegate = (string token) => token.Replace('.', ',');
        }

        public void SetOtherLocale()
        {
            CheckFloatDelegate = (char c) => char.IsDigit(c) || c == '.';
            ReplaceFloatDelimeterDelegate = (string token) => token.Replace(',', '.');
        }

        public void SetConstants(Dictionary<string, double> ConstantsDicitionary)
        {
            this.ConstantsDicitionary = ConstantsDicitionary;
            ConstantsNames = new HashSet<string>(ConstantsDicitionary.Keys);
            HasConstatsDictionary = true;
        }

        public string Calculate(string input)
        {
            if (!input.StartsWith("=")) return input;
            return InternalCalculate(input);
        }

        #endregion

        #region Helpers
        private class OperatorInfo
        {
            public int Precedence;
            public Associativity Associativity;

            public OperatorInfo(int Precedence, Associativity Associativity)
            {
                this.Associativity = Associativity;
                this.Precedence = Precedence;
            }
        }
        private enum Associativity { Right, Left }
        #endregion
    }

}