namespace Subtitles_Extractor_GUI
{
    partial class CfrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CfrmMain));
            this.dgvMediaFiles = new System.Windows.Forms.DataGridView();
            this.cFileName = new System.Windows.Forms.DataGridViewLinkColumn();
            this.cStream = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExtract = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaWithExistingSubtitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaWithExistingSubtitles10kbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtitles10kbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToContextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createRemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMediaFiles)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMediaFiles
            // 
            this.dgvMediaFiles.AllowUserToAddRows = false;
            this.dgvMediaFiles.AllowUserToDeleteRows = false;
            this.dgvMediaFiles.AllowUserToResizeRows = false;
            this.dgvMediaFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMediaFiles.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMediaFiles.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMediaFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMediaFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cFileName,
            this.cStream,
            this.cStatus});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMediaFiles.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMediaFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMediaFiles.EnableHeadersVisualStyles = false;
            this.dgvMediaFiles.Location = new System.Drawing.Point(0, 24);
            this.dgvMediaFiles.Name = "dgvMediaFiles";
            this.dgvMediaFiles.RowHeadersVisible = false;
            this.dgvMediaFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMediaFiles.Size = new System.Drawing.Size(792, 414);
            this.dgvMediaFiles.TabIndex = 0;
            this.dgvMediaFiles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMediaFiles_CellClick);
            this.dgvMediaFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMediaFiles_CellContentClick);
            this.dgvMediaFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvMediaFiles_KeyDown);
            // 
            // cFileName
            // 
            this.cFileName.ActiveLinkColor = System.Drawing.Color.Black;
            this.cFileName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cFileName.HeaderText = "File Name";
            this.cFileName.LinkColor = System.Drawing.Color.Black;
            this.cFileName.Name = "cFileName";
            this.cFileName.ReadOnly = true;
            this.cFileName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cFileName.TrackVisitedState = false;
            this.cFileName.VisitedLinkColor = System.Drawing.Color.Black;
            // 
            // cStream
            // 
            this.cStream.HeaderText = "Stream";
            this.cStream.Name = "cStream";
            this.cStream.Width = 170;
            // 
            // cStatus
            // 
            this.cStatus.HeaderText = "Status";
            this.cStatus.Name = "cStatus";
            this.cStatus.ReadOnly = true;
            this.cStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnExtract
            // 
            this.btnExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExtract.Location = new System.Drawing.Point(714, 442);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(76, 28);
            this.btnExtract.TabIndex = 1;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.BtnExtract_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(0, 442);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(708, 28);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Status: 0 Files";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(792, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.AddToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mediaWithExistingSubtitlesToolStripMenuItem,
            this.mediaWithExistingSubtitles10kbToolStripMenuItem,
            this.subtitles10kbToolStripMenuItem});
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            // 
            // mediaWithExistingSubtitlesToolStripMenuItem
            // 
            this.mediaWithExistingSubtitlesToolStripMenuItem.Name = "mediaWithExistingSubtitlesToolStripMenuItem";
            this.mediaWithExistingSubtitlesToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.mediaWithExistingSubtitlesToolStripMenuItem.Text = "Media with existing subtitles";
            this.mediaWithExistingSubtitlesToolStripMenuItem.Click += new System.EventHandler(this.MediaWithExistingSubtitlesToolStripMenuItem_Click);
            // 
            // mediaWithExistingSubtitles10kbToolStripMenuItem
            // 
            this.mediaWithExistingSubtitles10kbToolStripMenuItem.Name = "mediaWithExistingSubtitles10kbToolStripMenuItem";
            this.mediaWithExistingSubtitles10kbToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.mediaWithExistingSubtitles10kbToolStripMenuItem.Text = "Media with existing subtitles >10kb";
            this.mediaWithExistingSubtitles10kbToolStripMenuItem.Click += new System.EventHandler(this.MediaWithExistingSubtitles10kbToolStripMenuItem_Click);
            // 
            // subtitles10kbToolStripMenuItem
            // 
            this.subtitles10kbToolStripMenuItem.Name = "subtitles10kbToolStripMenuItem";
            this.subtitles10kbToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.subtitles10kbToolStripMenuItem.Text = "Subtitles <10kb";
            this.subtitles10kbToolStripMenuItem.Click += new System.EventHandler(this.Subtitles10kbToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendToContextToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // sendToContextToolStripMenuItem
            // 
            this.sendToContextToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createRemoveToolStripMenuItem});
            this.sendToContextToolStripMenuItem.Name = "sendToContextToolStripMenuItem";
            this.sendToContextToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sendToContextToolStripMenuItem.Text = "Send To (Context)";
            // 
            // createRemoveToolStripMenuItem
            // 
            this.createRemoveToolStripMenuItem.Name = "createRemoveToolStripMenuItem";
            this.createRemoveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.createRemoveToolStripMenuItem.Text = "Create";
            this.createRemoveToolStripMenuItem.Click += new System.EventHandler(this.CreateRemoveToolStripMenuItem_Click);
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "Media Files (*.avi,*.mkv,*.mov,*.mp4,*.webm)|*.avi;*.mkv;*.mov;*.mp4;*.webm";
            this.dlgOpenFile.Title = "Select Media File";
            // 
            // CfrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 473);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.dgvMediaFiles);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(520, 345);
            this.Name = "CfrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subtitles Extractor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CfrmMain_FormClosing);
            this.Shown += new System.EventHandler(this.CfrmMain_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.CfrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.CfrmMain_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMediaFiles)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMediaFiles;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.DataGridViewLinkColumn cFileName;
        private System.Windows.Forms.DataGridViewComboBoxColumn cStream;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediaWithExistingSubtitlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.ToolStripMenuItem subtitles10kbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediaWithExistingSubtitles10kbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendToContextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createRemoveToolStripMenuItem;
    }
}

