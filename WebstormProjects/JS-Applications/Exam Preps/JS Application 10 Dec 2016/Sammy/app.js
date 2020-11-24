function startApp() {
    if (sessionStorage.getItem('authtoken') === null) {
        window.location = '#/welcome';
    } else {
        window.location = '#/edit';
    }
    $(document).on({
        ajaxStart: function () {
            $('#loadingBox').show()
        },
        ajaxStop: function () {
            $('#loadingBox').hide()
        }
    });

    const app = Sammy('#app', function () {
        this.use('Handlebars', 'handlebars');

        this.get('#/welcome', function () {
            this.loadPartials({
                header: './templates/common/header.handlebars',
                footer: './templates/common/footer.handlebars'
            }).then(function () {
                this.partial('./templates/welcome/welcome.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    username: sessionStorage.getItem('username')
                })
            })
        });

        this.get('#/login', function () {
            this.loadPartials({
                header: './templates/common/header.handlebars',
                footer: './templates/common/footer.handlebars'
            }).then(function () {
                this.partial('./templates/submit/submit.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    username: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/login', function () {
            let username = this.params.username;
            let password = this.params.password;

            let login = auth.login(username, password);

            login
                .then((res) => {
                    auth.showInfo('You have logged in successful!');
                    auth.saveSession(res);
                    this.redirect('#/edit');
                }).catch(function (err) {
                auth.handleError(err);
                auth.showError('Invalid credentials. Please retry your request with correct credentials.');
            });

        });

        this.get('#/comments', function () {
            this.loadPartials({
                header: './templates/common/header.handlebars',
                footer: './templates/common/footer.handlebars'
            }).then(function () {
                this.partial('./templates/comments/comments.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    username: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/comments', function () {
            let username = this.params.username;
            let password = this.params.password;
            let name = this.params.name;

            let register = auth.register(username, password, name);

            register
                .then((res) => {
                    auth.showInfo('You have registered successful!');
                    auth.saveSession(res);
                    this.redirect('#/edit');
                }).catch(function (err) {
                auth.handleError(err);
                auth.showError('This username is already taken. Please try with different!');
            });
        });

        this.get('#/logout', function () {
            auth.logout();
            sessionStorage.clear();

            auth.showInfo('You have logout successful!');

            this.redirect('#/welcome');
        });

        this.get('#/edit', function () {
            this.loadPartials({
                header: './templates/common/header.handlebars',
                footer: './templates/common/footer.handlebars'
            }).then(function () {
                this.partial('./templates/edit/welcome.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    username: sessionStorage.getItem('username')
                })
            })
        });

        this.get('#/myMessages', function () {
            let messages = auth.getMyMessages();

            let result = [];
            messages.then((messages) => {
                console.log(messages);
                for (let message of messages) {
                    result.push({
                        username: formater.formatSender(message.sender_name, message.sender_username),
                        message: message.text,
                        date: formater.formatDate(message._kmd.lmt)
                    })
                }

                this.loadPartials({
                    header: './templates/common/header.handlebars',
                    footer: './templates/common/footer.handlebars'
                }).then(function () {
                    this.partial('./templates/myMessages/myMessages.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        username: sessionStorage.getItem('username'),
                        messages: result
                    })
                })
            });
        });

        this.get('#/archiveMessages', function () {
            let archiveMessages = auth.getArchiveMessages();

            archiveMessages.then((archiveMessages) => {
                let result = [];

                archiveMessages = archiveMessages.sort((a, b) => a._kmd.lmt - b._kmd.lmt);
                for (let archiveMessage of archiveMessages) {
                    result.push({
                        id: archiveMessage._id,
                        username: archiveMessage.recipient_username,
                        message: archiveMessage.text,
                        date: formater.formatDate(archiveMessage._kmd.lmt)
                    })
                }

                this.loadPartials({
                    header: './templates/common/header.handlebars',
                    footer: './templates/common/footer.handlebars'
                }).then(function () {
                    this.partial('./templates/archiveMessages/archiveMessages.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        username: sessionStorage.getItem('username'),
                        messages: result
                    }).then(function () {
                        $('.btnDel').click(function () {
                            let parent = $(this).parent().parent();
                            let id = parent.attr('data-id');
                            let del = requester.remove('appdata', 'messages/' + id);

                            del.then(() => {
                                parent.remove();

                                auth.showInfo('Message deleted.')
                            })
                        });
                    });
                })
            })
        });

        this.get('#/sendMessage', function () {
            let users = auth.getUsers();

            let result = [];
            users.then((users) => {
                for (let user of users) {
                    result.push({
                        userName: user.username,
                        name: formater.formatSender(user.username, user.firstName)
                    })
                }

                this.loadPartials({
                    header: './templates/common/header.handlebars',
                    footer: './templates/common/footer.handlebars'
                }).then(function () {
                    this.partial('./templates/sendMessage/sendMessage.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        username: sessionStorage.getItem('username'),
                        users: result
                    })
                })
            })
        });
        this.post('#/sendMessage', function () {
            let text = this.params.text;
            let sender_username = sessionStorage.getItem('username');
            let sender_name = sessionStorage.getItem('name');
            let recipient_username = $('#formSendMessage select option:selected').val();

            let data = {
                text, sender_username, sender_name,
                recipient_username
            };

            let sentMsg = requester.post('appdata', 'messages/', '', data);

            sentMsg.then(() => {
                auth.showInfo('Message sent.');

                this.redirect('#/archiveMessages');
            })
        })
    });

    app.run();
}