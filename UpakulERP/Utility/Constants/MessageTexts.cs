
namespace Utility.Constants
{
    public class MessageTexts
    {
        public const string insert_success = "Data inserted successfully.";
        public const string insert_failed = "Data Insertion failed";
        public const string update_success = "Data updated successfully";
        public const string update_failed = "Update failed";
        public const string update_failed_ref = "Update failed because the row has reference";
        public const string approved_success = "Data approved successfully";
        public const string approved_failed = "Approved failed";
        public const string approved_failed_ref = "Approved failed because the row has reference";
        public const string checked_success = "Contact No checked successfully.";
        public const string checked_failed = "Contact No check failed.";
        public const string migrate_success = "Member migrated successfully.";
        public const string migrate_failed = "Member migration failed.";


        // Add rejected messages
        public const string rejected_success = "Contact No rejected successfully.";
        public const string rejected_failed = "Contact No rejection failed.";

        public const string delete_success = "Data deleted successfully";
        public const string delete_failed = "Delete failed";
        public const string delete_failed_ref = "Delete failed because the row has reference";
        //public const string duplicate_entry = "Duplicate entry. This item is already exists";
        public const string change_password_success = "Password changed successfully";
        public const string change_password_sailed = "Password change failed.";
        public const string reset_password_success = "Password Reset successfully";
        public const string reset_password_failed = "Password Reset failed.";
        public const string data_not_found = "Data Not Found.";
        public const string required_date = "Please check all required data.";
        public const string user_not_found = "User not found. Please send valid user.";
        public const string transaction_date_required = "Transaction date is required";
        public const string drop_down = "Please select";

        public static string child_found(string obj_name)
        {
            return $"This {obj_name} has child {obj_name}(s). Please delete them first.";
        }
        public static string string_length(string prefix, int length)
        {
            return $"{prefix} is allowed upto {length} characters";
        }
        public static string duplicate_entry(string item)
        {
            return $"Duplicate entry.This {item} is/are already exists";
        }
        public static string required_field(string item)
        {
            return $"{item} is required.";
        }
    }
}
