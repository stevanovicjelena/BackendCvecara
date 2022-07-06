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
    [Route("api/dodatak")]
    [Produces("application/json", "applciation/xml")]
    public class DodatakController : ControllerBase
    {
        private readonly IDodatakRepository dodatakRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        public readonly ITipDodatkaRepository tipDodatkaRepository;

        public DodatakController(IDodatakRepository dodatakRepository, ITipDodatkaRepository tipDodatkaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.dodatakRepository = dodatakRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.tipDodatkaRepository = tipDodatkaRepository;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
     //   [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Dodatak>> GetDodaci(string bojaDodatka)
        {
            var dodaci = dodatakRepository.GetAllDodaci(bojaDodatka);
            if (dodaci == null || dodaci.Count == 0)
            {
                return NoContent();
            }

            foreach (Dodatak d in dodaci)
            {
                TipDodatka tipDodatka = tipDodatkaRepository.GetTipDodatkaById(d.tipDodatkaID);
                d.tipDodatka = tipDodatka;
            }

            return Ok(dodaci);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{dodatakId}")]
        public ActionResult<Dodatak> GetDodatak(int dodatakId)
        {
            var dodatak = dodatakRepository.GetDodatakById(dodatakId);

            if (dodatak == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Dodatak>(dodatak));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public ActionResult<Dodatak> CreateDodatak([FromBody] Dodatak dodatak)
        {
            try
            {

                Dodatak dodatakEntity = mapper.Map<Dodatak>(dodatak);
                Dodatak confirmation = dodatakRepository.CreateDodatak(dodatakEntity);
                dodatakRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetDodatak", "Dodatak", new { dodatakId = confirmation.dodatakID });


                return Created(location, mapper.Map<Dodatak>(confirmation));

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
        public ActionResult<Dodatak> UpdateDodatak([FromBody] Dodatak dodatak)
        {
            try
            {
                var oldDodatak = dodatakRepository.GetDodatakById(dodatak.dodatakID);

                if (oldDodatak == null)
                {
                    return NotFound();
                }

                Dodatak dodatakEntity = mapper.Map<Dodatak>(dodatak);
                mapper.Map(dodatakEntity, oldDodatak);
                dodatakRepository.SaveChanges();

                return Ok(mapper.Map<Dodatak>(dodatak));
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
        [HttpDelete("{dodatakId}")]
        public IActionResult DeleteDodatak(int dodatakId)
        {
            try
            {
                var dodatak = dodatakRepository.GetDodatakById(dodatakId);

                if (dodatak == null)
                {
                    return NotFound();
                }

                dodatakRepository.DeleteDodatak(dodatakId);
                dodatakRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
