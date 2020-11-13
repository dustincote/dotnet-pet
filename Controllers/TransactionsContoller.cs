using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public TransactionsController(ApplicationContext context) {
            _context = context;
        }
        //Sets the transactions to only show 10 at a time on the page
        [HttpGet("{start}/{limit}")]
        public IEnumerable<Transaction> getAllTransactions(int start, int limit)
        {
         Console.WriteLine("Getting Transactions");
         return _context.transactions.OrderBy(transaction => transaction.id).Skip(start).Take(limit).ToList();
        }

    }
}