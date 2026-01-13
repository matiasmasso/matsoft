//----------------- data

//var apiRoot = 'https://matiasmasso-api.azurewebsites.net/api/';
var apiRoot = 'https://api.matiasmasso.es/api/';


//----------------- UI

function selectOption(dropdown, option) {
    const value = $(dropdown).find('option:nth-child(' + option + ')').val();
    $(dropdown).val(value);
}


//----------------- Arrays

function sortedGuidNoms(items) {
    var retval = items.sort(function (a, b) {
        var nameA = a.Nom.toUpperCase();
        var nameB = b.Nom.toUpperCase();
        if (nameA < nameB) {
            return -1;
        } else if (nameA > nameB) {
            return 1;
        } else {
            return 0;
        }
    });
    return retval;
}

//----------------- Numbers

function padLeadingZeros(num, size) {
    var s = num + "";
    while (s.length < size) s = "0" + s;
    return s;
}

function formattedEur(src) {
    var formatter = new Intl.NumberFormat('de-DE', {
        //var formatter = new Intl.NumberFormat('es-ES', {
        style: 'currency',
        currency: 'EUR',
    });
    var retval = formatter.format(src);
    return retval;
}

function formattedInt(src) {
    var retval = src.toLocaleString(
        'de-DE', // leave undefined to use the visitor's browser
        // locale or a string like 'en-US' to override it.
        { minimumFractionDigits: 0 }
    );
    return retval;
}




//----------------- Time

function fromMicrosoftDate(src) {
    return new Date(parseInt(src.substr(6)));
}

function formattedFch(fch) {
    var retval = '';
    if (!isEmptyFch(fch))
        retval = padLeadingZeros(getDay(fch), 2) + '/' + padLeadingZeros(getMonth(fch), 2) + '/' + getYear(fch);
    return retval;
}
function formattedFchCompact(fch) {
    var retval = '';
    if (!isEmptyFch(fch))
        retval = padLeadingZeros(getDay(fch), 2) + '/' + padLeadingZeros(getMonth(fch), 2) + '/' + getYear(fch).toString().substr(2,2);
    return retval;
}

function formattedTime(fch) {
    var retval = '';
    if (!isEmptyFch(fch)) {
        var date = new Date(fch);
        retval = padLeadingZeros(date.getHours(), 2) + ':' + padLeadingZeros(date.getMinutes(), 2);
    }
    return retval;
}

function getYear(fch) {
    var date = new Date(fch);
    var retval = date.getFullYear();
    return retval;
}

function getMonth(fch) {
    var date = new Date(fch);
    var retval = date.getMonth()+1;
    return retval;
}

function getDay(fch) {
    var date = new Date(fch);
    var retval = date.getDate();
    return retval;
}

function getWeekday(fch) {
    var date = new Date(fch);
    var retval = date.getDay();
    return retval;
}


function isEmptyFch(fch) {
    return (fch == '0001-01-01T00:00:00')
}

function isToday(fch) {
    var today = new Date().toDateString();
    var date = new Date(fch).toDateString();
    var retval = (date === today);
    return retval;
}

function NowToString() {
    var m = new Date();
    var retval =
        m.getUTCFullYear() + "-" +
        ("0" + (m.getUTCMonth() + 1)).slice(-2) + "-" +
        ("0" + m.getUTCDate()).slice(-2) + " " +
        ("0" + m.getUTCHours()).slice(-2) + " " +
        ("0" + m.getUTCMinutes()).slice(-2) + " " +
        ("0" + m.getUTCSeconds()).slice(-2);
    return retval;
}