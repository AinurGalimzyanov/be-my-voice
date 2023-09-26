using ElectronCgi.DotNet;
using System.Text;
using ElectroneConsole;
using Muter;

class Program
{
    public static async Task Main()
    {
        var connection = new ConnectionBuilder().WithLogging(minimumLogLevel: Microsoft.Extensions.Logging.LogLevel.Trace).Build();
    }
}