﻿namespace HiCraftApi.Services
{
    public interface IEmailService
    {

            Task SendEmailAsync(string toEmail, string subject, string body);
        
    }
}
