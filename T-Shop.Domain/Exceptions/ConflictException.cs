﻿namespace T_Shop.Domain.Exceptions;
public class ConflictException : Exception
{
    public ConflictException(string message)
        : base(message)
    {
    }
}
