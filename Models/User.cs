using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }

        public bool CheckLogon()
        {
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\aspearce\Documents\ASP1\ASPLogin\ASPLogin\App_Data\aspDB.mdf;Integrated Security=True"))

            {
                string _sql = @"SELECT [Username] FROM [dbo].[System_Users] WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters.Add(new SqlParameter("@u", SqlDbType.NVarChar)).Value = UserName;
                cmd.Parameters.Add(new SqlParameter("@p", SqlDbType.NVarChar)).Value = Test.SHA1.Encode(Password);
                cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }
    }
}
