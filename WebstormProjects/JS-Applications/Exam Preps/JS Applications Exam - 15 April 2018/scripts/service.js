const service = (() => {
    function getActiveReceipts() {
        const userId = sessionStorage.getItem('userId');
        return requester.get('appdata', 'receipts?query={"_acl.creator":"' + userId + '","active":"true"}')
    }

    function getEntriesByReceiptId(receiptId) {
        return requester.get(`appdata', 'entries?query={"receiptId":"${receiptId}"}`)
    }

    function createReceipt(active, productCount, total) {
        let data = {active, productCount, total};
        return requester.post('appdata', 'receipts', '', data)
    }

    function addEntry(type, qty, price, receiptId) {
        let data = {type, qty, price, receiptId};
        return requester.post('appdata', 'entries', '', data)
    }

    function deleteEntry(entryId) {
        return requester.remove('appdata', 'entries/' + entryId)
    }

    function getMyReceipts() {
        const userId = sessionStorage.getItem('userId');
        return requester.get('appdata', `receipts?query={"_acl.creator":"${userId}","active":"false"}`)
    }

    function receiptDetails(receiptId) {
        return requester.get('appdata', 'receipts/' + receiptId);
    }

    function commitReceipt(receiptId, active, productCount, total) {
        let data = {active, productCount, total};
        return requester.put('appdata', 'receipts/' + receiptId, '', data);
    }

    function register(username, password, repPassword) {
        if (username.length >= 5 && password !== '' && (password === repPassword)) {
            auth.register(username, password).then(function (res) {
                auth.saveSession(res);

                auth.showInfo('Registration successful.');
            }).catch(function () {
                auth.showError('This username already exist. Please try with different one.')
            });
        }
    }

    function login(username, password) {
        auth.login(username, password).then(function (res) {
            auth.saveSession(res);

            auth.showInfo('Logged in successful.');
        }).catch(function () {
            auth.showError('Invalid credentials. Please retry your request with valid credentials.')
        });
    }

    return {
        getActiveReceipts,
        getEntriesByReceiptId,
        createReceipt,
        addEntry,
        deleteEntry,
        getMyReceipts,
        receiptDetails,
        commitReceipt,
        register,
        login
    }
})();