function makeCard(face, suit) {
    const validFaces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J',
        'Q', 'K', 'A'];
    const validSuits = {S: "♠", H: "♥", D: "♦", C: "♣"};
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

console.log('' + makeCard('A', 'S'));
console.log('' + makeCard('10', 'H'));
console.log('' + makeCard('1', 'C'));