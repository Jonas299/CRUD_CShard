using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;

namespace pro2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnEdit.Enabled = false;
        }

        private void readEmp()
        {
            var conString = ConfigurationManager.ConnectionStrings["dbEmployees"].ConnectionString;

            using (OleDbConnection Conector = new OleDbConnection(conString))
            {
                Conector.Open();
                DataTable dt = new DataTable();
                string sql = @"select * from employees";
                OleDbCommand cmd = new OleDbCommand(sql, Conector);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.SelectCommand = cmd;
                da.Fill(dt);
                employeesGrid.DataSource = dt;
            }
        }

        private void insertEmp()
        {
            var conString = ConfigurationManager.ConnectionStrings["dbEmployees"].ConnectionString;

            using (OleDbConnection Conector = new OleDbConnection(conString))
            {
                Conector.Open();
                DataTable dt = new DataTable();
                string sql = @"insert into employees(nameE,lastname, birthday, gender, email) VALUES (@nameE,@lastname, @bithday, @gender, @email)";
                OleDbCommand cmd = new OleDbCommand(sql, Conector);
                cmd.Parameters.AddWithValue("@nameE", nameTxt.Text.ToUpper().Trim());
                cmd.Parameters.AddWithValue("@lastname", lastnameTxt.Text.ToUpper().Trim());
                cmd.Parameters.AddWithValue("@birthday", birthdayTxt.Value.Date.ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@gender", genderTxt.Text);
                cmd.Parameters.AddWithValue("@email", emailTxt.Text);

                if (cmd.ExecuteNonQuery() > 0) 
                {
                    MessageBox.Show("Empleado registrado");
                }

                this.readEmp();
                this.cleanFields();
            }
        }

        private void updateEmp()
        {
            var conString = ConfigurationManager.ConnectionStrings["dbEmployees"].ConnectionString;

            using (OleDbConnection Conector = new OleDbConnection(conString))
            {
                Conector.Open();
                DataTable dt = new DataTable();
                string sql = @"UPDATE employees set nameE=@nameE,lastname=@lastname, birthday=@bithday, gender=@gender, email=@email WHERE Id = @Id";
                OleDbCommand cmd = new OleDbCommand(sql, Conector);
                cmd.Parameters.AddWithValue("@nameE", nameTxt.Text.ToUpper().Trim());
                cmd.Parameters.AddWithValue("@lastname", lastnameTxt.Text.ToUpper().Trim());
                cmd.Parameters.AddWithValue("@birthday", birthdayTxt.Value.Date.ToString("yyyy/MM/dd"));
                cmd.Parameters.AddWithValue("@gender", genderTxt.Text);
                cmd.Parameters.AddWithValue("@email", emailTxt.Text);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(idTxt.Text));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Empleado actualizado");
                }

                this.readEmp();
                this.cleanFields();
            }
        }

        private void deleteEmp()
        {
            var conString = ConfigurationManager.ConnectionStrings["dbEmployees"].ConnectionString;
            string id = employeesGrid.CurrentRow.Cells[0].Value.ToString();
            using (OleDbConnection Conector = new OleDbConnection(conString))
            {
                Conector.Open();
                DataTable dt = new DataTable();
                string sql = @"DELETE FROM employees WHERE id = @Id";
                OleDbCommand cmd = new OleDbCommand(sql, Conector);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(id));

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Empleado eliminado");
                }

                this.readEmp();
            }
        }

        private void cleanFields() 
        { 
            idTxt.Text = string.Empty;
            nameTxt.Text = string.Empty;
            lastnameTxt.Text = string.Empty;
            birthdayTxt.ResetText();
            genderTxt.ResetText();
            emailTxt.Text = string.Empty;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            readEmp();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            insertEmp();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (idTxt.Text != string.Empty)
            {
                this.updateEmp();
                btnEdit.Enabled = false;
                btnOk.Enabled = true;
            }
            else {
                MessageBox.Show("Haz doble click sobre un registro para editarlo");
            }  
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cleanFields();
            btnOk.Enabled = true;
        }

        private void employeesGrid_DoubleClick(object sender, EventArgs e)
        {
            idTxt.Text = employeesGrid.CurrentRow.Cells[0].Value.ToString();
            nameTxt.Text = employeesGrid.CurrentRow.Cells[1].Value.ToString();
            lastnameTxt.Text = employeesGrid.CurrentRow.Cells[2].Value.ToString();
            birthdayTxt.Text = employeesGrid.CurrentRow.Cells[3].Value.ToString();
            genderTxt.Text = employeesGrid.CurrentRow.Cells[4].Value.ToString();
            emailTxt.Text = employeesGrid.CurrentRow.Cells[5].Value.ToString();
            btnOk.Enabled = false;
            btnEdit.Enabled = true;
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.deleteEmp();
        }
    }
}
