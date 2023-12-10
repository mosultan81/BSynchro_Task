namespace Accounts_API.Controllers
{
    using Accounts_API.Repositories;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/Accounts/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // create new instance of the repository to use its function

        private readonly AccountRepository _accountRepository;

        public AccountController(AccountRepository userRepository)
        {
            _accountRepository = userRepository;
        }

        // Implementing a function that post new account by calling method from repository

        [HttpPost]
        public async Task<IActionResult> OpenNewAccount([FromBody] int customerId)
        {
            var account = await _accountRepository.OpenNewAccount(customerId);
            return Ok(account);
        }
    }
}
