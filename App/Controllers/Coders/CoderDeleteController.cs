using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderDeleteController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        /*public string Error = "Server Error: The request has not been resolve";*/
        public CoderDeleteController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }
        
        // ==> Acción para "eliminar" un Coder cambiando su estado a "inactivo"
        [HttpDelete]
        [Route("RiwiTalent/{id:length(24)}/Delete")] // Define la ruta y el tipo de método HTTP que este endpoint acepta (DELETE)
        public async Task<IActionResult> delete(string id)
        {
            try
            {                
                _coderRepository.delete(id);// Llama al método delete del repositorio, que marca el Coder como "inactivo"                
                return Ok(new { Message = "El estado del Coder ha sido cambiado con éxito a Inactive" });// Retorna ok un mensaje "Coder inactivado con éxito", lo que significa que la operación fue exitosa
            }
            catch (Exception)
            {
                // Si ocurre un error, retorna un estado 500 Internal Server Error con un mensaje de error
                return StatusCode(500, "Error en el servidor: No se pudo completar la solicitud.");
            }
        }
    [HttpPut]
    [Route("RiwiTalent/{id:length(24)}/Reactivate")]
    public IActionResult Reactivate(string id)
    {
        try
        {
            _coderRepository.ReactivateCoder(id);// Llama al método ReactivateCoder del repositorio, que cambia el estado del Coder a "activo"
            return Ok(new { Message = "El estado del Coder ha sido cambiado con éxito a Active" });// Retorna un estado 200 OK con un mensaje de éxito
        }
        catch (Exception)
        {
            
            return StatusCode(500, "Error en el servidor: No se pudo completar la solicitud.");// Si ocurre un error, retorna un estado 500 Internal Server Error con un mensaje de error   
        }
    }
    }
}


    