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
    public class TestDetails
    {
        public string Attendance { get; set; }


    }
    public class ProductController
    {


        #region "OLD Vinit"


        public static DataSet GetAllActivecenter()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Getallcenter"));
        }


        public static int InsertCounsellingreuqest(string txtName, string txtmobileno, string Email, string Pdate, string ptime, int center, int Registrationtype)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@Name", txtName);
            p[1] = new SqlParameter("@mobileno", txtmobileno);
            p[2] = new SqlParameter("@Email", Email);
            p[3] = new SqlParameter("@pCenter", center);
            p[4] = new SqlParameter("@pdate", Pdate);
            p[5] = new SqlParameter("@ptime", ptime);
            p[6] = new SqlParameter("@reg_type_id", Registrationtype);
            p[7] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertCounsellinginfo", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static int InsertQuery(string name, string Mobile, string email, string query)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@Name", name);
            p[1] = new SqlParameter("@mobileno", Mobile);
            p[2] = new SqlParameter("@Email", email);
            p[3] = new SqlParameter("@query", query);
            p[4] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertQuery", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static DataSet Getallactivetimeslot()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallActivetimeslot"));
        }


        public static DataSet Sendmail(string Center, int userid)
        {
            SqlParameter p1 = new SqlParameter("@Center", Center);
            SqlParameter p2 = new SqlParameter("@id", userid);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Sendmail", p1, p2));
        }

        public static DataSet Sendmailforquery(int Queryid)
        {
            SqlParameter p1 = new SqlParameter("@id", Queryid);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Sendmailforquery", p1));
        }

        public static DataSet GetallCourseName()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallActiveCourses"));
        }

        public static int InsertScholarshiprequest(string txtName, string txtmobileno, string Email, int center, int Registrationtype, int Cur_Course, string College_Name)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@Name", txtName);
            p[1] = new SqlParameter("@mobileno", txtmobileno);
            p[2] = new SqlParameter("@Email", Email);
            p[3] = new SqlParameter("@pCenter", center);
            p[4] = new SqlParameter("@Cur_Course", Cur_Course);
            p[5] = new SqlParameter("@College_Name", College_Name);
            p[6] = new SqlParameter("@reg_type_id", Registrationtype);
            p[7] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertScholarshipinfo", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static DataSet Scholarshipmail(string Center, int userid)
        {
            SqlParameter p1 = new SqlParameter("@Center", Center);
            SqlParameter p2 = new SqlParameter("@id", userid);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Scholarshipmail", p1, p2));
        }

        public static SqlDataReader GetLogin(string Username, string Password)
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Username", SqlDbType.NVarChar);
            p[0].Value = Username;
            p[1] = new SqlParameter("@Password", SqlDbType.NVarChar);
            p[1].Value = Password;
            return (SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Login", p));
        }

        public static Object GetRoleByUsername(string Username)
        {
            SqlParameter p = new SqlParameter("@Username", SqlDbType.NVarChar);
            p.Value = Username;
            return (SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetRoleByUsername", p));
        }


        public static DataSet Getallproducttype()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_EC_Getallproducttype"));
        }

        public static DataSet Getallproducts()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_EC_Getallproducts"));
        }

        public static void AddContactUs(string fullName, string eId, string msg)
        {
            //Dim parr As SqlParameter() = New SqlParameter(20) {}
            //parr(0) = New SqlParameter("@fullName", SqlDbType.VarChar)
            //parr(0).Value = fullName
            //parr(1) = New SqlParameter("@eId", SqlDbType.VarChar)
            //parr(1).Value = eId
            //parr(1) = New SqlParameter("@msg", SqlDbType.VarChar)
            //parr(1).Value = msg
            //SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spaddnewuser", parr)

        }


        public static string getProductPrice(int ProductID)
        {
            string Price = "0";
            SqlDataReader drPrice = null;
            SqlParameter p = new SqlParameter("@ProductId", ProductID);
            drPrice = SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spGetProductbyproductId", p);
            if (drPrice != null)
            {
                if (drPrice.Read())
                {
                    Price = drPrice["Sale_Price"].ToString();
                }
            }
            return (Price);
        }

        public static DataSet GetQuantity(int ProductID, string SessionID, int Quantity, float Price)
        {
            SqlParameter p1 = new SqlParameter("@ProductID", ProductID);
            SqlParameter p2 = new SqlParameter("@p_sessionId", SessionID);
            SqlParameter p3 = new SqlParameter("@p_quantity", Quantity);
            SqlParameter p4 = new SqlParameter("@p_price", Price);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "spQuantity", p1, p2, p3, p4));
        }

        public static DataSet GetShoppingCartDetails(string SessionID)
        {
            SqlParameter p = new SqlParameter("@SessionId", SessionID);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_CartProducts", p));
        }

        public static float GetCartTotal(string SID)
        {
            SqlParameter p = new SqlParameter("@SessionID", SID);
            return (float.Parse(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetCartTotal", p).ToString()));
        }

        public static int shoppingcartitems(string Sessionid)
        {
            //SqlParameter[] p = new SqlParameter[2];
            SqlParameter p = new SqlParameter("@SessionID", Sessionid);
            return (int.Parse(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetCartItems", p).ToString()));
        }


        public static void Deletecartlogout(string Sessionid)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@sessionId", SqlDbType.NVarChar);
            p[0].Value = Sessionid;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_DeleteCartlogout", p);
        }

        public static SqlDataReader getAccountDetails(string username)
        {
            SqlParameter p = new SqlParameter("@User", username);
            return (SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_ChangeDetails", p));
        }

        public static DataSet getAllStatedetails()
        {
            return SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetALLState");
        }

        public static DataSet getCountrydetails()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "usp_GetallCountry"));
        }

        public static DataSet getCitydetailsByStateID(int StateID)
        {
            SqlParameter p = new SqlParameter("@StateID", SqlDbType.Int);
            p.Value = StateID;
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllCitiesByStateID", p));
        }


        public static DataSet getCitydetails()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetALLCity"));
        }

        public static int[] spInsertAddressV2(string fname, string lname, string email, string gender, string contact, string nadrr, int ncountry, int nstate, int ncity)
        {
            int[] a = new int[2];
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@firstname", SqlDbType.NVarChar);
            p[0].Value = fname;
            p[1] = new SqlParameter("@lastname", SqlDbType.NVarChar);
            p[1].Value = lname;
            p[2] = new SqlParameter("@Email", SqlDbType.NVarChar);
            p[2].Value = email;
            p[3] = new SqlParameter("@Gender", SqlDbType.NVarChar);
            p[3].Value = gender;
            p[4] = new SqlParameter("@contactno", SqlDbType.NVarChar);
            p[4].Value = contact;
            p[5] = new SqlParameter("@Address", SqlDbType.NVarChar);
            p[5].Value = nadrr;
            p[6] = new SqlParameter("@CountryID", SqlDbType.Int);
            p[6].Value = ncountry;
            p[7] = new SqlParameter("@StateID", SqlDbType.Int);
            p[7].Value = nstate;
            p[8] = new SqlParameter("@CityID", SqlDbType.Int);
            p[8].Value = ncity;

            p[9] = new SqlParameter("@AID", SqlDbType.BigInt);
            p[9].Direction = ParameterDirection.Output;
            p[10] = new SqlParameter("@Cid", SqlDbType.BigInt);
            p[10].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertAddressV2", p);
            if (p[9] != null)
            {
                a[0] = int.Parse(p[9].Value.ToString());
            }
            if (p[10] != null)
            {
                a[1] = int.Parse(p[10].Value.ToString());
            }
            return (a);
        }

        public static DataSet getAllStatesByCountryID(int CountryID)
        {
            SqlParameter p = new SqlParameter("@CountryID", SqlDbType.Int);
            p.Value = CountryID;
            return SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStatesByCountryID", p);
        }

        public static int GetIdFromtblUserAccount(string username)
        {
            SqlParameter p = new SqlParameter("@username", username);
            SqlParameter p1 = new SqlParameter("@customerid", SqlDbType.BigInt);
            p1.Direction = ParameterDirection.Output;

            SqlHelper.ExecuteReader(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetCustId", p, p1);
            return (int.Parse(p1.Value.ToString()));
        }

        public static int insertorder(int custid, float orderamnt, float taxamnt, float netamnt, int shipaddrId, int billaddrId, string sessionid, string billfname)
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@CustId", custid);
            p[1] = new SqlParameter("@OrderAmnt", orderamnt);
            p[2] = new SqlParameter("@TaxAmnt", taxamnt);
            p[3] = new SqlParameter("@NetAmnt", netamnt);
            p[4] = new SqlParameter("@ShipAddrId", shipaddrId);
            p[5] = new SqlParameter("@BillAddrId", billaddrId);
            p[6] = new SqlParameter("@SessionId", sessionid);
            p[7] = new SqlParameter("@OId", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            p[8] = new SqlParameter("@BillFname", billfname);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_insertorder", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        #endregion

        #region "Vivek New"
        public static DataSet GetAllActiveUser_Login(string User_ID, string Password, string DBName)
        {
            SqlParameter p1 = new SqlParameter("@Flag", 1);
            SqlParameter p2 = new SqlParameter("@User_Name", User_ID);
            SqlParameter p3 = new SqlParameter("@User_Password", Password);
            SqlParameter p4 = new SqlParameter("@IP_Address", "");
            if (DBName == "DB03_Test_Engine")
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_User", p1, p2, p3, p4));
            }
            else
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_USER_Login", p1, p2));
            }
        }

        public static DataSet GetAllActiveTestMode()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestMode"));
        }

        public static DataSet GetAllActiveTestCategory()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestCategory"));
        }

        public static DataSet GetAllActiveTestType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestType"));
        }

        public static DataSet GetAllActiveTestAbsentReason()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestAbsentReason"));
        }

        public static DataSet GetAllActiveDiffLevel()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveDiffLevel"));
        }

        public static DataSet GetAllActiveUnitOfMeasurement()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveUnitOfMeasurement"));
        }

        public static DataSet GetAllActiveUser_Company_Division_Zone_Center(string User_ID, string Company_Code, string Division_Code, string Zone_Code, string Flag, string DBName)
        {
            SqlParameter p1 = new SqlParameter("@user_id", User_ID);
            SqlParameter p2 = new SqlParameter("@company_code", Company_Code);
            SqlParameter p3 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p4 = new SqlParameter("@Zone_Code", Zone_Code);
            SqlParameter p5 = new SqlParameter("@Flag", Flag);

            if (DBName == "MTEducare")
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetUser_Company_Division_Zone_Center_MTEducare", p1, p2, p3, p4, p5));
            }
            else
            {
                return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetUser_Company_Division_Zone_Center", p1, p2, p3, p4, p5));
            }

        }

        public static DataSet GetUser_Company_Division_Zone_Center(int Flag, string Userid, string Divisioncode, string Zonecode, string Companycode)
        {
            SqlParameter p = new SqlParameter("@flag", SqlDbType.Int);
            p.Value = Flag;
            SqlParameter p1 = new SqlParameter("@user_id", SqlDbType.VarChar);
            p1.Value = Userid;
            SqlParameter p2 = new SqlParameter("@division_code", SqlDbType.VarChar);
            p2.Value = Divisioncode;
            SqlParameter p3 = new SqlParameter("@Zone_code", SqlDbType.VarChar);
            p3.Value = Zonecode;
            SqlParameter p4 = new SqlParameter("@Company_Code", SqlDbType.VarChar);
            p4.Value = Companycode;
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetUser_Company_Division_Zone_Center", p, p1, p2, p3, p4));
        }

        public static DataSet Get_SupervisorDetails(string CenterCode)
        {
            SqlParameter p1 = new SqlParameter("@CenterCode", CenterCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GET_SUPERVISOR_DETAILS_BY_CENTER", p1));

        }

        public static DataSet Get_Supervisor_ExcelUploadFlag(string ExcelName, string Flag)
        {
            SqlParameter p = new SqlParameter("@ExcelName", ExcelName);
            SqlParameter p1 = new SqlParameter("@flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_Supervisor_ExcelUploadFlag", p, p1));
        }


        public static DataSet Get_SlabBy_Division(string Division_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Test_Get_SlabBy_Division", p1));

        }


        public static DataSet GetAllActiveCountry()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllCountry"));
        }

        public static DataSet GetAllActiveState(string Country_Code)
        {
            SqlParameter p1 = new SqlParameter("@Countrycode", Country_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStatebyCountry", p1));
        }

        public static DataSet GetAllActiveCity(string State_Code)
        {
            SqlParameter p1 = new SqlParameter("@Statecode", State_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllCitybyState", p1));
        }

        public static DataSet GetAllActiveLocation(string City_Code)
        {
            SqlParameter p1 = new SqlParameter("@CityCode", City_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllLocationByCity", p1));
        }

        public static DataSet GetAllActiveClassroomType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveClassroomType"));
        }

        public static DataSet GetAllActivePremisesType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActivePremisesType"));
        }

        public static DataSet GetAllActivePartnerTitle()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActivePartnerTitle"));
        }

        public static DataSet GetAllActiveActivity(string ClassroomType_Id, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@ClassroomType_Id", ClassroomType_Id);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetAllActiveActivityType", p1, p2));
        }

        public static DataSet GetTest_Notification(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetTest_Notification_New", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet GetAllActiveUser_AcadYear()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallCurrentYear"));
        }

        public static DataSet GetAllActive_AllStandard(string Division_Code)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStandard", p1));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStandard_New", p1));
        }

        public static DataSet GetAllActiveStreamsBy_Division_Year(string Division_Code, string Acad_Year, string AAG, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@AAG", AAG);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStreamsBy_Division_Year_AAG", p1, p2, p3, p4));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStreamsBy_Division_Year_AAG_New", p1, p2, p3, p4));


        }

        public static DataSet GetAllSubjectsBy_Division_Year_Standard(string Division_Code, string Acad_Year, string Standard_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllSubjectsBy_Division_Year_Standard", p1, p2, p3));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllSubjectsBy_Division_Year_Standard_New", p1, p2, p3));

        }
        public static DataSet GetAllSubjectsByStandard(string Standard_Code)
        {

            SqlParameter p1 = new SqlParameter("@Standard_Code", Standard_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetLMSSubjectByStandard", p1));

        }


        public static DataSet GetAllActiveSubjectsBy_Stream_AAG(string Stream_Code, string AAG, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@stream_code", Stream_Code);
            SqlParameter p2 = new SqlParameter("@AAG", AAG);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllSubjectsBy_Stream_AAG", p1, p2, p3));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllSubjectsBy_Stream_AAG_NEW", p1, p2, p3));
        }

        public static int Insert_Batches(string DivisionCode, string YearName, string StandardCode, string ProductCode, string SubjectCode, string CentreCode, int MaxBatchStrength, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@ProductCode", ProductCode);
            p[4] = new SqlParameter("@SubjectCode", SubjectCode);
            p[5] = new SqlParameter("@CentreCode", CentreCode);
            p[6] = new SqlParameter("@MaxBatchStrength", MaxBatchStrength);
            p[7] = new SqlParameter("@CreatedBy", CreatedBy);
            p[8] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertBatch", p);
            return (int.Parse(p[8].Value.ToString()));
        }

        public static int Insert_Batches_LikeExistingBatch(string PKey, string CentreCode, int NewBatchCount, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@CentreCode", CentreCode);
            p[2] = new SqlParameter("@NewBatchCount", NewBatchCount);
            p[3] = new SqlParameter("@CreatedBy", CreatedBy);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertBatch_LikeExistingBatch", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Update_Batch(string PKey, string ProductCode, string SubjectCode, int MaxBatchStrength, string BatchShortName, int IsActiveFlag, string AlteredBy)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@ProductCode", ProductCode);
            p[2] = new SqlParameter("@SubjectCode", SubjectCode);
            p[3] = new SqlParameter("@MaxBatchStrength", MaxBatchStrength);
            p[4] = new SqlParameter("@BatchShortName", BatchShortName);
            p[5] = new SqlParameter("@IsActiveFlag", IsActiveFlag);
            p[6] = new SqlParameter("@AlteredBy", AlteredBy);
            p[7] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateBatch", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static DataSet GetAllActive_Standard_ForYear(string Division_Code, string YearName)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStandard", p1, p2, p3));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStandard_New", p1, p2, p3));

        }

        public static DataSet GetBatchBy_Division_Year_Standard_Centre(string Division_Code, string YearName, string StandardCode, string CentreCode, string BatchName)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@BatchName", BatchName);
            SqlParameter p6 = new SqlParameter("@Flag", 1);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatchBy_Division_Year_Standard_Centre", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetAllActive_Batch_ForStandard(string Division_Code, string YearName, string StandardCode, string CentreCode)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@Flag", 1);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForStandard", p1, p2, p3, p4, p5));
        }
        // GetAllActive_Batch_ForStandard(string Division_Code, string YearName, string StandardCode, string CentreCode)
        public static DataSet GetAllActive_Batch_ForStandard(string Division_Code, string YearName, string StandardCode, string CentreCode, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForStandard", p1, p2, p3, p4, p5));
        }

        public static DataSet GetAllChaptersBy_Division_Year_Standard_Subject(string Division_Code, string YearName, string StandardCode, string SubjectCode)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@SubjectCode", SubjectCode);



            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllChaptersBy_Division_Year_Standard_Subject", p1, p2, p3, p4));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllChaptersBy_Division_Year_Standard_Subject_New", p1, p2, p3, p4));
        }

        public static DataSet GetBatchBY_PKey(string PKey)
        {
            //Try
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            DataSet XYZ = SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatch_ByPKey", p1, p2, p3);
            return (XYZ);

            //Catch ex As Exception
            //    'Return nulldb

            //End Try

        }

        public static DataSet GetTestMasterBY_PKey(string PKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMaster_ByPKey", p1, p2));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMaster_ByPKey_New", p1, p2));
        }

        public static DataSet GetTestQPSet_ByPKey(string TestPKey, string Set_Number, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@Set_Number", Set_Number);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestQPSet_ByPKey", p1, p2, p3));
        }

        public static DataSet GetTestMasterBy_Division_Year_Standard(string Division_Code, string YearName, string Standard_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@Flag", Flag);

            // return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMasterBy_Division_Year_Standard", p1, p2, p3, p4, p5, p6, p7,
            // p8));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMasterBy_Division_Year_Standard_New", p1, p2, p3, p4, p5, p6, p7,
             p8));
        }

        public static DataSet GetTestScheduleBy_Division_Year_Standard(string Division_Code, string YearName, string Standard_Code, string Batch_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate,
        int AttendClosureStatus_Flag, int MarksClosureStatus_Flag, int Flag, string Centre_Code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p6 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p7 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p8 = new SqlParameter("@TestName", TestName);
            SqlParameter p9 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p10 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p11 = new SqlParameter("@AttendClosureStatus_Flag", AttendClosureStatus_Flag);
            SqlParameter p12 = new SqlParameter("@MarksClosureStatus_Flag", MarksClosureStatus_Flag);
            SqlParameter p13 = new SqlParameter("@Flag", Flag);
            SqlParameter p14 = new SqlParameter("@Centre_Code", Centre_Code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestScheduleBy_Division_Year_Standard", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10, p11, p12, p13, p14));
        }

        public static DataSet GetTestScheduleBy_Division_Year_Standard_Centre(string Division_Code, string YearName, string Standard_Code, string Batch_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate,
        int AttendClosureStatus_Flag, int MarksClosureStatus_Flag, string Centre_Code, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p6 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p7 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p8 = new SqlParameter("@TestName", TestName);
            SqlParameter p9 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p10 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p11 = new SqlParameter("@AttendClosureStatus_Flag", AttendClosureStatus_Flag);
            SqlParameter p12 = new SqlParameter("@MarksClosureStatus_Flag", MarksClosureStatus_Flag);
            SqlParameter p13 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p14 = new SqlParameter("@Flag", Flag);

            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestScheduleBy_Division_Year_Standard_Centre", p1, p2, p3, p4, p5, p6, p7,
            //p8, p9, p10, p11, p12, p13, p14));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestScheduleBy_Division_Year_Standard_Centre_New", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10, p11, p12, p13, p14));

        }

        public static DataSet GetTestForCancellationBy_Division_Year_Standard(string Division_Code, string YearName, string Standard_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p9 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p10 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestForCancellationBy_Division_Year_Standard", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10));
        }

        public static DataSet GetAnswerSheet_IssueBy_Division_Year_Standard_Centre(string Division_Code, string YearName, string Standard_Code, string Batch_Code, string TestMode_Id, string TestName, string FromDate, string ToDate, string Centre_Code, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p6 = new SqlParameter("@TestName", TestName);
            SqlParameter p7 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p8 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p9 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p10 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAnswerSheet_IssueBy_Division_Year_Standard_Centre", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10));
        }

        public static DataSet GetTestFor_Batch_Centre(string Division_Code, string YearName, string Centre_Code, string Standard_Code, string Batch_Code, int ReTest_Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p5 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p6 = new SqlParameter("@ReTest_Flag", ReTest_Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestFor_Batch_Centre", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetTestPresentStudent_ByPKey(string PKey, int Conduct_No, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", PKey);
            SqlParameter p2 = new SqlParameter("@Conduct_Number", Conduct_No);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestPresentStudent_ByPKey", p1, p2, p3));
        }


        public static DataSet GetStudent_ForTest_ByDivision_Centre_Standard(string Division_Code, string YearName, string Centre_Code, string Standard_Code, string Batch_Code, string DBSource, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p5 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p6 = new SqlParameter("@DBSource", DBSource);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);

            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByDivision_Centre_Standard", p1, p2, p3, p4, p5, p6, p7));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByDivision_Centre_Standard_New", p1, p2, p3, p4, p5, p6, p7));



        }


        public static DataSet GetTest_AnswerUploadHistory(string Division_Code, string YearName, string Standard_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, string FromDate, string ToDate, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p9 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p10 = new SqlParameter("@Flag", Flag);

            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTest_AnswerUploadHistory", p1, p2, p3, p4, p5, p6, p7,
            //p8, p9, p10));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTest_AnswerUploadHistory_New", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10));


        }

        public static DataSet GetPartnerMaster_ByPKey(string PKey, string User_ID, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@User_ID", User_ID);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPartnerMaster_ByPKey", p1, p2, p3));
        }

        public static DataSet GetAnswerSheetIssueDetails_ByPKey(string PKey, string User_ID, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@User_ID", User_ID);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAnswerSheetIssueDetails_ByPKey", p1, p2, p3));
        }

        public static int Insert_Batch_Students(string PKey, string SBEntryCode, int ActionFlag, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[2] = new SqlParameter("@CreatedBy", CreatedBy);
            p[3] = new SqlParameter("@ActionFlag", ActionFlag);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertBatch_Student", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Update_Batch_ShortName(string PKey, string BatchShortName, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@BatchShortName", BatchShortName);
            p[2] = new SqlParameter("@CreatedBy", CreatedBy);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_Batch_ShortName", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static int Update_Batch_Student_RollNo(string PKey, string SBEntryCode, string NewRollNo, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[2] = new SqlParameter("@CreatedBy", CreatedBy);
            p[3] = new SqlParameter("@NewRollNo", NewRollNo);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_Batch_Student_RollNo", p);
            return (int.Parse(p[4].Value.ToString()));
        }
        public static int Update_Test(string TestCode, string DivisionCode, string YearName, string StandardCode, string TestModeCode, string TestCategoryCode, string TestTypeCode, string SubjectCode, string CentreCode, string ChapterCode,
        double MaxMarks, int TestDuration, int QPSetCount, int QuestionCount, string TestName, string TestDescription, string Remarks, int NegativeMarkingFlag, string CreatedBy, int HideChapterFlag, string Slab_Code, string Syllabus_Description)
        {

            SqlParameter[] p = new SqlParameter[23];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@TestModeCode", TestModeCode);
            p[4] = new SqlParameter("@TestCategoryCode", TestCategoryCode);
            p[5] = new SqlParameter("@TestTypeCode", TestTypeCode);
            p[6] = new SqlParameter("@TestName", TestName);
            p[7] = new SqlParameter("@TestDescription", TestDescription);
            p[8] = new SqlParameter("@Remarks", Remarks);
            p[9] = new SqlParameter("@SubjectCode", SubjectCode);
            p[10] = new SqlParameter("@CentreCode", CentreCode);
            p[11] = new SqlParameter("@ChapterCode", ChapterCode);
            p[12] = new SqlParameter("@QPSetCnt", QPSetCount);
            p[13] = new SqlParameter("@MaxMarks", MaxMarks);
            p[14] = new SqlParameter("@TestDuration", TestDuration);
            p[15] = new SqlParameter("@CreatedBy", CreatedBy);
            p[16] = new SqlParameter("@QuestionCount", QuestionCount);
            //
            p[17] = new SqlParameter("@NegativeMarkingFlag", NegativeMarkingFlag);
            p[18] = new SqlParameter("@TestCode", TestCode);
            p[19] = new SqlParameter("@IsChapterHide", HideChapterFlag);
            p[20] = new SqlParameter("@Slab_Code", Slab_Code);

            p[21] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[21].Direction = ParameterDirection.Output;

            p[22] = new SqlParameter("@Syllabus_Description", Syllabus_Description);

            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTest", p);
            return (int.Parse(p[21].Value.ToString()));
        }
        public static int Insert_Test(string DivisionCode, string YearName, string StandardCode, string TestModeCode, string TestCategoryCode, string TestTypeCode, string SubjectCode, string CentreCode, string ChapterCode, double MaxMarks,
        int TestDuration, int QPSetCount, int QuestionCount, string TestName, string TestDescription, string Remarks, int NegativeMarkingFlag, string CreatedBy, int HideChapterFlag, string Slab_Code, string Syllabus_Description)
        {

            SqlParameter[] p = new SqlParameter[22];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@TestModeCode", TestModeCode);
            p[4] = new SqlParameter("@TestCategoryCode", TestCategoryCode);
            p[5] = new SqlParameter("@TestTypeCode", TestTypeCode);
            p[6] = new SqlParameter("@TestName", TestName);
            p[7] = new SqlParameter("@TestDescription", TestDescription);
            p[8] = new SqlParameter("@Remarks", Remarks);
            p[9] = new SqlParameter("@SubjectCode", SubjectCode);
            p[10] = new SqlParameter("@CentreCode", CentreCode);
            p[11] = new SqlParameter("@ChapterCode", ChapterCode);
            p[12] = new SqlParameter("@QPSetCnt", QPSetCount);
            p[13] = new SqlParameter("@MaxMarks", MaxMarks);
            p[14] = new SqlParameter("@TestDuration", TestDuration);
            p[15] = new SqlParameter("@CreatedBy", CreatedBy);
            p[16] = new SqlParameter("@QuestionCount", QuestionCount);
            //
            p[17] = new SqlParameter("@NegativeMarkingFlag", NegativeMarkingFlag);
            p[18] = new SqlParameter("@IsChapterHide", HideChapterFlag);
            p[19] = new SqlParameter("@Slab_Code", Slab_Code);


            p[20] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[20].Direction = ParameterDirection.Output;

            p[21] = new SqlParameter("@Syllabus_Description", Syllabus_Description);
            // SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTest", p);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTest_New", p);
            return (int.Parse(p[20].Value.ToString()));
        }


        public static int Insert_Test_Set(string PKey, string Set_Number, int Question_No, string Subject_Code, string Chapter_Code, string Topic_Code, string Correct_Ans_Key, float Correct_Marks, float Wrong_Marks, string DiffLevel_Id,
        string Createdby, int ActionFlag, string Assesment_Engine_Test_Code, string Question_Type_Id, string SubTopic, string Module, string RefCourse_Code, string RefSubject_Code, string RuleId)
        {

            SqlParameter[] p = new SqlParameter[20];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@Set_Number", Set_Number);
            p[2] = new SqlParameter("@Question_No", Question_No);
            p[3] = new SqlParameter("@Subject_Code", Subject_Code);
            p[4] = new SqlParameter("@Chapter_Code", Chapter_Code);
            p[5] = new SqlParameter("@Topic_Code", Topic_Code);
            p[6] = new SqlParameter("@Correct_Ans_Key", Correct_Ans_Key);
            p[7] = new SqlParameter("@Correct_Marks", Correct_Marks);
            p[8] = new SqlParameter("@Wrong_Marks", Wrong_Marks);
            p[9] = new SqlParameter("@DiffLevel_Id", DiffLevel_Id);
            p[10] = new SqlParameter("@Createdby", Createdby);
            p[11] = new SqlParameter("@ActionFlag", ActionFlag);
            p[12] = new SqlParameter("@Assesment_Engine_Test_Code", Assesment_Engine_Test_Code);
            p[13] = new SqlParameter("@Question_Type_Id", Question_Type_Id);
            p[14] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[14].Direction = ParameterDirection.Output;
            p[15] = new SqlParameter("@SubTopic", SubTopic);
            p[16] = new SqlParameter("@Module", Module);
            p[17] = new SqlParameter("@RefCourse_Code", RefCourse_Code);
            p[18] = new SqlParameter("@RefSubject_Code", RefSubject_Code);
            p[19] = new SqlParameter("@RuleId", RuleId);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTest_Set", p);
            return (int.Parse(p[14].Value.ToString()));
        }

        //public static int Insert_Test_Set(string PKey, string Set_Number, int Question_No, string Subject_Code, string Chapter_Code, string Topic_Code, string Correct_Ans_Key, float Correct_Marks, float Wrong_Marks, string DiffLevel_Id,
        //string Createdby, int ActionFlag, string Assesment_Engine_Test_Code, string Question_Type_Id, string SubTopic, string Module,string RefCourse_Code, string RefSubject_Code)
        //{

        //    SqlParameter[] p = new SqlParameter[19];
        //    p[0] = new SqlParameter("@PKey", PKey);
        //    p[1] = new SqlParameter("@Set_Number", Set_Number);
        //    p[2] = new SqlParameter("@Question_No", Question_No);
        //    p[3] = new SqlParameter("@Subject_Code", Subject_Code);
        //    p[4] = new SqlParameter("@Chapter_Code", Chapter_Code);
        //    p[5] = new SqlParameter("@Topic_Code", Topic_Code);
        //    p[6] = new SqlParameter("@Correct_Ans_Key", Correct_Ans_Key);
        //    p[7] = new SqlParameter("@Correct_Marks", Correct_Marks);
        //    p[8] = new SqlParameter("@Wrong_Marks", Wrong_Marks);
        //    p[9] = new SqlParameter("@DiffLevel_Id", DiffLevel_Id);
        //    p[10] = new SqlParameter("@Createdby", Createdby);
        //    p[11] = new SqlParameter("@ActionFlag", ActionFlag);
        //    p[12] = new SqlParameter("@Assesment_Engine_Test_Code", Assesment_Engine_Test_Code);
        //    p[13] = new SqlParameter("@Question_Type_Id", Question_Type_Id);
        //    p[14] = new SqlParameter("@Result", SqlDbType.BigInt);
        //    p[14].Direction = ParameterDirection.Output;
        //    p[15] = new SqlParameter("@SubTopic", SubTopic);
        //    p[16] = new SqlParameter("@Module", Module);
        //    p[17] = new SqlParameter("@RefCourse_Code", RefCourse_Code);
        //    p[18] = new SqlParameter("@RefSubject_Code", RefSubject_Code);
        //    SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTest_Set", p);
        //    return (int.Parse(p[14].Value.ToString()));
        //}

        public static int UpdateTest_Authorise_Block(string PKey, int Flag, string AlteredBy)
        {

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@ActionFlag", Flag);
            p[2] = new SqlParameter("@AlteredBy", AlteredBy);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTest_Authorise_Block", p);
            return (int.Parse(p[3].Value.ToString()));
        }
        public static int UpdateTest_Delete(string PKey, string AlteredBy)
        {

            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@AlteredBy", AlteredBy);
            p[2] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTest_Delete", p);
            return (int.Parse(p[2].Value.ToString()));
        }

        public static string Insert_TestSchedule(string TestPKey, string CentreCode, string BatchCode, string Test_Date, int FromTime, int ToTime, string FromTimeStr, string ToTimeStr, float MaxMarks, string Remarks,
        string Createdby, string partner_code)
        {
            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@CentreCode", CentreCode);
            p[2] = new SqlParameter("@BatchCode", BatchCode);
            p[3] = new SqlParameter("@Test_Date", Test_Date);
            p[4] = new SqlParameter("@FromTime", FromTime);
            p[5] = new SqlParameter("@ToTime", ToTime);
            p[6] = new SqlParameter("@FromTimeStr", FromTimeStr);
            p[7] = new SqlParameter("@ToTimeStr", ToTimeStr);
            p[8] = new SqlParameter("@MaxMarks", MaxMarks);
            p[9] = new SqlParameter("@Remarks", Remarks);
            p[10] = new SqlParameter("@Createdby", Createdby);
            p[11] = new SqlParameter("@Partner_Code", partner_code);
            p[12] = new SqlParameter("@Result", SqlDbType.VarChar, 300);
            p[12].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertTestSchedule", p);
            return (p[12].Value.ToString());
        }


        public static int Update_TestSchedule(string TestPKey, string Test_Date, int FromTime, int ToTime, string FromTimeStr, string ToTimeStr, float MaxMarks, string Remarks, int IsActiveFlag, string AlteredBy,
        int Flag, string partner_Code)
        {

            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Test_Date", Test_Date);
            p[2] = new SqlParameter("@FromTime", FromTime);
            p[3] = new SqlParameter("@ToTime", ToTime);
            p[4] = new SqlParameter("@FromTimeStr", FromTimeStr);
            p[5] = new SqlParameter("@ToTimeStr", ToTimeStr);
            p[6] = new SqlParameter("@MaxMarks", MaxMarks);
            p[7] = new SqlParameter("@Remarks", Remarks);
            p[8] = new SqlParameter("@IsActiveFlag", IsActiveFlag);
            p[9] = new SqlParameter("@AlteredBy", AlteredBy);
            p[10] = new SqlParameter("@Flag", Flag);
            p[11] = new SqlParameter("@Partner_Code", partner_Code);
            p[12] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[12].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTestSchedule", p);
            return (int.Parse(p[12].Value.ToString()));
        }


        public static DataSet GetAllTestAttendanceActionType()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestAttendanceActionType"));
        }

        public static DataSet GetAllTestAttendanceEntityType(string Action_Id, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Action_Id", Action_Id);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActiveTestAttendanceEntityType", p1, p2));
        }

        public static DataSet GetTest_AnswerUploadHistory_ByPKey(string PKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTest_AnswerUploadHistory_ByPKey_New", p1, p2, p3));
        }

        public static DataSet GetClassroomMaster_ByPKey(string PKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetClassroomMaster_ByPKey", p1, p2));
        }

        public static DataSet GetStudent_ForTest_ByTestPKey(string TestPKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByTestPKey", p1, p2, p3));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByTestPKey_New", p1, p2, p3));

        }

        public static DataSet GetAllStudentDetails_ByTestPKey(string TestPKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStudentDetails_ByTestPKey", p1, p2));

        }


        public static DataSet GetStudent_ForAnswerSheetIssue_ByTestPKey(string TestPKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForAnswerSheetIssue_ByTestPKey", p1, p2, p3));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForAnswerSheetIssue_ByTestPKey_New", p1, p2, p3));
        }

        public static int Insert_StudentTestAttendace(string TestPKey, string ActionFlag, string SBEntryCode, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[3] = new SqlParameter("@Createdby", Createdby);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestAttendace", p);
            return (int.Parse(p[4].Value.ToString()));
        }
        public static int Insert_UpdateStudentTestAttendace(string TestPKey, string ActionFlag, string SBEntryCode, string Createdby, string AbsentReason, int IsRetest, Nullable<DateTime> RetestDate)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[3] = new SqlParameter("@Createdby", Createdby);
            p[4] = new SqlParameter("@AbsentReason", AbsentReason);
            p[5] = new SqlParameter("@IsRetest", IsRetest);
            p[6] = new SqlParameter("@RetestDate", RetestDate);
            p[7] = new SqlParameter("@Result", SqlDbType.Int);
            p[7].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertUpdateStudentTestAttendace", p);
            return (int.Parse(p[7].Value.ToString()));
        }




        public static int InsertAnswerSheet_Issue(string TestPKey, string PartnerCode, string Issue_Date, int Issue_Quantity, string SBEntryCode, string PaperChecker_SlabId, string Createdby, string Expected_Return_Date)
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@PartnerCode", PartnerCode);
            p[2] = new SqlParameter("@Issue_Date", Issue_Date);
            p[3] = new SqlParameter("@Issue_Quantity", Issue_Quantity);
            p[4] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[5] = new SqlParameter("@Createdby", Createdby);
            p[6] = new SqlParameter("@PaperChecker_SlabId", PaperChecker_SlabId);
            p[7] = new SqlParameter("@Expected_Return_Date", Expected_Return_Date);
            p[8] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertAnswerSheet_Issue", p);
            return (int.Parse(p[8].Value.ToString()));
        }
        public static int UpdateAnswerSheet_Issue(string TestPKey, DateTime Return_Date, int Returned_Quantity, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Return_Date", Return_Date);
            p[2] = new SqlParameter("@Returned_Quantity", Returned_Quantity);
            p[3] = new SqlParameter("@Createdby", Createdby);
            p[4] = new SqlParameter("@Result", SqlDbType.Int);
            p[4].Direction = ParameterDirection.Output;
            p[5] = new SqlParameter("@Flag", 1);
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Test_AnswerUpdateSheet_Issue", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int UpdateAnswerSheet_Issue_Edit(string TestPKey, DateTime Issue_Date, string Partner_Code, string PaperChecker_SlabId, string Createdby, DateTime Expected_Return_Date)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Issue_Date", Issue_Date);
            p[2] = new SqlParameter("@Createdby", Createdby);
            p[3] = new SqlParameter("Flag", 2);
            p[4] = new SqlParameter("@Result", SqlDbType.Int);
            p[4].Direction = ParameterDirection.Output;
            p[5] = new SqlParameter("@Partner_Code", Partner_Code);
            p[6] = new SqlParameter("@PaperChecker_SlabId", PaperChecker_SlabId);
            p[7] = new SqlParameter("@Expected_Return_Date", Expected_Return_Date);

            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Test_AnswerUpdateSheet_Issue", p);
            return (int.Parse(p[4].Value.ToString()));
        }






        public static int Insert_StudentTestMarks(string TestPKey, string SBEntryCode, string TestMarks, string MaxMarks, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[6];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[2] = new SqlParameter("@TestMarks", TestMarks);
            p[3] = new SqlParameter("@MaxMarks", MaxMarks);
            p[4] = new SqlParameter("@Createdby", Createdby);
            p[5] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[5].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestMarks", p);
            return (int.Parse(p[5].Value.ToString()));
        }

        public static int InsertStudentTestAttendace_Authorisation(string TestPKey, string ActionFlag, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@Createdby", Createdby);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestAttendace_Authorisation", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static int InsertStudentTestAbsentReason(string TestPKey, string AbsentReasonId, string SBEntryCode, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@AbsentReason_Id", AbsentReasonId);
            p[2] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[3] = new SqlParameter("@Createdby", Createdby);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestAbsentReason", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int InsertStudentTestMarks_Authorisation(string TestPKey, string ActionFlag, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@ActionFlag", ActionFlag);
            p[2] = new SqlParameter("@Createdby", Createdby);
            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudentTestMarks_Authorisation", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static string InsertStudent_Answer_Import(string TestPKey, int Conduct_Number, string Import_FileName, string Student_ID_Column_Name, int Correct_Record_Cnt, int Warning_Record_Cnt, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Conduct_Number", Conduct_Number);
            p[2] = new SqlParameter("@Import_FileName", Import_FileName);
            p[3] = new SqlParameter("@Student_ID_Column_Name", Student_ID_Column_Name);
            p[4] = new SqlParameter("@Correct_Record_Cnt", Correct_Record_Cnt);
            p[5] = new SqlParameter("@Warning_Record_Cnt", Warning_Record_Cnt);
            p[6] = new SqlParameter("@Createdby", Createdby);
            p[7] = new SqlParameter("@Result", SqlDbType.VarChar, 20);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import", p);
            return (p[7].Value.ToString());
        }

        public static int InsertStudent_Answer_Import_StudentAnswerKey(string TestPKey, string Centre_Code, int Conduct_Number, string SBEntryCode, int MCQ_Set_Number, ref string MCQ_Import_Run_No, string MCQ_Answer_Key, string Createdby)
        {
            SqlParameter[] p = new SqlParameter[9];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Centre_Code", Centre_Code);
            p[2] = new SqlParameter("@Conduct_Number", Conduct_Number);
            p[3] = new SqlParameter("@SBEntryCode", SBEntryCode);
            p[4] = new SqlParameter("@MCQ_Set_Number", MCQ_Set_Number);
            p[5] = new SqlParameter("@MCQ_Import_Run_No", MCQ_Import_Run_No);
            p[6] = new SqlParameter("@MCQ_Answer_Key", MCQ_Answer_Key);
            p[7] = new SqlParameter("@Createdby", Createdby);
            p[8] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[8].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import_StudentAnswerKey", p);
            return (int.Parse(p[8].Value.ToString()));
        }

        public static int InsertStudent_Answer_Import_Error_Item(string TestPKey, int Conduct_Number, string Import_Run_No, int Excel_Row_No, ref string Excel_Roll_No, string Excel_Set_No, string Excel_Error_Remarks)
        {
            SqlParameter[] p = new SqlParameter[8];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Conduct_Number", Conduct_Number);
            p[2] = new SqlParameter("@Import_Run_No", Import_Run_No);
            p[3] = new SqlParameter("@Excel_Row_No", Excel_Row_No);
            p[4] = new SqlParameter("@Excel_Roll_No", Excel_Roll_No);
            p[5] = new SqlParameter("@Excel_Set_No", Excel_Set_No);
            p[6] = new SqlParameter("@Excel_Error_Remarks", Excel_Error_Remarks);
            p[7] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[7].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import_Error_Item", p);
            return (int.Parse(p[7].Value.ToString()));
        }

        public static int InsertStudent_Answer_Import_Background_Process(string TestPKey, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Createdby", CreatedBy);
            p[2] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStudent_Answer_Import_Background_Process", p);
            return (int.Parse(p[2].Value.ToString()));

        }

        public static int Insert_Standard_Subject(string PKey, string Subject_ShortName, string Subject_ShortCode, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@PKey", PKey);
            p[1] = new SqlParameter("@Subject_ShortName", Subject_ShortName);
            p[2] = new SqlParameter("@Subject_ShortCode", Subject_ShortCode);
            p[3] = new SqlParameter("@CreatedBy", CreatedBy);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertStandardSubject", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Insert_Chapter(string DivisionCode, string YearName, string StandardCode, string SubjectCode, string ChapterName, double LectureCount, int LectureDuration, string ChapterShortName, string ChapterCodeForEdit, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@DivisionCode", DivisionCode);
            p[1] = new SqlParameter("@YearName", YearName);
            p[2] = new SqlParameter("@StandardCode", StandardCode);
            p[3] = new SqlParameter("@SubjectCode", SubjectCode);
            p[4] = new SqlParameter("@ChapterName", ChapterName);
            p[5] = new SqlParameter("@LectureCount", LectureCount);
            p[6] = new SqlParameter("@LectureDuration", LectureDuration);
            p[7] = new SqlParameter("@ChapterShortName", ChapterShortName);
            p[8] = new SqlParameter("@ChapterCodeForEdit", ChapterCodeForEdit);
            p[9] = new SqlParameter("@CreatedBy", CreatedBy);
            p[10] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[10].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertChapter", p);
            return (int.Parse(p[10].Value.ToString()));
        }

        public static string Insert_Classroom(string Classroom_LName, string Classroom_SName, double Length_inFeet, double Width_inFeet, double Height_inFeet, double Area_inSQFeet, string ClassroomType_Id, int IsActive, string Premises_Code, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@Classroom_LName", Classroom_LName);
            p[1] = new SqlParameter("@Classroom_SName", Classroom_SName);
            p[2] = new SqlParameter("@Length_inFeet", Length_inFeet);
            p[3] = new SqlParameter("@Width_inFeet", Width_inFeet);
            p[4] = new SqlParameter("@Height_inFeet", Height_inFeet);
            p[5] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[6] = new SqlParameter("@ClassroomType_Id", ClassroomType_Id);
            p[7] = new SqlParameter("@IsActive", IsActive);
            p[8] = new SqlParameter("@Premises_Code", Premises_Code);
            p[9] = new SqlParameter("@CreatedBy", CreatedBy);
            p[10] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[10].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertClassroom", p);
            return (p[10].Value.ToString());
        }

        public static int Insert_ClassroomCapacity(string Classroom_Code, string Activity_Id, int Capacity, string UOM)
        {

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@Classroom_Code", Classroom_Code);
            p[1] = new SqlParameter("@Activity_Id", Activity_Id);
            p[2] = new SqlParameter("@Capacity", Capacity);
            p[3] = new SqlParameter("@UOM", UOM);

            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertClassroom_Capacity", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int Insert_ClassroomCentre(string Classroom_Code, string Primary_Centre_Code, string Centre_Code, string Created_By)
        {

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@Classroom_Code", Classroom_Code);
            p[1] = new SqlParameter("@Primary_Centre_Code", Primary_Centre_Code);
            p[2] = new SqlParameter("@Centre_Code", Centre_Code);
            p[3] = new SqlParameter("@Created_By", Created_By);

            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertClassroom_Centre", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static int UpdateTestCancellation_Authorise(string PKey, int Flag, string Reason, string AlteredBy)
        {

            SqlParameter[] p = new SqlParameter[5];
            p[0] = new SqlParameter("@TestPKey", PKey);
            p[1] = new SqlParameter("@ActionFlag", Flag);
            p[2] = new SqlParameter("@Reason", Reason);
            p[3] = new SqlParameter("@AlteredBy", AlteredBy);
            p[4] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[4].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTestCancellation_Authorise", p);
            return (int.Parse(p[4].Value.ToString()));
        }

        public static DataSet GetClassroomMasterBy_Country_State_City(string Country_Code, string State_Code, string City_Code, string Location_Code, string ClassroomName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Country_Code", Country_Code);
            SqlParameter p2 = new SqlParameter("@State_Code", State_Code);
            SqlParameter p3 = new SqlParameter("@City_Code", City_Code);
            SqlParameter p4 = new SqlParameter("@Location_Code", Location_Code);
            SqlParameter p5 = new SqlParameter("@ClassroomName", ClassroomName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetClassroomMasterBy_Country_State_City", p1, p2, p3, p4, p5, p6));
        }

        public static string Update_Classroom(string Classroom_Code, string Premises_Code, string Classroom_LName, string Classroom_SName, double Length_inFeet, double Width_inFeet, double Height_inFeet, double Area_inSQFeet, string ClassroomType_Id, int IsActive,
        string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("@Classroom_Code", Classroom_Code);
            p[1] = new SqlParameter("@Premises_Code", Premises_Code);
            p[2] = new SqlParameter("@Classroom_LName", Classroom_LName);
            p[3] = new SqlParameter("@Classroom_SName", Classroom_SName);
            p[4] = new SqlParameter("@Length_inFeet", Length_inFeet);
            p[5] = new SqlParameter("@Width_inFeet", Width_inFeet);
            p[6] = new SqlParameter("@Height_inFeet", Height_inFeet);
            p[7] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[8] = new SqlParameter("@ClassroomType_Id", ClassroomType_Id);
            p[9] = new SqlParameter("@IsActive", IsActive);
            p[10] = new SqlParameter("@CreatedBy", CreatedBy);
            p[11] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[11].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateClassroom", p);
            return (p[11].Value.ToString());
        }

        public static string Insert_Premises(string CompanyCode, string Country_Code, string State_Code, string City_Code, string Location_Code, string Premises_LName, string Premises_SName, double Area_inSQFeet, string PremisesType_Id, int IsActive,
        string Premises_Address, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@CompanyCode", CompanyCode);
            p[1] = new SqlParameter("@Country_Code", Country_Code);
            p[2] = new SqlParameter("@State_Code", State_Code);
            p[3] = new SqlParameter("@City_Code", City_Code);
            p[4] = new SqlParameter("@Location_Code", Location_Code);
            p[5] = new SqlParameter("@Premises_LName", Premises_LName);
            p[6] = new SqlParameter("@Premises_SName", Premises_SName);
            p[7] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[8] = new SqlParameter("@PremisesType_Id", PremisesType_Id);
            p[9] = new SqlParameter("@IsActive", IsActive);
            p[10] = new SqlParameter("@CreatedBy", CreatedBy);
            p[11] = new SqlParameter("@Premises_Address", Premises_Address);
            p[12] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[12].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertPremises", p);
            return (p[12].Value.ToString());
        }

        public static string Update_Premises(string Premises_Code, string Location_Code, string Premises_LName, string Premises_SName, double Area_inSQFeet, string PremisesType_Id, int IsActive, string Premises_Address, string CreatedBy)
        {

            SqlParameter[] p = new SqlParameter[10];
            p[0] = new SqlParameter("@Premises_Code", Premises_Code);
            p[1] = new SqlParameter("@Location_Code", Location_Code);
            p[2] = new SqlParameter("@Premises_LName", Premises_LName);
            p[3] = new SqlParameter("@Premises_SName", Premises_SName);
            p[4] = new SqlParameter("@Area_inSQFeet", Area_inSQFeet);
            p[5] = new SqlParameter("@PremisesType_Id", PremisesType_Id);
            p[6] = new SqlParameter("@IsActive", IsActive);
            p[7] = new SqlParameter("@CreatedBy", CreatedBy);
            p[8] = new SqlParameter("@Premises_Address", Premises_Address);
            p[9] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[9].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdatePremises", p);
            return (p[9].Value.ToString());
        }

        public static DataSet GetPremisesMasterBy_Country_State_City(string Country_Code, string State_Code, string City_Code, string Location_Code, string PremisesName, string PremisesType_Id, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Country_Code", Country_Code);
            SqlParameter p2 = new SqlParameter("@State_Code", State_Code);
            SqlParameter p3 = new SqlParameter("@City_Code", City_Code);
            SqlParameter p4 = new SqlParameter("@Location_Code", Location_Code);
            SqlParameter p5 = new SqlParameter("@PremisesName", PremisesName);
            SqlParameter p6 = new SqlParameter("@PremisesType_Id", PremisesType_Id);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPremisesMasterBy_Country_State_City", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet GetPremisesMaster_ByPKey(string PKey, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPremisesMaster_ByPKey", p1, p2));
        }

        public static string Insert_Partner(string CompanyCode, string Title, string FirstName, string MiddleName, string LastName, string Gender, System.DateTime DOB, System.DateTime DOJ, string Qualification, string HandPhone1,
        string HandPhone2, string Landline, string EmailId, string FlatNo, string BuildingName, string StreetName, string Country_Code, string State_Code, string City_Code, string Location_Code,
        string Pincode, int IsActive, string CreatedBy, string ActivityCode, string DivisionCode, string EmployeeNo, string Area_Name, string Remarks)
        {

            SqlParameter[] p = new SqlParameter[29];
            p[0] = new SqlParameter("@CompanyCode", CompanyCode);
            p[1] = new SqlParameter("@Title", Title);
            p[2] = new SqlParameter("@FirstName", FirstName);
            p[3] = new SqlParameter("@MiddleName", MiddleName);
            p[4] = new SqlParameter("@LastName", LastName);
            p[5] = new SqlParameter("@Gender", Gender);
            p[6] = new SqlParameter("@DOB", DOB);
            p[7] = new SqlParameter("@DOJ", DOJ);
            p[8] = new SqlParameter("@Qualification", Qualification);
            p[9] = new SqlParameter("@HandPhone1", HandPhone1);
            p[10] = new SqlParameter("@HandPhone2", HandPhone2);
            p[11] = new SqlParameter("@Landline", Landline);
            p[12] = new SqlParameter("@EmailId", EmailId);
            p[13] = new SqlParameter("@FlatNo", FlatNo);
            p[14] = new SqlParameter("@BuildingName", BuildingName);
            p[15] = new SqlParameter("@StreetName", StreetName);
            p[16] = new SqlParameter("@Country_Code", Country_Code);
            p[17] = new SqlParameter("@State_Code", State_Code);
            p[18] = new SqlParameter("@City_Code", City_Code);
            p[19] = new SqlParameter("@Location_Code", Location_Code);
            p[20] = new SqlParameter("@Pincode", Pincode);
            p[21] = new SqlParameter("@IsActive", IsActive);
            p[22] = new SqlParameter("@CreatedBy", CreatedBy);
            p[23] = new SqlParameter("@ActivityCode", ActivityCode);
            p[24] = new SqlParameter("@DivisionCode", DivisionCode);
            p[25] = new SqlParameter("@EmployeeNo", EmployeeNo);
            p[26] = new SqlParameter("@Area_Name", Area_Name);
            p[27] = new SqlParameter("@Remarks", Remarks);
            p[28] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[28].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertPartner", p);
            return (p[28].Value.ToString());
        }

        public static string Update_Partner(string Partner_Code, string CompanyCode, string Title, string FirstName, string MiddleName, string LastName, string Gender, System.DateTime DOB, System.DateTime DOJ, string Qualification,
        string HandPhone1, string HandPhone2, string Landline, string EmailId, string FlatNo, string BuildingName, string StreetName, string Country_Code, string State_Code, string City_Code,
        string Location_Code, string Pincode, int IsActive, string CreatedBy, string ActivityCode, string DivisionCode, string EmployeeNo, string Area_Name, string Remarks)
        {

            SqlParameter[] p = new SqlParameter[30];
            p[0] = new SqlParameter("@CompanyCode", CompanyCode);
            p[1] = new SqlParameter("@Title", Title);
            p[2] = new SqlParameter("@FirstName", FirstName);
            p[3] = new SqlParameter("@MiddleName", MiddleName);
            p[4] = new SqlParameter("@LastName", LastName);
            p[5] = new SqlParameter("@Gender", Gender);
            p[6] = new SqlParameter("@DOB", DOB);
            p[7] = new SqlParameter("@DOJ", DOJ);
            p[8] = new SqlParameter("@Qualification", Qualification);
            p[9] = new SqlParameter("@HandPhone1", HandPhone1);
            p[10] = new SqlParameter("@HandPhone2", HandPhone2);
            p[11] = new SqlParameter("@Landline", Landline);
            p[12] = new SqlParameter("@EmailId", EmailId);
            p[13] = new SqlParameter("@FlatNo", FlatNo);
            p[14] = new SqlParameter("@BuildingName", BuildingName);
            p[15] = new SqlParameter("@StreetName", StreetName);
            p[16] = new SqlParameter("@Country_Code", Country_Code);
            p[17] = new SqlParameter("@State_Code", State_Code);
            p[18] = new SqlParameter("@City_Code", City_Code);
            p[19] = new SqlParameter("@Location_Code", Location_Code);
            p[20] = new SqlParameter("@Pincode", Pincode);
            p[21] = new SqlParameter("@IsActive", IsActive);
            p[22] = new SqlParameter("@CreatedBy", CreatedBy);
            p[23] = new SqlParameter("@ActivityCode", ActivityCode);
            p[24] = new SqlParameter("@DivisionCode", DivisionCode);
            p[25] = new SqlParameter("@EmployeeNo", EmployeeNo);
            p[26] = new SqlParameter("@Area_Name", Area_Name);
            p[27] = new SqlParameter("@Remarks", Remarks);
            p[28] = new SqlParameter("@Partner_Code", Partner_Code);
            p[29] = new SqlParameter("@Result", SqlDbType.VarChar, 10);
            p[29].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdatePartner", p);
            return (p[29].Value.ToString());
        }

        public static DataSet GetPartnerMasterBy_Country_State_City(string Country_Code, string State_Code, string City_Code, string Location_Code, string PartnerName, string HandPhoneNo, int ActiveStatus, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Country_Code", Country_Code);
            SqlParameter p2 = new SqlParameter("@State_Code", State_Code);
            SqlParameter p3 = new SqlParameter("@City_Code", City_Code);
            SqlParameter p4 = new SqlParameter("@Location_Code", Location_Code);
            SqlParameter p5 = new SqlParameter("@PartnerName", PartnerName);
            SqlParameter p6 = new SqlParameter("@HandPhone", HandPhoneNo);
            SqlParameter p7 = new SqlParameter("@ActiveStatus", ActiveStatus);
            SqlParameter p8 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPartnerMasterBy_Country_State_City", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet GetPartnerMasterBy_Division(string Division_Code, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetPartnerMasterBy_Division", p1, p2));
        }

        public static int Insert_RemoveTestRequest(string PKey, string Reason, string Created_By)
        {

            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@TestPKey", PKey);
            p[1] = new SqlParameter("@Reason", Reason);
            p[2] = new SqlParameter("@Createdby", Created_By);

            p[3] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Insert_RemoveTestRequest", p);
            return (int.Parse(p[3].Value.ToString()));
        }

        public static string Raise_Error(string Error_Code)
        {
            string Error_Desc = null;

            switch (Error_Code)
            {
                case "0000":
                    Error_Desc = "Record saved successfully";
                    break;
                case "0001":
                    Error_Desc = "Select Division";
                    break;
                case "0002":
                    Error_Desc = "Select Academic Year";
                    break;
                case "0003":
                    Error_Desc = "Select Course";
                    break;
                case "0004":
                    Error_Desc = "Select Product";
                    break;
                case "0005":
                    Error_Desc = "Select Subject";
                    break;
                case "0006":
                    Error_Desc = "Select Center";
                    break;
                case "0007":
                    Error_Desc = "Select Student(s)";
                    break;
                case "0008":
                    Error_Desc = "Number of Students selected is more than Maximum Batch Strength";
                    break;
                case "0009":
                    Error_Desc = "Enter New Batch Count";
                    break;
                case "0010":
                    Error_Desc = "Select Chapter(s)";
                    break;
                case "0011":
                    Error_Desc = "Select Test Mode";
                    break;
                case "0012":
                    Error_Desc = "Select Test Category";
                    break;
                case "0013":
                    Error_Desc = "Select Test Type";
                    break;
                case "0014":
                    Error_Desc = "Select Test Duration";
                    break;
                case "0015":
                    Error_Desc = "Select Batch";
                    break;
                case "0016":
                    Error_Desc = "Select Test Name";
                    break;
                case "0017":
                    Error_Desc = "Enter Maximum Marks";
                    break;
                case "0018":
                    Error_Desc = "Invalid Start Time";
                    break;
                case "0019":
                    Error_Desc = "Invalid End Time";
                    break;
                case "0020":
                    Error_Desc = "Start Time can't be after End Time";
                    break;
                case "0021":
                    Error_Desc = "Select Entity Type";
                    break;
                case "0022":
                    Error_Desc = "Select CSV File that you want to upload using Browse button";
                    break;
                case "0023":
                    Error_Desc = "File with the same name already processed for this test";
                    break;
                case "0024":
                    Error_Desc = "Invalid file format";
                    break;
                case "0025":
                    Error_Desc = "Enter Name of the column where Student ID is stored";
                    break;
                case "0026":
                    Error_Desc = "Invalid ID Column name";
                    break;
                case "0027":
                    Error_Desc = "No records found for importing";
                    break;
                case "0028":
                    Error_Desc = "Select Conduct Number";
                    break;
                case "0029":
                    Error_Desc = "Duplicate Test Name";
                    break;
                case "0030":
                    Error_Desc = "Select Student";
                    break;
                case "0031":
                    Error_Desc = "Attendance Authorisation can't be done as attendance of few Students is not marked";
                    break;
                case "0032":
                    Error_Desc = "Attendance Authorisation done successfully";
                    break;
                case "0033":
                    Error_Desc = "Attendance Authorisation removed successfully";
                    break;
                case "0034":
                    Error_Desc = "Marks Authorisation done successfully";
                    break;
                case "0035":
                    Error_Desc = "Marks Authorisation removed successfully";
                    break;
                case "0036":
                    Error_Desc = "Marks Authorisation can't be done as marks of few students are not entered";
                    break;
                case "0037":
                    Error_Desc = "File not found";
                    break;
                case "0038":
                    Error_Desc = "Test names matching with search options not found";
                    break;
                case "0039":
                    Error_Desc = "Student Answers reprocessed successfully";
                    break;
                case "0040":
                    Error_Desc = "Select Country";
                    break;
                case "0041":
                    Error_Desc = "Select State";
                    break;
                case "0042":
                    Error_Desc = "Select City";
                    break;
                case "0043":
                    Error_Desc = "Select Company";
                    break;
                case "0044":
                    Error_Desc = "Select Location";
                    break;
                case "0045":
                    Error_Desc = "Select Classroom Type";
                    break;
                case "0046":
                    Error_Desc = "Enter Classroom Length (in feet)";
                    break;
                case "0047":
                    Error_Desc = "Enter Classroom Width (in feet)";
                    break;
                case "0048":
                    Error_Desc = "Enter Classroom Height (in feet)";
                    break;
                case "0049":
                    Error_Desc = "Duplicate Classroom name";
                    break;
                case "0050":
                    Error_Desc = "Select Primary Owner Centre for the Classroom";
                    break;
                case "0051":
                    Error_Desc = "Select only 1 Centre as Primary Owner Centre for the Classroom";
                    break;
                case "0052":
                    Error_Desc = "Select Unit of Measurement for Classroom Capacity";
                    break;
                case "0053":
                    Error_Desc = "Select Title";
                    break;
                case "0054":
                    Error_Desc = "Enter First Name";
                    break;
                case "0055":
                    Error_Desc = "Enter Hand Phone number (1)";
                    break;
                case "0056":
                    Error_Desc = "Select Gender";
                    break;
                case "0057":
                    Error_Desc = "Select Activity";
                    break;
                case "0058":
                    Error_Desc = "Duplicate Partner details";
                    break;
                case "0059":
                    Error_Desc = "Invalid Hand Phone number (1)";
                    break;
                case "0060":
                    Error_Desc = "Invalid Hand Phone number (2)";
                    break;
                case "0061":
                    Error_Desc = "Enter Size of Premises in Sq. Feet";
                    break;
                case "0062":
                    Error_Desc = "Duplicate Premises name";
                    break;
                case "0063":
                    Error_Desc = "Select Premises Type";
                    break;
                case "0064":
                    Error_Desc = "Test Removal Request Approved successfully.";
                    break;
                case "0065":
                    Error_Desc = "Test Removal Request Rejected successfully";
                    break;
                case "0066":
                    Error_Desc = "Select Action";
                    break;
                case "0067":
                    Error_Desc = "Record deleted successfully";
                    break;
                case "0068":
                    Error_Desc = "Select Issuer Type";
                    break;
                case "0069":
                    Error_Desc = "Select Receiver Type";
                    break;
                case "0070":
                    Error_Desc = "Select Date Range";
                    break;

                default:
                    Error_Desc = Error_Code;
                    break;
            }
            return Error_Desc;
        }
        #endregion

        #region "Vivek Report"
        public static DataSet Report_Test_MCQ_Test_Subject_Student_Rank(string TestPKey, string SBEntryCode, int Flag, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_MCQ_Test_Subject_Student_Rank", p1, p2, p3));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_MCQ_Test_Subject_Student_Rank_New", p1, p2, p3, p4, p5));

        }

        public static DataSet Report_Test_NonMCQ_Test_Subject_Student_Rank(string TestPKey, string SBEntryCode, int Flag, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_NonMCQ_Test_Subject_Student_Rank", p1, p2, p3));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_NonMCQ_Test_Subject_Student_Rank_New", p1, p2, p3, p4, p5));


        }

        public static DataSet Report_Test_MCQ_Attendance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            SqlParameter p4 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@ToDate", ToDate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Absenteeism_New", p1, p2, p3, p4, p5, p6));
        }



        public static DataSet Report_Test_MCQ_Ranking(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string Batch_Code)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New_New", p1, p2, p3, p4, p5, p6, p7));

        }

        public static DataSet Report_Test_MCQ_Ranking(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, int MarksType)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@MarksType", MarksType);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New_New", p1, p2, p3, p4, p5, p6, p7));

        }

        public static DataSet Report_Test_MCQ_Ranking_Subject(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string Subject_Code)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_Subject", p1, p2, p3, p4, p5, p6, p7));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_Subject_New", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet Report_Test_MCQ_Ranking_Subject_Batch(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string Subject_Code, string Batch_Code)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p8 = new SqlParameter("@Subject_Code", Subject_Code);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_Subject", p1, p2, p3, p4, p5, p6, p7));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_Subject_New", p1, p2, p3, p4, p5, p6, p7, p8));
        }


        public static DataSet Report_Test_MCQ_Ranking_Subject(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string Subject_Code, int MarksType)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@Subject_Code", Subject_Code);
            SqlParameter p8 = new SqlParameter("@MarksType", MarksType);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_Subject", p1, p2, p3, p4, p5, p6, p7));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_Subject_New", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet Report_Test_PendingAuthorisation(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            SqlParameter p4 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@ToDate", ToDate);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_PendingAuthorisation", p1, p2, p3, p4));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_PendingAuthorisation_New", p1, p2, p3, p4, p5, p6));

        }
        #endregion

        #region "Vivek Dashboard"
        public static DataSet Dashboard_TestSchedule(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_TestSchedule_New", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet Dashboard_PendingAttendAuthorisation(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_PendingAttendAuthorisation_New", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet Dashboard_PendingMarksAuthorisation(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_PendingMarksAuthorisation_New", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet Dashboard_PendingTestCancellationAuthorisation(string Company_Code, string UserCode, string FromDate)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestForCancellation", p1, p2, p3));

        }



        public static DataSet Dashboard_Test(string Company_Code, string UserCode, string FromDate, string ToDate, string DBName, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@Company_Code", Company_Code);
            SqlParameter p2 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@DBName", DBName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_Test", p1, p2, p3, p4, p5, p6));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Dashboard_Test_New", p1, p2, p3, p4, p5, p6));

        }

        #endregion



        public static DataSet UpdateImagePath(string SBEntrycode, string fileext)
        {
            SqlParameter p1 = new SqlParameter("@SBEntrycode", SBEntrycode);
            SqlParameter p2 = new SqlParameter("@fileext", fileext);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateImagePath", p1, p2));

        }


        public static DataSet InsertSms_Email_Log(string FirstName, string MaxMark, string Mark, string TestDate, string TestName, string Percentage, string Message, string MobileNo, bool IsStudent, string EMailId, string MailBody, string CreatedBy)
        {
            SqlParameter p1 = new SqlParameter("@FirstName", FirstName);
            SqlParameter p2 = new SqlParameter("@MaxMark", MaxMark);
            SqlParameter p3 = new SqlParameter("@Mark", Mark);
            SqlParameter p4 = new SqlParameter("@TestDate", TestDate);
            SqlParameter p5 = new SqlParameter("@TestName", TestName);
            SqlParameter p6 = new SqlParameter("@Percentage", Percentage);
            SqlParameter p7 = new SqlParameter("@Message", Message);
            SqlParameter p8 = new SqlParameter("@MobileNo", MobileNo);
            SqlParameter p9 = new SqlParameter("@IsStudent", IsStudent);
            SqlParameter p10 = new SqlParameter("@EMailId", EMailId);
            SqlParameter p11 = new SqlParameter("@MailBody", MailBody);
            SqlParameter p12 = new SqlParameter("@CreatedBy", CreatedBy);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertSms_Email_Log", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12));

        }

        public static DataSet GetStudentDetailsBySBEntrycode(string SBEntrycode, int flag)
        {
            SqlParameter p1 = new SqlParameter("@SBEntryCode", SBEntrycode);
            SqlParameter p2 = new SqlParameter("@flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudentDetailsBySBEntryCode", p1, p2));

        }


        public static int GetCountRemoveTestRequest(string TestPKey)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_CountRemoveTestRequest", p1));
        }


        public static int Insert_UPdate_SubjectNEW(string Course_Code, string Course_Name, string SubjectName, string IsReference, string RefferenceCourseCode, string RefferenceSubjectCode, string SubjectDisplayName, string SubjectSequenceno, string Created_By, string Is_Active, string Flag, string recordnumber)
        {
            SqlParameter p1 = new SqlParameter("@Course_Code", Course_Code);
            SqlParameter p2 = new SqlParameter("@Course_Name", Course_Name);
            SqlParameter p3 = new SqlParameter("@SubjectName", SubjectName);
            SqlParameter p4 = new SqlParameter("@IsReference", IsReference);
            SqlParameter p5 = new SqlParameter("@RefferenceCourseCode", RefferenceCourseCode);
            SqlParameter p6 = new SqlParameter("@RefferenceSubjectCode", RefferenceSubjectCode);
            SqlParameter p7 = new SqlParameter("@SubjectDisplayName", SubjectDisplayName);
            SqlParameter p8 = new SqlParameter("@SubjectSequenceno", SubjectSequenceno);
            SqlParameter p9 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p10 = new SqlParameter("@Is_Active", Is_Active);
            SqlParameter p11 = new SqlParameter("@Flag", Flag);
            SqlParameter p12 = new SqlParameter("@recordnumber", recordnumber);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Insert_Update_SubjectNEW", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12));
        }

        //Batch
        public static DataSet GetAllClassroomProduct_ByPKEY(string PKey)
        {
            //Try
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@DBSource", "CDB");
            SqlParameter p3 = new SqlParameter("@Flag", 1);
            DataSet XYZ = SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetBatch_ByPKey_Classroom", p1, p2, p3);
            return (XYZ);
        }


        public static string GetDivisiononlyScience()
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetDivisiononlyScience"));
        }


        public static DataSet Check_SMSSendStatus(string PKey, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Pkey", PKey);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p1, p2));
        }

        public static DataSet Check_MesageTemplate(string Message_Code, string Division_Code, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Message_Code", Message_Code);
            SqlParameter p3 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p1, p2, p3));
        }

        public static int Insert_SMSLog(string Centre_Code, string Message_Code, string MobileNo, string SMSText, string SendStatus, string Sendby, string SMSType)
        {
            SqlParameter p0 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p1 = new SqlParameter("@Message_Code", Message_Code);
            SqlParameter p2 = new SqlParameter("@MobileNo", MobileNo);
            SqlParameter p3 = new SqlParameter("@SMSText", SMSText);
            SqlParameter p4 = new SqlParameter("@SendStatus", SendStatus);
            SqlParameter p5 = new SqlParameter("@Sendby", Sendby);
            SqlParameter p6 = new SqlParameter("@SMSType", SMSType);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertSMSLog", p0, p1, p2, p3, p4, p5, p6));
        }

        public static int Update_SMSSendStatus_T601(string Pkey, int Sentflag, string Mode, int flag)
        {
            SqlParameter p0 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p1 = new SqlParameter("@Sentflag", Sentflag);
            SqlParameter p2 = new SqlParameter("@SendingMode", Mode);
            SqlParameter p3 = new SqlParameter("@flag", flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p0, p1, p2, p3));
        }

        public static DataSet GetAllStudentDetails_ByTestPKeyForTestSchedule(string TestPKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllStudentDetails_ForTestScheduleSMS", p1, p2));

        }
        public static int Update_TestScheduleSMSSendStatus_T601(string Pkey, int Sentflag, string Mode, int flag)
        {
            SqlParameter p0 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p1 = new SqlParameter("@Sentflag", Sentflag);
            SqlParameter p2 = new SqlParameter("@SendingMode", Mode);
            SqlParameter p3 = new SqlParameter("@flag", flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Check_MessageTemplate", p0, p1, p2, p3));
        }




        public static DataSet Get_UnAuthorizedAnswerSheet_PaperChecker(string Division_Code, string Acad_Year, string Standard_Code, string Center_Code, DateTime FromDate, DateTime ToDate, string Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p3 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Test_Get_UnAuthorizedAnswerSheet_PaperChecker", p0, p1, p2, p3, p4, p5, p6));
        }

        

        public static DataSet GetSubjectCodeBY_Course(string Course, string SubjectName, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Cource_code", Course);
            SqlParameter p2 = new SqlParameter("@SubjectName", SubjectName);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_CodeDetails_QP", p1, p2, p3));
        }

        public static DataSet GetChapCodeBY_Course(string Division, string Course, string SubjectCode, string chapterName, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Cource_code", Course);
            SqlParameter p2 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p3 = new SqlParameter("@Division_code", Division);
            SqlParameter p4 = new SqlParameter("@chapterName", chapterName);
            SqlParameter p5 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_CodeDetails_QP", p1, p2, p3, p4, p5));
        }

        public static DataSet GetTopiCodeBY_Course(string Division, string Course, string SubjectCode, string chapter, string TopicName, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_code", Division);
            SqlParameter p2 = new SqlParameter("@Cource_code", Course);
            SqlParameter p3 = new SqlParameter("@Subject_Code", SubjectCode);
            SqlParameter p4 = new SqlParameter("@ChapterCode", chapter);
            SqlParameter p5 = new SqlParameter("@TopicName", TopicName);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_CodeDetails_QP", p1, p2, p3, p4, p5, p6));
        }


        public static int AnswerSheet_Issue_Authorised(string Division_Code, string Acad_Year, string Standard_Code, string Center_Code, string Test_ID, string Batch_Code, string Conduct_No, string Partner_Code,
         string PCSlab_Id, decimal PCAmount, decimal PCRate, int PCIsAuthorised, string PCAuthorised_By, DateTime PCAuthorised_On, string BagDispatch_ID,
         int PC_Is_Proceed_For_Payment, string PC_Is_Proceed_For_Payment_By, DateTime PC_Is_Proceed_For_Payment_On,
         int PC_Is_Payment_Done, string PC_Is_Payment_Done_By, DateTime PC_Is_Payment_Done_On, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p3 = new SqlParameter("@Test_ID", Test_ID);
            SqlParameter p4 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p5 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p6 = new SqlParameter("@Conduct_No", Conduct_No);
            SqlParameter p7 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p8 = new SqlParameter("@PCSlab_Id", PCSlab_Id);
            SqlParameter p9 = new SqlParameter("@PCAmount", PCAmount);
            SqlParameter p10 = new SqlParameter("@PCRate", PCRate);
            SqlParameter p11 = new SqlParameter("@PCIsAuthorised", PCIsAuthorised);
            SqlParameter p12 = new SqlParameter("@PCAuthorised_By", PCAuthorised_By);
            SqlParameter p13 = new SqlParameter("@PCAuthorised_On", PCAuthorised_On);
            SqlParameter p14 = new SqlParameter("@BagDispatch_ID", BagDispatch_ID);
            SqlParameter p15 = new SqlParameter("@PC_Is_Proceed_For_Payment", PC_Is_Proceed_For_Payment);
            SqlParameter p16 = new SqlParameter("@PC_Is_Proceed_For_Payment_By", PC_Is_Proceed_For_Payment_By);
            SqlParameter p17 = new SqlParameter("@PC_Is_Proceed_For_Payment_On", PC_Is_Proceed_For_Payment_On);
            SqlParameter p18 = new SqlParameter("@PC_Is_Payment_Done", PC_Is_Payment_Done);
            SqlParameter p19 = new SqlParameter("@PC_Is_Payment_Done_By", PC_Is_Payment_Done_By);
            SqlParameter p20 = new SqlParameter("@PC_Is_Payment_Done_On", PC_Is_Payment_Done_On);
            SqlParameter p21 = new SqlParameter("@Flag", Flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Test_AnswerSheet_Issue_Authorised", p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21));
        }

        public static DataSet GetPartnerCode_BySubCode(string Division, string acad_year, string standard_code, string subject_code)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division);
            SqlParameter p2 = new SqlParameter("@Acad_Year", acad_year);
            SqlParameter p3 = new SqlParameter("@standard_Code", standard_code);
            SqlParameter p4 = new SqlParameter("@Subject_Code", subject_code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_PartnerCode_BySubCodefor_test", p1, p2, p3, p4));

        }

        public static DataSet GettestCode_ForSubCode(string Pkey, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p2 = new SqlParameter("@flag", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_PartnerCode_BySubCodefor_test", p1, p2));

        }

        public static DataSet Report_Object_Test(string TestPKey, string SBEntryCode, string Other_Key)
        {
            SqlParameter p1 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p2 = new SqlParameter("@Test_Key", TestPKey);
            SqlParameter p3 = new SqlParameter("@Other_Key", Other_Key);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Test_Objective_Result", p1, p2, p3));

        }

        public static DataSet Get_Test_Name(string Division_Code, string YearName, string Standard_Code, string Batch_Code, string TestCategory_Id, string TestType_ID, string FromDate, string ToDate, string Centre_Code, string Conduct_No)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p8 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p9 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p10 = new SqlParameter("@Conduct_No", Conduct_No);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_Test_Name", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10));

        }

        // 17-05-2016   paper corrector payment details Archana
        public static DataSet Get_RPTPaperCorrector_Details(string Division_Code, string Standard_Code, string Centre_Code, DateTime ToDate, string Acad_Year, DateTime FromDate, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p2 = new SqlParameter("@Center_code", Centre_Code);
            SqlParameter p3 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p4 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetRpt_Paper_CorrectorPaymntDetail", p0, p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetMailDetails_ByCenter(string Center_Code, string MailType)
        {
            SqlParameter p1 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p2 = new SqlParameter("@MailType", MailType);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_MailDetails", p1, p2));
        }

        public static int Insert_Mailog(string Mail_To, string Subject, string Body, int Att_Flag, string Att_FileName, string sendstatus, string sendby, int flag, string Center_Code, string MailType)
        {
            SqlParameter p0 = new SqlParameter("@Mail_To", Mail_To);
            SqlParameter p1 = new SqlParameter("@Subject", Subject);
            SqlParameter p2 = new SqlParameter("@Body", Body);
            SqlParameter p3 = new SqlParameter("@Att_Flag", Att_Flag);
            SqlParameter p4 = new SqlParameter("@Att_FileName", Att_FileName);
            SqlParameter p5 = new SqlParameter("@SendStatus", sendstatus);
            SqlParameter p6 = new SqlParameter("@SendBy", sendby);
            SqlParameter p7 = new SqlParameter("@flag", flag);
            SqlParameter p8 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p9 = new SqlParameter("@MailType", MailType);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_InsertMailLog", p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }


        public static DataSet Dashboard_ReTest(string UserCode)
        {
            SqlParameter p1 = new SqlParameter("@User_Id", UserCode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetReTestDetail", p1));

        }

        // report paper corrector payment details
        public static DataSet Get_RPTPaperChecker_Details(string Division_Code, string Standard_Code, string Centre_Code, string Batch_Code, string Acad_Year, DateTime FromDate, DateTime ToDate)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p2 = new SqlParameter("@Center_code", Centre_Code);
            SqlParameter p3 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p4 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@ToDate", ToDate);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetRpt_PaperPaymntDetail", p0, p1, p2, p3, p4, p5, p6));
        }
        //fill_batch
        public static DataSet GetAllActive_Batch_Forcenter(string Division_Code, string YearName, string StandardCode, string CentreCode, string User_code)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);
            SqlParameter p5 = new SqlParameter("@Flag", 1);
            SqlParameter p6 = new SqlParameter("@User_code", User_code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_Forcenter", p1, p2, p3, p4, p5, p6));
        }


        //added for test cancellation dropdown Reason Vinod 16-05-2015

        public static DataSet GetAll_LectureTestReason()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_GetallTestCancel_Reason]"));
        }
        //added for test Abcent dropdown Reason Vinod 16-05-2015

        public static DataSet GetAllAbsentreasons()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetallTestAbcent_Reason"));
        }

        //Added for Rpt_Studentwise_Absentisum_Detailed Report for batch fill on Ceter "All" selection    18-05-20116

        public static DataSet GetAllActive_Batch_ForStandard_New(string Division_Code, string YearName, string StandardCode, string CentreCode)
        {
            SqlParameter p1 = new SqlParameter("@division_code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", StandardCode);
            SqlParameter p4 = new SqlParameter("@Centre_Code", CentreCode);


            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetAllActive_Batch_ForStandard_New", p1, p2, p3, p4));
        }

        //23-05-2016 added search for Rpt Attendance Authentocation search  Vnd

        public static DataSet GetAttendance_AuthorisationDetailed(string Division_Code, string Acad_Year, string course, string centerCode, DateTime FromDate, DateTime ToDate)
        {
            SqlParameter p0 = new SqlParameter("@DivisionCode", Division_Code);
            SqlParameter p1 = new SqlParameter("@AcadYear", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", course);
            SqlParameter p3 = new SqlParameter("@CenterCode", centerCode);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[Usp_GetAttendance_Authentication_Detailed]", p0, p1, p2, p3, p4, p5));
        }

        public static DataSet GetAttendance_Mark_AuthorisationDetailed(string Division_Code, string Acad_Year, string course, string centerCode, DateTime FromDate, DateTime ToDate, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@DivisionCode", Division_Code);
            SqlParameter p1 = new SqlParameter("@AcadYear", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", course);
            SqlParameter p3 = new SqlParameter("@CenterCode", centerCode);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[Usp_GetAttendance_Authentication_Detailed]", p0, p1, p2, p3, p4, p5, p6));
        }





        // added to fill roll no 18-05-20116
        public static DataSet GetRollNumber_batchcode(string Batchcode)
        {
            SqlParameter p1 = new SqlParameter("@BatchCode", Batchcode);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetRollNo_Batch", p1));
        }


        public static DataSet GetRollNumber_batchcode_Center_Batch(string Batchcode, string flag)
        {
            SqlParameter p1 = new SqlParameter("@BatchCode", Batchcode);
            SqlParameter p2 = new SqlParameter("@Flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetRollNo_Batch", p1, p2));
        }
        ///

        // 18-05-2016 addede for paper corrector payment summary  Archana

        public static DataSet Get_RPTPaperCorrector_PaymentSummary(string Division_Code, string Acad_Year, string CourseCode, DateTime FromDate, DateTime ToDate)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@StandardCode", CourseCode);
            SqlParameter p3 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetRpt_Paper_CorrectorPaymntSummary", p0, p1, p2, p3, p4));
        }

        public static DataSet Report_Faculty_Performance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_Report_FacultyPerformance]", p1, p2, p3, p4, p5, p6));

        }
        public static DataSet Report_Faculty_Performance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, int MarksType)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@MarksType", MarksType);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_FacultyPerformance", p1, p2, p3, p4, p5, p6, p7));

        }
        //
        public static DataSet Report_Test_MCQ_Ranking1(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string Batch)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@batch_Code", Batch);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New_New", p1, p2, p3, p4, p5, p6, p7));

        }
        public static DataSet Report_Test_Performance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_Report_TestPerformance]", p1, p2, p3, p4, p5, p6));

        }


        public static DataSet Report_Test_Performance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, int MarksType)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@MarksType", MarksType);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_TestPerformance", p1, p2, p3, p4, p5, p6, p7));

        }


        public static DataSet Report_MonthlyReport_Card(string Division_Code, string Course_code, string Batch_Code, string AcadYear, string ROll_No, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Standard_code", Course_code);
            SqlParameter p3 = new SqlParameter("@Batch_code", Batch_Code);
            SqlParameter p4 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p5 = new SqlParameter("@Roll_no", ROll_No);
            SqlParameter p6 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p7 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p8 = new SqlParameter("@ToDate", ToDate);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Student_MonthlyReport_Card", p1, p2, p3, p4, p5, p6, p7, p8));

        }

        // student Wise absent detailed
        public static DataSet Report_StudentWise_AbsentDetailed(string Division_Code, string Course_code, string Batch_Code, string AcadYear, string ROll_No, string Centre_Code, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Standard_code", Course_code);
            SqlParameter p3 = new SqlParameter("@Batch_code", Batch_Code);
            SqlParameter p4 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p5 = new SqlParameter("@Roll_no", ROll_No);
            SqlParameter p6 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p7 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p8 = new SqlParameter("@ToDate", ToDate);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Rpt_StudentWise_Absenteeism_Detailed", p1, p2, p3, p4, p5, p6, p7, p8));

        }
        // Student Attendance Remender Letter

        public static DataSet Attendance_Reminder_Letter(string Division_Code, string Course_code, string Batch_Code, string AcadYear, string Centre_Code, string FromDate, string ToDate, int flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Standard_code", Course_code);
            SqlParameter p3 = new SqlParameter("@Batch_code", Batch_Code);
            SqlParameter p4 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p7 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p8 = new SqlParameter("@flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Get_AbsentStudent_For_Attendance_Reminder", p1, p2, p3, p4, p5, p6, p7, p8));

        }
        public static DataSet Attendance_Reminder_Student(string Division_Code, string Course_code, string Batch_Code, string AcadYear, string Centre_Code, string FromDate, string ToDate, int flag, string rollno)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@Standard_code", Course_code);
            SqlParameter p3 = new SqlParameter("@Batch_code", Batch_Code);
            SqlParameter p4 = new SqlParameter("@Acad_Year", AcadYear);
            SqlParameter p5 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p6 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p7 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p8 = new SqlParameter("@flag", flag);
            SqlParameter p9 = new SqlParameter("@RollNo", rollno);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Get_AbsentStudent_For_Attendance_Reminder", p1, p2, p3, p4, p5, p6, p7, p8, p9));

        }

        public static DataSet Report_Test_Performance1(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, int mark)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@MarksType", mark);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_TestPerformance", p1, p2, p3, p4, p5, p6, p7));

        }

        public static DataSet Report_Test_Performance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string subject)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@Subject_Code", subject);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_TestPerformance", p1, p2, p3, p4, p5, p6, p7));

        }

        public static DataSet Report_Test_Performance(string TestPKey, string UserCode, int Flag, string Centre_Code, string FromDate, string ToDate, string subject, int MarksType)
        {
            SqlParameter p1 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p2 = new SqlParameter("@CreatedBy", UserCode);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            SqlParameter p7 = new SqlParameter("@Subject_Code", subject);
            SqlParameter p8 = new SqlParameter("@MarksType", MarksType);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Ranking_New", p1, p2, p3, p4, p5, p6));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_TestPerformance", p1, p2, p3, p4, p5, p6, p7, p8));

        }

        public static DataSet UPDATE_DBSYNCFLAG_LMSSERVICE(int Flag, int Inner_Flag, string Pkey, string Status_Code, string Reason_Phrase, string Created_By)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@Inner_Flag", Inner_Flag);
            SqlParameter p3 = new SqlParameter("@Pkey", Pkey);
            SqlParameter p4 = new SqlParameter("@Status_Code", Status_Code);
            SqlParameter p5 = new SqlParameter("@Reason_Phrase", Reason_Phrase);
            SqlParameter p6 = new SqlParameter("@Created_By", Created_By);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UPDATE_DBSYNCFLAG_WEBSERVICE", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet UpdateQPSetPath(string PKey, int Flag, string Path)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            SqlParameter p3 = new SqlParameter("@Path", Path);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMaster_ByPKey", p1, p2));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UPDATE_PATH_QPSET", p1, p2, p3));
        }


        public static int Update_Test_After_Authorisation(string TestCode, string DivisionCode, string YearName, string StandardCode, string Chapter_Code, string ModifiedBy)
        {


            SqlParameter p1 = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@StandardCode", StandardCode);
            SqlParameter p4 = new SqlParameter("@TestCode", TestCode);
            SqlParameter p5 = new SqlParameter("@ModifiedBy", ModifiedBy);
            SqlParameter p6 = new SqlParameter("@ChapterCode", Chapter_Code);



            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_UpdateTest_After_Authorisation", p1, p2, p3, p4, p5, p6));
        }


        public static DataSet Get_UnProcessedAnswerSheet_PaperChecker(string Division_Code, string Acad_Year, string Standard_Code, string Center_Code, DateTime FromDate, DateTime ToDate, string Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p3 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Test_Get_UnProcessedAnswerSheet_PaperChecker", p0, p1, p2, p3, p4, p5, p6));
        }

        public static DataSet GetQPSetPath(string PKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@PKey", PKey);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);

            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMaster_ByPKey", p1, p2));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GET_PATH_QPSET", p1, p2));
        }


        public static int Update_OMR_Sheets_Path(string TestCode, string DivisionCode, string YearName, string StandardCode, string OMR_Path, string ModifiedBy, int Flag)
        {


            SqlParameter p1 = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@StandardCode", StandardCode);
            SqlParameter p4 = new SqlParameter("@TestCode", TestCode);
            SqlParameter p5 = new SqlParameter("@ModifiedBy", ModifiedBy);
            SqlParameter p6 = new SqlParameter("@OMR_Path", OMR_Path);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);



            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_OMR_Sheets_Path", p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet Insert_Student_Test_Details_LMS(string SPID, string TestAssignStartDate, string TestAssignEndDate, string TestName, string TestId,
            string TotalQuestion, string Score, string OutOf, string CenterCode, string BatchCode, string ProductCode, string SkipQuestionCount, string RightAnswerCount, string InCorrectAnswerCount,
            string TestCompletedDate, string CenterName, string BatchName, string ProductName, string ExamMode, string ExamType, string Attendance, string Subjects, string Syllabus,
            string Correct, string InCorrect, string UnAnswered, string OverAllRank, string CenterRank, int Flag
            )
        {
            SqlParameter p1 = new SqlParameter("@SPID", SPID);
            SqlParameter p2 = new SqlParameter("@TestAssignStartDate", TestAssignStartDate);
            SqlParameter p3 = new SqlParameter("@TestAssignEndDate", TestAssignEndDate);
            SqlParameter p4 = new SqlParameter("@TestName", TestName);
            SqlParameter p5 = new SqlParameter("@TestId", TestId);
            SqlParameter p6 = new SqlParameter("@TotalQuestion", TotalQuestion);
            SqlParameter p7 = new SqlParameter("@Score", Score);
            SqlParameter p8 = new SqlParameter("@OutOf", OutOf);
            SqlParameter p9 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p10 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p11 = new SqlParameter("@ProductCode", ProductCode);
            SqlParameter p12 = new SqlParameter("@SkipQuestionCount", SkipQuestionCount);

            SqlParameter p13 = new SqlParameter("@RightAnswerCount", RightAnswerCount);
            SqlParameter p14 = new SqlParameter("@InCorrectAnswerCount", InCorrectAnswerCount);
            SqlParameter p15 = new SqlParameter("@TestCompletedDate", TestCompletedDate);
            SqlParameter p16 = new SqlParameter("@CenterName", CenterName);
            SqlParameter p17 = new SqlParameter("@BatchName", BatchName);
            SqlParameter p18 = new SqlParameter("@ProductName", ProductName);
            SqlParameter p19 = new SqlParameter("@ExamMode", ExamMode);
            SqlParameter p20 = new SqlParameter("@ExamType", ExamType);
            SqlParameter p21 = new SqlParameter("@Attendance", Attendance);
            SqlParameter p22 = new SqlParameter("@Subjects", Subjects);
            SqlParameter p23 = new SqlParameter("@Syllabus", Syllabus);
            SqlParameter p24 = new SqlParameter("@Correct", Correct);
            SqlParameter p25 = new SqlParameter("@InCorrect", InCorrect);
            SqlParameter p26 = new SqlParameter("@UnAnswered", UnAnswered);
            SqlParameter p27 = new SqlParameter("@OverAllRank", OverAllRank);
            SqlParameter p28 = new SqlParameter("@CenterRank", CenterRank);
            SqlParameter p29 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_UPDATE_STUDENT_TEST_DETAILS_LMS", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29));

        }


        public static DataSet Insert_Student_Test_Details_LMS_NEW(string XMLData, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@XMLData", XMLData);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_UPDATE_STUDENT_TEST_DETAILS_LMS", p1, p2));

        }


        public static DataSet rpt_GetTestScheduleBy_Division_Year_Standard_Centre(string Division_Code, string YearName, string Standard_Code, string centercode, string Testcategorycode, string TestTypecode, string batchcode, string TestName, string FromDate, string ToDate,
        int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", batchcode);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", Testcategorycode);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestTypecode);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p9 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p10 = new SqlParameter("@Centre_Code", centercode);
            SqlParameter p11 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestScheduleBy_Division_Year_Standard_Centre_Report", p1, p2, p3, p4, p5, p6, p7,
            p8, p9, p10, p11));

        }


        public static DataSet GetQuestionTypeIdByName(string QuestionType, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@QuestionType", QuestionType);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetQuestionTypeIdByName", p1, p2));
        }


        public static DataSet Process_Online_Test_Details(string Assesment_Test_Code, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Assesment_Test_Code", Assesment_Test_Code);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMaster_ByPKey", p1, p2));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_PROCESS_ONLINE_TEST_DETAILS", p1, p2));
        }


        public static DataSet GetStudent_ForTest_ByDivision_Centre_Standard_TestWise(string Division_Code, string YearName, string Centre_Code, string Standard_Code, string Batch_Code, string DBSource, int Flag, string TestPKey)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p5 = new SqlParameter("@Batch_Code", Batch_Code);
            SqlParameter p6 = new SqlParameter("@DBSource", DBSource);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);
            SqlParameter p8 = new SqlParameter("@TestPKey", TestPKey);
            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByDivision_Centre_Standard", p1, p2, p3, p4, p5, p6, p7));

            //return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByDivision_Centre_Standard_New", p1, p2, p3, p4, p5, p6, p7));
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetStudent_ForTest_ByDivision_Centre_Standard_New_TestWise", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet Get_Rpt_test_Performance_LEPL(string Centre_Code, string TestPkey, string Userid, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p2 = new SqlParameter("@TestPkey", TestPkey);
            SqlParameter p3 = new SqlParameter("@Userid", Userid);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Test_Performance_LEPL", p1, p2, p3, p4));
        }

        public static DataSet Get_Rpt_test_Performance_Detailed(string Centre_Code, string TestPkey, string Userid, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p2 = new SqlParameter("@TestPkey", TestPkey);
            SqlParameter p3 = new SqlParameter("@Userid", Userid);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Test_Performance_Detailed", p1, p2, p3, p4));
        }

        public static DataSet GetTestStudentDetail_ByPKey(string RollNo, string CenterCode, string TestPKey, int Conduct_Number, string UserID, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@RollNo", RollNo);
            SqlParameter p2 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p3 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p4 = new SqlParameter("@Conduct_Number", Conduct_Number);
            SqlParameter p5 = new SqlParameter("@UserID", UserID);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestStudentDetail_ByPKey", p1, p2, p3, p4, p5, p6));
        }

        public static int AutoAttendance_Closure(string TestPKey, int conductNo, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Createdby", CreatedBy);
            p[2] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[3] = new SqlParameter("@ConductNo", conductNo);

            p[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_AutoObjectiveTestAttendanceClosure", p);
            return (int.Parse(p[2].Value.ToString()));
        }


        public static DataSet Get_Rpt_test_Chapterwise_Analysis(string Centre_Code, string BatchCode, string TestPkey, string Userid, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p1 = new SqlParameter("@Batch_Code", BatchCode);
            SqlParameter p2 = new SqlParameter("@TestPkey", TestPkey);
            SqlParameter p3 = new SqlParameter("@Userid", Userid);
            SqlParameter p4 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Test_Chapterwise_Analysis", p0, p1, p2, p3, p4));
        }

        public static DataSet Get_Rpt_test_Questionwise_Analysis(string TestPkey, string Userid, string Centre_Code, string Batch_Code, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@TestPkey", TestPkey);
            SqlParameter p1 = new SqlParameter("@Userid", Userid);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);
            SqlParameter p3 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p4 = new SqlParameter("@Batch_Code", Batch_Code);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Test_Questionwise_Analysis", p0, p1, p2, p3, p4));
        }

        public static DataSet Get_Rpt_Process_Online_Test_Details(string Div_Code, string Acad_Year, string Standard_Code, string Category_Id, string TestCode, string AssessmentTestCode, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@Div_Code", Div_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p3 = new SqlParameter("@Category_Id", Category_Id);
            SqlParameter p4 = new SqlParameter("@TestCode", TestCode);
            SqlParameter p5 = new SqlParameter("@AssessmentTestCode", AssessmentTestCode);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Process_Online_Test_Detail", p0, p1, p2, p3, p4, p5, p6));
        }

        public static DataSet Update_AutoTestStudentAttendance_ByPKey(string SBEntryCode, string TestPKey, int Conduct_Number, string UserID, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@SBEntryCode", SBEntryCode);
            SqlParameter p2 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p3 = new SqlParameter("@Conduct_Number", Conduct_Number);
            SqlParameter p4 = new SqlParameter("@UserID", UserID);
            SqlParameter p5 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Update_Test_Attendance", p1, p2, p3, p4, p5));
        }

        public static DataSet Get_Report_Assessment_Code_Status(string Division_Code, string YearName, string Standard_Code, string Test_Category, string TestPKey, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@DivCode", Division_Code);
            SqlParameter p2 = new SqlParameter("@Acad_Year", YearName);
            SqlParameter p3 = new SqlParameter("@Course_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@Test_Category", Test_Category);
            SqlParameter p5 = new SqlParameter("@TestPKey", TestPKey);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Report_Test_Assessment_Code", p1, p2, p3, p4, p5, p6));
        }

        public static DataSet Get_Report_PaymentDone(string xmlData, string Created_By, string Flag)
        {
            SqlParameter p1 = new SqlParameter("@xmlData", xmlData);
            SqlParameter p2 = new SqlParameter("@Created_By", Created_By);
            SqlParameter p3 = new SqlParameter("@Flag", Flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_PaperCorrector_PaymentDone_Export", p1, p2, p3));
        }

        public static string Insert_Test_Schedule_Upload(string Division_Code, string Acad_Year, string Standard_Code, string Test_Name, string CentreShortName,
         string BatchShortName, string Test_Date, int FromTime, int ToTime, string FromTimeStr, string ToTimeStr, int MaxMarks, string Remarks,
         string Createdby, string Import_Code)
        {
            SqlParameter[] p = new SqlParameter[16];
            p[0] = new SqlParameter("@Division_Code", Division_Code);
            p[1] = new SqlParameter("@Acad_Year", Acad_Year);
            p[2] = new SqlParameter("@Statndard_Code", Standard_Code);
            p[3] = new SqlParameter("@Test_Name", Test_Name);
            p[4] = new SqlParameter("@CentreShortName", CentreShortName);
            p[5] = new SqlParameter("@BatchShortName", BatchShortName);
            p[6] = new SqlParameter("@Test_Date", Test_Date);
            p[7] = new SqlParameter("@FromTime", FromTime);
            p[8] = new SqlParameter("@ToTime", ToTime);
            p[9] = new SqlParameter("@FromTimeStr", FromTimeStr);
            p[10] = new SqlParameter("@ToTimeStr", ToTimeStr);
            p[11] = new SqlParameter("@MaxMarks", MaxMarks);
            p[12] = new SqlParameter("@Remarks", Remarks);
            p[13] = new SqlParameter("@Createdby", Createdby);
            p[14] = new SqlParameter("@Import_Code", Import_Code);
            p[15] = new SqlParameter("@ReturnMessage", SqlDbType.VarChar, 1000);
            p[15].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_TEST_SCHEDULE_UPLOAD", p);
            return ((p[15].Value.ToString()));
        }


        public static int AutoMarksClosureForOnlineTest(string TestPKey, string CreatedBy)
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@TestPKey", TestPKey);
            p[1] = new SqlParameter("@Createdby", CreatedBy);
            p[2] = new SqlParameter("@Result", SqlDbType.BigInt);
            p[2].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_AUTO_MARKS_CLOSURE", p);
            return (int.Parse(p[2].Value.ToString()));

        }

        public static DataSet INSERT_LOG_EXCEL_IMPORT(string Flag, string Import_Code, string Import_Function, string Import_File_Name, int Record_Count, string Created_By)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@Import_Code", Import_Code);
            SqlParameter p3 = new SqlParameter("@Import_Function", Import_Function);
            SqlParameter p4 = new SqlParameter("@Import_File_Name", Import_File_Name);
            SqlParameter p5 = new SqlParameter("@Record_Count", Record_Count);
            SqlParameter p6 = new SqlParameter("@Created_By", Created_By);

            //SqlParameter p6 = new SqlParameter("@ReturnMessage", SqlDbType.VarChar, 50);
            //p6.Direction = ParameterDirection.Output;
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_LOG_EXCEL_IMPORT", p1, p2, p3, p4, p5, p6));
            //SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_LOG_EXCEL_IMPORT", p1,p2,p3,p4,p5);
            //return ((p6.Value.ToString()));
        }

        public static DataSet GetQuestionRuleName(string QuestionRule, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@QuestionRule", QuestionRule);
            SqlParameter p2 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_GetQuestionRuleName", p1, p2));
        }

        public static DataSet GetTestDetailsForAssigningTestCode(string Division_Code, string YearName, string Standard_Code, string TestMode_Id, string TestCategory_Id, string TestType_ID, string TestName, int Flag)
        {
            SqlParameter p1 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p4 = new SqlParameter("@TestMode_Id", TestMode_Id);
            SqlParameter p5 = new SqlParameter("@TestCategory_Id", TestCategory_Id);
            SqlParameter p6 = new SqlParameter("@TestType_ID", TestType_ID);
            SqlParameter p7 = new SqlParameter("@TestName", TestName);
            SqlParameter p8 = new SqlParameter("@Flag", Flag);

            // return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetTestMasterBy_Division_Year_Standard", p1, p2, p3, p4, p5, p6, p7,
            // p8));

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GET_TEST_DETAILS_FOR_ASSIGNING_TESTCODE", p1, p2, p3, p4, p5, p6, p7,
             p8));
        }

        public static string Insert_QPSetUpload(string Assesment_Test_Code, int QueNo, string QueType, string AnswerKey, string DifficultyLevel,
         string CorrectMarks, string WrongMarks, string SubjectName, string RefCourseName, string RefSubjectName, string ChapterName, string TopicName, string SubTopicName,
         string ModuleName, string RuleName, string Createdby, string Import_Code)
        {
            SqlParameter[] p = new SqlParameter[18];
            p[0] = new SqlParameter("@Assesment_Test_Code", Assesment_Test_Code);
            p[1] = new SqlParameter("@QueNo", QueNo);
            p[2] = new SqlParameter("@QueType", QueType);
            p[3] = new SqlParameter("@AnswerKey", AnswerKey);
            p[4] = new SqlParameter("@DifficultyLevel", DifficultyLevel);
            p[5] = new SqlParameter("@CorrectMarks", CorrectMarks);
            p[6] = new SqlParameter("@WrongMarks", WrongMarks);
            p[7] = new SqlParameter("@SubjectName", SubjectName);
            p[8] = new SqlParameter("@RefCourseName", RefCourseName);
            p[9] = new SqlParameter("@RefSubjectName", RefSubjectName);
            p[10] = new SqlParameter("@ChapterName", ChapterName);
            p[11] = new SqlParameter("@TopicName", TopicName);
            p[12] = new SqlParameter("@SubTopicName", SubTopicName);
            p[13] = new SqlParameter("@ModuleName", ModuleName);
            p[14] = new SqlParameter("@RuleName", RuleName);
            p[15] = new SqlParameter("@Createdby", Createdby);
            p[16] = new SqlParameter("@Import_Code", Import_Code);
            p[17] = new SqlParameter("@ReturnMessage", SqlDbType.VarChar, 1000);
            p[17].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_QPSET_ITEMS_UPLOAD", p);
            return ((p[17].Value.ToString()));
        }

        //Digambar added on 12 Jul 2017
        public static DataSet Get_Rpt_test_Concise_Subjectwise_Chapterwise_Analysis(string AcadYear, string Centre_Code, string CourseCode,
                 string BatchCode, string Test_Category, string TestPkey, string FromDate, string ToDate, string Userid, int Flag)
        {

            SqlParameter p0 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p1 = new SqlParameter("@Centre_Code", Centre_Code);
            SqlParameter p2 = new SqlParameter("@CourseCode", CourseCode);
            SqlParameter p3 = new SqlParameter("@Batch_Code", BatchCode);
            SqlParameter p4 = new SqlParameter("@Test_Category", Test_Category);
            SqlParameter p5 = new SqlParameter("@TestPkey", TestPkey);
            SqlParameter p6 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p7 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p8 = new SqlParameter("@Userid", Userid);
            SqlParameter p9 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Rpt_Test_Concise_Subjectwise_Chapterwise_Analysis", p0, p1, p2, p3, p4, p5, p6, p7, p8, p9));
        }

        public static DataSet GetApplication_Url()
        {
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_Application_URL"));
        }

        public static DataSet GetMenuList(string Flag, string User_Code, string Menu_Code)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@User_Code", User_Code);
            SqlParameter p3 = new SqlParameter("@Menu_Code", Menu_Code);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Get_Menu_List", p1, p2, p3));
        }

        public static DataSet Get_RPTPaperCorrector_Details_And_TeacherDetails(string Division_Code, string Standard_Code, string Centre_Code, string Acad_Year, string FromDate, string ToDate, string UserCode, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p2 = new SqlParameter("@Center_code", Centre_Code);
            SqlParameter p3 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p4 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@UserCode", UserCode);
            SqlParameter p7 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GetRpt_Paper_Corrector_And_Teacher_Details", p0, p1, p2, p3, p4, p5, p6, p7));
        }

        public static DataSet UpdateTest_Schedule_Paper_Corrector_Details(string Flag, string DivisionCode, string AcadYear, string StandardCode, string CenterCode,
        string BatchCode, string TestId, int ConductNo, string TestBagPkey, int FromTime, int ToTime, string FromTimeStr, string ToTimeStr, string PartnerCode, string SlabId, string CreatedBy)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@DivisionCode", DivisionCode);
            SqlParameter p3 = new SqlParameter("@AcadYear", AcadYear);
            SqlParameter p4 = new SqlParameter("@StandardCode", StandardCode);
            SqlParameter p5 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p6 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p7 = new SqlParameter("@TestId", TestId);
            SqlParameter p8 = new SqlParameter("@ConductNo", ConductNo);
            SqlParameter p9 = new SqlParameter("@TestBagPkey", TestBagPkey);
            SqlParameter p10 = new SqlParameter("@FromTime", FromTime);
            SqlParameter p11 = new SqlParameter("@ToTime", ToTime);
            SqlParameter p12 = new SqlParameter("@FromTimeStr", FromTimeStr);
            SqlParameter p13 = new SqlParameter("@ToTimeStr", ToTimeStr);
            SqlParameter p14 = new SqlParameter("@PartnerCode", PartnerCode);
            SqlParameter p15 = new SqlParameter("@SlabId", SlabId);
            SqlParameter p16 = new SqlParameter("@CreatedBy", CreatedBy);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_UPDATE_TEST_SCHEDULE_PAPER_CORRECTOR_DETAILS", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16));

        }


        public static string Insert_Test_Supervisor_Details(string Flag, string SupervisorCategory, string DivisionCode, string AcadYear, string StandardCode,
        string TestName, string CenterName, string BatchShortName, int ConductNo, string TestDate, string TestTiming, string Hrs, string Rate,
        string Amt, string PartnerCode, string Remarks, string Createdby, string Import_Code)
        {
            SqlParameter[] p = new SqlParameter[19];
            p[0] = new SqlParameter("@Flag", Flag);
            p[1] = new SqlParameter("@SupervisorCategory", SupervisorCategory);
            p[2] = new SqlParameter("@DivisionCode", DivisionCode);
            p[3] = new SqlParameter("@AcadYear", AcadYear);
            p[4] = new SqlParameter("@StandardCode", StandardCode);
            p[5] = new SqlParameter("@TestName", TestName);
            p[6] = new SqlParameter("@CenterName", CenterName);
            p[7] = new SqlParameter("@BatchShortName", BatchShortName);
            p[8] = new SqlParameter("@ConductNo", ConductNo);
            p[9] = new SqlParameter("@TestDate", TestDate);
            p[10] = new SqlParameter("@TestTiming", TestTiming);
            p[11] = new SqlParameter("@Hrs", Hrs);
            p[12] = new SqlParameter("@Rate", Rate);
            p[13] = new SqlParameter("@Amt", Amt);
            p[14] = new SqlParameter("@PartnerCode", PartnerCode);
            p[15] = new SqlParameter("@Remarks", Remarks);
            p[16] = new SqlParameter("@Createdby", Createdby);
            p[17] = new SqlParameter("@Import_Code", Import_Code);
            p[18] = new SqlParameter("@ReturnMessage", SqlDbType.VarChar, 1000);
            p[18].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_INSERT_UPDATE_TEST_SUPERVISION_UPLOAD", p);
            return ((p[18].Value.ToString()));
        }


        public static DataSet Get_Test_Supervisor_Payment_Summary(string Division_Code, string Acad_Year, string Standard_Code, string Center_Code, DateTime FromDate, DateTime ToDate, string Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p3 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_RPT_PAYMENT_SUMMARY_TEST_SUPERVISOR", p0, p1, p2, p3, p4, p5, p6));
        }

        public static DataSet Report_MarkeENtry(string Flag, string TestPkey, string CenterCode, string BatchCode, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@TestPkey", TestPkey);
            SqlParameter p3 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p4 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@ToDate", ToDate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_GET_RPT_TEST_MARKS_ENTRY", p1, p2, p3, p4, p5, p6));

        }

        public static DataSet Report_TestScheduled(string Flag, string TestPkey, string CenterCode, string BatchCode, string FromDate, string ToDate)
        {
            SqlParameter p1 = new SqlParameter("@Flag", Flag);
            SqlParameter p2 = new SqlParameter("@TestPkey", TestPkey);
            SqlParameter p3 = new SqlParameter("@CenterCode", CenterCode);
            SqlParameter p4 = new SqlParameter("@BatchCode", BatchCode);
            SqlParameter p5 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p6 = new SqlParameter("@ToDate", ToDate);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_RPT_TEST_SCHEDULED", p1, p2, p3, p4, p5, p6));

        }


        /// <summary>
        /// Supervisor and Telecallers
        /// </summary>
        /// <returns></returns>

        public static DataSet Get_UnAuthorizedAnswerSupervisor_TC_payment(string Division_Code, string Acad_Year, string Standard_Code, string Center_Code, DateTime FromDate, DateTime ToDate, string Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Standard_Code", Standard_Code);
            SqlParameter p3 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p4 = new SqlParameter("@FromDate", FromDate);
            SqlParameter p5 = new SqlParameter("@ToDate", ToDate);
            SqlParameter p6 = new SqlParameter("@Flag", Flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "USP_Get_UnAuthorized_Supervisor_TC_List", p0, p1, p2, p3, p4, p5, p6));
        }

        public static int Supervisor_TC_Authorised(string Division_Code, string Acad_Year, string Center_Code,
         string Record_id, string Partner_Code, int Is_Payment_Done, string Is_Payment_Done_By, DateTime Is_Payment_Done_On, int Flag)
        {
            SqlParameter p0 = new SqlParameter("@Division_Code", Division_Code);
            SqlParameter p1 = new SqlParameter("@Acad_Year", Acad_Year);
            SqlParameter p2 = new SqlParameter("@Record_Id", Record_id);
            SqlParameter p3 = new SqlParameter("@Center_Code", Center_Code);
            SqlParameter p4 = new SqlParameter("@Partner_Code", Partner_Code);
            SqlParameter p5 = new SqlParameter("@Is_Payment_Done", Is_Payment_Done);
            SqlParameter p6 = new SqlParameter("@Is_Payment_Done_By", Is_Payment_Done_By);
            SqlParameter p7 = new SqlParameter("@Is_Payment_Done_On", Is_Payment_Done_On);
            SqlParameter p8 = new SqlParameter("@Flag", Flag);

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "Usp_Supervisor_TC_Authorised", p0, p1, p2, p3, p4, p5, p6, p7, p8));
        }



        public static DataSet GetStudentBy_Division_Year_Standard_Centre(string Division_Code, string coursecode, string centercode, string Productcode, string acadyear, string Fromdate, string Todate, string flag)
        {
            SqlParameter p1 = new SqlParameter("@DIVISIONCODE", Division_Code);
            SqlParameter p2 = new SqlParameter("@COURSECODE", coursecode);
            SqlParameter p3 = new SqlParameter("@CENTERCODE", centercode);
            SqlParameter p4 = new SqlParameter("@PRODUCTCODE", Productcode);
            SqlParameter p5 = new SqlParameter("@ACADYEAR", acadyear);
            SqlParameter p6 = new SqlParameter("@FROMDATE", Fromdate);
            SqlParameter p7 = new SqlParameter("@TODATE", Todate);
            SqlParameter p8 = new SqlParameter("@falg", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "RPT_GET_STUDENT_TEST_ABSENTDETAILS", p1, p2, p3, p4, p5, p6, p7, p8));
        }

        public static DataSet GetAllActive_Product(string Division_Code, string YearName, string course, string Product, string center, string flag)
        {
            SqlParameter p1 = new SqlParameter("@divisioncode", Division_Code);
            SqlParameter p2 = new SqlParameter("@YearName", YearName);
            SqlParameter p3 = new SqlParameter("@coursecode", course);
            SqlParameter p4 = new SqlParameter("@productcode", Product);
            SqlParameter p5 = new SqlParameter("@Centercode", center);
            SqlParameter p6 = new SqlParameter("@Flag", flag);
            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[USP_GetLMSproduct_For_TestReport]", p1, p2, p3, p4, p5, p6));
        }

        /// <summary>
        /// Supervisor Report
        /// </summary>
        /// <param name="Division_Code"></param>
        /// <param name="centercode"></param>
        /// <param name="paymentstatus"></param>
        /// <param name="acadyear"></param>
        /// <param name="Fromdate"></param>
        /// <param name="Todate"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static DataSet GetSupervisorBy_Division_Year_Standard_Centre(string Division_Code, string centercode, string paymentstatus, string acadyear, string Fromdate, string Todate, string flag)
        {
            SqlParameter p1 = new SqlParameter("@DIVISIONCODE", Division_Code);
            SqlParameter p2 = new SqlParameter("@CENTERCODE", centercode);
            SqlParameter p3 = new SqlParameter("@PAYMENTSTATUS", paymentstatus);
            SqlParameter p4 = new SqlParameter("@ACADYEAR", acadyear);
            SqlParameter p5 = new SqlParameter("@FROMDATE", Fromdate);
            SqlParameter p6 = new SqlParameter("@TODATE", Todate);
            SqlParameter p7 = new SqlParameter("@falg", flag);

            return (SqlHelper.ExecuteDataset(ConnectionString.GetConnectionString(), CommandType.StoredProcedure, "[RPT_GET_TEST_Supervisor_DETAILS]", p1, p2, p3, p4, p5, p6, p7));
        }



    }
}
