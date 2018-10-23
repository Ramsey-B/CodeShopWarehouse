using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeShopWarehouse.Web.Models;
using CodeShopWarehouse.Business;
using CodeShopWarehouse.Entities;

namespace CodeShopWarehouse.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FillOrderController : Controller
    {
        private readonly FillOrderService _fillOrderService;

        public FillOrderController(FillOrderService fillOrderService)
        {
            _fillOrderService = fillOrderService;
        }

        [HttpGet]
        public IActionResult GetUnprocessedFillOrders()
        {
            return Ok(_fillOrderService.GetUnProcessedFillOrders());
        }

        [HttpGet("{fillOrderId}")]
        public IActionResult GetFillOrderById(int fillOrderId)
        {
            return Ok(_fillOrderService.GetFillOrderById(fillOrderId));
        }

        [HttpGet("product/{productId}")]
        public IActionResult GetFillOrderByProductId(int productId)
        {
            return Ok(_fillOrderService.GetFillOrdersByProductId(productId));
        }

        [HttpPost]
        public IActionResult CreateFillOrder([FromBody]FillOrder createdFillOrder)
        {
            try
            {
                return Created("", _fillOrderService.CreateFillOrder(createdFillOrder));
            }
            catch(Exception exception)
            {
                return NotFound(exception.Message);
            }
        }

        [HttpPut]
        public IActionResult ProcessFillOrder([FromBody]FillOrder submittedFillOrder)
        {
            try
            {
                _fillOrderService.ProcessFillOrder(submittedFillOrder);
                return Ok();
            }
            catch(Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}
