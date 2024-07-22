using System.Globalization;
using funny_job.Common;
using funny_job.Entity;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace funny_job.Features;

public class AddEmployeeHandle
{
    public void Handle(string[] args)
    {
        var credentials = args
            .Where(x => x != args[0])
            .Select(x => x.Split(':')[1]).ToArray();

        var employee = Employee.Create(
            Guid.NewGuid(),
            credentials[0],
            credentials[1],
            decimal.Parse(credentials[2], CultureInfo.InvariantCulture));
        
        using(StreamWriter writer = new StreamWriter(Constains.FILE_PATH, true))
        {
            var jsonEmployee = JsonSerializer.Serialize(employee);
            writer.WriteLine(jsonEmployee);
        }
    }
}