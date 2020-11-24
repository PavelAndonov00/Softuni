function startApp() {
    if (sessionStorage.getItem('authtoken') !== null) {
        window.location = '#/catalog';
    } else {
        window.location = '#/welcome';
    }

    $(document).on({
        ajaxStart: function () {
            $('#loadingBox').show();
        },
        ajaxStop: function () {
            $('#loadingBox').hide();
        }
    });

    const app = Sammy('.content', function () {
        this.use('Handlebars', 'handlebars');

        // Define routes
        this.get('#/welcome', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                menu: 'templates/common/menu.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/welcome/welcome.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    user: sessionStorage.getItem('username')
                }).then(function () {
                    $('#btnLogin').click(function () {
                        window.location = '#/submit';
                    });
                    $('#btnRegister').click(function () {
                        window.location = '#/comments';
                    });
                })
            })
        });

        this.post('#/login', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;

            if (username.length >= 3 && /^[A-Za-z]+$/.test(username) && /^[A-Za-z0-9]+$/.test(password)) {
                let loginPromise = auth.login(username, password);

                loginPromise.then(function (res) {
                    let formLogin = $('#loginForm');
                    formLogin.find('input[name=username]').val('');
                    formLogin.find('input[name=password]').val('');
                    auth.showInfo('Login successful.');

                    auth.saveSession(res);

                    ctx.redirect('#/catalog');
                }).catch(function (err) {
                    auth.handleError(err);

                    auth.showError('Invalid credentials. Please retry your request with correct credentials.');
                })
            }
        });

        this.post('#/register', function (ctx) {
            let username = ctx.params.username;
            let password = ctx.params.password;
            let repeatPassword = ctx.params.repeatPass;

            if (username.length >= 3 && /^[A-Za-z]+$/.test(username) &&
                /^[A-Za-z0-9]+$/.test(password) && (password === repeatPassword)) {
                let registerPromise = auth.register(username, password);

                registerPromise.then(function (res) {
                    let formRegister = $('#registerForm');
                    formRegister.find('input[name=username]').val('');
                    formRegister.find('input[name=password]').val('');
                    formRegister.find('input[name=repeatPass]').val('');

                    auth.showInfo('User registration successful.');

                    auth.saveSession(res);

                    ctx.redirect('#/catalog');

                    alert('register')
                }).catch(function (err) {
                    auth.handleError(err);

                    auth.showError('This username is already taken. Please try with other different.');
                })
            }
        });

        this.get('#/logout', function (ctx) {
            let logoutPromise = auth.logout();

            logoutPromise.then(function () {
                auth.showInfo('Logout successful.')
            });

            sessionStorage.clear();

            ctx.redirect('#/welcome');
        });

        this.get('#/catalog', function (ctx) {
            let postsPromise = post.getPosts();

            let result = [];
            postsPromise.then(function (posts) {
                for (let i = 0; i < posts.length; i++) {
                    let post = posts[i];
                    result.push({
                        counter: i + 1,
                        url: post.url,
                        imageUrl: post.imageUrl,
                        title: post.title,
                        date: calcTime(post._kmd.ect),
                        author: post.author,
                        id: post._id,
                    })
                }

                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    menu: 'templates/common/menu.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/catalog/catalog.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        user: sessionStorage.getItem('username'),
                        posts: result
                    }).then(function () {
                        editEvent(ctx);
                        deleteEvent();
                        commentEvent(ctx);
                    })
                });
            });
        });

        this.post('#/edit', function (ctx) {
            let url = ctx.params.url;
            let imageUrl = ctx.params.image;
            let title = ctx.params.title;
            let description = ctx.params.description;
            let id = ctx.params.id;
            let author = sessionStorage.getItem('username');

            let editPromise = post.editPost(author, url, imageUrl, title, description, id);
            editPromise.then(function () {
                auth.showInfo('Post edited.');

                let editForm = $('#editPostForm');

                editForm.find('input[name=url]').val('');
                editForm.find('input[name=image]').val('');
                editForm.find('input[name=title]').val('');
                editForm.find('textarea').val('');

                ctx.redirect('#/myPosts')
            })
        });

        this.get('#/submitLink', function (ctx) {
            ctx.loadPartials({
                header: 'templates/common/header.handlebars',
                menu: 'templates/common/menu.handlebars',
                footer: 'templates/common/footer.handlebars',
            }).then(function () {
                this.partial('templates/submit/submit.handlebars', {
                    isLogged: sessionStorage.getItem('authtoken') !== null,
                    user: sessionStorage.getItem('username')
                })
            })
        });
        this.post('#/submitLink', function (ctx) {
            let url = ctx.params.url;
            let imageUrl = ctx.params.image;
            let title = ctx.params.title;
            let description = ctx.params.comment;
            let author = sessionStorage.getItem('username');

            let createPromise = post.createPost(author, url, imageUrl, title, description);
            createPromise.then(function () {
                auth.showInfo('Post created.');

                ctx.redirect('#/myPosts');

                let submitForm = $('#submitForm');

                submitForm.find('input[name=url]').val('');
                submitForm.find('input[name=image]').val('');
                submitForm.find('input[name=title]').val('');
                submitForm.find('input[name=comment]').val('');

                ctx.redirect('#/myPosts')
            })
        });

        this.get('#/myPosts', function (ctx) {
            let postsPromise = post.getPosts();
            let myPosts = [];
            postsPromise.then(function (posts) {
                for (let i = 0; i < posts.length; i++) {
                    let post = posts[i];
                    if(post.author === sessionStorage.getItem('username')) {
                        myPosts.push({
                            counter: i + 1,
                            url: post.url,
                            imageUrl: post.imageUrl,
                            title: post.title,
                            date: calcTime(post._kmd.ect),
                            author: post.author,
                            id: post._id
                        });
                    }
                }

                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    menu: 'templates/common/menu.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/posts/posts.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        user: sessionStorage.getItem('username'),
                        posts: myPosts
                    }).then((function () {
                        editEvent(ctx);
                        deleteEvent();
                        commentEvent(ctx);
                    }))
                })
            });
        });

        this.post('#/comments', function (ctx) {
            let author = sessionStorage.getItem('username');
            let postId = sessionStorage.getItem('postId');
            let content = ctx.params.content;

            let postPromise = post.postComment(author, postId, content);
            postPromise.then(function (res) {
                let commentForm = $('#commentForm');

                let date = calcTime(res._kmd.ect);
                let html = $(`<article class="post post-content"  data-id="${res._id}">
                                <p>${content}</p>
                                <div class="info">
                                    submitted ${date} ago by ${author} | <a href="#/deleteComment/:${res._id}" class="deleteCom">delete</a>
                                </div>
                                </article>`);

                $('#viewComments').append(html);

                commentForm.find('textarea').val('');

                auth.showInfo('Comment added.')
            })
        });

        this.get('#/deleteComment/:comId', function (ctx) {
            let id = ctx.params.comId;
            let deletePromise = post.deleteCom(id);
            deletePromise.then(function () {
                $(`article[data-id=${id}]`).remove();

                auth.showInfo('Comment deleted.')
            })
        })
    });

    app.run();

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

    function editEvent(ctx) {
        $('.editLink').click(function editPost () {
            let parent = $(this).parent().parent();
            let postId = parent.attr('data-id');

            let postPromise = post.getPost(postId);
            postPromise.then(function (post) {
                ctx.loadPartials({
                    header: 'templates/common/header.handlebars',
                    menu: 'templates/common/menu.handlebars',
                    footer: 'templates/common/footer.handlebars',
                }).then(function () {
                    this.partial('templates/edit/edit.handlebars', {
                        isLogged: sessionStorage.getItem('authtoken') !== null,
                        user: sessionStorage.getItem('username'),
                        id: post._id,
                        url: post.url,
                        imageUrl: post.imageUrl,
                        title: post.title,
                        author: post.author,
                        description: post.description
                    })
                });
            })
        });
    }

    function deleteEvent() {
        $('.deleteLink').click(function deletePost () {
            let parent = $(this).parent().parent();
            let postId = parent.attr('data-id');

            let deletePromise = post.deletePost(postId);
            deletePromise.then(function () {
                auth.showInfo('Post deleted.');

                parent.parent().parent().parent().parent().remove();
            })
        });
    }

    function commentEvent(ctx) {
        $('.commentsLink').click(function commentsOnClick() {
            let parent = $(this).parent().parent();
            let postId = parent.attr('data-id');

            let commentsPromise = post.getComments();
            let currentComments = [];
            commentsPromise.then(function (comments) {
                for (let comment of comments) {
                    if (comment.postId === postId) {
                        currentComments.push({
                            commentDate: calcTime(comment._kmd.ect),
                            commentAuthor: comment.author,
                            content: comment.content,
                            commentId: comment._id
                        });
                    }
                }

                let postPromise = post.getPost(postId);
                postPromise.then(function (currentPost) {
                    ctx.loadPartials({
                        header: 'templates/common/header.handlebars',
                        menu: 'templates/common/menu.handlebars',
                        footer: 'templates/common/footer.handlebars',
                    }).then(function () {
                        this.partial('templates/comments/comments.handlebars', {
                            isLogged: sessionStorage.getItem('authtoken') !== null,
                            user: sessionStorage.getItem('username'),
                            url: currentPost.url,
                            imageUrl: currentPost.imageUrl,
                            title: currentPost.title,
                            author: currentPost.author,
                            description: currentPost.description,
                            date: calcTime(currentPost._kmd.ect),
                            comments: currentComments,
                            postId
                        }).then(function () {
                            deleteEvent();
                            editEvent(ctx);
                        })
                    });
                });
            });

        });
    }
}