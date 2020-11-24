class LineManager {
    constructor(busStops) {
        for (let busStop of busStops) {
            if (busStop.name === "" || (typeof busStop.name !== "string")) {
                throw new Error(`Invalid name -> ${busStop.name}!`);
            }

            if (busStop.timeToNext < 0 || (typeof busStop.timeToNext !== "number")) {
                throw new Error(`Invalid time -> ${busStop.timeToNext}!`);
            }
        }

        this.busStops = busStops;
        this.currentStop = 0;
        this.totalTime = 0;
        this.delay = 0;
    }

    get atDepot() {
        return this.currentStop === this.busStops.length - 1;
    }

    get nextStopName() {
        if (this.currentStop === this.busStops.length - 1) {
            return "At depot.";
        }

        let nextStopName = this.busStops[this.currentStop+1].name;
        return nextStopName;
    }

    get currentDelay() {
        return this.delay;
    }

    arriveAtStop(minutes) {
        if (minutes < 0) {
            throw new Error("The minutes can't be a negative number!");
        }

        if (this.currentStop === this.busStops.length - 1) {
            throw new Error("Last stop reached!");
        }

        let current = this.busStops[this.currentStop];

        this.delay += minutes - current.timeToNext;
        this.totalTime += minutes;
        this.currentStop++;
    }

    toString() {
        let current = this.busStops[this.currentStop+1];
        if(this.currentStop === this.busStops.length-1) {
            current = {name: "Course completed"}
        }

        let result = `Line summary\n` +
            `- Next stop: ${current.name}\n` +
            `- Stops covered: ${this.currentStop}\n` +
            `- Time on course: ${this.totalTime} minutes\n` +
            `- Delay: ${this.delay} minutes`;

        return result;
    }
}

// Initialize a line manager with correct values
const man = new LineManager([
    {name: 'Depot', timeToNext: 4},
    {name: 'Romanian Embassy', timeToNext: 2},
    {name: 'TV Tower', timeToNext: 3},
    {name: 'Interpred', timeToNext: 4},
    {name: 'Dianabad', timeToNext: 2},
    {name: 'Depot', timeToNext: 0},
]);

// Travel through all the stops until the bus is at depot
while(man.atDepot === false) {
    console.log(man.toString());
    man.arriveAtStop(4);
}

console.log(man.toString());

// Should throw an Error (minutes cannot be negative)
//man.arriveAtStop(-4);
// Should throw an Error (last stop reached)
//man.arriveAtStop(4);

// Should throw an Error at initialization
/*const wrong = new LineManager([
    { name: 'Stop', timeToNext: { wrong: 'Should be a number'} }
]);*/
