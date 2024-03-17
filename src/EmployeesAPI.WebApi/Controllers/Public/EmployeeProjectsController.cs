using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EmployeesAPI.Persistence.Services.Abstractions;
using EmployeesAPI.Common.Models.Request;
using System.Globalization;
using EmployeesAPI.Persistence.Entities;
using EmployeesAPI.WebApi.Models;
using EmployeesAPI.Common.Models.Response;


namespace EmployeesAPI.WebApi.Public.Controllers;

[ApiController]
[Route("api/public/[controller]")]
public class EmployeeProjectsController : ControllerBase
{
    private readonly IEmployeeProjectsService employeeProjectService;
    private readonly IMapper mapper;

    private List<string> dateFormats = new List<string>{ "dd.MM.yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "d.M.yyyy", "d.MM.yyyy", "dd.M.yyyy", "dd.MM.yyyy"};
    private string headers = "EmpID;ProjectID;DateFrom;DateTo";
    public EmployeeProjectsController(IEmployeeProjectsService employeeProjectService, IMapper mapper)
    {
        this.employeeProjectService = employeeProjectService;
        this.mapper = mapper;
    }

    [HttpGet("all-employees")]
    public ActionResult<IEnumerable<EmployeesProjectsResponse>> GetAll()
    {
        var allItems = employeeProjectService.GetAll();
        var responseAllItems = mapper.Map<IEnumerable<EmployeesProjectsResponse>>(allItems);

        return Ok(responseAllItems);
    }

    [HttpGet("employees-worked-together")]
    public ActionResult<IEnumerable<EmployeesWorkTogetherForLongPeriod>> EmployeesWorkedTogetherForLongestPeriod()
    {
        var emplWorkedTogetherLongPer = employeeProjectService.GetEmployeesWorkTogetherForLongPeriod();
        var responseEmplWorkedTogetherLongPer = mapper.Map<IEnumerable<EmployeesWorkTogetherForLongPeriod>>(emplWorkedTogetherLongPer);

        return Ok(responseEmplWorkedTogetherLongPer);
    }

    [HttpPost("import-file")]
    public ActionResult<ImportResult> ImportData([FromForm] ImportFile file)
    {
        if(file.File == null)
        {
            throw new ArgumentNullException(nameof(file));
        }

        var success = 0;
        var failed = 0;
        using (var memoryStream = new MemoryStream())
        {
            file.File.CopyTo(memoryStream);
            memoryStream.Position = 0;
           
            using (var reader = new StreamReader(memoryStream, System.Text.Encoding.UTF8, true))
            {               
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    if(!line.Contains(headers))
                    {
                        try
                        {
                            var newRecord = new CreateEmployeeProject();
                            newRecord.EmpID = int.Parse(values[0]);
                            newRecord.ProjectID = int.Parse(values[1]);
                            var dateFormatFrom = CheckDateFormat(values[2]);
                            newRecord.DateFrom = DateTime.ParseExact(values[2], dateFormatFrom, CultureInfo.InvariantCulture).ToUniversalTime();
                            var dateFormatTo = CheckDateFormat(values[3]);
                            newRecord.DateTo = string.IsNullOrWhiteSpace(values[3]) ? DateTime.Now.ToUniversalTime() : DateTime.ParseExact(values[3], dateFormatTo, CultureInfo.InvariantCulture).ToUniversalTime();

                            var entityItem = this.mapper.Map<EmployeeProjects>(newRecord);
                            employeeProjectService.CreateEmployeeProject(entityItem);
                            success++;
                        }
                        catch (Exception)
                        {
                            failed++;
                            continue;
                        }                   
                    }
                }
            }
        }

        var result = new ImportResult
        {
            InsertedRows = success,
            FailedRows = failed
        };

        return Ok(result);
    }
    private string CheckDateFormat(string value)
    {
        DateTime tempDate;
        foreach (var item in dateFormats)
        {
            bool validDate = DateTime.TryParseExact(value, item, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate);
            if (validDate)
                return item;
        }
        return null;
    } 
}
