using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanManagement.Models;
using LoanManagement.Repositories;
using LoanManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoanManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanManagemantController : ControllerBase
    {

        private readonly ILoanRepository _loanRepository;
        public LoanManagemantController(ILoanRepository loanRepository)
        {
            this._loanRepository = loanRepository;
        }

        // GET: api/<LoanManagemantController>
        [HttpGet]
        public List<LoanDetail> Get()
        {
            return _loanRepository.GetAllLoan();
        }

        // GET api/<LoanManagemantController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoanManagemantController>
        [HttpPost]
        [Route("addloan")]
        public LoanDetail Post([FromBody] LoanDetail loanDetail)
        {
            var loandetail = _loanRepository.AddLoan(loanDetail);
            return loandetail;
        }
        [HttpPost]
        [Route("searchloan")]
        public List<LoanDetail> SearchLoan([FromBody] SearchViewModel searchViewModel)
        {
            //List<LoanDetail> loandetails = null;
            var loandetails = _loanRepository.SearchLoan(searchViewModel);

            return loandetails;
        }

        // PUT api/<LoanManagemantController>/5
        [HttpPut("{id}")]
        [Route("updateloan")]
        public LoanDetail UpdateLoan([FromBody] LoanDetail loanDetail)
        {
            var updatedloandetail = _loanRepository.UpdateLoan(loanDetail);
            return updatedloandetail;
        }

       // DELETE api/<LoanManagemantController>/5
        [HttpDelete("{id}")]
        [Route("deleteloan")]
        public int DeleteLoan(string loanNumber)
        {
            var loanNum = _loanRepository.DeleteLoan(loanNumber);
            return loanNum; ;
        }
    }
}
