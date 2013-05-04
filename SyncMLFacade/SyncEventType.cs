using System;
using System.Collections.Generic;
using System.Text;

namespace Fonlow.SyncML
{
    /// <summary>
    /// To deliver info of a device in a SyncML transaction.
    /// </summary>
    public class DeviceInfoEventArgs : EventArgs
    {
        private string man;

        public string Man
        {
            get { return man; }
            set { man = value; }
        }

        private string model;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        private string softwareVersion;

        public string SoftwareVersion
        {
            get { return softwareVersion; }
            set { softwareVersion = value; }
        }
        private string deviceId;

        public string DeviceId
        {
            get { return deviceId; }
            set { deviceId = value; }
        }

        private string deviceType;

        public string DeviceType
        {
            get { return deviceType; }
            set { deviceType = value; }
        }

        private string manufacturer;

        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        private string ctType;

        public string CTType
        {
            get { return ctType; }
            set { ctType = value; }
        }

        private string ctVersion;

        public string CTVersion
        {
            get { return ctVersion; }
            set { ctVersion = value; }
        }

    }
}
