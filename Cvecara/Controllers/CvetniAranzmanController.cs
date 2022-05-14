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
    [Route("api/cvetniAranzman")]
    [Produces("application/json", "applciation/xml")]
    public class CvetniAranzmanController : ControllerBase
    {
        private readonly ICvetniAranzmanRepository cvetniAranzmanRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public CvetniAranzmanController(ICvetniAranzmanRepository cvetniAranzmanRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.cvetniAranzmanRepository = cvetniAranzmanRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<CvetniAranzman>> GetCvetniAranzmani()
        {
            var cvetniAranzmani = cvetniAranzmanRepository.GetAllCvetniAranzmani();
            if (cvetniAranzmani == null || cvetniAranzmani.Count == 0)
            {
                return NoContent();
            }

            return Ok(cvetniAranzmani);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{cvetniAranzmanId}")]
        public ActionResult<CvetniAranzman> GetCvetniAranzman(int cvetniAranzmanId)
        {
            var cvetniAranzman = cvetniAranzmanRepository.GetCvetniAranzmanById(cvetniAranzmanId);

            if (cvetniAranzman == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CvetniAranzman>(cvetniAranzman));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni")]
        [HttpPost]
        public ActionResult<CvetniAranzman> CreateCvetniAranzman([FromBody] CvetniAranzman cvetniAranzman)
        {
            try
            {

                CvetniAranzman cvetniAranzmanEntity = mapper.Map<CvetniAranzman>(cvetniAranzman);
                CvetniAranzman confirmation = cvetniAranzmanRepository.CreateCvetniAranzman(cvetniAranzmanEntity);
                cvetniAranzmanRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetCvetniAranzman", "CvetniAranzman", new { cvetniAranzmanId = confirmation.cvetniAranzmanID });


                return Created(location, mapper.Map<CvetniAranzman>(confirmation));

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
        public ActionResult<CvetniAranzman> UpdateCvetniAranzman([FromBody] CvetniAranzman cvetniAranzman)
        {
            try
            {
                var oldCvetniAranzman = cvetniAranzmanRepository.GetCvetniAranzmanById(cvetniAranzman.cvetniAranzmanID);

                if (oldCvetniAranzman == null)
                {
                    return NotFound();
                }

                CvetniAranzman cvetniAranzmanEntity = mapper.Map<CvetniAranzman>(cvetniAranzman);
                mapper.Map(cvetniAranzmanEntity, oldCvetniAranzman);
                cvetniAranzmanRepository.SaveChanges();

                return Ok(mapper.Map<CvetniAranzman>(cvetniAranzman));
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
        [HttpDelete("{cvetniAranzmanId}")]
        public IActionResult DeleteCvettniAranzman(int cvetniAranzmanId)
        {
            try
            {
                var cvetniAranzman = cvetniAranzmanRepository.GetCvetniAranzmanById(cvetniAranzmanId);

                if (cvetniAranzman == null)
                {
                    return NotFound();
                }

                cvetniAranzmanRepository.DeleteCvetniAranzman(cvetniAranzmanId);
                cvetniAranzmanRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
