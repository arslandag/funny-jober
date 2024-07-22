using funny_job.Common;
using funny_job.Entity;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace funny_job.Features;

public class DeleteEmployeeHandle
{
    public void Handle(string[] args)
    {
        var id = args
            .Skip(1)
            .Select(x => x.Split(':')[1]).ToArray();

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

            var employeeToDelete = employees
                .FirstOrDefault(e => e.Id == Guid.Parse(id[0])) 
                    ?? throw new ("not found");

            employees.Remove(employeeToDelete);
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