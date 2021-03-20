using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Customer
    {
        public string name { get; set; }
        public string phone { get; set; }
        public List<int> Ghe { get; set; }
        public Customer()
        {
            Ghe = new List<int>();
        }
        public int TinhTien
        {
            get
            {
               int tt = Ghe.Count * 100000;
                return tt;
            }   
                      
        }
        public override string ToString()
        {
            return name + "-" + phone;
        }

    }
}
