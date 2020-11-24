function getPeople() {
    class Person {
        constructor(firstName, lastName, age, email) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
            this.email = email;
        }

        toString() {
            return `${this.firstName} ${this.lastName} (age: ${this.age}, email: ${this.email})`;
        }
    }

    let Maria = new Person("Maria", "Petrova", 22, "mp@yahoo.com");
    let SoftUni = new Person("SoftUni");
    let Stephan = new Person("Stephan", "Nikolov", 25);
    let Peter = new Person("Peter", "Kolev", 24, "ptr@gmail.com");

    return [Maria, SoftUni, Stephan, Peter];
}