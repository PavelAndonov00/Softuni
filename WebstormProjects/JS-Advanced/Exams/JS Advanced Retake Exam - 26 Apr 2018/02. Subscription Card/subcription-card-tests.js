let expect = require("chai").expect;
let SubscriptionCard = require("./subcription-card");

describe("Tests for SubriptionCard", function () {
    describe("First name accessor tests", function () {
        it("test get func", function () {
            let subscriptionCard = new SubscriptionCard("firstName");
            expect(subscriptionCard.firstName).to.be.equal("firstName");
        });

        it('test set func should not change', function () {
            let subscriptionCard = new SubscriptionCard("string");
            subscriptionCard.firstName = "firstName";
            expect(subscriptionCard.firstName).to.be.equal("string");
        });

        it('without value', function () {
            let subscriptionCard = new SubscriptionCard();
            expect(subscriptionCard.firstName).to.be.undefined;
        });
    });

    describe("Last name accessor tests", function () {
        it("test get func", function () {
            let subscriptionCard = new SubscriptionCard("firstName", "lastName");
            expect(subscriptionCard.lastName).to.be.equal("lastName");
        });

        it('test set func should not change', function () {
            let subscriptionCard = new SubscriptionCard("first", "last");
            subscriptionCard.lastName = "lastName";
            expect(subscriptionCard.lastName).to.be.equal("last");
        });

        it('without value', function () {
            let subscriptionCard = new SubscriptionCard();
            expect(subscriptionCard.lastName).to.be.undefined;
        });
    });

    describe("SSN accessor tests", function () {
        it("test get func", function () {
            let subscriptionCard = new SubscriptionCard("firstName", "lastName", "ssn");
            expect(subscriptionCard.SSN).to.be.equal("ssn");
        });

        it('test set func should not change', function () {
            let subscriptionCard = new SubscriptionCard("first", "last", "SSN");
            subscriptionCard.SSN = "ssn";
            expect(subscriptionCard.SSN).to.be.equal("SSN");
        });

        it('without value', function () {
            let subscriptionCard = new SubscriptionCard();
            expect(subscriptionCard.SSN).to.be.undefined;
        });
    });

    describe("isBlocked accessor tests", function () {
        it("test get func", function () {
            let subscriptionCard = new SubscriptionCard();
            expect(subscriptionCard.isBlocked).to.be.equal(false);
        });

        it('test set func should not change', function () {
            let subscriptionCard = new SubscriptionCard();
            subscriptionCard.isBlocked = true;
            expect(subscriptionCard.isBlocked).to.be.false;
        });
    });

    describe("Add subscription accessor tests", function () {
        it('', function () {
            let subscriptionCard = new SubscriptionCard("", "", "");
            expect(subscriptionCard._subscriptions.length).to.be.equal(0);
            expect(subscriptionCard._subscriptions).to.eql([])
        });

        it("with many params", function () {
            let subscriptionCard = new SubscriptionCard();
            subscriptionCard.addSubscription("line", "", "");
            expect(subscriptionCard._subscriptions.length).to.be.equal(1);
            expect(subscriptionCard._subscriptions[0].line).to.be.equal("line");
            expect(subscriptionCard._subscriptions[0].startDate).to.be.equal("");
            expect(subscriptionCard._subscriptions[0].endDate).to.be.equal("");
            subscriptionCard.addSubscription("line2", new Date(2016, 10, 5), new Date(2002, 2, 22));
            subscriptionCard.addSubscription("line3", new Date(208, 8, 28), new Date(1992, 9, 29));
            expect(subscriptionCard._subscriptions.length).to.be.equal(3);
            expect(subscriptionCard._subscriptions[2].line).to.be.equal("line3");
            expect(subscriptionCard._subscriptions[2].startDate).to.be.eql(new Date(208, 8, 28));
            expect(subscriptionCard._subscriptions[2].endDate).to.be.eql(new Date(1992, 9, 29));
        });
    });

    describe("isValid func tests", function () {
        it("with blocked == true", function () {
            let subscriptionCard = new SubscriptionCard();
            subscriptionCard.block();
            expect(subscriptionCard.isValid("line", new Date())).to.be.false;
            subscriptionCard.unblock();
            expect(subscriptionCard.isValid("line", new Date())).to.be.false;
        });

        it('with many subscriptions', function () {
            let subscriptionCard = new SubscriptionCard("isValid", "func", "tests");
            subscriptionCard.addSubscription('120', new Date('2018-04-22'), new Date('2018-05-21'));
            subscriptionCard.addSubscription('*', new Date('2018-05-25'), new Date('2018-06-24'));

            expect(subscriptionCard.isBlocked).to.be.false;
            subscriptionCard.block();
            expect(subscriptionCard.isBlocked).to.be.true;
            subscriptionCard.unblock();
            expect(subscriptionCard.isBlocked).to.be.false;

            expect(subscriptionCard.isValid('120', new Date('2018-04-22'))).to.be.true;
            expect(subscriptionCard.isValid('*', new Date("new date"))).to.be.false;
            expect(subscriptionCard.isValid('120', new Date('2018-06-25'))).to.be.false;
            expect(subscriptionCard.isValid('*', new Date('2018-05-25'))).to.be.true;
            expect(subscriptionCard.isValid('*', new Date('2018-06-22'))).to.be.true;
        });

        it('with invalid params', function () {
            let subscriptionCard = new SubscriptionCard("isValid", "func", "tests");
            subscriptionCard.addSubscription('120', "This is new date", "This is another date");
            subscriptionCard.addSubscription('*', "This is another date", "This is another date");

            expect(subscriptionCard.isValid('120', new Date('2018-04-22'))).to.be.false;
            expect(subscriptionCard.isValid('*', new Date('2018-04-22'))).to.be.false;
        });
    });
});