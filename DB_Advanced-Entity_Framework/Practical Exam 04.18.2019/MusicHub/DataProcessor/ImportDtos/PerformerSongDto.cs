﻿using System.Xml.Serialization;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class PerformerSongDto
    {
        [XmlAttribute("id")]
        public int SongId { get; set; }
    }
}