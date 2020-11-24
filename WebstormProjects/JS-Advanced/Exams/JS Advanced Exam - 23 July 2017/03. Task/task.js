class Task {
    constructor(title, deadline) {
        this.title = title;
        this.status = "Open";
        this.deadline = deadline;
    }

    get deadline() {
        return this._deadline;
    }

    set deadline(value) {
        if (value < Date.now()) {
            throw new Error("Past date can't be passed as deadline!")
        }
        this._deadline = value;
    }

    get isOverdue() {
        if (this.deadline < Date.now() && this.status !== "Completed") {
            return true;
        }

        return false;
    }

    toString() {
        let statusObj = {
            "Open": "\u2731",
            "In Progress": "\u219D"
        };
        if (this.isOverdue && this.status !== "Complete") {
            return `[${"\u26A0"}] ${this.title} (overdue)`;
        } else if (this.status === "Complete") {
            return `[${"\u2714"}] ${this.title}`;
        }

        return `[${statusObj[this.status]}] ${this.title} (deadline: ${this.deadline})`;
    }

    static comparator(taskA, taskB) {
        if(taskA.status === taskB.status) {
            return taskA.deadline > taskB.deadline;
        }

        if((taskA.isOverdue && taskA.status !== "Complete") && (taskB.isOverdue && taskB.status !== "Complete")) {
            return 0;
        }

        if((taskA.isOverdue && taskA.status !== "Complete") && !taskB.isOverdue) {
            return -1;
        }

        if((taskB.isOverdue && taskB.status !== "Complete") && !taskA.isOverdue) {
            return 1;
        }

        if(taskA.status === "In Progress" && taskB.status !== "In Progress") {
            return -1;
        }

        if(taskB.status === "In Progress" && taskA.status !== "In Progress") {
            return 1;
        }

        if(taskA.status === "Open" && taskB.status === "result") {
            return -1;
        }

        if(taskB.status === "Open" && taskA.status === "result") {
            return 1;
        }
    }
}