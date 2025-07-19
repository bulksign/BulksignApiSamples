namespace Bulksign.ApiSamples;

public static class FileUtility
{
    public static byte[] GetFileContent(string fileName)
    {
        string fullPath = Environment.CurrentDirectory + @"\Files\" + fileName;

        fullPath = fullPath.Replace('\\', Path.DirectorySeparatorChar);
        
        return File.ReadAllBytes(fullPath);
    }

}