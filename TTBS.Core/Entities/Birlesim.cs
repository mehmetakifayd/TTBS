﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TTBS.Core.BaseEntities;
using TTBS.Core.Enums;

namespace TTBS.Core.Entities
{
    public class Birlesim : BaseEntity
    {
        public Guid Id { get; set; }    
        public string BirlesimNo { get; set; }         
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public Yasama Yasama { get; set; }
        public Guid YasamaId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double StenoSure { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public double UzmanStenoSure { get; set; }
        public string Yeri { get; set; } = String.Empty;
        public ToplanmaTuru ToplanmaTuru { get; set; }
        public int TurAdedi { get; set; } = 3;
        public ToplanmaStatu ToplanmaDurumu { get; set; }
        public virtual ICollection<Oturum> Oturums { get; set; }
        public Guid KomisyonId { get; set; } 
        public Guid? AltKomisyonId { get; set; } 
        public Guid OzelToplanmaId { get; set; }
        public Komisyon Komisyon { get; set; }
        public AltKomisyon AltKomisyon { get; set; }  
        public OzelToplanma OzelToplanma { get; set; }
    }
}
