using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace GradebookBackend.Services
{
    public class UserProviderService : IUserProviderService
    {
        private readonly IHttpContextAccessor context;

        public UserProviderService(IHttpContextAccessor context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int GetUserId()
        {
            try
            {
                return int.Parse(context.HttpContext.User.Claims.First(i => i.Type == "userId").Value);
            }
            catch (Exception exception)
            {
                throw new GradebookServerException(exception.Message);
            }
        }
    }
}