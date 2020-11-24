let expect = require("chai").expect;
let createList = require("./Add Swap Shift Left Right in List");

describe("Test for Add Swap Shift Left Right in List", function () {
    let list;
    beforeEach(function () {
        list = createList();
    });

    describe("Add func tests", function () {
        it("with one item", function () {
            list.add("Gosho");
            expect(list.toString()).to.be.equal("Gosho");
        });

        it('with many items', function () {
            list.add("Pesho");
            list.add("Nakov");
            expect(list.toString()).to.be.equal("Pesho, Nakov");
            list.add("Kiro");
            list.add("Maria");
            list.add("Raq");
            expect(list.toString()).to.be.equal("Pesho, Nakov, Kiro, Maria, Raq");
        });
    });

    describe("Shift-Left func tests", function () {
        it("with one item in array", function () {
            list.add("one item");
            list.shiftLeft();
            expect(list.toString()).to.be.equal("one item");
        });

        it('with many items in array', function () {
            list.add(1);
            list.add(2);
            list.add(3);
            list.add(4);
            list.add(5);
            list.shiftLeft();
            expect(list.toString()).to.be.equal("2, 3, 4, 5, 1");
            list.add(7);
            list.shiftLeft();
            expect(list.toString()).to.be.equal("3, 4, 5, 1, 7, 2")
        });
    });

    describe("Shift-Right func tests", function () {
        it("with one item in array", function () {
            list.add("one item");
            list.shiftRight();
            expect(list.toString()).to.be.equal("one item");
        });

        it('with many items in array', function () {
            list.add(1);
            list.add(2);
            list.add(3);
            list.add(4);
            list.add(5);
            list.shiftRight();
            expect(list.toString()).to.be.equal("5, 1, 2, 3, 4");
            list.add(7);
            list.shiftRight();
            expect(list.toString()).to.be.equal("7, 5, 1, 2, 3, 4")
        });
    });

    describe("Swap func tests", function () {
        it("with index1 - not an integer", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap("gosho", 2)).to.be.false;
            list.swap("gosho", 2);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");
        });

        it("with index2 - not an integer", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(4, "pesho")).to.be.false;
            list.swap(4, "pesho");
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");
        });

        it("with index1 - negative number", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(-20, 1)).to.be.false;
            list.swap(-20, 1);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");
        });

        it("with index2 - negative number", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(3, -3)).to.be.false;
            list.swap(3, -3);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");
        });

        it("with index1 - number equal to array's length", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(5, 1)).to.be.false;
            list.swap(5, 1);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");
        });

        it("with index2 - number equal to array's length", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(4, 5)).to.be.false;
            list.swap(4, 5);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");
        });

        it("with index1 - number bigger than array's length", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(7, 1)).to.be.false;
            list.swap(7, 1);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");
        });

        it("with index2 - number bigger than array's length", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(2, 9)).to.be.false;
            list.swap(2, 9);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");

        });

        it("with index1 equal to index2", function () {
            list.add("first");
            list.add("second");
            list.add("third");
            list.add("forth");
            list.add(5);
            expect(list.swap(3, 3)).to.be.false;
            list.swap(3, 3);
            expect(list.toString()).to.be.equal("first, second, third, forth, 5");

        });

        it('test with correct values', function () {
            list.add("Any name");
            list.add("Another name");
            list.add("Martin");
            list.add(23);
            list.add(5);
            list.swap(2, 4);
            expect(list.toString()).to.be.equal("Any name, Another name, 5, 23, Martin");
            list.add("item to swap");
            list.swap(1, 5);
            expect(list.toString()).to.be.equal("Any name, item to swap, 5, 23, Martin, Another name");
            list.add("last item");
            list.add("added");
            list.swap(3, 7);
            expect(list.toString()).to.be.equal("Any name, item to swap, 5, added, Martin, Another name, last item, 23");
        });
    });
});