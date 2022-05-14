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
    [Route("api/kupac")]
    [Produces("application/json", "applciation/xml")]
    public class KupacController : ControllerBase
    {
        private readonly IKupacRepository kupacRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public KupacController(IKupacRepository kupacRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.kupacRepository = kupacRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "Zaposleni")]
        [HttpHead]
        public ActionResult<List<Kupac>> GetKupci()
        {
            var kupci = kupacRepository.GetAllKupci();
            if (kupci == null || kupci.Count == 0)
            {
                return NoContent();
            }

            return Ok(kupci);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{kupacId}")]
        public ActionResult<Kupac> GetKupac(int kupacId)
        {
            var kupac = kupacRepository.GetKupacById(kupacId);

            if (kupac == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Kupac>(kupac));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [HttpPost]
        public ActionResult<Kupac> CreateKupac([FromBody] Kupac kupac)
        {
            try
            {

                Kupac kupacEntity = mapper.Map<Kupac>(kupac);
                Kupac confirmation = kupacRepository.CreateKupac(kupacEntity);
                kupacRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetKupac", "Kupac", new { kupacId = confirmation.kupacID });


                return Created(location, mapper.Map<Kupac>(confirmation));

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
        public ActionResult<Kupac> UpdateKupac([FromBody] Kupac kupac)
        {
            try
            {
                var oldKupac = kupacRepository.GetKupacById(kupac.kupacID);

                if (oldKupac == null)
                {
                    return NotFound();
                }

                Kupac kupacEntity = mapper.Map<Kupac>(kupac);
                mapper.Map(kupacEntity, oldKupac);
                kupacRepository.SaveChanges();

                return Ok(mapper.Map<Kupac>(kupac));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{kupacId}")]
        public IActionResult DeleteKupac(int kupacId)
        {
            try
            {
                var kupac = kupacRepository.GetKupacById(kupacId);

                if (kupac == null)
                {
                    return NotFound();
                }

                kupacRepository.DeleteKupac(kupacId);
                kupacRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
