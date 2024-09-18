using System.Data;

namespace tableDataGateway
{
    public class Program
    {
        
        private const string CONNECTION_STRING = "Server=localhost;Database=classicmodels;Uid=root;Pwd=Ivan123456789;";

        private static void printTable(DataTable t)
        {
            if (t.Rows.Count > 0)
            {
                // Iterar sobre cada fila en la tabla
                foreach (DataRow row in t.Rows)
                {
                    // Iterar sobre cada columna en la fila
                    foreach (DataColumn col in t.Columns)
                    {
                        // Imprimir el nombre de la columna y el valor de la celda
                        Console.WriteLine($"{col.ColumnName}: {row[col]}");
                    }
                    // Separador entre filas
                    Console.WriteLine("--------------------");
                }
            }
            else
            {
                Console.WriteLine("La tabla 1 está vacía.");
            }
        }
        
        public static void Main(string[] args)
        {
            CustomersGateway customersGateway = new CustomersGateway(CONNECTION_STRING);
            DataTable table = customersGateway.GetByCustomerNumber(103);
            EmployeesGateway employeesGateway = new EmployeesGateway(CONNECTION_STRING);
            DataTable table2 = employeesGateway.GetByEmployeeNumber(1337);

            //printTable(table);


            //printTable(table2);


            DataTable customersTable = customersGateway.GetAll();
            //printTable(customersTable);

            
            bool success = customersGateway.UpdateCustomerPhone(103,"7777777777777");

            if(success)
            {
                Console.WriteLine("Actualizacion de telefono exitosa");
            }
            else{
                Console.WriteLine("No se pudo actualizar el telefono");
            }
            


        }

    }
}
