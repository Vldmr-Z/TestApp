var geosrv = L.tileLayer.wms("http://10.28.71.242/map_geoserver_lnk/", { layers: "test-gis:test-gis" });
var osmlayer = L.tileLayer("http://{s}.tile.osm.org/{z}/{x}/{y}.png");
var dblgis = L.tileLayer("http://tile{s}.maps.2gis.com/tiles?x={x}&y={y}&z={z}&v=1", $.extend({ subdomains: '0123456789' }));

var map = L.map('mapid');
map.setView([59.01794, 72.015381], 7);
map.invalidateSize();

map.on('click', layerClick);

var baseLayer = osmlayer.addTo(map);

var wells;
var myCircle;
function layerClick(e) {
    if (wells != undefined)
        wells.remove();

    if (myCircle != undefined)
        myCircle.remove();

    var cirleRadius = prompt('Укажите радиус, м.', 1000);

    GetWells(e.latlng.lng, e.latlng.lat, cirleRadius);
    myCircle = L.circle(e.latlng, { radius: cirleRadius }).addTo(map);
    map.fitBounds(myCircle.getBounds());
}

function GetLic() {
    $.ajax({
        url: window.location.origin + "/Analytics/GetLic",
        type: "GET",
        cache: false,
        async: true,
        traditional: true,
        success: function (response) {
            DrawLic(response)
        }
    });
}

function DrawLic(geo) {
    var lic = L.geoJSON(JSON.parse(geo), {
        style: function (feature) {
            return { color: feature.properties.color, weight: 2 };
        },
        onEachFeature: onEachFeatureLic
    });

    lic.addTo(map);
}

function onEachFeatureLic(feature, layer) {
    if (feature.properties) {
        layer.bindTooltip('ЛУ <b>"' + feature.properties.name + '"</b><br />' + feature.properties.nameholder);
    }
}

function GetWells(lng, lat, radius) {
    $.ajax({
        url: window.location.origin + "/Analytics/GetWells",
        data: { lng: lng, lat: lat, radius: radius },
        type: "GET",
        cache: false,
        async: true,
        traditional: true,
        success: function (response) {
            DrawWells(response);
        }
    });
}

function DrawWells(geo) {
    try {
        wells = L.geoJSON(JSON.parse(geo), {
            onEachFeature: onEachFeatureWell,
            pointToLayer: function (feature, latlng) {
                return L.circleMarker(latlng, {
                    color: '#404040',
                    opacity: 1,
                    weight: 2,
                    fillColor: '#E0E0E0',
                    fillOpacity: 1,
                    radius: 5
                });
            }
            });

        wells.addTo(map);
    }
    catch(ex)
    {
        alert('Нет данных');
    }
}

function onEachFeatureWell(feature, layer) {
    if (feature.properties) {
        layer.bindTooltip("<b>Скважина №: " + feature.properties.num + "</b><br />Тип: " + feature.properties.type + "<br />Дебит: " + feature.properties.debit + "м3/сутки");
    }
}
