using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinChallenge
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(MobileServiceClient client);
    }
}
