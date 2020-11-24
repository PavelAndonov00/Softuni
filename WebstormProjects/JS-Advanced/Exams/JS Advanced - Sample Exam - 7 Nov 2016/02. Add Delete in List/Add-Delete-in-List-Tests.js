let expect = require("chai").expect;
let list = require("./Add-Delete-in-List");

describe("Test for Add Delete in List", function () {
    let list;
     beforeEach(function () {
        list = list();
    });

    describe("General tests", function () {
        it("add exist", function () {
            expect(list.hasOwnProperty("add")).to.be.true;
        });
        it('add is a function', function () {
            expect(typeof list.add).to.be.equal("function");
        });

        it('delete exist', function () {
            expect(list.hasOwnProperty("delete")).to.be.true;
        });
        it('delete is a function', function () {
            expect(typeof list.delete).to.be.equal("function");
        });

        it('toString exist', function () {
            expect(list.hasOwnProperty("toString")).to.be.true;
        });
        it('add is a function', function () {
            expect(typeof list.toString).to.be.equal("function");
        });
    });

    describe("Add func tests", function () {
        it('with one item', function () {
            list.add(5);
            expect(list.toString()).to.be.equal("5");
        });

        it('with many params', function () {
            list.add("this");
            list.add("is");
            list.add("the");
            expect(list.toString()).to.be.equal("this, is, the");
            list.add("gosho");
            list.add("and");
            list.add("pesho");
            expect(list.toString()).to.be.equal("this, is, the, gosho, and, pesho");
            list.add("last");
            list.add("item");
            expect(list.toString()).to.be.equal("this, is, the, gosho, and, pesho, last, item");
        });
    });
    describe("Delete func", function () {
        it("with not an integer as index", function () {
            list.add("this");
            list.add("is");
            list.add("the");
            expect(list.delete(3.3)).to.be.undefined;
            expect(list.toString()).to.be.equal("this, is, the");
            list.add("end");
            expect(list.delete("gosho")).to.be.undefined;
            expect(list.toString()).to.be.equal("this, is, the, end");
        });

        it("with negative number as index", function () {
            list.add("this");
            list.add("is");
            list.add("the");
            expect(list.delete(-5)).to.be.undefined;
            expect(list.toString()).to.be.equal("this, is, the");
        });

        it("with number bigger than array length", function () {
            list.add("this");
            list.add("is");
            list.add("the");
            expect(list.delete(4)).to.be.undefined;
            expect(list.toString()).to.be.equal("this, is, the");
        });

        it("with valid index", function () {
            list.add("this");
            list.add("is");
            list.add("not");
            list.add("the");
            expect(list.delete(2)).to.be.equal("not");
            expect(list.toString()).to.be.equal("this, is, the");
            expect(list.delete(0)).to.be.equal("this");
            expect(list.delete(0)).to.be.equal("is");
            expect(list.delete(0)).to.be.equal("the");
            list.add("it");
            list.add("is");
            list.add("4:20");
            list.add("time");
            list.add("not");
            list.add("to smoke");
            expect(list.toString()).to.be.equal("it, is, 4:20, time, not, to smoke");
            expect(list.delete(4)).to.be.equal("not");
            expect(list.toString()).to.be.equal("it, is, 4:20, time, to smoke");
        });
    });
});