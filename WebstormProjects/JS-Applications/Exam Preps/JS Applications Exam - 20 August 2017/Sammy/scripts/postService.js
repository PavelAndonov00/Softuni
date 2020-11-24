const post = (() => {
    function getPosts() {
        return requester.get('appdata', 'posts?query={}&sort={"_kmd.ect": -1}');
    }

    function getPost(id) {
        return requester.get('appdata', 'posts/' + id);
    }

    function getComments() {
        return requester.get('appdata', 'comments');
    }

    function editPost(author, url, imageUrl, title, description, id) {
        let data = {author, url, imageUrl, title, description};

        return requester.put('appdata', 'posts/' + id, 'kinvey', data);
    }

    function deletePost(id) {
        return requester.remove('appdata', 'posts/' + id);
    }

    function createPost(author, url, imageUrl, title, description) {
        let data = {author, url, imageUrl, title, description};
        return requester.post('appdata', 'posts', '', data)
    }

    function postComment(author, postId, content) {
        let data = {author, postId, content};

        return requester.post('appdata', 'comments', '', data);
    }

    function deleteCom(id) {
        return requester.remove('appdata', 'comments/' + id);
    }

    return {
        getPosts,
        getComments,
        getPost,
        editPost,
        deletePost,
        createPost,
        postComment,
        deleteCom
    }
})();