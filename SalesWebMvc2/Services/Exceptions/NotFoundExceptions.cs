using System;

namespace SalesWebMvc2.Services.Exceptions
{
    public class NotFoundExceptions : ApplicationException
    {
        public NotFoundExceptions(string message) : base(message)
        {

        }
    }
}
