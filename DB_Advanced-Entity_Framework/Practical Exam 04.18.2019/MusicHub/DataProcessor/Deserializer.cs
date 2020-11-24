namespace MusicHub.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using MusicHub.Data.Models;
    using MusicHub.Data.Models.Enums;
    using MusicHub.DataProcessor.ImportDtos;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data";

        private const string SuccessfullyImportedWriter 
            = "Imported {0}";
        private const string SuccessfullyImportedProducerWithPhone 
            = "Imported {0} with phone: {1} produces {2} albums";
        private const string SuccessfullyImportedProducerWithNoPhone
            = "Imported {0} with no phone number produces {1} albums";
        private const string SuccessfullyImportedSong 
            = "Imported {0} ({1} genre) with duration {2}";
        private const string SuccessfullyImportedPerformer
            = "Imported {0} ({1} songs)";

        public static string ImportWriters(MusicHubDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var writers = JsonConvert.DeserializeObject<Writer[]>(jsonString);

            var validWriters = new List<Writer>();

            foreach (var writer in writers)
            {
                if (!IsValid(writer))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                validWriters.Add(writer);
                sb.AppendLine(string.Format(SuccessfullyImportedWriter, writer.Name));
            }

            context.Writers.AddRange(validWriters);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportProducersAlbums(MusicHubDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var producersDto = JsonConvert.DeserializeObject<ProducerDto[]>(jsonString);

            var validProducers = new List<Producer>();

            foreach (var producerDto in producersDto)
            {
                if(!IsValid(producerDto) || !producerDto.Albums.All(IsValid))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var producer = new Producer
                {
                    Albums = producerDto.Albums.Select(a => new Album
                    {
                        Name = a.Name,
                        ReleaseDate = DateTime.ParseExact(a.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)
                    })
                        .ToArray(),
                    Name = producerDto.Name,
                    PhoneNumber = producerDto.PhoneNumber,
                    Pseudonym = producerDto.Pseudonym
                };

                validProducers.Add(producer);

                if(producer.PhoneNumber != null)
                {
                    sb.AppendLine(string.Format(SuccessfullyImportedProducerWithPhone, producer.Name, producer.PhoneNumber, producer.Albums.Count));
                }
                else
                {
                    sb.AppendLine(string.Format(SuccessfullyImportedProducerWithNoPhone, producer.Name, producer.Albums.Count));
                }
            }

            context.Producers.AddRange(validProducers);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportSongs(MusicHubDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(SongDto[]), new XmlRootAttribute("Songs"));
            var songDtos = (SongDto[])serializer.Deserialize(new StringReader(xmlString));

            var validSongs = new List<Song>();
            foreach (var songDto in songDtos)
            {
                if (!IsValid(songDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Genre genre;
                if(!Enum.TryParse(songDto.Genre, out genre))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var album = context.Albums.SingleOrDefault(a => a.Id == songDto.AlbumId);
                if(album == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var writer = context.Writers.SingleOrDefault(w => w.Id == songDto.WriterId);
                if (writer == null)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var song = new Song
                {
                    Name = songDto.Name,
                    CreatedOn = DateTime.ParseExact(songDto.CreatedOn, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Album = album,
                    Duration = TimeSpan.ParseExact(songDto.Duration, "c", CultureInfo.InvariantCulture),
                    Price = songDto.Price,
                    Genre = genre,
                    Writer = writer
                };

                validSongs.Add(song);

                sb.AppendLine(string.Format(SuccessfullyImportedSong, song.Name, songDto.Genre, song.Duration));
            }

            context.Songs.AddRange(validSongs);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportSongPerformers(MusicHubDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(PerformerDto[]), new XmlRootAttribute("Performers"));
            var performerDtos = (PerformerDto[])serializer.Deserialize(new StringReader(xmlString));

            var validPerformers = new List<Performer>();
            foreach (var performerDto in performerDtos)
            {
                if (!IsValid(performerDto) || !performerDto.PerformersSongs.All(ps => context.Songs.Select(s => s.Id).Contains(ps.SongId)))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var performer = new Performer
                {
                    FirstName = performerDto.FirstName,
                    LastName = performerDto.LastName,
                    Age = performerDto.Age,
                    NetWorth = performerDto.NetWorth
                };

                foreach (var performerSong in performerDto.PerformersSongs)
                {
                    performer.PerformerSongs.Add(new SongPerformer
                    {
                        SongId = performerSong.SongId
                    });
                }

                validPerformers.Add(performer);

                sb.AppendLine(string.Format(SuccessfullyImportedPerformer, performer.FirstName, performer.PerformerSongs.Count));
            }

            context.Performers.AddRange(validPerformers);
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