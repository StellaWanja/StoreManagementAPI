using System;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Management.BusinessLogic;
using Management.Data;
using Management.Models;
using Management.Data.DTOs.StoreDTOs;
using Management.Data.DTOs.Mappings;

namespace Management.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] //authorize data annotation to be applied in all methods

    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService ?? throw new ArgumentNullException(nameof(storeService));
        }

        //get store
        [HttpGet]
        public async Task<ActionResult<GetStoreResponse>> GetStore([FromQuery] string storeId)
        {
            try
            {
                var store = await _storeService.DisplayStore(storeId);
                var result = StoreMappings.GetStoreResponse(store);
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
        public async Task<IActionResult> AddStore(AddStoreRequest addStore)
        {
            try
            {
                //use storeactions method
                var store = await _storeService.CreateStore(addStore.StoreName, addStore.StoreNumber, addStore.StoreType, addStore.StoreProducts, addStore.UserId);
                //pass through mapping
                var result = StoreMappings.AddStoreResponse(store);
                //create new store
                return CreatedAtAction(nameof(GetStore), new { storeId = store.Id }, result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //update an entire store/resource
        [HttpPut]
        public async Task<IActionResult> UpdateStoreUsingPut(PutStoreRequest putRequest, [FromQuery] string storeId, string userId)
        {
            try
            {
                var result = await _storeService.UpdateStoreUsingPut(putRequest, storeId, userId);
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
        public async Task<IActionResult> UpdateStoreUsingPatch(PatchStoreRequest patchRequest, [FromQuery] string storeId, string userId)
        {
            try
            {
                var result = await _storeService.UpdateStoreUsingPatch(patchRequest, storeId, userId);
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
        public async Task<ActionResult<Store>> DeleteStore([FromQuery] string storeId)
        {
            try
            {
                //find user id
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                //remove store
                var result = await _storeService.RemoveStore(storeId, userId);
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