function attachEvents() {
    // BASE DATA AND VARIABLES
    const baseUrl = 'https://baas.kinvey.com/appdata/kid_rkeTRAT7m/players';
    const auth = {Authorization: 'Basic ' + btoa('pesho' + ':' + 'ppp')};
    const playersDiv = $('#players');
    const reloadBtn = $('#reload');
    const saveBtn = $('#save');
    const addInput = $('#addName');
    const canvas = $('.canvas');

    // CODE
    loadPlayers();
    $('#addPlayer').click(addPlayer);
    saveBtnEvent();


    // FUNCTIONS AND OTHER
    function saveBtnEvent() {

    }

    function addPlayer() {
        let name = addInput.val();

        let data = {
            name,
            money: 500,
            bullets: 6
        };

        $.post({
            url: baseUrl,
            headers: auth,
            data: JSON.stringify(data),
            contentType: 'application/json'
        }).then(loadPlayers)
            .catch(handleError)
    }

    function loadPlayers() {
        $.get({
            url: baseUrl,
            headers: auth
        }).then(function (players) {
            addInput.val('');
            playersDiv.empty();
            for (let player of players) {
                let playerTemplate = $('<div class="player" data-id="${player._id}">').append(
                    $('<div class="row">').append(
                        $('<label>Name:</label>')
                    ).append(
                        $(`<label class="name">${player.name}</label>`)
                    )
                ).append(
                    $('<div class="row">').append(
                        $('<label>Money:</label>')
                    ).append(
                        $(`<label class="name">${player.money}</label>`)
                    )
                ).append(
                    $('<div class="row">').append(
                        $('<label>Bullets:</label>')
                    ).append(
                        $(`<label class="name">${player.bullets}</label>`)
                    )
                );

                let playBtn = $('<button class="play">Play</button>').click(function () {
                    canvas.empty();
                    canvas.append($('<canvas style="display: none;" width="800" height="600" style="border: 5px solid orange" id="canvas">Canvas not supported</canvas>'))
                    saveBtn.css('display', '');
                    reloadBtn.css('display', '');
                    $('#canvas').css('display', '');
                    loadCanvas(player);

                    reloadBtn.click(function () {
                        let diff = 6 - player.bullets;
                        player.money -= diff * 10;
                        player.bullets += diff;
                    });

                    saveBtn.click(function () {
                        let data = {
                            name: player.name,
                            money: player.money,
                            bullets: player.bullets
                        };

                        $.ajax({
                            method: 'PUT',
                            url: baseUrl + '/' + player._id,
                            headers: auth,
                            data: JSON.stringify(data),
                            contentType: 'application/json'
                        }).then(function () {
                            loadPlayers();
                        }).catch(handleError)
                    })
                });

                let deleteBtn = $('<button class="delete">Delete</button>').click(function () {
                    $.ajax({
                        method: 'DELETE',
                        url: baseUrl + '/' + player._id,
                        headers: auth
                    }).then(() => {
                        $(this).parent().remove();
                    }).catch(handleError);
                });

                playerTemplate.append(playBtn);
                playerTemplate.append(deleteBtn);

                playersDiv.append(playerTemplate);
            }
        }).catch(handleError)
    }

    function handleError(Error) {
        console.log(Error);
    }
}