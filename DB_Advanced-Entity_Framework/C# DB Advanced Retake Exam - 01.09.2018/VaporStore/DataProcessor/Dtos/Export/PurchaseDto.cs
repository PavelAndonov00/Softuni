namespace VaporStore.DataProcessor.Dtos.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Purchase")]
    public class PurchaseDto
    {
        [XmlElement("Card")]
        public string CardNumber { get; set; }

        [XmlElement("Cvc")]
        public string Cvc { get; set; }

        [XmlElement("Date")]
        public string Date { get; set; }

        [XmlElement("Game")]
        public GameDto Game { get; set; }
    }
}