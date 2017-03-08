using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OtherWorldsGames.Models
{
    public static class Context
    {
        private static ISession current = Http.Context

        public static ISession Current
        {
            get
            {
                return current;
            }

            set
            {
                current = value;
            }
        }
    }
}
