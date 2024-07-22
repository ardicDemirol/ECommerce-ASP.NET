using ECommerce.Extensions;
using ECommerce.Interfaces;
using ECommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers;

[Route("api/portfolio")]
[ApiController]
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
    public async Task<IActionResult> GetUserPortfolio()
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var stock = await _stockRepository.GetBySymbolAsync(symbol);

        if (stock == null) return BadRequest("Stock not found!");

        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

        if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Stock already exists in your portfolio!");

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id,
        };

        await _portfolioRepository.CreateAsync(portfolioModel);

        if (portfolioModel == null)
        {
            return StatusCode(500, "Could not create!");
        }
        else
        {
            return Created();
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var userName = User.GetUserName();
        var appUser = await _userManager.FindByNameAsync(userName);

        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

        var filterStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

        if (filterStock.Count == 1) await _portfolioRepository.DeletePortfolio(appUser, symbol);
        else return BadRequest("Stock not found!");

        return Ok();
    }

}
