using LoanManagement.Models;
using LoanManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagement.Repositories
{
    public interface ILoanRepository
    {
        List<LoanDetail> SearchLoan(SearchViewModel searchViewModel);
        LoanDetail AddLoan(LoanDetail loanDetail);
        LoanDetail UpdateLoan(LoanDetail loanDetail);
        int DeleteLoan(string loanNumber);
        List<LoanDetail> GetAllLoan();
    }
}
