﻿using Microsoft.IdentityModel.Tokens;

namespace GradeBook.API
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(int userId, SymmetricSecurityKey signingKey);
    }
}