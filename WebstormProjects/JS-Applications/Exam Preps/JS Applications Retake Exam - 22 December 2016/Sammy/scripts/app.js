function startApp() {
    if (sessionStorage.getItem('authtoken') !== null) {
        window.location = '#/edit';
    } else {
        window.location = '#/welcome';
    }
    ajaxStopAndStartEvent();

    const app = Sammy('#app', function () {
        this.use('Handlebars', 'handlebars');

        // Define routes
        this.get('#/welcome', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/welcome/welcome.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    user: sessionStorage.getItem('username')
                })
            })
        });

        this.get('#/login', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/submit/submit.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    user: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/login', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;

            let loginPromise = auth.login(username, password);

            loginPromise.then(function (res) {
                auth.showInfo('Login successful.');

                auth.saveSession(res);

                ctx.redirect('#/edit');
            }).catch(function (err) {
                auth.handleError(err);

                auth.showError('Invalid credentials. Please retry your request with correct credentials.');
            })
        });

        this.get('#/comments', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/comments/comments.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    user: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/comments', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;
            let name = ctx.params.name;
            let cart = {};

            let registerPromise = auth.register(username, password, name, cart);

            registerPromise.then(function (res) {
                auth.showInfo('User registration successful.');

                auth.saveSession(res);

                ctx.redirect('#/edit');
            }).catch(function (err) {
                auth.handleError(err);

                auth.showError('This username is already taken. Please try with other different.');
            })
        });

        this.get('#/logout', function (ctx) {
            let logoutPromise = auth.logout();

            logoutPromise.then(function () {
                auth.showInfo('Logout successful.')
            });

            sessionStorage.clear();

            ctx.redirect('#/welcome');
        });

        this.get('#/edit', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/edit/welcome.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    user: sessionStorage.getItem('username')
                })
            })
        });

        this.get('#/posts', function (ctx) {
            let productsPromise = shop.getProducts();

            productsPromise.then(function (products) {
                let result = [];
                for (let product of products) {
                    result.push({
                        id: product._id,
                        product: product.name,
                        description: product.description,
                        price: product.price.toFixed(2)
                    })
                }

                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/posts/posts.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        user: sessionStorage.getItem('username'),
                        products: result
                    }).then(function () {
                        $('.purchaseBtn').click(function () {
                            let parent = $(this).parent().parent();
                            let productId = parent.attr('data-id');

                            let product = parent.find('td:first-child').text();
                            let description = parent.find('td:nth-child(2)').text();
                            let price = parent.find('td:nth-child(3)').text();

                            let userId = sessionStorage.getItem('userId');

                            let productPromise = shop.putProductInCart(userId, productId, product, description, price);

                            productPromise.then(function () {
                                auth.showInfo('Purchased successful.');

                                ctx.redirect('#/catalog');
                            })
                        })
                    })
                })
            }).catch(auth.handleError);
        });

        this.get('#/catalog', function (ctx) {
            let userId = sessionStorage.getItem('userId');
            let userPromise = shop.getPurchasedProducts(userId);


            userPromise.then(function (user) {
                let cart = user.cart;

                let result = [];
                for (let productKey in cart) {
                    let productInfo = cart[productKey];

                    let quantity = Number(productInfo.quantity);
                    let totalPrice = Number(productInfo.product.price) * quantity;
                    result.push({
                        id: productKey,
                        product: productInfo.product.name,
                        description: productInfo.product.description,
                        quantity,
                        totalPrice
                    })
                }

                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    footer: 'templates/common/footer.handlebars'
                }).then(function () {
                    this.partial('templates/catalog/catalog.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        user: sessionStorage.getItem('username'),
                        products: result
                    }).then(function () {
                        $('.discardBtn').click(function () {
                            let parent = $(this).parent().parent();
                            let id = parent.attr('data-id');

                            delete cart[id];

                            let discardPromise = shop.discardProduct(user, userId);
                            discardPromise.then(function () {
                                auth.showInfo('Discarded successful.');

                                parent.remove();
                            })
                        })
                    });
                });
            });

        })
    });

    app.run();

    function ajaxStopAndStartEvent() {
        $(document).on({
            ajaxStart: function () {
                $('#loadingBox').show();
            },
            ajaxStop: function () {
                $('#loadingBox').hide();
            }
        })
    }
}