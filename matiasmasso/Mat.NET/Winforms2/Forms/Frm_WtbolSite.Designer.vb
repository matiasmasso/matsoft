<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WtbolSite
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBoxWeb = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBoxContactTel = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxContactEmail = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxContactNom = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxStocks = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxLandingPages = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxMerchantId = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.CheckBoxActive = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Contact21 = New Mat.Net.Xl_Contact2()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_WtbolLandingPages1 = New Mat.Net.Xl_WtbolLandingPages()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_WtbolStocks1 = New Mat.Net.Xl_WtbolStocks()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_WtbolSiteClicks1 = New Mat.Net.Xl_WtbolSiteClicks()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Xl_WtbolBaskets1 = New Mat.Net.Xl_WtbolBaskets()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopiarGuidToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.FeedStocksParaHatchToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_WtbolLandingPages1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_WtbolStocks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_WtbolSiteClicks1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.Xl_WtbolBaskets1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxWeb
        '
        Me.TextBoxWeb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxWeb.Location = New System.Drawing.Point(94, 17)
        Me.TextBoxWeb.Name = "TextBoxWeb"
        Me.TextBoxWeb.Size = New System.Drawing.Size(306, 20)
        Me.TextBoxWeb.TabIndex = 57
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 498)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(420, 31)
        Me.Panel1.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(201, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(312, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Web"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Location = New System.Drawing.Point(0, 37)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(420, 459)
        Me.TabControl1.TabIndex = 58
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label10)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.TextBoxStocks)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.TextBoxLandingPages)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxMerchantId)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxObs)
        Me.TabPage1.Controls.Add(Me.CheckBoxActive)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Xl_Contact21)
        Me.TabPage1.Controls.Add(Me.TextBoxWeb)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(412, 433)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(94, 43)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(306, 20)
        Me.TextBoxNom.TabIndex = 75
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 46)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(29, 13)
        Me.Label10.TabIndex = 74
        Me.Label10.Text = "Nom"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxContactTel)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.TextBoxContactEmail)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TextBoxContactNom)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 210)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(390, 117)
        Me.GroupBox1.TabIndex = 73
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Contacte tècnic"
        '
        'TextBoxContactTel
        '
        Me.TextBoxContactTel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContactTel.Location = New System.Drawing.Point(78, 81)
        Me.TextBoxContactTel.Name = "TextBoxContactTel"
        Me.TextBoxContactTel.Size = New System.Drawing.Size(157, 20)
        Me.TextBoxContactTel.TabIndex = 76
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(22, 84)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 13)
        Me.Label9.TabIndex = 75
        Me.Label9.Text = "Tel"
        '
        'TextBoxContactEmail
        '
        Me.TextBoxContactEmail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContactEmail.Location = New System.Drawing.Point(78, 55)
        Me.TextBoxContactEmail.Name = "TextBoxContactEmail"
        Me.TextBoxContactEmail.Size = New System.Drawing.Size(306, 20)
        Me.TextBoxContactEmail.TabIndex = 74
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(22, 58)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(32, 13)
        Me.Label8.TabIndex = 73
        Me.Label8.Text = "Email"
        '
        'TextBoxContactNom
        '
        Me.TextBoxContactNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxContactNom.Location = New System.Drawing.Point(78, 29)
        Me.TextBoxContactNom.Name = "TextBoxContactNom"
        Me.TextBoxContactNom.Size = New System.Drawing.Size(306, 20)
        Me.TextBoxContactNom.TabIndex = 72
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(22, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 71
        Me.Label7.Text = "Nom"
        '
        'TextBoxStocks
        '
        Me.TextBoxStocks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStocks.Location = New System.Drawing.Point(94, 159)
        Me.TextBoxStocks.Name = "TextBoxStocks"
        Me.TextBoxStocks.ReadOnly = True
        Me.TextBoxStocks.Size = New System.Drawing.Size(150, 20)
        Me.TextBoxStocks.TabIndex = 70
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(11, 162)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "Stocks"
        '
        'TextBoxLandingPages
        '
        Me.TextBoxLandingPages.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLandingPages.Location = New System.Drawing.Point(94, 135)
        Me.TextBoxLandingPages.Name = "TextBoxLandingPages"
        Me.TextBoxLandingPages.ReadOnly = True
        Me.TextBoxLandingPages.Size = New System.Drawing.Size(150, 20)
        Me.TextBoxLandingPages.TabIndex = 68
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 138)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 67
        Me.Label5.Text = "Landing pages"
        '
        'TextBoxMerchantId
        '
        Me.TextBoxMerchantId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxMerchantId.Location = New System.Drawing.Point(94, 107)
        Me.TextBoxMerchantId.Name = "TextBoxMerchantId"
        Me.TextBoxMerchantId.Size = New System.Drawing.Size(150, 20)
        Me.TextBoxMerchantId.TabIndex = 65
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 64
        Me.Label4.Text = "Merchant Id"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 361)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 63
        Me.Label3.Text = "Observacions"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(5, 377)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(405, 50)
        Me.TextBoxObs.TabIndex = 62
        '
        'CheckBoxActive
        '
        Me.CheckBoxActive.AutoSize = True
        Me.CheckBoxActive.Location = New System.Drawing.Point(88, 333)
        Me.CheckBoxActive.Name = "CheckBoxActive"
        Me.CheckBoxActive.Size = New System.Drawing.Size(59, 17)
        Me.CheckBoxActive.TabIndex = 61
        Me.CheckBoxActive.Text = "Activat"
        Me.CheckBoxActive.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Client"
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Emp = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(94, 81)
        Me.Xl_Contact21.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(306, 20)
        Me.Xl_Contact21.TabIndex = 58
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_WtbolLandingPages1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(412, 433)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Landing pages"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_WtbolLandingPages1
        '
        Me.Xl_WtbolLandingPages1.AllowUserToAddRows = False
        Me.Xl_WtbolLandingPages1.AllowUserToDeleteRows = False
        Me.Xl_WtbolLandingPages1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolLandingPages1.DisplayObsolets = False
        Me.Xl_WtbolLandingPages1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolLandingPages1.Filter = Nothing
        Me.Xl_WtbolLandingPages1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WtbolLandingPages1.MouseIsDown = False
        Me.Xl_WtbolLandingPages1.Name = "Xl_WtbolLandingPages1"
        Me.Xl_WtbolLandingPages1.ReadOnly = True
        Me.Xl_WtbolLandingPages1.Size = New System.Drawing.Size(406, 427)
        Me.Xl_WtbolLandingPages1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_WtbolStocks1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(412, 433)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Stocks"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_WtbolStocks1
        '
        Me.Xl_WtbolStocks1.AllowUserToAddRows = False
        Me.Xl_WtbolStocks1.AllowUserToDeleteRows = False
        Me.Xl_WtbolStocks1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolStocks1.DisplayObsolets = False
        Me.Xl_WtbolStocks1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolStocks1.Filter = Nothing
        Me.Xl_WtbolStocks1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WtbolStocks1.MouseIsDown = False
        Me.Xl_WtbolStocks1.Name = "Xl_WtbolStocks1"
        Me.Xl_WtbolStocks1.ReadOnly = True
        Me.Xl_WtbolStocks1.Size = New System.Drawing.Size(412, 433)
        Me.Xl_WtbolStocks1.TabIndex = 0
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_WtbolSiteClicks1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(412, 433)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Clicks"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_WtbolSiteClicks1
        '
        Me.Xl_WtbolSiteClicks1.AllowUserToAddRows = False
        Me.Xl_WtbolSiteClicks1.AllowUserToDeleteRows = False
        Me.Xl_WtbolSiteClicks1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolSiteClicks1.DisplayObsolets = False
        Me.Xl_WtbolSiteClicks1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolSiteClicks1.Filter = Nothing
        Me.Xl_WtbolSiteClicks1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_WtbolSiteClicks1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_WtbolSiteClicks1.MouseIsDown = False
        Me.Xl_WtbolSiteClicks1.Name = "Xl_WtbolSiteClicks1"
        Me.Xl_WtbolSiteClicks1.ReadOnly = True
        Me.Xl_WtbolSiteClicks1.RowTemplate.Height = 40
        Me.Xl_WtbolSiteClicks1.Size = New System.Drawing.Size(412, 433)
        Me.Xl_WtbolSiteClicks1.TabIndex = 0
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_WtbolBaskets1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(412, 433)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Baskets"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Xl_WtbolBaskets1
        '
        Me.Xl_WtbolBaskets1.AllowUserToAddRows = False
        Me.Xl_WtbolBaskets1.AllowUserToDeleteRows = False
        Me.Xl_WtbolBaskets1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_WtbolBaskets1.DisplayObsolets = False
        Me.Xl_WtbolBaskets1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_WtbolBaskets1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_WtbolBaskets1.MouseIsDown = False
        Me.Xl_WtbolBaskets1.Name = "Xl_WtbolBaskets1"
        Me.Xl_WtbolBaskets1.ReadOnly = True
        Me.Xl_WtbolBaskets1.Size = New System.Drawing.Size(406, 427)
        Me.Xl_WtbolBaskets1.TabIndex = 0
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(420, 24)
        Me.MenuStrip1.TabIndex = 59
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopiarGuidToolStripMenuItem, Me.FeedStocksParaHatchToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'CopiarGuidToolStripMenuItem
        '
        Me.CopiarGuidToolStripMenuItem.Name = "CopiarGuidToolStripMenuItem"
        Me.CopiarGuidToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.CopiarGuidToolStripMenuItem.Text = "Copiar Guid"
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(268, 3)
        Me.Xl_TextboxSearch1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 60
        Me.Xl_TextboxSearch1.Visible = False
        '
        'FeedStocksParaHatchToolStripMenuItem
        '
        Me.FeedStocksParaHatchToolStripMenuItem.Name = "FeedStocksParaHatchToolStripMenuItem"
        Me.FeedStocksParaHatchToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.FeedStocksParaHatchToolStripMenuItem.Text = "Feed stocks para Hatch"
        '
        'Frm_WtbolSite
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 529)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_WtbolSite"
        Me.Text = "WhereToBuyOnline Site"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_WtbolLandingPages1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_WtbolStocks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Xl_WtbolSiteClicks1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.Xl_WtbolBaskets1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxWeb As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents CheckBoxActive As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_WtbolLandingPages1 As Xl_WtbolLandingPages
    Friend WithEvents Xl_WtbolStocks1 As Xl_WtbolStocks
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents TextBoxMerchantId As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxStocks As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxLandingPages As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TextBoxContactTel As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxContactEmail As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxContactNom As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_WtbolSiteClicks1 As Xl_WtbolSiteClicks
    Friend WithEvents CopiarGuidToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents Xl_WtbolBaskets1 As Xl_WtbolBaskets
    Friend WithEvents FeedStocksParaHatchToolStripMenuItem As ToolStripMenuItem
End Class
