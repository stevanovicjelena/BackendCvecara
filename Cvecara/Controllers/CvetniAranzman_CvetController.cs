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
    [Route("api/cvetniAranzman_Cvet")]
    [Produces("application/json", "applciation/xml")]
    public class CvetniAranzman_CvetController : ControllerBase
    {
        private readonly ICvetniAranzman_CvetRepository cvetniAranzman_CvetRepository;
        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;
        private readonly ICvetRepository cvetRepository;
        private readonly ICvetniAranzmanRepository cvetniAranzmanRepository;

        public CvetniAranzman_CvetController(ICvetniAranzman_CvetRepository cvetniAranzman_CvetRepository, ICvetRepository cvetRepository, ICvetniAranzmanRepository cvetniAranzmanRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.cvetniAranzman_CvetRepository = cvetniAranzman_CvetRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;
            this.cvetRepository = cvetRepository;
            this.cvetniAranzmanRepository = cvetniAranzmanRepository;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<CvetniAranzman_Cvet>> GetCvetniAranzmani_Cvetovi()
        {
            var cvetniAranzmani_Cvetovi = cvetniAranzman_CvetRepository.GetAllCvetniAranzman_Cvet();
            if (cvetniAranzmani_Cvetovi == null || cvetniAranzmani_Cvetovi.Count == 0)
            {
                return NoContent();
            }

            foreach (CvetniAranzman_Cvet cvetniAranzman_Cvet in cvetniAranzmani_Cvetovi)
            {
                Cvet cvet = cvetRepository.GetCvetById(cvetniAranzman_Cvet.cvetID);
                cvetniAranzman_Cvet.cvet = cvet;

                CvetniAranzman cvetniAranzman = cvetniAranzmanRepository.GetCvetniAranzmanById(cvetniAranzman_Cvet.cvetniAranzmanID);
                cvetniAranzman_Cvet.cvetniAranzman = cvetniAranzman;
            }

            return Ok(cvetniAranzmani_Cvetovi);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet("{cvetniAranzman_CvetId}")]
        public ActionResult<CvetniAranzman_Cvet> GetCvetniAranzman_Cvet(int cvetniAranzman_CvetId)
        {
            var cvetniAranzman_Cvet = cvetniAranzman_CvetRepository.GetCvetniAranzman_CvetById(cvetniAranzman_CvetId);

            if (cvetniAranzman_Cvet == null)
            {
                return NotFound();
            }

            Cvet cvet = cvetRepository.GetCvetById(cvetniAranzman_Cvet.cvetID);
            cvetniAranzman_Cvet.cvet = cvet;

            CvetniAranzman cvetniAranzman = cvetniAranzmanRepository.GetCvetniAranzmanById(cvetniAranzman_Cvet.cvetniAranzmanID);
            cvetniAranzman_Cvet.cvetniAranzman = cvetniAranzman;

            return Ok(mapper.Map<CvetniAranzman_Cvet>(cvetniAranzman_Cvet));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpPost]
        public ActionResult<CvetniAranzman_Cvet> CreateCvetniAranzman_Cvet([FromBody] CvetniAranzman_Cvet cvetniAranzman_Cvet)
        {
            try
            {

                CvetniAranzman_Cvet cvetniAranzman_CvetEntity = mapper.Map<CvetniAranzman_Cvet>(cvetniAranzman_Cvet);
                CvetniAranzman_Cvet confirmation = cvetniAranzman_CvetRepository.CreateCvetniAranzman_Cvet(cvetniAranzman_CvetEntity);
                cvetniAranzman_CvetRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetCvetniAranzman_Cvet", "CvetniAranzman_Cvet", new { cvetniAranzman_CvetId = confirmation.cvetniAranzman_Cvet_ID });


                return Created(location, mapper.Map<CvetniAranzman_Cvet>(confirmation));
                
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
        public ActionResult<CvetniAranzman_Cvet> UpdateCvetniAranzman_Cvet([FromBody] CvetniAranzman_Cvet cvetniAranzman_Cvet)
        {
            try
            {
                var oldCvetniAranzman_Cvet = cvetniAranzman_CvetRepository.GetCvetniAranzman_CvetById(cvetniAranzman_Cvet.cvetniAranzman_Cvet_ID);

                if (oldCvetniAranzman_Cvet == null)
                {
                    return NotFound();
                }

                CvetniAranzman_Cvet cvetniAranzman_CvetEntity = mapper.Map<CvetniAranzman_Cvet>(cvetniAranzman_Cvet);
                mapper.Map(cvetniAranzman_CvetEntity, oldCvetniAranzman_Cvet);
                cvetniAranzman_CvetRepository.SaveChanges();

                return Ok(mapper.Map<CvetniAranzman_Cvet>(cvetniAranzman_Cvet));
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
        [HttpDelete("{cvetniAranzman_CvetId}")]
        public IActionResult DeleteCvetniAranzman_Cvet(int cvetniAranzman_CvetId)
        {
            try
            {
                var cvetniAranzman_Cvet = cvetniAranzman_CvetRepository.GetCvetniAranzman_CvetById(cvetniAranzman_CvetId);

                if (cvetniAranzman_Cvet == null)
                {
                    return NotFound();
                }

                cvetniAranzman_CvetRepository.DeleteCvetniAranzman_Cvet(cvetniAranzman_CvetId);
                cvetniAranzman_CvetRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
