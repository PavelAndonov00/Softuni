function posts() {
    class Post {
        constructor(title, content) {
            this.title = title;
            this.content = content;
        }

        toString() {
            return `Post: ${this.title}` + "\n" +
                `Content: ${this.content}`;
        }
    }

    class SocialMediaPost extends Post {
        constructor(title, content, likes, dislikes) {
            super(title, content);
            this.likes = likes;
            this.dislikes = dislikes;
            this.arrayOfComments = [];
        }

        addComment(comment) {
            this.arrayOfComments.push(comment);
        }

        toString() {
            if (this.arrayOfComments.length > 0) {
                return super.toString() + "\n" +
                `Rating: ${this.likes - this.dislikes}` + "\n" +
                    `Comments:` + "\n" +
                    `${this.arrayOfComments.map(e => " * " + e).join("\n")}`;
            }

            return super.toString() + "\n" +
                `Rating: ${this.likes - this.dislikes}`;
        }
    }

    class BlogPost extends Post {
        constructor(title, content, views) {
            super(title, content);
            this.views = views;
        }

        view() {
            this.views++;

            return this;
        }

        toString() {
            return super.toString() + "\n" +
                `Views: ${this.views}`;
        }

    }

    return {
        Post,
        SocialMediaPost,
        BlogPost
    }
}

posts();