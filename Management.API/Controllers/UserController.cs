using Management.BusinessLogic;
using Management.Data.DTOs;
using Management.Data.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Management.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize] //authorize data annotation to be applied in all methods
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //get the user
        [HttpGet]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                return Ok(await _userService.GetUser(userId));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //update user details (not entire user details)
        [HttpPatch]
        public async Task<IActionResult> Update(UpdateUserRequest updateUserRequest)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var result = await _userService.UpdateUser(userId, updateUserRequest);
                return NoContent(); //204
            }
            catch (MissingMemberException mmex)
            {
                return BadRequest(mmex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        //delete the user
        //must be both regular and admin to handle delete
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        // [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return NoContent(); //404
            }
            catch (MissingMemberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
