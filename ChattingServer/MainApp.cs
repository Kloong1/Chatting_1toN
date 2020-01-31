using ChattingMessage;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;

namespace Chatting1to1Server {
    class MainApp {

        const int bindPort = 7777;
        static IPEndPoint localAddress = new IPEndPoint(IPAddress.Loopback, bindPort);

        static Message makeFloodingMessage(Message chattingMsg) {
            byte[] cmBytes = chattingMsg.body.GetBytes();

            byte nicknameSize = cmBytes[0];
            byte[] nBytes = new byte[(int)nicknameSize];
            Array.Copy(cmBytes, 1, nBytes, 0, (int)nicknameSize);
            //Console.WriteLine($"{(char)nBytes[4]}{(char)nBytes[5]}");

            byte[] chatBytes = new byte[cmBytes.Length - (int)nicknameSize - 1];
            Array.Copy(cmBytes, (int)nicknameSize + 1, chatBytes, 0, chatBytes.Length);

            string nickname = Encoding.Default.GetString(nBytes);
            string chat = Encoding.Default.GetString(chatBytes);
            string flooding = String.Format("[{0}:{1}:{2}]", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString())
                                        + String.Format("{0}: {1}", nickname, chat);

            BodyFlooding body = new BodyFlooding(flooding);
            Header header = new Header(CONSTANT.FLOODING, body.GetSize());

            return new Message() { header = header, body = body };
        }

        static Message makeNoticeMessage(string notice) {
            BodyNotice body = new BodyNotice(notice);
            Header header = new Header(CONSTANT.NOTICE, body.GetSize());

            return new Message() { header = header, body = body };
        }

        static Message makeChangeNicknameNoticeMessage(Message msg, ref string nickname) {
            byte[] bytes = msg.body.GetBytes();

            byte oldNicknameSize = bytes[0];
            byte[] oldNicknameBytes = new byte[(int)oldNicknameSize];
            Array.Copy(bytes, 1, oldNicknameBytes, 0, (int)oldNicknameSize);

            byte newNicknameSize = bytes[oldNicknameSize + 1];
            byte[] newNicknameBytes = new byte[(int)newNicknameSize];
            Array.Copy(bytes, oldNicknameSize + 2, newNicknameBytes, 0, (int)newNicknameSize);

            string oldNickname = Encoding.Default.GetString(oldNicknameBytes);
            string newNickname = Encoding.Default.GetString(newNicknameBytes);
            string notice = String.Format("[{0}:{1}:{2}]Server: 닉네임 변경 {3} -> {4}", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), oldNickname, newNickname);

            nickname = newNickname;

            return makeNoticeMessage(notice);
        }

        static void FloodMessage(NetworkStream[] streams, object streamLock, Message msg) {
            Message floodingMsg = null;

            switch (msg.header.MSGTYPE) {
                case CONSTANT.CHATTING:
                    floodingMsg = makeFloodingMessage(msg); //chatting을 flooding하는 메세지를 만듦
                    break;
                case CONSTANT.FLOODING:
                    floodingMsg = msg; //Do nothing.
                    break;
                case CONSTANT.NOTICE:
                    //아직은 NOTICE Message의 종류가 flooding 할 Message밖에 없음. 추후에 기능 추가 시 수정 요.
                    floodingMsg = msg; //Do nothing. 특정 client에게 보낼 Notice의 경우는 다른 방식으로 처리.
                    break;
            }

            lock (streamLock) {
                for (int i = 0; i < 8; i++) {
                    if (streams[i] != null)
                        ChattingMessageUtil.Send(streams[i], floodingMsg);
                }
            }
        }

        static async void connectClient(TcpListener server, NetworkStream[] streams, object streamLock, int index) {
            while (true) {
                TcpClient client = server.AcceptTcpClient();

                lock (streamLock) {
                    streams[index] = client.GetStream();
                }

                Message msg = default(Message);
                string nickname = null;
                try {
                    //잘못된 접속 시도. client 프로그램에 문제가 있는 경우.
                    if ((msg = ChattingMessageUtil.Recieve(streams[index])) == null || msg.header.MSGTYPE != CONSTANT.NOTICE) {
                        streams[index].Close();
                        client.Close();
                        return;
                    }

                    //새로운 사용자가 입장했다는 Wellcome Notice Message flooding
                    nickname = Encoding.Default.GetString(msg.body.GetBytes());
                    string wellcome = String.Format("[{0}:{1}:{2}]Server: {3}님이 입장하셨습니다.", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), nickname);

                    FloodMessage(streams, streamLock, makeNoticeMessage(wellcome));

                    //Chatting Start
                    while ((msg = ChattingMessageUtil.Recieve(streams[index])) != null) {
                        if (msg.header.MSGTYPE == CONSTANT.CHANGE_NICKNAME) 
                            FloodMessage(streams, streamLock, makeChangeNicknameNoticeMessage(msg, ref nickname));
                        else
                            FloodMessage(streams, streamLock, msg);
                    }
                    
                }
                catch (SocketException) {
                }
                catch (IOException) {
                }
                catch (ObjectDisposedException) {
                }
                finally {
                    streams[index].Close();
                    client.Close();
                    streams[index] = null;
                    string gooodbye = String.Format("[{0}:{1}:{2}]Server: {3}님이 퇴장하셨습니다.", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), nickname);
                    FloodMessage(streams, streamLock, makeNoticeMessage(gooodbye));
                }
            }
        }

        static void Main(string[] args) {

            TcpListener server = new TcpListener(localAddress);

            server.Start();

            Task[] tasks = new Task[8]; //8명까지 입장 가능.
            NetworkStream[] streams = new NetworkStream[8]; //각 client가 사용하는 stream
            object streamLock = new object();

            for (int i = 0; i < 8; i++) {
                streams[i] = null;
                int j = i; //lamda식에서 variable은 reference 참조임! 이렇게 해야함!
                tasks[i] = new Task(() => connectClient(server, streams, streamLock, j));
            }

            for (int i = 0; i < 8; i++)
                tasks[i].Start();

            Task.WaitAll(tasks);
        }
    }
}
