using CoffeeAndWifi.WebApi.Models;
using CoffeeAndWifi.WebApi.Models.Response;
using CoffeeAndWifi.WebApi.Models.Converters;
using CoffeeAndWifi.WebApi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//dodać autentykację,  obsługę userów z JWT


namespace CoffeeAndWifi.WebApi.Controllers
{
    /// <summary>
    /// Cafe Controller
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CafeController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="unitOfWork"> UnitOfWork </param>
        public CafeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get All Cafes
        /// </summary>
        /// <returns> DataResponse - IEnumerable CafeDto </returns>
        [HttpGet("GetAll")]
        public DataResponse<IEnumerable<CafeDto>> Get()
        {
            var response = new DataResponse<IEnumerable<CafeDto>>();
            try
            {
                response.Data = _unitOfWork.Cafe.Get().ToDtos();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Get Cafe by ID
        /// </summary>
        /// <param name="id"> int Cafe.Id </param>
        /// <returns> DataResponse - Cafe </returns>
        [HttpGet("GetCafe/{id}")]
        public DataResponse<CafeDto> Get(int id)
        {
            var response = new DataResponse<CafeDto>();
            try
            {
                response.Data = _unitOfWork.Cafe.Get(id).ToDto();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Get Cafe by City
        /// </summary>
        /// <param name="city"> string Cafe.City </param>
        /// <returns> DataResponse - IEnumerable CafeDto </returns>
        [HttpGet("GetCafes/{city}")]
        public DataResponse<IEnumerable<CafeDto>> Get(string city)
        {
            var response = new DataResponse<IEnumerable<CafeDto>>();
            try
            {
                response.Data = _unitOfWork.Cafe.Get(city).ToDtos();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Add new Cafe
        /// </summary>
        /// <param name="cafeDto"> CafeDto </param>
        /// <returns> DataResponse - Added Cafe Id </returns>
        [HttpPost("Add"), Authorize(Roles = "User")]
        public DataResponse<int> Add(CafeDto cafeDto)
        {
            var response = new DataResponse<int>();
            try
            {
                var cafe = cafeDto.ToDao();
                _unitOfWork.Cafe.Add(cafe);
                _unitOfWork.Complete();
                response.Data = cafe.Id;
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Update Cafe (PUT)
        /// </summary>
        /// <param name="cafeDto"> Cafe cafe </param>
        /// <returns></returns>
        [HttpPut("Update"), Authorize(Roles = "User")]
        public Response Update(CafeDto cafeDto)
        {
            var response = new Response();
            try
            {
                _unitOfWork.Cafe.Update(cafeDto.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Update Cafe (PATCH)
        /// </summary>
        /// <param name="cafeDto"> Cafe cafe </param>
        /// <returns></returns>
        [HttpPatch("UpdateValue"), Authorize(Roles = "Admin")]
        public Response UpdatePatch(CafeDto cafeDto)
        {
            var response = new Response();
            try
            {
                _unitOfWork.Cafe.Update(cafeDto.ToDao());
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }

        /// <summary>
        /// Delete Cafe 
        /// </summary>
        /// <param name="id"> int Id </param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}"), Authorize(Roles = "Admin")]
        public Response Delete(int id)
        {
            var response = new Response();
            try
            {
                _unitOfWork.Cafe.Delete(id);
                _unitOfWork.Complete();
            }
            catch (Exception exception)
            {
                response.Errors.Add(new Error(exception.Source!, exception.Message));
            }
            return response;
        }
    }
}
