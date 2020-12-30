using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace PathSystem.BLL.Abstractions
{
    public class AuthorizedRequest
    {
        public int UserId { get; set; }
    }
}
