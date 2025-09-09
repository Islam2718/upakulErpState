using System.Data;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Dapper;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Constants;
using MF.Application.Contacts.Enums;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Domain;
using Utility.Response;
using static Utility.Enums.OfficeType;

namespace MF.Infrastructure.Repository
{
    public class MemberRepository : CommonRepository<Member>, IMemberRepository
    {
        AppDbContext _context;
        private readonly string _connectionString;
        public MemberRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Member GetById(int id)
        {
            var obj = _context.members.FirstOrDefault(c => c.IsActive && c.MemberId == id);
            return obj;
        }

        public VWMember GetMemberDetailById(int id)
        {
            return _context.vw_members_details.FirstOrDefault(x=>x.MemberId == id);
        }

        public string MemberDataCheck(string NationalId, string SmartCard, string ContactNoOwn)
        {
            var vfy = GetMany(c => c.NationalId == NationalId || c.SmartCard == SmartCard || c.ContactNoOwn == ContactNoOwn);
            if (vfy.Any())
            {
                if (vfy.Where(c => c.NationalId == NationalId).Any())
                    return MessageTexts.duplicate_entry("National Id");
                else if (vfy.Where(c => c.SmartCard == SmartCard).Any())
                    return MessageTexts.duplicate_entry("Smart Card");
                else if (vfy.Where(c => c.ContactNoOwn == ContactNoOwn).Any())
                    return MessageTexts.duplicate_entry("Pesonal mobile number");
                else return "Contact the system admin";
            }
            else return "";
        }

        public IEnumerable<Member> GetMany(Expression<Func<Member, bool>> where)
        {
            var entities = _context.members.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<VWmemberCommonData>> LoadGrid(int page, int pageSize, string search, string sortOrder, int logedInofficeId)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "MemberName.Contains(@0) OR MemberCode.Contains(@0) OR GroupCode.Contains(@0) OR GroupName.Contains(@0)";  // OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "MemberId" : sortOrder;
            var query = _context.vw_members.Where(b => b.OfficeId == logedInofficeId)
                 .Where(src_qry, search).OrderBy(sortOrder).AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var obj = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<VWmemberCommonData>(obj, totalRecords);
        }

        public async Task<MultipleDropdownForMemberProfileVM> AllMemberProfileDropDown(int officeId)
        {
            try
            {
                var obj = new MultipleDropdownForMemberProfileVM();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var param = new { officeId = officeId };
                    using (var multi = await connection.QueryMultipleAsync("udp_AllMemberProfileDropDown", param, commandType: CommandType.StoredProcedure))
                    {
                        obj.group = multi.Read<CustomSelectListItem>().ToList();
                        obj.authorizedPerson = multi.Read<CustomSelectListItem>().ToList();
                        obj.referenceMember = multi.Read<CustomSelectListItem>().ToList();
                        obj.occupation = multi.Read<CustomSelectListItem>().ToList();
                        obj.country = multi.Read<CustomSelectListItem>().ToList();
                        obj.division = multi.Read<CustomSelectListItem>().ToList();
                    }
                }
                obj.academicQualification = new MemberEducation().GetMemberEducationDropDown();
                obj.remarks = new MemberRemarks().GetMemberRemarksDropDown();
                obj.gender = new Utility.Enums.Gender().GetGenderDropDown();
                obj.maritalStatus = new Utility.Enums.MaritalStatus().GetMaritalStatusDropDown(false);
                return obj;
            }
            catch (Exception ex)
            {
                return new MultipleDropdownForMemberProfileVM();
            }
        }

        public List<CustomSelectListItem> GetMemberByGroupIdDropdown(int groupId)
        {
            var lst = new List<CustomSelectListItem>();
            lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Selected = true });  //Value="" , 
            lst.AddRange( _context.members
                .Where(x=>x.IsActive && x.MemberStatus== MemberStatus.ApprovedMember && x.GroupId==groupId)
                .Select(s => new CustomSelectListItem
                {
                    Selected = false,
                    Text = s.MemberCode + " - " + s.MemberName,
                    Value = s.MemberId.ToString()
                }).ToList());
            return lst;
        }
       
        public Task<List<GroupXMemberComponentDetailsVM>> GroupXMemberComponentDetails(int logedInofficeId, int groupId)
        {
            var query = from m in _context.members
                        //join g in _context.groups on m.GroupId equals g.GroupId
                        //join of in _context.GetOfficeHierarchi(logedInofficeId, (int)OfficeTypeEnum.Branch)
                        //on m.OfficeId equals of.OfficeId
                        where m.IsActive && m.OfficeId == logedInofficeId && m.GroupId == groupId
                        select new GroupXMemberComponentDetailsVM
                        {
                            MemberId = m.MemberId,
                            MemberName = m.MemberName,
                            MemberCode = m.MemberCode,
                            LoanNo = 0,
                            InsNo = 0,
                            Component = 0,
                            DisburseDate = DateTime.Now,
                            DisburseAmt = 0,
                            OpeningOutst = 0,
                            OpeningOD = 0,
                            OpeningAdv = 0,
                            OpeningSaving = 0,
                            InsAmt = 0,
                            Att = 0,
                            LoanCollection = 0,
                            LoanRebate = 0,
                            LoanAdjust = 0,
                            SavingsCollectionCom = true,
                            SavingsCollectionVol = false,
                            SavingsCollectionOth = true,
                            SavingsRefundRefund = 0,
                            SavingsRefundCom = 0,
                            SavingsRefundVol = 0,
                            SavingsRefundOth = 0,
                            Ledger = "Ledger Ref."
                        };
            return query.ToListAsync();
        }

    }
}
