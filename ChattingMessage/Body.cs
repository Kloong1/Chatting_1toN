using System;
using System.Text;

namespace ChattingMessage {

    public interface Body {
        byte[] GetBytes();
        int GetSize();
    }

    public class BodyChat : Body {
        byte NicknameSize;
        byte[] Nickname;
        byte[] Chat;

        public BodyChat(string nickname, string chat ) {
            byte[] temp = Encoding.Default.GetBytes(nickname);
            Nickname = new byte[temp.Length];
            temp.CopyTo(Nickname, 0);

            NicknameSize = (byte)Nickname.Length;

            temp = Encoding.Default.GetBytes(chat);
            Chat = new byte[temp.Length];
            temp.CopyTo(Chat, 0);  
        }

        public BodyChat(byte[] bytes) {
            NicknameSize = bytes[0];
            Nickname = new byte[(int)NicknameSize];
            Array.Copy(bytes, 1, Nickname, 0, NicknameSize);

            Chat = new byte[bytes.Length - NicknameSize - 1];
            Array.Copy(bytes, NicknameSize + 1, Chat, 0, Chat.Length);
        }
        
        public byte[] GetBytes() {
            byte[] bytes = new byte[GetSize()];
            
            bytes[0] = NicknameSize;
            Nickname.CopyTo(bytes, 1);

            Chat.CopyTo(bytes, NicknameSize + 1);

            return bytes;
        }

        public int GetSize() {
            return Nickname.Length + Chat.Length + 1;
        }
    }

    public class BodyFlooding : Body {
        byte[] Flooding;

        public BodyFlooding(string flooding) {
            byte[] temp = Encoding.Default.GetBytes(flooding);
            Flooding = new byte[temp.Length];
            temp.CopyTo(Flooding, 0);
        }

        public BodyFlooding(byte[] bytes) {
            Flooding = new byte[bytes.Length];
            bytes.CopyTo(Flooding, 0);
        }

        public byte[] GetBytes() {
            return Flooding;
        }

        public int GetSize() {
            return Flooding.Length;
        }
    }

    public class BodyNotice : Body{
        byte[] Notice;

        public BodyNotice(string notice) {
            byte[] temp = Encoding.Default.GetBytes(notice);
            Notice = new byte[temp.Length];
            temp.CopyTo(Notice, 0);
        }

        public BodyNotice(byte[] bytes) {
            Notice = new byte[bytes.Length];
            bytes.CopyTo(Notice, 0);
        }

        public byte[] GetBytes() {
            return Notice;
        }

        public int GetSize() {
            return Notice.Length;
        }
    }

    public class BodyNickname : Body {
        byte oldNicknameSize; 
        byte[] oldNickname; 
        byte newNicknameSize;
        byte[] newNickname;

        public BodyNickname(string oldNickname, string newNickname) {
            byte[] temp = Encoding.Default.GetBytes(oldNickname);
            this.oldNickname = new byte[temp.Length];
            temp.CopyTo(this.oldNickname, 0);
            oldNicknameSize = (byte)temp.Length;

            temp = Encoding.Default.GetBytes(newNickname);
            this.newNickname = new byte[temp.Length];
            temp.CopyTo(this.newNickname, 0);
            newNicknameSize = (byte)temp.Length;
        }

        public BodyNickname(byte[] bytes) {
            oldNicknameSize = bytes[0];
            oldNickname = new byte[(int)oldNicknameSize];
            Array.Copy(bytes, 1, oldNickname, 0, (int)oldNicknameSize);

            newNicknameSize = bytes[oldNicknameSize + 1];
            newNickname = new byte[(int)newNicknameSize];
            Array.Copy(bytes, oldNicknameSize + 2, newNickname, 0, (int)newNicknameSize);
        }

        public byte[] GetBytes() {
            byte[] bytes = new byte[GetSize()];

            bytes[0] = oldNicknameSize;
            oldNickname.CopyTo(bytes, 1);

            bytes[(int)oldNicknameSize + 1] = newNicknameSize;
            newNickname.CopyTo(bytes, (int)oldNicknameSize + 2);

            return bytes;
        }

        public int GetSize() {
            return oldNickname.Length + newNickname.Length + 2;
        }
    }
}
