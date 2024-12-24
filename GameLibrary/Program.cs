namespace GameLibrary;

internal class Program
{
    static void Main(string[] args)
    {
        using (var context = new GameLibraryContext())
        {
            // Створення бази даних
            context.Database.EnsureCreated();

            // Додавання ігор
            if (context.Games.Count() == 0)
            {
                context.Games.AddRange(
                    new Game
                    {
                        Name = "The Witcher 3: Wild Hunt",
                        Developer = "CD Projekt Red",
                        Genre = "RPG",
                        ReleaseDate = new DateTime(2015, 5, 19),
                        Mode = "Single-player",
                        CopiesSold = 40000000
                    },
                    new Game
                    {
                        Name = "Counter-Strike: Global Offensive",
                        Developer = "Valve",
                        Genre = "Shooter",
                        ReleaseDate = new DateTime(2012, 8, 21),
                        Mode = "Multiplayer",
                        CopiesSold = 25000000
                    },
                    new Game
                    {
                        Name = "Minecraft",
                        Developer = "Mojang Studios",
                        Genre = "Sandbox",
                        ReleaseDate = new DateTime(2011, 11, 18),
                        Mode = "Single-player, Multiplayer",
                        CopiesSold = 238000000
                    }
                );
                context.SaveChanges();
            }

            AddGame(context, new Game
            {
                Name = "New Game",
                Developer = "New Studio",
                Genre = "Action",
                ReleaseDate = DateTime.Now,
                Mode = "Single-player",
                CopiesSold = 100000
            });
            FindGameByName(context, "New Game");
            ShowTop3Games(context);

            // Відображення ігор
            Console.WriteLine("List of games in the database:");
            foreach (var game in context.Games)
            {
                Console.WriteLine($"Name: {game.Name}, " +
                    $"\nDeveloper: {game.Developer}, " +
                    $"\nGenre: {game.Genre}, " +
                    $"\nRelease Date: {game.ReleaseDate.ToShortDateString()}, " +
                    $"\nMode: {game.Mode}, " +
                    $"\nCopies Sold: {game.CopiesSold}\n");
            }
        }
    }

    // Пошук інформації за назвою гри
    static void FindGameByName(GameLibraryContext context, string name)
    {
        var game = context.Games.FirstOrDefault(g => g.Name == name);
        if (game != null)
        {
            Console.WriteLine($"Game found: {game.Name}, " +
                $"\nDeveloper: {game.Developer}, " +
                $"\nRelease: {game.ReleaseDate.ToShortDateString()}\n");
        }
        else
        {
            Console.WriteLine("Game not found.");
        }
    }

    // Пошук ігор за назвою студії
    static void FindGamesByDeveloper(GameLibraryContext context, string developer)
    {
        var games = context.Games.Where(g => g.Developer == developer).ToList();
        foreach (var game in games)
        {
            Console.WriteLine($"Name: {game.Name}, " +
                $"\nGenre: {game.Genre}, " +
                $"\nRelease: {game.ReleaseDate.ToShortDateString()}\n");
        }
    }

    // Пошук інформації за назвою студії та гри
    static void FindGameByNameAndDeveloper(GameLibraryContext context, string name, string developer)
    {
        var game = context.Games.FirstOrDefault(g => g.Name == name && g.Developer == developer);
        if (game != null)
        {
            Console.WriteLine($"Game found: {game.Name}, " +
                $"\nGenre: {game.Genre}, " +
                $"\nRelease: {game.ReleaseDate.ToShortDateString()}\n");
        }
        else
        {
            Console.WriteLine("Game not found.");
        }
    }

    // Пошук ігор за стилем
    static void FindGamesByGenre(GameLibraryContext context, string genre)
    {
        var games = context.Games.Where(g => g.Genre == genre).ToList();
        foreach (var game in games)
        {
            Console.WriteLine($"Name: {game.Name}, " +
                $"Developer: {game.Developer}, " +
                $"Release: {game.ReleaseDate.ToShortDateString()}");
        }
    }

    // Пошук ігор за роком релізу
    static void FindGamesByYear(GameLibraryContext context, int year)
    {
        var games = context.Games.Where(g => g.ReleaseDate.Year == year).ToList();
        foreach (var game in games)
        {
            Console.WriteLine($"Name: {game.Name}, " +
                $"Developer: {game.Developer}, " +
                $"Release: {game.ReleaseDate.ToShortDateString()}");
        }
    }

    // Відображення однокористувацьких ігор
    static void ShowSinglePlayerGames(GameLibraryContext context)
    {
        var games = context.Games.Where(g => g.Mode.Contains("Single-player")).ToList();
        foreach (var game in games)
        {
            Console.WriteLine($"Name: {game.Name}, " +
                $"Developer: {game.Developer}");
        }
    }

    // Відображення багатокористувацьких ігор
    static void ShowMultiplayerGames(GameLibraryContext context)
    {
        var games = context.Games.Where(g => g.Mode.Contains("Multiplayer")).ToList();
        foreach (var game in games)
        {
            Console.WriteLine($"Name: {game.Name}, " +
                $"Developer: {game.Developer}");
        }
    }

    // Гра з максимальною кількістю проданих копій
    static void ShowMostPopularGame(GameLibraryContext context)
    {
        var game = context.Games.OrderByDescending(g => g.CopiesSold).FirstOrDefault();
        Console.WriteLine($"The most popular game: {game.Name}, " +
            $"Copies sold: {game.CopiesSold}");
    }

    // Гра з мінімальною кількістю проданих копій
    static void ShowLeastPopularGame(GameLibraryContext context)
    {
        var game = context.Games.OrderBy(g => g.CopiesSold).FirstOrDefault();
        Console.WriteLine($"The least popular game: {game.Name}, " +
            $"Copies sold: {game.CopiesSold}");
    }

    // Топ-3 найпопулярніших ігор
    static void ShowTop3Games(GameLibraryContext context)
    {
        var games = context.Games.OrderByDescending(g => g.CopiesSold).Take(3).ToList();
        Console.WriteLine("Top 3 most popular games:");
        foreach (var game in games)
        {
            Console.WriteLine($"{game.Name}, Copies sold: {game.CopiesSold}");
        }
    }

    // Топ-3 найнепопулярніших ігор
    static void ShowBottom3Games(GameLibraryContext context)
    {
        var games = context.Games.OrderBy(g => g.CopiesSold).Take(3).ToList();
        Console.WriteLine("Top 3 least popular games:");
        foreach (var game in games)
        {
            Console.WriteLine($"{game.Name}, Copies sold: {game.CopiesSold}");
        }
    }

    static void AddGame(GameLibraryContext context, Game newGame)
    {
        if (context.Games.Any(g => g.Name == newGame.Name && g.Developer == newGame.Developer))
        {
            Console.WriteLine("The game already exists.");
            return;
        }
        context.Games.Add(newGame);
        context.SaveChanges();
        Console.WriteLine("The game has been successfully added.");
    }

    // Зміна даних гри
    static void UpdateGame(GameLibraryContext context, int gameId, Action<Game> updateAction)
    {
        var game = context.Games.FirstOrDefault(g => g.Id == gameId);
        if (game != null)
        {
            updateAction(game);
            context.SaveChanges();
            Console.WriteLine("Game data successfully changed.");
        }
        else
        {
            Console.WriteLine("Game not found.");
        }
    }

    // Видалення гри
    static void DeleteGame(GameLibraryContext context, string name, string developer)
    {
        var game = context.Games.FirstOrDefault(g => g.Name == name && g.Developer == developer);
        if (game != null)
        {
            Console.WriteLine($"Do you really want to uninstall the game {name}? (y/n)");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                context.Games.Remove(game);
                context.SaveChanges();
                Console.WriteLine("The game has been deleted.");
            }
        }
        else
        {
            Console.WriteLine("Game not found.");
        }
    }
}

