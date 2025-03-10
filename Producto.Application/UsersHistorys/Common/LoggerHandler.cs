using MediatR;
using Producto.Application.Common.Logger;
using Producto.Application.Configurations;
using Producto.Application.Helpers;
using static Producto.Application.UsersHistorys.Common.LoggerHandler;

namespace Producto.Application.UsersHistorys.Common;

public class LoggerHandler() : IRequestHandler<RegisterLogCommand, bool>
{
    public record RegisterLogCommand(LogsRegister logsRegister) : IRequest<bool>;
    public async Task<bool> Handle(RegisterLogCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var _logFilePath = ConfigHelper.ConfiLoggerFile!.LoggerFile;

            if (!File.Exists(_logFilePath))
                File.Create(_logFilePath).Dispose();

            var resultHelper = LoggerHelper.GetLeyendaMessages(request);

            File.AppendAllText(_logFilePath, resultHelper + Environment.NewLine);

            if (_logFilePath.Any())
                return true;

            return false;
        }
        catch (Exception)
        {
            throw new NotImplementedException();
        }
    }

}
