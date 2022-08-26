﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class GorevAtamalarKOM : BaseEntity 
    {
        public Guid Id { get; set; }
        public int SatırNo { get; set; }
        public Guid BirlesimId { get; set; }
        public Guid StenografId { get; set; }
        public Guid OturumId { get; set; }
        public DateTime? GorevBasTarihi { get; set; }
        public DateTime? GorevBitisTarihi { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StenoSure { get; set; }
        public GorevStatu GorevStatu { get; set; }
        public string KomisyonAd { get; set; }
        public IzınTuru StenoIzinTuru { get; set; }
        public int SureAsmaVar { get; set; }
        public string GidenGrupSaat { get; set; }
        public int GidenGrupSaatUygula { get; set; }
        public string StenoAdSoyad { get; set; }
    }
}