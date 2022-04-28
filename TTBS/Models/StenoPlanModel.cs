﻿using Microsoft.AspNetCore.Mvc.Rendering;
using TTBS.Core.Enums;

namespace TTBS.Models
{
    public class StenoPlanModel
    {
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public int StenoSayisi { get; set; }
        public int StenoSure { get; set; }
        public int UzmanStenoSure { get; set; }
        public int UzmanStenoSayisi { get; set; }
        public string GorevYeri { get; set; }
        public string GorevAd { get; set; }
        public SelectListItem BirlesimList { get; set; }
        public SelectListItem KomisyonList {get;set;}
        public SelectListItem GorevList { get; set; }

    }
}
