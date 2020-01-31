using System;
using System.IO;

namespace ChattingMessage {
    public class ChattingMessageUtil {
        public static void Send(Stream writer, Message msg) {
            byte[] bytes = msg.GetBytes();
            writer.Write(bytes, 0, bytes.Length);
        }

        public static Message Recieve(Stream reader) {
            //Read Header
            int sizeToRead = 5; //sizeof(byte) + sizeof(int)
            byte[] hBytes = new byte[sizeToRead]; 

            if (reader.Read(hBytes, 0, sizeToRead) == 0) //fail
                return null;

            Header header = new Header();
            header.MSGTYPE = hBytes[0];

            //MSGTYPE 값이 예상하지 못한 값일 경우 -> 메세지에 문제가 있음
            if (header.MSGTYPE < 0x01 || header.MSGTYPE > 0x04)
                return null;

            header.BODYLEN = BitConverter.ToInt32(hBytes, 1);

            //Read Body
            sizeToRead = header.BODYLEN;
            byte[] bBytes = new byte[sizeToRead];

            if (reader.Read(bBytes, 0, sizeToRead) == 0) //fail
                return null;

            Body body = null;
            switch (header.MSGTYPE) {
                case CONSTANT.CHATTING:
                    body = new BodyChat(bBytes);
                    break;
                case CONSTANT.NOTICE:
                    body = new BodyNotice(bBytes);
                    break;
                case CONSTANT.CHANGE_NICKNAME:
                    body = new BodyNickname(bBytes);
                    break;
                case CONSTANT.FLOODING:
                    body = new BodyFlooding(bBytes);
                    break;
            }

            return new Message { header = header, body = body };
        }
    }
}
