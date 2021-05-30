﻿using HotelAPP.Tools;
using System;
using System.Data;
using System.Windows.Forms;

namespace HotelAPP.AppForm.EmpForm
{
    public partial class ManageStaffForm : Form
    {
        Employee employee;
        public ManageStaffForm()
        {
            InitializeComponent();
        }

        private void ManageStaffForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void show_dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void show_dgv_DoubleClick(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            Account account = new Account();
            employee.Id = Convert.ToInt32(show_dgv.CurrentRow.Cells[0].Value);
            account.userID = Convert.ToInt32(show_dgv.CurrentRow.Cells[0].Value);

            EditDeleteEmpForm editDeleteEmp = new EditDeleteEmpForm();
            editDeleteEmp.employee = employee;
            editDeleteEmp.account = account;
            editDeleteEmp.ShowDialog();

            // refresh
            loadData();
        }

        private void refresh_btn_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            employee = new Employee();
            var list = employee.getAllEmp();

            DataTable table = new DataTable();
            table.Columns.Add().ColumnName = "Id";
            table.Columns.Add().ColumnName = "First Name";
            table.Columns.Add().ColumnName = "Last Name";
            table.Columns.Add().ColumnName = "CMND";
            table.Columns.Add().ColumnName = "Birth Date";
            table.Columns.Add().ColumnName = "Salary";
            table.Columns.Add("Avatar", typeof(byte[]));
            table.Columns.Add().ColumnName = "Position Code";
            table.Columns.Add().ColumnName = "Phone";
            table.Columns.Add().ColumnName = "Address";
            table.Columns.Add().ColumnName = "Gender";
            table.Columns.Add().ColumnName = "Manager Id";

            foreach (var emp in list)
            {
                DataRow row = table.NewRow();
                row[0] = emp.Id;
                row[1] = emp.fname;
                row[2] = emp.lname;
                row[3] = emp.CMND;
                row[4] = ((DateTime)emp.bdate).ToShortDateString();
                row[5] = emp.salary;
                row[6] = emp.avatar;
                row[7] = emp.posId;
                row[8] = emp.phone;
                row[9] = emp.address;
                row[10] = emp.gender;
                row[11] = emp.manager;
                table.Rows.Add(row);
            }
            show_dgv.DataSource = table;
            show_dgv.RowTemplate.Height = 80;
            DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
            imageColumn = (DataGridViewImageColumn)show_dgv.Columns["Avatar"];
            imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
            show_dgv.AllowUserToAddRows = false;
        }

        private void print_btn_Click(object sender, EventArgs e)
        {
            ReportTool report = new ReportTool()
            {
                Title = "Employees",
                Table = (DataTable)show_dgv.DataSource
            };

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.DefaultExt = "*.docx";
            savefile.Filter = "DOCX files(*.docx)|*.docx|Excel files(.xlsx) |*.xlsx";

            if (savefile.ShowDialog() == DialogResult.OK && savefile.FileName.Length > 0)
            {
                if (savefile.FileName.EndsWith("docx") == true)
                {
                    report.toWordReport(savefile.FileName);
                    MessageBox.Show("File saved!", "Message Dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (savefile.FileName.EndsWith("xlsx") == true)
                {
                    report.ToExcelReport(savefile.FileName);
                    MessageBox.Show("File saved!", "Message Dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
