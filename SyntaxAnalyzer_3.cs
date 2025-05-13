using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Лабораторная_1_компиляторы
{
    public class SyntaxAnalyzer
    {
        private enum State
        {
            Start,
            AfterStruct,
            AfterIdentifier,
            AfterOpenBrace,
            AfterFieldType,
            AfterFieldName,
            AfterSemicolon,
            AfterCloseBrace,
            Error
        }

        public class SyntaxError
        {
            public string Message { get; set; }
            public string Fragment { get; set; }
            public int Position { get; set; }
            public string Recommendation { get; set; }

            public SyntaxError(string message, string fragment, int position, string recommendation)
            {
                Message = message;
                Fragment = fragment;
                Position = position;
                Recommendation = recommendation;
            }

            public string ToRichText()
            {
                return $"• Ошибка: {Message}\n  Позиция: {Position}\n  Фрагмент: '{Fragment}'\n  Рекомендация: {Recommendation}\n\n";
            }
        }

        public List<SyntaxError> Analyze(List<LexicalAnalyzer.Token> tokens)
        {
            List<SyntaxError> errors = new List<SyntaxError>();
            State currentState = State.Start;
            int currentTokenIndex = 0;

            while (currentTokenIndex < tokens.Count && currentState != State.Error)
            {
                var currentToken = tokens[currentTokenIndex];
                bool transitionFound = false;

                switch (currentState)
                {
                    case State.Start:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.StructKeyword)
                        {
                            currentState = State.AfterStruct;
                            transitionFound = true;
                        }
                        break;

                    case State.AfterStruct:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.Identifier)
                        {
                            currentState = State.AfterIdentifier;
                            transitionFound = true;
                        }
                        break;

                    case State.AfterIdentifier:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.OpenBrace)
                        {
                            currentState = State.AfterOpenBrace;
                            transitionFound = true;
                        }
                        break;

                    case State.AfterOpenBrace:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.TypeKeyword)
                        {
                            currentState = State.AfterFieldType;
                            transitionFound = true;
                        }
                        else if (currentToken.Code == (int)LexicalAnalyzer.TokenType.CloseBrace)
                        {
                            currentState = State.AfterCloseBrace;
                            transitionFound = true;
                        }
                        break;

                    case State.AfterFieldType:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.Identifier)
                        {
                            currentState = State.AfterFieldName;
                            transitionFound = true;
                        }
                        break;

                    case State.AfterFieldName:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.EndOfStatement)
                        {
                            currentState = State.AfterSemicolon;
                            transitionFound = true;
                        }
                        break;

                    case State.AfterSemicolon:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.TypeKeyword)
                        {
                            currentState = State.AfterFieldType;
                            transitionFound = true;
                        }
                        else if (currentToken.Code == (int)LexicalAnalyzer.TokenType.CloseBrace)
                        {
                            currentState = State.AfterCloseBrace;
                            transitionFound = true;
                        }
                        break;

                    case State.AfterCloseBrace:
                        if (currentToken.Code == (int)LexicalAnalyzer.TokenType.EndOfStatement)
                        {
                            currentState = State.Start;
                            transitionFound = true;
                        }
                        break;
                }

                if (!transitionFound)
                {
                    string expected = GetExpectedTokens(currentState);
                    errors.Add(new SyntaxError(
                        $"Ожидалось {expected}, но получено '{currentToken.Value}' ({GetTokenTypeName(currentToken.Code)})",
                        currentToken.Value,
                        currentToken.StartPos,
                        $"Исправьте на {expected}"
                    ));
                    currentState = State.Error;
                }
                else
                {
                    currentTokenIndex++;
                }
            }

            if (currentState != State.Start && currentState != State.Error && errors.Count == 0)
            {
                string expected = GetExpectedTokens(currentState);
                errors.Add(new SyntaxError(
                    $"Незавершенная конструкция: ожидалось {expected}",
                    "",
                    tokens.Count > 0 ? tokens[tokens.Count - 1].EndPos : 0,
                    $"Добавьте {expected}"
                ));
            }

            return errors;
        }

        private string GetTokenTypeName(int tokenCode)
        {
            switch (tokenCode)
            {
                case (int)LexicalAnalyzer.TokenType.StructKeyword: return "ключевое слово struct";
                case (int)LexicalAnalyzer.TokenType.Identifier: return "идентификатор";
                case (int)LexicalAnalyzer.TokenType.OpenBrace: return "открывающая скобка";
                case (int)LexicalAnalyzer.TokenType.CloseBrace: return "закрывающая скобка";
                case (int)LexicalAnalyzer.TokenType.TypeKeyword: return "тип данных";
                case (int)LexicalAnalyzer.TokenType.EndOfStatement: return "точка с запятой";
                default: return "неизвестный токен";
            }
        }

        private string GetExpectedTokens(State state)
        {
            switch (state)
            {
                case State.Start: return "'struct'";
                case State.AfterStruct: return "идентификатор";
                case State.AfterIdentifier: return "'{'";
                case State.AfterOpenBrace: return "тип данных или '}'";
                case State.AfterFieldType: return "идентификатор поля";
                case State.AfterFieldName: return "';'";
                case State.AfterSemicolon: return "тип данных или '}'";
                case State.AfterCloseBrace: return "';'";
                default: return "неизвестный токен";
            }
        }

        // Метод для проверки корректности структуры
        public bool IsStructCorrect(List<LexicalAnalyzer.Token> tokens)
        {
            int braceLevel = 0;
            bool hasFields = false;

            foreach (var token in tokens)
            {
                if (token.Code == (int)LexicalAnalyzer.TokenType.OpenBrace)
                {
                    braceLevel++;
                }
                else if (token.Code == (int)LexicalAnalyzer.TokenType.CloseBrace)
                {
                    braceLevel--;
                    if (braceLevel < 0) return false;
                }
                else if (braceLevel == 1 && token.Code == (int)LexicalAnalyzer.TokenType.TypeKeyword)
                {
                    hasFields = true;
                }
            }

            return braceLevel == 0 && hasFields;
        }
        public void PrintResultsToRichTextBox(List<SyntaxError> errors, RichTextBox richTextBox)
        {
            richTextBox.Clear();

            if (errors.Count == 0)
            {
                richTextBox.SelectionColor = System.Drawing.Color.Green;
                richTextBox.AppendText("Синтаксический анализ завершен успешно!\n");
                richTextBox.AppendText("Ошибок не обнаружено.\n");
            }
            else
            {
                richTextBox.SelectionColor = System.Drawing.Color.Red;
                richTextBox.AppendText($"Найдена {errors.Count} ошибка:\n\n");

                foreach (var error in errors)
                {
                    richTextBox.SelectionColor = System.Drawing.Color.Black;
                    richTextBox.AppendText("• Ошибка: ");

                    richTextBox.SelectionColor = System.Drawing.Color.DarkRed;
                    richTextBox.AppendText($"{error.Message}\n");

                    richTextBox.SelectionColor = System.Drawing.Color.Black;
                    richTextBox.AppendText("  Позиция: ");

                    richTextBox.SelectionColor = System.Drawing.Color.Blue;
                    richTextBox.AppendText($"{error.Position}\n");

                    richTextBox.SelectionColor = System.Drawing.Color.Black;
                    richTextBox.AppendText("  Фрагмент: ");

                    richTextBox.SelectionColor = System.Drawing.Color.DarkMagenta;
                    richTextBox.AppendText($"'{error.Fragment}'\n");

                    //richTextBox.SelectionColor = System.Drawing.Color.Black;
                    //richTextBox.AppendText("  Рекомендация: ");

                    //richTextBox.SelectionColor = System.Drawing.Color.DarkGreen;
                    //richTextBox.AppendText($"{error.Recommendation}\n\n");
                }
            }
        }
    }
}