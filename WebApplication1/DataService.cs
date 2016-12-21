using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApplication1
{
    public class DataService
    {
        private static string connectionstring = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.10.4.3)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME = orcl)));User ID=system;Password=manager";
        public static void get_staff(string user_name, out string dept_name, out string name, out string password, out string role)
        {
            using (OracleConnection myConn = new OracleConnection(connectionstring))
            {
                myConn.Open();
                OracleCommand myCmd = new OracleCommand("get_staff", myConn);
                myCmd.CommandType = CommandType.StoredProcedure;
                myCmd.Parameters.Add("inUSER_NAME", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Input;
                myCmd.Parameters.Add("outDEPT_NAME", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                myCmd.Parameters.Add("outNAME", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                myCmd.Parameters.Add("outPASSWORD", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Output;
                myCmd.Parameters.Add("outROLE", OracleDbType.Varchar2, 200).Direction = ParameterDirection.Output;
                myCmd.Parameters["inUSER_NAME"].Value = user_name;
                myCmd.ExecuteNonQuery();
                dept_name = myCmd.Parameters["outDEPT_NAME"].Value.ToString();
                name = myCmd.Parameters["outNAME"].Value.ToString();
                password = myCmd.Parameters["outPASSWORD"].Value.ToString();
                role = myCmd.Parameters["outROLE"].Value.ToString();
                myConn.Close();
            }
        }
        
        public static void get_patients_name(string dept_name, out string names)
        {
            names = null;
            using (OracleConnection myConn = new OracleConnection(connectionstring))
            {
                OracleDataAdapter dataAdapter = new OracleDataAdapter();
                OracleCommand myCmd = new OracleCommand("", myConn);
                myCmd.CommandText = "select pat_master_index.name,bed_rec.bed_label,PAT_MASTER_INDEX.Inp_no,pat_master_index.sex,floor(MONTHS_BETWEEN(sysdate,pat_master_index.date_of_birth)/12) as age, to_char(PATS_IN_HOSPITAL.ADMISSION_DATE_TIME, 'YYYY-MM-DD') as admission_date,dept_dict.dept_name,pat_visit.charge_type,pats_in_hospital.diagnosis from pat_visit,bed_rec,dept_dict,dual, PATS_IN_HOSPITAL left join pat_master_index on PATS_IN_HOSPITAL.Patient_Id = pat_master_index.patient_id where pats_in_hospital.patient_id = pat_visit.patient_id  and pats_in_hospital.visit_id = pat_visit.visit_id and PATS_IN_HOSPITAL.WARD_CODE = bed_rec.WARD_CODE and PATS_IN_HOSPITAL.bed_no = bed_rec.bed_no and dept_dict.dept_code = PATS_IN_HOSPITAL.dept_code and dept_dict.dept_name = :q order by bed_rec.bed_no";
                myCmd.Parameters.Add("q", OracleDbType.Varchar2);
                myCmd.Parameters["q"].Value = System.Text.RegularExpressions.Regex.Replace(dept_name, "护理站", ""); ;
                dataAdapter.SelectCommand = myCmd;
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                //List<string> lnames = new List<string>();
                //foreach (DataRow x in ds.Tables[0].Rows)
                //{
                //    lnames.Add(x[0].ToString() + "_" + x[1].ToString() + "_" + x[2].ToString() + "_" + x[3].ToString() + "_" + x[4].ToString() + "_" + x[5].ToString() + "_" + x[6].ToString() + "_" + x[7].ToString());
                //}
                //names = JsonConvert.SerializeObject(ds.Tables[0]); 
                //names = "[\n\"" + string.Join("\",\n\"", lnames.ToArray()) + "\"\n]";
                //names = lnames.ToArray();
                names = JsonConvert.SerializeObject(ds.Tables[0], new DataTableConverter());
            }
        }
    }
}