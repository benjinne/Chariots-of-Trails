<template>
    <div>
        <h1>Welcome to Chariots of Trails!</h1>
        <form action="https://www.strava.com/routes/new/" target="_blank">
            <input type="submit" value="Create a Route" class="button" />
        </form>

        <link rel="stylesheet" href="https://unpkg.com/leaflet@1.3.4/dist/leaflet.css"
            integrity="sha512-puBpdR0798OZvTTbP4A8Ix/l+A4dHDD0DGqYW6RQ+9jxkRFclaxxQb/SJAWZfWAkuyeQUytO7+7N4QKrDh+drA=="
            crossorigin=""/>
        <script2 src="https://unpkg.com/leaflet@1.3.4/dist/leaflet.js"/>
        <script2 src="https://cdn.jsdelivr.net/gh/jieter/Leaflet.encoded/Polyline.encoded.js"/>


        <div id="map" style="width: 500px; height: 500px"></div>

        

        

    </div>
</template>



<script >


export default {

mounted: function () {
  window.addEventListener('load', () => {
    // Code that will run only after the
    // entire view has been rendered
    var map = L.map('map', {
            scrollWheelZoom: false
        });
        
        L.tileLayer(
            'http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                maxZoom: 18,
            }).addTo(map);

        var encoded = "w|xrF`kjsMq@gE{p@b\\zLj^{AdCxDfQaY|LxI`c@lE`qAvBtUbHxZgJrp@~Stj@vGbXsAb\\zIzoA|CdaBiH`BuRvNeLiA}WpOqJq[|G_T{Ewd@{Acc@cEgTd@sRwBsUnBgV_DpA~CqAG_BoLgm@e@ed@uE{RfS_YjEcNcGmLaAeHyD}{AjY}mAuYeuAr^iD|P`D~JxFfZp`@lg@~iA|A~BjCiCA_DsD{C~AeICuUcBeLaXyt@gK`FrBrLgBxBn@S"
        var coordinates = L.Polyline.fromEncoded(encoded).getLatLngs();

        L.polyline(coordinates, {
            color: 'blue',
            weight: 2,
            opacity: .7,
            lineJoin: 'round'
        }).addTo(map);
        
        map.fitBounds(coordinates);
  })
},

  methods: {


      click(){

        
      }


  }
}
</script>

<style>
</style>
