namespace RegKeyGen
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGen = new System.Windows.Forms.Button();
            this.btnGetKey = new System.Windows.Forms.Button();
            this.txtDomain = new System.Windows.Forms.TextBox();
            this.txtApp = new System.Windows.Forms.TextBox();
            this.btnEnc = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.txtRecover = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBase64 = new System.Windows.Forms.Button();
            this.Hash = new System.Windows.Forms.Button();
            this.btnKeyExport = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnKeyFile = new System.Windows.Forms.Button();
            this.txtNewDomain = new System.Windows.Forms.TextBox();
            this.btnBToH = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(183, 181);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(166, 28);
            this.btnGen.TabIndex = 0;
            this.btnGen.Text = "키 생성(reg, 10개)";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // btnGetKey
            // 
            this.btnGetKey.Location = new System.Drawing.Point(256, 27);
            this.btnGetKey.Name = "btnGetKey";
            this.btnGetKey.Size = new System.Drawing.Size(95, 21);
            this.btnGetKey.TabIndex = 1;
            this.btnGetKey.Text = "키 확인";
            this.btnGetKey.UseVisualStyleBackColor = true;
            this.btnGetKey.Click += new System.EventHandler(this.btnGetKey_Click);
            // 
            // txtDomain
            // 
            this.txtDomain.Location = new System.Drawing.Point(72, 27);
            this.txtDomain.Name = "txtDomain";
            this.txtDomain.Size = new System.Drawing.Size(86, 21);
            this.txtDomain.TabIndex = 2;
            // 
            // txtApp
            // 
            this.txtApp.Location = new System.Drawing.Point(164, 27);
            this.txtApp.Name = "txtApp";
            this.txtApp.Size = new System.Drawing.Size(86, 21);
            this.txtApp.TabIndex = 3;
            // 
            // btnEnc
            // 
            this.btnEnc.Location = new System.Drawing.Point(9, 135);
            this.btnEnc.Name = "btnEnc";
            this.btnEnc.Size = new System.Drawing.Size(79, 28);
            this.btnEnc.TabIndex = 4;
            this.btnEnc.Text = "암호 테스트";
            this.btnEnc.UseVisualStyleBackColor = true;
            this.btnEnc.Click += new System.EventHandler(this.btnEnc_Click);
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(72, 54);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(279, 21);
            this.txtSource.TabIndex = 5;
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(72, 81);
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.Size = new System.Drawing.Size(279, 21);
            this.txtEncrypt.TabIndex = 6;
            // 
            // txtRecover
            // 
            this.txtRecover.Location = new System.Drawing.Point(72, 108);
            this.txtRecover.Name = "txtRecover";
            this.txtRecover.Size = new System.Drawing.Size(279, 21);
            this.txtRecover.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "원본";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "암호";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "복호";
            // 
            // btnBase64
            // 
            this.btnBase64.Location = new System.Drawing.Point(96, 135);
            this.btnBase64.Name = "btnBase64";
            this.btnBase64.Size = new System.Drawing.Size(79, 28);
            this.btnBase64.TabIndex = 11;
            this.btnBase64.Text = "Base64";
            this.btnBase64.UseVisualStyleBackColor = true;
            this.btnBase64.Click += new System.EventHandler(this.btnBase64_Click);
            // 
            // Hash
            // 
            this.Hash.Location = new System.Drawing.Point(183, 135);
            this.Hash.Name = "Hash";
            this.Hash.Size = new System.Drawing.Size(79, 28);
            this.Hash.TabIndex = 12;
            this.Hash.Text = "MD5, SHA1";
            this.Hash.UseVisualStyleBackColor = true;
            this.Hash.Click += new System.EventHandler(this.Hash_Click);
            // 
            // btnKeyExport
            // 
            this.btnKeyExport.Location = new System.Drawing.Point(183, 215);
            this.btnKeyExport.Name = "btnKeyExport";
            this.btnKeyExport.Size = new System.Drawing.Size(166, 28);
            this.btnKeyExport.TabIndex = 13;
            this.btnKeyExport.Text = "키 내보내기(reg->file)";
            this.btnKeyExport.UseVisualStyleBackColor = true;
            this.btnKeyExport.Click += new System.EventHandler(this.btnKeyExport_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "Domain :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(162, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "App : ";
            // 
            // btnKeyFile
            // 
            this.btnKeyFile.Enabled = false;
            this.btnKeyFile.Location = new System.Drawing.Point(9, 215);
            this.btnKeyFile.Name = "btnKeyFile";
            this.btnKeyFile.Size = new System.Drawing.Size(79, 28);
            this.btnKeyFile.TabIndex = 16;
            this.btnKeyFile.Text = "키파일 확인";
            this.btnKeyFile.UseVisualStyleBackColor = true;
            this.btnKeyFile.Visible = false;
            this.btnKeyFile.Click += new System.EventHandler(this.btnKeyFile_Click);
            // 
            // txtNewDomain
            // 
            this.txtNewDomain.Location = new System.Drawing.Point(72, 181);
            this.txtNewDomain.Name = "txtNewDomain";
            this.txtNewDomain.Size = new System.Drawing.Size(101, 21);
            this.txtNewDomain.TabIndex = 17;
            // 
            // btnBToH
            // 
            this.btnBToH.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnBToH.Location = new System.Drawing.Point(270, 135);
            this.btnBToH.Name = "btnBToH";
            this.btnBToH.Size = new System.Drawing.Size(79, 28);
            this.btnBToH.TabIndex = 18;
            this.btnBToH.Text = "B64->Hex";
            this.btnBToH.UseVisualStyleBackColor = true;
            this.btnBToH.Click += new System.EventHandler(this.btnBToH_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "키 도메인";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 12);
            this.label7.TabIndex = 10;
            this.label7.Text = "키 도메인";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(270, 260);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(79, 28);
            this.btnExit.TabIndex = 20;
            this.btnExit.Text = "닫 기";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 297);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnBToH);
            this.Controls.Add(this.txtNewDomain);
            this.Controls.Add(this.btnKeyFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnKeyExport);
            this.Controls.Add(this.Hash);
            this.Controls.Add(this.btnBase64);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRecover);
            this.Controls.Add(this.txtEncrypt);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.btnEnc);
            this.Controls.Add(this.txtApp);
            this.Controls.Add(this.txtDomain);
            this.Controls.Add(this.btnGetKey);
            this.Controls.Add(this.btnGen);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "IntCrypt";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.Button btnGetKey;
        private System.Windows.Forms.TextBox txtDomain;
        private System.Windows.Forms.TextBox txtApp;
        private System.Windows.Forms.Button btnEnc;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.TextBox txtRecover;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBase64;
        private System.Windows.Forms.Button Hash;
        private System.Windows.Forms.Button btnKeyExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnKeyFile;
        private System.Windows.Forms.TextBox txtNewDomain;
        private System.Windows.Forms.Button btnBToH;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnExit;
    }
}

