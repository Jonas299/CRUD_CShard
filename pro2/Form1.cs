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
using pro2.app;

namespace pro2
{
    public partial class Form1 : Form 
    {
        Employees employeesClass;
        public Form1()
        {
            InitializeComponent();
            btnEdit.Enabled = false;
            this.employeesClass = new Employees();
        }

        private void readEmp()
        {
            employeesGrid.DataSource = this.employeesClass.readEmp();
        }

        private void insertEmp()
        {
            var conString = ConfigurationManager.ConnectionStrings["dbEmployees"].ConnectionString;
            string name = nameTxt.Text.ToUpper().Trim();
            string lastname = lastnameTxt.Text.ToUpper().Trim();
            string birthday = birthdayTxt.Value.Date.ToString("yyyy/MM/dd");
            string gender = genderTxt.Text;
            string email = emailTxt.Text;

            if (this.employeesClass.insertEmp(name, lastname, birthday, gender, email) > 0)
            {
                MessageBox.Show("Empleado registrado");
                this.readEmp();
                this.cleanFields();
            }
        }

        private void updateEmp()
        {
            string id = idTxt.Text;
            string name = nameTxt.Text.ToUpper().Trim();
            string lastname = lastnameTxt.Text.ToUpper().Trim();
            string birthday = birthdayTxt.Value.Date.ToString("yyyy/MM/dd");
            string gender = genderTxt.Text;
            string email = emailTxt.Text;

            if (this.employeesClass.updateEmp(id, name, lastname, birthday, gender, email) > 0)
            {
                MessageBox.Show("Empleado actualizado");
                this.readEmp();
                this.cleanFields();
                btnEdit.Enabled = false;
                btnOk.Enabled = true;
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error al momento de actualizar el registro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteEmp()
        {
            string id = employeesGrid.CurrentRow.Cells[0].Value.ToString();

            if (this.employeesClass.deleteEmp(id) > 0)
            {
                MessageBox.Show("Empleado eliminado");
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

        private void selectEdit()
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
            }
            else {
                MessageBox.Show("Haz doble click sobre un registro para editarlo");
            }  
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.cleanFields();
            btnOk.Enabled = true;
            btnEdit.Enabled = false;
        }

        private void employeesGrid_DoubleClick(object sender, EventArgs e)
        {
            this.selectEdit();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.deleteEmp();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectEdit();
        }
    }
}
