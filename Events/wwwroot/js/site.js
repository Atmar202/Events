var page = window.location.pathname.split("/");

switch (page[1].toLowerCase()) {
    case '':
    case 'home':
        document.getElementById("home").className = document.getElementById("home").className + " active";
    break;
    case 'events':
        document.getElementById("events").className = document.getElementById("events").className + " active";
    break;
}
