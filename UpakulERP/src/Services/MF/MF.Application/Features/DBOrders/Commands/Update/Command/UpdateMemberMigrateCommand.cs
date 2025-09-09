using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Update.Command
{
  public  class UpdateMemberMigrateCommand : IRequest<CommadResponse>
    {
        public int MemberId { get; set; }
        public bool? IsMigrated { get; set; }
        public string? MigratedNote { get; set; }
        public int? MigrateBy { get; set; }
        public DateTime? MigrateDate { get; set; } = DateTime.Now;
    }
}
