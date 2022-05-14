﻿using AutoMapper;
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
    [Route("api/porudzbina")]
    [Produces("application/json", "applciation/xml")]
    public class PorudzbinaController : ControllerBase 
    {
        private readonly IPorudzbinaRepository porudzbinaRepository;

        private readonly LinkGenerator linkGenerator;
        private readonly IMapper mapper;

        public PorudzbinaController(IPorudzbinaRepository porudzbinaRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this.porudzbinaRepository = porudzbinaRepository;
            this.linkGenerator = linkGenerator;
            this.mapper = mapper;

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize(Roles = "Zaposleni")]
        [HttpGet]
        [HttpHead]
        public ActionResult<List<Porudzbina>> GetPorudzbine()
        {
            var porudzbine = porudzbinaRepository.GetAllPorudzbine();
            if (porudzbine == null || porudzbine.Count == 0)
            {
                return NoContent();
            }

            return Ok(porudzbine);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpGet("{porudzbinaId}")]
        public ActionResult<Porudzbina> GetPorudzbina(int porudzbinaId)
        {
            var porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaId);

            if (porudzbina == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Porudzbina>(porudzbina));
        }


        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("application/json")]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpPost]
        public ActionResult<Porudzbina> CreatePorudzbina([FromBody] Porudzbina porudzbina)
        {
            try
            {

                Porudzbina porudzbinaEntity = mapper.Map<Porudzbina>(porudzbina);
                Porudzbina confirmation = porudzbinaRepository.CreatePorudzbina(porudzbinaEntity);
                porudzbinaRepository.SaveChanges();

                string location = linkGenerator.GetPathByAction("GetPorudzbina", "Porudzbina", new { porudzbinaId = confirmation.porudzbinaID });


                return Created(location, mapper.Map<Porudzbina>(confirmation));

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
        public ActionResult<Porudzbina> Updateporudzbina([FromBody] Porudzbina porudzbina)
        {
            try
            {
                var oldPorudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbina.porudzbinaID);

                if (oldPorudzbina == null)
                {
                    return NotFound();
                }

                Porudzbina porudzbinaEntity = mapper.Map<Porudzbina>(porudzbina);
                mapper.Map(porudzbinaEntity, oldPorudzbina);
                porudzbinaRepository.SaveChanges();

                return Ok(mapper.Map<Porudzbina>(porudzbina));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Update Error");
            }
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Zaposleni, Kupac")]
        [HttpDelete("{porudzbinaId}")]
        public IActionResult Deleteporudzbina(int porudzbinaId)
        {
            try
            {
                var porudzbina = porudzbinaRepository.GetPorudzbinaById(porudzbinaId);

                if (porudzbina == null)
                {
                    return NotFound();
                }

                porudzbinaRepository.DeletePorudzbina(porudzbinaId);
                porudzbinaRepository.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Delete Error");
            }
        }
    }
}
