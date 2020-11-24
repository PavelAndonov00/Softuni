namespace VaporStore.DataProcessor.Dtos.Export
{
    using System.Xml.Serialization;

    [XmlType("Game")]
    public class GameDto
    {
        [XmlAttribute("title")]
        public string GameName { get; set; }

        [XmlElement("Genre")]
        public string GenreName { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}