using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GradebookBackend.ServicesCore
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor context;

        public UserProvider(IHttpContextAccessor context)
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
                return string.Empty;
                throw new Exception(exception.Message);
            }
        }
    }
}