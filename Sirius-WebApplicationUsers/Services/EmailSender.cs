﻿using Microsoft.AspNetCore.Identity.UI.Services;

namespace Sirius_WebApplicationUsers.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}
