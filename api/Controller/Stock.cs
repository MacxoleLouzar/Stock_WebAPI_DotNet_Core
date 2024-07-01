using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Dtos.StockDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Model;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class Stock : ControllerBase
    {
        // private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        //constructor
        public Stock(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepository.GetAllStocksAsync(query);
            var stockDto = stock.Select(s => s.ToStockDto()).ToList();
            return Ok(stockDto);
        }

        [HttpGet("id:int")]
        public async Task<IActionResult> GetById(int id)
        {
           if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = await _stockRepository.GetStockByIdAsync(id);
            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
           if (!ModelState.IsValid) return BadRequest(ModelState);

            var stockModel = stockDto.ToCreateStockRequestDto();
            await _stockRepository.CreateStockAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
           if (!ModelState.IsValid) return BadRequest(ModelState);

            var stock = updateStockDto.ToUpdateStockRequestDto();
            var existingStock = await _stockRepository.UpdateStockAsync(id, stock);
            if (existingStock == null)
            {
                return NotFound();
            }
            return Ok(existingStock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var stock = await _stockRepository.DeleteStockAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}