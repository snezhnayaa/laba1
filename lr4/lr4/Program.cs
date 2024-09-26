using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary
{
    // Перечисляемый тип для издательств
    enum Publisher
    {
        Издатель1,
        Издатель2,
        Издатель3
    }

    // Структура для хранения информации о книге
    struct Book
    {
        public int number;
        public string Author;
        public string Title;
        public Publisher Publisher;
        public int Year;
        public int PageCount;
    }

    class Program
    {
        static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            bool ContinueWorking = true;

            while (ContinueWorking)
            {
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("1. Добавить новую книгу");
                Console.WriteLine("2. Удалить книгу по индексу");
                Console.WriteLine("3. Вывести все книги");
                Console.WriteLine("4. Обновить информацию о книге по автору");
                Console.WriteLine("5. Найти книги по издательству");
                Console.WriteLine("6. Рассчитать средний возраст книги");
                Console.WriteLine("7. Выйти");
                Console.WriteLine("----------------------------------------------------");
                Console.Write("Введите ваш выбор: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddBook();
                        break;
                    case 2:
                        DeleteBook();
                        break;
                    case 3:
                        ListOfBooks();
                        break;
                    case 4:
                        UpdateBook();
                        break;
                    case 5:
                        SearchByPublisher();
                        break;
                    case 6:
                        AverageAgeOfBooks();
                        break;
                    case 7:
                        ContinueWorking = false;
                        break;
                    default:
                        Console.WriteLine("Недопустимый выбор. Пожалуйста, попробуйте снова.");
                        break;
                }
            }
        }

        // Метод для добавления новой книги
        static void AddBook()
        {
            Book newBook = new Book();

            Console.WriteLine();
            Console.Write("Введите инвентарный номер: ");
            newBook.number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите автора: ");
            newBook.Author = Console.ReadLine();
            Console.Write("Введите название: ");
            newBook.Title = Console.ReadLine();
            Console.Write("Введите издательство (0 для Издатель1, 1 для Издатель2, 2 для Издатель3): ");
            newBook.Publisher = (Publisher)Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите год издания: ");
            newBook.Year = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество страниц: ");
            newBook.PageCount = Convert.ToInt32(Console.ReadLine());

            books.Add(newBook);
        }

        // Метод для удаления книги из списка по индексу
        static void DeleteBook()
        {
            Console.WriteLine();
            Console.Write("Введите инвентарный номер книги для удаления: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Book foundBook = books.Find(b => b.number == number);

            if (foundBook.Author != null)
            {
                books.Remove(foundBook);
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }

        // Метод для вывода списка всех книг
        static void ListOfBooks()
        {
            foreach (var book in books)
            {
                Console.WriteLine();
                Console.WriteLine($"Инвентарный Номер: {book.number}");
                Console.WriteLine($"Автор: {book.Author}");
                Console.WriteLine($"Название: {book.Title}");
                Console.WriteLine($"Издательство: {book.Publisher}");
                Console.WriteLine($"Год издания: {book.Year}");
                Console.WriteLine($"Количество Страниц: {book.PageCount}");
                Console.WriteLine();
            }
        }

        // Метод для обновления информации о книге
        static void UpdateBook()
        {
            Console.Write("Введите автора книги для обновления: ");
            string author = Console.ReadLine();

            bool bookFound = books.Any(b => b.Author == author);

            if (bookFound)
            {
                Book foundBook = books.Find(b => b.Author == author);
                Console.WriteLine();
                Console.WriteLine("Книга найдена. Введите новую информацию:");
                Console.Write("Введите нового автора: ");
                foundBook.Author = Console.ReadLine();
                Console.Write("Введите новое название: ");
                foundBook.Title = Console.ReadLine();
                Console.Write("Введите новое издательство (0 для Издатель1, 1 для Издатель2, 2 для Издатель3): ");
                foundBook.Publisher = (Publisher)Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите новый год издания: ");
                foundBook.Year = Convert.ToInt32(Console.ReadLine());
                Console.Write("Введите новое количество страниц: ");
                foundBook.PageCount = Convert.ToInt32(Console.ReadLine());

                // Сохраняем изменения в исходном списке
                int index = books.FindIndex(b => b.Author == author);
                books[index] = foundBook;

                Console.WriteLine("Информация о книге обновлена.");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Книга не найдена.");
            }
        }

        // Метод для поиска книги по издательству
        static void SearchByPublisher()
        {
            Console.Write("Введите издательство для поиска (0 для Издатель1, 1 для Издатель2, 2 для Издатель3): ");
            Publisher Publisher = (Publisher)Convert.ToInt32(Console.ReadLine());

            List<Book> ByPublisher = books.Where(b => b.Publisher == Publisher).ToList();

            if (ByPublisher.Count > 0)
            {
                Console.WriteLine("Книги выбранного издательства:");
                foreach (var book in ByPublisher)
                {
                    Console.WriteLine($"Автор: {book.Author}, Название: {book.Title}");
                }
            }
            else
            {
                Console.WriteLine("Книги выбранного издательства не найдены.");
            }
        }

        // Метод для расчета среднего возраста книг
        static void AverageAgeOfBooks()
        {
            int thisYear = DateTime.Now.Year;
            int totalAge = 0;
            int count = 0;

            foreach (var book in books)
            {
                totalAge += thisYear - book.Year;
                count++;
            }

            if (count > 0)
            {
                double AverageAge = (double)totalAge / count;
                Console.WriteLine($"Средний возраст имеющихся книг: {AverageAge:F2} лет");
            }
            else
            {
                Console.WriteLine("Нет книг в списке.");
            }
        }
    }
}