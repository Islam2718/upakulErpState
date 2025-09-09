namespace Message.Library.Template
{
    public class EmailBodyCommonTemplate
    {
        public Tuple<string,string,string> LoanApplicationApproval 
            (string office,string memberid,string memberName,string memberAddr ,string applicationDate,string amount,string approvalUrl,string validtime
            ,int pre_loan_amount,string pre_lst_ins_date,string pre_lst_rec_dt,string inp_Id,string inp_name)
        {
            string title = @$"{memberid} - {memberName}'s loan application from {office} branch.";
            string html = @$"<table>
<tr><td>Office/Branch</td><td>: </td><td>{office}</td></tr>
<tr><td>Member id</td><td>: </td><td>{memberid}</td></tr>
<tr><td>Member name</td><td>: </td><td>{memberName}</td></tr>
<tr><td>Address</td><td>: </td><td>{memberAddr}</td>
<tr><td>Application date</td><td>: </td><td>{applicationDate}</td>
<tr><td>Amount</td><td>: </td><td>{amount}</td></tr>
<tr><td colspan='3' style='text-align: center;font-weight: bold;'>Previous loan Info</td></tr>
<tr><td>Loan</td><td>: </td><td>{pre_loan_amount}</td></tr>
<tr><td>Last Installment Dt</td><td>: </td><td>{pre_lst_ins_date}</td></tr>
<tr><td>Last Received Dt</td><td>: </td><td>{pre_lst_rec_dt}</td></tr>
<tr><td colspan=""3"" style=""text-align: center;font-weight: bold;"">Application Input by</td></tr>
<tr><td>Id</td><td>: </td><td>{inp_Id}</td></tr>
<tr><td>name</td><td>: </td><td>{inp_name}</td></tr>
</table><br /><br /><br />
<span>valid: {validtime}</span><br /><br />
<button onclick=""window.open('{approvalUrl}', '_blank')"">Approval</button>";
            string txt = "";
            return Tuple.Create(title, html, txt);
        }
    }
}
