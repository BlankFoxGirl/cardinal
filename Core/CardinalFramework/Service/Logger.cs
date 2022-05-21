namespace Cardinal.Service;
using System;
public class Logger : ILogger
{
    public enum Level
    {
        Error,
        Warning,
        Info,
        Debug,
        Verbose
    }

    public bool Log(string message, string payload = "", Level logLevel = Level.Info)
    {
        return this.write(message, payload, logLevel);
    }

    public void Error(string message, string payload = "")
    {
        this.Log(message, payload, Level.Error);
    }

    public void Info(string message, string payload = "")
    {
        this.Log(message, payload, Level.Info);
    }

    public void Warning(string message, string payload = "")
    {
        this.Log(message, payload, Level.Warning);
    }

    public void Debug(string message, string payload = "")
    {
        this.Log(message, payload, Level.Debug);
    }

    public void Verbose(string message, string payload = "")
    {
        this.Log(message, payload, Level.Verbose);
    }

    private bool write(string message, string payload = "", Level level = Level.Info)
    {
        if (Core.logLevel < level) {
            return false; // Do not write log.
        }
        string prefix = "";
        string stack = new System.Diagnostics.StackTrace().ToString();
        switch (level) {
            case Level.Error:
                prefix = "[Error]";
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case Level.Warning:
                prefix = "[Warning]";
                Console.ForegroundColor = ConsoleColor.Yellow;
                stack = "";
                break;
            case Level.Debug:
                prefix = "[Debug]";
                Console.ForegroundColor = ConsoleColor.Cyan;
                stack = "";
                break;
            case Level.Verbose:
                prefix = "[Verbose]";
                Console.ForegroundColor = ConsoleColor.Gray;
                break;
            default:
                prefix = "[Info]";
                Console.ForegroundColor = ConsoleColor.White;
                stack = "";
            break;
        }

        string timeStamp = (DateTime.Now).ToString("yyyy-MM-dd-HH:mm:ss.ffff");
        if (!string.IsNullOrEmpty(stack)) {
            stack = " ->> " + stack;
        }
        if (string.IsNullOrEmpty(payload)) {
            Console.WriteLine("\t" + timeStamp + " - " + prefix + message  + stack);
        } else {
            Console.WriteLine("\t" + timeStamp + " - " + prefix + message + " -> " + payload + stack);
        }

        Console.ResetColor();
        return true;
    }

    public Level LevelFromString(string? level)
    {
        if (string.IsNullOrEmpty(level)) 
        {
            throw new Exceptions.InvalidLogLevelException("Supplied log level is null.");
        }
        switch(level)
        {
            case "Error":
            case "0":
                return Level.Error;
            case "Warning":
            case "1":
                return Level.Warning;
            case "Info":
            case "2":
                return Level.Info;
            case "Debug":
            case "3":
                return Level.Debug;
            case "Verbose":
            case "4":
                return Level.Verbose;
            default:
                throw new Exceptions.InvalidLogLevelException("Supplied log level " + level + " is invalid.");
        }
    }
}