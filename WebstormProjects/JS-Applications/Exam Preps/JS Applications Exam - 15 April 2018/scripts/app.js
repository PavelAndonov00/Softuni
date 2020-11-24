function startApp() {
    attachEvents();

    Sammy('#container', function () {
        this.use('Handlebars', 'handlebars');

        this.get('#/welcome', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars'
            }).then(function () {
                this.partial('templates/welcome.handlebars', {
                    username: sessionStorage.getItem('username'),
                    logged: sessionStorage.getItem('authtoken') !== null
                }).then(function () {
                    loginRegisterRedirectEvents();
                })
            })
        });

        this.post('#/login', function (ctx) {
            let username = ctx.params['username-login'];
            let password = ctx.params['password-login'];

            service.login(username, password);

            ctx.redirect('#/home');

            let loginForm = $('#login-form');
            loginForm.find("input[name=username-login]").val('');
            loginForm.find("input[name=password-login]").val('');
        });

        this.post('#/register', function (ctx) {
            let username = ctx.params['username-register'];
            let password = ctx.params['password-register'];
            let repPassword = ctx.params['password-register-check'];

            service.register(username, password, repPassword);

            ctx.redirect('#/home');

            let registerForm = $('#register-form');
            registerForm.find("input[name=username-register]").val('');
            registerForm.find("input[name=password-register]").val('');
            registerForm.find("input[name=password-register-check]").val('');
        });

        this.get('#/logout', function (ctx) {
            auth.logout().then(function () {
                auth.showInfo('Logout successful.');

                sessionStorage.clear();

                ctx.redirect('#/welcome')
            })
        });

        this.get('#/home', function (ctx) {

        })

    }).run();

    function attachEvents() {
        $(document).on({
            ajaxStart: function () {
                $('#loadingBox').show();
            },
            ajaxStop: function () {
                $('#loadingBox').hide();
            }
        });

        window.location = '#/welcome';
    }

    function loginRegisterRedirectEvents() {
        let registerForm = $('#register-form');
        registerForm.find("input[name=username-register]").on('change', () => window.location = '#/register');
        registerForm.find("input[name=password-register]").on('change', () => window.location = '#/register');
        registerForm.find("input[name=password-register-check]").on('change', () => window.location = '#/register');
        registerForm.find("input[type=submit]").on('click', () => window.location = '#/register');

        let loginForm = $('#login-form');
        loginForm.find("input[name=username-login]").on('change', () => window.location = '#/login');
        loginForm.find("input[name=password-login]").on('change', () => window.location = '#/login');
        loginForm.find("input[type=submit]").on('click', () => window.location = '#/login');
    }
}