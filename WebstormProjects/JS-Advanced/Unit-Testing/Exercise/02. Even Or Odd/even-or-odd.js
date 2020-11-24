function isOddOrEven(string) {
    if (typeof(string) !== 'string') {
        return undefined;
    }
    if (string.length % 2 === 0) {
        return "even";
    }

    return "odd";
}


let expect = require("chai").expect;

describe("Tests for Odd or Even (isOddOrEven())", function () {
    
    describe("All tests",function () {
        
        it('should return undefined for input diff from string', function () {
            expect(isOddOrEven(3)).to.be.undefined;
        });

        it('should return odd for "odd"', function () {
            expect(isOddOrEven("odd")).to.be.equal("odd");
        });

        it('should return even for "even"', function () {
            expect(isOddOrEven("even")).to.be.equal("even");
        });

    });
    
});