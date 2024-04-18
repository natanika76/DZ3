using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DZ3
{
    public class DataAccess
    {
        private readonly string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //Название канцтоваров, которые не продавались заданное количество дней:
        public List<string> GetProductsNotSoldForDays(int days)
        {
            List<string> products = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetProductsNotSoldForDays", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Days", days);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string productName = reader.GetString(0);
                            products.Add(productName);
                        }
                    }
                }
            }

            return products;
        }

        //Информация о менеджере по продажам с наибольшей общей суммой прибыли за указанный промежуток времени:
        public int GetTopManagerBySalesProfitInRange(DateTime startDate, DateTime endDate)
        {
            int topManagerID = -1;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetTopManagerBySalesProfitInRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StartDate", startDate);
                    command.Parameters.AddWithValue("@EndDate", endDate);

                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        topManagerID = Convert.ToInt32(result);
                    }
                }
            }

            return topManagerID;
        }

        // Вставка новых канцтоваров
        public void InsertNewProduct(string productName, string productType, decimal unitPrice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO Products (ProductName, ProductType, UnitPrice) VALUES (@ProductName, @ProductType, @UnitPrice)", connection))
                {
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@ProductType", productType);
                    command.Parameters.AddWithValue("@UnitPrice", unitPrice);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Вставка новых типов канцтоваров
        public void InsertNewProductType(string productType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO ProductTypes (ProductType) VALUES (@ProductType)", connection))
                {
                    command.Parameters.AddWithValue("@ProductType", productType);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Вставка новых менеджеров по продажам
        public void InsertNewManager(string managerName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO Managers (ManagerName) VALUES (@ManagerName)", connection))
                {
                    command.Parameters.AddWithValue("@ManagerName", managerName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Вставка новых фирм покупателей
        public void InsertNewCustomer(string customerName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO Customers (CustomerName) VALUES (@CustomerName)", connection))
                {
                    command.Parameters.AddWithValue("@CustomerName", customerName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Обновление информации о существующих канцтоварах
        public void UpdateProduct(int productID, string productName, string productType, decimal unitPrice)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE Products SET ProductName = @ProductName, ProductType = @ProductType, UnitPrice = @UnitPrice WHERE ProductID = @ProductID", connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productID);
                    command.Parameters.AddWithValue("@ProductName", productName);
                    command.Parameters.AddWithValue("@ProductType", productType);
                    command.Parameters.AddWithValue("@UnitPrice", unitPrice);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Обновление информации о существующих фирмах покупателях
        public void UpdateCustomer(int customerID, string customerName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE Customers SET CustomerName = @CustomerName WHERE CustomerID = @CustomerID", connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerID);
                    command.Parameters.AddWithValue("@CustomerName", customerName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Обновление информации о существующих менеджерах по продажам
        public void UpdateManager(int managerID, string managerName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE Managers SET ManagerName = @ManagerName WHERE ManagerID = @ManagerID", connection))
                {
                    command.Parameters.AddWithValue("@ManagerID", managerID);
                    command.Parameters.AddWithValue("@ManagerName", managerName);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Обновление информации о существующих типах канцтоваров
        public void UpdateProductType(int productTypeID, string productType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("UPDATE ProductTypes SET ProductType = @ProductType WHERE ProductTypeID = @ProductTypeID", connection))
                {
                    command.Parameters.AddWithValue("@ProductTypeID", productTypeID);
                    command.Parameters.AddWithValue("@ProductType", productType);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Удаление канцтоваров
        public void DeleteProduct(int productID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Удаление менеджеров по продажам
        public void DeleteManager(int managerID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM Managers WHERE ManagerID = @ManagerID", connection))
                {
                    command.Parameters.AddWithValue("@ManagerID", managerID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Удаление типов канцтоваров
        public void DeleteProductType(int productTypeID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM ProductTypes WHERE ProductTypeID = @ProductTypeID", connection))
                {
                    command.Parameters.AddWithValue("@ProductTypeID", productTypeID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Удаление фирм покупателей
        public void DeleteCustomer(int customerID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @CustomerID", connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public void SetIdentityInsert(string tableName, bool value)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SET IDENTITY_INSERT {tableName} {(value ? "ON" : "OFF")}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
