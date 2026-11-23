using System.Diagnostics;
namespace SkrinshoterApp;
public interface IForkUpdateInstaller
{
    void Start(string installerPath, string targetDirectory);
}
public sealed class ForkUpdateInstaller : IForkUpdateInstaller
{
    public void Start(string installerPath, string targetDirectory)
    {
        using var process = Process.Start(new ProcessStartInfo(installerPath) {
            UseShellExecute = true, Arguments = "/DIR=\"" + targetDirectory + "\""
        });
    }
}
