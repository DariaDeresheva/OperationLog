﻿using System;
using System.Linq;
using System.Net;

namespace OperationLog.Presentation.Desktop.Helpers
{
    public class DatabaseValuesConverter
    {
        public static string GetIpString(int ipValue)
        {
            return new IPAddress(BitConverter.GetBytes(ipValue)).ToString();
        }

        public static string GetMacString(byte[] macValue)
        {
            return string.Join("::", macValue.Take(6).Select(value => value.ToString("X2")));
        }
    }
}
