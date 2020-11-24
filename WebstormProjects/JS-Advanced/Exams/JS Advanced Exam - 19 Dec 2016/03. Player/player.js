class Player {
    constructor(nickName) {
        this.nickName = nickName;
        this.scores = [];
    }

    orderByDesc() {
        this.scores.sort((a, b) => b - a);
    }

    addScore(score) {
        if(isNaN(score) || score === null) return this;
        this.orderByDesc();
        this.scores.push(score);
        return this;
    }

    get scoreCount() {
        this.orderByDesc();
        return this.scores.length;
    }

    get highestScore() {
        this.orderByDesc();
        return this.scores[0];
    }

    get topFiveScore() {
        this.orderByDesc();
        return this.scores.slice(0, 5);
    }

    toString() {
        this.orderByDesc();
        return `${this.nickName}: [${this.scores}]`
    }
}