let expect = require("chai").expect;
let Sumator = require("./sumator-class");

describe("p02. Tests for Sumator class", function () {
    let list;
    beforeEach(function () {
        list = new Sumator();
    });

    describe("General tests", function () {
        it("add exist", function () {
            expect(Sumator.prototype.hasOwnProperty("add")).to.be.true;
        });
        it('add is a function', function () {
            expect(typeof Sumator.prototype.add).to.be.equal("function");
        });

        it("sumNums exist", function () {
            expect(Sumator.prototype.hasOwnProperty("sumNums")).to.be.true;
        });
        it('sumNums is a function', function () {
            expect(typeof Sumator.prototype.sumNums).to.be.equal("function");
        });

        it("removeByFilter exist", function () {
            expect(Sumator.prototype.hasOwnProperty("removeByFilter")).to.be.true;
        });
        it('removeByFilter is a function', function () {
            expect(typeof Sumator.prototype.removeByFilter).to.be.equal("function");
        });

        it("toString exist", function () {
            expect(Sumator.prototype.hasOwnProperty("toString")).to.be.true;
        });
        it('toString is a function', function () {
            expect(typeof Sumator.prototype.toString).to.be.equal("function");
        });
    });

    describe("p02. Tests for add func", function () {
        it("with one item", function () {
            list.add("one item");
            expect(list.toString()).to.be.equal("one item");
        });

        it('with many items', function () {
            list.add("test");
            list.add("with");
            list.add("many");
            expect(list.toString()).to.be.equal("test, with, many");
            list.add("items");
            expect(list.toString()).to.be.equal("test, with, many, items");
        });
    });

    describe("p02. Tests for sumNums func", function () {
        it("with not a number", function () {
            list.add("not");
            list.add("a");
            expect(list.sumNums()).to.be.equal(0);
            list.add("number");
            list.add({});
            expect(list.sumNums()).to.be.equal(0);
        });

        it('with a number', function () {
            list.add(5);
            list.add("+");
            list.add("0");
            expect(list.sumNums()).to.be.equal(5);
        });

        it('with numbers', function () {
            list.add(13);
            list.add(17);
            expect(list.sumNums()).to.be.equal(30);
            list.add(55);
            list.add(120);
            expect(list.sumNums()).to.be.equal(205);
            list.add(295);
            expect(list.sumNums()).to.be.equal(500);
        });
    });

    describe("p02. Tests for removeByFilter", function () {
        it("with even func filter", function () {
            let evenFunc = function(num) {
                if(num % 2 !== 0) {
                    return true;
                }

                return false;
            };
            list.add(5);
            list.add(10);
            list.add(15);
            list.add(20);
            list.add(25);
            list.add(30);
            list.add(35);
            list.add(40);
            list.removeByFilter(evenFunc);
            expect(list.toString()).to.be.equal("10, 20, 30, 40");
        });

        it('with odd func filter', function () {
            let oddFunc = function(num) {
                if(num % 2 === 0) {
                    return true;
                }

                return false;
            };
            list.add(5);
            list.add(10);
            list.add(15);
            list.add(20);
            list.add(25);
            list.add(30);
            list.add(35);
            list.add(40);
            list.removeByFilter(oddFunc);
            expect(list.toString()).to.be.equal("5, 15, 25, 35");
        });

        it('with numbers below 50 func', function () {
            let below50Func = function(num) {
                if(num >= 50) {
                    return true;
                }

                return false;
            };
            list.add(12);
            list.add(22);
            list.add(32);
            list.add(42);
            list.add(50);
            list.add(52);
            list.add(62);
            list.add(72);
            list.add(82);
            list.removeByFilter(below50Func);
            expect(list.toString()).to.be.equal("12, 22, 32, 42");
        });
    });

    describe("ToString func tests", function () {
        it("with empty array", function () {
            expect(list.toString()).to.be.equal("(empty)");
        });
    });
});