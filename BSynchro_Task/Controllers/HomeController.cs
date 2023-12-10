namespace BSynchro_Task.Controllers
{
    using BSynchro_Task.Models;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    public class HomeController : Controller
    {
        // create variables to store the path of account api and transaction api in it (these paths are from launchSettings.json files of each api project)

        private Uri accountApiUri = new Uri("https://localhost:7079/");
        private Uri transactionApiUri = new Uri("http://localhost:5208");

        // Create an instance from httpclient to be used for api calls

        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        // create the 3 static lists to store data in it during the runtime

        public static List<Customer> customers = new List<Customer>();
        public static List<Account> accounts = new List<Account>();
        public static List<Transaction> transactions = new List<Transaction>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        // on runtime the Customers List is generated as you want the customers to be already available

        public async Task<IActionResult> Index()
        {
            customers = await GenerateCustomersList();
            return View(customers);
        }

        // This Function will Generate the Customers List to be used as fixed data that could be updated by other methods action

        public async Task<List<Customer>> GenerateCustomersList() 
        {
            if (customers.Count == 0)
            {
                customers.Add(new Customer { CustomerId = 1, Name = "Mohammad", Surname = "Sultan", CreatedDate = DateTime.Now, NumberOfAccounts = 0 });
                customers.Add(new Customer { CustomerId = 2, Name = "Ali", Surname = "Sultan", CreatedDate = DateTime.Now.AddDays(1), NumberOfAccounts = 0 });
                customers.Add(new Customer { CustomerId = 3, Name = "Youssef", Surname = "Hassrouty", CreatedDate = DateTime.Now.AddDays(3), NumberOfAccounts = 0 });
                customers.Add(new Customer { CustomerId = 4, Name = "Ahmad", Surname = "Alwan", CreatedDate = DateTime.Now.AddDays(5), NumberOfAccounts = 0 });
            }
            return customers;
        }

        // This function will be used to open a new account for one of the existing customers in the list above

        [HttpPost]
        public async Task<Account> OpenNewAccount(int customerId, float initialCredit) 
        {
            Account account = new Account(); // create new instance from account model
            var customer = customers.Where(x => x.CustomerId == customerId); // get customer from list by using given customerId
            if (customer != null)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(accountApiUri + "api/Accounts/OpenNewAccount", customerId); // call account api and get response 
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync(); // read data fro response (data is account model)
                    account = JsonConvert.DeserializeObject<Account>(data); // deserailize the account json object and set it in the already created account object
                    if (account != null)
                    {
                        accounts.Add(account);// add the account to the accounts list
                        customers[customerId - 1].NumberOfAccounts += 1; // add number of accounts of the customer that account is created for him
                        if (initialCredit != 0)
                        {
                            var transaction = await AddNewTransaction(account.AccountId,initialCredit); // apply the method of adding transaction to the added account
                        }
                    }
                }
            }
            return account;
        }

        // This Function is used to Add new Transaction to already new added account from the accounts list declared above

        [HttpPost]
        public async Task<Transaction> AddNewTransaction(int accountId, float initialCredit)
        {
            Transaction transaction = new Transaction(); // create new instance of transaction object model
            if (accountId > 0)
            {
                TransactionParams requestData = new TransactionParams { AccountId = accountId, InitialCredit = initialCredit };// create request data 
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(transactionApiUri + "api/Transactions/AddingNewTransaction", requestData);// call transaction api and get response 
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync(); // read data fro response (data is transaction model)
                    transaction = JsonConvert.DeserializeObject<Transaction>(data); // deserailize the transaction json object and set it in the already created transaction object
                    if (transaction != null)
                    {
                        transactions.Add(transaction); // add the created transaction to the transactions list
                    }
                }
            }
            return transaction;
        }

        // This method is used to Display the Customer information Name, Surname, Balance and also all transactions of his account

        [HttpGet]
        public async Task<IActionResult> DisplayCustomerInfos(int  customerId) 
        {
            CustomerInformation customerInformation = new CustomerInformation(); // create new instance of CustomerInformation Moedel
            List<Transaction> totalCustomerTransactions = new List<Transaction>(); // Create new list of transactions to store the Transactions of this customer inside it
            float customerBalance = 0; // set balance to 0
            var customer = customers.FirstOrDefault(c => c.CustomerId == customerId); // Get Customer from list by his Customer Id
            var customerAccounts = accounts.Where(acc => acc.CustomerId == customerId).ToList(); // Get All created accounts of this customer from accounts list
            foreach (var account in customerAccounts)
            {
                var customerTransactions = transactions.Where(t => t.AccountId == account.AccountId).ToList(); // Get transactions done on each account of this Customer
                foreach(var transaction in customerTransactions)
                {
                    customerBalance += transaction.InitialCreditValue; // The Balance will sum all the transactions value available for all accounts of the Customer
                    totalCustomerTransactions.Add(transaction); // Add each appeared transaction to the list of transactions of this Customer
                }
            }
            
            //Fill the Data in the CustomerInformation object created and then return it in the view

            customerInformation.Name = customer.Name;
            customerInformation.Surname = customer.Surname;
            customerInformation.Balance = customerBalance;
            customerInformation.Transactions = totalCustomerTransactions;
            return View(customerInformation);
        }

    }
}