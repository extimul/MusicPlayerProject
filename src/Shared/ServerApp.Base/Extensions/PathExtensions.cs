namespace ServerApp.Base.Extensions;

public static class PathExtensions
{
    public static string FolderUp(this string path, int levelCount)
    {
        List<string> pathList = new() { path };
        var levels = Enumerable.Repeat("..", levelCount).ToList();
        pathList.AddRange(levels);
        var pathArray = pathList.ToArray();
        return Path.GetFullPath(Path.Combine(pathArray));
    }
}