using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.Training
{
    public class TrainingVM : EntityBase
    {
        public int TrainingId { get; set; }
        public string Title { get; set; }
        public string Institute { get; set; }
        public string? reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
