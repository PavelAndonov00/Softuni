import './Main.css';
import Post from './Post/Post';

function Main(Main) {
    var posts = [];
    for (const post of Main.posts) {
        posts.push(<Post key={post.id} 
                        description={post.description} 
                        author={post.author}></Post>)
    }

    return (
       <section className="main">
           <h1>Soooooooooooome heading</h1>

           <div className="posts">
                {posts}
           </div>
       </section>
    );
}

export default Main;