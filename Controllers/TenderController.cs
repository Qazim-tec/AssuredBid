﻿using AssuredBid.Services.Iservice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IO;
using AssuredBid.Models;
using AssuredBid.DTOs;

namespace AssuredBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenderController : ControllerBase
    {
        private readonly ITenderService tenderService;

        public TenderController(ITenderService tender)
        {
            this.tenderService = tender;
        }

        /// <summary>
        /// get tenders with limit = number of tenders to generate
        /// </summary>
        /// <param name="limit"></param>
        /// <returns>The List of TENDERS</returns>
        [HttpGet]
        [Route("GetTendersWithLimitAndStages")]

        public async Task<IActionResult> GetTenders(int limit)
        {
            var response = await tenderService.GetTendersByLimits(limit);
            return Ok(response);
        }

        /// <summary>
        /// This Endpoint is used to push CreateTenders to the users
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        //[HttpPost("SendTendersToUsers")]

        //public async Task<IActionResult> SendTenders(int limit, string stage)
        //{
        //    var response = await tenderService.GetTendersByLimitsAndStages(limit, stage);
        //    return Ok("The Tenders have been pushed successfully");
        //}

        /// <summary>
        /// This Endpoint is used to create new tenders
        /// </summary>
        /// <param name="tender"></param>
        /// <returns></returns>
        [HttpPost("CreateNewTender")]

        public async Task<IActionResult> CreateNewTenders([FromBody] CreateTenderDTO tender)
        {
            var newTender = await tenderService.AddTender(tender);
            return CreatedAtAction(nameof(GetTenderById), newTender);

        }

        /// <summary>
        /// This Enpoint is used to get tender by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetTenderById")]
        public async Task<IActionResult> GetTenderById(Guid id)
        {
            var tender = await tenderService.GetTenderById(id);
            if (tender == null) return NotFound();
            return Ok(tender);
        }

        /// <summary>
        /// This Endpoint is used to update tender by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tender"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateTender(Guid id, [FromBody] CreateTenders tender)
        {
            if (id != tender.Id) return BadRequest();
            var updatedTender = await tenderService.UpdateTender(tender);
            return Ok(updatedTender);
        }

        /// <summary>
        /// This is used to delete tender by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTender(Guid id)
        {
            var result = await tenderService.DeleteTender(id);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// This Endpoint is used to get all the created tenders
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTenders()
        {
            var tenders = await tenderService.GetAllTenders();
            return Ok(tenders);
        }

    }
}
