class Data {
    constructor(method, uri, version, message, response = undefined, fulfilled = false){
        this._method = method;
        this._uri = uri;
        this._version = version;
        this._message = message;
        this._response = response;
        this._fulfilled = fulfilled;
    }
}