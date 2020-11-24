let result = (function () {
    class Textbox {
        constructor(selector, regex) {
            this._elements = $(selector);
            this.value = $(this._elements[0]).val();
            this._invalidSymbols = regex;
            this.onInput();
        }

        get elements() {
            return this._elements;
        }

        get value() {
            return this._value;
        }

        set value(value) {
            this._value = value;

            for (let element of this.elements) {
                $(element).val(value);
            }
        }

        onInput() {
            this.elements.on('input', (event) => {
                this.value = $(event.target).val();
            });
        }

        isValid() {
            return !this._invalidSymbols.test(this.value);
        }
    }

    class Form {
        constructor() {
            this._element = $("<div>").addClass("form");
            this.textboxes = arguments;
        }

        get textboxes() {
            return this._textboxes;
        }

        set textboxes(args) {
            for (let arg of args) {
                if (!arg instanceof Textbox) {
                    throw new Error("Invalid instance!")
                }
            }

            this._textboxes = args;

            for (let textbox of args) {
                for (let input of textbox.elements) {
                    $(this._element).append(input);
                }
            }
        }

        submit() {
            let allValid = true;
            for (let textbox of this.textboxes) {
                if (textbox.isValid()) {
                    for (let element of textbox.elements) {
                        $(element).css("border", "2px solid green");
                    }
                } else{
                    for (let element of textbox.elements) {
                        $(element).css("border", "2px solid red");
                    }

                    allValid = false;
                }
            }

            return allValid;
        }

        attach(selector) {
            $(selector).append(this._element);
        }
    }

    return {
        Textbox,
        Form
    }
}())


let Textbox = result.Textbox;
let Form = result.Form;

let username = new Textbox("#username", /[^a-zA-Z0-9]/);
let password = new Textbox("#password", /[^a-zA-Z]/);
let usernameTextbox = $('#username');
let passwordTextbox = $('#password');
username.value = 'Pesho007';
expect(usernameTextbox.val()).to.equal('Pesho007', "Username input value did not change.");
password.value = 'parola12';
expect(passwordTextbox.val()).to.equal('parola12', "Password input value did not change.");
expect(username.isValid()).to.be.true;
expect(password.isValid()).to.be.false;
let form = new Form(username, password);

