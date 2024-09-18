using System;
using System.Data;
using MySql.Data.MySqlClient;  // Asegúrate de usar el espacio de nombres correcto

namespace tableDataGateway
{
    public class CustomersGateway
    {
        private readonly string _connectionString;
        private const string GET_BY_NUMBER_SQL = "SELECT * FROM CUSTOMERS WHERE customerNumber = @customerNumber";
        private const string DELETE_BY_NUMBER_SQL = "DELETE FROM CUSTOMERS WHERE customerNumber = @customerNumber";
        private const string GET_ALL_SQL = "SELECT * FROM customers";

        public CustomersGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetByCustomerNumber(int customerNumber)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable table = new DataTable();

                MySqlCommand command = new MySqlCommand(GET_BY_NUMBER_SQL, connection);
                command.Parameters.AddWithValue("@customerNumber", customerNumber);  // Usar el prefijo '@'

                adapter.SelectCommand = command;
                adapter.Fill(table);
                return table;
            }
        }

        public bool DeleteByCustomerNumber(int customerNumber)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(DELETE_BY_NUMBER_SQL, connection);
                command.Parameters.AddWithValue("@customerNumber", customerNumber);

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public DataTable GetAll()
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable table = new DataTable();

                MySqlCommand command = new MySqlCommand(GET_ALL_SQL, connection);

                adapter.SelectCommand = command;
                adapter.Fill(table);
                return table;
            }
        }

        private bool Update(DataTable table)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter(GET_ALL_SQL,connection); // Pasar el comando de selección al constructor de MySqlDataAdapter
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter); // Builder para generar comandos INSERT, UPDATE y DELETE automáticamente

                adapter.UpdateCommand = builder.GetUpdateCommand(); // Genera automáticamente el comando UPDATE

                int rowsAffected = adapter.Update(table); // Aplica los cambios en el DataTable a la base de datos

                return rowsAffected > 0;
            }
        }

        public bool UpdateCustomerPhone(int customerNumber, string customerPhone)
        {
            // Obtener el registro del DataTable
            DataTable customerTable = GetByCustomerNumber(customerNumber);

            // Verificar si se encontró el cliente
            if (customerTable.Rows.Count == 0)
            {
                Console.WriteLine("No se encontró el cliente con el número especificado.");
                return false;
            }

            // Modificar el nombre del cliente en el DataTable
            DataRow customerRow = customerTable.Rows[0]; 
            customerRow["phone"] = customerPhone;

            // Actualizar la base de datos
            return Update(customerTable);
        }
        
    }
}
