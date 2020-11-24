namespace SoftJail.DataProcessor
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string errorMsg = "Invalid Data";

        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var departmentDtos = JsonConvert.DeserializeObject<DepartmentDto[]>(jsonString);

            var validDepartments = new List<Department>();
            foreach (var departmentDto in departmentDtos)
            {
                if (!IsValid(departmentDto) || !departmentDto.Cells.All(IsValid))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var department = new Department
                {
                    Name = departmentDto.Name,
                    Cells = departmentDto.Cells.AsQueryable().ProjectTo<Cell>().ToList()
                };

                validDepartments.Add(department);

                sb.AppendLine($"Imported {department.Name} with {department.Cells.Count} cells");
            }

            context.Departments.AddRange(validDepartments);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var prisonerDtos = JsonConvert.DeserializeObject<PrisonerDto[]>(jsonString);

            var validPrisoners = new List<Prisoner>();
            foreach (var prisonerDto in prisonerDtos)
            {
                if (!IsValid(prisonerDto) || !prisonerDto.Mails.All(IsValid))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var parsedIncarcerationDate = DateTime.ParseExact(prisonerDto.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                DateTime? parsedReleaseDate = null;
                if (prisonerDto.ReleaseDate != null)
                {
                    parsedReleaseDate = DateTime.ParseExact(prisonerDto.ReleaseDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                var mails = prisonerDto.Mails.AsQueryable().ProjectTo<Mail>().ToList();

                var prisoner = new Prisoner
                {
                    FullName = prisonerDto.FullName,
                    Nickname = prisonerDto.Nickname,
                    Age = prisonerDto.Age,
                    IncarcerationDate = parsedIncarcerationDate,
                    ReleaseDate = parsedReleaseDate,
                    Bail = prisonerDto.Bail,
                    CellId = prisonerDto.CellId,
                    Cell = prisonerDto.Cell,
                    Mails = mails
                };

                validPrisoners.Add(prisoner);

                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
            }

            context.Prisoners.AddRange(validPrisoners);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(OfficerDto[]), new XmlRootAttribute("Officers"));
            var officerDtos = (OfficerDto[])serializer.Deserialize(new StringReader(xmlString));

            var validOfficers = new List<Officer>();
            foreach (var officerDto in officerDtos)
            {
                if (!IsValid(officerDto))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                Position position;
                if (!Enum.TryParse(officerDto.Position, out position))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                Weapon weapon;
                if (!Enum.TryParse(officerDto.Weapon, out weapon))
                {
                    sb.AppendLine(errorMsg);
                    continue;
                }

                var officer = new Officer
                {
                    Position = position,
                    Weapon = weapon,
                    DepartmentId = officerDto.DepartmentId,
                    FullName = officerDto.Name,
                    Salary = officerDto.Money
                };
                
                var validPrisoners = officerDto
                    .Prisoners
                    .AsQueryable()
                    .ProjectTo<OfficerPrisoner>()
                    .ToList();
                officer.OfficerPrisoners = validPrisoners;
                validOfficers.Add(officer);

                sb.AppendLine($"Imported {officer.FullName} ({validPrisoners.Count} prisoners)");
            }

            context.Officers.AddRange(validOfficers);
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