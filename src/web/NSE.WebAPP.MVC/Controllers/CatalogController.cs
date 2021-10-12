using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSE.WebAPP.MVC.Services;

namespace NSE.WebAPP.MVC.Controllers
{
    public class CatalogController : MainController
    {
        private readonly ICatalogServiceRefit _catalogService;

        public CatalogController(ICatalogServiceRefit catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("showcase")]
        public async Task<IActionResult> Index()
        {
            var produtos = await _catalogService.GetAll();

            return View(produtos);
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            var produto = await _catalogService.GetById(id);

            return View(produto);
        }
    }
}
