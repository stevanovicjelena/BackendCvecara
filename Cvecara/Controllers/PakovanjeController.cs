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
    [Route("api/pakovanje")]
    [Produces("application/json", "applciation/xml")]
    public class PakovanjeController : ControllerBase 
    {
        private readonly IPakovanjeRepository pakovanjeRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public PakovanjeController(IPakovanjeRepository pakovanjeRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.pakovanjeRepository = pakovanjeRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Pakovanje>> GetPakovanja(string nazivPakovanja)
        {
            var pakovanja = pakovanjeRepository.GetAllPakovanja(nazivPakovanja);
            if (pakovanja == null || pakovanja.Count == 0)
            {
                return NoContent();
            }

            return Ok(pakovanja);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{pakovanjeId}")]
        public ActionResult<Pakovanje> GetPakovanje(int pakovanjeId)
        {
            var pakovanje = pakovanjeRepository.GetPakovanjeById(pakovanjeId);

            if (pakovanje == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Pakovanje>(pakovanje));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public ActionResult<Pakovanje> CreatePakovanje([FromBody] Pakovanje pakovanje)
        {
            try
            {

                Pakovanje pakovanjeEntity = mapper.Map<Pakovanje>(pakovanje);
                Pakovanje confirmation = pakovanjeRepository.CreatePakovanje(pakovanjeEntity);
                pakovanjeRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetPakovanje", "Pakovanje", new { pakovanjeId = confirmation.pakovanjeID });


                return Created(location, mapper.Map<Pakovanje>(confirmation));

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
        public ActionResult<Pakovanje> Updatepakovanje([FromBody] Pakovanje pakovanje)
        {
            try
            {
                var oldpakovanje = pakovanjeRepository.GetPakovanjeById(pakovanje.pakovanjeID);

                if (oldpakovanje == null)
                {
                    return NotFound();
                }

                Pakovanje pakovanjeEntity = mapper.Map<Pakovanje>(pakovanje);
                mapper.Map(pakovanjeEntity, oldpakovanje);
                pakovanjeRepository.SaveChanges();

                return Ok(mapper.Map<Pakovanje>(pakovanje));
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
        [HttpDelete("{pakovanjeId}")]
        public IActionResult Deletepakovanje(int pakovanjeId)
        {
            try
            {
                var pakovanje = pakovanjeRepository.GetPakovanjeById(pakovanjeId);

                if (pakovanje == null)
                {
                    return NotFound();
                }

                pakovanjeRepository.DeletePakovanje(pakovanjeId);
                pakovanjeRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
