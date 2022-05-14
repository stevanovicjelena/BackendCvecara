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
    [Route("api/lokacije")]
    [Produces("application/json", "applciation/xml")]
    public class LokacijeController : ControllerBase
    {
        private readonly ILokacijeRepository lokacijeRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public LokacijeController(ILokacijeRepository lokacijeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.lokacijeRepository = lokacijeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Lokacije>> GetLokacije()
        {
            var lokacije = lokacijeRepository.GetAllLokacije();
            if (lokacije == null || lokacije.Count == 0)
            {
                return NoContent();
            }

            return Ok(lokacije);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{lokacijeId}")]
        public ActionResult<Lokacije> GetLokacija(int lokacijeId)
        {
            var lokacije = lokacijeRepository.GetLokacijeById(lokacijeId);

            if (lokacije == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Lokacije>(lokacije));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public ActionResult<Lokacije> CreateLokacije([FromBody] Lokacije lokacije)
        {
            try
            {

                Lokacije lokacijeEntity = mapper.Map<Lokacije>(lokacije);
                Lokacije confirmation = lokacijeRepository.CreateLokacije(lokacijeEntity);
                lokacijeRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetLokacije", "Lokacije", new { lokacijeId = confirmation.lokacijaID });


                return Created(location, mapper.Map<Lokacije>(confirmation));

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
        public ActionResult<Lokacije> UpdateLokacije([FromBody] Lokacije lokacije)
        {
            try
            {
                var oldLokacije = lokacijeRepository.GetLokacijeById(lokacije.lokacijaID);

                if (oldLokacije == null)
                {
                    return NotFound();
                }

                Lokacije lokacijeEntity = mapper.Map<Lokacije>(lokacije);
                mapper.Map(lokacijeEntity, oldLokacije);
                lokacijeRepository.SaveChanges();

                return Ok(mapper.Map<Lokacije>(lokacije));
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
        [HttpDelete("{lokacijeId}")]
        public IActionResult DeleteLokacije(int lokacijeId)
        {
            try
            {
                var lokacije = lokacijeRepository.GetLokacijeById(lokacijeId);

                if (lokacije == null)
                {
                    return NotFound();
                }

                lokacijeRepository.DeleteLokacije(lokacijeId);
                lokacijeRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
