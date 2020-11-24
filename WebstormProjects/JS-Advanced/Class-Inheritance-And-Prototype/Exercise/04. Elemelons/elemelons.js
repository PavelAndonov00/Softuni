function elemelons() {
    class Melon {
        constructor(weight, melonSort) {
            if (new.target === Melon) {
                throw new TypeError("Abstract class cannot be instantiated directly");
            }
            this.weight = weight;
            this.melonSort = melonSort;
        }

        get elementIndex() {
            return this.weight * this.melonSort.length;
        }

        toString() {
            let element = this.constructor.name;
            if(element == "Melolemonmelon"){
                element = this.__proto__;
                if(element instanceof Watermelon){
                    element = "Watermelon";
                } else if(element instanceof Firemelon){
                    element = "Firemelon";
                } else if(element instanceof Earthmelon){
                    element = "Earthmelon";
                } else {
                    element = "Airmelon";
                }
            }
            element = element.slice(0, element.length-5);
            let result = `Element: ${element}\n`;
            result += `Sort: ${this.melonSort}\n`;
            result += `Element Index: ${this.elementIndex}`;

            return result;
        }
    }

    class Watermelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Firemelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Earthmelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Airmelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort);
        }
    }

    class Melolemonmelon extends Watermelon{
        constructor(weight, melonSort) {
            super(weight, melonSort);
            this.elements = [Firemelon, Earthmelon, Airmelon, Watermelon];
        }

        morph() {
            let current = this.elements.shift();
            Object.setPrototypeOf(Melolemonmelon.prototype, current.prototype);
            this.elements.push(current);
        }
    }

    let melolemonmelon = new Melolemonmelon(50, "Medium");
    console.log(melolemonmelon.toString());

    return {
        Melon,
        Watermelon,
        Firemelon,
        Earthmelon,
        Airmelon,
        Melolemonmelon
    }
}

elemelons();



