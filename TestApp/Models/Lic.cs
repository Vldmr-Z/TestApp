using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Lic
    {
        public Int32 Id { get; set; }
        [DisplayName("Наименование ЛУ")]
        public String Name { get; set; }
        [DisplayName("Номер лицензии")]
        public String NumLc { get; set; }
        [DisplayName("Дата регистрации лицензии")]
        public DateTime RegDate { get; set; }
        [DisplayName("Дата окончания лицензии")]
        public DateTime EndDate { get; set; }
        [DisplayName("Наименование организации")]
        public String NameHolder { get; set; }

        public Int32 EndMonth {
            get {
                //return (DateTime.Now.Date - EndDate.Date)
                return ((EndDate.Date.Year - DateTime.Now.Date.Year) * 12) + (EndDate.Date.Month - DateTime.Now.Date.Month);
            }
        }

        public List<Lic> Get()
        {
            List<Lic> result = new List<Lic>();
            string query = "SELECT id, name, numlc, regdate, enddate, nameholder FROM gis.v_lic_info;";
            DataTable dataTable = new AppData().GetDataTable(query);

            result = dataTable.AsEnumerable().Select(x => new Lic()
            {
                Id = Convert.ToInt32(x["id"]),
                Name = Convert.ToString(x["name"]),
                NumLc = Convert.ToString(x["numlc"]),
                RegDate = Convert.ToDateTime(x["regdate"]).Date,
                EndDate = Convert.ToDateTime(x["enddate"]).Date,
                NameHolder = Convert.ToString(x["nameholder"])
            }).OrderBy(f => f.NameHolder).OrderBy(f => f.Name).ToList();
            return result;
        }
    }
}