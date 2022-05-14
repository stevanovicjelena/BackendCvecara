using AutoMapper;
using Cvecara.Entities;
using Cvecara.Repository;
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
    [Route("api/zaposleni")]
    [Produces("application/json", "applciation/xml")]
    public class ZaposleniController : ControllerBase
    {
        private readonly IZaposleniRepository zaposleniRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public ZaposleniController(IZaposleniRepository zaposleniRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.zaposleniRepository = zaposleniRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Zaposleni>> GetZaposlene()
        {
            var zaposleni = zaposleniRepository.GetAllZaposlene();
            if (zaposleni == null || zaposleni.Count == 0)
            {
                return NoContent();
            }

            return Ok(zaposleni);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{zaposleniId}")]
        public ActionResult<Zaposleni> GetZaposleni(int zaposleniId)
        {
            var zaposleni = zaposleniRepository.GetZaposleniById(zaposleniId);

            if (zaposleni == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Zaposleni>(zaposleni));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<Zaposleni> CreateZaposleni([FromBody] Zaposleni zaposleni)
        {
            try
            {

                Zaposleni zaposleniEntity = mapper.Map<Zaposleni>(zaposleni);
                Zaposleni confirmation = zaposleniRepository.CreateZaposleni(zaposleniEntity);
                zaposleniRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetZaposleni", "Zaposleni", new { zaposleniId = confirmation.zaposleniID });


                return Created(location, mapper.Map<Zaposleni>(confirmation));

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
        [HttpPut]
        public ActionResult<Zaposleni> Updatezaposleni([FromBody] Zaposleni zaposleni)
        {
            try
            {
                var oldZaposleni = zaposleniRepository.GetZaposleniById(zaposleni.zaposleniID);

                if (oldZaposleni == null)
                {
                    return NotFound();
                }

                Zaposleni zaposleniEntity = mapper.Map<Zaposleni>(zaposleni);
                mapper.Map(zaposleniEntity, oldZaposleni);
                zaposleniRepository.SaveChanges();

                return Ok(mapper.Map<Zaposleni>(zaposleni));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{zaposleniId}")]
        public IActionResult DeleteZaposleni(int zaposleniId)
        {
            try
            {
                var zaposleni = zaposleniRepository.GetZaposleniById(zaposleniId);

                if (zaposleni == null)
                {
                    return NotFound();
                }

                zaposleniRepository.DeleteZaposleni(zaposleniId);
                zaposleniRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
