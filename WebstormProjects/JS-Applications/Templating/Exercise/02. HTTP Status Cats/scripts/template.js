async function renderCatTemplate() {
    await (async function f() {
        let cats = window.cats;
        let html = await $.get('./catStatus.hbs');
        let template = Handlebars.compile(html);
        let result = template({cats});
        $('#allCats').append(result);
    })();


    $('.btn').click(async function () {
        let div = $(this).parent().find('div');
        console.log(div);

        if(div.css('display') === 'none') {
            div.show();
            $(this).text('Hide status code')
        } else {
            div.hide();
            $(this).text('Show status code')
        }
    });
}