using Barigui;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task ProcessBaseEvent(BaseEvent baseEvent);
    }
}
