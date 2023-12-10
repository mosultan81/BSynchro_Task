namespace Transactions_API.Controllers
{
    using Transactions_API.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Transactions_API.Models;

    [Route("api/Transactions/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionRepository _transactionRepository;

        // create new instance of the repository to use its function

        public TransactionController(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        // Implementing a function that post new Transaction by calling method from repository

        [HttpPost]
        public async Task<IActionResult> AddingNewTransaction([FromBody] TransactionParams tParams)
        {
            var transaction = await _transactionRepository.AddNewTransaction(tParams.AccountId, tParams.InitialCredit);
            return Ok(transaction);
        }
    }
}
