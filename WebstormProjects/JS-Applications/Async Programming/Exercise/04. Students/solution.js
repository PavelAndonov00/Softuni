// BASE DATA
const baseUrl = 'https://baas.kinvey.com/appdata/kid_BJXTsSi-e/students';
const user = 'guest';
const pass = 'guest';
const base64 = btoa(user + ':' + pass);
const auth = {Authorization: 'Basic ' + base64};
const table = $('#results');

// CODE
getStudents();
$('#addBtn').click(addStudent);


function handleError(Error) {
    console.log(Error);
    alert('err');
}

function getStudents() {
    $.get({
        url: baseUrl,
        headers: auth
    }).then(function (students) {
        // table.find('tbody').empty();
        students = students.slice(0, 11).sort((a, b) => a.ID - b.ID);
        for (let student of students) {
            let tr = $('<tr>').append(
                $('<td>').text(student.ID)
            ).append(
                $('<td>').text(student.FirstName)
            ).append(
                $('<td>').text(student.LastName)
            ).append(
                $('<td>').text(student.FacultyNumber)
            ).append(
                $('<td>').text(student.Grade)
            ).append(
                $('<td>').append(
                    $('<button>').text('Delete').click(function () {
                        $.ajax({
                            method: 'DELETE',
                            url: baseUrl + '/' + student._id,
                            headers: auth
                        }).then(() => {
                            $(this).parent().parent().remove();
                        }).catch(handleError);
                    })
                )
            );

            table.find('tbody').append(tr);
        }
    }).catch(handleError);
}

function addStudent() {
    let data = {
        ID: Number($('#ID').val()),
        FirstName: $('#firstName').val(),
        LastName: $('#lastName').val(),
        FacultyNumber: $('#facultyNumber').val(),
        Grade: Number($('#grade').val())
    };


    $.post({
        url: baseUrl,
        headers: auth,
        contentType: 'application/json',
        data: JSON.stringify(data)
    }).then(function () {
        $('#ID').val('');
        $('#firstName').val('');
        $('#lastName').val('');
        $('#facultyNumber').val('');
        $('#grade').val('');
        getStudents();
    }).catch(handleError)
}