let expect = require("chai").expect;
let makeList = require("./02. Add Left Right Clear in List");

describe("p02. Tests for Add Left Right Clear in List", function () {
    let list;
    beforeEach(function () {
       list = makeList();
    });

    describe("Initial test", function () {
        it("addLeft exist", function () {
            expect(list.hasOwnProperty("addLeft")).to.be.true;
        });
        it("addLeft is a function", function () {
            expect(typeof list.addLeft).to.be.equal("function");
        });

        it("addRight exist", function () {
            expect(list.hasOwnProperty("addRight")).to.be.true;
        });
        it("addRight is a function", function () {
            expect(typeof list.addRight).to.be.equal("function");
        });

        it("clear exist", function () {
            expect(list.hasOwnProperty("clear")).to.be.true;
        });
        it("clear is a function", function () {
            expect(typeof list.clear).to.be.equal("function");
        });

        it("toString exist", function () {
            expect(list.hasOwnProperty("toString")).to.be.true;
        });
        it("toString is a function", function () {
            expect(typeof list.toString).to.be.equal("function");
        });
    });

    describe("Add Left func tests", function () {
        it("with one item", function () {
            list.addLeft("left");
            expect(list.toString()).to.be.equal("left");
        });

        it('with many items', function () {
            list.addLeft("left");
            list.addLeft("left2");
            list.addLeft("left3");
            expect(list.toString()).to.be.equal("left3, left2, left");
            list.addLeft("item");
            list.addLeft("first");
            expect(list.toString()).to.be.equal("first, item, left3, left2, left");
        });
    });

    describe("Add Right func tests", function () {
        it("with one item", function () {
            list.addRight("right");
            expect(list.toString()).to.be.equal("right");
        });

        it('with many items', function () {
            list.addRight("right");
            list.addRight("right2");
            list.addRight("right3");
            expect(list.toString()).to.be.equal("right, right2, right3");
            list.addRight("last");
            list.addRight("item");
            expect(list.toString()).to.be.equal("right, right2, right3, last, item");
        })
    });

    describe("Clear func tests", function () {
        it("with one item", function () {
            list.addLeft("one");
            list.clear();
            expect(list.toString()).to.be.equal("");
        });

        it("with many item", function () {
            list.addLeft("this");
            list.addRight("is");
            list.addRight("test");
            list.addRight("for");
            list.addRight("clear");
            list.clear();
            expect(list.toString()).to.be.equal("");
        });
    });
});