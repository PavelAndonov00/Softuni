class Stringer {
    constructor(innerString, innerLength) {
        this.innerString = innerString;
        this.innerLength = innerLength;
        this.firstStateOfinnerString = this.innerString;
    }

    toString() {
        if(this._innerLength >= this.firstStateOfinnerString.length) {
            this._innerString = this.firstStateOfinnerString;
        } else if(this._innerLength < this.firstStateOfinnerString.length){
            this.innerString = this.firstStateOfinnerString.substring(0, this._innerLength) + "...";
        }

        return this.innerString;
    }

    increase(value) {
        this._innerLength += value;
    }

    decrease(value) {
        this._innerLength -= value;

        if(this._innerLength < 0){
            this._innerLength = 0;
        }
    }

    get innerString() {
        return this._innerString;
    }
    set innerString(value) {
        this._innerString = value;
    }

    get innerLength() {
        return this._innerLength;
    }
    set innerLength(value) {
        this._innerLength = value;

        if(this._innerLength < 0){
            this._innerLength = 0;
        }
    }

}

let test = new Stringer("Test", 5);
console.log(test.toString()); //Test

test.decrease(3);
console.log(test.toString()); //Te...

test.decrease(5);
console.log(test.toString()); //...

test.increase(4);
console.log(test.toString()); //Test
