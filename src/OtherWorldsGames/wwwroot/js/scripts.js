
$(document).ready(function () {
    var map = L.map('map').setView([45.47, -122.69], 13);

    var tiles = L.tileLayer('http://stamen-tiles-{s}.a.ssl.fastly.net/toner/{z}/{x}/{y}.{ext}', {
    attribution: 'Map tiles by <a href="http://stamen.com">Stamen Design</a>, <a href="http://creativecommons.org/licenses/by/3.0">CC BY 3.0</a> &mdash; Map data &copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
    ext: 'png'
    }).addTo(map);

    marker = L.marker([45.477893, -122.695749], 14).addTo(map)
            .bindPopup("Based out of Hillsdale, in Portland, OR")
            .openPopup();
});