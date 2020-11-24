using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster
{
    public class StorageMaster
    {
        //Fields
        private List<Product> pool;
        private List<Storage> storages;
        private Vehicle currentVehicle;
        private ProductFactory productFactory;
        private VehicleFactory vehicleFactory;
        private StorageFactory storageFactory;

        //Constructors
        public StorageMaster()
        {
            this.pool = new List<Product>();
            this.storages = new List<Storage>();
            this.productFactory = new ProductFactory();
            this.vehicleFactory = new VehicleFactory();
            this.storageFactory = new StorageFactory();
        }


        //Properties

        //Methods
        public string AddProduct(string type, double price)
        {
            this.pool.Add(this.productFactory.CreateProduct(type, price)); 

            return $"Added {type} to pool";
        }

        public string RegisterStorage(string type, string name)
        {
            this.storages.Add(this.storageFactory.CreateStorage(type, name));

            return $"Registered {name}";
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {
            var currentStorage = this.storages.FirstOrDefault(s => s.Name == storageName);
            var vehicle = currentStorage.GetVehicle(garageSlot);

            this.currentVehicle = vehicle;

            return $"Selected {this.currentVehicle.GetType().Name}";
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            var loadedProductsCount = 0;
            foreach (var product in productNames)
            {
                if (this.currentVehicle.IsFull)
                {
                    break;
                }

                var currentProduct = this.pool.LastOrDefault(p => p.GetType().Name == product);
                if (currentProduct == null)
                {
                    throw new InvalidOperationException($"{product} is out of stock!");
                }

                this.currentVehicle.LoadProduct(currentProduct);
                this.pool.Remove(currentProduct);
                loadedProductsCount++;
            }

            return $"Loaded {loadedProductsCount}/{productNames.Count()} products into {this.currentVehicle.GetType().Name}";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            var sourceStorage = this.storages.FirstOrDefault(s => s.Name == sourceName);
            if (sourceStorage == null)
            {
                throw new InvalidOperationException("Invalid source storage!");
            }

            var destinationStorage = this.storages.FirstOrDefault(s => s.Name == destinationName);
            if (destinationStorage == null)
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }

            var sendingVehicle = sourceStorage.GetVehicle(sourceGarageSlot);
            var sentVehicleSlot = sourceStorage.SendVehicleTo(sourceGarageSlot, destinationStorage);

            return $"Sent {sendingVehicle.GetType().Name} to {destinationName} (slot {sentVehicleSlot})";
        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            var currentStorage = this.storages.FirstOrDefault(s => s.Name == storageName);
            var unloadingVehicle = currentStorage.GetVehicle(garageSlot);
            var productsCountAtFirst = unloadingVehicle.Trunk.Count;
            var unloadedProductsCount = currentStorage.UnloadVehicle(garageSlot);

            return $"Unloaded {unloadedProductsCount}/{productsCountAtFirst} products at {storageName}";
        }

        public string GetStorageStatus(string storageName)
        {
            var sb = new StringBuilder();
            var currentStorage = this.storages.FirstOrDefault(s => s.Name == storageName);
            var storageProductsCount = currentStorage.Products.Count;

            var orderedProducts = currentStorage
                .Products
                .GroupBy(p => p.GetType().Name)
                .OrderByDescending(p => p.Count())
                .ThenBy(p => p.Key)
                .Select(p => $"{p.Key} ({p.Count()})")
                .ToList();

            var productsWeightSum = currentStorage.Products.Sum(p => p.Weight);
            var currentVehicles = currentStorage.Garage.Select(v => v == null ? "empty" : v.GetType().Name);

            sb.AppendLine($"Stock ({productsWeightSum}/{currentStorage.Capacity}): [{String.Join(", ", orderedProducts)}]");
            sb.AppendLine($"Garage: [{String.Join("|", currentVehicles)}]");

            return sb.ToString().Trim();
        }

        public string GetSummary()
        {
            var sb = new StringBuilder();

            this
                .storages
                .OrderByDescending(s => s.Products.Sum(p => p.Price))
                .ToList()
                .ForEach(s => sb.AppendLine(s.ToString()));

            return sb.ToString().Trim();
        }

    }
}

