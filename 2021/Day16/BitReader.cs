using System;
using System.IO;

namespace Day16
{
    class BitReaderOld
    {
        int _bit;
        byte _currentByte;
        Stream _stream;
        public BitReaderOld(Stream stream)
        { _stream = stream; }


        public bool nextByte()
        {
            var r = _stream.ReadByte();
            if (r== -1) return false;
            _bit = 0; 
            _currentByte = (byte)r;
            return true;
        }
        
        public bool? ReadBit(bool bigEndian = false)
        {
            if (_bit == 8 ) 
            {
                var r = _stream.ReadByte();
                Console.WriteLine(r);
                if (r== -1) return null;
                _bit = 0; 
                _currentByte  = (byte)r;
            }
            bool value;
            if (!bigEndian)
                value = (_currentByte & (1 << _bit)) > 0;
            else
                value = (_currentByte & (1 << (7-_bit))) > 0;

            _bit++;
            return value;
        }
    }
    
    class BitReader
    {
        int _bit = 8;
        byte _currentByte;
        byte[] _stream;
        private int byteIndex = 0;

        public BitReader(byte[] stream)
        {
            _stream = stream;
        }

        
        

        public bool nextByte()
        {
            if (byteIndex >= _stream.Length)
            {
                return false;
            }

            byteIndex += 1;
            
            var r = _stream[byteIndex];
            if (r== -1) return false;
            _bit = 0; 
            _currentByte = (byte)r;
            return true;
        }
        
        public bool? ReadBit(bool bigEndian = false)
        {

            if (_bit == 8 )
            {

                if (byteIndex >= _stream.Length)
                {
                    return null;
                }
                
                var r = _stream[byteIndex];
                byteIndex += 1;
                Console.WriteLine(r);

                _bit = 0; 
                _currentByte  = (byte)r;
            }
            bool value;
            if (!bigEndian)
                value = (_currentByte & (1 << _bit)) > 0;
            else
                value = (_currentByte & (1 << (7-_bit))) > 0;

            _bit++;
            return value;
        }
    }
}