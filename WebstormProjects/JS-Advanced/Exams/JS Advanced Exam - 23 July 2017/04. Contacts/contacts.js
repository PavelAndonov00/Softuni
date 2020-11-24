class Contact {
    constructor(firstName, lastName, phone, email) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.phone = phone;
        this.email = email;
        this._online = false;
        this.element = this.createHtml();
    }

    get online() {
        return this._online;
    }

    set online(value) {
        let elem = $(this.element).find(".title");
        if(value) {
            elem.addClass("online");
        } else{
            elem.removeClass("online");
        }

        this._online = value;
    }

    createHtml() {
        let article = $("<article>");

        let titleDiv = $("<div>");
        titleDiv.addClass(`title ${this.id}`);
        titleDiv.text(this.firstName + " " + this.lastName);

        let infoDiv = $("<div>");
        infoDiv.addClass(`info`);
        infoDiv.css("display", "none");

        let toggle = $("<button>");
        toggle.text("&#8505;");
        toggle.click(function () {
            infoDiv.toggle();
        });

        titleDiv.append(toggle);

        let phoneSpan = $("<span>");
        phoneSpan.text(`&phone; ${this.phone}`);

        let emailSpan = $("<span>");
        emailSpan.text(`&#9993;  ${this.email}`);

        infoDiv.append(phoneSpan);
        infoDiv.append(emailSpan);

        article.append(titleDiv);
        article.append(infoDiv);

        return article;
    }

    render(id) {
        $(`#${id}`).append(this.element);
    }
}