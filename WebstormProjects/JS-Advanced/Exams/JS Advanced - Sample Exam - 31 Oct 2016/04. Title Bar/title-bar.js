class TitleBar {
    constructor(title) {
        this.title = title;
        this.links = [];
    }


    addLink(href, name) {
        let a = $("<a>");
        a.attr("class", "menu-link");
        a.attr("href", href);
        a.text(name);

        this.links.push(a);
    }

    appendTo(selector) {
        let header = $("<header>");
        header.attr("class", "header");

        let headerRow = $("<div>");
        headerRow.attr("class", "header-row");

        let aButton = $("<a>");
        aButton.attr("class", "button");
        aButton.text("&#9776;");

        let titleSpan = $("<span>");
        titleSpan.attr("class", "title");
        titleSpan.text(this.title);

        let drawer = $("<div>");
        drawer.attr("class", "drawer");

        aButton.click(function () {
            if ($(drawer).css('display') === 'none') {
                $(drawer).css('display', 'block');
            } else {
                $(drawer).css('display', 'none');
            }
        });

        let navMenu = $("<nav>");
        navMenu.attr("class", "menu");

        for (let link of this.links) {
            navMenu.append(link);
        }

        headerRow.append(aButton);
        headerRow.append(titleSpan);
        header.append(headerRow);

        drawer.append(navMenu);
        header.append(drawer);

        $(selector).append(header);
    }
}
