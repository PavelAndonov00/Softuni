const shop = (() => {
    function getProducts() {
        return requester.get('appdata','products');
    }

    async function putProductInCart(userId, productId, name, description, price) {
        let user = await requester.get('user', userId);
        let cart = user.cart;

        let currentProductId;
        if(cart !== undefined) {
            currentProductId = Object.keys(cart).filter(e => e === productId)[0];
         } else {
            currentProductId = undefined;
            user.cart = {};
            cart = user.cart;
        }

        console.log(user);

        let quantity;
        if(currentProductId !== undefined) {
            quantity = Number(cart[currentProductId].quantity) + 1;
        } else {
            quantity = 1;
        }


        cart[productId] = {
            quantity,
            product: {
                name,
                description,
                price
            }
        };

        console.log(user);

        return requester.put('user', userId, '', user);
    }

    function getPurchasedProducts(userId) {
        return requester.get('user', userId);
    }

    function discardProduct(data, userId) {
        return requester.put('user', userId, '', data);
    }

    return {
        getProducts,
        putProductInCart,
        getPurchasedProducts,
        discardProduct
    }
})();