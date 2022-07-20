﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoGorevAtamaKomisyonModel
    {
        public List<Guid> StenografIds { get; set; }
        public Guid BirlesimId { get; set; }
    }
}
