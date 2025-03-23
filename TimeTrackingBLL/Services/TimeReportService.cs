using AutoMapper;
using TimeTrackingDAL.Interfaces;
using TimeTrackingBLL.Models;
using TimeTrackingDAL.Entities;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class TimeReportService : ITimeReportService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ITimeReportService> _logger;

    public TimeReportService(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient, IConfiguration configuration, ILogger<ITimeReportService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task AddAsync(TimeReportModel model, string token)
    {
        _logger.LogInformation($"Adding TimeReport of user {model.UserId}");
        var user = await GetUserByIdAsync(model.UserId, token);
        var report = _mapper.Map<TimeReport>(model);
        var project = await _unitOfWork.ProjectRepository.GetByIdAsync(model.ProjectId);
        var activity = await _unitOfWork.ActivityTypeRepository.GetByIdAsync(model.ActivityTypeId);
        report.ActivityDescription =
            $"User {user.Email} worked on the project {project.ProjectName} " +
            $"from {model.StartTime} to {model.EndTime} as {user.RoleName} " +
            $"with activity {activity.ActivityName}";
        await SendNotification(model.ActivityDescription, user.Email);
        await _unitOfWork.TimeReportRepository.AddAsync(report);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<TimeReportModel>> GetByUserIdAndDateAsync(DateReportingSearchModel model)
    {
        _logger.LogInformation($"Fetching TimeReports by id and date");
        var timeReports = await _unitOfWork.TimeReportRepository.GetByUserIdAsync(model.UserId);
        var filteredReports = timeReports.Where(r => r.StartTime.Date == model.Date.Date || r.EndTime.Date == model.Date.Date);
        return filteredReports.Select(_mapper.Map<TimeReportModel>);
    }

    public async Task<IEnumerable<TimeReportModel>> GetByUserIdAndWeekAsync(WeekReportingSearchModel model)
    {
        _logger.LogInformation($"Fetching TimeReports by id and week");
        var timeReports = await _unitOfWork.TimeReportRepository.GetByUserIdAsync(model.UserId);
        var filteredReports = timeReports.Where(report => (GetWeekOfYear(report.StartTime) == model.Week) || (GetWeekOfYear(report.EndTime) == model.Week));
        return filteredReports.Select(_mapper.Map<TimeReportModel>);
    }

    private async Task<UserModel> GetUserByIdAsync(Guid id, string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["ExternalServices:IdentityService"]}/api/User/{id}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Split(' ').Last());

        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UserModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return null;
    }

    private async Task SendNotification(string description, string email)
    {
        var factory = new ConnectionFactory { HostName = _configuration["RabbitMQSettings:Host"] };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: _configuration["RabbitMQSettings:QueueName"], durable: false, exclusive: false, autoDelete: false,
            arguments: null);

        var message = JsonSerializer.Serialize(new TimeReportMessage
        {
            Email = email,
            ActivityDescription = description
        });
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _configuration["RabbitMQSettings:QueueName"], body: body);
    }

    private static int GetWeekOfYear(DateTime date)
    {
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        return culture.Calendar.GetWeekOfYear(date, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
    }
}
