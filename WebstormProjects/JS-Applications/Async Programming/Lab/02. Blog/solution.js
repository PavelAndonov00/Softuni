function attachEvents() {
    const BASE_URL = 'https://baas.kinvey.com/appdata/kid_rkeTRAT7m/';
    const USERNAME = 'guest';
    const PASSWORD = 'guest';
    const AUTH_BASE64 = btoa(USERNAME + ':' + PASSWORD);
    const authorization = {Authorization: 'Basic ' + AUTH_BASE64};

    $('#btnLoadPosts').click(function () {
        $.ajax({
            url: BASE_URL + 'posts',
            headers: authorization,
        }).then(function (posts) {
            for (let post of posts) {
                let option = $(`<option value="${post._id}">${post.title}</option>`);

                option.appendTo($('#posts'));
            }
        }).catch(handleError);
    });

    $('#btnViewPost').click(function () {
        let selected = $('#posts option:selected');

        $('#post-title').text(selected.text());
        $.ajax({
            url: BASE_URL + 'posts/' + selected.val(),
            headers: authorization
        }).then(function (post) {
            $('#post-body').empty();
            $('#post-body').append($('<li>').text(post.body));
        }).catch(handleError);

        $.ajax({
            url: BASE_URL + 'comments/' + `?query={"post_id":"${selected.val()}"}`,
            headers: authorization
        }).then(function (comments) {
            $('#post-comments').empty();
            for (let comment of comments) {
                let li = $('<li>').text(comment.text);

                $('#post-comments').append(li);
            }
        }).catch(handleError);
    });


    function handleError(error) {
        console.log(error);
    }
}