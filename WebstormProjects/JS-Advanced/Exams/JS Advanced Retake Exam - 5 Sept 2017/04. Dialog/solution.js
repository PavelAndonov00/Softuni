class Dialog {
    constructor(textMessage, callback) {
        this.textMessage = textMessage;
        this.callback = callback;

        this.dialog = null;
        this.inputs = [];
    }

    addInput(labelText, name, type) {
        this.inputs.push({
            labelText,
            name,
            type
        });
    }

    generate() {
        let overlayDiv = $("<div class='overlay'></div>");
        let dialogDiv = $("<div class='dialog'></div>");
        let p = $(`<p>${this.textMessage}</p>`);

        dialogDiv.append(p);
        if(this.inputs.length > 0) {
            for (let input of this.inputs) {

                let label = $(`<label>${input.labelText}</label>`);
                dialogDiv.append(label);

                let inputBox = $(`<input name="${input.name}" type="${input.type}">`);
                dialogDiv.append(inputBox);
            }
        }

        let okBtn = $("<button>OK</button>");
        okBtn.click(() => {
            let inputs = $("input");
            $(overlayDiv).remove();

            let result = {};
            for (let input of inputs) {
                let name = $(input).attr("name");
                let value = $(input).val();

                result[name] = value;
            }
            this.callback(result);
        });

        let cancelBtn = $("<button>Cancel</button>");
        cancelBtn.click(() => {
            $(overlayDiv).remove();
        });

        dialogDiv.append(okBtn);
        dialogDiv.append(cancelBtn);

        overlayDiv.append(dialogDiv);

        return overlayDiv;
    }

    render() {
        this.dialog = this.generate();
        $(document.body).append(this.dialog);
    }

}