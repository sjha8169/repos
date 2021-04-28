using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TwitterApiConsumer.Base.Client
{
    public class BaseClient
    {
        #region Fields
        protected readonly string _bearerToken = ConfigurationManager.AppSettings["BearerToken"];
        #endregion
    }
}
