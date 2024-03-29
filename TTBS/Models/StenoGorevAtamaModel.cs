﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevAtamaModel
    {
        public IEnumerable<StenoKomisyonGrupModel> StenografIds { get; set; }
        public Guid BirlesimId { get; set; }
        public Guid OturumId { get; set; } = Guid.Empty;
        public int TurAdedi { get; set; }
        public StenoGorevTuru StenoGorevTuru { get; set; }
    }
}
