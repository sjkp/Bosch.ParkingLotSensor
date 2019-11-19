using System;
using Xunit;

namespace Bosch.ParkingLotSensor.Test
{
    public class DecoderTest
    {
        
        [Fact]
        public void TestOccupied()
        {
            int port = 1;
            var res = Decoder.Decode(port, "FF");

            Assert.True(res.Occupied);

            res = Decoder.Decode(port, "FE");
            Assert.False(res.Occupied);

            res = Decoder.Decode(port, "01");
            Assert.True(res.Occupied);

            res = Decoder.Decode(port, "51");
            Assert.True(res.Occupied);
        }


        [Fact]
        public void TestHeartBeat()
        {
            int port = 2;
            var res = Decoder.Decode(port, "FF");

            Assert.True(res.Occupied);

            res = Decoder.Decode(port, "FE");
            Assert.False(res.Occupied);

            res = Decoder.Decode(port, "01");
            Assert.True(res.Occupied);

            res = Decoder.Decode(port, "51");
            Assert.True(res.Occupied);
        }

        [Fact]
        public void TestStartup()
        {
            int port = 3;
            var res = Decoder.Decode(port, "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 FF".Replace(" ", ""));

            Assert.True(res.Occupied);
            Assert.Equal(ResetCauseEnum.WatchDogReset, res.ResetCause);

            res = Decoder.Decode(port, "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02 FE".Replace(" ", ""));
            Assert.False(res.Occupied);
            Assert.Equal(ResetCauseEnum.PowerOnReset, res.ResetCause);

            res = Decoder.Decode(port, "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 03 01".Replace(" ", ""));
            Assert.True(res.Occupied);
            Assert.Equal(ResetCauseEnum.SystemRequestReset, res.ResetCause);

            res = Decoder.Decode(port, "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 51".Replace(" ", ""));
            Assert.True(res.Occupied);
            Assert.Equal(ResetCauseEnum.OtherReset, res.ResetCause);
        }

        [Fact]
        public void TestRealData()
        {
            int port = 3;
            var res = Decoder.Decode(port, "00 00 00 00 99 02 02 06 00 02 00 00 00 17 03 02 00".Replace(" ", ""));
            Assert.False(res.Occupied);
            Assert.Equal(ResetCauseEnum.PowerOnReset, res.ResetCause);
            

        }
    }
}
