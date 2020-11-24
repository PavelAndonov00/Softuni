class PublicTransportTable {
    constructor(town) {
        this.town = town;
        $("caption").text(this.town + "'s Public Transport");
    }

    createButton(route, price, driver) {
        let buttonTd = $("<td>");
        let button = $("<button>More Info</button>");
        button.click(function () {
            // Create more info row
            let infoRow = $(`<tr class='more-info'></tr>`);
            let infoTd = $("<td colspan='3'></td>");
            let infoTable = $("<table>");
            let routeTr = $(`<tr><td>Route: ${route}</td></tr>`);
            let priceTr = $(`<tr><td>Price: ${price}</td></tr>`);
            let driverTr = $(`<tr><td>Driver: ${driver}</td></tr>`);

            // Append parts of info row
            infoTable.append([routeTr, priceTr, driverTr]);
            infoTd.append(infoTable);
            infoRow.append(infoTd);

            let btn = $(button);
            if(btn.text() === "Less Info") {
                btn.text("More Info");
            } else{
                btn.text("Less Info");
            }

            let moreInfo =  $(this).parent().parent().next();
            if(!moreInfo.hasClass("more-info")) {
                let parent = $(this).parent().parent();
                $(infoRow).insertAfter(parent);
            } else {
                moreInfo.remove();
            }
        });
        buttonTd.append(button);

        return buttonTd;
    }

    createInfoRow(type, name, buttonTd) {
        // Create info tr
        let infoTr = $("<tr>");

        let typeTd = $("<td>");
        typeTd.text(type);

        let nameTd = $("<td>");
        nameTd.text(name);

        // Append parts of info tr
        infoTr.append([typeTd, nameTd, buttonTd]);

        return infoTr
    }

    addEvents() {
        let typeInput = $("input[name=type]");
        let nameInput = $("input[name=name]");
        $(".search-btn").on("click", () => {
            let typeVal = typeInput.val();
            let nameVal = nameInput.val();
            if(typeVal === "" && nameVal === "") return;

            let trArray = $(".vehicles-info tr").not(".more-info");
            if(typeVal || nameVal) {
                for (let tr of trArray) {
                    let tds = tr.querySelectorAll("td");

                    let typeText = $(tds[0]).text();
                    let nameText = $(tds[1]).text();
                    let btn = $(tds[2]).find("button");
                    if(btn.text() === "Less Info") {
                        btn.click();
                    }
                    if(!typeText.includes(typeVal) || !nameText.includes(nameVal)) {
                        tr.style.display = "none";
                    } else {
                        $(tr).show();
                    }
                }
            }
        });

        $(".clear-btn").click(() => {
            let trArray = $(".vehicles-info tr");
            for (let tr of trArray) {
                $(tr).show();
            }

            typeInput.val("");
            nameInput.val("");
        });
    }

    addVehicle(obj) {
        let type = obj.type;
        let name = obj.name;
        let route = obj.route;
        let price = obj.price;
        let driver = obj.driver;

        let buttonTd = this.createButton(route, price, driver);
        let infoTr = this.createInfoRow(type, name, buttonTd);

        $(".vehicles-info").append(infoTr);

        this.addEvents();
    }
}