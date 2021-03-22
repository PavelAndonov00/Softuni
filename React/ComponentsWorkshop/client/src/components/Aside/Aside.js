import './Aside.css';
import AsideListItem from './AsideListItem/AsideListItem';

function Aside(Aside) {
    var lis = [];
    for (let index = 1; index <= 11; index++) {
        lis.push(<AsideListItem key={index}>Going to {index}</AsideListItem>);
    }
    return (
        <aside>
            <ul>
                {lis}
            </ul>
        </aside>
    );
}

export default Aside;