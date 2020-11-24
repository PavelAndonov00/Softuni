function solve() {
    class Figure {
        constructor() {
            if(new.target === Figure) {
                throw new Error("It can't make an instance of this class! Try with other.")
            }
        }

        get area() {
            let figureType = this.constructor.name;
            switch (figureType) {
                case "Circle":
                    return Math.PI * this.radius * this.radius;
                case "Rectangle":
                    return this.width * this.height;
            }
        }

        toString(){
            let props = Object.getOwnPropertyNames(this);
            return this.constructor.name + " - " + props.map(e => `${e}: ${this[e]}`).join(", ");
        }

    }

    class Circle extends Figure{
        constructor(radius) {
            super();
            this.radius = radius;
        }
    }

    class Rectangle extends Figure{
        constructor(width, height) {
            super();
            this.width = width;
            this.height = height;
        }
    }

    return {
        Figure,
        Circle,
        Rectangle
    }
}

let classes = solve();
let Figure = classes.Figure;
let Rectangle = classes.Rectangle;
let Circle = classes.Circle;

let r = new Rectangle(3,4);
console.log(r.width);
console.log(r.height);
let c = new Circle(5);
console.log(c.radius);

