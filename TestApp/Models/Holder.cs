using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Holder
    {
        //Int32 Id { get; set; }
        [DisplayName("Наименование компании")]
        public String Name { get; set; }
        [DisplayName("Кол-во скважин")]
        public Int32 Wells { get; set; }
        [DisplayName("Общий дебит, м3/сутки")]
        public Single Debit { get; set; }
        [DisplayName("Общая площадь, м2")]
        public Single SQM { get; set; }
        [DisplayName("Кол-во ЛУ")]
        public Int32 Cnt { get; set; }

        public List<Holder> Get()
        {
            List<Holder> result = new List<Holder>();
            string query = "SELECT wells, debit, sqm, cnt, nameholder FROM gis.v_holder_info;";
            DataTable dataTable = new AppData().GetDataTable(query);

            result =  dataTable.AsEnumerable().Select(x => new Holder()
            {
                Name = Convert.ToString(x["nameholder"]),
                Wells = Convert.ToInt32(x["wells"]),
                Debit = Convert.ToSingle(x["debit"]),
                SQM = Convert.ToSingle(x["sqm"]),
                Cnt = Convert.ToInt32(x["cnt"])
            }).OrderBy(f => f.Name).ToList();

            return result;
        }
    }
}