﻿namespace Somadhan.Domain.Interfaces;
public interface ITokenService
{
    Task<string> GenerateTokenAsync();
}

