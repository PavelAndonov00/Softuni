function mapSort(aMap, sortFunc) {
    if(sortFunc !== undefined) {
        return new Map([...aMap].sort(sortFunc));
    }

    return new Map([...aMap].sort((a, b) => a[0] - b[0]));
}

module.exports = mapSort;