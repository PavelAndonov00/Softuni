function startApp() {
    // ATTACH LOADING MESSAGE ON AJAX STOP AND START
    $(document).on({
        ajaxStart: function () {
            $("#loadingBox").show()
        },
        ajaxStop: function () {
            $("#loadingBox").hide()
        }
    });

    // BASE DATA AND VARIABLES
    const BASE_URL = 'https://baas.kinvey.com/appdata/kid_rkeTRAT7m/advertisments';
    const BASE_USER_URL = 'https://baas.kinvey.com/user/kid_rkeTRAT7m/';
    const APP_KEY = 'kid_rkeTRAT7m';
    const APP_SECRET = '3fb16ab239db4ead846deaf90d03ecd9';
    const AUTH_HEADERS = {Authorization: 'Basic ' + btoa(APP_KEY + ':' + APP_SECRET)};
    let showView = (() => {
        function home() {
            $('.views').hide();
            $('#viewHome').trigger('reset').show();
        }

        function login() {
            $('.views').hide();
            $('#viewLogin').show();
            $('#formLogin').trigger('reset');
        }

        function register() {
            $('.views').hide();
            $('#viewRegister').show();
            $('#formRegister').trigger('reset');
        }

        function ads() {
            $('.views').hide();
            $('#viewAds').show();
        }

        function createAd() {
            $('.views').hide();
            $('#viewCreateAd').show();
            $('#formCreateAd').trigger('reset');
        }

        function editAd() {
            $('.views').hide();
            $('#viewEditAd').show();
        }

        return {
            home,
            login,
            register,
            ads,
            createAd,
            editAd
        }
    })();
    let showLink = (() => {
        function home() {
            $('#linkHome').show();
        }

        function login() {
            $('#linkLogin').show();
        }

        function register() {
            $('#linkRegister').show();
        }

        function listAds() {
            $('#linkListAds').show();
        }

        function createAd() {
            $('#linkCreateAd').show();
        }

        function logout() {
            $('#linkLogout').show();
        }

        return {
            home,
            login,
            register,
            listAds,
            createAd,
            logout
        }
    })();
    let hideLink = (() => {
        function home() {
            $('#linkHome').hide();
        }

        function login() {
            $('#linkLogin').hide();
        }

        function register() {
            $('#linkRegister').hide();
        }

        function listAds() {
            $('#linkListAds').hide();
        }

        function createAd() {
            $('#linkCreateAd').hide();
        }

        function logout() {
            $('#linkLogout').hide();
        }

        return {
            home,
            login,
            register,
            listAds,
            createAd,
            logout
        }
    })();
    let showMsg = (() => {
        function loading() {
            $('#loadingBox').show()
        }

        function info(infoMsg) {
            let infoBox = $('#infoBox');
            infoBox.show().text(infoMsg);
            setInterval(infoBox.fadeOut(), 4000)
        }

        function error(errorMsg) {
            $('#errorBox').show().text(errorMsg)
        }

        return {
            loading,
            info,
            error
        }
    })();

    // ATTACH EVENT LISTENERS
    showHideMenuLinks();

    // Attach event to error msg
    $('#errorBox').click(function () {
        $(this).fadeOut();
    });

    // Attach events on links in menu
    $('#linkHome').click(showView.home);
    $('#linkLogin').click(showView.login);
    $('#linkRegister').click(showView.register);
    $('#linkListAds').click(listAdvertisements);
    $('#linkCreateAd').click(showView.createAd);
    $('#linkLogout').click(logoutUser);

    // Attach events on form's buttons
    $('#buttonRegisterUser').click(registerUser);
    $('#buttonLoginUser').click(loginUser);
    $('#buttonEditAd').click(editAd);
    $('#buttonCreateAd').click(createAd);


    // FUNCTIONS AND OTHERS
    function registerUser() {
        let formRegister = $('#formRegister');
        let username = formRegister.find('input[name=username]').val();
        let password = formRegister.find('input[name=passwd]').val();

        $.post({
            url: BASE_USER_URL,
            headers: AUTH_HEADERS,
            data: {username, password},
            'Content-Type': 'application/json'
        }).then(function (res) {
            saveSessionInfo(res);
            showHideMenuLinks();
            showMsg.info('Registered successful!');
        }).catch(function () {
            showMsg.error('Invalid credentials!')
        });
    }

    function loginUser() {
        let formLogin = $('#formLogin');
        let username = formLogin.find('input[name=username]').val();
        let password = formLogin.find('input[name=passwd]').val();

        $.post({
            url: BASE_USER_URL + 'login',
            headers: AUTH_HEADERS,
            data: {username, password},
            'Content-Type': 'application/json'
        }).then(function (res) {
            saveSessionInfo(res);
            showHideMenuLinks();
            showMsg.info('Login successful!');
        }).catch(function () {
            showMsg.error('Invalid credentials!');
        })
    }

    function logoutUser() {
        $.post({
            url: BASE_USER_URL + '_logout',
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')}
        }).then(function () {
            sessionStorage.clear();
            showHideMenuLinks();
            showMsg.info('Logout successful!');
        }).catch(function () {
            showMsg.error()
        });
    }

    function listAdvertisements() {
        $.get({
            url: BASE_URL,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')}
        }).then(function (ads) {
            let tbody = $('#viewAds table tbody');
            tbody.empty();
            for (let ad of ads.sort((a, b) => b.views - a.views)) {
                let tr = $('<tr>').append(
                    $('<td>').text(ad.title)
                ).append(
                    $('<td>').text(ad.publisher)
                ).append(
                    $('<td>').text(ad.description)
                ).append(
                    $('<td>').text(ad.price)
                ).append(
                    $('<td>').text(ad.dataPublished)
                );

                let td = $('<td>').append(
                    $('<a href="#">[Read More]</a>').click(function () {
                        $.ajax({
                            method: 'PUT',
                            url: BASE_URL + '/' + ad._id,
                            headers: {Authorization: 'Kinvey ' + localStorage.getItem('authToken')},
                            data: {
                                "userID": ad.userID,
                                "publisher": ad.publisher,
                                "title": ad.title,
                                "description": ad.description,
                                "price": ad.price,
                                "dataPublished": ad.dataPublished,
                                "views": Number(Number(ad.views) + 1),
                                "img": ad.img
                            },
                            'Content-Type': 'application/json'
                        });

                        $('.views').hide();
                        $('#img').attr('src', ad.img);
                        $('#title').text(ad.title);
                        $('#description').text(ad.description);
                        $('#publisher').text(ad.publisher);
                        $('#date').text(ad.dataPublished);
                        $('#views').text(ad.views);

                        $('#viewReadMore').show();
                    })
                );
                if (ad.userID === sessionStorage.getItem('userID')) {
                    td.append(
                        $('<a href="#">[Delete]</a>').click(function () {
                            deleteAd(ad._id)
                        })
                    ).append(
                        $('<a href="#">[Edit]</a>').click(function () {
                            fillEditFormWithCurrentData(ad._id, ad.publisher, ad.title, ad.description,
                                ad.price, ad.dataPublished, ad.img);
                        })
                    )
                } else {
                    td.append(
                        $('<a>[Delete]</a>')
                    ).append(
                        $('<a>[Edit]</a>')
                    )
                }

                tr.append(td);
                tbody.append(tr);
            }

            showView.ads();
        }).catch(function () {
            showMsg.error()
        });
    }

    function createAd() {
        let createForm = $('#formCreateAd');
        let views = 1;
        let userID = sessionStorage.getItem('userID');
        let title = createForm.find('input[name=title]').val();
        let publisher = sessionStorage.getItem('username');
        let description = createForm.find('textarea').val();
        let dataPublished = createForm.find('input[name=datePublished]').val();
        let price = createForm.find('input[name=price]').val();
        let img = createForm.find('input[name=image]').val();

        let data = {
            views: Number(views),
            userID,
            title,
            publisher,
            description,
            dataPublished,
            price,
            img
        };

        $.post({
            url: BASE_URL,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
            data: data,
            'Content-Type': 'application/json',
        }).then(function () {
            listAdvertisements();
            showMsg.info('Created successful!');
        }).catch(function () {
            showMsg.error()
        });
    }

    function deleteAd(id) {
        $.ajax({
            method: 'DELETE',
            url: BASE_URL + '/' + id,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')}
        }).then(function () {
            $(this).parent().parent().remove();
            listAdvertisements();
            showMsg.info('Deleted successful!');
        }).catch(function () {
            showMsg.error()
        });
    }

    function editAd() {
        let formEdit = $('#formEditAd');
        let id = formEdit.find('input[name=id]').val();
        let userID = sessionStorage.getItem('userID');
        let publisher = formEdit.find('input[name=publisher]').val();
        let title = formEdit.find('input[name=title]').val();
        let description = formEdit.find('textarea').val();
        let price = formEdit.find('input[name=price]').val();
        let dataPublished = formEdit.find('input[name=datePublished]').val();
        let img = formEdit.find('input[name=image]').val();

        let data = {
            publisher, title,
            description, price,
            dataPublished, userID, img
        };

        $.ajax({
            method: 'PUT',
            url: BASE_URL + '/' + id,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
            data: data,
            'Content-Type': 'application/json'
        }).then(function () {
            listAdvertisements();
            showMsg.info('Edited successful!');
        }).catch(function () {
            showMsg.error()
        });
    }

    function fillEditFormWithCurrentData(id, publisher, title, description, price, dataPublished, img) {
        showView.editAd();

        let formEdit = $('#formEditAd');
        formEdit.find('input[name=id]').val(id);
        formEdit.find('input[name=publisher]').val(publisher);
        formEdit.find('input[name=title]').val(title);
        formEdit.find('textarea').val(description);
        formEdit.find('input[name=price]').val(price);
        formEdit.find('input[name=datePublished]').val(dataPublished);
        formEdit.find('input[name=image]').val(img);
    }

    function showHideMenuLinks() {
        if (sessionStorage.getItem('authToken') !== null) {
            showLink.home();
            showLink.createAd();
            showLink.listAds();
            showLink.logout();
            hideLink.login();
            hideLink.register();
            $('#loggedInUser').show().text('Welcome, ' + sessionStorage.getItem('username') + '!');
        } else {
            showLink.home();
            showLink.login();
            showLink.register();
            hideLink.createAd();
            hideLink.listAds();
            hideLink.logout();
            $('#loggedInUser').text('');
        }

        showView.home();
    }

    function saveSessionInfo(res) {
        sessionStorage.setItem('username', res.username);
        sessionStorage.setItem('userID', res._id);
        sessionStorage.setItem('authToken', res._kmd.authtoken);
    }
}