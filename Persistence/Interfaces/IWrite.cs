using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IWrite
    {
        Task<Customer> Save(Customer customer);
        Task<Customer> Update(Customer customer);
    }
}
