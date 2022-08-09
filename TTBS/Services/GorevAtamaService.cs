﻿using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Core.Extensions;
using TTBS.Core.Interfaces;
using TTBS.MongoDB;

namespace TTBS.Services
{
    public interface IGorevAtamaService
    {
        Birlesim CreateBirlesim(Birlesim birlesim);
        Guid CreateOturum(Oturum Oturum);
        void CreateStenoAtamaGK(List<GorevAtamaGKM> gorevAtamaGKMongoList);
        void CreateStenoAtamaKom(List<GorevAtamaKomM> gorevAtamaGKMongoList);
        Birlesim UpdateBirlesimGorevAtama(Guid birlesimId);
        void CreateBirlesimKomisyonRelation(Guid id, Guid komisyonId, Guid? altKomisyonId);
        void CreateBirlesimOzelToplanmaRelation(Guid id, Guid ozelToplanmaId);
        IEnumerable<Stenograf> GetStenografIdList();
        void AddStenoGorevAtamaKomisyon(List<Guid> stenografIds, string birlesimId, string oturumId);
    }
    public class GorevAtamaService : BaseService, IGorevAtamaService
    {
        private IRepository<Birlesim> _birlesimRepo;
        private IRepository<GorevAtama> _stenoGorevRepo;
        private IRepository<BirlesimKomisyon> _birlesimKomisyonRepo;
        private IRepository<BirlesimOzelToplanma> _birlesimOzeToplanmaRepo;
        private readonly IGorevAtamaGKMBusiness _gorevAtamaGKMRepo;
        private readonly IGorevAtamaKomMBusiness _gorevAtamaKomMRepo;
        private IRepository<Stenograf> _stenografRepo;
        private IRepository<Oturum> _oturumRepo;

        public GorevAtamaService(IRepository<Birlesim> birlesimRepo, 
                                 IRepository<GorevAtama> stenoGorevRepo,
                                 IGorevAtamaGKMBusiness gorevAtamaGKMRepo,
                                 IGorevAtamaKomMBusiness gorevAtamaKomMRepo,
                                 IRepository<BirlesimKomisyon> birlesimKomisyonRepo,
                                 IRepository<BirlesimOzelToplanma> birlesimOzeToplanmaRepo,
                                 IRepository<Stenograf> stenografRepo,
                                 IRepository<Oturum> oturumRepo,
                                 IServiceProvider provider) : base(provider)
        {
            _birlesimRepo=birlesimRepo;
            _stenoGorevRepo = stenoGorevRepo;
            _gorevAtamaGKMRepo = gorevAtamaGKMRepo;
            _gorevAtamaKomMRepo= gorevAtamaKomMRepo;
            _birlesimKomisyonRepo = birlesimKomisyonRepo;
            _birlesimOzeToplanmaRepo = birlesimOzeToplanmaRepo;
            _birlesimOzeToplanmaRepo = birlesimOzeToplanmaRepo;
            _stenografRepo = stenografRepo;
            _oturumRepo = oturumRepo;
        }
        public Birlesim CreateBirlesim(Birlesim birlesim)
        {
            _birlesimRepo.Create(birlesim, CurrentUser.Id);
            _birlesimRepo.Save();

            return birlesim;  
        }
        public Guid CreateOturum(Oturum oturum)
        {
            var otr = _oturumRepo.Get(x => x.BirlesimId == oturum.BirlesimId);
            if (otr != null && otr.Count() > 0)
                oturum.OturumNo = otr.Max(x => x.OturumNo) + 1;
            _oturumRepo.Create(oturum, CurrentUser.Id);
            _oturumRepo.Save();
            return oturum.Id;
        }

        public void CreateStenoAtamaGK(List<GorevAtamaGKM> gorevAtamaGKMongoList)
        {
             var result = _gorevAtamaGKMRepo.AddRangeAsync(gorevAtamaGKMongoList);
        }
        public void CreateStenoAtamaKom(List<GorevAtamaKomM> gorevAtamaMongoList)
        {
            var result = _gorevAtamaKomMRepo.AddRangeAsync(gorevAtamaMongoList);
        }
        public void CreateBirlesimKomisyonRelation(Guid id, Guid komisyonId, Guid? altKomisyonId)
        {
            _birlesimKomisyonRepo.Create(new BirlesimKomisyon { BirlesimId = id, KomisyonId = komisyonId, AltKomisyonId = altKomisyonId });
            _birlesimKomisyonRepo.Save();
        }
        public void CreateBirlesimOzelToplanmaRelation(Guid id, Guid ozelToplanmaId)
        {
            _birlesimOzeToplanmaRepo.Create(new BirlesimOzelToplanma { BirlesimId = id, OzelToplanmaId = ozelToplanmaId });
            _birlesimOzeToplanmaRepo.Save();
        }

        public Birlesim UpdateBirlesimGorevAtama(Guid birlesimId)
        {
            var birlesim = _birlesimRepo.GetById(birlesimId);
            birlesim.ToplanmaDurumu = ToplanmaStatu.Planlandı;
            birlesim.TurAdedi = birlesim.TurAdedi;
            _birlesimRepo.Update(birlesim);
            _birlesimRepo.Save();
            return birlesim;
        }

        public IEnumerable<Stenograf> GetStenografIdList()
        {
           return _stenografRepo.Get().OrderBy(x => x.SiraNo).Select(x => new Stenograf { Id =x.Id,StenoGorevTuru =x.StenoGorevTuru});
        }
        public void AddStenoGorevAtamaKomisyon(List<Guid> stenografIds, string birlesimId, string oturumId)
        {
            var stenoList = _gorevAtamaKomMRepo.Get(x => x.BirlesimId == birlesimId).OrderBy(x => x.GorevBasTarihi).ToList();
            //var stenoList = _stenoGorevRepo.Get(x => x.BirlesimId == birlesimId,  includeProperties: "Stenograf,Birlesim").OrderBy(x => x.GorevBasTarihi).ToList();
            //if (stenoList != null && stenoList.Count() > 0)
            //{

            //    var lastSteno = stenoList.Where(x=> x.StenografId == stenoList.LastOrDefault().StenografId);
            //    stenoList.ForEach(x => 
            //    {    
            //         x.GorevBasTarihi.Value.AddMinutes(lastSteno.FirstOrDefault().StenoSure);
            //         x.GorevBitisTarihi.Value.AddMinutes(x.StenoSure+ lastSteno.FirstOrDefault().StenoSure);
            //    });
            //    stenoList.Add(new GorevAtamaKomM
            //    {
            //        StenografId = stenografIds.FirstOrDefault().ToString(),
            //        BirlesimId = birlesimId,
            //        GorevBasTarihi = lastSteno.First().GorevBitisTarihi,
            //        GorevBitisTarihi = lastSteno.First().GorevBitisTarihi.Value.AddMinutes(lastSteno.FirstOrDefault().StenoSure)
            //    });
            //    var grpListCnt = stenoList.GroupBy(c => new
            //    {
            //        c.StenografId,
            //    }).Count();


            //    var atamaList = new List<GorevAtamaKomM>();
            //    for (int i = 1; i <= stenoList.Count() / grpListCnt; i++)
            //    {
            //        var grpList = stenoList.Take(i * grpListCnt);
            //        var maxDate = grpList.Max(x => x.GorevBitisTarihi);
            //        var maxSure = grpList.Max(x => x.StenoSure);
            //        int firstRec = 0;
            //        foreach (var item in entity.StenografIds)
            //        {
            //            var newEntity = new GorevAtamaKomM();
            //            newEntity.BirlesimId = entity.BirlesimId.ToString();
            //            newEntity.OturumId =
            //            newEntity.StenografId = item.ToString();
            //            newEntity.GorevBasTarihi = maxDate.Value.AddMinutes(firstRec * maxSure).ToLongDateString();
            //            newEntity.GorevBitisTarihi = maxDate.Value.AddMinutes((firstRec * maxSure) + maxSure).ToLongDateString();
            //            newEntity.StenoSure = maxSure;
            //            var durum = maxDate.Value > stenoList.FirstOrDefault().Birlesim.BaslangicTarihi.Value ? GorevStatu.GorevZamanAsim : GorevStatu.Planlandı;
            //            if (durum != GorevStatu.GorevZamanAsim)
            //                newEntity.GorevStatu = GorevStatu.GidenGrup; // (GetStenoGidenGrupDurum(item) == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı);
            //            else
            //                newEntity.GorevStatu = durum;
            //            atamaList.Add(newEntity);
            //            firstRec++;
            //            maxDate = maxDate.Value.AddMinutes(firstRec * maxSure);
            //            var upateList = stenoList.Where(x => x.GorevBasTarihi >= maxDate);
            //            if (upateList != null && upateList.Count() > 0)
            //            {
            //                var firstRecord = upateList.OrderBy(x => x.GorevBasTarihi).FirstOrDefault();
            //                var firstDate = firstRecord.GorevBasTarihi.Value.AddMinutes(firstRecord.StenoSure);
            //                foreach (var hedef in upateList)
            //                {
            //                    hedef.GorevBasTarihi = firstDate;
            //                    hedef.GorevBitisTarihi = hedef.GorevBasTarihi.Value.AddMinutes(hedef.StenoSure);
            //                    //hedef.GorevStatu = hedef.Stenograf.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && hedef.GorevBasTarihi.Value.AddMinutes(9 * hedef.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
            //                    firstDate = hedef.GorevBitisTarihi.Value;
            //                    _stenoGorevRepo.Update(hedef);
            //                    _stenoGorevRepo.Save();
            //                }
            //            }
            //        }
            //    }
            //    _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
            //    _stenoGorevRepo.Save();

            //    //UpdateGidenGrup(atamaList);
            //}
            //else if (entity.StenografIds != null && entity.StenografIds.Count > 0)
            //{
            //    var birlesim = _birlesimRepo.GetById(entity.BirlesimId);
            //    if (birlesim != null)
            //    {
            //        var minDate = birlesim.BaslangicTarihi;
            //        var atamaList = new List<GorevAtamaKomM>();
            //        int firstRec = 1;
            //        foreach (var item in entity.StenografIds)
            //        {
            //            var newEntity = new GorevAtamaKomM();
            //            newEntity.BirlesimId = entity.BirlesimId;
            //            newEntity.OturumId = entity.OturumId;
            //            newEntity.StenografId = item;
            //            newEntity.GorevBasTarihi = minDate.Value;
            //            newEntity.GorevBitisTarihi = newEntity.GorevBasTarihi.Value.AddMinutes(firstRec * birlesim.StenoSure);
            //            newEntity.StenoSure = birlesim.StenoSure;
            //            newEntity.GorevStatu = GorevStatu.GidenGrup; //GetStenoGidenGrupDurum(item) == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
            //            atamaList.Add(newEntity);
            //            minDate = newEntity.GorevBitisTarihi;
            //            firstRec++;
            //        }
            //        _stenoGorevRepo.Create(atamaList, CurrentUser.Id);
            //        _stenoGorevRepo.Save();

            //        // UpdateGidenGrup(atamaList);
            //    }

            }
        }
 
    }


