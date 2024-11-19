
namespace VS2019_ObjectAnnotationToolApp
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAnnotationHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showShortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.annotationProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.OutPutRichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.snapShotButton = new System.Windows.Forms.Button();
            this.processImageLabelDataButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.generateYMLButton = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataPathSelectionButton = new System.Windows.Forms.Button();
            this.clearSelectionButton = new System.Windows.Forms.Button();
            this.minusTenSecBtn = new System.Windows.Forms.Button();
            this.plusTenSecBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.sharpenBtn = new System.Windows.Forms.Button();
            this.blurrBtn = new System.Windows.Forms.Button();
            this.contrastBtn = new System.Windows.Forms.Button();
            this.brightenBtn = new System.Windows.Forms.Button();
            this.darkenBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resetViewBtn = new System.Windows.Forms.Button();
            this.medianBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1337, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.videoToolStripMenuItem,
            this.imageToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.videoToolStripMenuItem.Text = "Video";
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.imageToolStripMenuItem.Text = "Image";
            this.imageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showAnnotationHelpToolStripMenuItem,
            this.showShortcutsToolStripMenuItem,
            this.annotationProcessToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // showAnnotationHelpToolStripMenuItem
            // 
            this.showAnnotationHelpToolStripMenuItem.Name = "showAnnotationHelpToolStripMenuItem";
            this.showAnnotationHelpToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showAnnotationHelpToolStripMenuItem.Text = "Annotation Suggestion";
            this.showAnnotationHelpToolStripMenuItem.Click += new System.EventHandler(this.showAnnotationHelpToolStripMenuItem_Click);
            // 
            // showShortcutsToolStripMenuItem
            // 
            this.showShortcutsToolStripMenuItem.Name = "showShortcutsToolStripMenuItem";
            this.showShortcutsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.showShortcutsToolStripMenuItem.Text = "Show Shortcuts";
            this.showShortcutsToolStripMenuItem.Click += new System.EventHandler(this.showShortcutsToolStripMenuItem_Click);
            // 
            // annotationProcessToolStripMenuItem
            // 
            this.annotationProcessToolStripMenuItem.Name = "annotationProcessToolStripMenuItem";
            this.annotationProcessToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.annotationProcessToolStripMenuItem.Text = "Annotation Process";
            this.annotationProcessToolStripMenuItem.Click += new System.EventHandler(this.annotationProcessToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 834);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1337, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axWindowsMediaPlayer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1309, 646);
            this.splitContainer1.SplitterDistance = 658;
            this.splitContainer1.TabIndex = 2;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(640, 640);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 646);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 640);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(947, 680);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(374, 136);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.OutPutRichTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(366, 110);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Output Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // OutPutRichTextBox1
            // 
            this.OutPutRichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutPutRichTextBox1.Location = new System.Drawing.Point(3, 3);
            this.OutPutRichTextBox1.Name = "OutPutRichTextBox1";
            this.OutPutRichTextBox1.Size = new System.Drawing.Size(357, 101);
            this.OutPutRichTextBox1.TabIndex = 0;
            this.OutPutRichTextBox1.Text = "";
            // 
            // snapShotButton
            // 
            this.snapShotButton.Location = new System.Drawing.Point(9, 54);
            this.snapShotButton.Name = "snapShotButton";
            this.snapShotButton.Size = new System.Drawing.Size(67, 60);
            this.snapShotButton.TabIndex = 4;
            this.snapShotButton.Text = "SnapShot";
            this.snapShotButton.UseVisualStyleBackColor = true;
            this.snapShotButton.Click += new System.EventHandler(this.snapShotButton_Click);
            // 
            // processImageLabelDataButton
            // 
            this.processImageLabelDataButton.Location = new System.Drawing.Point(82, 54);
            this.processImageLabelDataButton.Name = "processImageLabelDataButton";
            this.processImageLabelDataButton.Size = new System.Drawing.Size(84, 60);
            this.processImageLabelDataButton.TabIndex = 5;
            this.processImageLabelDataButton.Text = "Process/Save  Image And Label Data";
            this.processImageLabelDataButton.UseVisualStyleBackColor = true;
            this.processImageLabelDataButton.Click += new System.EventHandler(this.processImageLabelDataButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(66, 25);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(178, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Class";
            // 
            // generateYMLButton
            // 
            this.generateYMLButton.Location = new System.Drawing.Point(182, 55);
            this.generateYMLButton.Name = "generateYMLButton";
            this.generateYMLButton.Size = new System.Drawing.Size(93, 60);
            this.generateYMLButton.TabIndex = 8;
            this.generateYMLButton.Text = "Generate YML Confg";
            this.generateYMLButton.UseVisualStyleBackColor = true;
            this.generateYMLButton.Click += new System.EventHandler(this.generateYMLButton_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PathTextBox.Location = new System.Drawing.Point(70, 30);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(170, 20);
            this.PathTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Data Path:";
            // 
            // dataPathSelectionButton
            // 
            this.dataPathSelectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataPathSelectionButton.Location = new System.Drawing.Point(246, 29);
            this.dataPathSelectionButton.Name = "dataPathSelectionButton";
            this.dataPathSelectionButton.Size = new System.Drawing.Size(29, 20);
            this.dataPathSelectionButton.TabIndex = 11;
            this.dataPathSelectionButton.Text = "...";
            this.dataPathSelectionButton.UseVisualStyleBackColor = true;
            this.dataPathSelectionButton.Click += new System.EventHandler(this.dataPathSelectionButton_Click);
            // 
            // clearSelectionButton
            // 
            this.clearSelectionButton.Location = new System.Drawing.Point(253, 25);
            this.clearSelectionButton.Name = "clearSelectionButton";
            this.clearSelectionButton.Size = new System.Drawing.Size(92, 36);
            this.clearSelectionButton.TabIndex = 12;
            this.clearSelectionButton.Text = "Clear Selection";
            this.clearSelectionButton.UseVisualStyleBackColor = true;
            this.clearSelectionButton.Click += new System.EventHandler(this.clearSelectionButton_Click);
            // 
            // minusTenSecBtn
            // 
            this.minusTenSecBtn.Location = new System.Drawing.Point(6, 18);
            this.minusTenSecBtn.Name = "minusTenSecBtn";
            this.minusTenSecBtn.Size = new System.Drawing.Size(33, 30);
            this.minusTenSecBtn.TabIndex = 13;
            this.minusTenSecBtn.Text = "<";
            this.minusTenSecBtn.UseVisualStyleBackColor = true;
            this.minusTenSecBtn.Click += new System.EventHandler(this.minusTenSecBtn_Click);
            // 
            // plusTenSecBtn
            // 
            this.plusTenSecBtn.Location = new System.Drawing.Point(45, 19);
            this.plusTenSecBtn.Name = "plusTenSecBtn";
            this.plusTenSecBtn.Size = new System.Drawing.Size(30, 28);
            this.plusTenSecBtn.TabIndex = 14;
            this.plusTenSecBtn.Text = ">";
            this.plusTenSecBtn.UseVisualStyleBackColor = true;
            this.plusTenSecBtn.Click += new System.EventHandler(this.plusTenSecBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.plusTenSecBtn);
            this.groupBox1.Controls.Add(this.minusTenSecBtn);
            this.groupBox1.Location = new System.Drawing.Point(15, 679);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(96, 137);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Video Control";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(7, 89);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(53, 17);
            this.radioButton3.TabIndex = 17;
            this.radioButton3.Text = "1 Sec";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(7, 72);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(56, 17);
            this.radioButton2.TabIndex = 16;
            this.radioButton2.Text = ".5 Sec";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(7, 55);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(56, 17);
            this.radioButton1.TabIndex = 15;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = ".1 Sec";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // sharpenBtn
            // 
            this.sharpenBtn.Location = new System.Drawing.Point(6, 22);
            this.sharpenBtn.Name = "sharpenBtn";
            this.sharpenBtn.Size = new System.Drawing.Size(75, 23);
            this.sharpenBtn.TabIndex = 19;
            this.sharpenBtn.Text = "Sharpen";
            this.sharpenBtn.UseVisualStyleBackColor = true;
            this.sharpenBtn.Click += new System.EventHandler(this.sharpenBtn_Click);
            // 
            // blurrBtn
            // 
            this.blurrBtn.Location = new System.Drawing.Point(6, 49);
            this.blurrBtn.Name = "blurrBtn";
            this.blurrBtn.Size = new System.Drawing.Size(75, 23);
            this.blurrBtn.TabIndex = 20;
            this.blurrBtn.Text = "Blur";
            this.blurrBtn.UseVisualStyleBackColor = true;
            this.blurrBtn.Click += new System.EventHandler(this.blurrBtn_Click);
            // 
            // contrastBtn
            // 
            this.contrastBtn.Location = new System.Drawing.Point(6, 77);
            this.contrastBtn.Name = "contrastBtn";
            this.contrastBtn.Size = new System.Drawing.Size(75, 23);
            this.contrastBtn.TabIndex = 21;
            this.contrastBtn.Text = "Contrast";
            this.contrastBtn.UseVisualStyleBackColor = true;
            this.contrastBtn.Click += new System.EventHandler(this.contrastBtn_Click);
            // 
            // brightenBtn
            // 
            this.brightenBtn.Location = new System.Drawing.Point(87, 22);
            this.brightenBtn.Name = "brightenBtn";
            this.brightenBtn.Size = new System.Drawing.Size(75, 23);
            this.brightenBtn.TabIndex = 22;
            this.brightenBtn.Text = "Brighten";
            this.brightenBtn.UseVisualStyleBackColor = true;
            this.brightenBtn.Click += new System.EventHandler(this.brightenBtn_Click);
            // 
            // darkenBtn
            // 
            this.darkenBtn.Location = new System.Drawing.Point(87, 46);
            this.darkenBtn.Name = "darkenBtn";
            this.darkenBtn.Size = new System.Drawing.Size(75, 23);
            this.darkenBtn.TabIndex = 23;
            this.darkenBtn.Text = "Darken";
            this.darkenBtn.UseVisualStyleBackColor = true;
            this.darkenBtn.Click += new System.EventHandler(this.darkenBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resetViewBtn);
            this.groupBox2.Controls.Add(this.medianBtn);
            this.groupBox2.Controls.Add(this.sharpenBtn);
            this.groupBox2.Controls.Add(this.darkenBtn);
            this.groupBox2.Controls.Add(this.blurrBtn);
            this.groupBox2.Controls.Add(this.brightenBtn);
            this.groupBox2.Controls.Add(this.contrastBtn);
            this.groupBox2.Location = new System.Drawing.Point(117, 679);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 137);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Image Enhancement";
            // 
            // resetViewBtn
            // 
            this.resetViewBtn.Location = new System.Drawing.Point(6, 106);
            this.resetViewBtn.Name = "resetViewBtn";
            this.resetViewBtn.Size = new System.Drawing.Size(75, 23);
            this.resetViewBtn.TabIndex = 25;
            this.resetViewBtn.Text = "Reset View";
            this.resetViewBtn.UseVisualStyleBackColor = true;
            this.resetViewBtn.Click += new System.EventHandler(this.resetViewBtn_Click);
            // 
            // medianBtn
            // 
            this.medianBtn.Location = new System.Drawing.Point(88, 76);
            this.medianBtn.Name = "medianBtn";
            this.medianBtn.Size = new System.Drawing.Size(75, 23);
            this.medianBtn.TabIndex = 24;
            this.medianBtn.Text = "Median Ftlr";
            this.medianBtn.UseVisualStyleBackColor = true;
            this.medianBtn.Click += new System.EventHandler(this.medianBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.PathTextBox);
            this.groupBox3.Controls.Add(this.dataPathSelectionButton);
            this.groupBox3.Controls.Add(this.generateYMLButton);
            this.groupBox3.Location = new System.Drawing.Point(293, 679);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 137);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Config Setup";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Controls.Add(this.snapShotButton);
            this.groupBox4.Controls.Add(this.processImageLabelDataButton);
            this.groupBox4.Controls.Add(this.clearSelectionButton);
            this.groupBox4.Location = new System.Drawing.Point(590, 680);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(351, 136);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image Annotation";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1337, 856);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Object Annotation Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox OutPutRichTextBox1;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button snapShotButton;
        private System.Windows.Forms.Button processImageLabelDataButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button generateYMLButton;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dataPathSelectionButton;
        private System.Windows.Forms.Button clearSelectionButton;
        private System.Windows.Forms.Button minusTenSecBtn;
        private System.Windows.Forms.Button plusTenSecBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem showAnnotationHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showShortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.Button sharpenBtn;
        private System.Windows.Forms.Button blurrBtn;
        private System.Windows.Forms.Button contrastBtn;
        private System.Windows.Forms.Button brightenBtn;
        private System.Windows.Forms.Button darkenBtn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripMenuItem annotationProcessToolStripMenuItem;
        private System.Windows.Forms.Button medianBtn;
        private System.Windows.Forms.Button resetViewBtn;
    }
}

