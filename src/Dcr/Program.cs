using System;

namespace Dcr
{
    static class Program
    {
        static void Main()
            => new Bot().Run().GetAwaiter().GetResult();
    }
}
