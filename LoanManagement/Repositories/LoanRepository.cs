using LoanManagement.Data;
using LoanManagement.Models;
using LoanManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagement.Repositories
{
    public class LoanRepository : ILoanRepository
    {

        //testcheck in

         private readonly LoanDbContext _loandBContext;

        //  AngularDBContext db = new AngularDBContext();

        //loandetails = db.LoanDetails.Where(loan => loan.FirstName == searchViewModel.FirstName).ToList();

        public LoanRepository(LoanDbContext loandBContext)
        {
            this._loandBContext = loandBContext;
        }
        public LoanDetail AddLoan(LoanDetail loanDetail)
        {
            try
            {
                var totalLoans =_loandBContext.LoanDetails.Count();
                string loanNumber = "LN10001";
                int newloanNumber = Convert.ToInt32(loanNumber.Substring(2)) + Convert.ToInt32(totalLoans);
                loanNumber = string.Concat("LN", newloanNumber);
                loanDetail.LoanNumber = loanNumber;
                _loandBContext.Add(loanDetail);
                _loandBContext.SaveChanges();
                return loanDetail;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteLoan(string loanNumber)
        {
            LoanDetail deleteLoan = _loandBContext.LoanDetails.Where(loan => loan.LoanNumber == loanNumber).FirstOrDefault();
            if (deleteLoan != null)
            {
                _loandBContext.LoanDetails.Remove(deleteLoan);
                _loandBContext.SaveChanges();
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public List<LoanDetail> SearchLoan(SearchViewModel searchViewModel)
        {
            List<LoanDetail> loandetails = null;
            if (!string.IsNullOrEmpty(searchViewModel.LoanNumber ))
            {
                loandetails = _loandBContext.LoanDetails.Where(loan => loan.LoanNumber == searchViewModel.LoanNumber).ToList();
            }
            else if (! string.IsNullOrEmpty(searchViewModel.LastName) && !string.IsNullOrEmpty(searchViewModel.FirstName))
            {
                loandetails = _loandBContext.LoanDetails.Where(loan => loan.LastName == searchViewModel.LastName && loan.FirstName == searchViewModel.FirstName).ToList();
               

            }
            else if (string.IsNullOrEmpty(searchViewModel.FirstName))
            {
                loandetails = _loandBContext.LoanDetails.Where(loan => loan.LastName == searchViewModel.LastName).ToList();
            }
            else if (string.IsNullOrEmpty(searchViewModel.LastName))
            {
                loandetails = _loandBContext.LoanDetails.Where(loan => loan.FirstName == searchViewModel.FirstName).ToList();
            }

           return loandetails;

        }

        public LoanDetail UpdateLoan(LoanDetail loanDetail)
        {

            var existingloandetail = _loandBContext.LoanDetails.Where(loan => loan.LoanNumber == loanDetail.LoanNumber).FirstOrDefault();

            if (existingloandetail != null)
            {
                existingloandetail.LoanAmount = loanDetail.LoanAmount;
                existingloandetail.LoanTerm = loanDetail.LoanTerm;
                existingloandetail.LoanType = loanDetail.LoanType;
                _loandBContext.SaveChanges();
                return loanDetail;
            }
            else {
                return default(LoanDetail);
            }
            
        }

        List<LoanDetail> ILoanRepository.GetAllLoan()
        {
            return _loandBContext.LoanDetails.ToList();
        }
    }
}
