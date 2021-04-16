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
    public partial class Form1 : Form
    {
        public delegate void MyDel();
        public MyDel d { get; set; }
        private BindingSource _source = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            SetCBB();
            ShowData();
        }
        public void SetCBB()
        {
            cbbLSH.Text = "Tat Ca";
            cbbLSH.Items.Add("Tat Ca");
            cbbSort.Text = "MSSV";
            cbbSort.Items.Add("MSSV");
            cbbSort.Items.Add("Name");
            cbbSort.Items.Add("LopSH");
        }
        public void ShowData()
        {
            dataGridView1.DataSource = CSDL_OOP.Instance.GetAllSV();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (cbbLSH.Text.Equals("Tat Ca"))
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.GetAllSV();
            }
            else
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.GetListSV(GetIdLSH(cbbLSH.Text));
            }
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            f2.d += new Form2.MyDel(ShowData);
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                Form2 f2 = new Form2();
                f2.Show();
                f2.SetCBB(rowSelected());
                f2.d += new Form2.MyDel(ShowData);
            }
        }
        public DataRow rowSelected()
        {
            DataRow dr1 = CSDL.Instance.DTSV.NewRow();
            foreach (DataGridViewRow i in dataGridView1.SelectedRows)
            {
                dr1["MSSV"] = i.Cells["MSSV"].Value.ToString();
                dr1["NameSV"] = i.Cells["NameSV"].Value.ToString();
                dr1["Gender"] = Convert.ToBoolean(i.Cells["Gender"].Value.ToString());
                dr1["ID_Lop"] = Convert.ToInt32(i.Cells["ID_Lop"].Value.ToString());
            }
            return dr1;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridView1.SelectedRows)
            {
                CSDL_OOP.Instance.DelSV(i.Cells["MSSV"].Value.ToString());
            }
            ShowData();
        }
        public void Show(int ID_Lop)
        {
            dataGridView1.DataSource = CSDL_OOP.Instance.GetListSV(ID_Lop);
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            if (cbbSort.SelectedItem != null)
            {
                List<SV> res = CSDL_OOP.Instance.GetAllSV();
                if ("MSSV".Equals(cbbSort.SelectedItem.ToString()))
                {
                    dataGridView1.DataSource = CSDL_OOP.Instance.SortMS(res);
                }
                else if ("LopSH".Equals(cbbSort.SelectedItem.ToString()))
                {
                    dataGridView1.DataSource = CSDL_OOP.Instance.SortLop(res,"LopSH");
                }
                else if ("Name".Equals(cbbSort.SelectedItem.ToString()))
                {
                    dataGridView1.DataSource = CSDL_OOP.Instance.SortLop(res,"Name");
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SV>l = new List<SV>();
            foreach (SV item in CSDL_OOP.Instance.GetAllSV())
            {
                if (item.NameSV.ToString().Contains(txtSearch.Text.ToString()))
                {
                    l.Add(item);
                    continue;
                }
            }
            dataGridView1.DataSource = l;
        }
    }
}