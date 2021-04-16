using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bt02
{
    public partial class Form2 : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        public int ID_Lop { get; set; }
        public Form2()
        {
            InitializeComponent();
            SetCBB();
        }

        public void SetCBB()
        {
            cbbLSH.Text = "19TCLC-DT5";
            foreach (LSH i in CSDL_OOP.Instance.GetAllLSH())
            {
                cbbLSH.Items.Add(i.NameLop);
            }
            Form1 f1 = new Form1();
        }
        public void SetCBB(DataRow row)
        {
            cbbLSH.Text = GetNameLSH(Convert.ToInt32(row["ID_Lop"].ToString()));
            foreach (LSH i in CSDL_OOP.Instance.GetAllLSH())
            {
                cbbLSH.Items.Add(i.NameLop);
            }
            txtMSSV.Text = row["MSSV"].ToString();
            txtName.Text = row["NameSV"].ToString();

        }
        public string GetNameLSH(int id)
        {
            string s = null;
            foreach (DataRow item in CSDL.Instance.DTLSH.Rows)
            {
                if (item["ID_Lop"].ToString().Equals(id.ToString()))
                {
                    s = item["NameLop"].ToString();
                }
            }
            return s;
        }
        public int GetIdLSH(string name)
        {
            int s = 1;
            foreach (DataRow item in CSDL.Instance.DTLSH.Rows)
            {
                if (item["NameLop"].Equals(name))
                {
                    s = Convert.ToInt32(item["ID_Lop"]);
                }
            }
            return s;
        }
        public DataRow GetData()
        {
            String MSSV = txtMSSV.Text;
            String NameSV = txtName.Text;
            int ID_Lop = GetIdLSH(cbbLSH.Text);
            bool Gender = false;
            if (male.Checked == true) Gender = true;
            else Gender = false;
            DateTime iDate;
            iDate = dateTimePicker1.Value;
            DataRow dr1 = CSDL.Instance.DTSV.NewRow();
            dr1["MSSV"] = MSSV;
            dr1["NameSV"] = NameSV;
            dr1["Gender"] = Gender;
            dr1["ID_Lop"] = ID_Lop;
            dr1["NS"] = iDate;
            return dr1;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (SV i in CSDL_OOP.Instance.GetAllSV())
            {
                if (GetData()["MSSV"].Equals(i.MSSV))
                {
                    check = true;
                    SV dr1 = new SV();
                    dr1.MSSV = GetData()["MSSV"].ToString();
                    dr1.NameSV = GetData()["NameSV"].ToString();
                    dr1.Gender = Convert.ToBoolean(GetData()["Gender"].ToString());
                    dr1.ID_Lop = Convert.ToInt32(GetData()["ID_Lop"].ToString());
                    dr1.NS = Convert.ToDateTime(GetData()["NS"].ToString());
                    CSDL.Instance.EditDataSV(dr1);
                    break;
                }
            }
            if (!check)
            {
                CSDL.Instance.AddDataRowSV(GetData());
            }
            d();
            this.Dispose();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
