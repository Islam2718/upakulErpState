namespace Message.Library.Template.User
{
    public class ResetPasswordTemplate
    {
        public Tuple<string, string, string> ResetPassword(string userid,string newpassword,string passwordResetUser)
        {
            string title = @$"User password reset request.";
            string html = @$"<table role = ""presentation"" valign=""top"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""512"" align=""center""  style=""margin-left:auto;margin-right:auto;margin-top:0px;margin-bottom:0px;width:512px;max-width:512px;padding:0px"">
<tbody> <tr> <td> 
<table role = ""presentation"" valign=""top"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""background-color:#ffffff"">
<tbody> <tr> <td style = ""padding:24px;text-align:center"" > 
<table role=""presentation"" valign=""top"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"" style=""min-width:100%""> 
<tbody> <tr> <td align = ""left"" valign=""middle"">Password Reset Request</td> <td valign = ""middle"" align=""right""> 
<table role = ""presentation"" valign=""top"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%""> 
<tbody> <tr> <td align = ""right"" valign=""middle"" style=""width:32px"" width=""32""> 
UserId:{userid}</td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> <tr> 
<td style = ""padding-left:24px;padding-right:24px;padding-bottom:24px""> 
<div> <table role=""presentation"" valign=""top"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%""> 
<tbody> <tr> <td> 
<table role = ""presentation"" valign=""top"" border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%""> 
<tbody> <tr> <td style = ""text-align:center"">Reset Complete User:{passwordResetUser}</td> </tr> 
<tr style = ""text-align:center"" > < td style=""padding-top:8px;color:#282828""> 
<h3 style = ""margin:0;font-weight:500;font-size:20px;color:#282828"">New Password:{newpassword}</h3> 
 </td> </tr> </tbody> </table> </td> </tr> </tbody> </table> </td> </tr> </tbody> </table>";
            string txt = "";
            return Tuple.Create(title, html, txt);
        }
        
    }
}
