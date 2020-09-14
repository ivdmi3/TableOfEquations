﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MathMatrix
{
    public class EquationParser
    {
        private const int STRING_BUIDER_MAX_CAPACITY = 16;
        private const string LEFT_PARENTHESIS = "(";
        private const string RIGHT_PARENTHESIS = ")";
        private const string ERROR_PARENTHESIS = "Mismatched parentheses";

        private readonly static HashSet<char> Separators;
        private readonly static Dictionary<char, OperatorInfo> OperatorInfos;
        private readonly static HashSet<string> OperatorChecker;
        
        private delegate decimal BinaryOperator(decimal operand1, decimal operand2);
        private readonly static Dictionary<char, BinaryOperator> operations;

        private Dictionary<string, double> ConstantsDicitionary;
        private HashSet<string> ConstantsNames;
        private bool HasConstatsDictionary = false;
        static EquationParser()
        {
            Separators = new HashSet<char> { '(', ')', '+', '-', '*', '/', '^' };
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
                { '^', (op1,op2) => (decimal)Math.Pow(Convert.ToDouble(op2), Convert.ToDouble(op1))}
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
                        decimal a = Convert.ToDecimal(stack.Pop());
                        decimal b = Convert.ToDecimal(stack.Pop());
                        result = operations[str.ToCharArray()[0]](a, b);
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
            int charIndex = 0;
            char thisChar;
            while (charIndex < input.Length)
            {
                thisChar = input[charIndex];
                StringBuilder sb = new StringBuilder(STRING_BUIDER_MAX_CAPACITY);
                sb.Append(thisChar);
                if (!Separators.Contains(thisChar))
                {
                    if (char.IsDigit(thisChar))
                        for (int i = charIndex + 1; i < input.Length && (char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'); i++)
                            sb.Append(input[i]);
                    else if (char.IsLetter(thisChar)) 
                        for (int i = charIndex + 1; i < input.Length && (char.IsLetter(input[i]) || char.IsDigit(input[i])); i++)
                            sb.Append(input[i]);
                }
                yield return sb.ToString();
                charIndex += sb.Length;
            }
        }
        private IEnumerable<string> ShuntingYardAlgorithm(string input)
        {
            Queue<string> outputQueue = new Queue<string>();
            Stack<string> StackOperators = new Stack<string>();

            foreach(string token in TokenIterator(input))
            {
                double dValue;
                if (double.TryParse(token, out dValue))
                    outputQueue.Enqueue(token);
                else if (HasConstatsDictionary && ConstantsNames.Contains(token))
                    outputQueue.Enqueue(ConstantsDicitionary[token].ToString());
                else if (OperatorChecker.Contains(token))
                {
                    while (StackOperators.Count > 0
                        && !StackOperators.Peek().Equals(LEFT_PARENTHESIS)
                        && (OperatorInfos[StackOperators.Peek()[0]].Precedence > OperatorInfos[token[0]].Precedence
                        || (OperatorInfos[StackOperators.Peek()[0]].Precedence == OperatorInfos[token[0]].Precedence && OperatorInfos[token[0]].Associativity == Associativity.Left)))
                        outputQueue.Enqueue(StackOperators.Pop());
                    StackOperators.Push(token);
                }
                else if (token.Equals(LEFT_PARENTHESIS))
                    StackOperators.Push(token);
                else if (token.Equals(RIGHT_PARENTHESIS))
                {
                    while (StackOperators.Count > 0 && !StackOperators.Peek().Equals(LEFT_PARENTHESIS))
                        outputQueue.Enqueue(StackOperators.Pop());

                    if (StackOperators.Count == 0)
                        return new string[] { ERROR_PARENTHESIS }; //throw new ArithmeticException("Mismatched parentheses");

                    if (StackOperators.Peek().Equals(LEFT_PARENTHESIS))
                        StackOperators.Pop();
                }
            }

            while(StackOperators.Count > 0)
            {
                string top = StackOperators.Pop();
                if(top == LEFT_PARENTHESIS || top == RIGHT_PARENTHESIS) return new string[] { ERROR_PARENTHESIS }; //throw new ArithmeticException("Mismatched parentheses");
                outputQueue.Enqueue(top);
            }

            return outputQueue;
        }
        #endregion

        #region Public API
        public EquationParser()
        {

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