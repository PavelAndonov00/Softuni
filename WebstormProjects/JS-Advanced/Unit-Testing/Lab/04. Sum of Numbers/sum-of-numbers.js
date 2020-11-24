function sumNumbers(arr) {
    let sum = 0;
    for (let num of arr)
        sum += Number(num);
    return sum;
}

let expect = require('chai').expect;
describe("Test summator", function () {

    it('should return 2 for [2]', function () {
        expect(sumNumbers([2])).to.be.equal(2);
    });

    it("should return 7 for [5, 2]", function() {
        expect(sumNumbers([5, 2])).to.be.equal(7);
    });

    it('should return 100 for [1, 52, 42, 5]', function () {
        expect(sumNumbers([1, 52, 42, 5])).to.be.equal(100);
    });

    it('should return 5 for [10, -25, 5, 15]', function () {
        expect(sumNumbers([10, -25, 5, 15])).to.be.equal(5);
    });

    it('should return 0 for empty array', function () {
        expect(sumNumbers([])).to.be.equal(0);
    });

    it('should return NaN for invalid input', function () {
        expect(sumNumbers([3, 5, 'gosho', 6, "pesho"])).to.be.NaN;
    });

    it("Should return ~3.3 for [1.1, 1.1, 1.1]", function () {
        expect(sumNumbers([1.1, 1.1, 1.1])).to.be.closeTo(3.3, 0.01);
    });
});
