using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server.Pack;

internal class PackContentException : Exception
{
    public PackContentException(string? message) : base(message)
    {
    }
    public PackContentException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}