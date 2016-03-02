using System;
namespace AAYW.Core.Mail
{
    interface IMailProcessor
    {
        void Send(string adress, string subject, string templateKey, System.Collections.Generic.Dictionary<string, string> replacements = null);
    }
}
