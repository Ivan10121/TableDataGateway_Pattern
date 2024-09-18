using System.Data;
using MySql.Data.MySqlClient;

namespace tableDataGateway
{
    public class EmployeesGateway
    {
        private readonly string _connectionString;
        private const string GET_BY_NUMBER_SQL = "SELECT * FROM employees WHERE employeeNumber = @employeeNumber";
        private const string DELETE_BY_NUBER_SQL = "DELETE FROM employees WHERE employeeNumber = @employeeNumber";

        public EmployeesGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable GetByEmployeeNumber(int employeeNumber)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                MySqlDataAdapter adapter = new MySqlDataAdapter();
                DataTable table = new DataTable();

                MySqlCommand command = new MySqlCommand(GET_BY_NUMBER_SQL, connection);
                command.Parameters.AddWithValue("@employeeNumber", employeeNumber);  // Usar el prefijo '@'

                adapter.SelectCommand = command;
                adapter.Fill(table);
                return table;
            }
        }

        public bool DeleteByEmployeeNumber(int employeeNumber)
        {
            using(MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(DELETE_BY_NUBER_SQL, connection);
                command.Parameters.AddWithValue("@customerNumber", employeeNumber);

                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}