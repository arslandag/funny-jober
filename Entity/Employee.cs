using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace funny_job.Entity;

public class Employee
{
    public Employee()
    {
        
    }
    
    private Employee(
        Guid id,
        string firstName,
        string lastName,
        decimal salaryPerHour)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        SalaryPerHour = salaryPerHour;
    }
    
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public decimal SalaryPerHour { get; set; }

    public static Employee Create(
        Guid id,
        string firstName,
        string lastName,
        decimal salaryPerHour)
    {
        return new Employee(
            id,
            firstName,
            lastName,
            salaryPerHour);
    }
    
    public static Employee Update(
        Employee employee,
        string[] credentials,
        string[] arguments)
    {
        for (int i = 0; i < arguments.Length; i++)
        {
            switch (arguments[i])
            {
                case "FirstName":
                    employee.FirstName = credentials[i];
                    break;
                case "LastName":
                    employee.LastName = credentials[i];
                    break;
                case "SalaryPerHour":
                    employee.SalaryPerHour = decimal.Parse(credentials[i]);
                    break;
            }
        }

        return employee;
    }
}