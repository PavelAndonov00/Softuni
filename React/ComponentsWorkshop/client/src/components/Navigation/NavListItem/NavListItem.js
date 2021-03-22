import './NavListItem.css';

function NavListItem(NavListItem) {
    return (
        <>
            <li className="listItem">{NavListItem.children}</li>
        </>
    );
}

export default NavListItem;