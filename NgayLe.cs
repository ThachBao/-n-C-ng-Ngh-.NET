using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMresourcemanagementsystem
{
    internal class NgayLe
    {
        public int Ngay { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        public string TenLe { get; set; }

        public NgayLe(int ngay, int thang, int nam, string tenLe)
        {
            Ngay = ngay;
            Thang = thang;
            Nam = nam;
            TenLe = tenLe;
        }
    }
}
