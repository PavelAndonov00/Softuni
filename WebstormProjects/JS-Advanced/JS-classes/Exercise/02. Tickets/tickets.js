function tickets(tickets, sortingCriteria) {
    class Ticket {
        constructor(destination, price, status){
            this.destination = destination;
            this.price = parseFloat(price);
            this.status = status;
        }
    }

    let result = [];
    for (let ticket of tickets) {
        let [destination, price, status] = ticket.split("|");

        result.push(new Ticket(destination, price, status));
    }

    return result.sort(sortAscending);

    function sortAscending(a, b) {
        let first = a[sortingCriteria];
        let second = b[sortingCriteria];

        if(sortingCriteria == "price") {
            return first - second;
        }
        else{
            return first.localeCompare(second);
        }
    }
}

console.log(tickets(['Philadelphia|94.20|available',
        'New York City|95.99|available',
        'New York City|95.99|sold',
        'Boston|126.20|departed'],
    'destination'
));