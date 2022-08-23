﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StenografController : BaseController<StenografController>
    {
        private readonly IStenografService _stenoService;
        private readonly IGlobalService _globalService;
        //private readonly IMongoDBService _mongoDBService;
        private readonly ILogger<StenografController> _logger;
        public readonly IMapper _mapper;

        public StenografController(IStenografService stenoService, IGlobalService globalService, ILogger<StenografController> logger, IMapper mapper)
        {
            _stenoService = stenoService;
            _globalService = globalService;
            //_mongoDBService = mongoDBService;
            _logger = logger;
            _mapper = mapper;
        }
        #region KomisyonToplanma

        //[HttpGet("GetStenoPlanByStatus")]
        //public List<StenoPlanModel> GetStenoPlanByStatus(int status=0)
        //{
        //    var stenoEntity = _stenoService.GetStenoPlanByStatus(status);
        //    var model = _mapper.Map<List<StenoPlanModel>>(stenoEntity);
        //    return model;
        //}

        [HttpGet("GetBirlesimByDateAndTur")]
        public List<BirlesimViewModel> GetBirlesimByDateAndTur(DateTime gorevTarihi, DateTime gorevBitTarihi, int toplanmaTuru)
        {
            var stenoEntity = _stenoService.GetBirlesimByDateAndTur(gorevTarihi, gorevBitTarihi, toplanmaTuru);
            var model = _mapper.Map<List<BirlesimViewModel>>(stenoEntity);
            return model;
        }
        [HttpGet("GetBirlesimByDate")]
        public List<BirlesimViewModel> GetBirlesimByDate(DateTime gorevTarihi, int gorevTuru)
        {
            var stenoEntity = _stenoService.GetBirlesimByDate(gorevTarihi, gorevTuru);
            var model = _mapper.Map<List<BirlesimViewModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup")]
        public StenoGroupStatisticsModel GetStenoWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grupId)
        {
            StenoGroupStatisticsModel model = new StenoGroupStatisticsModel();
            var istatistikEntity = _globalService.GetGrupToplamSureByDate(grupId, baslangic, bitis, yasamaId);
            model.stenoToplamGenelSureModels = _mapper.Map<List<StenoToplamGenelSureModel>>(istatistikEntity);
            return model;
        }

        [HttpGet("GetUzmanWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup")]
        public StenoGroupStatisticsModel GetUzmanWeeklyStatisticstKomisyonAndBirlesimByDateAndGroup(DateTime? baslangic, DateTime? bitis, Guid? yasamaId, Guid grupId)
        {
            StenoGroupStatisticsModel model = new StenoGroupStatisticsModel();
            var istatistikEntity = _globalService.GetGrupToplamSureByDate(grupId, baslangic, bitis, yasamaId);
            // TODO : Burada uzman stenografların okuduğu sayfa sayısı de eklenecek editör yapıldıktan sonra kararlaştırılıcak.
            model.stenoToplamGenelSureModels = _mapper.Map<List<StenoToplamGenelSureModel>>(istatistikEntity);
            return model;
        }


        //[HttpGet("GetIntersectStenoPlan")]
        //public List<StenoGorevPlanModel> GetIntersectStenoPlan(Guid stenoPlanId, Guid stenoId)
        //{
        //    var stenoEntity = _stenoService.GetIntersectStenoPlan(stenoPlanId, stenoId);
        //    var model = _mapper.Map<List<StenoGorevPlanModel>>(stenoEntity);
        //    return model;
        //}

        #endregion 
        #region StenoIzin
        [HttpGet("GetAllStenoIzin")]
        public IEnumerable<StenoIzinModel> GetAllStenoIzin()
        {
            var stenoEntity = _stenoService.GetAllStenoIzin();
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinByStenografId")]
        public IEnumerable<StenoIzinModel> GetStenoIzinByStenografId(Guid id)
        {
            var stenoEntity = _stenoService.GetStenoIzinByStenografId(id);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinByName")]
        public IEnumerable<StenoIzinModel> GetStenoIzinByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoIzinByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinBetweenDateAndStenograf")]
        public IEnumerable<StenoIzinModel> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi,DateTime bitTarihi,string? field,string? sortOrder, int? izinTur,Guid? stenograf, int pageIndex, int pagesize)
        {
            var stenoEntity = _stenoService.GetStenoIzinBetweenDateAndStenograf(basTarihi, bitTarihi, field, sortOrder, izinTur, stenograf, pageIndex, pagesize);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpPost("CreateStenoIzin")]
        public IActionResult CreateStenoIzin(StenoIzinModel model)
        {
            var entity = Mapper.Map<StenoIzin>(model);
            _stenoService.CreateStenoIzin(entity);
            return Ok(entity);

        }
        #endregion

        #region StenoGorev

        [HttpGet("GetStenoGorevById")]
        public IEnumerable<StenoGorevModel> GetStenoGorevById(Guid id)
        {
            var stenoEntity = _stenoService.GetStenoGorevById(id);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }       

        //[HttpGet("GetStenoUzmanGorevByBirlesimId")]
        //public List<StenoGorevModel> GetStenoUzmanGorevByBirlesimId(Guid birlesimId, int gorevturu)
        //{
        //    var lst = new List<StenoGorevModel>();
        //    var stenoEntity = _stenoService.GetStenoGorevByGorevTuru(gorevturu);
        //    if (stenoEntity != null && stenoEntity.Count() > 0)
        //    {
        //        var birlesimList = stenoEntity.Where(x => x.BirlesimId == birlesimId).OrderBy(x => x.GorevBasTarihi).ToList();
        //        if (birlesimList != null && birlesimList.Count() > 0)
        //        {
        //            var model = _mapper.Map<List<StenoGorevModel>>(birlesimList);
        //            var gorevBasTarihi = model.FirstOrDefault().GorevBasTarihi.Value;
        //            var gorevBitTarihi = model.FirstOrDefault().GorevBitisTarihi.Value;

        //            var birlesim = birlesimList.FirstOrDefault().Birlesim;
        //            double sure = 0;
        //            var ste = model.Where(x => x.StenografId == model.FirstOrDefault().StenografId);
        //            var stenoToplamSureAsım = ste.Max(x => x.GorevBitisTarihi.Value).Subtract(ste.Min(x => x.GorevBasTarihi.Value)).TotalMinutes <= 50;

        //            foreach (var item in model)
        //            {

        //                var iz = birlesimList.Where(x => x.StenografId == item.StenografId).SelectMany(x => x.Stenograf.StenoIzins)
        //                                    .Where(x => x.BaslangicTarihi.Value <= gorevBitTarihi &&
        //                                                x.BitisTarihi.Value >= gorevBitTarihi);
        //                item.StenoIzinTuru = iz != null && iz.Count() > 0 ? iz.Select(x => x.IzinTuru).FirstOrDefault() : 0;

        //                if (birlesim.ToplanmaTuru == ToplanmaTuru.GenelKurul)
        //                {
        //                    var maxBitis = stenoEntity.Where(x => x.BirlesimId != item.BirlesimId && x.StenografId == item.StenografId && x.GorevStatu != GorevStatu.Iptal).Max(x => x.GorevBitisTarihi);

        //                    var query = stenoEntity.Where(x => x.BirlesimId != item.BirlesimId &&
        //                                                       x.StenografId == item.StenografId &&
        //                                                       x.GorevStatu != GorevStatu.Iptal &&
        //                                                       x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes > 0 &&
        //                                                       x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes <= 60);

        //                    item.StenoToplantiVar = (query != null && query.Count() > 0) || (maxBitis.HasValue && maxBitis.Value.AddMinutes(sure * 9) >= gorevBitTarihi) ? true : false;
        //                    sure = gorevturu == (int)StenoGorevTuru.Stenograf ? birlesim.StenoSure : birlesim.UzmanStenoSure;
        //                }
        //                else
        //                {
        //                    item.StenoToplantiVar = false;
        //                    sure = item.StenoSure;
        //                }


        //                if (item.StenoToplantiVar || item.GorevStatu == GorevStatu.Iptal || /*item.GorevStatu == GorevStatu.GidenGrup ||*/ (iz != null && iz.Count() > 0))
        //                {
        //                    //item.GorevStatu = item.GorevStatu  == GorevStatu.GidenGrup ? GorevStatu.GidenGrup : GorevStatu.Iptal;
        //                }
        //                else
        //                {

        //                    if (item.GorevBasTarihi != gorevBasTarihi)
        //                    {
        //                        item.GorevBasTarihi = gorevBitTarihi;
        //                    }
        //                    if (item.GorevBitisTarihi != gorevBitTarihi)
        //                    {
        //                        item.GorevBitisTarihi = gorevBitTarihi.AddMinutes(sure);

        //                    }

        //                    gorevBasTarihi = item.GorevBasTarihi.Value;
        //                    gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value : DateTime.MinValue;
        //                }

        //                item.StenoToplamSureAsım = stenoToplamSureAsım;
        //                lst.Add(item);
        //            }
        //        }
        //    }
        //    //var entity = Mapper.Map<List<GorevAtama>>(model);
        //    //_stenoService.UpdateStenoGorev(entity);
        //    return lst;
        //}

        //[HttpGet("GetStenoGorevByBirlesimId")]
        //public List<StenoGorevModel> GetStenoGorevByBirlesimId(Guid birlesimId, int gorevturu)
        //{
        //    var lst = new List<StenoGorevModel>();
        //    var stenoEntity = _stenoService.GetStenoGorevByGorevTuru(gorevturu);
        //    //var model = _mapper.Map<List<StenoGorevModel>>(stenoEntity);

        //    if (stenoEntity != null && stenoEntity.Count() > 0)
        //    {
        //        var birlesimList = stenoEntity.Where(x => x.BirlesimId == birlesimId).OrderBy(x => x.GorevBasTarihi).ToList();
        //        if (birlesimList != null && birlesimList.Count() > 0)
        //        {
        //            var model = _mapper.Map<List<StenoGorevModel>>(birlesimList);
        //            var gorevBasTarihi = model.FirstOrDefault().GorevBasTarihi.Value;
        //            var gorevBitTarihi = model.FirstOrDefault().GorevBitisTarihi.Value;

        //            var birlesim = birlesimList.FirstOrDefault().Birlesim;
        //            double sure = 0;
        //            var ste = model.Where(x => x.StenografId == model.FirstOrDefault().StenografId);
        //            var stenoToplamSureAsım = ste.Max(x => x.GorevBitisTarihi.Value).Subtract(ste.Min(x => x.GorevBasTarihi.Value)).TotalMinutes <= 50;

        //            foreach (var item in model)
        //            {

        //                var iz = birlesimList.Where(x => x.StenografId == item.StenografId).SelectMany(x => x.Stenograf.StenoIzins)
        //                                   .Where(x => x.BaslangicTarihi.Value <= gorevBitTarihi &&
        //                                               x.BitisTarihi.Value >= gorevBitTarihi);
        //               item.StenoIzinTuru = iz != null && iz.Count() > 0 ? iz.Select(x => x.IzinTuru).FirstOrDefault() : 0;

        //               if (birlesim.ToplanmaTuru == ToplanmaTuru.GenelKurul)
        //               {
        //                    var maxBitis =  stenoEntity.Where(x => x.BirlesimId != item.BirlesimId && x.StenografId == item.StenografId && x.GorevStatu != GorevStatu.Iptal).Max(x => x.GorevBitisTarihi);
     
        //                    var query = stenoEntity.Where(x => x.BirlesimId != item.BirlesimId &&
        //                                                       x.StenografId == item.StenografId &&
        //                                                       x.GorevStatu != GorevStatu.Iptal &&
        //                                                       x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes > 0 &&
        //                                                       x.GorevBasTarihi.Value.Subtract(gorevBitTarihi).TotalMinutes <= 60);

        //                    item.StenoToplantiVar = (query != null && query.Count() > 0) || (maxBitis.HasValue && maxBitis.Value.AddMinutes(sure * 9) >= gorevBitTarihi) ? true : false;
        //                   sure = gorevturu == (int)StenoGorevTuru.Stenograf ? birlesim.StenoSure : birlesim.UzmanStenoSure;
        //               }
        //               else
        //               {
        //                   item.StenoToplantiVar = false;
        //                   sure = item.StenoSure;
        //               }


        //               if (item.StenoToplantiVar || item.GorevStatu == GorevStatu.Iptal || /*item.GorevStatu == GorevStatu.GidenGrup ||*/ (iz != null && iz.Count() > 0))
        //               {
        //                   //item.GorevStatu = item.GorevStatu == GorevStatu.GidenGrup ? GorevStatu.GidenGrup : GorevStatu.Iptal;
        //               }
        //               else
        //               {

        //                   if (item.GorevBasTarihi != gorevBasTarihi)
        //                   {
        //                       item.GorevBasTarihi = gorevBitTarihi;
        //                   }
        //                   if (item.GorevBitisTarihi != gorevBitTarihi)
        //                   {
        //                       item.GorevBitisTarihi = gorevBitTarihi.AddMinutes(sure);

        //                   }

        //                   gorevBasTarihi = item.GorevBasTarihi.Value;
        //                   gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value : DateTime.MinValue;
        //               }

        //               item.StenoToplamSureAsım = stenoToplamSureAsım;
        //               lst.Add(item);
        //           };
        //        }
        //    }
        //    //var entity = Mapper.Map<List<GorevAtama>>(model);
        //    //_stenoService.UpdateStenoGorev(entity);
        //    return lst;
        //}

        [HttpGet("GetStenoGorevByName")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoGorevByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByDateAndStatus")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByDateAndStatus(DateTime gorevBasTarihi, DateTime gorevBitTarihi,int status)
        {
            var stenoEntity = _stenoService.GetStenoGorevByDateAndStatus(gorevBasTarihi, gorevBitTarihi, status);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByStenografAndDate")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByStenografAndDate(Guid? stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            var stenoEntity = _stenoService.GetStenoGorevByStenografAndDate(stenografId, gorevBasTarihi, gorevBitTarihi);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }


        [HttpPut("UpdateStenoGorevAtama")]
        public IActionResult UpdateStenoGorevAtama(List<StenoGorevGüncelleModel> model)
        {
            try
            {
                var entity = Mapper.Map<List<GorevAtama>>(model);
                _stenoService.UpdateStenoGorev(entity);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetStenoGorevByStatus")]
        public List<StenoGorevModel> GetStenoGorevByStatus(int status=0)
        {
            var stenoEntity = _stenoService.GetStenoGorevBySatatus(status);
            var model = _mapper.Map<List<StenoGorevModel>>(stenoEntity);
            return model;
        }
        #endregion

        #region Stenograf
        [HttpGet("GetAllStenografByGroupId")]
        public IEnumerable<StenoModel> GetAllStenografByGroupId(Guid? groupId)
        {
            var stenoEntity = _stenoService.GetAllStenografByGroupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            model.ToList().ForEach(x => { x.GorevStatu = -1; });
            return model;
        }

        [HttpGet("GetAllStenografWithStatisticsByGroupId")]
        public IEnumerable<StenoModel> GetAllStenografWithStatisticsByGroupId(Guid? groupId, Guid yasamaId)
        {
            var stenoEntity = _stenoService.GetAllStenografByGroupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            //şimdilik kaldırıldı, tablodan direkt getirelecek, perfomanstan dolayı
            //model.ToList().ForEach(x => { x.GorevStatu = -1; x.GunlukGorevSuresi = _globalService.GetStenoSureDailyById(x.Id); x.HaftalikGorevSuresi = (int)_globalService.GetStenoSureWeeklyById(x.Id); x.YillikGorevSuresi = (int)_globalService.GetStenoSureYearlyById(x.Id, yasamaId); });
            return model;
        }

        [HttpGet("GetAllStenografGroup")]
        public List<StenoGrupViewModel> GetAllStenografGroup(int gorevTur)
        {
            var stenoEntity = _stenoService.GetAllStenografGroup(gorevTur);
            var lst =new List<StenoGrupViewModel>();

            foreach (var item in stenoEntity.GroupBy(c => new {
                c.Id,
                c.Ad,
                c.StenoGrupTuru
            }).Select(gcs => new StenoGrupViewModel()
            {
                GrupId = gcs.Key.Id,
                GrupName = gcs.Key.Ad,
                StenoGrupTuru = gcs.Key.StenoGrupTuru
            }))
            {
                lst.Add(new StenoGrupViewModel { GrupId =item.GrupId,GrupName =item.GrupName,StenoGrupTuru =item.StenoGrupTuru} );
            }
            foreach (var item in lst) 
            {
                var steno = stenoEntity.SelectMany(x => x.Stenografs).Where(x => x.GrupId == item.GrupId).Select(cl => new StenoViewModel
                {
                    AdSoyad = cl.AdSoyad,
                    Id = cl.Id,
                    SonGorevSuresi = 0, //kaldırıldı başka bir yere ekleencek//cl.GorevAtamas.Where(x => x.GorevBasTarihi >= DateTime.Now.AddDays(-7)).Sum(c => c.GorevDakika),
                    StenoGorevTuru = cl.StenoGorevTuru
                }).ToList();
                item.StenoViews = new List<StenoViewModel>();
                foreach (var item2 in steno.Where(x=>(int)x.StenoGorevTuru == gorevTur))
                {
                    item.StenoViews.Add(item2);
                }
            }
            return lst;
        }

        [HttpGet("GetAllStenografByGorevTuru")]
        public IEnumerable<StenoModel> GetAllStenografByGorevTuru(int? gorevTuru)
        {
            var stenoEntity = _stenoService.GetAllStenografByGorevTuru(gorevTuru);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            return model;
        }
        [HttpPost("CreateStenograf")]
        public IActionResult CreateStenograf(StenoModel model)
        {
            try
            {
                var entity = Mapper.Map<Stenograf>(model);
                _stenoService.CreateStenograf(entity);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetStenoGorevByGrupId")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByGrupId(Guid groupId)
        {
            var stenoGrpEntity = _stenoService.GetStenoGorevByGrupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoGrpEntity);
            return model;
        }
        [HttpDelete("DeleteStenoGorev")]
        public IActionResult DeleteStenoGorev(Guid stenoGorevId)
        {
            try
            {
                _stenoService.DeleteStenoGorev(stenoGorevId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetAllStenoGrupNotInclueded")]
        public IEnumerable<StenoModel> GetAllStenoGrupNotInclueded()
        {
            var stenoGrpEntity = _stenoService.GetAllStenoGrupNotInclueded();
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
            return model;
        }

        //[HttpGet("GetAvaliableStenoBetweenDateBySteno")]
        //public IEnumerable<StenoModel> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi,int gorevTuru, int toplantiTur)
        //{
        //    var stenoGrpEntity = _stenoService.GetAvaliableStenoBetweenDateBySteno(basTarihi, bitTarihi, gorevTuru, toplantiTur);
        //    var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
        //    return model;
        //}

        //[HttpGet("GetAvaliableStenoBetweenDateByGroup")]
        //public IEnumerable<StenoModel> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur)
        //{
        //    var stenoGrpEntity = _stenoService.GetAvaliableStenoBetweenDateByGroup(basTarihi, bitTarihi, groupId, toplantiTur);
        //    var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
        //    return model;
        //}

        [HttpPut("UpdateStenoSiraNo")]
        public IActionResult UpdateStenoSiraNo(List<StenoModel> model)
        {
            try
            {
                var entityList = Mapper.Map<List<Stenograf>>(model);
                _stenoService.UpdateStenoSiraNo(entityList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        //[HttpGet("GetStenoGorevByIdFromMongo")]
        //public GorevAtamaMongo GetStenoGorevByIdFromMongo(Guid id)
        //{
        //   var result = _stenoService.GetStenoGorevByIdFromMongo(id);
        //    return result;
        //}
        #endregion
        [HttpPost("CreateStenoGroup")]
        public IActionResult CreateStenoGroup(StenoGrupModel model)
        {
            try
            {
                _stenoService.CreateStenoGroup(model.StenoId, model.GrupId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
