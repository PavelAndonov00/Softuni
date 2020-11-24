class Rat {
    constructor(name) {
        this.name = name;
        this.unitedRats = [];
    }

    toString() {
        console.log(this.name);
        console.log(this.unitedRats.map(e => "##" + e.name).join("\n"))
    }

    unite(otherRat) {
        let instanceOfRat = otherRat instanceof Rat;
        if(!instanceOfRat) return;
        this.unitedRats.push(otherRat);
    }

    getRats() {
        return this.unitedRats;
    }
}

let rat2 = new Rat("Viktor");
let rat3 = new Rat("Vichi");
let rat4 = "fake rat";

rat2.unite(rat3);

console.log(rat2.getRats());