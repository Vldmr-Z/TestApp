﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class Well
    {
        public Int32 Id { get; set; }
        [DisplayName("Номер скважины")]
        public String Num { get; set; }
        [DisplayName("Тип")]
        public string Type { get; set; }
        [DisplayName("Дебит, м3/сутки")]
        public Single Debit { get; set; }

        public Int32 LicId { get; set; } //Идентификатор ЛУ

        public List<Well> Get(Int32? licId = null)
        {
            if (!licId.HasValue)
                return new List<Well>();

            string query = $"SELECT id, num, type, debit, lic_id FROM gis.v_wells{ (licId.HasValue ? " where lic_id = " + licId : string.Empty) };";

            return DtToList(query);
        }

        private List<Well> DtToList(string query)
        {
            List<Well> result = new List<Well>();

            DataTable dataTable = new AppData().GetDataTable(query);
            result = dataTable.AsEnumerable().Select(x => new Well()
            {
                Id = Convert.ToInt32(x["id"]),
                Num = Convert.ToString(x["num"]),
                Type = Convert.ToString(x["type"]),
                Debit = Convert.ToSingle(x["debit"]),
                LicId = Convert.ToInt32(x["lic_id"])
            }).ToList();

            return result;
        }
    }

    
}