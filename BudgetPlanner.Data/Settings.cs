using System.IO;

using Windows.Storage;

namespace BudgetPlanner.Data;

internal static class Settings
{
    public static string AppDataFile { get; } = "data.db";

    public static string AppDataFilePath { get; } = Path.Combine(ApplicationData.Current.LocalFolder.Path, AppDataFile);

    public static string CurrencyMarker { get; } = "$";

    public static int MaxOperations { get; } = 100;
}
