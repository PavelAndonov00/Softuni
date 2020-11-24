namespace BookShop
{
    using BookShop.Initializer;
    using BookShop.Models.Enums;
    using Data;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                Console.WriteLine($"{RemoveBooks(db)} books were deleted");
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var sb = new StringBuilder();

            var ageRestriction = AgeRestriction.Minor;
            switch (command.ToLower())
            {
                case "teen":
                    ageRestriction = AgeRestriction.Teen;
                    break;
                case "adult":
                    ageRestriction = AgeRestriction.Adult;
                    break;
            }

            var titles = context
                .Books
                .Where(b => b.AgeRestriction == ageRestriction)
                .Select(b => b.Title)
                .OrderBy(t => t)
                .ToArray();

            sb.Append(string.Join(Environment.NewLine, titles));

            return sb.ToString().TrimEnd();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var titles = context
                .Books
                .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title);

            sb.Append(string.Join(Environment.NewLine, titles));

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            var sb = new StringBuilder();

            var titlesAndPrices = context
                .Books
                .Where(b => b.Price > 40)
                .OrderByDescending(b => b.Price)
                .Select(b => $"{b.Title} - ${b.Price:f2}");

            sb.Append(string.Join(Environment.NewLine, titlesAndPrices));

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            var sb = new StringBuilder();

            var titles = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .OrderBy(b => b.BookId)
                .Select(b => b.Title);

            sb.Append(string.Join(Environment.NewLine, titles));

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            string[] categories = input
                   .Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(c => c.ToLower())
                   .ToArray();
            var titles = context
                .Books
                .Where(b => b
                .BookCategories
                .Any(bc => categories.Contains(bc.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(t => t);

            sb.Append(string.Join(Environment.NewLine, titles));

            return sb.ToString().TrimEnd();
        }

        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            var sb = new StringBuilder();

            var releasedDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);


            var titles = context
                .Books
                .Where(b => b.ReleaseDate < releasedDate)
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => $"{b.Title} - {b.EditionType} - ${b.Price:f2}");

            sb.Append(string.Join(Environment.NewLine, titles));

            return sb.ToString().TrimEnd();

        }

        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var authors = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .Select(a => a.FirstName + " " + a.LastName)
                .OrderBy(a => a);

            sb.Append(string.Join(Environment.NewLine, authors));

            return sb.ToString().TrimEnd();

        }

        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var titles = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(t => t);

            sb.Append(string.Join(Environment.NewLine, titles));

            return sb.ToString().TrimEnd();

        }

        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            var sb = new StringBuilder();

            var titlesAndAuthors = context
                .Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
                .OrderBy(b => b.BookId)
                .Select(b => $"{b.Title} ({b.Author.FirstName + " " + b.Author.LastName})");

            sb.Append(string.Join(Environment.NewLine, titlesAndAuthors));

            return sb.ToString().TrimEnd();

        }

        public static int CountBooks(BookShopContext context, int lengthCheck)
        {
            return context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .Count();
        }

        public static string CountCopiesByAuthor(BookShopContext context)
        {
            var sb = new StringBuilder();

            var authorsAndCopies = context
                .Authors
                .OrderByDescending(a => a
                    .Books
                    .Sum(b => b.Copies))
                .Select(a =>
                    a.FirstName + " " + a.LastName + " - " + a
                    .Books
                    .Sum(b => b.Copies));

            sb.Append(string.Join(Environment.NewLine, authorsAndCopies));

            return sb.ToString().TrimEnd();

        }

        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            var sb = new StringBuilder();

            var categoryAndProfit = context
                .BooksCategories
                .GroupBy(bc => new { bc.Category }, (c, n) => new
                {
                    TotalProfit = n.Sum(b => b.Book.Price * b.Book.Copies),
                    CategoryName = c.Category.Name
                })
                .OrderByDescending(bc => bc.TotalProfit)
                .ThenBy(bc => bc.CategoryName)
                .Select(bc => $"{bc.CategoryName} ${bc.TotalProfit:f2}");

            sb.Append(string.Join(Environment.NewLine, categoryAndProfit));

            return sb.ToString().TrimEnd();

        }

        public static string GetMostRecentBooks(BookShopContext context)
        {
            var sb = new StringBuilder();

            var books = context
                .Categories
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    CategoryName = c.Name,
                    Books = c.
                    CategoryBooks
                    .Select(cb => cb.Book)
                    .OrderByDescending(cb => cb.ReleaseDate)
                    .Take(3)
                    .Select(cb => cb.Title + $" ({cb.ReleaseDate.Value.Year})")
                })
                .Select(c => $"--{c.CategoryName}{Environment.NewLine}{string.Join(Environment.NewLine, c.Books)}");

            sb.Append(string.Join(Environment.NewLine, books));

            return sb.ToString().TrimEnd();

        }

        public static void IncreasePrices(BookShopContext context)
        {
            context
                 .Books
                 .Where(b => b.ReleaseDate.Value.Year < 2010)
                 .ToList()
                 .ForEach(b => b.Price += 5);


            context.SaveChanges();
        }

        public static int RemoveBooks(BookShopContext context)
        {
            context
                .RemoveRange(context
                    .Books
                    .Where(b => b.Copies < 4200));

            var removedCount = context.SaveChanges();
            return removedCount;
        }
    }
}
