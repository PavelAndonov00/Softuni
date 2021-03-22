import './AsideListItem.css';

function AsideListItem(AsideListItem) {
    return (
        <>
            <li className="listItem">{AsideListItem.children}</li>
        </>
    );
}

export default AsideListItem;