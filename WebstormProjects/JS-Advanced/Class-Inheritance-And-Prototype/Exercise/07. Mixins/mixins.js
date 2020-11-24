function mixins() {
    function computerQualityMixin(classToExtend) {
        classToExtend.prototype.getQuality = function () {
            let total = Number(this.processorSpeed) + Number(this.ram) + Number(this.hardDiskSpace);
            return total / 3;
        };

        classToExtend.prototype.isFast = function () {
            if(this.processorSpeed > this.ram / 4) {
                return true;
            }

            return false;
        };

        classToExtend.prototype.isRoomy = function () {
            if (this.hardDiskSpace  > Math.floor(this.ram * this.processorSpeed)) {
                return true;
            }

            return false;
        }
    }

    function styleMixin(classToExtend) {
        classToExtend.prototype.isFullSet = function () {
            let compManufacturer = this.manufacturer;
            let keyboardManufacturer = this.keyboard.manufacturer;
            let monManufacturer = this.monitor.manufacturer;

            if(compManufacturer === keyboardManufacturer) {
                if(keyboardManufacturer === monManufacturer){
                    return true;
                }
            }

            return false;
        };

        classToExtend.prototype.isClassy = function () {
            if(this.battery.expectedLife >= 3) {
                if(this.color === "Silver" || this.color === "Black") {
                    if(this.weight < 3) {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    return {
        computerQualityMixin,
        styleMixin
    }
}