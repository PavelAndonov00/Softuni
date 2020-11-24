const service = (() => {
    function listAllCars() {
        return requester.get('appdata', 'cars?query={}&sort={"_kmd.ect": -1}');
    }
    
    function createCar(brand, description, fuel, imageUrl, model, price, title, year) {
        let seller = sessionStorage.getItem('username');
        let data = {brand, description, fuel, imageUrl, model, price, seller, title, year};
        
        return requester.post('appdata', 'cars', '', data);
    }
    
    function editCar(carId, brand, description, fuel, imageUrl, model, price, title, year) {
        let seller = sessionStorage.getItem('username');
        let data = {brand, description, fuel, imageUrl, model, price, seller, title, year};


        return requester.put('appdata', 'cars/' + carId, '', data);
    }

    function deleteCar(carId) {
        return requester.remove('appdata', 'cars/' + carId);
    }
    
    function myCarsListings() {
        const username = sessionStorage.getItem('username');

        return requester.get('appdata', `cars?query={"seller":"${username}"}&sort={"_kmd.ect": -1}`)
    }

    function getCarById(carId) {
        return requester.get('appdata', 'cars/' + carId);
    }

    return {
        listAllCars,
        createCar,
        editCar,
        deleteCar,
        myCarsListings,
        getCarById
    }
})();