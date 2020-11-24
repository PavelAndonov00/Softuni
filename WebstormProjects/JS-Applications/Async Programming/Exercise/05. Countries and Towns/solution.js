// BASE DATA AND VARIABLES --->
const baseUrl = 'https://baas.kinvey.com/appdata/kid_rkeTRAT7m/';


// CODE --->
getCountries();
getTowns();

$('#addCountry').click(function () {
    let country = $('#country').val();

    if(country) {
        let data = {name: country};
        $('#countryLoad').css('display', '');
        $.post({
            url: baseUrl + 'countries',
            headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).then(function () {
            getCountries();
        }).catch(handleError);
    }
});

$('.addTown').click(function () {
    let town = $('.town').val();
    let country = $('.country').val();

    if(town && country) {
        let data = {name: town, country};
        $('#townLoad').css('display', '');
        $.post({
            url: baseUrl + 'towns',
            headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).then(function () {
            getTowns();
        }).catch(handleError);
    }
});


// CLASSES AND FUNCTIONS --->
class Country {
    constructor(name) {
        this.name = name;
    }
}

class Town {
    constructor(name, country) {
        this.name = name;
        this.country = country;
    }
}

function getCountries() {
    $.get({
        url: baseUrl + 'countries',
        headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
    }).then(function (countries) {
        $('#countries tbody').empty();
        for (let country of countries) {
            let tr = $('<tr>').append(
                $('<td>').append($('<input>').val(country.name))
            ).append(
                $('<td>').append(
                    $('<button class="deleteBtn">').text('Delete').click(function () {
                        $('#countryLoad').css('display', '');
                        $.ajax({
                            method: 'DELETE',
                            url: baseUrl + 'countries/' + country._id,
                            headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
                            contentType: 'application/json'
                        }).then(() => {
                            $(this).parent().parent().remove();
                            $('#countryLoad').css('display', 'none');
                        }).catch(handleError)
                    })
                ).append(
                    $('<button class="updateBtn">').text('Update').click(function () {
                        let parentTr = $(this).parent().parent();

                        let data = {name: $(parentTr).find('input').val()};

                        $.ajax({
                            method: 'PUT',
                            url: baseUrl + 'countries/' + country._id,
                            headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
                            contentType: 'application/json',
                            data: JSON.stringify(data)
                        }).catch(handleError)
                    })
                )
            );

            tr.appendTo($('#countries tbody'));
            $('p').css('display', 'none');
        }

        $('#country').val('');
    }).catch(handleError);
}

function getTowns() {
    $.get({
        url: baseUrl + 'towns',
        headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
    }).then(function (towns) {
        $('.towns tbody').empty();
        for (let town of towns) {
            let id = town._id;
            let tr = $('<tr>').append(
                $('<td>').append($('<input>').val(town.name))
            ).append(
                $('<td>').append($('<input>').val(town.country))
            ).append(
                $('<td>').append(
                    $('<button class="deleteBtn">').text('Delete').click(function () {
                        $('#townLoad').css('display', '');
                        $.ajax({
                            method: 'DELETE',
                            url: baseUrl + 'towns/' + id,
                            headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
                            contentType: 'application/json'
                        }).then( () => {
                            $(this).parent().parent().remove();
                            $('#townLoad').css('display', 'none');
                        }).catch(handleError)
                    })
                ).append(
                    $('<button class="updateBtn">').text('Update').click(function () {
                        let parentTr = $(this).parent().parent();

                        let data = {
                            name: $(parentTr).find('td:first-child input').val(),
                            country: $(parentTr).find('td:nth-child(2) input').val()
                        };

                        $.ajax({
                            method: 'PUT',
                            url: baseUrl + 'towns/' + id,
                            headers: {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')},
                            contentType: 'application/json',
                            data: JSON.stringify(data)
                        }).catch(handleError)
                    })
                )
            );

            tr.appendTo($('.towns tbody'));
            $('p').css('display', 'none');
        }

        $('.town').val('');
        $('.country').val('');
    }).catch(handleError);
}

function handleError(Error) {
    console.log(Error);
}