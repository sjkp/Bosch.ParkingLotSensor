using System;
using System.Collections.Generic;

namespace Bosch.ParkingLotSensor
{
    public static class Decoder
    {

        public static Data Decode(int port, string hex)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes.Add(byte.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
            }

            return Decode(port, bytes.ToArray());
        }

        public static Data Decode(int port, byte[] bytes)
        {
            var data = new Data();
            switch(port)
            {
                case 1: //Parking status message
                    data.Occupied = (bytes[0] & 0x1) == 0x1;
                    break;
                case 2: //heartbeat
                    data.Occupied = (bytes[0] & 0x1) == 0x1;
                    break;
                case 3: //Startup
                    data.Occupied = (bytes[16] & 0x1) == 0x1;
                    data.ResetCause = (ResetCauseEnum)bytes[15];
                    data.Firmware = $"{bytes[12]}.{bytes[13]}.{bytes[14]}";
                    break;
                default:
                    return null;


            }

            return data;
        }
    }
}
