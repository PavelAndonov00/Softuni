import {useEffect, useState} from 'react';

import './App.css';
import Navigation from './components/Navigation/Navigation';
import Aside from './components/Aside/Aside';
import Main from './components/Main/Main';

const App = () => {
	var [posts, setPosts] = useState([]);
    
    useEffect(() => {
        if(posts.length == 0){
            fetch("https://localhost:5001/api/Posts")
                .then((result) => result.json())
                .then((result) => setPosts(result));
        }
    });

	return (
		<div>
			<Navigation></Navigation>

			<main className="container">
				<Aside></Aside>

				<Main posts={posts}></Main>
			</main>
		</div>
	);
}

export default App;