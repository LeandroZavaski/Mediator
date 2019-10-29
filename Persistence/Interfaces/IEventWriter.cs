﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IEventWriter
    {
        Task SaveLeadEventAsync(CustomerEvent customerEvent);
    }
}
