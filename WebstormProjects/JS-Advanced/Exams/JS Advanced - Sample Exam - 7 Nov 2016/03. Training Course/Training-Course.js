class TrainingCourse {
    constructor(title, trainer) {
        this.title = title;
        this.trainer = trainer;
        this.topics = [];
    }

    orderByDate() {
        this.topics = this.topics.sort((a, b) => a.date - b.date);
    }

    addTopic(title, date) {
        let obj = {
            title,
            date
        };

        this.topics.push(obj);
        this.orderByDate();
        return this;
    }

    get firstTopic() {
        this.orderByDate();
        return this.topics[0];
    }

    get lastTopic() {
        this.orderByDate();
        return this.topics[this.topics.length - 1];
    }

    toString() {
        this.orderByDate();

        if(this.topics.length === 0) {
            return `Course "${this.title}" by ${this.trainer}`;
        }

        let result = `Course "${this.title}" by ${this.trainer}\n`;
        for (let topic of this.topics) {
            result += ` * ${topic.title} - ${topic.date}\n`
        }

        return result;
    }
}

let test = new TrainingCourse("PHP", "Royal");
test.addTopic('Syntax', new Date(2017, 10, 12, 18, 0));
test.addTopic('Exam prep', new Date(2017, 10, 14, 18, 0));
test.addTopic('Intro', new Date(2017, 10, 10, 18, 0));
console.log(test.toString());