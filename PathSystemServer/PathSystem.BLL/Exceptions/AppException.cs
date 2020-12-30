﻿using System;
using System.Net;

namespace PathSystem.BLL.Exceptions
{
    public class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; init; }

        public AppException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}