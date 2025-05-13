using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Лабораторная_1_компиляторы
{
    public partial class Form1 : Form
    {
        private string currentFilePath = string.Empty; 
        private bool isTextChanged = false;
        //private Panel lineNumberPanel; // Панель для отображения номеров строк
        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeFontSizeComboBox();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile();
        }

        
        private void UpdateTitle()
        {
            this.Text = string.IsNullOrEmpty(currentFilePath) ? "Компилятор" : Path.GetFileName(currentFilePath) ;
        }
        
        private bool ConfirmSaveChanges()
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Подтверждение", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                return result != DialogResult.Cancel;
            }
            return true;
        }

        
        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveFileAs(); // Если путь пуст, вызываем "Сохранить как"
            else
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text);
                isTextChanged = false;
            }
        }

        
        private void SaveFileAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                    currentFilePath = saveFileDialog.FileName;
                    isTextChanged = false;
                    UpdateTitle();
                }
            }
        }

        
        private void OpenFile()
        {
            if (ConfirmSaveChanges())
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "Текстовые файлы|*.txt|Все файлы|*.*" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                        currentFilePath = openFileDialog.FileName;
                        isTextChanged = false;
                        UpdateTitle();
                    }
                }
            }
        }
        private void NewFile()
        {
            if (ConfirmSaveChanges())
            {
                richTextBox1.Clear();
                currentFilePath = string.Empty;
                isTextChanged = false;
                UpdateTitle();
            }
        }

        
        private void ExitApplication()
        {
            if (ConfirmSaveChanges())
                Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void правкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void сохранитьКакToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void InitializeFontSizeComboBox()
        {
            // Добавляем размеры шрифта в комбобокс
            comboBoxFontSize.Items.AddRange(new object[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 });

            // Устанавливаем начальный размер шрифта
            comboBoxFontSize.SelectedIndex = 4; // Например, 12

            // Подписываемся на событие изменения выбора
            comboBoxFontSize.SelectedIndexChanged += ComboBoxFontSize_SelectedIndexChanged;
        }

        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBoxFontSize.SelectedItem != null)
            {
                int fontSize = (int)comboBoxFontSize.SelectedItem;
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, fontSize);
                //richTextBox2.Font = new Font(richTextBox2.Font.FontFamily, fontSize);
            }
        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            CheckSyntax();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
            CheckSyntax();
        }


        private void CheckSyntax()
        {
            // Получаем текст из редактора
            string code = richTextBox1.Text;

            // Лексический анализ
            var lexer = new LexicalAnalyzer();
            var tokens = lexer.Analyze(code);

            // Синтаксический анализ
            var parser = new SyntaxAnalyzer();
            var errors = parser.Analyze(tokens);

            // Вывод результатов в richTextBox2
            parser.PrintResultsToRichTextBox(errors, richTextBox2);

            //// Дополнительная информация о структуре
            //if (parser.IsStructCorrect(tokens))
            //{
            //    richTextBox2.AppendText("\n\nДополнительная проверка: структура объявлена корректно.");
            //}
            //else if (errors.Count == 0)
            //{
            //    richTextBox2.AppendText("\n\nДополнительная проверка: найдены проблемы в структуре (возможно, не хватает полей или скобки не сбалансированы).");
            //}
        }

        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @"Структуры в языке PHP -- это составные типы данных, которые позволяют
группировать переменные различных типов под одним именем. Для объявления
структуры и её инициализации в PHP используется синтаксис вида:

struct ИмяСтруктуры { тип $поле1; тип $поле2; ... тип $полеN; };

Примеры:

1. Структура Point -- описывает точку в двумерном пространстве с
   координатами x и y:, например: struct Point { float \$x; float \$y; };

2. Пустая структура -- может использоваться как заглушка для
   дальнейшего расширения: struct EmptyStruct {};

В связи с разработанной автоматной грамматикой G[<Struct>],
синтаксический анализатор (парсер) будет считать верными следующие
записи объявления структур:

1. struct User {string $name; int $age; };

2. struct Product { string $title; float $price; };

3. struct Config {};";

            Form4 form4 = new Form4("Постановка задачи", content);
            form4.Show();
        }



        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @"Грамматика G[‹Def›] является автоматной.

Правила (1) -- (18) для G[‹Def›] реализованы на графе (см. рисунок 1).

Сплошные стрелки на графе характеризуют синтаксически верный разбор;
пунктирные символизируют состояние ошибки (ERROR);

Состояние 11 символизирует успешное завершение разбора.";

            Form4 form4 = new Form4("Метод анализа", content);
            form4.Show();
        }



        private void исходныйКодПрограммыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @"Листинг программной части разработанного синтаксического анализатора
объявления и определения структуры на языке PHP представлен в приложении В.";

            Form4 form4 = new Form4("Исходный код программы", content);
            form4.Show();
        }

        private void грамматикаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"Определим грамматику структур на языке PHP G[‹Def›] в нотации Хомского
с продукциями P:

1. <START> → 'STRUCT' <SPACE>
2. <SPACE> → ' '→ <Name>
3. <Name> → 'letter' → <NameRem>
4. <NameRem> → 'letter' → <NameRem>
5. <NameRem> → <Digit> <NameRem>
6. <NameRem> → '{' <TYPE>
7. <TYPE> → 'string' → 'SPACE'
8. <TYPE> → 'int' → <SPACE>
9. <TYPE> → 'bool' → <SPACE>
10. <TYPE> → 'float' → <SPACE>
11. <TYPE> -> 'double' → <SPACE>
12. <SPACE> → ' '→ <SPACE>
13. <SPACE> → '\$' → <StrRem>
14. <StrRem> → symbol <StrRem>
15. <StrRem> → ';' <TYPE>
16. <StrRem> → ';' <END>
17. <END> → '}' FINAL
18. FINAL → ';'‹E› → ‹;›

- ‹Digit› → '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9'
- ‹Letter› → 'a' | 'b' | 'c' | ... | 'z' | 'A' | 'B' | 'C' | ... | 'Z'";

            Form4 form4 = new Form4("Грамматика", content);
            form4.Show();
        }

        private void классификацияГрамматикиToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"Согласно классификации Хомского, грамматика G[‹Def›] является
автоматной.

Поскольку все правила продукции имеют форму либо A → aB, либо A → a,
грамматика является праворекурсивной и, следовательно, соответствует
автоматной грамматике. Это удовлетворяет требованию о том, что все
правила должны быть либо леворекурсивными, либо праворекурсивными -- в
нашем случае они однородно праворекурсивные.";

            Form4 form4 = new Form4("Классификация грамматики", content);
            form4.Show();
        }

        private void метToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
            //// Загрузка изображения из ресурсов проекта
            ////Image image = Properties.Resources.R4.png; // Ваше изображение из ресурсов

            //// Копирование в буфер обмена
            //Clipboard.SetImage(image);

            //// Вставка из буфера обмена в RichTextBox
            //richTextBox1.Paste();
        }

        private void тестовыйПримерToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"Примеры корректных структур:

    Ввод:
struct User {string $name; int $age; };
    Вывод:
Синтаксический анализ завершен успешно!
Ошибок не обнаружено.

    Ввод:
struct Product { string \$title; float \$price; };
    Вывод:
Синтаксический анализ завершен успешно!
Ошибок не обнаружено.

Примеры некорректных структур:

    Ввод
struct { float \$price; }; 
    Вывод
Найдена 1 ошибка:

• Ошибка: Ожидалось идентификатор, но получено '{' (открывающая скобка)
  Позиция: 8
  Фрагмент: '{'

    Ввод:
struct Product string \$title; float \$price; }; 
    Вывод
Найдена 1 ошибка:

• Ошибка: Ожидалось '{', но получено 'string' (тип данных)
  Позиция: 16
  Фрагмент: 'string'";

            Form4 form4 = new Form4("Тестовый пример", content);
            form4.Show();
        }

        private void списокЛитературыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"1. Шорников Ю.В. Теория и практика языковых процессоров : учеб. пособие
   / Ю.В. Шорников. -- Новосибирск: Изд-во НГТУ, 2022.

2. Gries D. Designing Compilers for Digital Computers. New York, Jhon
   Wiley, 1971. 493 p.

3. Теория формальных языков и компиляторов [Электронный ресурс] /
   Электрон. дан. URL:
   https://dispace.edu.nstu.ru/didesk/course/show/8594, свободный.
   Яз.рус. (дата обращения 25.03.2025).";

            Form4 form4 = new Form4("Список литературы", content);
            form4.Show();
        }

        private void исходныйКодПрограммыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string content = @"using System;
using System.Collections.Generic;
using System.Text;

namespace RGZ
{
    public class LexicalAnalyzer
    {
        // Перечисление типов токенов с их числовыми кодами
        public enum TokenType
        {
            UnsignedInt = 1,         // Целое число без знака
            SignedInt = 3,           // Целое число со знаком
            Identifier = 2,          // Идентификатор (начинается с $)
            Keyword = 14,            // Ключевое слово
            Separator = 11,          // Разделитель (пробел)
            AssignmentOperator = 10, // Оператор присваивания
            EndOfStatement = 16,     // Конец оператора (;)
            TypeKeyword = 17,        // Ключевое слово, обозначающее тип данных
            StructKeyword = 18,      // Ключевое слово struct
            OpenBrace = 19,          // Открывающая фигурная скобка {
            CloseBrace = 20,         // Закрывающая фигурная скобка }
            DollarSign = 21,         // Знак доллара $
            StringLiteral = 22,      // Строковый литерал (в кавычках)
            PlusOperator = 23,      // Оператор +
            MinusOperator = 24,      // Оператор -
            InvalidChar = 99         // Недопустимый символ
        }

        // Класс для представления токена
        public class Token
        {
            public int Code { get; set; }       // Код токена (из TokenType)
            public string Type { get; set; }    // Текстовое описание типа
            public string Value { get; set; }   // Значение токена
            public int StartPos { get; set; }   // Начальная позиция в тексте
            public int EndPos { get; set; }     // Конечная позиция в тексте

            // Конструктор токена
            public Token(int code, string type, string value, int startPos, int endPos)
            {
                Code = code;
                Type = type;
                Value = value;
                StartPos = startPos;
                EndPos = endPos;
            }

            // Переопределение метода ToString для удобного вывода
            public override string ToString()
            {
                return $""{Code} - {Type} - {Value} - с {StartPos} по {EndPos} символ"";
            }
        }

        // Словарь ключевых слов и соответствующих им типов токенов
        private static readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
        {
            {""struct"", TokenType.StructKeyword},  // Ключевое слово struct
            {""string"", TokenType.TypeKeyword},     // Ключевое слово для типа string
            {""int"", TokenType.TypeKeyword},       // Ключевое слово для типа int
            {""bool"", TokenType.TypeKeyword},      // Ключевое слово для типа bool
            {""float"", TokenType.TypeKeyword},      // Ключевое слово для типа float
            {""double"", TokenType.TypeKeyword}      // Ключевое слово для типа float
        };

        // Основной метод анализа входной строки
        public List<Token> Analyze(string input)
        {
            List<Token> tokens = new List<Token>(); // Список для хранения токенов
            int currentPos = 0;                     // Текущая позиция в строке
            int lineNumber = 1;                    // Номер текущей строки (для отслеживания переносов)

            // Основной цикл обработки входной строки
            while (currentPos < input.Length)
            {
                char currentChar = input[currentPos]; // Текущий символ

                // Обработка пробельных символов
                if (char.IsWhiteSpace(currentChar))
                {
                    if (currentChar == '\n')
                    {
                        lineNumber++; // Увеличиваем номер строки при переносе
                    }
                    currentPos++;
                    continue; // Пропускаем пробелы
                }

                // Обработка строковых литералов (в кавычках)
                if (currentChar == '""')
                {
                    StringBuilder sb = new StringBuilder();
                    int startPos = currentPos + 1; // Позиция начала строки (без кавычки)
                    sb.Append(currentChar); // Добавляем открывающую кавычку
                    currentPos++;

                    bool escape = false; // Флаг экранирования
                    bool closed = false; // Флаг закрытия строки

                    // Собираем содержимое строкового литерала
                    while (currentPos < input.Length)
                    {
                        currentChar = input[currentPos];
                        sb.Append(currentChar);

                        // Обработка экранированных символов
                        if (currentChar == '\\' && !escape)
                        {
                            escape = true;
                        }
                        else if (currentChar == '""' && !escape)
                        {
                            closed = true; // Найдена закрывающая кавычка
                            currentPos++;
                            break;
                        }
                        else
                        {
                            escape = false;
                        }

                        currentPos++;
                    }

                    // Если строка не закрыта, добавляем ошибку
                    if (!closed)
                    {
                        tokens.Add(new Token(
                            (int)TokenType.InvalidChar,
                            ""незакрытый строковый литерал"",
                            sb.ToString(),
                            startPos,
                            currentPos
                        ));
                    }
                    else
                    {
                        // Добавляем корректный строковый литерал
                        tokens.Add(new Token(
                            (int)TokenType.StringLiteral,
                            ""строковый литерал"",
                            sb.ToString(),
                            startPos,
                            currentPos
                        ));
                    }
                    continue;
                }

                // Обработка операторов + и -
                if (currentChar == '+' || currentChar == '-')
                {
                    // Специальная проверка для минуса (может быть частью числа)
                    if (currentChar == '-' && currentPos + 1 < input.Length && char.IsDigit(input[currentPos + 1]))
                    {
                        StringBuilder sb = new StringBuilder();
                        int startPos = currentPos + 1;
                        sb.Append(currentChar); // Добавляем минус
                        currentPos++;

                        // Собираем все цифры после минуса
                        while (currentPos < input.Length && char.IsDigit(input[currentPos]))
                        {
                            sb.Append(input[currentPos]);
                            currentPos++;
                        }

                        // Добавляем число со знаком
                        tokens.Add(new Token(
                            (int)TokenType.SignedInt,
                            ""целое со знаком"",
                            sb.ToString(),
                            startPos,
                            currentPos
                        ));
                    }
                    else
                    {
                        // Добавляем оператор + или -
                        tokens.Add(new Token(
                            currentChar == '+' ? (int)TokenType.PlusOperator : (int)TokenType.MinusOperator,
                            currentChar == '+' ? ""оператор +"" : ""оператор -"",
                            currentChar.ToString(),
                            currentPos + 1,
                            currentPos + 1
                        ));
                        currentPos++;
                    }
                    continue;
                }

                // Обработка идентификаторов (начинаются с $)
                if (currentChar == '$')
                {
                    int startPos = currentPos + 1;
                    currentPos++;

                    // Проверяем, что после $ идет латинская буква
                    if (currentPos < input.Length && IsLatinLetter(input[currentPos]))
                    {
                        StringBuilder sb = new StringBuilder();

                        // Собираем все допустимые символы идентификатора
                        while (currentPos < input.Length && (IsLatinLetterOrDigit(input[currentPos]) || input[currentPos] == '_'))
                        {
                            sb.Append(input[currentPos]);
                            currentPos++;
                        }

                        // Добавляем идентификатор
                        tokens.Add(new Token(
                            (int)TokenType.Identifier,
                            ""идентификатор"",
                            sb.ToString(),
                            startPos,
                            currentPos
                        ));
                    }
                    else
                    {
                        // Ошибка - после $ нет буквы
                        tokens.Add(new Token(
                            (int)TokenType.InvalidChar,
                            ""недопустимый символ после $"",
                            ""$"",
                            startPos,
                            currentPos
                        ));
                    }
                    continue;
                }

                // Обработка ключевых слов и других буквенных последовательностей
                if (char.IsLetter(currentChar) && IsLatinLetter(currentChar))
                {
                    StringBuilder sb = new StringBuilder();
                    int startPos = currentPos + 1;

                    // Собираем последовательность латинских букв
                    while (currentPos < input.Length && IsLatinLetter(input[currentPos]))
                    {
                        sb.Append(input[currentPos]);
                        currentPos++;
                    }

                    string word = sb.ToString();
                    int endPos = currentPos;

                    // Проверяем, является ли слово ключевым
                    bool isKeyword = Keywords.TryGetValue(word, out TokenType keywordType);

                    // Проверка на частичное совпадение с ключевыми словами
                    bool hasPartialKeyword = false;
                    foreach (var kw in Keywords.Keys)
                    {
                        if (word.StartsWith(kw) && word != kw && word.Length > kw.Length)
                        {
                            // Разделяем на ключевое слово и остаток
                            string keywordPart = kw;
                            string remainingPart = word.Substring(kw.Length);

                            // Добавляем часть, которая является ключевым словом
                            tokens.Add(new Token(
                                (int)Keywords[kw],
                                Keywords[kw] == TokenType.StructKeyword ? ""ключевое слово (struct)"" : ""ключевое слово типа"",
                                keywordPart,
                                startPos,
                                startPos + kw.Length - 1
                            ));

                            // Добавляем остаток как ошибку
                            tokens.Add(new Token(
                                (int)TokenType.InvalidChar,
                                ""недопустимые символы"",
                                remainingPart,
                                startPos + kw.Length,
                                endPos
                            ));

                            hasPartialKeyword = true;
                            break;
                        }
                    }

                    if (hasPartialKeyword)
                    {
                        continue;
                    }

                    // Если это ключевое слово
                    if (isKeyword)
                    {
                        tokens.Add(new Token(
                            (int)keywordType,
                            keywordType == TokenType.StructKeyword ? ""ключевое слово (struct)"" : ""ключевое слово типа"",
                            word,
                            startPos,
                            endPos
                        ));

                        // Обработка идентификатора после ключевого слова и пробела
                        if (currentPos < input.Length && input[currentPos] == ' ')
                        {
                            currentPos++;

                            if (currentPos < input.Length && IsLatinLetter(input[currentPos]))
                            {
                                sb = new StringBuilder();
                                startPos = currentPos + 1;

                                while (currentPos < input.Length && (IsLatinLetterOrDigit(input[currentPos]) || input[currentPos] == '_'))
                                {
                                    sb.Append(input[currentPos]);
                                    currentPos++;
                                }

                                tokens.Add(new Token(
                                    (int)TokenType.Identifier,
                                    ""идентификатор"",
                                    sb.ToString(),
                                    startPos,
                                    currentPos
                                ));
                            }
                        }
                    }
                    else
                    {
                        // Если это не ключевое слово, добавляем как ошибку
                        tokens.Add(new Token(
                            (int)TokenType.InvalidChar,
                            ""недопустимые символы"",
                            word,
                            startPos,
                            endPos
                        ));
                    }
                    continue;
                }

                // Обработка целых чисел без знака
                if (char.IsDigit(currentChar))
                {
                    StringBuilder sb = new StringBuilder();
                    int startPos = currentPos + 1;

                    // Собираем последовательность цифр
                    while (currentPos < input.Length && char.IsDigit(input[currentPos]))
                    {
                        sb.Append(input[currentPos]);
                        currentPos++;
                    }

                    // Добавляем число без знака
                    tokens.Add(new Token(
                        (int)TokenType.UnsignedInt,
                        ""целое без знака"",
                        sb.ToString(),
                        startPos,
                        currentPos
                    ));
                    continue;
                }

                // Обработка специальных символов
                switch (currentChar)
                {
                    case '{':
                        tokens.Add(new Token(
                            (int)TokenType.OpenBrace,
                            ""открывающая фигурная скобка"",
                            ""{"",
                            currentPos + 1,
                            currentPos + 1
                        ));
                        currentPos++;
                        continue;
                    case '}':
                        tokens.Add(new Token(
                            (int)TokenType.CloseBrace,
                            ""закрывающая фигурная скобка"",
                            ""}"",
                            currentPos + 1,
                            currentPos + 1
                        ));
                        currentPos++;
                        continue;
                    case ';':
                        tokens.Add(new Token(
                            (int)TokenType.EndOfStatement,
                            ""конец оператора"",
                            "";"",
                            currentPos + 1,
                            currentPos + 1
                        ));
                        currentPos++;
                        continue;
                    default:
                        // Обработка русских букв как ошибки
                        if (IsCyrillic(currentChar))
                        {
                            StringBuilder sb = new StringBuilder();
                            int startPos = currentPos + 1;

                            while (currentPos < input.Length && IsCyrillic(input[currentPos]))
                            {
                                sb.Append(input[currentPos]);
                                currentPos++;
                            }

                            tokens.Add(new Token(
                                (int)TokenType.InvalidChar,
                                ""недопустимые символы (русские буквы)"",
                                sb.ToString(),
                                startPos,
                                currentPos
                            ));
                        }
                        else
                        {
                            // Любой другой недопустимый символ
                            tokens.Add(new Token(
                                (int)TokenType.InvalidChar,
                                ""недопустимый символ"",
                                currentChar.ToString(),
                                currentPos + 1,
                                currentPos + 1
                            ));
                            currentPos++;
                        }
                        continue;
                }
            }

            return tokens; // Возвращаем список распознанных токенов
        }

        // Проверка, является ли символ латинской буквой
        private bool IsLatinLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        // Проверка, является ли символ латинской буквой или цифрой
        private bool IsLatinLetterOrDigit(char c)
        {
            return IsLatinLetter(c) || char.IsDigit(c);
        }

        // Проверка, является ли символ кириллическим
        private bool IsCyrillic(char c)
        {
            return (c >= 'А' && c <= 'я') || c == 'ё' || c == 'Ё';
        }
    }
}

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
                return $""• Ошибка: {Message}\n  Позиция: {Position}\n  Фрагмент: '{Fragment}'\n  Рекомендация: {Recommendation}\n\n"";
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
                        $""Ожидалось {expected}, но получено '{currentToken.Value}' ({GetTokenTypeName(currentToken.Code)})"",
                        currentToken.Value,
                        currentToken.StartPos,
                        $""Исправьте на {expected}""
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
                    $""Незавершенная конструкция: ожидалось {expected}"",
                    """",
                    tokens.Count > 0 ? tokens[tokens.Count - 1].EndPos : 0,
                    $""Добавьте {expected}""
                ));
            }

            return errors;
        }

        private string GetTokenTypeName(int tokenCode)
        {
            switch (tokenCode)
            {
                case (int)LexicalAnalyzer.TokenType.StructKeyword: return ""ключевое слово struct"";
                case (int)LexicalAnalyzer.TokenType.Identifier: return ""идентификатор"";
                case (int)LexicalAnalyzer.TokenType.OpenBrace: return ""открывающая скобка"";
                case (int)LexicalAnalyzer.TokenType.CloseBrace: return ""закрывающая скобка"";
                case (int)LexicalAnalyzer.TokenType.TypeKeyword: return ""тип данных"";
                case (int)LexicalAnalyzer.TokenType.EndOfStatement: return ""точка с запятой"";
                default: return ""неизвестный токен"";
            }
        }

        private string GetExpectedTokens(State state)
        {
            switch (state)
            {
                case State.Start: return ""'struct'"";
                case State.AfterStruct: return ""идентификатор"";
                case State.AfterIdentifier: return ""'{'"";
                case State.AfterOpenBrace: return ""тип данных или '}'"";
                case State.AfterFieldType: return ""идентификатор поля"";
                case State.AfterFieldName: return ""';'"";
                case State.AfterSemicolon: return ""тип данных или '}'"";
                case State.AfterCloseBrace: return ""';'"";
                default: return ""неизвестный токен"";
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
                richTextBox.AppendText(""Синтаксический анализ завершен успешно!\n"");
                richTextBox.AppendText(""Ошибок не обнаружено.\n"");
            }
            else
            {
                richTextBox.SelectionColor = System.Drawing.Color.Red;
                richTextBox.AppendText($""Найдена {errors.Count} ошибка:\n\n"");

                foreach (var error in errors)
                {
                    richTextBox.SelectionColor = System.Drawing.Color.Black;
                    richTextBox.AppendText(""• Ошибка: "");

                    richTextBox.SelectionColor = System.Drawing.Color.DarkRed;
                    richTextBox.AppendText($""{error.Message}\n"");

                    richTextBox.SelectionColor = System.Drawing.Color.Black;
                    richTextBox.AppendText(""  Позиция: "");

                    richTextBox.SelectionColor = System.Drawing.Color.Blue;
                    richTextBox.AppendText($""{error.Position}\n"");

                    richTextBox.SelectionColor = System.Drawing.Color.Black;
                    richTextBox.AppendText(""  Фрагмент: "");

                    richTextBox.SelectionColor = System.Drawing.Color.DarkMagenta;
                    richTextBox.AppendText($""'{error.Fragment}'\n"");

                    //richTextBox.SelectionColor = System.Drawing.Color.Black;
                    //richTextBox.AppendText(""  Рекомендация: "");

                    //richTextBox.SelectionColor = System.Drawing.Color.DarkGreen;
                    //richTextBox.AppendText($""{error.Recommendation}\n\n"");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Лабораторная_1_компиляторы
{
    public partial class Form1 : Form
    {
        private string currentFilePath = string.Empty; 
        private bool isTextChanged = false;
        //private Panel lineNumberPanel; // Панель для отображения номеров строк
        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeFontSizeComboBox();
            //InitializeLineNumberPanel(); 
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFile();
        }

        
        private void UpdateTitle()
        {
            this.Text = string.IsNullOrEmpty(currentFilePath) ? ""Компилятор"" : Path.GetFileName(currentFilePath) ;
        }
        
        private bool ConfirmSaveChanges()
        {
            if (isTextChanged)
            {
                DialogResult result = MessageBox.Show(""Сохранить изменения?"", ""Подтверждение"", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                    SaveFile();
                return result != DialogResult.Cancel;
            }
            return true;
        }

        
        private void SaveFile()
        {
            if (string.IsNullOrEmpty(currentFilePath))
                SaveFileAs(); // Если путь пуст, вызываем ""Сохранить как""
            else
            {
                File.WriteAllText(currentFilePath, richTextBox1.Text);
                isTextChanged = false;
            }
        }

        
        private void SaveFileAs()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = ""Текстовые файлы|*.txt|Все файлы|*.*"" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                    currentFilePath = saveFileDialog.FileName;
                    isTextChanged = false;
                    UpdateTitle();
                }
            }
        }

        
        private void OpenFile()
        {
            if (ConfirmSaveChanges())
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog { Filter = ""Текстовые файлы|*.txt|Все файлы|*.*"" })
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.Text = File.ReadAllText(openFileDialog.FileName);
                        currentFilePath = openFileDialog.FileName;
                        isTextChanged = false;
                        UpdateTitle();
                    }
                }
            }
        }
        private void NewFile()
        {
            if (ConfirmSaveChanges())
            {
                richTextBox1.Clear();
                currentFilePath = string.Empty;
                isTextChanged = false;
                UpdateTitle();
            }
        }

        
        private void ExitApplication()
        {
            if (ConfirmSaveChanges())
                Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = string.Empty;
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void правкаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }

        private void текстToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void сохранитьКакToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void вызовСправкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void InitializeFontSizeComboBox()
        {
            // Добавляем размеры шрифта в комбобокс
            comboBoxFontSize.Items.AddRange(new object[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 });

            // Устанавливаем начальный размер шрифта
            comboBoxFontSize.SelectedIndex = 4; // Например, 12

            // Подписываемся на событие изменения выбора
            comboBoxFontSize.SelectedIndexChanged += ComboBoxFontSize_SelectedIndexChanged;
        }

        private void ComboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Получаем выбранный размер шрифта
            if (comboBoxFontSize.SelectedItem != null)
            {
                int fontSize = (int)comboBoxFontSize.SelectedItem;

                // Применяем размер шрифта к richTextBox1 и richTextBox2
                richTextBox1.Font = new Font(richTextBox1.Font.FontFamily, fontSize);
                //richTextBox2.Font = new Font(richTextBox2.Font.FontFamily, fontSize);
            }
        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            //AnalyzeCode();
            CheckSyntax();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
            //AnalyzeCode();
            CheckSyntax();
        }



        private void CheckSyntax()
        {
            // Получаем текст из редактора
            string code = richTextBox1.Text;

            // Лексический анализ
            var lexer = new LexicalAnalyzer();
            var tokens = lexer.Analyze(code);

            // Синтаксический анализ
            var parser = new SyntaxAnalyzer();
            var errors = parser.Analyze(tokens);

            // Вывод результатов в richTextBox2
            parser.PrintResultsToRichTextBox(errors, richTextBox2);

            //// Дополнительная информация о структуре
            //if (parser.IsStructCorrect(tokens))
            //{
            //    richTextBox2.AppendText(""\n\nДополнительная проверка: структура объявлена корректно."");
            //}
            //else if (errors.Count == 0)
            //{
            //    richTextBox2.AppendText(""\n\nДополнительная проверка: найдены проблемы в структуре (возможно, не хватает полей или скобки не сбалансированы)."");
            //}
        }

        private void постановкаЗадачиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @""Структуры в языке PHP -- это составные типы данных, которые позволяют
группировать переменные различных типов под одним именем. Для объявления
структуры и её инициализации в PHP используется синтаксис вида:

struct ИмяСтруктуры { тип $поле1; тип $поле2; ... тип $полеN; };

Примеры:

1. Структура Point -- описывает точку в двумерном пространстве с
   координатами x и y:, например: struct Point { float \$x; float \$y; };

2. Пустая структура -- может использоваться как заглушка для
   дальнейшего расширения: struct EmptyStruct {};

В связи с разработанной автоматной грамматикой G[<Struct>],
синтаксический анализатор (парсер) будет считать верными следующие
записи объявления структур:

1. struct User {string $name; int $age; };

2. struct Product { string $title; float $price; };

3. struct Config {};"";

            Form4 form4 = new Form4(""Постановка задачи"", content);
            form4.Show();
        }



        private void методАнализаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string content = @""Грамматика G[‹Def›] является автоматной.

Правила (1) -- (18) для G[‹Def›] реализованы на графе (см. рисунок 1).

Сплошные стрелки на графе характеризуют синтаксически верный разбор;
пунктирные символизируют состояние ошибки (ERROR);

Состояние 11 символизирует успешное завершение разбора."";

            Form4 form4 = new Form4(""Метод анализа"", content);
            form4.Show();
        }

";
            Form4 form4 = new Form4("Исходный код", content);
            form4.Show();
        }
    }
}
