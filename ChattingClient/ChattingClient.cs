using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace Chatting1to1Client {
    static class ChattingClient {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ChattingForm chattingForm = new ChattingForm();

            Application.Run(chattingForm);

            chattingForm.Dispose();
            Application.Exit();
        }
    }
}
