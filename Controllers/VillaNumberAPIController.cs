using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/VillaNumberAPI")]


    [ApiController]
    public class VillaNumberAPIController : ControllerBase

    {
        protected APIResponse _response;
        private readonly IVillaNumberRepository _VillaNumber;
        private readonly IMapper _mapper;

        public VillaNumberAPIController(IVillaNumberRepository VillaNumber, IMapper mapper)
        {
            _VillaNumber = VillaNumber;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumberVillas()
        {
            try { 
                IEnumerable<VillaNumber> villaNumberList = await _VillaNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        [HttpGet("{VillaNo:int}", Name = "GetNumberVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetNumberVilla(int VillaNo)
        {
            try
            {
                if (VillaNo == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villaNumber = await _VillaNumber.GetAsync(u => u.VillaNo == VillaNo);

                if (villaNumber == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateNumberVilla([FromBody] VillaNumberCreateDTO villaNumberCreateDTO)
        {
            try
            {

                if (await _VillaNumber.GetAsync(item => item.VillaNo == villaNumberCreateDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("CustomErrorMessage", "VillaNumber already Exists");
                    return BadRequest(ModelState);
                }
                if (villaNumberCreateDTO == null)
                {
                    return BadRequest(villaNumberCreateDTO);
                }

                VillaNumber villaNumber = _mapper.Map<VillaNumber>(villaNumberCreateDTO);

                await _VillaNumber.CreateAsync(villaNumber);
                _response.Result = _mapper.Map<VillaNumberDTO>(villaNumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetNumberVilla", new { VillaNo = villaNumber.VillaNo }, _response);

            } catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{VillaNo:int}", Name = "DeleteVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteNumberVilla(int VillaNo)
        {
            try
            {
                if (VillaNo == 0)
                {
                    return BadRequest();
                }


                var villaNumber = await _VillaNumber.GetAsync(item => item.VillaNo == VillaNo);
                if (villaNumber == null)
                {
                    return NotFound();
                }

                await _VillaNumber.RemoveAsync(villaNumber);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{VillaNo:int}", Name = "UpdateVillaNumber")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateNumberVilla(int villaNumber, [FromBody] VillaNumberUpdateDTO villaNumberUpdateDTO)
        {
            try
            {
                if (villaNumberUpdateDTO == null || villaNumber != villaNumberUpdateDTO.VillaNo)
                {
                    return BadRequest();
                }
                VillaNumber model = _mapper.Map<VillaNumber>(villaNumberUpdateDTO);

                await _VillaNumber.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{VillaNo:int}", Name = "UpdatePartialNumberVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePartialNumberVilla(int VillaNo, JsonPatchDocument<VillaNumberUpdateDTO> patchDTO)
        {
            if (patchDTO == null || VillaNo == 0)
            {
                return BadRequest();
            }
            var villaNumber = await _VillaNumber.GetAsync(item => item.VillaNo == VillaNo, tracked: false);

            VillaNumberUpdateDTO VillaNumberDTO = _mapper.Map<VillaNumberUpdateDTO>(villaNumber);

            if (villaNumber == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(VillaNumberDTO, ModelState);
            VillaNumber model = _mapper.Map<VillaNumber>(VillaNumberDTO);

            await _VillaNumber.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
