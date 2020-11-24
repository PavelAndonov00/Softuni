const chirpService = (function () {
    function getUsers() {
        return requester.get('user', '')
    }

    function getUser(id) {
        return requester.get('user', id);
    }

    function getChirps() {
        return requester.get('appdata', 'chirps')
    }

    function getChirpsCount() {
        let username = sessionStorage.getItem('username');
        return requester.get('appdata', `chirps?query={"author":"${username}"}`);
    }

    function getFollowingCount() {
        let username = sessionStorage.getItem('username');
        return requester.get('user', `?query={"username":"${username}"}`)
    }

    function getFollowersCount() {
        let username = sessionStorage.getItem('username');
        return requester.get('user', `?query={"subscriptions":"${username}"}`)
    }

    function createChirp(text, author) {
        let data = {text, author};

        return requester.post('appdata', 'chirps', '', data);
    }

    function deleteChirp(id) {
        return requester.remove('appdata', 'chirps/' + id);
    }

    return {
        getUsers,
        getUser,
        getChirps,
        getChirpsCount,
        getFollowersCount,
        getFollowingCount,
        createChirp,
        deleteChirp
    }
})();