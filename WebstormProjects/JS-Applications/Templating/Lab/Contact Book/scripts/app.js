$(async function (){
   await (async function () {
        // Get data and template
        let contact = await $.get('./templates/contact.hbs');
        let contacts = await $.get('./data.json');

        // Retrieves only the names from the data
        let contactsRes = [];
        for (let contact of contacts) {
            contactsRes.push({
                name: contact.firstName
            })
        }
        contactsRes = {names: contactsRes};

        // Compile template
        let contactTemplate = Handlebars.compile(contact);
        let result = contactTemplate(contactsRes);

        // Append result
        let contentDiv = $('#list .content');
        contentDiv.empty();
        await contentDiv.append(result);
    })();


    $('.contact').on('click', async function () {
        $('.contact').css('background-color', '');
        $(this).css('background-color', '#D59450');

        // Get the name of clicked div
        let name = $(this).find('.title').text();

        // Get data and template
        let detail = await $.get('./templates/details.hbs');
        let details = await $.get('./data.json');

        // Retrieves only the name that correspond to the clicked div's name
        let detailInfo = details.filter(e => e.firstName === name)[0];

        // Compile template and get the result
        let template = Handlebars.compile(detail);
        let result = template(detailInfo);

        // Empty the details div and append result to it
        let detailsDiv = $('#details');
        detailsDiv.empty();
        detailsDiv.append(result);
    })
});