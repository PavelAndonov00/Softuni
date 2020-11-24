function attachEvents() {
    const conditions = {
        Sunny: '&#x2600;',
        'Partly sunny': '&#x26C5;',
        Overcast: '&#x2601;',
        Rain: '&#x2614;',
        Degrees: '&#176;'
    };

    const BASE_URL = 'https://judgetests.firebaseio.com/';

    $('#submit').click(function () {
        $.ajax({
            url: BASE_URL + 'locations.json'
        }).then(function (locations) {
            // get and find name
            let targetName = $('#location').val();
            let current = locations.find(e => e.name === targetName);
            let code = current.code;

            // get current conditions
            $.ajax({
                url: BASE_URL + 'forecast/today/' + code +'.json'
            }).then(function (targetObj) {
                let condition = targetObj.forecast.condition;
                let high = targetObj.forecast.high;
                let low = targetObj.forecast.low;
                let name = targetObj.name;

                $('#current').append(
                    $(`<span class="condition symbol">${conditions[condition]}</span>`)
                ).append(
                    $('<span class="condition">').append(
                        $('<span class="forecast-data">').text(name)
                    ).append(
                        $(`<span class="forecast-data">${low}&#8451;/${high}&#8451;</span>`)
                    ).append(
                        $('<span class="forecast-data">').text(condition)
                    )
                );
            }).catch(handleError);


            // get upcoming days forecast
            $.ajax({
                url: BASE_URL + 'forecast/upcoming/' + code + '.json'
            }).then(function (upcomingObj) {
                console.log(upcomingObj);
                let forecastObj = upcomingObj.forecast;
                console.log(forecastObj);
                for (let forecast of forecastObj) {
                    let condition = forecast.condition;
                    let high = forecast.high;
                    let low = forecast.low;

                    let upcomingSpan = $('<span class="upcoming">').append(
                        $(`<span class="symbol">${conditions[condition]}</span>`)
                    ).append(
                        $(`<span class="forecast-data">${low}&#8451;/${high}&#8451;</span>`)
                    ).append(
                        $('<span class="forecast-data">').text(condition)
                    );

                    $('#upcoming').append(upcomingSpan);
                }
            }).catch(handleError);

            // display forecast div
            $('#forecast').css('display', '');
        }).catch(handleError);
    });

    function handleError(error) {
        console.log(error);
    }
}