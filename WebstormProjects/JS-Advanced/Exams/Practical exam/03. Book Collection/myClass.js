class BookCollection {
    constructor(shelfGenre, room, shelfCapacity) {
        this.validateRoom(room);
        this.shelfGenre = shelfGenre;
        this.room = room;
        this.shelfCapacity = shelfCapacity;
        this.shelf = [];
    }

    validateRoom(room) {
        let validRooms = ["livingRoom", "bedRoom", "closet"];
        if (!validRooms.includes(room)) {
            throw new Error(`Cannot have book shelf in ${room}`);
        }
    }

    addBook(name, author, genre) {
        let freeSpace = this.shelfCapacity - this.shelf.length;
        if (freeSpace === 0) {
            this.shelf.shift();
        }

        this.shelf.push({name, author, genre});

        this.shelf.sort((a, b) => a.author.localeCompare(b.author));
        return this;
    }

    throwAwayBook(bookName) {
        for (let i = 0; i < this.shelf.length; i++) {
            let book = this.shelf[i];
            if(book.name === bookName) {
                this.shelf.splice(i, 1);
                i--;
            }
        }
    }

    showBooks(genre) {
        let result = [];
        for (let book of this.shelf) {
            if(book.genre === genre) {
                result.push(book);
            }
        }

        let resultString = `Results for search "${genre}":\n`;
        for (let i = 0; i < result.length; i++) {
            let book = result[i];

            if (i === result.length - 1) {
                resultString += `\uD83D\uDCD6 ${book.author} - "${book.name}"`;
            } else {
                resultString += `\uD83D\uDCD6 ${book.author} - "${book.name}"\n`;
            }
        }

        return resultString;
    }

    get shelfCondition() {
        let freeSpace = this.shelfCapacity - this.shelf.length;
        return freeSpace;
    }

    toString() {
        if (this.shelf.length === 0) {
            return `It's an empty shelf`;
        }

        let result = `"${this.shelfGenre}" shelf in ${this.room} contains:\n`;
        for (let i = 0; i < this.shelf.length; i++) {
            let book = this.shelf[i];
            if (i === this.shelf.length - 1) {
                result += `\uD83D\uDCD6 "${book.name}" - ${book.author}`;
            } else {
                result += `\uD83D\uDCD6 "${book.name}" - ${book.author}\n`;
            }
        }

        return result;
    }
}

