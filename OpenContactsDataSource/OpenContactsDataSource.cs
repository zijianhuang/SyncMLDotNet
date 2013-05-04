using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fonlow.SyncML.OpenContacts
{
    /// <summary>
    /// This class is for binding without using configuration and reflection, as program's configuration cares
    /// only DataSource, not Provider.
    /// </summary>
    public class OpenContactsDataSource : OCLocalDataSource
    {
        public OpenContactsDataSource(string exchangeType):base(new OCPlatformProvider(), exchangeType)
        {

        }
    }
}
