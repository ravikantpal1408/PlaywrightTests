using System.Diagnostics;
using StreamJsonRpc;
namespace PlaywrightTests.Helpers;

public static class McpManager
{
    private static Process _process;
    private static JsonRpc _rpc;

    public static void Start(string serverPath)
    {
        if (_process != null && !_process.HasExited)
            return; // already running

        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "node",
                Arguments = serverPath,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        _process.Start();

        _rpc = JsonRpc.Attach(
            _process.StandardOutput.BaseStream, // readable
            _process.StandardInput.BaseStream   // writable
        );
    }

    public static async Task<string> CallToolAsync(string toolName, object parameters)
    {
        return await _rpc.InvokeWithCancellationAsync<string>(
            "tools/" + toolName,
            new object[] { parameters },
            default
        );
    }

    public static void Stop()
    {
        _rpc?.Dispose();
        if (_process != null && !_process.HasExited)
            _process.Kill();
    }
}
