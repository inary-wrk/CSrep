using System;
using System.IO;
using 

public class CorrectPath
{


    public bool CorrectDirectoryPath(in string path out Exception ex)
    {
        bool logic = false;
        try
        {
            Directory.CreateDirectory
            logic = true;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("No access to the file.");
        }
        catch (ArgumentException ex)
        catch (ArgumentNullException ex)
        catch (PathTooLongException ex)
        catch (NotSupportedException ex)
        {
            Console.WriteLine("Invalid path.");
        }
        catch (IOException ex)
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

        return logic;
    }


	public bool CorrectFilePath(in string path out string ex)
    {
        bool logic = false;
        try
        {
            if (File.Exists(path)) return true;
            File.Create(path);
            File.Delete(path);
            logic = true;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("No access to the file.");
        }
        catch (ArgumentException ex)
        catch (ArgumentNullException ex)
        catch (PathTooLongException ex)
        catch (NotSupportedException ex)
        {
            Console.WriteLine("Invalid path.");
        }
        catch (IOException ex)
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
        
        return logic;
        

    }
}
