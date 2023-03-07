using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class MapObjects
    {

        public string GetGeoJsonLic()
        {
            String query = @"SELECT json_build_object(
                                'type', 'FeatureCollection',
                                'features', json_agg(ST_AsGeoJSON(t.*)::json)
                                )
                            FROM (
                            SELECT ""name"", numlc, regdate, enddate, color, geom, nameholder FROM gis.v_lic_info
                            ) as t(""name"", numlc, regdate, enddate, color, geom, nameholder);";

            return new AppData().GetScalar(query) as String;
        }

        public string GetGeoJsonWells(string lng, string lat, Int32 radius)
        {
            string query = $@"SELECT json_build_object(
                                'type', 'FeatureCollection',
                                'features', json_agg(ST_AsGeoJSON(t.*)::json)
                                )
                            FROM (
                            with buf as (select st_buffer((ST_GeomFromText('POINT ({lng} {lat})'))::geography, {radius}::double precision)::geometry as geom)
                            SELECT geom, num, ""type"", debit
                            FROM gis.wells where ST_Contains((select geom from buf), wells.geom)
                            ) as t(geom, num, ""type"", debit)";

            return new AppData().GetScalar(query) as String;
        }

        public string GetGeoJsonLic_251()
        {
            string query = $@"select row_to_json(fc)
                            from (select
		                        'FeatureCollection' as ""type"",
                                array_to_json(array_agg(f)) as ""features""
                                from(
                                    select
                                    'Feature' as ""type"",
                                    ST_AsGeoJSON(geom)::json as ""geometry"", (
                                        select json_strip_nulls(row_to_json(t))
                                        from (select name, numlc, regdate, enddate, nameholder, color) t
				                        ) as ""properties""
                                    from gis.v_lic_info
	                            ) as f
                            ) as fc; ";
            string result = new AppData().GetScalar(query) as String;
            return result;
        }

        public string GetGeoJsonWells_251(string lng, string lat, string radius)
        {
            string query = $@"select row_to_json(fc)
                            from(
                                   select 'FeatureCollection' as ""type"",
                                array_to_json(array_agg(f)) as ""features""
                                from(
                                    select 'Feature' as ""type"",
                                    st_asgeojson(geom)::json as ""geometry"",
                                    (select json_strip_nulls(row_to_json(t)) from(select num, ""type"", debit) t) as ""properties""
                                    from gis.wells where ST_Contains(ST_Buffer(ST_GeomFromText('POINT({lng} {lat})')::geography, {radius})::geometry, geom)
                                   ) as f
                            ) as fc;";
            string result = new AppData().GetScalar(query) as String;
            return result;
        }
    }
}