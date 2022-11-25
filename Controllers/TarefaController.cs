using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using desafio_api.Context;
using desafio_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class TarefaController :ControllerBase
    {
        private readonly AppTarefaDbContext _context;
        public TarefaController(AppTarefaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos() => Ok(await _context.Tarefas.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if(tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public async Task<IActionResult> ObterPorData(DateTime dataModel)
        {
            var data = _context.Tarefas.Where(x => x.Data == dataModel);

            return Ok(data);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> ObterPorStatus(EnumStatus statusModel)
        {
            var status = _context.Tarefas.Where(x => x.Status == statusModel);

            if(status == null)
                return NotFound();

            return Ok(status);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo(string tituloModel)
        {
            var titulo = _context.Tarefas.Where(x => x.Titulo == tituloModel);
            if(titulo == null)
                return NotFound();

            return Ok(titulo);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
             if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if(tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
            
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Titulo = tarefa.Titulo;

            _context.Tarefas.Update(tarefaBanco);
            await _context.SaveChangesAsync();

            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            _context.Tarefas.Remove(tarefaBanco);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }
    }
}