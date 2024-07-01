using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;

            _portfolioRepository = portfolioRepository;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetPortfolio()
        {
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);
            var userportfolio = await _portfolioRepository.GetUserPortfolio(AppUser);
            return Ok(userportfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddStockToPortfolio(string symbol)
        {
            
            var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);
            var stock_ = await _stockRepository.GetStockBySymbol(symbol);
            if (stock_ == null)
            {
                return BadRequest("Failed to add stock to portfolio");
            }
          
            var userportfolio = await _portfolioRepository.GetUserPortfolio(AppUser);
            if(userportfolio.Any(x =>x.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Already exist");

            var portfolioModel = new Portfolio
            {
                StockId = stock_.id,
                AppUserId = AppUser.Id
            };

           var portfolio =  await _portfolioRepository.CreatePortfolioAsync(portfolioModel);
             if(portfolio == null) return BadRequest("Failed to add stock to portfolio");
             return Ok(portfolio);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteStockFromPortfolio(string symbol  )
        {
           var username = User.GetUsername();
            var AppUser = await _userManager.FindByNameAsync(username);

            var portfolio = await _portfolioRepository.GetUserPortfolio(AppUser);
            var filteredStock = portfolio.Where(x => x.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(filteredStock.Count() > 0){
                await _portfolioRepository.DeletePortfolioAsync(AppUser, symbol);
            }else{
                return BadRequest("Failed to delete stock from portfolio");
            }
            return Ok();
        }
    }
}