function phonebook() {
    function loadBtn() {
        $("#btnLoad").on("click", function () {
            $("#phonebook").empty();
            $.ajax({
                method: 'GET',
                url: 'https://tests-398a7.firebaseio.com/phonebook.json',
                success: loadSuccess,
                error: loadError
            });

            function loadSuccess(repos) {
                if(repos[0] === null || repos === null) {
                    repos.splice(0, 1);
                }

                let length = Object.keys(repos).length;
                for (let key in repos) {
                    let repo = repos[key];
                    let li = $('<li>').text(repo.name + ": " + repo.phone + " ");
                    let a = $("<a href='#'>[Delete]</a>").on("click", function () {
                        li.remove();

                        $.ajax({
                            method: "DELETE",
                            url: `https://tests-398a7.firebaseio.com/phonebook/${key}.json`,
                            success: deleteSuccess,
                            error: deleteError
                        });

                        function deleteSuccess() {
                            $("#btnLoad").click();
                        }

                        function deleteError(err) {
                            console.log(err);
                        }
                    });

                    if(length > 1) {
                        li.append(a);
                    }

                    $('#phonebook').append(li);
                }
            }

            function loadError(err) {
                console.log(err);
            }

        });
    }

    function createBtn() {
        $("#btnCreate").on("click", function () {
            let url = `https://tests-398a7.firebaseio.com/phonebook/.json`;

            let inputName = $('#person');
            let inputPhone = $('#phone');

            let name = inputName.val();
            let phone = inputPhone.val();
            if(!name || !phone) return;
            let data = JSON.stringify({name, phone});

            $.ajax({
                method: "POST",
                data: data,
                url: url,
                success: createSuccess,
                error: createError
            });

            inputName.val("");
            inputPhone.val("");

            function createSuccess() {
                $("#btnLoad").click();
            }

            function createError(error) {
                console.log(error);
            }
        })
    }

    loadBtn();
    createBtn();
}