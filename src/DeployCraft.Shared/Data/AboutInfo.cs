using System.Reflection;

namespace DeployCraft.Server.Data;

/// <summary>Content of the console "À propos" window (shared so it can be tested).</summary>
public static class AboutInfo
{
    public const string ProjectUrl = "https://github.com/vzeol/DeployCraft";
    public const string LicenseUrl = "https://www.gnu.org/licenses/gpl-3.0.html";

    public static string Notice => Msg.Get("AboutInfo_Notice");

    /// <summary>Service state text shown in the window.</summary>
    public static string ServiceText(string? version, string? machine, string? error) =>
        error != null ? error
        : string.IsNullOrWhiteSpace(machine) ? Version(version)
        : Msg.Format("AboutInfo_VersionOnMachine", Version(version), machine);

    /// <summary>Summary to paste into a problem report ("Copier les informations" button).</summary>
    public static string Summary(string? consoleVersion, string serviceText, string? connectedAs, string windows, string runtime)
    {
        var lines = new List<string>
        {
            "DeployCraft",
            Msg.Format("AboutInfo_ConsoleLine", Version(consoleVersion)),
            Msg.Format("AboutInfo_ServiceLine", serviceText),
        };
        if (!string.IsNullOrWhiteSpace(connectedAs)) lines.Add(Msg.Format("AboutInfo_ConnectedAs", connectedAs));
        lines.Add(Msg.Format("AboutInfo_WindowsLine", windows));
        lines.Add(Msg.Format("AboutInfo_RuntimeLine", runtime));
        return string.Join(Environment.NewLine, lines);
    }

    /// <summary>
    /// Installed version of an assembly, pre-release included ("1.1.0-rc.2"), without the build metadata.
    /// Assembly.GetName().Version drops the pre-release suffix, so a release candidate would look final.
    /// </summary>
    public static string? InstalledVersion(Assembly assembly) =>
        InstalledVersion(assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
            assembly.GetName().Version);

    public static string? InstalledVersion(string? informational, System.Version? version) =>
        !string.IsNullOrWhiteSpace(informational) ? informational.Split('+')[0] : version?.ToString(3);

    public static string Version(string? version) => string.IsNullOrWhiteSpace(version) ? Msg.Get("AboutInfo_UnknownVersion") : version;
}
