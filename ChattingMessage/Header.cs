using System;

namespace ChattingMessage {
    public class Header {
        public byte MSGTYPE { get; set; }
        public int BODYLEN { get; set; }

        public Header() { }

        public Header(byte MSGTYPE, int BODYLEN) {
            this.MSGTYPE = MSGTYPE;
            this.BODYLEN = BODYLEN;
        }

        public Header(byte[] bytes) {
            MSGTYPE = bytes[0];
            BODYLEN = BitConverter.ToInt32(bytes, 1);
        }
        
        public byte[] GetBytes() {
            byte[] bytes = new byte[GetSize()];
            bytes[0] = MSGTYPE;

            BitConverter.GetBytes(BODYLEN).CopyTo(bytes, 1);

            return bytes;
        }

        public int GetSize() {
            return sizeof(byte) + sizeof(int);
        }
    }
}
