import './Post.css';

function Post(Post) {
    return (
        <div className="post">
            <img src="blue-origami-bird.png" alt=""/>
            <p className="description">{Post.description}</p>
            <div>
                <span>
                    <small>
                        Author:
                    </small>
                    {Post.author}
                </span>
            </div>
        </div>
    );
}

export default Post;