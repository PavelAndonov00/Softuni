function attachEvents() {
    $('#btnLoadTowns').click(async function () {
        let towns = $('#towns').val().split(', ').filter(e => e !== "");
        let counter = 0;
        for (let i = 0; i < towns.length; i++) {
            let town = towns[i];
            towns.push({
                name: town
            });

            towns.splice(i, 1);
            i--;
            counter++;
            if(counter === towns.length) break;
        }

        let li = await $.get('./li.hbs');
        let template = Handlebars.compile(li);
        towns = template({towns});

        let root = $('#root');
        root.empty();
        root.append(towns);
    })
}