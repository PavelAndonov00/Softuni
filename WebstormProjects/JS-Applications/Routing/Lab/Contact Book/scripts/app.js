$(() => {
    const app = Sammy('#main', function () {
        this.use('Handlebars', 'hbs');

        this.get('#/login', function () {
            this.partial('./templates/login.hbs')
        });

        this.post('#/login', function () {
            let username = this.params.username;
            let password = this.params.password;

            kinveyController.login(username, password);
        });

        this.get('#/logout', function () {
            kinveyController.logout();
            this.redirect('#/login')
        });

        this.get('#/register', function () {
            this.partial('./templates/reg.hbs')
        });

        this.post('#/register', function () {
            let username = this.params.username;
            let password = this.params.password;
            let firstName = this.params.firstName;
            let lastName = this.params.lastName;
            let phone = this.params.phone;
            let email = this.params.email;

            kinveyController.register(username, password, firstName, lastName, phone, email);

            this.redirect('#/profile')
        });


        this.get('#/contacts', async function () {
            this.contacts = await $.get({
                url: 'https://baas.kinvey.com/user/kid_BJOvMC-r7/',
                headers: {Authorization: 'Kinvey ' + sessionStorage.getItem('authToken')}
            });

            await this.partial('./templates/contacts.hbs');


            let contacts = this.contacts;
            $('.contact').click(async function () {
                $('#details').remove();

                let firstName = $(this).find('.title').text().split(' ')[0];
                let contact = contacts.filter(e => e.firstName === firstName)[0];
                let parent = $(this).parent().parent();

                let html = await $.get('./templates/details.hbs');
                let template = Handlebars.compile(html);
                let result = template(contact);

                $(result).insertAfter(parent);
            })
        });

        this.get('#/profile', function () {
            this.firstName = sessionStorage.getItem('firstName');
            this.lastName = sessionStorage.getItem('lastName');
            this.phone = sessionStorage.getItem('phone');
            this.email = sessionStorage.getItem('email');

            this.partial('./templates/profile.hbs');
        });

        this.post('#/profile', function () {
            let firstName = this.params.firstName;
            let lastName = this.params.lastName;
            let phone = this.params.phone;
            let email = this.params.email;

            kinveyController.edit(firstName, lastName, phone, email);

            this.redirect('#/contacts')
        })
    });

    app.run();

    kinveyController.showHideMenuLinks();
});