function rgbToHexColor(red, green, blue) {
    if (!Number.isInteger(red) || (red < 0) || (red > 255))
        return undefined; // Red value is invalid
    if (!Number.isInteger(green) || (green < 0) || (green > 255))
        return undefined; // Green value is invalid
    if (!Number.isInteger(blue) || (blue < 0) || (blue > 255))
        return undefined; // Blue value is invalid
    return "#" +
        ("0" + red.toString(16).toUpperCase()).slice(-2) +
        ("0" + green.toString(16).toUpperCase()).slice(-2) +
        ("0" + blue.toString(16).toUpperCase()).slice(-2);
}

let expect = require('chai').expect;

describe('All Tests', function () {

    describe('Invalid inputs', function () {

        describe("Different from numbers", function () {

            it('should return undefined for "red", 5, 4', function () {
                expect(rgbToHexColor("red", 5, 4)).to.be.equal(undefined);
            });

            it('should return undefined for 3, {}, 10', function () {
                expect(rgbToHexColor(3, {}, 10)).to.be.equal(undefined);
            });

            it('should return undefined for 12, 15, []', function () {
                expect(rgbToHexColor(12, 15, [])).to.be.equal(undefined);
            });

        });

        describe("Greater than 255", function () {

            it('should return undefined for 256, 15, 20', function () {
                expect(rgbToHexColor(256, 15, 20)).to.be.equal(undefined);
            });

            it('should return undefined for 13, 35, 256', function () {
                expect(rgbToHexColor(13, 35, 256)).to.be.equal(undefined);
            });

            it('should return undefined for 42, 256, 55', function () {
                expect(rgbToHexColor(42, 256, 55)).to.be.equal(undefined);
            });

        });

        describe("Lower than 0", function () {
            it('should return undefined for -25, 33, 78', function () {
                expect(rgbToHexColor(-25, 33, 78)).to.be.equal(undefined);
            });

            it('should return undefined for 56, -38, 65', function () {
                expect(rgbToHexColor(56, -38, 65)).to.be.equal(undefined);
            });

            it('should return undefined for 2, 99, -120', function () {
                expect(rgbToHexColor(2, 99, -120)).to.be.equal(undefined);
            });
        });

    });

    describe('Correct inputs', function () {

        it('should return #000000 for 0, 0, 0', function () {
            expect(rgbToHexColor(0, 0, 0)).to.be.equal("#000000");
        });

        it('should return #FFFFFF for 255, 255, 255', function () {
            expect(rgbToHexColor(255, 255, 255)).to.be.equal("#FFFFFF");
        });

        it('should return #C83757 for 200, 55, 87', function () {
            expect(rgbToHexColor(200, 55, 87)).to.be.equal("#C83757");
        });

        it('should return #8A0308 for 138, 3, 8', function () {
            expect(rgbToHexColor(138, 3, 8)).to.be.equal("#8A0308");
        });

    });

});