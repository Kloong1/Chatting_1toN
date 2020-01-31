namespace ChattingMessage {
    public class CONSTANT {
        public const byte CHATTING = 0x01;
        public const byte CHANGE_NICKNAME = 0x02;
        public const byte NOTICE = 0x03;
        public const byte FLOODING = 0x04;
    }

    public class Message { //Header & Body
        
        public Header header { get; set; }
        public Body body { get; set; }

        public byte[] GetBytes() {
            byte[] bytes = new byte[GetSize()];
            header.GetBytes().CopyTo(bytes, 0);
            body.GetBytes().CopyTo(bytes, header.GetSize());

            return bytes;
        }

        public int GetSize() {
            return header.GetSize() + body.GetSize();
        }
    }
}
