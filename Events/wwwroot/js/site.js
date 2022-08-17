var page = window.location.pathname.split("/");

switch (page[1].toLowerCase()) {
    case '':
    case 'home':
        document.getElementById("home").className = document.getElementById("home").className + " active";
    break;
    case 'addevents':
        document.getElementById("events").className = document.getElementById("events").className + " active";
    break;
}

function checkRadio() {

    var radio = document.querySelector('#inlineRadio1');

    if (radio.checked) {
        document.getElementById("juriidiline").style.display = "none";
        document.getElementById("eraisik").style.display = "initial";
    } else {
        document.getElementById("eraisik").style.display = "none";
        document.getElementById("juriidiline").style.display = "initial";
    }
}