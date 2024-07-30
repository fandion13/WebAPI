using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Threading.Tasks;
using WebAPI.Camadas.Entidades;
using WebAPI.Camadas.Negocios;
using Workflowapi.util;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly NTask _businessLogicLayer;

        public TaskController(NTask businessLogicLayer)
        {
            _businessLogicLayer = businessLogicLayer;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            try
            {
                var retorno = _businessLogicLayer.GetAllTasks();
               
                //return Ok(tasks); test lint
                return Ok(new RetornoREST<TaskModel>() { Error = 0, Mensagem = "Successfully performed operation", Dados = retorno });
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest(new RetornoSingleREST<TaskModel>() { Error = 1, Mensagem = ex.Message, Dados = null });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            try
            {
                TaskModel retorno = _businessLogicLayer.GetTaskById(id);

                if (retorno != null)
                {
                    //return Ok(task);
                    return Ok(new RetornoSingleREST<TaskModel>() { Error = 0, Mensagem = "Successfully performed operation!", Dados = retorno });
                }

                //return NotFound("Task not found.");
                throw new ArgumentException("Task not found.");
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest(new RetornoSingleREST<TaskModel>() { Error = 1, Mensagem = ex.Message, Dados = null });
            }
        }

        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskModel task)
        {
            try
            {
                TaskModel retorno = _businessLogicLayer.CreateTask(task);

                //return Ok("Task created successfully.");
                return Ok(new RetornoSingleREST<TaskModel>() { Error = 0, Mensagem = "Task created successfully.", Dados = retorno });
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest(new RetornoSingleREST<TaskModel>() { Error = 1, Mensagem = ex.Message, Dados = null });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskModel task)
        {
            try
            {
                task.Id = id;

                var existingTask = _businessLogicLayer.GetTaskById(id);

                if (existingTask == null)
                {
                    throw new ArgumentException("Task not found.");
                }

                _businessLogicLayer.UpdateTask(id, task);

                //return Ok("Task updated successfully.");
                return Ok(new RetornoSingleREST<TaskModel>() { Error = 0, Mensagem = "Task updated successfully.", Dados = task });
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest(new RetornoSingleREST<TaskModel>() { Error = 1, Mensagem = ex.Message, Dados = null });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                TaskModel task = new TaskModel();
                task.Id = id;

                _businessLogicLayer.DeleteTask(id);

                //return Ok("Task deleted successfully.");
                return Ok(new RetornoSingleREST<TaskModel>() { Error = 0, Mensagem = "Task deleted successfully.", Dados = task });
            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message);
                return BadRequest(new RetornoSingleREST<TaskModel>() { Error = 1, Mensagem = ex.Message, Dados = null });
            }
        }

    }
}