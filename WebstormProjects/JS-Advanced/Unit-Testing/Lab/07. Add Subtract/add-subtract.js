function createCalculator() {
    let value = 0;
    return {
        add: function(num) { value += Number(num); },
        subtract: function(num) { value -= Number(num); },
        get: function() { return value; }
    }
}

let expect = require("chai").expect;

describe("All Tests for createCalculator", function () {
    let calc;
    beforeEach(function () {
        calc = createCalculator();
    });

    describe("Tests for add function", function () {

        it("should be a function", () => {
            expect(typeof createCalculator).to.equal('function');
        });

        it("should return an object", () => {
            expect(typeof calc).to.equal('object');
        });

        it('should return 0 for add 0 twice subtract 0 triple add 0 subtract 0 add 0', function () {
            calc.add(0);
            calc.add(0);
            calc.subtract(0);
            calc.subtract(0);
            calc.subtract(0);
            calc.add(0);
            calc.subtract(0);
            calc.add(0);
            expect(calc.get()).to.be.equal(0);
        });

        it('should return 8 for add -5 subtract -10 add 5 add 3 subtract 5', function () {
            calc.add(-5);
            calc.subtract(-10);
            calc.add(5);
            calc.add(3);
            calc.subtract(5);
            expect(calc.get()).to.be.equal(8);
        });
        it('should return 10 for add 5 twice', function () {
            calc.add(5);
            calc.add(5);
            expect(calc.get()).to.be.equal(10);
        });

        it('should return 2 for add 7 subtract 5', function () {
            calc.add(7);
            calc.subtract(5);
            expect(calc.get()).to.be.equal(2);
        });

        it('should return 5 for add 5 subtract 5 add 5 add 5 subtract 5', function () {
            calc.add(5);
            calc.subtract(5);
            calc.add(5);
            calc.add(5);
            calc.subtract(5);
            expect(calc.get()).to.be.equal(5);
        });

        it('should return -20 for subtract 8, 12', function () {
            calc.subtract(8);
            calc.subtract(12);
            expect(calc.get()).to.be.equal(-20);
        });

        it('should return NaN for subtract 8 add 2 add 12 subtract "gosho"', function () {
            calc.subtract(8);
            calc.add(2);
            calc.add(12);
            calc.subtract("gosho");
            expect(calc.get()).to.be.NaN;
        });

        it('should return NaN for add 3 subtract 50 add {} subtract 255', function () {
            calc.add(3);
            calc.subtract(50);
            calc.add({});
            calc.subtract(255);
            expect(calc.get()).to.be.NaN;
        });
    });

});