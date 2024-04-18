using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Stationery; Integrated Security=SSPI;";

            // Создание экземпляра DataAccess для выполнения операций с базой данных
            DataAccess dataAccess = new DataAccess(connectionString);
            /*
                        // Вызываем методы для вставки данных
                        InsertSampleData(dataAccess);

                        // Вызываем методы для обновления данных
                        UpdateData(dataAccess);

                        // Вызываем методы для удаления данных
                        DeleteData(dataAccess);
            */
            // Вызываем методы для получения данных
            GetProductsNotSoldForDaysExample(dataAccess);
            GetTopManagerBySalesProfitInRangeExample(dataAccess);

        }

        static void InsertSampleData(DataAccess dataAccess)
        {
        
            // Вставка новых канцтоваров
            dataAccess.InsertNewProduct("Pen", "Writing", 1.5m);
            dataAccess.InsertNewProduct("Notebook", "Stationery", 3.0m);

            // Вставка новых типов канцтоваров
            dataAccess.InsertNewProductType("Art Supplies");
            dataAccess.InsertNewProductType("Office Supplies");

            // Вставка новых менеджеров по продажам
            dataAccess.InsertNewManager("John Doe");
            dataAccess.InsertNewManager("Jane Smith");

            // Вставка новых фирм покупателей
            dataAccess.InsertNewCustomer("ABC Company");
            dataAccess.InsertNewCustomer("XYZ Corporation");
           
        }

        static void UpdateData(DataAccess dataAccess)
        {
            // Обновление информации о существующем канцтоваре
            dataAccess.UpdateProduct(1, "Ballpoint Pen", "Writing", 2.0m);

            // Обновление информации о существующем клиенте
            dataAccess.UpdateCustomer(1, "New ABC Company Name");

            // Обновление информации о существующем менеджере
            dataAccess.UpdateManager(1, "John Smith");

            // Обновление информации о существующем типе канцтовара
            dataAccess.UpdateProductType(1, "School Supplies");
        }

        static void DeleteData(DataAccess dataAccess)
        {
            // Удаление канцтовара
            dataAccess.DeleteProduct(2);

            // Удаление клиента
            dataAccess.DeleteCustomer(2);

            // Удаление менеджера
            dataAccess.DeleteManager(2);

            // Удаление типа канцтовара
            dataAccess.DeleteProductType(2);
        }

        static void GetProductsNotSoldForDaysExample(DataAccess dataAccess)
        {
            int days = 30; // Указываем количество дней

            Console.WriteLine($"Products not sold for {days} days:");
            var products = dataAccess.GetProductsNotSoldForDays(days);
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        static void GetTopManagerBySalesProfitInRangeExample(DataAccess dataAccess)
        {
            DateTime startDate = new DateTime(2024, 1, 1); // Указываем начальную дату
            DateTime endDate = new DateTime(2024, 3, 31); // Указываем конечную дату

            int topManagerID = dataAccess.GetTopManagerBySalesProfitInRange(startDate, endDate);

            Console.WriteLine($"Top manager ID by sales profit in range ({startDate.ToShortDateString()} - {endDate.ToShortDateString()}): {topManagerID}");
        }
    }
}
/*
 CREATE TABLE Managers (
    ManagerID INT PRIMARY KEY,
    ManagerName NVARCHAR(50)
);

CREATE TABLE Sales (
    SaleID INT PRIMARY KEY,
    ManagerID INT,
    SaleDate DATE,
    SaleAmount DECIMAL(10,2),
    Quantity INT,
    ProductID INT,
    FOREIGN KEY (ManagerID) REFERENCES Managers(ManagerID)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(50),
    ProductType NVARCHAR(50),
    UnitPrice DECIMAL(10,2)
);

CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    CustomerName NVARCHAR(50)
);

CREATE TABLE Purchases (
    PurchaseID INT PRIMARY KEY,
    CustomerID INT,
    SaleID INT,
    PurchaseDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (SaleID) REFERENCES Sales(SaleID)
);

CREATE PROCEDURE GetProductsNotSoldForDays
    @Days INT
AS
BEGIN
    SELECT ProductName
    FROM Products
    WHERE ProductID NOT IN (
        SELECT DISTINCT ProductID
        FROM Sales
        WHERE DATEDIFF(DAY, SaleDate, GETDATE()) <= @Days
    );
END;

CREATE PROCEDURE GetTopManagerBySalesProfitInRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT TOP 1 ManagerID
    FROM Sales
    WHERE SaleDate BETWEEN @StartDate AND @EndDate
    GROUP BY ManagerID
    ORDER BY SUM(SaleAmount) DESC;
END;
 */