﻿namespace MusicStoreApi.Services
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html);
    }
}
