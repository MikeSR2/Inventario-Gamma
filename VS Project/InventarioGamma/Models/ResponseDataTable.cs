using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InventarioGamma.Models
{
    public class ResponseDataTable
    {
        public String sEcho { get; set; }

        public String recordsTotal { get; set; }

        public String recordsFiltered { get; set; }

        public String iTotalRecords { get; set; }

        public String iTotalDisplayRecords { get; set; }

        public List<Historial> aaData {get;set;}
    }
}