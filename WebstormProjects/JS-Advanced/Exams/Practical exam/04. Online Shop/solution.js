function onlineShop(selector) {
    let form = `<div id="header">Online Shop Inventory</div>
    <div class="block">
        <label class="field">Product details:</label>
        <br>
        <input placeholder="Enter product" class="custom-select">
        <input class="input1" id="price" type="number" min="1" max="999999" value="1"><label class="text">BGN</label>
        <input class="input1" id="quantity" type="number" min="1" value="1"><label class="text">Qty.</label>
        <button id="submit" class="button" disabled>Submit</button>
        <br><br>
        <label class="field">Inventory:</label>
        <br>
        <ul class="display">
        </ul>
        <br>
        <label class="field">Capacity:</label><input id="capacity" readonly>
        <label class="field">(maximum capacity is 150 items.)</label>
        <br>
        <label class="field">Price:</label><input id="sum" readonly>
        <label class="field">BGN</label>
    </div>`;
    $(selector).html(form);

    let checkerProduct = false;
    let checkerPrice = false;
    let checkerQuantity = false;

    let submit = $('#submit');

    let product = $('.custom-select');
    product.on("mouseleave", function () {
        if (product.val() !== "") {
            checkerProduct = true;
        }

        if (checkerPrice && checkerProduct && checkerQuantity) {
            submit.removeAttr("disabled");
            checkerProduct = false;
            checkerPrice = false;
            checkerQuantity = false;
        }
    });

    let price = $('#price');
    price.click("mouseleave", function () {
        if (price.val() !== "") {
            checkerPrice = true;
        }

        if (checkerPrice && checkerProduct && checkerQuantity) {
            submit.removeAttr("disabled");
            checkerProduct = false;
            checkerPrice = false;
            checkerQuantity = false;
        }
    });

    let quantity = $('#quantity');
    quantity.click("mouseleave", function () {
        if (quantity.val() !== "") {
            checkerQuantity = true;
        }


        if (checkerPrice && checkerProduct && checkerQuantity) {
            submit.removeAttr("disabled");
            checkerProduct = false;
            checkerPrice = false;
            checkerQuantity = false;
        }
    });


    let totalPrice = 0;
    let totalQuantity = 0;
    submit.on('click', function () {
        let productVal = product.val();
        let priceVal = Number(price.val());
        let quantityVal = Number(quantity.val());

        totalPrice += priceVal;
        totalQuantity += quantityVal;

        let inputQuantity = $('#capacity');
        let inputSum = $('#sum');

        // check for reach max capacity
        console.log(totalQuantity);
        if (totalQuantity >= 150) {
            product.attr("disabled", true);
            price.attr("disabled", true);
            quantity.attr("disabled", true);
            submit.attr("disabled", true);

            inputQuantity
                .val("full")
                .addClass("fullCapacity");
            return;
        } else {
            inputQuantity.val(totalQuantity);
            inputSum.val(totalPrice);
        }

        let li = $('<li>').text("Product: " + productVal + " Price: " + priceVal + " Quantity: " + quantityVal);

        $('.display').append(li);

        product.val("");
        price.val(1);
        quantity.val(1);

        submit.attr("disabled", true)
    })
}
