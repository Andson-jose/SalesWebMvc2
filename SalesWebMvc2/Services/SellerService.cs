using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc2.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc2.Services.Exceptions;

namespace SalesWebMvc2.Services
{
    public class SellerService
    {
        private readonly SalesWebMvc2Context _context;

        public SellerService(SalesWebMvc2Context context)
        {
            _context = context;
        }
        public async Task<List<Seller>> FindAllAsync()
        {
            return await  _context.Seller.ToListAsync();
        }
        public async Task InsertAsync(Seller obj)
        {
            
            _context.Add(obj);
            _context.SaveChangesAsync();
        }
        public async Task<Seller> FindByIdAsync(int id)
        {
            return await  _context.Seller. Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoveAsync(int id)
        {
           try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();  
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não Posso Deletar o Vendedor!");
            }
        }
        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundExceptions("Id not found");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
          
        } 
    }
}
