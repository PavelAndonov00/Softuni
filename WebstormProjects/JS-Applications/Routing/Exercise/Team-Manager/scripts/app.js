$(() => {
    window.location = '#/home';
    const app = Sammy('#main', function () {
        this.use('Handlebars', 'hbs');

        this.get('#/about', function () {
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs'
            }).then(function () {
                this.partial('./templates/about/about.hbs', {
                    username: sessionStorage.getItem('username'),
                    loggedIn: sessionStorage.getItem('username') !== null
                });
            });
        });

        this.get('#/home', function () {
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs'
            }).then(function () {
                this.partial('./templates/home/home.hbs', {
                    username: sessionStorage.getItem('username'),
                    loggedIn: sessionStorage.getItem('username') !== null
                });
            });
        });

        this.get('#/login', function () {
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
                loginForm: './templates/login/loginForm.hbs',
            }).then(function (){
                this.partial('./templates/login/loginPage.hbs', {
                    username: sessionStorage.getItem('username'),
                    loggedIn: sessionStorage.getItem('username') !== null
                });
            });
        });

        this.post('#/login', function (ctx) {
            let username = this.params.username;
            let password = this.params.password;

            auth.login(username, password)
                .then(function (res) {
                    auth.showInfo('Logged in successful!');
                    auth.saveSession(res);
                    ctx.redirect('#/home');
                }).catch(auth.handleError);
        });

        this.get('#/register', function () {
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
                registerForm: './templates/register/registerForm.hbs'
            }).then(function () {
                this.partial('./templates/register/registerPage.hbs', {
                    username: sessionStorage.getItem('username'),
                    loggedIn: sessionStorage.getItem('username') !== null
                });
            });
        });

        this.post('#/register', function (ctx) {
            let username = this.params.username;
            let password = this.params.password;

            auth.register(username, password)
                .then(function (res) {
                    auth.showInfo('Registered successful!');
                    auth.saveSession(res);
                    ctx.redirect('#/home')
                }).catch(auth.handleError);
        });

        this.get('#/logout', function () {
            auth.logout();
            this.redirect('#/home');
            sessionStorage.clear();
        });

        this.get('#/catalog', function () {
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
                team: './templates/catalog/team.hbs'
            }).then(function () {
                this.partial('./templates/catalog/teamCatalog.hbs', {
                    username: sessionStorage.getItem('username'),
                    loggedIn: sessionStorage.getItem('username') !== null,
                    hasNoTeam: sessionStorage.getItem('teamId') !== null,
                    teams: teamsService.loadTeams(),
                });
            })
        });

        this.get('#/catalog/:id', function () {
                this.loadPartials({
                    header: './templates/common/header.hbs',
                    footer: './templates/common/footer.hbs',
                    teamMember: './templates/catalog/teamMember.hbs',
                    teamControls: './templates/catalog/teamControls.hbs'
                }).then( () => {
                    // let id = this.params._id;
                    // let teamDetails = teamsService.loadTeamDetails(id);
                    // let members = [];
                    // let comment = '';
                    // let isAuthor = id === sessionStorage.getItem('userId');
                    // let isOnTeam = sessionStorage.getItem('teamId') !== null;
                    // teamDetails.then(function (res) {
                    //     members = res.members;
                    //     comment = res.comment;
                    // });
                    this.partial('./templates/catalog/details.hbs', {
                        username: sessionStorage.getItem('username'),
                        loggedIn: sessionStorage.getItem('username') !== null,
                        // members, comment, isAuthor, isOnTeam, id
                    })

                })
        });

        this.get('#/create', function () {
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
                createForm: './templates/create/createForm.hbs'
            }).then(function () {
                this.partial('./templates/create/createForm.hbs', {
                    username: sessionStorage.getItem('username'),
                    loggedIn: sessionStorage.getItem('username') !== null
                })
            })
        })
    });

    app.run();
});