function startApp() {
    attachEvents();
    if(sessionStorage.getItem('authtoken') !== null) {
        window.location = '#/home';
    } else {
        window.location = '#/register';
    }

    const app = Sammy('#main', function () {
        this.use('Handlebars', 'handlebars');

        this.get('#/register', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
                menu: 'templates/common/menu.handlebars'
            }).then(function () {
                this.partial('templates/register/register.handlebars', {
                    logged: sessionStorage.getItem('authtoken') !== null
                })
            })
        });
        this.post('#/register', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;
            let repeatPassword = ctx.params.repeatPass;

            if (username.length >= 5 && (password === repeatPassword) && password !== '') {
                let registerPromise = auth.register(username, password);

                registerPromise.then(function (res) {
                    let formRegister = $('#formRegister');
                    formRegister.find('input[name=username]').val('');
                    formRegister.find('input[name=password]').val('');
                    formRegister.find('input[name=repeatPass]').val('');

                    auth.showInfo('User registration successful.');

                    auth.saveSession(res);

                    ctx.redirect('#/home');
                }).catch(function (err) {
                    auth.handleError(err);

                    auth.showError('This username is already taken. Please try with other different.');
                })
            }
        });

        this.get('#/login', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
                menu: 'templates/common/menu.handlebars'
            }).then(function () {
                this.partial('templates/login/login.handlebars', {
                    logged: sessionStorage.getItem('authtoken') !== null
                })
            })
        });
        this.post('#/login', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;

            let loginPromise = auth.login(username, password);

            loginPromise.then(function (res) {
                let loginForm = $('#loginForm');
                loginForm.find('input[name=username]').val('');
                loginForm.find('input[name=password]').val('');

                auth.showInfo('Login successful.');

                auth.saveSession(res);

                ctx.redirect('#/home');
            }).catch(function (err) {
                auth.handleError(err);

                auth.showError('Invalid credentials. Please retry your request with correct credentials.');
            })
        });

        this.get('#/logout', function (ctx) {
            let logoutPromise = auth.logout();

            logoutPromise.then(function () {
                auth.showInfo('Logout successful.')
            });

            sessionStorage.clear();

            ctx.redirect('#');
        });

        this.get('#/home', function (ctx) {
            let userPromise = chirpService.getUser(sessionStorage.getItem('userId'));
            let username = sessionStorage.getItem('username');

            // get current logged user
            userPromise.then(function (user) {
                let subs = user.subscriptions;

                // fill an array with data
                let chirpersPromise = chirpService.getChirps();
                chirpersPromise.then(function (chirpers) {
                    let result = [];
                    for (let chirper of chirpers) {
                        if (subs.includes(chirper.author)) {
                            result.push({
                                text: chirper.text,
                                author: chirper.author,
                                time: calcTime(chirper._kmd.ect)
                            });
                        }
                    }


                    // get chirpers count
                    let chirpersCount = 0;
                    let chirpersCountPromise = chirpService.getChirpsCount();
                    chirpersCountPromise.then(function (res) {
                        chirpersCount = res.length;

                        // get following count
                        let followingCount = 0;
                        let followingPromise = chirpService.getFollowingCount();
                        followingPromise.then(function (res) {
                            followingCount = res.length;

                            // get followers count
                            let followersCount = 0;
                            let followersPromise = chirpService.getFollowersCount();
                            followersPromise.then(function (res) {
                                followersCount = res.length;

                                // render view
                                ctx.loadPartials({
                                    header: 'templates/common/header.handlebars',
                                    footer: 'templates/common/footer.handlebars',
                                    menu: 'templates/common/menu.handlebars'
                                }).then(function () {
                                    this.partial('templates/feed/feed.handlebars', {
                                        logged: sessionStorage.getItem('authtoken') !== null,
                                        username,
                                        chirps: result,
                                        chirpersCount,
                                        followingCount,
                                        followersCount
                                    })
                                })
                            });
                        });
                    });
                });
            });
        });
        this.post('#/home', function (ctx) {
            let username = ctx.params.text;

            // get all users to find this one
            let usersPromise = chirpService.getUsers();
            usersPromise.then(function (users) {
                let currentUser = users.filter(e => e.username = username);


            });
        });

        this.get('#/me', function (ctx) {
            let username = sessionStorage.getItem('username');

            // get all chirps to filter them for the current logged user
            let chirpsPromise = chirpService.getChirps();
            let result = [];
            chirpsPromise.then(function (chirps) {
                let userChirps = chirps.filter(e => e.author === username);

                for (let userChirp of userChirps) {
                    result.push({
                        author: userChirp.author,
                        time: calcTime(userChirp._kmd.ect),
                        text: userChirp.text,
                        id: userChirp._id
                    })
                }

                // get chirpers count
                let chirpersCount = 0;
                let chirpersCountPromise = chirpService.getChirpsCount();
                chirpersCountPromise.then(function (res) {
                    chirpersCount = res.length;

                    // get following count
                    let followingCount = 0;
                    let followingPromise = chirpService.getFollowingCount();
                    followingPromise.then(function (res) {
                        followingCount = res.length;

                        // get followers count
                        let followersCount = 0;
                        let followersPromise = chirpService.getFollowersCount();
                        followersPromise.then(function (res) {
                            followersCount = res.length;

                            // render view
                            ctx.loadPartials({
                                header: 'templates/common/header.handlebars',
                                footer: 'templates/common/footer.handlebars',
                                menu: 'templates/common/menu.handlebars'
                            }).then(function () {
                                this.partial('templates/me/me.handlebars', {
                                    logged: sessionStorage.getItem('authtoken') !== null,
                                    username,
                                    userChirps: result,
                                    chirpersCount,
                                    followingCount,
                                    followersCount
                                })
                            })
                        });
                    });
                });
            });
        });
        this.post('#/me', function (ctx) {
            let text = ctx.params.text;
            let author = sessionStorage.getItem('username');

            let createPromise = chirpService.createChirp(text, author);
            createPromise.then(function (createdChirp) {
                auth.showInfo('Chirp created.');

                $('#myChirps').append($(`<article class="chirp">
                            <div class="titlebar">
                                <a href="#/author" class="chirp-author">${createdChirp.author}</a>
                                <span class="chirp-time">${calcTime(createdChirp._kmd.ect)}</span>
                            </div>
                            <p>${createdChirp.text}</p>
                        </article>`));

                $('#formSubmitChirpMy textarea').val('')
            })
        });

        this.get('#/deleteChirp/:id', function (ctx) {
            let id = ctx.params.id;

            let deleteChirpPromise = chirpService.deleteChirp(id);
            deleteChirpPromise.then(function (deletedChirp) {
                auth.showInfo('Chirp deleted.');

                $(`article[data-id=${id}]`).remove();
            })
        })
    });

    app.run();


    function attachEvents() {
        $(document).on({
            ajaxStart: function () {
                $('#loadingBox').show();
            },
            ajaxStop: function () {
                $('#loadingBox').hide();
            }
        })
    }

    function calcTime(dateIsoFormat) {
        let diff = new Date - (new Date(dateIsoFormat));
        diff = Math.floor(diff / 60000);
        if (diff < 1) return 'less than a minute';
        if (diff < 60) return diff + ' minute' + pluralize(diff);
        diff = Math.floor(diff / 60);
        if (diff < 24) return diff + ' hour' + pluralize(diff);
        diff = Math.floor(diff / 24);
        if (diff < 30) return diff + ' day' + pluralize(diff);
        diff = Math.floor(diff / 30);
        if (diff < 12) return diff + ' month' + pluralize(diff);
        diff = Math.floor(diff / 12);
        return diff + ' year' + pluralize(diff);

        function pluralize(value) {
            if (value !== 1) return 's';
            else return '';
        }
    }

}