using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using apiManicure.Infra;
using apiManicure.Models;

namespace apiManicure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Agendamento")]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentosPorData([FromQuery] DateTime? data)
        {
            if (data == null)
                return BadRequest("A data é obrigatória para buscar os agendamentos.");

            var agendamentos = await _context.Agendamentos
                .Where(a => a.DataAgendamento.Date == data.Value.Date)
                .ToListAsync();

            if (agendamentos.Count == 0)
                return NotFound("Nenhum agendamento encontrado para essa data.");

            return Ok(agendamentos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarAgendamento(int id, [FromBody] Agendamento agendamentoAtualizado)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
                return NotFound("Agendamento não encontrado.");

            try
            {
                agendamento.ClienteNome = agendamentoAtualizado.ClienteNome;
                agendamento.DataAgendamento = agendamentoAtualizado.DataAgendamento;
                agendamento.Servico = agendamentoAtualizado.Servico;

                if (await _context.SaveChangesAsync() > 0)
                {
                    await RegistrarLog("Sucesso", $"Agendamento ID {id} atualizado.");
                    return Ok("Agendamento atualizado com sucesso.");
                }
                else
                {
                    await RegistrarLog("Erro", $"Falha ao atualizar agendamento ID {id}.");
                    return StatusCode(500, "Erro ao atualizar agendamento.");
                }
            }
            catch (Exception ex)
            {
                await RegistrarLog("Erro", $"Exceção ao atualizar agendamento ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno ao atualizar o agendamento.");
            }
        }

        [HttpPost("CriarAgendamento")]
        public async Task<IActionResult> CriarAgendamento([FromBody] Agendamento agendamento)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (agendamento == null)
                return BadRequest("Os dados do agendamento são obrigatórios.");

            try
            {
                _context.Agendamentos.Add(agendamento);
                if (await _context.SaveChangesAsync() > 0)
                {
                    var logSucesso = new LogTransacao
                    {
                        Tipo = "Sucesso",
                        Descricao = $"Agendamento criado para {agendamento.ClienteNome} no dia {agendamento.DataAgendamento.Date} as {agendamento.DataAgendamento.TimeOfDay}"
                    };

                    _context.LogTransacoes.Add(logSucesso);
                    await _context.SaveChangesAsync();

                    return Ok("Agendamento registrado com sucesso.");
                }
                else
                {
                    var logErro = new LogTransacao
                    {
                        Tipo = "Erro",
                        Descricao = $"Falha ao criar agendamento para {agendamento.ClienteNome} no dia {agendamento.DataAgendamento}. Motivo desconhecido."
                    };

                    _context.LogTransacoes.Add(logErro);
                    await _context.SaveChangesAsync();

                    return StatusCode(500, "Erro ao registrar agendamento.");
                }
            }
            catch (Exception ex)
            {
                var logExcecao = new LogTransacao
                {
                    Tipo = "Erro",
                    Descricao = $"Exceção ao criar agendamento para {agendamento.ClienteNome}: {ex.Message}"
                };

                _context.LogTransacoes.Add(logExcecao);
                await _context.SaveChangesAsync();

                return StatusCode(500, "Erro interno ao processar o agendamento.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAgendamento(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
                return NotFound("Agendamento não encontrado.");

            try
            {
                _context.Agendamentos.Remove(agendamento);

                if (await _context.SaveChangesAsync() > 0)
                {
                    await RegistrarLog("Sucesso", $"Agendamento ID {id} excluído.");
                    return Ok("Agendamento excluído com sucesso.");
                }
                else
                {
                    await RegistrarLog("Erro", $"Falha ao excluir agendamento ID {id}.");
                    return StatusCode(500, "Erro ao excluir agendamento.");
                }
            }
            catch (Exception ex)
            {
                await RegistrarLog("Erro", $"Exceção ao excluir agendamento ID {id}: {ex.Message}");
                return StatusCode(500, "Erro interno ao excluir o agendamento.");
            }
        }

        private bool AgendamentoExists(int id)
        {
            return _context.Agendamentos.Any(e => e.Id == id);
        }

        private async Task RegistrarLog(string tipo, string descricao)
        {
            _context.LogTransacoes.Add(new LogTransacao { Tipo = tipo, Descricao = descricao });
            await _context.SaveChangesAsync();
        }
    }
}
