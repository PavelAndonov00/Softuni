const kinveyController = (function () {
    const BASE_USER_URL = 'https://baas.kinvey.com/user/kid_BJOvMC-r7/';
    const APP_KEY = 'kid_BJOvMC-r7';
    const APP_SECRET = '30d738245ce24d46b4f0149a55782b6e';
    const AUTH_HEADERS = {Authorization: 'Basic ' + btoa(APP_KEY + ':' + APP_SECRET)};

    function register(username, password, firstName, lastName, phone, email) {
        let data = {username, password, firstName, lastName, phone, email};

        $.post({
            url: BASE_USER_URL,
            headers: AUTH_HEADERS,
            data,
        }).then(function (res) {
            sessionStorage.setItem('username', username);
            sessionStorage.setItem('password', password);
            sessionStorage.setItem('firstName', firstName);
            sessionStorage.setItem('lastName', lastName);
            sessionStorage.setItem('phone', phone);
            sessionStorage.setItem('email', email);
            sessionStorage.setItem('authToken', res._kmd.authtoken);
            sessionStorage.setItem('id', res._id);

            showHideMenuLinks();
        }).catch(handleError)
    }

    function login(username, password) {
        let data = {username, password};

        $.post({
            url: BASE_USER_URL + 'login',
            headers: AUTH_HEADERS,
            data,
            'Content-Type': 'application/json'
        }).then(function (res) {
            sessionStorage.setItem('username', username);
            sessionStorage.setItem('firstName', res.firstName);
            sessionStorage.setItem('lastName', res.lastName);
            sessionStorage.setItem('phone', res.phone);
            sessionStorage.setItem('email', res.email);
            sessionStorage.setItem('authToken', res._kmd.authtoken);
            sessionStorage.setItem('id', res._id);

            showHideMenuLinks();
        }).catch(handleError)
    }

    function logout() {
        $.post({
            url: BASE_USER_URL + '_logout',
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')}
        }).then(function () {
            sessionStorage.clear();
            showHideMenuLinks();
        }).catch(handleError);
    }

    function edit(firstName, lastName, phone, email) {
        let data = {firstName, lastName, phone, email};

        $.ajax({
            method: 'PUT',
            url: BASE_USER_URL + sessionStorage.getItem('id'),
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
            data,
            'Content-Type': 'application/json'
        }).then(function (res) {
            sessionStorage.setItem('firstName', res.firstName);
            sessionStorage.setItem('lastName', res.lastName);
            sessionStorage.setItem('phone', res.phone);
            sessionStorage.setItem('email', res.email);
            sessionStorage.setItem('authToken', res._kmd.authtoken);
            sessionStorage.setItem('id', res._id);

        }).catch(handleError)
    }

    function showHideMenuLinks() {
        if (sessionStorage.getItem('authToken') !== null) {
            $('#login').hide();
            $('#register').hide();
            $('#logout').show();
            $('#profile').show();
            $('#contacts').show();
        } else {
            $('#login').show();
            $('#register').show();
            $('#logout').hide();
            $('#profile').hide();
            $('#contacts').hide();
        }
    }

    function handleError(err) {
        console.log(err);
    }

    return {
        register,
        showHideMenuLinks,
        login,
        logout,
        edit,
    }
})();