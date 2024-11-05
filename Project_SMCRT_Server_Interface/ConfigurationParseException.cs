using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_SMCRT_Server_Interface;

public class ConfigurationParseException : Exception
{
    public ConfigurationParseException(string? message) : base(message) { }

    public ConfigurationParseException(string? message, Exception? innerException) : base(message, innerException) { }
}