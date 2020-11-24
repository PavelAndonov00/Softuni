class Console {

    static get placeholder() {
        return /{\d+}/g;
    }

    static writeLine() {
        let message = arguments[0];
        if (arguments.length === 1) {
            if (typeof (message) === 'object') {
                message = JSON.stringify(message);
                return message;
            }
            else if (typeof(message) === 'string') {
                return message;
            }
        }
        else {
            if (typeof (message) !== 'string') {
                throw new TypeError("No string format given!");
            }
            else {
                let tokens = message.match(this.placeholder).sort(function (a, b) {
                    a = Number(a.substring(1, a.length - 1));
                    b = Number(b.substring(1, b.length - 1));
                    return a - b;
                });
                if (tokens.length !== (arguments.length - 1)) {
                    throw new RangeError("Incorrect amount of parameters given!");
                }
                else {
                    for (let i = 0; i < tokens.length; i++) {
                        let number = Number(tokens[i].substring(1, tokens[i].length - 1));
                        if (number !== i) {
                            throw new RangeError("Incorrect placeholders given!");
                        }
                        else {
                            message = message.replace(tokens[i], arguments[i + 1])
                        }
                    }
                    return message;
                }
            }
        }
    }
}

let expect = require("chai").expect;
let mocha = require("mocha");
let jsdom = require("jsdom-global")();

describe("Tests for C# Console", function () {
    describe("Check writeLine function with invalid inputs", function () {
        it("should return correct string for test with one string param", function () {
            expect(Console.writeLine("Gosho")).to.be.equal("Gosho");
        });

        it('should return correct JSON for test with one object param', function () {
            let resultObject = {name: "Pesho", age: 20, course: "C#"};
            let jsonResult = JSON.stringify(resultObject);
            expect(Console.writeLine(resultObject)).to.be.equal(jsonResult);
        });

        it('should return TypeError for test with multiple params - 1st non-string', function () {
            try {
                Console.writeLine(new Date());
            }
            catch(err) {
                expect(err.name).to.eql("TypeError");
            }
        });

        it('should return RangeError for test with multiple params - greater params than the number of placeholders', function () {
            try {
                Console.writeLine("{0} is {1} in {2}.","Pesho", "student")
            }
            catch(err) {
                expect(err.name).to.eql("RangeError");
            }
        });

        it('should return RangeError for test with multiple params - invalid placeholders"s number', function () {
            try {
                Console.writeLine("{0} is {5} in {2}.","Pesho", "student", "SoftUni")
            }
            catch(err) {
                expect(err.name).to.eql("RangeError");
            }
        });

        it("if the placeholders have indexes not within the parameters range should throw RangeError", () => {
            expect(() => Console.writeLine('The {0} brown {22} jumps over the lazy {1}', 'quick', 'fox', 'dog')).to.throw(RangeError);
        });

        it('should return RangeError for wrong passed number in placeholder', function () {
            let string = "The sum of {0.5} and {1} is {2}";
            let result = "The sum of 3 and 4 is 7";
            try {
                Console.writeLine(string, 3, 4, 7)
            } catch (ex) {
                expect(ex.name).to.be.equal("RangeError");
            }
        });
    });

    describe("Check writeLine function with invalid inputs", function () {
        it("should return 'Kiro is student in softuni with C# technology.'", function () {
            let inputString = '{0} is {1} in {2} with {3} technology.';
            let result = 'Kiro is student in softuni with C# technology.';
            expect(Console.writeLine(inputString, "Kiro", "student", "softuni", "C#"))
                .to.be.equal(result);
        });

        it('should return "Maria is 21 years old bulgarian woman. She wants to become successful ASP.NET developer."', function () {
            let inputString = "{2} is 21 {5} old {3} {1}. She {4} to {7} {0} ASP.NET {6}.";
            let result = "Maria is 21 years old bulgarian woman. She wants to become successful ASP.NET developer.";
            expect
            (Console.writeLine(inputString, "successful", "woman", "Maria", "bulgarian", "wants", "years", "developer", "become"))
                .to.be.equal(result);
        });

        it("should support more than 10 placeholders and parameters passed", () => {
            expect(Console.writeLine('{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10}!'
                ,'This', 'task', 'is', 'so', 'annoying', 'its', 'not', 'cool', 'at', 'all', 'mate'))
                .to.equal('This task is so annoying its not cool at all mate!');
        });

        it("should exchange all placeholders with the correct parameters from different types", () => {
            expect(Console.writeLine('Why task {0} has a {1} when its so easy?', 5, 'star'))
                .to.equal('Why task 5 has a star when its so easy?');
        });
    });
});
