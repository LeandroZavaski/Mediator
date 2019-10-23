using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Interfaces
{
    public interface ICommand
    {
        string Id { get; set; }
    }
}
