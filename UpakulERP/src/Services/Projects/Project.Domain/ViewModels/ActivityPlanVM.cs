

namespace Project.Domain.ViewModels
{
    public class ActivityPlanVM
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ActivityName { get; set; }
        public DateTime ActivityFrom { get; set; }
        public DateTime ActivityTo { get; set; }
        public DateTime ReportingDate { get; set; }
        public string TargetType { get; set; }
        public int? MonthlyTarget { get; set; }
        public int? TotalTarget { get; set; }
        public int? ProgramParticipantsTarget { get; set; }
        public string? BeneficiaryType { get; set; }
        public int? ProgramParticipants_U_18_Boys { get; set; }
        public int? ProgramParticipants_U_18_Girls { get; set; }
        public int? ProgramParticipants_18_59_Male { get; set; }
        public int? ProgramParticipants_18_59_Female { get; set; }
        public int? ProgramParticipants_Up_59_Male { get; set; }
        public int? ProgramParticipants_Up_59_Female { get; set; }
        public int? ProgramParticipants_Disable_Male { get; set; }
        public int? ProgramParticipants_Disable_Female { get; set; }
        public int? ProgramParticipantsEthnicityandMarginalized { get; set; }
        public int? WomenHeadedProgramParticipants { get; set; }
        public int? TransgenderProgramParticipants { get; set; }
        public bool? IsApproved { get; set; } = false;
    }
}
