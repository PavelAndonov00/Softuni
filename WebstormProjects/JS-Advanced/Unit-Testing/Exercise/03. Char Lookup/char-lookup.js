function lookupChar(string, index) {
    if (typeof(string) !== 'string' || !Number.isInteger(index)) {
        return undefined;
    }
    if (string.length <= index || index < 0) {
        return "Incorrect index";
    }

    return string.charAt(index);
}

let expect = require("chai").expect;

describe("Tests for lookupChar (lookupChar())", function () {
    it('should return undefined if the first parameter is not a string', function () {
        expect(lookupChar(3, 5)).to.be.undefined;
    });

    it('should return undefined if the second parameter is not a integer', function () {
        expect(lookupChar("string", "gosho")).to.be.undefined;
    });

    it('should return undefined if the second parameter is a fraction', function () {
        expect(lookupChar("string", 3.45)).to.be.undefined;
    });

    it('should return Incorrect index for passed negative index', function () {
        expect(lookupChar("string", -1)).to.be.equal("Incorrect index");
    });

    it('should return Incorrect index for index that is outside the bounds', function () {
        expect(lookupChar("string", 7)).to.be.equal("Incorrect index");
    });

    it('should return Incorrect index for index that is equal to the length of the passed string', function () {
        expect(lookupChar("string", 6)).to.be.equal("Incorrect index");
    });

    it('should return s for "gosho" and index 2', function () {
        expect(lookupChar("gosho", 2)).to.be.equal("s");
    });

    it('should return s for "pesho" and index 2', function () {
        expect(lookupChar("pesho", 2)).to.be.equal("s");
    });

    it('should return s for "kiro" and index 2', function () {
        expect(lookupChar("kiro", 2)).to.be.equal("r");
    });
});