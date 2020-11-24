function printDeckOfCards(arr) {
    const validFaces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J',
        'Q', 'K', 'A'];
    const validSuits = {S: '\u2660', H: "\u2665", D: "\u2666", C: "\u2663"};

    let result = [];
    for (let arrElement of arr) {
        let suit = arrElement.substr(-1,1);
        let face = arrElement.substring(0, arrElement.length-1);
        if(!validSuits.hasOwnProperty(suit) || !validFaces.includes(face)){
            console.log(`Invalid card: ${arrElement}`);
            return;
        }

        result.push("" + makeCard(face,suit));
    }

    console.log(result.join(" "));

    function makeCard(face, suit) {
        if (!validFaces.includes(face)) {
            throw new Error("Invalid card face: " + face);
        }
        if (!validSuits.hasOwnProperty(suit)) {
            throw new Error("Invalid card suit: " + suit);
        }

        suit = validSuits[suit];
        return {
            face,
            suit,
            toString: function () {
                return face + suit;
            }
        }
    }

}

console.log(printDeckOfCards(['AS', '10D', 'KH', '2C']));