using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;
using ShoppingCart.DAL;
using System.Data.SqlClient;
using ShoppingCart.BL;
using System.Configuration;

namespace ShoppingCart.BL
{
    public class UserController
    {

        public static DataSet GetallRoles()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllRoles"));
        }

        public static DataSet GetRolesbyRoleid(int Role_id)
        {
            SqlParameter p = new SqlParameter("@Roleid", Role_id);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetRolesbyRoleid", p));
        }

        public static DataSet getALLUsers()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetALLUsers"));
        }

        public static DataSet Getusersbyuserid(int UserID)
        {
            SqlParameter p = new SqlParameter("@UserID", UserID);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetUsersbyuserid", p));
        }

        public static DataSet getallactivedepartment()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetALLactivedeppart"));
        }


















        public static void UpdateUserDetails(string p_username, string p_fname, string p_lname, DateTime p_dob, string p_no, string p_gender, string p_email, string p_address, int p_country, int p_state,
        int p_city, string p_zcode, int AddrID, int UID)
        {
            SqlParameter[] p = new SqlParameter[14];
            p[0] = new SqlParameter("@username", SqlDbType.NVarChar);
            p[0].Value = p_username;
            p[1] = new SqlParameter("@fname", SqlDbType.NVarChar);
            p[1].Value = p_fname;
            p[2] = new SqlParameter("@lname", SqlDbType.NVarChar);
            p[2].Value = p_lname;
            p[3] = new SqlParameter("@dob", SqlDbType.DateTime);
            p[3].Value = p_dob;
            p[4] = new SqlParameter("@ContactNo", SqlDbType.NVarChar);
            p[4].Value = p_no;
            p[5] = new SqlParameter("@gender", SqlDbType.NVarChar);
            p[5].Value = p_gender;
            p[6] = new SqlParameter("@emailId", SqlDbType.NVarChar);
            p[6].Value = p_email;
            p[7] = new SqlParameter("@Address", SqlDbType.NVarChar);
            p[7].Value = p_address;
            p[8] = new SqlParameter("@Country", SqlDbType.BigInt);
            p[8].Value = p_country;
            p[9] = new SqlParameter("@state", SqlDbType.BigInt);
            p[9].Value = p_state;
            p[10] = new SqlParameter("@City", SqlDbType.BigInt);
            p[10].Value = p_city;
            p[11] = new SqlParameter("@ZipCode", SqlDbType.VarChar);
            p[11].Value = p_zcode;
            p[12] = new SqlParameter("@AddressID", SqlDbType.Int);
            p[12].Value = AddrID;
            p[13] = new SqlParameter("@UserID", SqlDbType.Int);
            p[13].Value = UID;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spUpdateUserDetails", p);
        }

        public static void DeleteUserByUserID(int UserID)
        {
            SqlParameter p = new SqlParameter("@UserID", UserID);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spDeleteUserByUserID", p);
        }
    }
}
