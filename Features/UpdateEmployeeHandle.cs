using funny_job.Common;
using funny_job.Entity;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace funny_job.Features;

public class UpdateEmployeeHandle
{
    public void Handle(string[] args)
    {
        var credentials = args
            .Skip(2)
            .Select(x => x.Split(':')[1]).ToArray();
        
        var id = args
            .Skip(1)
            .Take(1)
            .Select(x => x.Split(':')[1])
            .FirstOrDefault();
        
        var arguments = args
            .Skip(2)
            .Select(x => x.Split(':')[0])
            .ToArray();
        
        List<Employee> employees = new List<Employee>();
        using (StreamReader reader = new StreamReader(Constains.FILE_PATH))
        {
            var jsonString = reader.ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToArray();
            
            foreach (string employeeJsonString in jsonString)
            {
                if (!string.IsNullOrWhiteSpace(employeeJsonString))
                {
                    Employee? employee = JsonSerializer.Deserialize<Employee>(employeeJsonString);
                    employees.Add(employee);
                }
            }
            
            var employeeToUpdate = employees
                    .FirstOrDefault(e => e.Id == Guid.Parse(id)) 
                                ?? throw new ("not found");
            
            employeeToUpdate = Employee.Update(
                employeeToUpdate,
                credentials,
                arguments);
        }

        using (StreamWriter writer = new StreamWriter(Constains.FILE_PATH, false))
        {
            foreach (Employee jsonString in employees)
            {
                string employee = JsonSerializer.Serialize(jsonString);
                writer.WriteLine(employee);
            }
        }
    }
}