let expect = require("chai").expect;
let Calculator = require("./solution");

describe("Test for Calculator", function () {
    let calculator;
    beforeEach(function () {
        calculator = new Calculator();
    });

    it('toString with empty array', function () {
        expect(calculator.toString()).to.be.equal("empty array");
    });

    it('add tests', function () {
        calculator.add("first");
        expect(calculator.toString()).to.be.equal("first");
        calculator.add("2nd");
        calculator.add(3);
        expect(calculator.toString()).to.be.equal("first -> 2nd -> 3");
        calculator.add([-4, 5.5]);
        expect(calculator.toString()).to.be.equal("first -> 2nd -> 3 -> -4,5.5");
        calculator.add(-3.8);
        expect(calculator.toString()).to.be.equal("first -> 2nd -> 3 -> -4,5.5 -> -3.8");
    });

    it('divide with empty array should throw', function () {
        expect(() => calculator.divideNums()).to.throw;
    });

    it('divide with non-empty array', function () {
        calculator.add("first");
        calculator.add("2nd");
        calculator.add(3);
        calculator.add(-4);
        calculator.add(3.5);
        expect(calculator.divideNums()).to.be.equal(-0.21428571428571427);
        expect(calculator.toString()).to.be.equal("-0.21428571428571427");
        calculator.add("first");
        calculator.add("2nd");
        calculator.add(3);
        calculator.add(-4);
        calculator.add(3.5);
        expect(calculator.divideNums()).to.be.equal(0.00510204081632653);
        expect(calculator.toString()).to.be.equal("0.00510204081632653");
    });

    it('divide with 0 item in array', function () {
        calculator.add(1);
        calculator.add(2);
        calculator.add(3);
        calculator.add(0);
        expect(calculator.divideNums()).to.be.equal('Cannot divide by zero');
    });

    it('orderBy with empty array', function () {
        expect(calculator.orderBy()).to.be.equal("empty");
    });

    it('orderBy with numbers', function () {
        calculator.add(1);
        calculator.add(3.2);
        calculator.add(-2);
        calculator.add(4);
        calculator.add(6);
        calculator.add(5);
        expect(calculator.orderBy()).to.be.equal("-2, 1, 3.2, 4, 5, 6");
        calculator.add(10);
        calculator.add(7);
        calculator.add(-8.5);
        calculator.add(11);
        calculator.add(9);
        calculator.add(1.52);
        expect(calculator.orderBy()).to.be.equal("-8.5, -2, 1, 1.52, 3.2, 4, 5, 6, 7, 9, 10, 11");
    });

    it('orderBy with string in array', function () {
        calculator.add("1st");
        calculator.add(3);
        calculator.add("2nd");
        calculator.add(4.5);
        calculator.add("6th");
        calculator.add(5);
        expect(calculator.orderBy()).to.be.equal("1st, 2nd, 3, 4.5, 5, 6th");
        calculator.add("10th");
        calculator.add(7);
        calculator.add("8th");
        calculator.add(-11);
        calculator.add(9.6);
        calculator.add("12th");
        expect(calculator.orderBy()).to.be.equal("-11, 10th, 12th, 1st, 2nd, 3, 4.5, 5, 6th, 7, 8th, 9.6");
    });

    it('divide with one number', function () {
        calculator.add(1);
        expect(calculator.divideNums()).to.be.equal(1);
        expect(calculator.toString()).to.be.equal("1");
    });
});