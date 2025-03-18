namespace Projekt_ZAJ3;

using System;

public class OverfillException : Exception
{
    public OverfillException()
    {
    }
    
    public OverfillException(string message)
        : base(message)
    {
        
    }
}