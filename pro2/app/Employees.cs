using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pro2.app
{
    internal class Employees : conection
    {
        public DataTable readEmp()
        {
            try
            {
                var conString = this.getConection();
                DataTable dt = new DataTable();

                using (OleDbConnection Conector = new OleDbConnection(conString))
                {
                    Conector.Open();
                    string sql = @"select * from employees";
                    OleDbCommand cmd = new OleDbCommand(sql, Conector);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                }

                return dt;

            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public int insertEmp(string name, string lastname, string birthday, string gender, string email)
        {
            try
            {
                var conString = this.getConection();
                using (OleDbConnection Conector = new OleDbConnection(conString))
                {
                    Conector.Open();
                    string sql = @"insert into employees(nameE,lastname, birthday, gender, email) VALUES (@nameE,@lastname, @bithday, @gender, @email)";
                    OleDbCommand cmd = new OleDbCommand(sql, Conector);
                    cmd.Parameters.AddWithValue("@nameE", name);
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@birthday", birthday);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@email", email);

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public int updateEmp(string id, string name, string lastname, string birthday, string gender, string email)
        {
            try
            {
                var conString = this.getConection();
                using (OleDbConnection Conector = new OleDbConnection(conString))
                {
                    Conector.Open();
                    string sql = @"UPDATE employees set nameE=@nameE,lastname=@lastname, birthday=@bithday, gender=@gender, email=@email WHERE Id = @Id";
                    OleDbCommand cmd = new OleDbCommand(sql, Conector);
                    cmd.Parameters.AddWithValue("@nameE", name);
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@birthday", birthday);
                    cmd.Parameters.AddWithValue("@gender", gender);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int deleteEmp(string id)
        {
            try
            {
                var conString = this.getConection();
                using (OleDbConnection Conector = new OleDbConnection(conString))
                {
                    Conector.Open();
                    string sql = @"DELETE FROM employees WHERE id = @Id";
                    OleDbCommand cmd = new OleDbCommand(sql, Conector);
                    cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));

                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
