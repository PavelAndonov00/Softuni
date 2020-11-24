class Circle {
    constructor(radius){
        this.diameter = this._radius * 2;
        this.radius = radius;
    }

    get radius() {
        return this._radius;
    }

    set radius(value) {
        this.diameter = value * 2;
        this._radius = value;
    }

    get area () {
        return Math.PI * this._radius ** 2;
    }

    get diameter() {
        return this._diameter;
    }

    set diameter(value) {
        this._radius = value / 2;
        this._diameter = value;
    }

}

let c = new Circle(2);
console.log(`Radius: ${c.radius}`);
console.log(`Diameter: ${c.diameter}`);
console.log(`Area: ${c.area}`);

c.diameter = 1.6;
console.log(`Radius: ${c.radius}`);
console.log(`Diameter: ${c.diameter}`);
console.log(`Area: ${c.area}`);

c.radius = 4;
console.log(`Radius: ${c.radius}`);
console.log(`Diameter: ${c.diameter}`);
console.log(`Area: ${c.area}`);

