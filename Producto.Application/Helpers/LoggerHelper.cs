using static Producto.Application.Enums.LoggerTypes;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;

namespace Producto.Application.Helpers;

public class LoggerHelper
{
    public static string GetLeyendaMessages(RegisterLogCommand request)
    {
        var dateTimeLog = $"{DateTime.Now:yyyy - MM - dd HH: mm: ss}";
        var messages = request.logsRegister.Type switch
        {
            LoggerType.Information => $"INFORMATION: {dateTimeLog} - {request.logsRegister.Messages}",
            LoggerType.Error => $"ERROR: {dateTimeLog} - {request.logsRegister.Messages}",
            LoggerType.Warning => $"WARNING: {dateTimeLog} - {request.logsRegister.Messages}",
            _ => throw new NotImplementedException(),
        };

        return messages;
    }
}
