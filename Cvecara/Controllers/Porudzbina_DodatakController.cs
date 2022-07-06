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
    [Route("api/porudzbina_dodatak")]
    [Produces("application/json", "applciation/xml")]
    public class Porudzbina_DodatakController : ControllerBase 
    {
        private readonly IPorudzbina_DodatakRepository porudzbina_dodatakRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly IPorudzbinaRepository porudzbinaRepository;
        private readonly IDodatakRepository dodatakRepository;

        public Porudzbina_DodatakController(IPorudzbina_DodatakRepository porudzbina_dodatakRepository, IPorudzbinaRepository porudzbinaRepository, IDodatakRepository dodatakRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.porudzbina_dodatakRepository = porudzbina_dodatakRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.porudzbinaRepository = porudzbinaRepository;
            this.dodatakRepository = dodatakRepository;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Porudzbina_Dodatak>> GetPorudzbine_Dodaci()
        {
            var porudzbine_dodaci = porudzbina_dodatakRepository.GetAllPorudzbina_Dodatak();
            if (porudzbine_dodaci == null || porudzbine_dodaci.Count == 0)
            {
                return NoContent();
            }

            foreach (Porudzbina_Dodatak pd in porudzbine_dodaci)
            {
                Porudzbina porudzbina = porudzbinaRepository.GetPorudzbinaById(pd.porudzbinaID);
                pd.porudzbina = porudzbina;

                Dodatak dodatak = dodatakRepository.GetDodatakById(pd.dodatakID);
                pd.dodatak = dodatak;
            }

            return Ok(porudzbine_dodaci);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{porudzbina_dodatakId}")]
        public ActionResult<Porudzbina_Dodatak> GetPorudzbina_Dodatak(int porudzbina_dodatakId)
        {
            var porudzbina_dodatak = porudzbina_dodatakRepository.GetPorudzbina_DodatakById(porudzbina_dodatakId);

            if (porudzbina_dodatak == null)
            {
                return NotFound();
            }

            Porudzbina porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbina_dodatak.porudzbinaID);
            porudzbina_dodatak.porudzbina = porudzbina;

            Dodatak dodatak = dodatakRepository.GetDodatakById(porudzbina_dodatak.dodatakID);
            porudzbina_dodatak.dodatak = dodatak;

            return Ok(mapper.Map<Porudzbina_Dodatak>(porudzbina_dodatak));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpPost]
        public ActionResult<Porudzbina_Dodatak> CreatePorudzbina_Dodatak([FromBody] Porudzbina_Dodatak porudzbina_dodatak)
        {
            try
            {

                Porudzbina_Dodatak porudzbina_dodatakEntity = mapper.Map<Porudzbina_Dodatak>(porudzbina_dodatak);
                Porudzbina_Dodatak confirmation = porudzbina_dodatakRepository.CreatePorudzbina_Dodatak(porudzbina_dodatakEntity);
                porudzbina_dodatakRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetPorudzbina_Dodatak", "Porudzbina_Dodatak", new { porudzbina_dodatakId = confirmation.porudzbina_Dodatak_ID });


                return Created(location, mapper.Map<Porudzbina_Dodatak>(confirmation));

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
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpPut]
        public ActionResult<Porudzbina_Dodatak> UpdatePorudzbina_Dodatak([FromBody] Porudzbina_Dodatak porudzbina_dodatak)
        {
            try
            {
                var oldPorudzbina_Dodatak = porudzbina_dodatakRepository.GetPorudzbina_DodatakById(porudzbina_dodatak.porudzbina_Dodatak_ID);

                if (oldPorudzbina_Dodatak == null)
                {
                    return NotFound();
                }

                Porudzbina_Dodatak porudzbina_dodatakEntity = mapper.Map<Porudzbina_Dodatak>(porudzbina_dodatak);
                mapper.Map(porudzbina_dodatakEntity, oldPorudzbina_Dodatak);
                porudzbina_dodatakRepository.SaveChanges();

                return Ok(mapper.Map<Porudzbina_Dodatak>(porudzbina_dodatak));
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
        [HttpDelete("{porudzbina_dodatakId}")]
        public IActionResult DeletePorudzbina_Dodatak(int porudzbina_dodatakId)
        {
            try
            {
                var porudzbina_dodatak = porudzbina_dodatakRepository.GetPorudzbina_DodatakById(porudzbina_dodatakId);

                if (porudzbina_dodatak == null)
                {
                    return NotFound();
                }

                porudzbina_dodatakRepository.DeletePorudzbina_Dodatak(porudzbina_dodatakId);
                porudzbina_dodatakRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
