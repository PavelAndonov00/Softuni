function startApp() {
    // BASE DATA AND VARIABLES
    const baseDataUrl = 'https://baas.kinvey.com/appdata/kid_rk-Y0vVBX/messages/';
    const baseUserUrl = 'https://baas.kinvey.com/user/kid_rk-Y0vVBX/';
    const appKey = 'kid_rk-Y0vVBX';
    const appSecret = 'c6957e6bcfb54efab5cbf3e705cd785b';
    const authHeaders = {Authorization: 'Basic ' + btoa(appKey + ':' + appSecret)};
    const showView = (() => {
        function home() {
            $('main section').hide();
            $('#viewAppHome').show();
        }

        function login() {
            $('main section').hide();
            $('#viewLogin').show();
            $('#formLogin').trigger('reset');
        }

        function register() {
            $('main section').hide();
            $('#viewRegister').show();
            $('#formRegister').trigger('reset');
        }

        function myMessages() {
            $('main section').hide();
            $('#viewMyMessages').show();

            getMyMessages();
        }

        function archiveSend() {
            $('main section').hide();
            $('#viewArchiveSent').show();

            getArchiveMessages();
        }

        function sendMessages() {
            let form = $('#formSendMessage');

            $('main section').hide();
            $('#viewSendMessage').show();
            form.trigger('reset');

            fillRecipients();
        }

        function userHome() {
            $('main section').hide();
            $('#viewUserHome').show();
            $('#viewUserHomeHeading').text('Welcome, ' + sessionStorage.getItem('userName'));
        }

        return {
            home,
            login,
            register,
            myMessages,
            archiveSend,
            sendMessages,
            userHome
        }
    })();

    // ATTACH EVENTS AND BASE CODE
    showHideMenuLinks();

    // show loading when ajax start and stop
    $(document).on({
        ajaxStart: function () {
            $("#loadingBox").show()
        },
        ajaxStop: function () {
            $("#loadingBox").hide()
        }
    });

    // show views
    $('#linkMenuAppHome').click(showView.home);
    $('#linkMenuLogin').click(showView.login);
    $('#linkMenuRegister').click(showView.register);
    $('#linkMenuArchiveSent, #linkUserHomeArchiveSent').click(showView.archiveSend);
    $('#linkMenuMyMessages, #linkUserHomeMyMessages').click(showView.myMessages);
    $('#linkMenuSendMessage, #linkUserHomeSendMessage').click(showView.sendMessages);
    $('#linkMenuUserHome').click(showView.userHome);

    // button events
    $('#formLogin').submit(loginUser);
    $('#formRegister').submit(registerUser);
    $('#linkMenuLogout').click(logoutUser);
    $('#formSendMessage').submit(sendMsg);

    $('form').submit(function (event) {
        event.preventDefault()
    });

    // FUNCTIONS AND OTHER

    function showHideMenuLinks() {
        if (sessionStorage.getItem('authToken') === null) {
            $('.anonymous').show();
            $('.useronly').hide();
            showView.home();
        } else {
            $('.anonymous').hide();
            $('.useronly').show();

            $('#spanMenuLoggedInUser').text('Welcome, ' + sessionStorage.getItem('userName'));

            showView.userHome();
        }
    }

    function registerUser() {
        let username = $('#registerUsername').val();
        let password = $('#registerPasswd').val();
        let firstName = $('#registerName').val();

        let data = {username, password, firstName};

        $.post({
            url: baseUserUrl,
            headers: authHeaders,
            data,
            'Content-Type': 'application/json'
        }).then(function (res) {
            sessionStorage.setItem('authToken', res._kmd.authtoken);
            sessionStorage.setItem('userName', res.username);
            sessionStorage.setItem('firstName', res.firstName);
            sessionStorage.setItem('userId', res._id);

            showHideMenuLinks();

            showMsg('You have registered successful!');
        }).catch(function (err) {
            showError('This username is already taken. Please try with another!')
        })
    }

    function loginUser() {
        let username = $('#loginUsername').val();
        let password = $('#loginPasswd').val();

        let data = {username, password};

        $.post({
            url: baseUserUrl + 'login',
            headers: authHeaders,
            data,
            'Content-Type': 'application/json'
        }).then(function (res) {
            sessionStorage.setItem('authToken', res._kmd.authtoken);
            sessionStorage.setItem('firstName', res.firstName);
            sessionStorage.setItem('userName', res.username);
            sessionStorage.setItem('userId', res._id);

            showHideMenuLinks();
            showMsg('You have logged in successful!');
        }).catch(function (err) {
            console.log(err);

            showError('Invalid credentials. Please retry your request with correct credentials!')
        })
    }

    function logoutUser() {
        $.post({
            url: baseUserUrl + '_logout',
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
        }).then(function (res) {
            sessionStorage.clear();

            showHideMenuLinks();

            showMsg('You have logged out successful!');
        }).catch(function (err) {
            showError(err.responseJSON.description)
            console.log(err);
        })
    }

    function getMyMessages() {
        $.get({
            url: baseDataUrl + `?query={"recipient_username":"${sessionStorage.getItem('userName')}"}`,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
        }).then(function (msgs) {
            let tbody = $('#myMessages.handlebars table tbody');
            tbody.empty();
            for (let msg of msgs) {
                let tr = $('<tr>');
                if (!msg.sender_name) {
                    tr.append(
                        $('<td>').text(msg.sender_username)
                    )
                } else {
                    tr.append(
                        $('<td>').text(msg.sender_username + ' (' + msg.sender_name + ')')
                    )
                }

                tr.append(
                    $('<td>').text(msg.text)
                ).append(
                    $('<td>').text(formatDate(msg._kmd.lmt))
                );

                tbody.append(tr);
            }

        }).catch(function (err) {
            showError(err.responseJSON.description)
            console.log(err);
        })
    }

    function getArchiveMessages() {
        $.get({
            url: baseDataUrl + `?query={"sender_username":"${sessionStorage.getItem('userName')}"}`,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
        }).then(function (msgs) {
            let tbody = $('#sentMessages table tbody');
            tbody.empty();
            for (let msg of msgs) {
                let tr = $('<tr>').append(
                    $('<td>').text(msg.recipient_username)
                ).append(
                    $('<td>').text(msg.text)
                ).append(
                    $('<td>').text(formatDate(msg._kmd.lmt))
                ).append(
                    $('<td>').append(
                        $('<button>').text('Delete').click(function (event) {
                            $.ajax({
                                method: 'DELETE',
                                url: baseDataUrl + msg._id,
                                headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')}
                            }).then(function () {
                                $(event.target).parent().parent().remove();

                                showMsg('Message deleted.')
                            }).catch(function (err) {
                                showError(err.responseJSON.description);
                                console.log(err);
                            })
                        })
                    )
                );

                tbody.append(tr);
            }

        }).catch(function (err) {
            showError(err.responseJSON.description);
            console.log(err);
        })
    }

    function fillRecipients() {
        $.get({
            url: baseUserUrl,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
            'Content-Type': 'application/json'
        }).then(function (recipients) {
            let select = $('#msgRecipientUsername');
            select.empty();

            for (let recipient of recipients) {
                if (recipient._id === sessionStorage.getItem('userId')) continue;
                let option = $(`<option value="${recipient.username}">`)
                    .text(formatSender(recipient.firstName, recipient.username));

                select.append(option);
            }
        }).catch(function (err) {
            showError(err.responseJSON.description);
            console.log(err);
        });
    }

    function sendMsg() {
        let text = $('#msgText').val();
        let recipient_username = $('#formSendMessage').find('select option:selected').val();

        let sender_username = sessionStorage.getItem('userName');
        let sender_name = sessionStorage.getItem('firstName');
        let data = {
            sender_username,
            sender_name,
            recipient_username,
            text
        };

        $.post({
            url: baseDataUrl,
            headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')},
            data,
            'Content-Type': 'application/json'
        }).then(function () {
            showMsg('Message sent.');

            showView.archiveSend();
        }).catch(function (err) {
            showError(err.responseJSON.description);
            console.log(err);
        });
    }

    function formatDate(dateISO8601) {
        let date = new Date(dateISO8601);
        if (Number.isNaN(date.getDate()))
            return '';
        return date.getDate() + '.' + padZeros(date.getMonth() + 1) +
            "." + date.getFullYear() + ' ' + date.getHours() + ':' +
            padZeros(date.getMinutes()) + ':' + padZeros(date.getSeconds());

        function padZeros(num) {
            return ('0' + num).slice(-2);
        }
    }

    function showMsg(msg) {
        let infoBox = $('#infoBox');
        infoBox.text(msg);
        infoBox.show();
        setInterval(function () {
            infoBox.fadeOut();
        }, 3000)
    }

    function showError(err) {
        let errorBox = $('#errorBox');
        errorBox.text(err);
        errorBox.show();
        setInterval(function () {
            errorBox.fadeOut();
        }, 3000)
    }

    function formatSender(name, username) {
        if (!name)
            return username;
        else
            return username + ' (' + name + ')';
    }
}