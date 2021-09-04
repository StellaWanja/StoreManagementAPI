using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Management.BusinessLogic;
using Management.Data;
using Management.Models;
using Management.Data.DTOs.ProductDTOs;
using Management.Data.DTOs.Mappings;

namespace Management.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] //authorize data annotation to be applied in all methods

    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        //get store
        [HttpGet]
        public async Task<ActionResult<GetProductResponse>> GetProduct([FromQuery] string productId)
        {
            try
            {
                var product = await _productService.DisplayProduct(productId);
                var result = ProductMappings.GetProductResponse(product);
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return NotFound("Resource does not exist");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //create new store
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequest addProduct)
        {
            try
            {
                //use productactions method
                var product = await _productService.CreateProduct(addProduct.StoreId, addProduct.ProductName, addProduct.Price);
                //pass through mapping
                var result = ProductMappings.AddProductResponse(product);
                //create new store
                return CreatedAtAction(nameof(GetProduct), new { productId = product.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //update an entire store/resource
        [HttpPut]
        public async Task<IActionResult> UpdateProductUsingPut(PutProductRequest putRequest, [FromQuery] string storeId, string productId)
        {
            try
            {
                var result = await _productService.UpdateProductUsingPut(putRequest, storeId, productId);
                if (result)
                {
                    //return no content - has been deleted
                    return NoContent();
                }

                return NotFound("Resource not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //update an entire store/resource
        [HttpPatch]
        public async Task<IActionResult> UpdateProductUsingPatch(PatchProductRequest patchRequest, [FromQuery] string storeId, string productId)
        {
            try
            {
                var result = await _productService.UpdateProductUsingPatch(patchRequest, storeId, productId);
                if (result)
                {
                    //return no content - has been deleted
                    return NoContent();
                }

                return NotFound("Resource not found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //delete store
        [HttpDelete]
        public async Task<ActionResult<Store>> DeleteProduct([FromQuery] string productId)
        {
            try
            {
                //remove store
                var result = await _productService.RemoveProduct(productId);
                if (result)
                {
                    //return no content - has been deleted
                    return NoContent();
                }

                return NotFound("Resource not found");
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}