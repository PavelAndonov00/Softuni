// BASE DATA
const BASE_URL = 'https://baas.kinvey.com/appdata/kid_rkeTRAT7m/books/';
const USER = 'pesho';
const PASS = 'ppp';
const BASE_64 = btoa(USER + ':' + PASS);
const AUTH = 'Basic ' + BASE_64

// CODE

$('#loadBtn').click(function () {
    $.get({
        url: BASE_URL,
        headers: {
            'Authorization': AUTH,
            'Content-Type': 'application/json'
        }
    }).then(function (books) {
        for (let book of books) {
            let div = $(`<fieldset class="book" data-id="${book._id}">`)
                .append(
                    $('<label>Title</label>')
                ).append(
                    $(`<input type="text" class="title">`).val(book.title)
                ).append(
                    $('<label>Tags</label>')
                ).append(
                    $(`<input type="text" class="tags">`).val(book.tags)
                ).append(
                    $('<label>ISBN</label>')
                ).append(
                    $(`<input type="text" class="isbn">`).val(book.isbn)
                ).append(
                    $('<label>Author</label>')
                ).append(
                    $(`<input type="text" class="author">`).val(book.author)
                ).append(
                    $('<button>').text('UPDATE').click(function () {
                        let currentBook = $(`div[data-id=${book._id}]`);
                        let data = {
                            title: currentBook.find('.title'),
                            author: currentBook.find('.author'),
                            isbn: currentBook.find('.isbn'),
                            tags: currentBook.find('.tags')
                        };

                        $.ajax({
                            method: 'PUT',
                            url: BASE_URL + book._id,
                            headers: {
                                'Authorization': AUTH,
                                'Content-Type': 'application/json'
                            },
                            data: JSON.stringify(data)
                        })
                    })
                ).append(
                    $('<button>').text('DELETE').click(function () {
                        $.ajax({
                            method: 'DELETE',
                            url: BASE_URL + book._id,
                            headers: {
                                'Authorization': AUTH,
                                'Content-Type': 'application/json'
                            }
                        }).then(() => {
                            $(this).parent().remove();
                        }).catch(handleError)
                    })
                );

            $('#books').append(div);
        }
    }).catch(handleError)
});

function handleError(Error) {
    console.log(Error);
}