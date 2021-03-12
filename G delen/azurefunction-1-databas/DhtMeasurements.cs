using System;
using System.Collections.Generic;
using System.Text;

namespace azurefunction1
{
    public class DhtMeasurements
    {
        public string DeviceId { get; set; }
        public string Name { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public string Timestamp { get; set; }
        public string School { get; set; }


    }
}
