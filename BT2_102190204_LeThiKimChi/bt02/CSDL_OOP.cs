using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bt02
{
    class CSDL_OOP
    {
        private static CSDL_OOP _Instance;
        public static CSDL_OOP Instance
        {
            get
            {
                if (_Instance == null) _Instance = new CSDL_OOP();
                return _Instance;
            }
            private set { }
        }
        private CSDL_OOP()
        {
        }

        public List<SV> GetAllSV()
        {
            List<SV> l = new List<SV>();
            foreach (DataRow i in CSDL.Instance.DTSV.Rows)
            {
                l.Add(GetSV(i));
            }
            return l;
        }
        public SV GetSV(DataRow i)
        {
            return new SV
            {
                MSSV = i["MSSV"].ToString(),
                NameSV = i["NameSV"].ToString(),
                Gender = Convert.ToBoolean(i["Gender"].ToString()),
                NS = Convert.ToDateTime(i["NS"].ToString()),
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString())
            };
        }
        public List<LSH> GetAllLSH()
        {
            List<LSH> data = new List<LSH>();
            foreach (DataRow i in CSDL.Instance.DTLSH.Rows)
            {
                data.Add(GetLSH(i));
            }
            return data;
        }
        public LSH GetLSH(DataRow i)
        {
            return new LSH
            {
                ID_Lop = Convert.ToInt32(i["ID_Lop"].ToString()),
                NameLop = i["NameLop"].ToString(),
            };
        }
        public List<SV> GetListSV(int ID_Lop)
        {
            List<SV> data = new List<SV>();
            foreach (SV i in GetAllSV())
            {
                if (i.ID_Lop == ID_Lop)
                {
                    data.Add(new SV
                    {
                        NameSV = i.NameSV,
                        MSSV = i.MSSV,
                        Gender = i.Gender,
                        NS = i.NS,
                        ID_Lop = i.ID_Lop,
                    });
                }
            }
            return data;
        }
        public string GetID_LSH(string nameLSH)
        {
            string x = nameLSH;
            foreach (LSH item in CSDL.Instance.DTLSH.Rows)
            {
                x += item.ID_Lop + " ";
            }
            return x;
        }
        public void DelSV(string mssv)
        {
            foreach (DataRow row in CSDL.Instance.DTSV.Rows)
            {
                if (row["MSSV"].Equals(mssv))
                {
                    CSDL.Instance.DTSV.Rows.Remove(row);
                    return;
                }
            }
        }
        public List<SV> SortMS(List<SV> lsv)
        {
            for (int i = 0; i < lsv.Count - 1; ++i)
            {
                for (int j = i + 1; j < lsv.Count; ++j)
                {
                    if (comparerL(Convert.ToInt32(lsv[i].MSSV), Convert.ToInt32(lsv[j].MSSV)))
                    {
                        SV temp = lsv[i];
                        lsv[i] = lsv[j];
                        lsv[j] = temp;
                    }
                }
            }
            return lsv;

        }
        public List<SV> SortLop(List<SV> lsv, string str)
        {
            for (int i = 0; i < lsv.Count - 1; ++i)
            {
                for (int j = i + 1; j < lsv.Count; ++j)
                {
                    if("LopSH".Equals(str))
                    {
                        if (comparerL(Convert.ToInt32(lsv[i].ID_Lop), Convert.ToInt32(lsv[j].ID_Lop)))
                        {
                            SV temp = lsv[i];
                            lsv[i] = lsv[j];
                            lsv[j] = temp;
                        }
                    }
                    else if("Name".Equals(str))
                    {
                        if (comparer(lsv[i].NameSV, lsv[j].NameSV))
                        {
                            SV temp = lsv[i];
                            lsv[i] = lsv[j];
                            lsv[j] = temp;
                        }
                    }
                }
            }
            return lsv;
        }
        public bool comparerL(int ms1, int ms2)
        {
            if (ms1 < ms2) return false;
            else return true;
        }
        public bool comparer(string ms1, string ms2)
        {
            if (ms1.CompareTo(ms2)<0) return false;
            return true;
        }
    }
}
