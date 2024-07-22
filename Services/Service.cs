using funny_job.Features;

namespace funny_job.Services;

public class Service
{
    public static void Worker(string[] args)
    {
        while (true)
        {
            string[] credentials = Console.ReadLine().Split();

            switch (credentials[0].ToLower())
            {
                case "-add":
                    AddEmployeeHandle addHandle = new();
                    addHandle.Handle(credentials);
                    break;

                case "-get":
                    GetEmployeeHandle getHandle = new();
                    getHandle.Handle(credentials);
                    break;

                case "-getall":
                    GetAllEmployeeHandle getAllHandle = new();
                    getAllHandle.Handle(credentials);
                    break;

                case "-delete":
                    DeleteEmployeeHandle deleteHandle = new();
                    deleteHandle.Handle(credentials);
                    break;

                case "-update":
                    UpdateEmployeeHandle updateHandle = new();
                    updateHandle.Handle(credentials);
                    break;

                default:
                    Console.WriteLine("invalid command");
                    break;
            }
        }
    }
}