using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaP1.Data;
using ProvaP1.Models;

namespace ProvaP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendasController : ControllerBase
    {
        private readonly ProvaP1Context _context;

        public VendasController(ProvaP1Context context)
        {
            _context = context;
        }

        // GET: api/Vendas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVenda()
        {
            if (_context.Venda == null)
            {
                return NotFound();
            }
            return await _context.Venda.Include(v => v.Cliente).Include(v => v.Produto).ToListAsync();
        }

        // GET: api/Vendas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Venda>> GetVenda(int id)
        {
            if (_context.Venda == null)
            {
                return NotFound();
            }
            var venda = await _context.Venda.Include(v => v.Cliente).Include(v => v.Produto).Where(v => v.Id == id).FirstOrDefaultAsync();

            if (venda == null)
            {
                return NotFound();
            }

            return venda;
        }

        // PUT: api/Vendas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenda(int id, Venda venda)
        {
            if (id != venda.Id)
            {
                return BadRequest();
            }

            _context.Entry(venda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vendas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Venda>> PostVenda(int clienteId, int produtoId, int quantidade)
        {
            if (_context.Venda == null || _context.Estoque == null || _context.Produto == null)
            {
                return Problem("Entity set 'ProvaP1Context.Venda'  is null.");
            }

            if (quantidade <= 0)
            {
                return BadRequest("Quantidade deve ser maior que 0.");
            }

            Estoque? estoque = await _context.Estoque.FirstOrDefaultAsync(e => e.Produto.Id == produtoId);

            if (estoque == null)
            {
                return NotFound("Nenhum estoque encontrado para o determinado produto.");
            }

            Cliente? cliente = await _context.Cliente.FindAsync(clienteId);

            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            Produto? produto = await _context.Produto.FindAsync(produtoId);

            if (produto == null)
            {
                return NotFound("Nenhum produto encontrado.");
            }

            Venda venda = new Venda()
            {
                Cliente = cliente,
                Produto = produto,
                Quantidade = quantidade
            };

            if (quantidade > estoque.Quantidade)
            {
                return BadRequest();
            }

            estoque.DecrementarQuantidade(quantidade);

            _context.Estoque.Update(estoque);
            _context.Venda.Add(venda);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVenda", new { id = venda.Id }, venda);
        }

        // DELETE: api/Vendas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenda(int id)
        {
            if (_context.Venda == null)
            {
                return NotFound();
            }
            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }

            _context.Venda.Remove(venda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendaExists(int id)
        {
            return (_context.Venda?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
