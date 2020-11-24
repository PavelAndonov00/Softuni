class PaymentProcessor {
    constructor(options) {
        this.options = {
            types: ["service", "product", "other"],
            precision: 2
        };

        this.payments = new Map();

        if (options !== undefined) {
            if (options.hasOwnProperty("types")) {
                this.options.types = options.types;
            }

            if (options.hasOwnProperty("precision")) {
                this.options.precision = options.precision;
            }
        }
    }

    registerPayment(id, name, type, value) {
        if (id == "") {
            throw new Error("Id cannot be empty string!");
        }

        if (name == "") {
            throw new Error("Name cannot be empty string!");
        }

        if (typeof id !== "string") {
            throw new Error("Id should be a string!");
        }

        if (typeof name !== "string") {
            throw new Error("Name should be a string!");
        }

        let validTypes = this.options.types;
        if (!validTypes.includes(type)) {
            throw new TypeError("Invalid type param!")
        }

        if(this.payments.has(id)) {
            throw new Error("There is object with same id!")
        }

        this.payments.set(id, {
            id,
            name,
            type,
            value
        });
    }

    deletePayment(id) {
        if (!this.payments.has(id)) {
            throw new Error("Id is not found!");
        }

        this.payments.delete(id);
    }

    get(id) {
        if (!this.payments.has(id)) {
            throw new Error("Id is not found!");
        }

        let precision = this.options.precision;

        let current = this.payments.get(id);
        let result = `Details about payment ID: ${current.id}\n` +
            `- Name: ${current.name}\n` +
            `- Type: ${current.type}\n` +
            `- Value: ${current.value.toFixed(precision)}`;

        return result;
    }

    setOptions(options) {
        if (options.types !== undefined) {
            this.options.types = options.types;
        }

        if (options.precision !== undefined) {
            this.options.precision = options.precision;
        }
    }

    toString() {
        let result = `Summary:\n` +
            `- Payments: ${this.payments.size}\n`;

        let balance = 0;
        for (let [key, value] of this.payments) {
            balance += value.value;
        }

        let precision = this.options.precision;
        result +=  `- Balance: ${balance.toFixed(precision)}`;

        return result;
    }
}