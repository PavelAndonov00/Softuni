let StringBuilder = require("./string-builder");
let expect = require("chai").expect;

describe("p02. Tests for StringBuilder", function () {
    let str;
    beforeEach(function () {
        str = new StringBuilder("hello")
    });

    describe("General tests", function () {
        it("append exist", function () {
            expect(StringBuilder.prototype.hasOwnProperty("append")).to.be.true;
        });
        it("append is a function", function () {
            expect(typeof StringBuilder.prototype.append).to.be.equal("function");
        });

        it("prepend exist", function () {
            expect(StringBuilder.prototype.hasOwnProperty("prepend")).to.be.true;
        });
        it("prepend is a function", function () {
            expect(typeof StringBuilder.prototype.prepend).to.be.equal("function");
        });

        it("insertAt exist", function () {
            expect(StringBuilder.prototype.hasOwnProperty("insertAt")).to.be.true;
        });
        it("insertAt is a function", function () {
            expect(typeof StringBuilder.prototype.insertAt).to.be.equal("function");
        });

        it("remove exist", function () {
            expect(StringBuilder.prototype.hasOwnProperty("remove")).to.be.true;
        });
        it("remove is a function", function () {
            expect(typeof StringBuilder.prototype.remove).to.be.equal("function");
        });

        it("vrfyParam exist", function () {
            expect(StringBuilder.hasOwnProperty("_vrfyParam")).to.be.true;
        });
        it("vrfyParam is a function", function () {
            expect(typeof StringBuilder._vrfyParam).to.be.equal("function");
        });

        it("toString exist", function () {
            expect(StringBuilder.prototype.hasOwnProperty("toString")).to.be.true;
        });
        it("vrfyParam is a function", function () {
            expect(typeof StringBuilder.prototype.toString).to.be.equal("function");
        });
    });

    describe("Append func tests", function () {
        it("with non-string", function () {
            expect(() => str.append(5)).to.throw;
        });

        it("with string", function () {
            str.append(" SB");
            expect(str.toString()).to.be.equal("hello SB");
        });

        it('with many strings', function () {
            str.append(" string ");
            str.append("builder");
            expect(str.toString()).to.be.equal("hello string builder");
            str.append(" i");
            str.append("'m");
            str.append(" gosho");
            expect(str.toString()).to.be.equal("hello string builder i'm gosho");
        });
    });

    describe("Prepend func tests", function () {
        it("with non-string", function () {
            expect(() => str.prepend("Error")).to.throw;
        });

        it("with string", function () {
            str.prepend("World says ");
            expect(str.toString()).to.be.equal("World says hello");
        });

        it('with many strings', function () {
            str.prepend("builder ");
            str.prepend("string ");
            expect(str.toString()).to.be.equal("string builder hello");
            str.prepend(" gosho ");
            str.prepend("'m");
            str.prepend("i");
            expect(str.toString()).to.be.equal("i'm gosho string builder hello");
        });
    });

    describe("InsertAt func tests", function () {
        it("with non-string", function () {
            expect(() => str.insertAt(420, 4)).to.throw;
        });

        it('with string', function () {
            str.insertAt(" world!", 5);
            expect(str.toString()).to.be.equal("hello world!");
        });

        it('with many strings', function () {
            str.insertAt(" world!", 5);
            str.insertAt("I am string builder. ", 0);
            expect(str.toString()).to.be.equal("I am string builder. hello world!");
            str.insertAt("Best ", 21);
            expect(str.toString()).to.be.equal("I am string builder. Best hello world!");
            str.insertAt("of all", 26);
            str.insertAt(" string builders! ", 32);
            expect(str.toString()).to.be.equal("I am string builder. Best of all string builders! hello world!");
        });
    });

    describe("Remove func tests", function () {
        it("with correct values", function () {
            str.append(" string ");
            str.append("builder");
            str.append(" i");
            str.append("'m");
            str.append(" gosho");
            str.remove(0, 5);
            str.prepend("Hello");
            expect(str.toString()).to.be.equal("Hello string builder i'm gosho");
            str.remove(0, 6);
            expect(str.toString()).to.be.equal("string builder i'm gosho");
            str.remove(7, 8);
            expect(str.toString()).to.be.equal("string i'm gosho");
            str.remove(0, 200);
            expect(str.toString()).to.be.equal("");
        });
    });
    describe("Edge cases", function () {
        it("should create new SB without param", function () {
            expect(() => new StringBuilder()).to.not.throw;
        });
    });
});