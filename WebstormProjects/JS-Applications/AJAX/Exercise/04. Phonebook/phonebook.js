function attachEvents() {
    const baseUrl = 'https://phonebook-nakov.firebaseio.com/phonebook';

    $('#btnLoad').click(function onClick() {
        $('#phonebook').empty();
        $.get({
            url: baseUrl + '.json'
        }).then(function (res) {
            for (let objectKey in res) {
                let obj = res[objectKey];

                let person = obj.person;
                let phone = obj.phone;

                let li = $('<li>').text(person + ': ' + phone + ' ').append(
                    $('<button>').text('[Delete]').click(function () {
                        $.ajax({
                            method: 'DELETE',
                            url: baseUrl + '/' + objectKey + '.json',
                        }).then(() => {
                            $(this).parent().remove();
                        }).catch(handleError);
                    })
                );

                $('#phonebook').append(li);
            }
        }).catch(handleError)
    });


    $('#btnCreate').click(function () {
        let personInput = $('#person');
        let phoneInput = $('#phone');

        if(personInput && phoneInput) {
            let person = personInput.val();
            let phone = phoneInput.val();
            let data = JSON.stringify({
                person,
                phone
            });
            console.log(data);

            $.post({
                url: baseUrl + '/.json',
                data: data
            }).then(function () {
                $('#btnLoad').click();
            }).catch(handleError);


            personInput.val('');
            phoneInput.val('');
        }
    });

    function handleError(err) {
        console.log(err);
    }
}