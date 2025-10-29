using System;
using UMF.Core.Infrastructure;

namespace UMF.Core.Implementation
{
    public class DateTimeTimeUtil : ITimeUtil
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }
    }
}