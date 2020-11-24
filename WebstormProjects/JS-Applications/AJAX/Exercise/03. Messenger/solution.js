function attachEvents() {
    const baseUrl = 'https://messenger-68d0c.firebaseio.com/messenger/';
    $('#submit').on('click', function () {
        let author = $('#author').val();
        let content = $('#content').val();

        if(author && content) {
            let timestamp = new Date();
            let data = {
                author,
                content,
                timestamp
            };

            $.post({
                url: baseUrl + '.json',
                data: JSON.stringify(data)
            }).then(function (res) {
                $('#content').val("");
            }).catch(function (err) {
                console.log(err);
            })
        }
    });

    $('#refresh').click(function () {
        $('#messages').empty();
        $.get({
            url: baseUrl + '.json',
        }).then(function (objects) {
            let result = [];
            for (let keyObject in objects) {
                result.push(objects[keyObject]);
            }

            result.sort((a, b) => a.timestamp > b.timestamp);

            for (let obj of result) {
                $('#messages').append(obj.author + ": " + obj.content + '\n');
            }
        }).catch(function (err) {
            console.log(err);
        })
    })
}