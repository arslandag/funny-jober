using funny_job.Common;
using funny_job.Entity;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace funny_job.Features;

public class GetAllEmployeeHandle
{
    public void Handle(string[] args)
    {
        List<Employee> employees = new List<Employee>();
        using (StreamReader reader = new StreamReader(Constains.FILE_PATH))
        {
            string[] jsonString = reader.ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToArray();
            
            foreach (string employeeJsonString in jsonString)
            {
                if (!string.IsNullOrWhiteSpace(employeeJsonString))
                {
                    Employee? employee = JsonSerializer.Deserialize<Employee>(employeeJsonString);
                    employees.Add(employee);
                }
            }

            foreach (var employee in employees)
            {
                Console.WriteLine(
                    $"Id: {
                        employee.Id
                    }, FirstName: {
                        employee.FirstName
                    }, LastName: {
                        employee.LastName
                    }, Salary: {
                        employee.SalaryPerHour}");
            }
        }
    }
}