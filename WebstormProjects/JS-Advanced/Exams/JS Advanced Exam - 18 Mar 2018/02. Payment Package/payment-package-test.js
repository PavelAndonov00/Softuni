let expect = require("chai").expect;
let PaymentPackage = require("./payment-package");

describe("p02. Tests for PaymentPackage class", function () {
    describe("General tests", function () {
        it("name exist", function () {
            expect(PaymentPackage.prototype.hasOwnProperty("name")).to.be.true;
        });

        it('value exist', function () {
            expect(PaymentPackage.prototype.hasOwnProperty("value")).to.be.true;
        });

        it('VAT exist', function () {
            expect(PaymentPackage.prototype.hasOwnProperty("VAT")).to.be.true;
        });

        it('active exist', function () {
            expect(PaymentPackage.prototype.hasOwnProperty("active")).to.be.true;
        });

        it('toString exist', function () {
            expect(PaymentPackage.prototype.hasOwnProperty("toString")).to.be.true;
        });
        it('toString is a function', function () {
            expect(typeof PaymentPackage.prototype.toString).to.be.equal("function");
        });
    });

    describe("name prop tests", function () {
        it("with empty string", function () {
            expect(() => new PaymentPackage("", 5)).to.throw;
        });

        it("without param", function () {
            expect(() => new PaymentPackage()).to.throw;
        });

        it("with non-string", function () {
            expect(() => new PaymentPackage(5, 5)).to.throw;
        });

        it("with a string", function () {
            expect(() => new PaymentPackage("string", 5)).to.not.throw;
            let payment = new PaymentPackage("string", 5);
            expect(payment.name).to.equal("string");
            payment.name = "another string";
            expect(payment.name).to.equal("another string");

            let expectedOutput = `Package: another string\n` +
                `- Value (excl. VAT): 5\n` +
                `- Value (VAT 20%): 6`;
            expect(payment.toString()).to.equal(expectedOutput);
        });
    });

    describe("value prop tests", function () {
        it("with not a number", function () {
            expect(() => new PaymentPackage("str", "not a number")).to.throw;
        });

        it("without param", function () {
            expect(() => new PaymentPackage("str")).to.throw;
        });

        it("with negative number", function () {
            expect(() => new PaymentPackage("str", -1)).to.throw;
        });

        it("with a number", function () {
            expect(() => new PaymentPackage("str", 22)).to.not.throw;
            let payment = new PaymentPackage("str", 3.5);
            expect(payment.value).to.equal(3.5);
            payment.value = 50;
            expect(payment.value).to.equal(50);

            let expectedOutput = `Package: str\n` +
                `- Value (excl. VAT): 50\n` +
                `- Value (VAT 20%): 60`;
            expect(payment.toString()).to.equal(expectedOutput);
        });
    });

    describe("VAT prop tests", function () {
        it("test default value to be equal to 20", function () {
            let payment = new PaymentPackage("sth", 666);
            expect(payment.VAT).to.be.equal(20);
        });

        it('with not a number', function () {
            let payment = new PaymentPackage("sth", 666);
            expect(() => payment.VAT = "NaN").to.throw;
        });

        it('with negative number', function () {
            let payment = new PaymentPackage("sth", 666);
            expect(() => payment.VAT = -5).to.throw;
        });

        it('with valid number', function () {
            let payment = new PaymentPackage("sth", 666);
            payment.VAT = 30;
            expect(payment.VAT).to.equal(30);
            payment.VAT = 21;
            payment.VAT = 33;
            expect(payment.VAT).to.equal(33);

            let expectedOutput = `Package: sth\n` +
                `- Value (excl. VAT): 666\n` +
                `- Value (VAT 33%): 885.7800000000001`;
            expect(payment.toString()).to.equal(expectedOutput);
        });
    });

    describe("active prop tests", function () {
        it("test default value to be equal to true", function () {
            let payment = new PaymentPackage("sth", 666);
            expect(payment.active).to.be.true;
        });

        it('with value different from boolean', function () {
            let payment = new PaymentPackage("sth", 666);
            expect(() => payment.active = "this is not a boolean").to.throw;
        });

        it('with correct value', function () {
            let payment = new PaymentPackage("sth", 666);
            payment.active = false;
            expect(payment.active).to.equal(false);
            payment.active = true;
            expect(payment.active).to.equal(true);

            let expectedOutput = `Package: sth\n` +
                `- Value (excl. VAT): 666\n` +
                `- Value (VAT 20%): 799.1999999999999`;
            expect(payment.toString()).to.equal(expectedOutput);
        });
    });
});