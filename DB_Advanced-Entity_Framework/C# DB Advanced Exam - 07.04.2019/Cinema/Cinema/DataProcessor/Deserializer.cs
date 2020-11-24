namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.Data.Models.Enums;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";
        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";
        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";
        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";
        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var movieDtos = JsonConvert.DeserializeObject<MovieDto[]>(jsonString);
            var validMovies = new List<Movie>();
            foreach (var movieDto in movieDtos)
            {
                var movieTitleAlreadyExist = validMovies.Any(vm => vm.Title == movieDto.Title);
                if (!IsValid(movieDto) || movieTitleAlreadyExist)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Genre genre;
                if (!Enum.TryParse(movieDto.Genre, out genre))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var movie = new Movie(movieDto.Title, genre, movieDto.Duration, movieDto.Rating, movieDto.Director);
                validMovies.Add(movie);

                var formatedRating = decimal.Parse(movieDto.Rating.ToString("F2"));
                sb.AppendLine(string.Format(SuccessfulImportMovie, movieDto.Title, movieDto.Genre, formatedRating));
            }

            context.Movies.AddRange(validMovies);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var hallDtos = JsonConvert.DeserializeObject<HallDto[]>(jsonString);

            var validHalls = new List<Hall>();

            foreach (var hallDto in hallDtos)
            {
                if (!IsValid(hallDto) || hallDto.Seats < 1)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var hallSeats = new List<Seat>();
                for(int i = 0; i < hallDto.Seats; i++)
                {
                    hallSeats.Add(new Seat());
                }

                var hall = new Hall
                {
                    Name = hallDto.Name,
                    Is4Dx = hallDto.Is4Dx,
                    Is3D = hallDto.Is3D,
                    Seats = hallSeats
                };

                validHalls.Add(hall);

                var projectionType = hall.Is4Dx ? "4Dx" : hall.Is3D ? "3D" : "Normal";
                projectionType = projectionType == "4Dx" ? hall.Is3D ? projectionType + "/3D" : "4Dx" : hall.Is3D ? "3D" : "Normal";
                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projectionType, hall.Seats.Count));
            }

            context.Halls.AddRange(validHalls);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ProjectionDto[]), new XmlRootAttribute("Projections"));
            var projectionDtos = (ProjectionDto[])serializer.Deserialize(new StringReader(xmlString));

            var validProjections = new List<Projection>();
            
            foreach (var projectionDto in projectionDtos)
            {
                var hall = context.Halls.FirstOrDefault(h => h.Id == projectionDto.HallId);
                var movie = context.Movies.FirstOrDefault(m => m.Id == projectionDto.MovieId);
                if (!IsValid(projectionDto) || hall == null || movie == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var projection = new Projection
                {
                    Hall = hall,
                    Movie = movie,
                    DateTime = DateTime.Parse(projectionDto.DateTime)
                };

                validProjections.Add(projection);

                var parsedDate = projection.DateTime.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                sb.AppendLine(string.Format(SuccessfulImportProjection, movie.Title, parsedDate));
            }

            context.Projections.AddRange(validProjections);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(CustomerDto[]), new XmlRootAttribute("Customers"));
            var customerDtos = (CustomerDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            var validCustomers = new List<Customer>();

            foreach (var customerDto in customerDtos)
            {
                if (!IsValid(customerDto) || !customerDto.Tickets.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var customer = new Customer
                {
                    FirstName = customerDto.FirstName,
                    LastName = customerDto.LastName,
                    Age = customerDto.Age,
                    Balance = customerDto.Balance,
                    Tickets = customerDto.Tickets.Select(t => new Ticket
                    {
                        ProjectionId = t.ProjectionId,
                        Price = t.Price
                    })
                        .ToList()
                };

                validCustomers.Add(customer);

                sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName, customer.LastName, customer.Tickets.Count));
            }

            context.Customers.AddRange(validCustomers);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var result = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return result;
        }
    }
}