class PaymentManager {
    constructor(title) {
        this.title = title;

        $("caption").text(this.title + " Payment Manager");
    }

    render(id) {
        let tableAndCaption = $(`<table><caption>${this.title} Payment Manager</caption></table>`);
        let tHead = $(`<thead><tr><th class="name">Name</th><th class="category">Category</th><th class="price">Price</th><th>Actions</th></tr></thead>`);
        let tBody = $(`<tbody class="payments"></tbody>`);
        let tFoot = $(`<tfoot class="input-data">`);
        let footTr = $(`<tr><td><input name="name" type="text"></td><td><input name="category" type="text"></td><td><input name="price" type="number"></td></tr>`);
        let tdButton = $("<td>");
        let button = $("<button>Add</button>");
        button.on("click", function (event) {
            let currentTable = $(event.target).parent().parent().parent().parent();
            let currentTfoot = currentTable.find("tfoot");
            let inputName = currentTfoot.find('input[name="name"]');
            let inputCategory = currentTfoot.find('input[name="category"]');
            let inputPrice = currentTfoot.find('input[name="price"]');


            let nameVal = inputName.val();
            let categoryVal = inputCategory.val();
            let priceVal = inputPrice.val();

            if (nameVal && categoryVal && priceVal) {
                let tr = $(`<tr><td>${nameVal}</td><td>${categoryVal}</td><td>${Number(priceVal)}</td></tr>`);

                let tdBtn = $("<td>");
                let btn = $("<button>Delete</button>");
                btn.on("click", function (event) {
                    $(event.target).parent().parent().remove();
                });

                tdBtn.append(btn);
                tr.append(tdBtn);

                currentTable.find(".payments").append(tr);

                inputName.val('');
                inputCategory.val('');
                inputPrice.val('');
            }
        });
        tdButton.append(button);
        footTr.append(tdButton);
        tFoot.append(footTr);

        tableAndCaption.append([tHead, tBody, tFoot]);

        $("#" + id).append(tableAndCaption);
    }
}