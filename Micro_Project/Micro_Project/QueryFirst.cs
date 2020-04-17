using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Micro_Project
{
    public class QueryFirst
    {
        public string name { get; set; }

        public string surname { get; set; }

        public string lastname { get; set; }

        public string nomer { get; set; }

        public string caterogy { get; set; }

        public string startLive { get; set; }

        public string endLive { get; set; }

        public string days { get; set; }

        public string summa { get; set; }

        public string desc { get; set; }

        public QueryFirst(string Name, string Surname, string Lastname, string Nomer, string Caterogy, string StartLive, string EndLive, string Days, string Summa, string Desc) 
        {
            this.name = Name;
            this.surname = Surname;
            this.lastname = Lastname;
            this.nomer = Nomer;
            this.caterogy = Caterogy;
            this.startLive = StartLive;
            this.endLive = EndLive;
            this.days = Days;
            this.summa = Summa;
            this.desc = Desc;
        }

    }
}
