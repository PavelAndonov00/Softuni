let result = (function () {
    let Suits = {
        SPADES: '♠',
        HEARTS: '♥',
        DIAMONDS: '♦',
        CLUBS: '♣'
    };

    class Card {
        constructor(face, suit) {
            this.face = face;
            this.suit = suit;
        }

        get face() {
            return this._face;
        }

        set face(value) {
            const validFaces = ["2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];

            if(!validFaces.includes(value)){
                throw new Error("Invalid face!")
            }

            this._face = value;
        }

        get suit() {
            return this._suit;
        }

        set suit(value) {
            const validSuits = ['♠', '♥', '♦', '♣'];

            if(!validSuits.includes(value)) {
                throw new Error("Invalid suit!")
            }

            this._suit = value;
        }
    }

    return {
        Suits,
        Card
    }
})();

let Card = result.Card;
let Suits = result.Suits;

let card = new Card("Q", Suits.CLUBS);
card.face = "A";
card.suit = Suits.DIAMONDS;
let card2 = new Card("2", Suits.DIAMONDS);