using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Day16
{
    public class PacketParser
    {
        // parsing
        private string data;
        private int index = 0;
        private int indexNextByte = 0;
        
        // value
        int version = 0;
        int type = 0;
        long literalValue = 0;

        private List<PacketParser> packets = new List<PacketParser>();

        public PacketParser(string data, bool aggressive = true)
        {
            this.data = data;
            
            // parse packet
            version = readBits(3);
            type = readBits(3);
            
            if (type == 4)
            {
                // literal value packet
                var readNext = true;
                do
                {
                    readNext = readBit();
                    var nibble = readBits(4);
                    literalValue <<= 4;
                    literalValue |= nibble;
                } while (readNext);
            }
            else
            {
                // everything not 4 is operator packet
                var lengthTypeID = (bool) readBit();
                var totalLength = 0;
                var numberPackets = 0;
                
                if (!lengthTypeID)
                {
                    totalLength = readBits(15);
                    var payLoad = new StringBuilder();
                    for (int j = 0; j < totalLength; j++)
                    {
                        if (readBit())
                        {
                            payLoad.Append('1');
                        }
                        else
                        {
                            payLoad.Append('0');
                        }
                    }

                    // sub packets are {totalLength} long, get substring and match until no viable input remains
                    var stringPayload = payLoad.ToString();
                    var continueParsing = true;
                    while (continueParsing)
                    {
                        var newPacket = new PacketParser(stringPayload, false);
                        packets.Add(newPacket);
                        stringPayload = stringPayload.Substring(newPacket.getIndex());

                        if (stringPayload.Length < 8)
                        {
                            continueParsing = false;
                        }
                    }
                }
                else
                {
                    numberPackets = readBits(11);
                    
                    for (int i = 0; i < numberPackets; i++)
                    {
                        var nextPacket = new PacketParser(data.Substring(index), false);
                        packets.Add(nextPacket);
                        index += nextPacket.getIndex();
                    }
                    
   
                }
            }
            
            // don't continue with remaining payload if available so we nest the packets correctly
            if (!aggressive)
            {
                return;
            }
            
            // check if any input remain that could contain a packet
            if (index < data.Length && (data.Length- index) >= 8)
            {
                packets.Add(new PacketParser(data.Substring(index)));
            }
        }
        
        public int getIndex()
        {
            return index;
        }
        
        public int getVersionSum()
        {
            var sum = version;

            foreach (var p in packets)
            {
                sum += p.getVersionSum();
            }

            return sum;
        }

        public long getValue()
        {
            if (type == 4)
            {
                return literalValue;
            }

            var values = new List<long>();
            foreach (var p in packets)
            {
                values.Add(p.getValue());
            }

            switch (type)
            {
                case 0:
                    return values.Sum();
                case 1:
                    return values.Aggregate(1L, (acc, val) => acc * val);
                case 2:
                    return values.Min();
                case 3:
                    return values.Max();
                case 5:
                    return (values[0] > values[1]) ? 1 : 0;
                case 6:
                    return (values[0] < values[1]) ? 1 : 0;
                case 7:
                    return (values[0] == values[1]) ? 1 : 0;
            }

            throw new Exception("Unknown operation");
        }
        
        private bool readBit()
        {
            if (index < data.Length)
            {
                var b = data[index];
                index += 1;
                indexNextByte = (indexNextByte / 8) + 8;
                
                if (b == '0')
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            throw new Exception("End of message");
        }
        
        private int readBits(int bits)
        {
            int value = 0;
            
            for (int i = 0; i < bits; i++)
            {
                var b = readBit();
   
                value = (value << 1);
                if (b == true)
                {
                    value |= 0x01;
                }
            }

            return value;
        }
    }
}