﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public interface ICommandHandler<T> where T : ICommand
    {
        void Handle(T command);
    }
}
