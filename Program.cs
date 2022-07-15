using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SQL
{
    internal class Program
    {
         static String ConnectionString = @"Data Source=.;Initial Catalog=MyDatabase;Integrated Security=True;";
        static void Main(string[] args)
        {
            string SQL = "select * from Employee";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = new SqlCommand(SQL, connection);
            connection.Open();
            SqlDataReader reader=command.ExecuteReader();
            List<Employee> employees = new List<Employee>();
            while (reader.Read())
            {
                var employee = new Employee();
                employee.Id = reader.GetInt32(0);
                employee.EmployeeName = reader.GetString(1);
                employee.Salary = reader.GetDecimal(2);
                employee.Designation = reader.GetString(3);
                employee.DeptId = reader.GetInt32(4);
                employees.Add(employee);
            }
            connection.Close();
            // any
            // without Linq
            bool is10DeptIdExistsInEmployeeTable = false;
            foreach(Employee employee in employees)
            {
                if (employee.DeptId == 10)
                {
                    is10DeptIdExistsInEmployeeTable = true;
                }
            }
            // Linq Any() Usage
            Console.WriteLine(employees.Any(x => x.DeptId == 3));

            // list the names of employees
            // traditional
            List<string> names = new List<string>();

            foreach(var emp in employees)
            {
                names.Add(emp.EmployeeName);
            }
            // linq Select
            var namess = employees.Select(x => x.EmployeeName);

            // employees whose deptid = 2
            List<Employee> empWithDeptId2= new List<Employee>();
            foreach(var emp in employees)
            {
                if (emp.DeptId == 2)
                {
                    empWithDeptId2.Add(emp);
                }
            }

            // Linq Where
            var empWithDeptId2Linq = employees.Where(x => x.DeptId == 2).ToList();

        }
        public static void Mai()
        {
            Employee emp = new Employee()
            {
                DeptId = 2,
                Designation = "Developer",
                EmployeeName = "Ajay",
                Salary=45000
            };
            var Sql = @$"insert into Employee values('{emp.EmployeeName}',{emp.Salary}, '{emp.Designation}',{emp.DeptId})";
            
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand sqlCommand = new SqlCommand(Sql,con);
            con.Open();
            int reader = sqlCommand.ExecuteNonQuery();
            Console.WriteLine(reader);
            Console.ReadKey();
            con.Close();
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
        public string  Designation { get; set; }
        public int DeptId { get; set; }
    }
}
