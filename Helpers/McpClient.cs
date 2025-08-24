using System.Diagnostics;
using StreamJsonRpc;

namespace PlaywrightTests.Helpers;

public class McpClient : IDisposable
{
    private readonly Process _process;
    private readonly JsonRpc _rpc;

    public McpClient(string serverCommand, string args = "")
    {
        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = serverCommand,
                Arguments = args,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        _process.Start();

        // order is important: (readable, writable)
        _rpc = JsonRpc.Attach(
            _process.StandardOutput.BaseStream, // read
            _process.StandardInput.BaseStream   // write
        );
    }

    public async Task<string> CallToolAsync(string toolName, object parameters)
    {
        return await _rpc.InvokeWithCancellationAsync<string>(
            "tools/" + toolName,
            new object[] { parameters },
            default
        );
    }

    public void Dispose()
    {
        _rpc.Dispose();
        if (!_process.HasExited)
            _process.Kill();
    }
}