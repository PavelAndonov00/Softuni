function attachEvents() {
    // BASE DATA
    const BASE_URL = 'https://baas.kinvey.com/appdata/kid_rkeTRAT7m/biggestCatches/';
    const USER = 'guest';
    const PASS = 'guest';
    const BASE_64 = btoa(USER + ':' + PASS);
    const AUTH = {'Authorization': 'Basic ' + BASE_64};

    // VARIABLES
    const anglerInput = $('.angler');
    const weightInput = $('.weight');
    const speciesInput = $('.species');
    const locationInput = $('.location');
    const baitInput = $('.bait');
    const captureTimeInput = $('.captureTime');
    const loadBtn = $('.load');

    // CODE --->

    // attach event to load button
    loadBtn.click(function () {
        $.get({
            url: BASE_URL,
            headers: AUTH
        }).then(function (catches) {
            $('#catches').empty();
            for (let c of catches) {
                let div = $(`<div class="catch" data-id="${c._id}">`).append(
                    $('<label>Angler</label>')
                ).append(
                    $(`<input type="text" class="angler" value="${c.angler}"/>`)
                ).append(
                    $('<label>Weight</label>')
                ).append(
                    $(`<input type="number" class="weight" value="${Number(c.weight)}"/>`)
                ).append(
                    $('<label>Species</label>')
                ).append(
                    $(`<input type="text" class="species" value="${c.species}"/>`)
                ).append(
                    $('<label>Location</label>')
                ).append(
                    $(`<input type="text" class="location" value="${c.location}"/>`)
                ).append(
                    $('<label>Bait</label>')
                ).append(
                    $(`<input type="text" class="bait" value="${c.bait}"/>`)
                ).append(
                    $('<label>Capture Time</label>')
                ).append(
                    $(`<input type="number" class="captureTime" value="${Number(c.captureTime)}"/>`)
                ).append(
                    $('<button class="update">Update</button>').click(function () {
                        let inputValue = $(`div[data-id="${c._id}"]`);

                        let data = {
                            captureTime: Number($(inputValue).find('.captureTime').val()),
                            bait: $(inputValue).find('.bait').val(),
                            location: $(inputValue).find('.location').val(),
                            species: $(inputValue).find('.species').val(),
                            weight: Number($(inputValue).find('.weight').val()),
                            angler: $(inputValue).find('.angler').val()
                        };

                        $.ajax({
                            method: 'PUT',
                            url: BASE_URL + c._id,
                            headers: AUTH,
                            contentType: 'application/json',
                            data: JSON.stringify(data),
                        }).then(function () {
                            loadBtn.click();
                        }).catch(handleError);
                    })
                ).append(
                    $('<button class="delete">Delete</button>').click(function (event) {
                        $.ajax({
                            method: 'DELETE',
                            url: BASE_URL + c._id,
                            headers: AUTH
                        }).then(function () {
                            $(event.target).parent().remove();
                        }).catch(handleError)
                    })
                );

                $('#catches').append(div);
            }
        }).catch(handleError);


        //attach event to add button
        $('.add').click(function () {
            let inputValues = getInputValues(anglerInput, weightInput, speciesInput, locationInput, baitInput, captureTimeInput);

            if(inputValues.angler && inputValues.weight && inputValues.species &&
               inputValues.location && inputValues.bait && inputValues.captureTime) {

                $.post({
                    url: BASE_URL,
                    headers: AUTH,
                    contentType: 'application/json',
                    data: JSON.stringify(inputValues)
                }).then(function () {
                    loadBtn.click()
                }).catch(handleError);
            }
        });


        function getInputValues(angler, weight, species, location, bait, captureTime) {
            return {
                angler: $(angler).val(),
                weight: Number($(weight).val()),
                species: $(species).val(),
                location: $(location).val(),
                bait: $(bait).val(),
                captureTime:Number($(captureTime).val())
            }
        }

        function handleError(error) {
            console.log(error);
        }
    })
}


