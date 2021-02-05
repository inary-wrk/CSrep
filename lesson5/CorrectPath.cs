using System;
using System.IO;

public class CorrectPath
{

    public bool CorrectDirectoryPath(in string path out Exception ex)
    {
        try
        {
            if (Directory.Exists(path)) return true;
            Path.GetDirectoryName(path);
            Directory.CreateDirectory(path);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("No access to the file.");
        }
        catch (ArgumentException)
        catch (ArgumentNullException)
        catch (PathTooLongException)
        catch (NotSupportedException)
        {
            Console.WriteLine("Invalid path.");
        }
        catch (IOException)
        {
            Console.WriteLine("An I/O error occurred while creating the file.");
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Directory not found Exception");
        }
        catch
        {
            Console.WriteLine("Unknown error.");
        }

        return false;
    }


	public bool CorrectFilePath(in string path out Exception ex)
    {
        try
        {
            if (File.Exists(path)) return true;
            File.Create(path);
            File.Delete(path);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("No access.");
        }
        catch (ArgumentException)
        catch (ArgumentNullException)
        catch (PathTooLongException)
        catch (NotSupportedException)
        {
            Console.WriteLine("Invalid path.");
        }
        catch (IOException)
        {
            Console.WriteLine("An I/O error occurred while creating the file.");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Directory not found Exception");
        }    
        catch
        {
            Console.WriteLine("Unknown error.");
        }

        return false;
    }
}
