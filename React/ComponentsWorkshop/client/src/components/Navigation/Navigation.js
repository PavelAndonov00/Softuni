import './Navigation.css';
import NavListItem from './NavListItem/NavListItem';

function Navigation() {
    var lis = [];
    lis.push(<NavListItem key={0}><img src="./white-origami-bird.png" alt="White origami" /></NavListItem>);
    for (let index = 1; index <= 11; index++) {
        lis.push(<NavListItem key={index}>Going to {index}</NavListItem>);
    }

    return (
        <nav>
            <ul>
                {lis}
            </ul>
        </nav>
    );
}

export default Navigation;