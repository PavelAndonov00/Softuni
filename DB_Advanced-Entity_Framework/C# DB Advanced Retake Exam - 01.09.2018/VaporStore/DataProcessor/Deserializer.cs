namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dtos;
    using VaporStore.DataProcessor.Dtos.Import;

    public static class Deserializer
	{
        private const string errorMsg = "Invalid Data";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var gameDtos = JsonConvert.DeserializeObject<GameDto[]>(jsonString);

            var developers = new List<Developer>();
            var genres = new List<Genre>();
            var tags = new List<Tag>();
            var games = new List<Game>();
            foreach (var gameDto in gameDtos)
            {
                if (!IsValid(gameDto) || !gameDto.Tags.All(IsValid))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var developer = developers.SingleOrDefault(d => d.Name == gameDto.DeveloperName);
                if (developer == null)
                {
                    developer = new Developer
                    {
                        Name = gameDto.DeveloperName
                    };
                    developers.Add(developer);
                }

                var genre = genres.SingleOrDefault(g => g.Name == gameDto.GenreName);
                if (genre == null)
                {
                    genre = new Genre
                    {
                        Name = gameDto.GenreName
                    };
                    genres.Add(genre);
                }

                var gameTags = new List<GameTag>();
                foreach (var gameDtoTagName in gameDto.Tags)
                {
                    var tag = tags.SingleOrDefault(t => t.Name == gameDtoTagName);
                    if (tag == null)
                    {
                        tag = new Tag
                        {
                            Name = gameDtoTagName
                        };
                        tags.Add(tag);
                    }

                    var gameTag = new GameTag
                    {
                        Tag = tag
                    };
                    gameTags.Add(gameTag);
                }

                var game = new Game
                {
                    Name = gameDto.Name,
                    Price = gameDto.Price,
                    Developer = developer,
                    Genre = genre,
                    GameTags = gameTags,
                    ReleaseDate = gameDto.ReleaseDate
                };
                games.Add(game);

                sb.AppendLine($"Added {game.Name} ({game.Genre.Name}) with {game.GameTags.Count} tags");
            }



            context.Games.AddRange(games);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }


        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
            var sb = new StringBuilder();

            var usersDtos = JsonConvert.DeserializeObject<UserDto[]>(jsonString);

            var users = new List<User>();
            foreach (var userDto in usersDtos)
            {
                if(!IsValid(userDto) || !userDto.Cards.All(IsValid))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var cards = userDto.Cards
                    .Select(c => new Card
                    {
                        Number = c.Number,
                        Type = c.Type,
                        Cvc = c.Cvc
                    })
                    .ToArray();

                var user = new User
                {
                    Age = userDto.Age,
                    Email = userDto.Email,
                    FullName = userDto.FullName,
                    Cards = cards,
                    Username = userDto.Username
                };

                users.Add(user);

                sb.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }

            context.Users.AddRange(users);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(PurchaseDto[]), new XmlRootAttribute("Purchases"));

            var purchasesDto = (PurchaseDto[])serializer.Deserialize(new StringReader(xmlString));

            var purchases = new List<Purchase>();
            foreach (var purchaseDto in purchasesDto)
            {
                if (!IsValid(purchaseDto))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var card = context.Cards.Include(c => c.User).SingleOrDefault(c => c.Number == purchaseDto.Card);
                var game = context.Games.SingleOrDefault(g => g.Name == purchaseDto.Title);

                var purchase = new Purchase
                {
                    Card = card,
                    Game = game,
                    ProductKey = purchaseDto.Key,
                    Date = DateTime.ParseExact(purchaseDto.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                    Type = purchaseDto.Type
                };
                purchases.Add(purchase);

                sb.AppendLine($"Imported {purchase.Game.Name} for {purchase.Card.User.Username}");
            }

            context.Purchases.AddRange(purchases);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
		}

        private static bool IsValid(this object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
	}
}