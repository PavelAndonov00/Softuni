function validateRequest(obj) {
    if (!obj.hasOwnProperty("method")) {
        throw new Error("Invalid request header: Invalid Method");
    }
    let method = obj.method;
    const validMethods = ['GET', 'POST', 'DELETE', 'CONNECT'];
    if (!validMethods.includes(method) && method != "") {
        throw new Error("Invalid request header: Invalid Method");
    }

    if (!obj.hasOwnProperty("uri")) {
        throw new Error("Invalid request header: Invalid Uri");
    }
    let uri = obj.uri;
    if (!/^[A-Za-z0-9.]+$/g.test(uri)) {
        throw new Error("Invalid request header: Invalid Uri");
    }

    if(!obj.hasOwnProperty("version")){
        throw new Error("Invalid request header: Invalid Version");
    }
    const validVersions = ['HTTP/0.9', 'HTTP/1.0', 'HTTP/1.1', 'HTTP/2.0'];
    let version = obj.version;
    if (!validVersions.includes(version) && version != "") {
        throw new Error("Invalid request header: Invalid Version");
    }

    if(!obj.hasOwnProperty("message")){
        throw new Error("Invalid request header: Invalid Message");
    }
    let message = obj.message;
    if (!/^[^<>\\&'"]+$/g.test(message) && message != "") {
        throw new Error("Invalid request header: Invalid Message");
    }

    return obj;
}

validateRequest({
    method: 'OPTIONS',
    uri: 'git.master',
    version: 'HTTP/1.1',
    message: '-recursive'
});

