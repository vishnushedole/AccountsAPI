using AccountsWebAPI.Model;
using AccountsWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        AccountRepository accountRepository=new AccountRepository();
        [HttpGet("details/{id}")]
        public ActionResult<Account> GetById(int id)
        {
            var acc= accountRepository.GetAccountById(id);
            if(acc is null || acc.IsActive==0) return NotFound();
            else return Ok(acc);
        }

        [HttpPut("passbook/{id}")]
        public IActionResult UpdatePassbookById(int id)
        {
            var acc = accountRepository.GetAccountById(id);
            if (acc is null) return NotFound();
            accountRepository.UpdatePassbook(id);
            return Ok();
           
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAccountById(int id)
        {
            var acc = accountRepository.GetAccountById(id);
            if (acc is null) return NotFound();
            accountRepository.DeleteAccount(id);
            return Ok();

        }

        [HttpGet("list/{id}")]
        public ActionResult<List<Account>> GetAllAccounts(int id)
        {
            var list = accountRepository.ListAll(id);
            if (list is null) return NotFound();

            else return Ok(list);
                
        }

        [HttpGet("transactionlist/{id}")]
        public ActionResult<List<Transactions>> GetAllTranactions(int id)
        {
            var list=accountRepository.ListAllTransaction(id);
            if (list is null) return NotFound();
            else return Ok(list);

        }


        [HttpGet("Benifitiarylist/{id}")]
        public ActionResult<BeneficiaryModel>GetAllBenifitiary(int id)
        {
            var list=accountRepository.ListBenifitiary(id);
            if (list is null) return NotFound();
            else
            {
                BeneficiaryModel model = new BeneficiaryModel();
                model.AccountId = list;
                return Ok(model);
            }
        }
        [HttpPost("AddBenifitiary")]
        public IActionResult AddBenifitiary(int accId,int benifitiaryId)
        {
            if (accountRepository.CheckBenificiary(accId, benifitiaryId))
                return Ok();

            return BadRequest();
        }
        
        [HttpPost("FundTransfer")]
        public IActionResult FundTransfer(int accId,int benifitiaryId,decimal amount)
        {

            bool check1 = accountRepository.CheckBenificiary(accId, benifitiaryId);
             if(check1 && (accId!=benifitiaryId))
             {
                 bool check2 = accountRepository.CheckTransactionAllowOrNot(accId, amount);
                 if(check2)
                 {
                     bool check3 = accountRepository.BankTransfer(accId, benifitiaryId, amount);
                     if(check3)
                        return Ok();
                 }
             }
            return BadRequest();
      
        }


    }
}
