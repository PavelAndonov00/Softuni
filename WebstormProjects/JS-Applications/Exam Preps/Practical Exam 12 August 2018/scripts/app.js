function startApp() {
    attachEvents();

    Sammy('#container', function () {
        this.use('Handlebars', 'handlebars');

        this.get('#/welcome', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/welcome.handlebars', {
                    logged: sessionStorage.getItem('authtoken'),
                    username: sessionStorage.getItem('username')
                })
            })
        });

        this.get('#/login', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/login.handlebars', {
                    logged: sessionStorage.getItem('authtoken'),
                    username: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/login', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;


            auth.login(username, password).then(function (res) {
                auth.saveSession(res);

                auth.showInfo('Logged in successful.');

                let loginForm = $('#login form');
                loginForm.find('input[name=username]').val('');
                loginForm.find('input[name=password]').val('');

                ctx.redirect('#/allListings')
            }).catch(function () {
                auth.showError('Invalid credentials. Please retry your request with valid credentials.')
            })
        });

        this.get('#/register', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/register.handlebars', {
                    logged: sessionStorage.getItem('authtoken'),
                    username: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/register', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;
            let repeatPass = ctx.params.repeatPass;

            if(username.length < 3 || /^[A-Za-z]+$/g.test(username) === false) {
                auth.showError('The username must be at least 3 characters long and should contains only english alphabet letters.');
                return;
            }

            if(password.length < 6 || /^[A-Za-z0-9]+/g.test(password) === false) {
                auth.showError('Password must be at least 6 characters long and should contains only english alphabet letters and digits.');
                return;
            }

            if(password !== repeatPass) {
                auth.showError('The passwords do not match.');
                return;
            }

            auth.register(username, password).then(function (res) {
                auth.saveSession(res);

                auth.showInfo('Registration successful.');

                ctx.redirect('#/allListings');
            }).catch(function () {
                auth.showError('This username is already taken. Try with different one.');
            })
        });

        this.get('#/logout', function (ctx) {
            auth.logout();

            sessionStorage.clear();

            auth.showInfo('Logout successful.');

            ctx.redirect('#/welcome');
        });

        this.get('#/allListings', function (ctx) {
            service.listAllCars().then(function (cars) {
                const userId = sessionStorage.getItem('userId');
                for (let car of cars) {
                    if(car._acl.creator === userId) {
                        car.isAuthor = true;
                    }
                }

                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/allListings.handlebars', {
                        logged: sessionStorage.getItem('authtoken'),
                        username: sessionStorage.getItem('username'),
                        cars
                    })
                })
            });
        });

        this.get('#/createListing', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/create.handlebars', {
                    logged: sessionStorage.getItem('authtoken'),
                    username: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/createListing', function (ctx) {
            let title = ctx.params.title;
            if(title.length > 33 || title.length === 0) {
                auth.showError('Title must not exceed 33 characters and cannot be empty!');
                return;
            }

            let description = ctx.params.description;
            if(description.length < 30 || description.length > 450) {
                auth.showError('Description length must not exceed 450 characters and should be at least 30!');
                return;
            }

            let brand = ctx.params.brand;
            if(brand.length > 11 || brand.length === 0) {
                auth.showError('Brand length must not exceed 11 characters and cannot be empty!');
                return;
            }

            let model = ctx.params.model;
            if(model.length > 11 || model < 4) {
                auth.showError('Model length must not exceed 11 characters and cannot be empty!');
                return;
            }

            let year = ctx.params.year;
            if(year.length !== 4) {
                auth.showError('Year must be only 4 chars long!');
                return;
            }

            let imageUrl = ctx.params.imageUrl;
            if(imageUrl.startsWith('http') === false) {
                auth.showError('Link url should always start with "http" and cannot be empty.');
                return;
            }

            let fuelType = ctx.params.fuelType;
            if(fuelType.length > 11 || fuelType.length === 0) {
                auth.showError('Fuel type length must not exceed 11 characters and cannot be empty!');
                return;
            }

            let price = ctx.params.price;
            if(price.length > 1000000) {
                auth.showError('The maximum price is 1000000$ and cannot be empty.');
                return;
            }

            service.createCar(brand, description, fuelType, imageUrl, model, price, title, year).then(function () {
                auth.showInfo('Listing created.');

                ctx.redirect('#/allListings')
            })
        });

        this.get('#/editListing/:carId', function (ctx) {
            service.getCarById(ctx.params.carId).then(function (car) {
                let result = [car];
                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/edit.handlebars', {
                        logged: sessionStorage.getItem('authtoken'),
                        username: sessionStorage.getItem('username'),
                        car: result
                    }).then(function () {
                        let editForm = $('#edit-listing');
                         editForm.find('input').on('change', function () {
                                window.location = '#/editListing'
                         });
                    })
                })
            });
        });
        this.post('#/editListing', function (ctx) {
            const carId = ctx.params.carId;

            let title = ctx.params.title;
            if(title.length > 33 || title.length === 0) {
                auth.showError('Title must not exceed 33 characters and cannot be empty!');
                return;
            }

            let description = ctx.params.description;
            if(description.length < 30 || description.length > 450) {
                auth.showError('Description length must not exceed 450 characters and should be at least 30!');
                return;
            }

            let brand = ctx.params.brand;
            if(brand.length > 11 || brand.length === 0) {
                auth.showError('Brand length must not exceed 11 characters and cannot be empty!');
                return;
            }

            let model = ctx.params.model;
            if(model.length > 11 || model < 4) {
                auth.showError('Model length must not exceed 11 characters and cannot be empty!');
                return;
            }

            let year = ctx.params.year;
            if(year.length !== 4) {
                auth.showError('Year must be only 4 chars long!');
                return;
            }

            let imageUrl = ctx.params.imageUrl;
            if(imageUrl.startsWith('http') === false) {
                auth.showError('Link url should always start with "http" and cannot be empty.');
                return;
            }

            let fuelType = ctx.params.fuelType;
            if(fuelType.length > 11 || fuelType.length === 0) {
                auth.showError('Fuel type length must not exceed 11 characters and cannot be empty!');
                return;
            }

            let price = ctx.params.price;
            if(price.length > 1000000) {
                auth.showError('The maximum price is 1000000$ and cannot be empty.');
                return;
            }

            service.editCar(carId, brand, description, fuelType, imageUrl, model, price, title, year)
            .then(function () {
                auth.showInfo('Listing edited.');

                ctx.redirect('#/allListings')
            })
        });

        this.get('#/deleteListing/:carId', function (ctx) {
            let carId = ctx.params.carId;

            service.deleteCar(carId).then(function () {
                auth.showInfo('Listing deleted.');

                ctx.redirect('#/allListings');
            })
        });

        this.get('#/myListings', function (ctx) {
            service.myCarsListings().then(function (myCars) {
                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/myListings.handlebars', {
                        logged: sessionStorage.getItem('authtoken'),
                        username: sessionStorage.getItem('username'),
                        myCars
                    })
                })
            });
        });

        this.get('#/details/:carId', function (ctx) {
            const carId = ctx.params.carId;

            service.getCarById(carId).then(function (car) {
                if(car._acl.creator === sessionStorage.getItem('userId')) {
                    car.isAuthor = true;
                }

                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/details.handlebars', {
                        logged: sessionStorage.getItem('authtoken'),
                        username: sessionStorage.getItem('username'),
                        car: [car]
                    })
                })
            });
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

        if(sessionStorage.getItem('authtoken') === null) {
            window.location = '#/welcome'
        } else {
            window.location = '#/allListings'
        }
    }
}