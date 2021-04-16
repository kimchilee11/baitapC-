using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bt02
{
    class CSDL
    {
        public DataTable DTLSH { get; set; }
        public DataTable DTSV { get; set; }
        public static CSDL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CSDL();
                }
                return _Instance;
            }
            private set { }
        }
        private static CSDL _Instance;
        private CSDL()
        {
            DTSV = new DataTable();
            DTSV.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("MSSV", typeof(string)),
                new DataColumn("NameSV", typeof(string)),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("NS", typeof(DateTime)),
                new DataColumn("ID_Lop", typeof(int)),
            });
            DataRow dr = DTSV.NewRow();
            dr["MSSV"] = "101";
            dr["NameSV"] = "Nguyen Van A";
            dr["Gender"] = true;
            dr["ID_Lop"] = 1;
            dr["NS"] = DateTime.Now;
            DTSV.Rows.Add(dr);
            DataRow dr1 = DTSV.NewRow();
            dr1["MSSV"] = "102";
            dr1["NameSV"] = "Nguyen Thi X";
            dr1["Gender"] = true;
            dr1["ID_Lop"] = 2;
            dr1["NS"] = DateTime.Now;
            DTSV.Rows.Add(dr1);
            DataRow dr2 = DTSV.NewRow();
            dr2["MSSV"] = "103";
            dr2["NameSV"] = "Tran Van B";
            dr2["Gender"] = true;
            dr2["ID_Lop"] = 1;
            dr2["NS"] = DateTime.Now;
            DTSV.Rows.Add(dr2);

            DTLSH = new DataTable("LopSH");
            DTLSH.Columns.Add("ID_Lop", typeof(int));
            DTLSH.Columns.Add("NameLop", typeof(string));
            DTLSH.Rows.Add(1, "19TCLC-DT5");
            DTLSH.Rows.Add(2, "19TCLC-DT6");
        }
        public void AddDataRowSV(DataRow sv)
        {
            DTSV.Rows.Add(sv);
        }
        public void EditDataSV(SV s)
        {
            for (int i = 0; i < DTSV.Rows.Count; ++i)
            {
                if (DTSV.Rows[i]["MSSV"].ToString() == s.MSSV)
                {
                    DTSV.Rows.RemoveAt(i);
                    DataRow d = DTSV.NewRow();
                    d["MSSV"] = s.MSSV;
                    d["NameSV"] = s.NameSV;
                    d["NS"] = s.NS.ToString();
                    d["Gender"] = s.Gender.ToString();
                    d["ID_Lop"] = s.ID_Lop.ToString();
                    DTSV.Rows.InsertAt(d, i);
                    return;
                }
            }
        }
    }
}
