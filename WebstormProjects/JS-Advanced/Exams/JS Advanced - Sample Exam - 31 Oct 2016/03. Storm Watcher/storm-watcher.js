class Record {
    constructor(temperature, humidity, pressure, windSpeed) {
        this.id = Record.getId();
        this.temperature = temperature;
        this.humidity = humidity;
        this.pressure = pressure;
        this.windSpeed = windSpeed;
    }

    toString() {
        let status = "Not stormy";
        if (this.temperature < 20
            && (this.pressure < 700 || this.pressure > 900)
            && this.windSpeed > 25) {
            status = "Stormy";
        }

        let result = `Reading ID: ${this.id}\n`;
        result += `Temperature: ${this.temperature}*C\n`;
        result += `Relative Humidity: ${this.humidity}%\n`;
        result += `Pressure: ${this.pressure}hpa\n`;
        result += `Wind Speed: ${this.windSpeed}m/s\n`;
        result += `Weather: ${status}`;

        return result;
    }

    static getId() {
        if(Record.nextId === undefined) {
            Record.nextId = 0;
        }

        return Record.nextId++;
    }
}



