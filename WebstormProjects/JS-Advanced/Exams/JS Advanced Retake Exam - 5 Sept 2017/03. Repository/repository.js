class Repository {
    constructor(props) {
        this.props = props;
        this.data = new Map();
        this._id = 0;
    }

    get id() {
        return this._id++;
    }

    add(repo) {
        this.validate(repo);

        let id = this.id;
        this.data.set(id, repo);
        return id;
    }

    get(id) {
        if(!this.data.has(id)) {
            throw Error(`Entity with id: ${id} does not exist!`)
        }

        return this.data.get(id);
    }

    update(id, repo) {
        if(!this.data.has(id)) {
            throw Error(`Entity with id: ${id} does not exist!`)
        }

        this.validate(repo);

        this.data.set(id, repo);
        return id;
    }

    del(id) {
        if(!this.data.has(id)) {
            throw Error(`Entity with id: ${id} does not exist!`)
        }

        this.data.delete(id);
    }

    get count() {
        return this.data.size;
    }

    validate(obj) {
        let propsArr = Object.keys(obj);
        let validProps = Object.keys(this.props);
        if(validProps.length === 0) return;

        for (let prop of propsArr) {
            if(!validProps.includes(prop)) {
                throw new Error(`Property ${prop} is missing from the entity!`);
            }

            let typeOfProp = typeof obj[prop];
            let validType = this.props[prop];
            if(typeOfProp !== validType) {
                throw new TypeError(`Property ${prop} is of incorrect type!`);
            }
        }
    }
}

// Independent repos
let repo1 = new Repository(props = {color: "string", length: "number"});
let repo2 = new Repository(props = {name: "string", counter: "number", someArr: "object"});
let id1 = repo1.add({color: 'yellow', length: 5});
let id3 = repo1.add({color: 'yellow', length: 5});
let id2 = repo2.add({name: "vasil", counter: 3, someArr: [1, 2, 3]});

console.log(id1);
console.log(id3);
console.log(id2);