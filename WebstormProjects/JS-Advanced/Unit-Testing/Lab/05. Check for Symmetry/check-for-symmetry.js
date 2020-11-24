isSymmetric = function(arr) {
    if (!Array.isArray(arr))
        return false; // Non-arrays are non-symmetric
    let reversed = arr.slice(0).reverse(); // Clone and reverse
    for(let i = 0;i<arr.length;i++){
        if(arr[i] != reversed[i]){
            return false;
        }
    }
    return true;
};



let expect = require('chai').expect;
describe("Test isSymmetric", function () {

    it('should return function for typeof isSymmetry', function () {
        expect(typeof isSymmetric).to.equal("function");
    });

    it('should return true for ["gosho", "pesho", "kircho", 7, "kircho", "pesho", "gosho"]', function () {
        expect(isSymmetric(["gosho", "pesho", "kircho", 7, "kircho", "pesho", "gosho"])).to.be.true;
    });

    it('should return true for ["gosho", "pesho", "kircho", "kircho", "pesho", "gosho"]', function () {
        expect(isSymmetric(["gosho", "pesho", "kircho", 7, "kircho", "pesho", "gosho"])).to.be.true;
    });

    it('should return false for ["gosho", "peso", "kircho", 7, "kicho", "pesho", "gosho"]', function () {
        expect(isSymmetric(["gosho", "peso", "kircho", 7, "kicho", "pesho", "gosho"])).to.be.false;
    });

    it('should return false for ["gosho", "peso", "kircho", 7, "kicho", "pesho", "gosho"]', function () {
        expect(isSymmetric(["gosho", "peso", "kircho", "kicho", "pesho", "gosho"])).to.be.false;
    });

    it('should return true for []', function () {
        expect(isSymmetric([])).to.be.true;
    });

    it('should return true for [5]', function () {
        expect(isSymmetric([5])).to.be.true;
    });

    it('should return false for non-array', function () {
        expect(isSymmetric("gosho")).to.be.false;
    });

    it("should return true for [5, 'hi', {a:5}, new Date(), {a:5}, 'hi', 5]", function () {
        expect(isSymmetric([5, 'hi', {a:5}, new Date(), {a:5}, 'hi', 5])).to.be.true;
    });

    it("should return false for [5, 'hi', {b:5}, new Date(), {a:5}, 'hi', 5]", function () {
        expect(isSymmetric([5, 'hi', {b:5}, new Date(), {a:5}, 'hi', 5])).to.be.false;
    });

});
