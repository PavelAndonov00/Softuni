class MailBox {
    constructor() {
        this.messages = [];
    }

    addMessage(subject, text) {
        let obj = {
            subject,
            text
        };

        this.messages.push(obj);
        return this;
    }

    get messageCount() {
        return this.messages.length;
    }

    deleteAllMessages() {
        this.messages = [];
    }

    findBySubject(substr) {
        let result = [];
        for (let mailBox of this.messages) {
            let subj = mailBox.subject;
            if (subj.indexOf(substr) > -1) {
                result.push(mailBox);
            }
        }

        return result;
    }

    toString() {
        if (this.messages.length === 0) {
            return " * (empty mailbox)";
        } else {
            return this.messages.map(e => ` * [${e.subject}] ${e.text}`);
        }
    }
}
