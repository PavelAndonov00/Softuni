const BASE_URL = 'https://baas.kinvey.com/';
const APP_KEY = 'kid_rkeTRAT7m';
const APP_SECRET = '3fb16ab239db4ead846deaf90d03ecd9';
const AUTH_HEADERS = {'Authorization': "Basic " + btoa(APP_KEY + ":" + APP_SECRET)};
const BOOKS_PER_PAGE = 10;

function loginUser() {
    let username = $('#formLogin input[name=username]').val();
    let password = $('#formLogin input[name=passwd]').val();
    $.post({
        url: BASE_URL + 'user/' + APP_KEY + '/login',
        headers: AUTH_HEADERS,
        data: {username, password},
    }).then(function (res) {
        signInUser(res, 'Login successful.')
    }).catch(handleAjaxError)
}

function registerUser() {
    let username = $('#formRegister input[name=username]').val();
    let password = $('#formRegister input[name=passwd]').val();
    $.post({
        url: BASE_URL + 'user/' + APP_KEY + '/',
        headers: AUTH_HEADERS,
        data: {username, password}
    }).then(function (res) {
        signInUser(res, 'Registration successful.')
    }).catch(handleAjaxError)
}

function listBooks() {
    $.get({
        url: BASE_URL + 'appdata/' + APP_KEY + '/books',
        headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')}
    }).then(function (res) {
        showView('viewBooks');
        displayPaginationAndBooks(res.reverse())
    }).catch(handleAjaxError)
}


function createBook() {
    let title = $('#formCreateBook input[name=title]').val();
    let author = $('#formCreateBook input[name=author]').val();
    let description = $('#formCreateBook textarea').val();
    let authorID = sessionStorage.getItem('userId');

    let data = {authorID, title, author, description};

    $.post({
        url: BASE_URL + 'appdata/' + APP_KEY + '/books',
        headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
        data: JSON.stringify(data),
        contentType: 'application/json'
    }).then(function () {
        listBooks();
        showView('viewBooks');
        showInfo('Books created.');
    }).catch(handleAjaxError);
}

function deleteBook(book) {
    $.ajax({
        method: 'DELETE',
        headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
        url: BASE_URL + 'appdata/' + APP_KEY + '/books/' + book._id,
    }).then(function () {
        $('#' + book._id).remove();
        setInterval(showInfo('Book deleted.'), 2000);
    }).catch(handleAjaxError)
}

function editBook() {
    let id = $('#formEditBook input[name=id]').val();
    let title = $('#formEditBook input[name=title]').val();
    let author = $('#formEditBook input[name=author]').val();
    let description = $('#formEditBook textarea').val();

    let editedBook = {
        authorID: sessionStorage.getItem('userId'),
        title,
        author,
        description
    };

    $.ajax({
        method: 'PUT',
        url: BASE_URL + 'appdata/' + APP_KEY + '/books/' + id,
        headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
        data: JSON.stringify(editedBook),
        contentType: 'application/json'
    }).then(function () {
        listBooks();
        showView('viewBooks');
        showInfo('Book edited.');
    }).catch(handleAjaxError);
}

function logoutUser() {
    sessionStorage.clear();

    showHomeView();
    showHideMenuLinks();
    showInfo('Logout successful.')
}

function signInUser(res, message) {
    sessionStorage.setItem('username', res.username);
    sessionStorage.setItem('userId', res._id);
    sessionStorage.setItem('authToken', res._kmd.authtoken);

    showHomeView();
    showHideMenuLinks();
    showInfo(message);
}

function displayPaginationAndBooks(books) {
    let pagination = $('#pagination-demo')
    if (pagination.data("twbs-pagination")) {
        pagination.twbsPagination('destroy')
    }
    pagination.twbsPagination({
        totalPages: Math.ceil(books.length / BOOKS_PER_PAGE),
        visiblePages: 5,
        next: 'Next',
        prev: 'Prev',
        onPageClick: function (event, page) {
            $('#books table tbody').empty();
            let startBook = (page - 1) * BOOKS_PER_PAGE
            let endBook = Math.min(startBook + BOOKS_PER_PAGE, books.length)
            $(`a:contains(${page})`).addClass('active')
            for (let i = startBook; i < endBook; i++) {
                let book = books[i];
                let tr = $(`<tr id="${book._id}">`).append(
                    $('<td>').text(book.title)
                ).append(
                    $('<td>').text(book.author)
                ).append(
                    $('<td>').text(book.description)
                );

                if (sessionStorage.getItem('userId') === book.authorID) {
                    $('<td>').append(
                        $('<a href="#">[Delete]</a>').click(function () {
                            deleteBook(book);
                        })
                    ).append(
                        $('<a href="#">[Edit]</a>').click(function () {
                            showView('viewEditBook');

                            $('#formEditBook input[name=id]').val(book._id);
                            $('#formEditBook input[name=title]').val(book.title);
                            $('#formEditBook input[name=author]').val(book.author);
                            $('#formEditBook textarea').val(book.description);
                        })
                    ).appendTo(tr);
                } else {
                    $('<td>').append(
                        $('<a>[Delete]</a>')
                    ).append(
                        $('<a>[Edit]</a>')
                    ).appendTo(tr);
                }

                $('#books table tbody').append(tr);
            }
        }
    })
}

function handleAjaxError(response) {
    let errorMsg = JSON.stringify(response)
    if (response.readyState === 0)
        errorMsg = "Cannot connect due to network error."
    if (response.responseJSON && response.responseJSON.description)
        errorMsg = response.responseJSON.description
    showError(errorMsg)
}