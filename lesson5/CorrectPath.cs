using System;
using System.IO;
using 

public class CorrectPath
{
	public bool CorrectPathCheck(string path)
    {
        
        try
        {
            if (File.Exists(path)) return true;
            File.Create(path);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("No access to the file.");
            return false;
        }
        catch (ArgumentException)
        catch (ArgumentNullException)
        catch (PathTooLongException)
        catch (NotSupportedException)

        {
            Console.WriteLine("Invalid path.");
            return false;
        }
        catch (IOException)
        {
            Console.WriteLine("An I/O error occurred while creating the file.");
            return false;
        }
        catch (DirectoryNotFoundException)
        {
            if (path.Contains(@"\"))
            {
                Directory.CreateDirectory(path);
            }
            else
            {
                return false;
            }
        }    
        catch (Exception)
        {

            throw;
        }

    }
}
