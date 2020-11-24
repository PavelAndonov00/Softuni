let expect = require("chai").expect;
let mocha = require("mocha");
let SortedList = require("./sortedList");

describe("p02. Tests for sortedList", function () {
    let list;
    beforeEach(function () {
        list = new SortedList();
    });

    describe("General tests", function () {
        it("add exist", function () {
            expect(SortedList.prototype.hasOwnProperty("add")).to.be.true;
        });

        it("remove exist", function () {
            expect(SortedList.prototype.hasOwnProperty("remove")).to.be.true;
        });

        it("get exist", function () {
            expect(SortedList.prototype.hasOwnProperty("get")).to.be.true;
        });

        it("size exist", function () {
            expect(SortedList.prototype.hasOwnProperty("size")).to.be.true;
        });

        it('size is not a function ', function () {
            expect(typeof list.size).to.not.equal('function', "Property size should not be a function.");
        });

    });

    describe("Add func tests", function () {
        it("with one param", function () {
            list.add(5);
            expect(list.list.join(", ")).to.be.equal(5, "The function don't add correctly!");
        });

        it('with many params', function () {
            list.add(25);
            list.add(15);
            list.add(5);
            expect(list.list.join(", ")).to.be.equal("5, 15, 25", "The function don't add correctly!");
        });
    });

    describe("Remove func tests", function () {
        it("with negative index", function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(() => list.remove(-5)).throw(Error, "Index was outside the bounds of the collection.");
        });

        it("with index equal to the array's length", function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(() => list.remove(5)).throw(Error, "Index was outside the bounds of the collection.");
        });

        it("with index bigger than the array's length", function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(() => list.remove(6)).throw(Error, "Index was outside the bounds of the collection.");
        });

        it("with empty array", function () {
            expect(() => list.remove(50)).throw(Error, "Collection is empty.");
        });

        it("with valid index remove ones", function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            list.remove(1);
            expect(list.list.join(", ")).to.equal("1, 3, 4, 5, {}");
        });

        it('with valid index remove many times', function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            list.remove(3);
            expect(list.list.join(", ")).to.be.equal("1, 2, 3, 4, {}");
            list.remove(2);
            expect(list.list.join(", ")).to.be.equal("1, 2, 4, {}");
        });
    });

    describe("Get func tests", function () {
        it("with negative index", function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(() => list.get(-5)).throw(Error, "Index was outside the bounds of the collection.");
        });

        it("with index equal to the array's length", function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(() => list.get(5)).throw(Error, "Index was outside the bounds of the collection.");
        });

        it("with index bigger than the array's length", function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(() => list.get(6)).throw(Error, "Index was outside the bounds of the collection.");
        });

        it("with empty array", function () {
            expect(() => list.get(50)).throw(Error, "Collection is empty.");
        });

        it('with valid index get ones', function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(list.get(4)).to.be.equal({});
        });

        it('with valid index get many', function () {
            list.add(5);
            list.add({});
            list.add(3);
            list.add(2);
            list.add(1);
            expect(list.get(2)).to.be.equal(3);
            list.add(22);
            list.add(55);
            expect(list.get(5)).to.be.equal(55);
        });
    });

    describe("size tests", function () {
        it("with empty array", function () {
            expect(list.size).to.be.equal(0);
        });

        it("with non-empty array", function () {
            list.add(300);
            list.add(200);
            list.add(100);
            expect(list.size).to.be.equal(3);
        });
    });
});