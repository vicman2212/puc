﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message
{
    public interface IMessageBuilder<T>
    {
        public Task<bool> SendAsync(T message);
    }
}
