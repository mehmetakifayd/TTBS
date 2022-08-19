﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Core.Enums;
using TTBS.Models;
using TTBS.MongoDB;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GorevAtamaController : BaseController<GorevAtamaController>
    {
        private readonly IGorevAtamaService _gorevAtamaService;
        private readonly ILogger<GorevAtamaController> _logger;
        public readonly IMapper _mapper;
        private readonly IGlobalService _globalService;
        public GorevAtamaController(IGorevAtamaService gorevAtamaService, ILogger<GorevAtamaController> logger, IMapper mapper,IGlobalService globalService)
        {
            _gorevAtamaService = gorevAtamaService;
            _logger = logger;
            _mapper = mapper;
            _globalService = globalService;
        }

        [HttpPost("CreateBirlesim")]
        public IActionResult CreateBirlesim(BirlesimModel model)
        {
            try
            {
                model.ToplanmaDurumu = model.ToplanmaTuru == ToplanmaTuru.GenelKurul ? ToplanmaStatu.Planlandı : ToplanmaStatu.Oluşturuldu;
                var entity = Mapper.Map<Birlesim>(model);
                var birlesim = _gorevAtamaService.CreateBirlesim(entity);
                var oturumId = _gorevAtamaService.CreateOturum(new Oturum
                {
                    BirlesimId = birlesim.Id,
                    BaslangicTarihi = birlesim.BaslangicTarihi
                });

                if (model.ToplanmaTuru == ToplanmaTuru.GenelKurul)
                {
                    var stenoAllList = _gorevAtamaService.GetStenografIdList();
                    var stenoList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Stenograf)
                                                .Select(x => x.Id);
                                                
                    var modelList = SetGorevAtama(birlesim, oturumId, stenoList,birlesim.StenoSure);
                    var stenoUzmanList = stenoAllList.Where(x => x.StenoGorevTuru == StenoGorevTuru.Uzman)
                                                     .Select(x => x.Id);
                                                    
                    var modelUzmanList = SetGorevAtama(birlesim, oturumId, stenoUzmanList, birlesim.UzmanStenoSure);
                    modelList.AddRange(modelUzmanList);
                    var entityList = Mapper.Map<List<GorevAtamaGenelKurul>>(modelList);
                    _gorevAtamaService.CreateStenoAtamaGK(entityList);
                }
                else if (model.ToplanmaTuru == ToplanmaTuru.Komisyon)
                {
                    _gorevAtamaService.CreateBirlesimKomisyonRelation(birlesim.Id, birlesim.KomisyonId, birlesim.AltKomisyonId);
                }
                else if (model.ToplanmaTuru == ToplanmaTuru.OzelToplanti)
                {
                    _gorevAtamaService.CreateBirlesimOzelToplanmaRelation(birlesim.Id, birlesim.OzelToplanmaId);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            if (model.StenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                var birlesim = _gorevAtamaService.UpdateBirlesimGorevAtama(model.BirlesimId,model.TurAdedi);
                var modelList = SetGorevAtama(birlesim, model.OturumId, model.StenografIds, birlesim.StenoSure);
                var entityList = Mapper.Map<List<GorevAtamaKomisyon>>(modelList);
                _gorevAtamaService.CreateStenoAtamaKom(entityList);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        private List<GorevAtamaModel> SetGorevAtama(Birlesim birlesim, Guid oturumId, IEnumerable<Guid> stenoList,double sure)
        {
            var atamaList = new List<GorevAtamaModel>();
            var basDate = birlesim.BaslangicTarihi.HasValue ? birlesim.BaslangicTarihi.Value:DateTime.Now;
            int firstRec = 0;
            for (int i = 0; i < birlesim.TurAdedi; i++)
            {
                foreach (var item in stenoList)
                {
                    var newEntity = new GorevAtamaModel();
                    newEntity.BirlesimId = birlesim.Id;
                    newEntity.OturumId = oturumId;
                    newEntity.StenografId = item;
                    newEntity.GorevBasTarihi = basDate.AddMinutes(firstRec * sure);
                    newEntity.GorevBitisTarihi = basDate.AddMinutes((firstRec * sure) + sure);
                    newEntity.StenoSure = sure;
                    //newEntity.GorevStatu = item.StenoGrups.Select(x => x.GidenGrupMu).FirstOrDefault() == DurumStatu.Evet && newEntity.GorevBasTarihi.Value.AddMinutes(9 * newEntity.StenoSure) >= DateTime.Today.AddHours(18) ? GorevStatu.GidenGrup : GorevStatu.Planlandı;
                    firstRec++;
                    newEntity.SatırNo = firstRec ;
                    atamaList.Add(newEntity);
                    
                }
            }
            return atamaList;
        }


        [HttpPost("AddStenoGorevAtamaKomisyon")]
        public IActionResult AddStenoGorevAtamaKomisyon(List<Guid> stenografIds,string birlesimId,string oturumId)
        {
            if (stenografIds == null)
                return BadRequest("Stenograf Listesi Dolu Olmalıdır!");
            try
            {
                _gorevAtamaService.AddStenoGorevAtamaKomisyon(stenografIds, birlesimId, oturumId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPut("CreateStenoGorevDonguEkle")]
        public IActionResult CreateStenoGorevDonguEkle(string birlesimId, string oturumId)
        {
            try
            {
                _gorevAtamaService.CreateStenoGorevDonguEkle(birlesimId, oturumId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("GetStenoGorevByBirlesimId")]
        public List<GorevAtamaModel> GetStenoGorevByBirlesimId(Guid birlesimId,ToplanmaTuru toplanmaTuru)
        { 
          return  _gorevAtamaService.GetGorevAtamaByBirlesimId(birlesimId, toplanmaTuru);
        }

        //[HttpPost("ChangeOrderStenografKomisyon")]
        //public IActionResult ChangeOrderStenografKomisyon(string kaynakBirlesimId, Dictionary<string, string> kaynakStenoList, string hedefBirlesimId, Dictionary<string, string> hedefStenografList)
        //{
        //    try
        //    {
        //        _gorevAtamaService.ChangeOrderStenografKomisyon(kaynakBirlesimId, kaynakStenoList, hedefBirlesimId, hedefStenografList);
        //    }
        //    catch (Exception ex)
        //    { return BadRequest(ex.Message); }

        //    return Ok();
        //}

        [HttpPost("ChangeSureStenografKomisyon")]
        public IActionResult ChangeSureStenografKomisyon(string birlesimId, int satırNo, double sure, bool digerAtamalarDahil = false)
        {
            try
            {
                _gorevAtamaService.ChangeSureStenografKomisyon(birlesimId, satırNo, sure, digerAtamalarDahil);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetStenografIdList")]
        public List<StenoModel> GetStenografIdList()
        {
            var entity = _gorevAtamaService.GetStenografIdList(DateTime.Now);
            var model = _mapper.Map<List<StenoModel>>(entity);
            return model;
        }
        [HttpPut("UpdateGorevDurumById")]
        public IActionResult UpdateGorevDurumById(Guid id, ToplanmaTuru toplanmaTuru)
        {
            try
            {
                _gorevAtamaService.UpdateGorevDurumById(id, toplanmaTuru);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpPut("UpdateGorevDurumByBirlesimAndSteno")]
        public IActionResult UpdateGorevDurumByBirlesimAndSteno(Guid birlesimId, Guid stenoId, ToplanmaTuru toplanmaTuru)
        {
            try
            {
                _gorevAtamaService.UpdateGorevDurumByBirlesimAndSteno(birlesimId, stenoId, toplanmaTuru);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("UpdateBirlesimStenoGorev")]
        public IActionResult UpdateBirlesimStenoGorev(BirlesimStenoGorevModel model)
        {
            try
            {
                if (ToplanmaBaslatmaStatu.Baslama == model.ToplanmaBaslatmaStatu)
                {
                    _gorevAtamaService.UpdateBirlesimStenoGorevBaslama(model.BirlesimId, model.BasTarihi, model.StenoGorevTuru,model.ToplanmaTuru);
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BaslangicTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);
                    }
                }
                else if (ToplanmaBaslatmaStatu.AraVerme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.AraVerme;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);
                    }
                }
                else if (ToplanmaBaslatmaStatu.DevamEtme == model.ToplanmaBaslatmaStatu)
                {
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.ToplanmaDurumu = ToplanmaStatu.DevamEdiyor;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi != null).LastOrDefault();
                    //var oturumId= _globalService.CreateOturum(new Oturum { BirlesimId = model.BirlesimId, BaslangicTarihi = model.BasTarihi });
                    var oturumId = Guid.Empty;
                    _gorevAtamaService.UpdateBirlesimStenoGorevDevamEtme(model.BirlesimId, model.BasTarihi, model.StenoGorevTuru, oturum.BitisTarihi.Value, oturumId,model.ToplanmaTuru);
                }
                else if (ToplanmaBaslatmaStatu.Sonladırma == model.ToplanmaBaslatmaStatu)
                {
                    var oturum = _globalService.GetOturumByBirlesimId(model.BirlesimId).Where(x => x.BitisTarihi == null).FirstOrDefault();
                    if (oturum != null)
                    {
                        oturum.BitisTarihi = model.BasTarihi;
                        _globalService.UpdateOturum(oturum);

                    }
                    var birlesim = _globalService.GetBirlesimById(model.BirlesimId).FirstOrDefault();
                    if (birlesim != null)
                    {
                        birlesim.BitisTarihi = model.BasTarihi;
                        birlesim.ToplanmaDurumu = ToplanmaStatu.Tamamlandı;
                        _globalService.UpdateBirlesim(birlesim);
                    }
                    _gorevAtamaService.UpdateStenoGorevTamamla(model.BirlesimId, model.StenoGorevTuru, model.ToplanmaTuru);
                }
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpDelete("DeleteGorevByBirlesimIdAndStenoId")]
        public IActionResult DeleteGorevByBirlesimIdAndStenoId(Guid birlesimId, Guid stenografId, ToplanmaTuru toplanmaTuru)
        {
            try
            {
                _gorevAtamaService.DeleteGorevByBirlesimIdAndStenoId(birlesimId, stenografId, toplanmaTuru);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }
    }
}
