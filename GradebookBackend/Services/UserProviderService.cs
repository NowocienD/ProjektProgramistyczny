using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GradebookBackend.Services
{
    public class UserProviderService : IUserProviderService
    {
        private readonly IHttpContextAccessor context;

        public UserProviderService(IHttpContextAccessor context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserId()
        {
            try
            {
                return context.HttpContext.User.Claims.First(i => i.Type == "userId").Value;
            }
            catch (Exception exception)
            {
                //TODO do decyzji co lepsze.
                //return string.Empty;
                throw new GradebookException(exception.Message);
            }
        }
    }
}