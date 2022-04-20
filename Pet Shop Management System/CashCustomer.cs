using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pet_Shop_Management_System
{
    public partial class CashCustomer : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Pet Shop Management System";
        CashForm cash;


        public CashCustomer(CashForm form)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.connection());
            cash = form;
            LoadCustom();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadCustom();
        }



        private void dgvCustom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustom.Columns[e.ColumnIndex].Name;
            if (colName=="Choice")
            {
                dbcon.executeQuery("UPDATE tbCash SET cid=" + dgvCustom.Rows[e.RowIndex].Cells[1].Value.ToString()+"WHERE transno="+cash.lblTransno.Text+"");
                cash.loadCash();
                this.Dispose();
            }

        }


        #region Method
        public void LoadCustom()
        {
            try
            {
                int i = 0;
                dgvCustom.Rows.Clear();
                cm = new SqlCommand("SELECT id,name,phone FROM tbCustomer WHERE name LIKE '%" + txtSearch.Text + "%'", cn);
                cn.Open();
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dgvCustom.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());
                }

                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, title);
            }
        }

        #endregion Method
    }
}
