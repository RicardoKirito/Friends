using Friends.Core.Application.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Friends.Core.Application.Interface.Service
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
