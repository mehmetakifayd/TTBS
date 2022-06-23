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
        private readonly ILogger<StenografController> _logger;
        public readonly IMapper _mapper;

        public StenografController(IStenografService stenoService, IGlobalService globalService, ILogger<StenografController> logger, IMapper mapper)
        {
            _stenoService = stenoService;
            _globalService = globalService;
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

        [HttpGet("GetStenoPlanByDateAndStatus")]
        public List<BirlesimViewModel> GetBirlesimByDateAndTur(DateTime gorevTarihi, DateTime gorevBitTarihi, int gorevTuru)
        {
            var stenoEntity = _stenoService.GetBirlesimByDateAndTur(gorevTarihi, gorevBitTarihi, gorevTuru);
            var model = _mapper.Map<List<BirlesimViewModel>>(stenoEntity);
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

        [HttpPut("UpdateGorevDurumByBirlesimAndSteno")]
        public IActionResult UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId)
        {
            try
            {
                _stenoService.UpdateGorevDurumByBirlesimAndSteno(birlesimId, stenoId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("UpdateGorevDurumById")]
        public IActionResult UpdateGorevDurumById(Guid id)
        {
            try
            {
                _stenoService.UpdateGorevDurumById(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetStenoGorevByBirlesimId")]
        public List<StenoGorevModel> GetStenoGorevByBirlesimId(Guid birlesimId,int gorevturu)
        {
            var lst = new List<StenoGorevModel>();
            var stenoEntity = _stenoService.GetStenoGorevByBirlesimId(gorevturu);
            var birlesimList = stenoEntity.Where(x=>x.BirlesimId == birlesimId).OrderBy(x=>x.GorevBasTarihi).ToList();
            var model = _mapper.Map<List<StenoGorevModel>>(birlesimList);
            var gorevBasTarihi = DateTime.MinValue;
            var gorevBitTarihi = DateTime.MinValue;
            bool checkTrue = true;
            foreach (var item in model)
            {
                var birlesim = birlesimList.FirstOrDefault().Birlesim;
                var sure = gorevturu == (int)StenoGorevTuru.Stenograf ? birlesim.StenoSure  : birlesim.UzmanStenoSure;
                var limit = birlesim.ToplanmaTuru == ToplanmaTuru.GenelKurul ? 60 : sure * 9;
                           
                var query = stenoEntity.Where(x => x.BirlesimId != item.BirlesimId && 
                                                   x.StenografId == item.StenografId && 
                                                   x.GorevBasTarihi.Value.Subtract(item.GorevBasTarihi.Value).TotalMinutes > 0 &&
                                                   x.GorevBasTarihi.Value.Subtract(item.GorevBasTarihi.Value).TotalMinutes <= limit);

              
                item.StenoToplantiVar = query != null && query.Count()>0 ?true : false;
                if (item.StenoToplantiVar)
                {
                    ////if(checkTrue)
                    ////{
                    //    gorevBasTarihi = item.GorevBasTarihi.Value;
                    //    gorevBitTarihi = item.GorevBitisTarihi.Value;
                    ////}
                      

                    item.GorevStatu = GorevStatu.Iptal;
                    checkTrue = false;
                }
                else
                {

                    //checkTrue = true;
                    if (item.GorevBasTarihi != gorevBasTarihi && gorevBasTarihi != DateTime.MinValue )
                    {
                        item.GorevBasTarihi = gorevBitTarihi;
                    }
                    if (item.GorevBitisTarihi != gorevBitTarihi && gorevBitTarihi != DateTime.MinValue )
                    {
                        item.GorevBitisTarihi = gorevBitTarihi.AddMinutes(sure);
   
                    }

                    gorevBasTarihi = item.GorevBasTarihi.Value;
                    gorevBitTarihi = item.GorevBitisTarihi.HasValue ? item.GorevBitisTarihi.Value:DateTime.MinValue;
                }

                var iz = birlesimList.Where(x=>  x.StenografId == item.StenografId).SelectMany(x => x.Stenograf.StenoIzins)
                                     .Where(x => x.BaslangicTarihi.Value <= item.GorevBasTarihi.Value &&
                                                 x.BitisTarihi.Value >= item.GorevBasTarihi.Value);
                item.StenoIzinTuru =iz!=null && iz.Count()>0 ? iz.Select(x=>x.IzinTuru).FirstOrDefault() : 0;

                lst.Add(item);
            }

            //var entity = Mapper.Map<List<GorevAtama>>(model);
            //_stenoService.UpdateStenoGorev(entity);
            return lst;
        }

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
        public IEnumerable<StenoGorevModel> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            var stenoEntity = _stenoService.GetStenoGorevByStenografAndDate(stenografId, gorevBasTarihi, gorevBitTarihi);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpPut("UpdateBirlesimStenoGorev")]
        public IActionResult UpdateBirlesimStenoGorev(BirlesimStenoGorevModel model)
        {
            try
            {
                if(ToplanmaBaslatmaStatu.Baslama == model.ToplanmaBaslatmaStatu)
                      _stenoService.UpdateBirlesimStenoGorev(model.BirlesimId,model.BasTarihi);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }


        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            if(model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                birlesim.TurAdedi = model.TurAdedi;
                _globalService.UpdateBirlesim(birlesim);

                model.OturumId = model.OturumId == Guid.Empty ? _globalService.CreateOturum(new Oturum { BirlesimId = model.BirlesimId, BaslangicTarihi = DateTime.Now }): model.OturumId;

                var entity = Mapper.Map<GorevAtama>(model);
               _stenoService.CreateStenoGorevAtama(entity,birlesim);             
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
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

        # region Stenograf
        [HttpGet("GetAllStenografByGroupId")]
        public IEnumerable<StenoModel> GetAllStenografByGroupId(Guid? groupId)
        {
            var stenoEntity = _stenoService.GetAllStenografByGroupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetAllStenografGroup")]
        public List<StenoGrupViewModel> GetAllStenografGroup(int gorevTur)
        {
            var stenoEntity = _stenoService.GetAllStenografGroup(gorevTur);
            var lst =new List<StenoGrupViewModel>();

            foreach (var item in stenoEntity.GroupBy(c => new {
                c.Id,
                c.Ad
            }).Select(gcs => new StenoGrupViewModel()
            {
                GrupId = gcs.Key.Id,
                GrupName = gcs.Key.Ad
            }))
            {
                lst.Add(new StenoGrupViewModel { GrupId =item.GrupId,GrupName =item.GrupName} );
            }
            foreach (var item in lst) 
            {
                var steno = stenoEntity.SelectMany(x=>x.StenoGrups).Where(x => x.GrupId == item.GrupId).Select(cl => new StenoViewModel
                {
                    AdSoyad = cl.Stenograf.AdSoyad,
                    Id = cl.Stenograf.Id,
                    SonGorevSuresi = cl.Stenograf.GorevAtamas.Where(x=>x.GorevBasTarihi>=DateTime.Now.AddDays(-7)).Sum(c => c.GorevDakika)
                }).ToList();
                item.StenoViews = new List<StenoViewModel>();
                foreach (var item2 in steno)
                {
                    item.StenoViews.Add(item2);
                }
            }
            return lst;
        }

        [HttpGet("GetAllStenografByGorevTuru")]
        public IEnumerable<StenoModel> GetAllStenografByGorevTuru(int gorevTuru)
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

        [HttpDelete("DeleteGorevByBirlesimIdAndStenoId")]
        public IActionResult DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId,Guid stenografId)
        {
            try
            {
                _stenoService.DeleteGorevByBirlesimIdAndStenoId(birlesimId, stenografId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("CreateStenoGroup")]
        public IActionResult CreateStenoGroup(StenoGrupModel model)
        {

            var entity = Mapper.Map<StenoGrup>(model);
            _stenoService.CreateStenoGroup(entity);
            return Ok();
        }

        [HttpDelete("DeleteStenoGroup")]
        public IActionResult DeleteStenoGroup(StenoGrupModel model)
        {

            var entity = Mapper.Map<StenoGrup>(model);
            _stenoService.DeleteStenoGroup(entity);
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

        [HttpGet("GetAssignedStenoByBirlesimId")]
        public IEnumerable<StenoModel> GetAssignedStenoByBirlesimId(Guid birlesimId)
        {
            var stenoEntity = _stenoService.GetAssignedStenoByBirlesimId(birlesimId);
            var stenoGroup = stenoEntity.GroupBy(x => new
            {
                x.StenografId,
                x.Stenograf.AdSoyad,
                x.Stenograf.StenoGorevTuru,
                x.Stenograf.SiraNo,
                x.Stenograf.SonGorevSuresi,
                x.Stenograf.StenoGorevDurum
            }).Select(z => new Stenograf
            {
                Id = z.Key.StenografId,
                AdSoyad = z.Key.AdSoyad,
                StenoGorevTuru = z.Key.StenoGorevTuru,
                SiraNo = z.Key.SiraNo,
                SonGorevSuresi = z.Key.SonGorevSuresi,
                StenoGorevDurum = z.Key.StenoGorevDurum
            });

            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGroup);
            return model;
        }

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
        #endregion

    }
}
