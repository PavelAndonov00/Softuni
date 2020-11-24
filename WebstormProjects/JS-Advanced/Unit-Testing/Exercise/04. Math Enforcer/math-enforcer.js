let mathEnforcer = {
    addFive: function (num) {
        if (typeof(num) !== 'number') {
            return undefined;
        }
        return num + 5;
    },
    subtractTen: function (num) {
        if (typeof(num) !== 'number') {
            return undefined;
        }
        return num - 10;
    },
    sum: function (num1, num2) {
        if (typeof(num1) !== 'number' || typeof(num2) !== 'number') {
            return undefined;
        }
        return num1 + num2;
    }
};

let expect = require("chai").expect;

describe("Tests for mathEnforce", function () {
    it("should be an object", function () {
        expect(Object.prototype.toString.call(mathEnforcer)).to.equal('[object Object]');
    });

    describe("Tests for addFive function", function () {
        it('should return undefined for not a number parameter', function () {
            expect(mathEnforcer.addFive("")).to.be.undefined;
        });

        it('should return 10 for addFive with parameter 5', function () {
            expect(mathEnforcer.addFive(5)).to.be.equal(10);
        });

        it("should return undefined if the parameter is not a number", function () {
            expect(mathEnforcer.addFive([1, 2, 3])).to.equal(undefined);
        });

        it('should return 5 for addFive with parameter 0', function () {
            expect(mathEnforcer.addFive(0)).to.be.equal(5);
        });

        it('should return 0 for addFive with parameter -5', function () {
            expect(mathEnforcer.addFive(-5)).to.be.equal(0);
        });
        it('should return 6.5 for addFive with parameter 1.5', function () {
            expect(mathEnforcer.addFive(1.5)).to.be.equal(6.5);
        });
    });

    describe("Test for subtractTen function", function () {
        it('should return undefined for not a number parameter', function () {
            expect(mathEnforcer.subtractTen({})).to.be.undefined;
        });

        it('should return -5 for subtractTen with parameter 5', function () {
            expect(mathEnforcer.subtractTen(5)).to.be.equal(-5);
        });

        it('should return -10 for subtractTen with parameter 0', function () {
            expect(mathEnforcer.subtractTen(0)).to.be.equal(-10);
        });

        it('should return -15 for subtractTen with parameter -5', function () {
            expect(mathEnforcer.subtractTen(-5)).to.be.equal(-15);
        });

        it('should return -8.5 for subtractTen with parameter 1.5', function () {
            expect(mathEnforcer.subtractTen(1.5)).to.be.equal(-8.5);
        });
    });

    describe("Test for sum function", function () {
        it("should return undefined if 1st parameter is not a number", function () {
            expect(mathEnforcer.sum([], 2)).to.be.undefined;
        });

        it("should return undefined if 2nd parameter is not a number", function () {
            expect(mathEnforcer.sum(5, new Date())).to.be.undefined;
        });

        it('should return 15 for sum of 5 and 10', function () {
            expect(mathEnforcer.sum(5, 10)).to.be.equal(15);
        });

        it('should return 5 for sum of 10 and -5', function () {
            expect(mathEnforcer.sum(10, -5)).to.be.equal(5);
        });

        it('should return 3.3 for sum of 1.65 and 1.65', function () {
            expect(mathEnforcer.sum(1.65, 1.65)).to.be.equal(3.3);
        });

        it('should return -1 for sum of 5.23 and - 6.23', function () {
            expect(mathEnforcer.sum(5.23, -6.23)).to.be.equal(-1);
        });
    });
    
});
