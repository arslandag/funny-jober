using funny_job.Common;
using funny_job.Entity;
using funny_job.Features;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UnitTest;

public class AddTest
{
    [Theory]
    [InlineData("Arslan", "Amai", "333.33")]
    [InlineData("Jeck", "Chirik", "1.1")]
    [InlineData("Masha", "TT", "0.1")]
    public async Task Handle_CreatesEmployeeAndWritesToJsonFile(params string[] args)
    {
        var handle = new AddEmployeeHandle();
        
        var expectedEmployee = Employee.Create(
            Guid.NewGuid(),
            args[0],
            args[1],
            decimal.Parse(args[2]));

        handle.Handle(args);

        Employee employee = new();
        using (StreamReader reader = new StreamReader(Constains.FILE_PATH))
        {
            string? jsonString = reader.ReadToEnd()
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .FirstOrDefault();
            
            employee = JsonSerializer.Deserialize<Employee>(jsonString);
        }

        Assert.Equal(expectedEmployee.Id, employee.Id);
        Assert.Equal(expectedEmployee.FirstName, employee.FirstName);
        Assert.Equal(expectedEmployee.LastName, employee.LastName);
        Assert.Equal(expectedEmployee.SalaryPerHour, employee.SalaryPerHour);
    }

    [Fact]
    public async Task Handle_ThrowsArgumentNullException_WhenArgsIsNull()
    {
        // Arrange
        var handle = new AddEmployeeHandle();

        // Act and Assert
        Assert.Throws<ArgumentNullException>(() => handle.Handle(null));
    }
}