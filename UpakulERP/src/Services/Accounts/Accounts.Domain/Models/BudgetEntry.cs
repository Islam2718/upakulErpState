using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Utility.Domain;

namespace Accounts.Domain.Models
{
    [Table("BudgetEntry",Schema = "budg")]
    public class BudgetEntry:EntityBase
    {
        [Key, Column(Order = 0)]
        public string FinancialYear { get; set; }
        [Key, Column(Order = 1)]
        public int OfficeId { get; set; }
        [Key, Column(Order = 2)]
        public int ComponentParentId { get; set; }
        [Key, Column(Order = 3)]
        public int ComponentId { get; set; }
        public int? NoOfStaff { get; set; }
        public int? NoOfPiece { get; set; }
        public int? ComponentNoOfDay { get; set; }
        public decimal ComponentPerAmount { get; set; }
        public decimal ComponentTotalAmount{ get; set; }
        public int? NoOfGratuity { get; set; }
        public decimal? Basic1 { get; set; }
        public decimal? Other1 { get; set; }
        public decimal? Basic2 { get; set; }
        public decimal? Other2 { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? PF { get; set; }
        public decimal? Gratuity { get; set; }
        public decimal? MedicalAllowance { get; set; }
    }
}
