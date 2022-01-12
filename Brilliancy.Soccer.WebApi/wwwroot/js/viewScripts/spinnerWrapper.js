import { Spinner } from "/lib/spin.js/spin.js"
window.onload = function () {
    var target = document.getElementById('custom-spinner');
    new Spinner({ color: '#000', lines: 12, length: 18, radius: 12, scale: 3 }).spin(target);
};