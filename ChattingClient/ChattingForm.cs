using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using ChattingMessage;
using System.IO;

namespace Chatting1to1Client {
    public partial class ChattingForm : Form {

        string Nickname = default(string);

        IPEndPoint clientAddress = default(IPEndPoint);
        IPEndPoint serverAddress = default(IPEndPoint);

        TcpClient client = default(TcpClient);
        NetworkStream stream = default(NetworkStream);

        ChattingMessage.Message msg = null;

        Task recieveTask = default(Task);

        public delegate void ChangeFormText(TextBox ctrl, string text);

        void appendText(TextBox tbx, string text) {
            if (tbx.InvokeRequired)
                tbx.Invoke(new ChangeFormText(appendText), tbx, text);
            else
                tbx.AppendText(text + Environment.NewLine);
        }

        void changeText(TextBox tbx, string text) {
            if (tbx.InvokeRequired)
                tbx.Invoke(new ChangeFormText(changeText), tbx, text);
            else
                tbx.Text = text;
        }

        bool connectToServer(int count) {
            try {
                if (count == 0) {
                    appendText(tbxChattingLog, "서버 연결 실패. 프로그램 종료.");
                    Thread.Sleep(2000);
                    client.Close();
                    Application.Exit();
                    return false;
                }
                client.Connect(serverAddress);
                appendText(tbxChattingLog, "서버 연결 성공.");
                return true;
            }
            catch (SocketException) {
                appendText(tbxChattingLog, "서버 연결 실패. 재시도 중..." + "(" + count + ")");
                Thread.Sleep(1000);
                return connectToServer(count - 1);
            }
        }

        async void recieveMessage(NetworkStream stream) {
            try {
                while (true) {
                    msg = ChattingMessageUtil.Recieve(stream);
                    if (msg == null) continue;
                    if (msg.header.MSGTYPE == CONSTANT.FLOODING) {
                        string chat = Encoding.Default.GetString(msg.body.GetBytes());
                        appendText(tbxChattingLog, chat);
                    }
                    else if(msg.header.MSGTYPE == CONSTANT.NOTICE) {
                        string notice = Encoding.Default.GetString(msg.body.GetBytes());
                        appendText(tbxChattingLog, notice);
                    }
                }
            }
            catch (SocketException) {
                stream.Close();
                client.Close();
            }
            catch (IOException) {
                stream.Close();
                client.Close();
            }
        }

        void sendMessage(NetworkStream stream, ChattingMessage.Message msg) {
            try {
                ChattingMessageUtil.Send(stream, msg);
            }
            catch (SocketException) {
                stream.Close();
                client.Close();
                Application.Exit();
            }
            catch (IOException) {
                stream.Close();
                client.Close();
                Application.Exit();
            }
            catch (ObjectDisposedException) {
                appendText(tbxChattingLog, "[전송 실패: 서버와의 연결 종료됨]");
            }
        }

        ChattingMessage.Message makeChattingMessage(string chat) {
            string newChat = chat;
            if(chat.Length >2 && (chat[0] == '\r' || chat[1] == '\n')) {
                newChat = chat.Substring(2);
            }

            BodyChat body = new BodyChat(Nickname, newChat);
            Header header = new Header(CONSTANT.CHATTING, body.GetSize());

            return new ChattingMessage.Message() { header = header, body = body };
        }

        ChattingMessage.Message makeChangeNicknameMessage(string oldNickname, string newNickname) {
            BodyNickname body = new BodyNickname(oldNickname, newNickname);
            Header header = new Header(CONSTANT.CHANGE_NICKNAME, body.GetSize());

            return new ChattingMessage.Message() { header = header, body = body };
        }

        ChattingMessage.Message makeConnectionMessage(string Nickname) {
            BodyNotice body = new BodyNotice(Nickname);
            Header header = new Header(CONSTANT.NOTICE, body.GetSize());

            return new ChattingMessage.Message() { header = header, body = body };
        }

        public ChattingForm() {
            InitializeComponent();
            Nickname = "Client" + String.Format("{0:D4}", new Random().Next(1, 9999).ToString());
            changeText(tbxNickname, Nickname);
        }

        private void ChattingForm_Load(object sender, EventArgs e) {
           
        }

        private void btnEnter_Click(object sender, EventArgs e) {
            if (stream == default(NetworkStream)) return;
            if (tbxInput.Text == null || tbxInput.Text.Length == 0) return;

            ChattingMessage.Message msg = makeChattingMessage(tbxInput.Text);
            tbxInput.Clear();

            sendMessage(stream, msg);
        }

        private void tbxInput_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode != Keys.Enter) return;
            btnEnter_Click(this, e);
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            if (client != default(TcpClient)) return;

            string serverIP = tbxServerIP.Text;
            int serverPort = int.Parse(tbxServerPort.Text);

            clientAddress = new IPEndPoint(IPAddress.Loopback, 0);
            serverAddress = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

            client = new TcpClient(clientAddress);

            if (!connectToServer(3)) return; //서버와의 연결 성공 여부에 따라

            stream = client.GetStream();
            //채팅 서버 접속자들에게 자신의 접속을 알리기 위한 메세지를 서버에 보냄
            ChattingMessage.Message msg = makeConnectionMessage(Nickname);
            sendMessage(stream, msg);

            recieveTask = Task.Run(() => recieveMessage(stream));
        }

        private void ChattingForm_FormClosed(object sender, FormClosedEventArgs e) {
            if(stream != default(NetworkStream))
                stream.Close();
            if(client != default(TcpClient))
                client.Close();
            if (recieveTask != default(Task)) {
                recieveTask.Wait();
            }
        }

        private void btnChangeName_Click(object sender, EventArgs e) {
            if (stream == default(NetworkStream)) return;

            if (tbxNickname.Text == null || tbxNickname.Text.Length == 0) {
                appendText(tbxChattingLog, "유효하지 않은 닉네임입니다.");
                return;
            }
            string oldNickname = Nickname;
            Nickname = tbxNickname.Text;

            ChattingMessage.Message msg = makeChangeNicknameMessage(oldNickname, Nickname);

            sendMessage(stream, msg);
        }
    }
}
