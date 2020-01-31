namespace Chatting1to1Client {
    partial class ChattingForm {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent() {
            this.tbxChattingLog = new System.Windows.Forms.TextBox();
            this.tbxInput = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.lblNickname = new System.Windows.Forms.Label();
            this.tbxNickname = new System.Windows.Forms.TextBox();
            this.btnChangeName = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbxServerPort = new System.Windows.Forms.TextBox();
            this.tbxServerIP = new System.Windows.Forms.TextBox();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxChattingLog
            // 
            this.tbxChattingLog.Location = new System.Drawing.Point(15, 21);
            this.tbxChattingLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxChattingLog.Multiline = true;
            this.tbxChattingLog.Name = "tbxChattingLog";
            this.tbxChattingLog.ReadOnly = true;
            this.tbxChattingLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxChattingLog.Size = new System.Drawing.Size(450, 422);
            this.tbxChattingLog.TabIndex = 0;
            // 
            // tbxInput
            // 
            this.tbxInput.Location = new System.Drawing.Point(15, 453);
            this.tbxInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbxInput.Multiline = true;
            this.tbxInput.Name = "tbxInput";
            this.tbxInput.Size = new System.Drawing.Size(385, 44);
            this.tbxInput.TabIndex = 1;
            this.tbxInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxInput_KeyDown);
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(408, 453);
            this.btnEnter.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(57, 46);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "입력";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // lblNickname
            // 
            this.lblNickname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNickname.Location = new System.Drawing.Point(6, 44);
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(60, 28);
            this.lblNickname.TabIndex = 3;
            this.lblNickname.Text = "닉네임";
            this.lblNickname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbxNickname
            // 
            this.tbxNickname.Location = new System.Drawing.Point(72, 39);
            this.tbxNickname.Name = "tbxNickname";
            this.tbxNickname.Size = new System.Drawing.Size(179, 39);
            this.tbxNickname.TabIndex = 4;
            // 
            // btnChangeName
            // 
            this.btnChangeName.Location = new System.Drawing.Point(257, 44);
            this.btnChangeName.Name = "btnChangeName";
            this.btnChangeName.Size = new System.Drawing.Size(52, 28);
            this.btnChangeName.TabIndex = 5;
            this.btnChangeName.Text = "변경";
            this.btnChangeName.UseVisualStyleBackColor = true;
            this.btnChangeName.Click += new System.EventHandler(this.btnChangeName_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.tbxServerPort);
            this.groupBox1.Controls.Add(this.tbxServerIP);
            this.groupBox1.Controls.Add(this.lblServerPort);
            this.groupBox1.Controls.Add(this.lblServerIP);
            this.groupBox1.Controls.Add(this.lblNickname);
            this.groupBox1.Controls.Add(this.btnChangeName);
            this.groupBox1.Controls.Add(this.tbxNickname);
            this.groupBox1.Location = new System.Drawing.Point(15, 507);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(458, 182);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "설정";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(266, 88);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(93, 84);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "연결";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbxServerPort
            // 
            this.tbxServerPort.Location = new System.Drawing.Point(148, 133);
            this.tbxServerPort.Name = "tbxServerPort";
            this.tbxServerPort.Size = new System.Drawing.Size(112, 39);
            this.tbxServerPort.TabIndex = 8;
            this.tbxServerPort.Text = "7777";
            // 
            // tbxServerIP
            // 
            this.tbxServerIP.Location = new System.Drawing.Point(124, 88);
            this.tbxServerIP.Name = "tbxServerIP";
            this.tbxServerIP.Size = new System.Drawing.Size(136, 39);
            this.tbxServerIP.TabIndex = 7;
            this.tbxServerIP.Text = "127.0.0.1";
            // 
            // lblServerPort
            // 
            this.lblServerPort.AutoSize = true;
            this.lblServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServerPort.Location = new System.Drawing.Point(6, 136);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(136, 34);
            this.lblServerPort.TabIndex = 6;
            this.lblServerPort.Text = "Server Port";
            // 
            // lblServerIP
            // 
            this.lblServerIP.AutoSize = true;
            this.lblServerIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblServerIP.Location = new System.Drawing.Point(6, 93);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(112, 34);
            this.lblServerIP.TabIndex = 6;
            this.lblServerIP.Text = "Server IP";
            // 
            // ChattingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 701);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.tbxInput);
            this.Controls.Add(this.tbxChattingLog);
            this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ChattingForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChattingForm_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox tbxChattingLog;
        private System.Windows.Forms.TextBox tbxInput;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label lblNickname;
        private System.Windows.Forms.TextBox tbxNickname;
        private System.Windows.Forms.Button btnChangeName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbxServerPort;
        private System.Windows.Forms.TextBox tbxServerIP;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Button btnConnect;
    }
}

