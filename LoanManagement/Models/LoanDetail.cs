using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagement.Models
{
    public class LoanDetail
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoanNumber { get; set; }
        public int LoanAmount { get; set; }
        public int LoanTerm { get; set; }
        public string PropertyAddress { get; set; }
        public string LoanManagementFees { get; set; }
        public string LoanType { get; set; }
        public string LienInformation { get; set; }
        public string LoanHistory { get; set; }
        public DateTime OriginationDate { get; set; }
        public string Status { get; set; }
    }
}
