using AutoMapper;
using Cvecara.Entities;
using Cvecara.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cvecara.Controllers
{
    [ApiController]
    [Route("api/tipDodatka")]
    [Produces("application/json", "applciation/xml")]
    public class TipDodatkaController : ControllerBase
    {
        private readonly ITipDodatkaRepository tipDodatkaRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public TipDodatkaController(ITipDodatkaRepository tipDodatkaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.tipDodatkaRepository = tipDodatkaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<TipDodatka>> GetTipoviDodataka()
        {
            var tipoviDodataka = tipDodatkaRepository.GetAllTipoveDodataka();
            if (tipoviDodataka == null || tipoviDodataka.Count == 0)
            {
                return NoContent();
            }

            return Ok(tipoviDodataka);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{tipDodatkaId}")]
        public ActionResult<TipDodatka> GetTipDodatka(int tipDodatkaId)
        {
            var tipDodatka = tipDodatkaRepository.GetTipDodatkaById(tipDodatkaId);

            if (tipDodatka == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TipDodatka>(tipDodatka));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public ActionResult<TipDodatka> CreateTipDodatka([FromBody] TipDodatka tipDodatka)
        {
            try
            {

                TipDodatka tipDodatkaEntity = mapper.Map<TipDodatka>(tipDodatka);
                TipDodatka confirmation = tipDodatkaRepository.CreateTipDodatka(tipDodatkaEntity);
                tipDodatkaRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetTipDodatka", "TipDodatka", new { tipDodatkaId = confirmation.tipDodatkaID });


                return Created(location, mapper.Map<TipDodatka>(confirmation));

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Create Error");
            }
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPut]
        public ActionResult<TipDodatka> UpdateTipDodatka([FromBody] TipDodatka tipDodatka)
        {
            try
            {
                var oldTipDodatka = tipDodatkaRepository.GetTipDodatkaById(tipDodatka.tipDodatkaID);

                if (oldTipDodatka == null)
                {
                    return NotFound();
                }

                TipDodatka tipDodatkaEntity = mapper.Map<TipDodatka>(tipDodatka);
                mapper.Map(tipDodatkaEntity, oldTipDodatka);
                tipDodatkaRepository.SaveChanges();

                return Ok(mapper.Map<TipDodatka>(tipDodatka));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni")]
        [HttpDelete("{tipDodatkaId}")]
        public IActionResult DeleteTipDodatka(int tipDodatkaId)
        {
            try
            {
                var tipDodatka = tipDodatkaRepository.GetTipDodatkaById(tipDodatkaId);

                if (tipDodatka == null)
                {
                    return NotFound();
                }

                tipDodatkaRepository.DeleteTipDodatka(tipDodatkaId);
                tipDodatkaRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
