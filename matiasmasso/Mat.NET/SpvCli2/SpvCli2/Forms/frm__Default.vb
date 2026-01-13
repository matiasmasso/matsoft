Public Class frm__Default
    Inherits System.Windows.Forms.Form

    Private _requestToExit As Boolean

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        If CheckNetworkAvailability() Then
            AddHandler System.Net.NetworkInformation.NetworkChange.NetworkAvailabilityChanged, AddressOf CheckNetworkAvailabilityOnceLoaded

            InitializeComponent()
        End If


    End Sub

    Public Sub CheckNetworkAvailabilityOnceLoaded()
        If Not CheckNetworkAvailability() Then
            Me.Close()
            Application.Exit()
            Exit Sub
        End If
    End Sub


    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents TextBoxSpv As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSpvPrnt As System.Windows.Forms.Button
    Friend WithEvents NumericUpDownYea As System.Windows.Forms.NumericUpDown
    Friend WithEvents LabelOrg As System.Windows.Forms.Label
    Friend WithEvents ButtonSpvOutPrnt As System.Windows.Forms.Button
    Friend WithEvents ButtonPrntSpvOutEtq As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnSpvsNotRead As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonSpvIn As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents ButtonNewSpvOut As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MenuToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemLabelPrinterSelection As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelVersion As System.Windows.Forms.Label
    Friend WithEvents DesconnectarToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PrintPreviewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ApiToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HelpProviderHG As HelpProvider

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.Container

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm__Default))
        Me.NumericUpDownYea = New System.Windows.Forms.NumericUpDown()
        Me.TextBoxSpv = New System.Windows.Forms.TextBox()
        Me.ButtonSpvPrnt = New System.Windows.Forms.Button()
        Me.ButtonSpvOutPrnt = New System.Windows.Forms.Button()
        Me.LabelOrg = New System.Windows.Forms.Label()
        Me.ButtonPrntSpvOutEtq = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.BtnSpvsNotRead = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ButtonSpvIn = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.ButtonNewSpvOut = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.MenuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintPreviewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemLabelPrinterSelection = New System.Windows.Forms.ToolStripMenuItem()
        Me.ApiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DesconnectarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.HelpProviderHG = New System.Windows.Forms.HelpProvider()
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NumericUpDownYea
        '
        Me.NumericUpDownYea.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.NumericUpDownYea, "frm__Default.htm#NumericUpDownYea")
        Me.HelpProviderHG.SetHelpNavigator(Me.NumericUpDownYea, System.Windows.Forms.HelpNavigator.Topic)
        Me.NumericUpDownYea.Location = New System.Drawing.Point(27, 136)
        Me.NumericUpDownYea.Maximum = New Decimal(New Integer() {3000, 0, 0, 0})
        Me.NumericUpDownYea.Minimum = New Decimal(New Integer() {2001, 0, 0, 0})
        Me.NumericUpDownYea.Name = "NumericUpDownYea"
        Me.HelpProviderHG.SetShowHelp(Me.NumericUpDownYea, True)
        Me.NumericUpDownYea.Size = New System.Drawing.Size(66, 30)
        Me.NumericUpDownYea.TabIndex = 3
        Me.NumericUpDownYea.Value = New Decimal(New Integer() {2001, 0, 0, 0})
        '
        'TextBoxSpv
        '
        Me.TextBoxSpv.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBoxSpv, "frm__Default.htm#Label2")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBoxSpv, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBoxSpv.Location = New System.Drawing.Point(99, 135)
        Me.TextBoxSpv.Name = "TextBoxSpv"
        Me.HelpProviderHG.SetShowHelp(Me.TextBoxSpv, True)
        Me.TextBoxSpv.Size = New System.Drawing.Size(74, 30)
        Me.TextBoxSpv.TabIndex = 1
        '
        'ButtonSpvPrnt
        '
        Me.ButtonSpvPrnt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonSpvPrnt.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonSpvPrnt, "frm__Default.htm#ButtonSpvPrnt")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonSpvPrnt, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonSpvPrnt.Location = New System.Drawing.Point(26, 194)
        Me.ButtonSpvPrnt.Name = "ButtonSpvPrnt"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonSpvPrnt, True)
        Me.ButtonSpvPrnt.Size = New System.Drawing.Size(262, 35)
        Me.ButtonSpvPrnt.TabIndex = 2
        Me.ButtonSpvPrnt.Text = "imprimir entrada"
        '
        'ButtonSpvOutPrnt
        '
        Me.ButtonSpvOutPrnt.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonSpvOutPrnt.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonSpvOutPrnt, "frm__Default.htm#ButtonSpvOutPrnt")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonSpvOutPrnt, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonSpvOutPrnt.Location = New System.Drawing.Point(26, 239)
        Me.ButtonSpvOutPrnt.Name = "ButtonSpvOutPrnt"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonSpvOutPrnt, True)
        Me.ButtonSpvOutPrnt.Size = New System.Drawing.Size(262, 35)
        Me.ButtonSpvOutPrnt.TabIndex = 2
        Me.ButtonSpvOutPrnt.Text = "imprimir salida"
        '
        'LabelOrg
        '
        Me.LabelOrg.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelOrg.ForeColor = System.Drawing.Color.Black
        Me.LabelOrg.Location = New System.Drawing.Point(162, 12)
        Me.LabelOrg.Name = "LabelOrg"
        Me.LabelOrg.Size = New System.Drawing.Size(408, 24)
        Me.LabelOrg.TabIndex = 5
        Me.LabelOrg.Text = "Connectant amb el servidor ..."
        '
        'ButtonPrntSpvOutEtq
        '
        Me.ButtonPrntSpvOutEtq.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ButtonPrntSpvOutEtq.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonPrntSpvOutEtq, "frm__Default.htm#ButtonPrntSpvOutEtq")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonPrntSpvOutEtq, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonPrntSpvOutEtq.Location = New System.Drawing.Point(26, 280)
        Me.ButtonPrntSpvOutEtq.Name = "ButtonPrntSpvOutEtq"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonPrntSpvOutEtq, True)
        Me.ButtonPrntSpvOutEtq.Size = New System.Drawing.Size(262, 35)
        Me.ButtonPrntSpvOutEtq.TabIndex = 2
        Me.ButtonPrntSpvOutEtq.Text = "imprimir etiqueta"
        '
        'PictureBox1
        '
        Me.PictureBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBox1.Image = Global.SpvCli2.My.Resources.Resources.LogopMMO_48
        Me.PictureBox1.Location = New System.Drawing.Point(712, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(50, 49)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.BtnSpvsNotRead)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(27, 101)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(309, 104)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Baixar fulls de reparació"
        '
        'TextBox1
        '
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBox1, "frm__Default.htm#TextBox1")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBox1, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBox1.Location = New System.Drawing.Point(10, 28)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBox1, True)
        Me.TextBox1.Size = New System.Drawing.Size(194, 65)
        Me.TextBox1.TabIndex = 3
        Me.TextBox1.Text = "Comprova si hi han nous fulls de treball pendents de llegir i els imprimeix"
        '
        'BtnSpvsNotRead
        '
        Me.BtnSpvsNotRead.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.HelpProviderHG.SetHelpKeyword(Me.BtnSpvsNotRead, "frm__Default.htm#BtnSpvsNotRead")
        Me.HelpProviderHG.SetHelpNavigator(Me.BtnSpvsNotRead, System.Windows.Forms.HelpNavigator.Topic)
        Me.BtnSpvsNotRead.Image = Global.SpvCli2.My.Resources.Resources.download
        Me.BtnSpvsNotRead.Location = New System.Drawing.Point(221, 23)
        Me.BtnSpvsNotRead.Name = "BtnSpvsNotRead"
        Me.HelpProviderHG.SetShowHelp(Me.BtnSpvsNotRead, True)
        Me.BtnSpvsNotRead.Size = New System.Drawing.Size(75, 70)
        Me.BtnSpvsNotRead.TabIndex = 1
        Me.BtnSpvsNotRead.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TextBox2)
        Me.GroupBox2.Controls.Add(Me.ButtonSpvIn)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(27, 213)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(309, 104)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Registrar entrada de paquetería"
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBox2, "frm__Default.htm#TextBox2")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBox2, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBox2.Location = New System.Drawing.Point(10, 28)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBox2, True)
        Me.TextBox2.Size = New System.Drawing.Size(194, 65)
        Me.TextBox2.TabIndex = 3
        Me.TextBox2.Text = "Quan arriba el transportista, seleccionem els fulls de treball que corresponen a " &
    "la mercancía que ens ha portat"
        '
        'ButtonSpvIn
        '
        Me.ButtonSpvIn.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonSpvIn, "frm__Default.htm#ButtonSpvIn")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonSpvIn, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonSpvIn.Image = Global.SpvCli2.My.Resources.Resources.unpack
        Me.ButtonSpvIn.Location = New System.Drawing.Point(221, 23)
        Me.ButtonSpvIn.Name = "ButtonSpvIn"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonSpvIn, True)
        Me.ButtonSpvIn.Size = New System.Drawing.Size(75, 70)
        Me.ButtonSpvIn.TabIndex = 1
        Me.ButtonSpvIn.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TextBox3)
        Me.GroupBox3.Controls.Add(Me.ButtonNewSpvOut)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(27, 325)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(309, 104)
        Me.GroupBox3.TabIndex = 10
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Albará per el client"
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBox3, "frm__Default.htm#TextBox3")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBox3, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBox3.Location = New System.Drawing.Point(10, 28)
        Me.TextBox3.Multiline = True
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBox3, True)
        Me.TextBox3.Size = New System.Drawing.Size(194, 65)
        Me.TextBox3.TabIndex = 3
        Me.TextBox3.Text = "Un cop reparat, redactem la documentació de client segons la feina feta"
        '
        'ButtonNewSpvOut
        '
        Me.ButtonNewSpvOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.HelpProviderHG.SetHelpKeyword(Me.ButtonNewSpvOut, "frm__Default.htm#ButtonNewSpvOut")
        Me.HelpProviderHG.SetHelpNavigator(Me.ButtonNewSpvOut, System.Windows.Forms.HelpNavigator.Topic)
        Me.ButtonNewSpvOut.Image = Global.SpvCli2.My.Resources.Resources.SpvOut
        Me.ButtonNewSpvOut.Location = New System.Drawing.Point(221, 23)
        Me.ButtonNewSpvOut.Name = "ButtonNewSpvOut"
        Me.HelpProviderHG.SetShowHelp(Me.ButtonNewSpvOut, True)
        Me.ButtonNewSpvOut.Size = New System.Drawing.Size(75, 70)
        Me.ButtonNewSpvOut.TabIndex = 1
        Me.ButtonNewSpvOut.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.PictureBox2)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.TextBox5)
        Me.GroupBox5.Controls.Add(Me.NumericUpDownYea)
        Me.GroupBox5.Controls.Add(Me.TextBoxSpv)
        Me.GroupBox5.Controls.Add(Me.ButtonSpvPrnt)
        Me.GroupBox5.Controls.Add(Me.ButtonSpvOutPrnt)
        Me.GroupBox5.Controls.Add(Me.ButtonPrntSpvOutEtq)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(445, 101)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(309, 328)
        Me.GroupBox5.TabIndex = 12
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Tornar a imprimir documentació"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.SpvCli2.My.Resources.Resources.printer_InkJet
        Me.PictureBox2.Location = New System.Drawing.Point(240, 118)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(48, 48)
        Me.PictureBox2.TabIndex = 8
        Me.PictureBox2.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(96, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "número:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 113)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "any:"
        '
        'TextBox5
        '
        Me.TextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox5.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.HelpProviderHG.SetHelpKeyword(Me.TextBox5, "frm__Default.htm#TextBox5")
        Me.HelpProviderHG.SetHelpNavigator(Me.TextBox5, System.Windows.Forms.HelpNavigator.Topic)
        Me.TextBox5.Location = New System.Drawing.Point(10, 28)
        Me.TextBox5.Multiline = True
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.ReadOnly = True
        Me.HelpProviderHG.SetShowHelp(Me.TextBox5, True)
        Me.TextBox5.Size = New System.Drawing.Size(281, 60)
        Me.TextBox5.TabIndex = 3
        Me.TextBox5.Text = "Si sens ha fet malbé algun paper del que sabem el número, podem tornar-lo a impri" &
    "mir desde aquesta secció"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(770, 24)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MenuToolStripMenuItem
        '
        Me.MenuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintPreviewToolStripMenuItem, Me.ToolStripMenuItemLabelPrinterSelection, Me.ApiToolStripMenuItem, Me.DesconnectarToolStripMenuItem})
        Me.MenuToolStripMenuItem.Name = "MenuToolStripMenuItem"
        Me.MenuToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.MenuToolStripMenuItem.Text = "menu"
        '
        'PrintPreviewToolStripMenuItem
        '
        Me.PrintPreviewToolStripMenuItem.CheckOnClick = True
        Me.PrintPreviewToolStripMenuItem.Name = "PrintPreviewToolStripMenuItem"
        Me.PrintPreviewToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.PrintPreviewToolStripMenuItem.Text = "print preview"
        '
        'ToolStripMenuItemLabelPrinterSelection
        '
        Me.ToolStripMenuItemLabelPrinterSelection.Name = "ToolStripMenuItemLabelPrinterSelection"
        Me.ToolStripMenuItemLabelPrinterSelection.Size = New System.Drawing.Size(240, 22)
        Me.ToolStripMenuItemLabelPrinterSelection.Text = "seleccionar impresora etiquetes"
        '
        'ApiToolStripMenuItem
        '
        Me.ApiToolStripMenuItem.CheckOnClick = True
        Me.ApiToolStripMenuItem.Name = "ApiToolStripMenuItem"
        Me.ApiToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.ApiToolStripMenuItem.Text = "Api local"
        Me.ApiToolStripMenuItem.Visible = False
        '
        'DesconnectarToolStripMenuItem
        '
        Me.DesconnectarToolStripMenuItem.Name = "DesconnectarToolStripMenuItem"
        Me.DesconnectarToolStripMenuItem.Size = New System.Drawing.Size(240, 22)
        Me.DesconnectarToolStripMenuItem.Text = "desconnectar"
        '
        'LabelVersion
        '
        Me.LabelVersion.AutoSize = True
        Me.LabelVersion.Location = New System.Drawing.Point(709, 64)
        Me.LabelVersion.Name = "LabelVersion"
        Me.LabelVersion.Size = New System.Drawing.Size(50, 13)
        Me.LabelVersion.TabIndex = 14
        Me.LabelVersion.Text = "version..."
        Me.LabelVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'HelpProviderHG
        '
        Me.HelpProviderHG.HelpNamespace = "MatNET.chm"
        '
        'frm__Default
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(770, 443)
        Me.Controls.Add(Me.LabelVersion)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LabelOrg)
        Me.Controls.Add(Me.MenuStrip1)
        Me.HelpProviderHG.SetHelpKeyword(Me, "frm__Default.htm")
        Me.HelpProviderHG.SetHelpNavigator(Me, System.Windows.Forms.HelpNavigator.Topic)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "frm__Default"
        Me.HelpProviderHG.SetShowHelp(Me, True)
        Me.Text = "Servei Tècnic"
        CType(Me.NumericUpDownYea, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region




    Private Async Sub frm__Default_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim exs As New List(Of Exception)
        Cursor = Cursors.WaitCursor

        DTOApp.Current = Await FEB.App.InitializeAsync(exs, DTOApp.AppTypes.spv, "https://api.matiasmasso.es", 55836)

        GlobalVariables.Emp = Await FEB.Emp.Find(DTOEmp.Ids.MatiasMasso, exs)
        If exs.Count = 0 Then
            With GlobalVariables.Emp
                .Org = FEB.Contact.FindSync(.Org.Guid, exs)
                .Mgz = DTOMgz.FromContact(Await FEB.Contact.Find(.Mgz.Guid, exs))
            End With

            If Await SessionHelper.AddSession(DTOEmp.Ids.MatiasMasso, DTOApp.AppTypes.Spv, DTOLang.Ids.CAT, DTOCur.Ids.EUR, exs) Then
                ApiToolStripMenuItem.Visible = Current.Session.User.Equals(DTOUser.Wellknown(DTOUser.Wellknowns.matias))
                LabelOrg.Text = GlobalVariables.Emp.Org.Nom
                Cursor = Cursors.Default
                SetPrintPreview()
                LabelVersion.Text = version()
                NumericUpDownYea.Value = Year(Today)
            Else
                UIHelper.WarnError(exs, "imposible iniciar la aplicació")
            End If
        Else
            Cursor = Cursors.Default
            UIHelper.WarnError(exs, "No hi ha connexíó")
            Me.Close()
            Application.Exit()
            Exit Sub
        End If

    End Sub


    Public Function version() As String
        Dim s As String = ""
        Try
            With My.Application.Deployment.CurrentVersion
                s = .Major & "." & .Minor & "." & .Revision
            End With

            If (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) Then
                Dim ver As Version
                ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion
                s = String.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision)
            Else
                s = "Versió no descarregada "
            End If

        Catch ex As Exception
        End Try

        Return s
    End Function

    Private Sub BtnNewSpvIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSpvIn.Click
        root.ShowNewSpvsIn()
    End Sub

    Private Async Sub ButtonSpvPrnt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonSpvPrnt.Click
        Dim IntYea As Integer = NumericUpDownYea.Value
        Dim iId As Integer

        If IsNumeric(TextBoxSpv.Text) Then
            iId = TextBoxSpv.Text
        Else
            MsgBox("falta el número", MsgBoxStyle.Exclamation, "Servei Tècnic")
            Exit Sub
        End If

        Dim exs As New List(Of Exception)
        Dim oSpv As DTOSpv = Await FEB.Spv.FromId(GlobalVariables.Emp, IntYea, iId, exs)
        If exs.Count = 0 Then
            If oSpv Is Nothing Then
                MsgBox("No hi ha cap reparació amb aquest número", MsgBoxStyle.Exclamation, "Servei Tècnic")
            Else
                Dim oSpvs As New List(Of DTOSpv)
                oSpvs.Add(oSpv)
                Dim rpt As New rpt_SPV(oSpvs)
                If PRINT_PREVIEWS Then
                    rpt.Printpreview()
                Else
                    rpt.Print()
                End If
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub BtnSpvsNotRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSpvsNotRead.Click
        Dim exs As New List(Of Exception)
        Dim oSpvs = Await FEB.Spvs.SpvsNotRead(Current.Session.User, exs)
        If exs.Count = 0 Then
            If oSpvs.Count = 0 Then
                MsgBox("No hay hojas para bajar", MsgBoxStyle.Information, "Servei Tècnic")
            Else
                PrintSpvs(oSpvs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonSpvOutPrnt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonSpvOutPrnt.Click
        Dim IntYea As Integer = NumericUpDownYea.Value
        Dim LngId As Long

        If IsNumeric(TextBoxSpv.Text) Then
            LngId = TextBoxSpv.Text
        Else
            MsgBox("falta el número", MsgBoxStyle.Exclamation, "Servei Tècnic")
            Exit Sub
        End If

        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = Await FEB.Delivery.FromNum(GlobalVariables.Emp, IntYea, LngId, exs)
        If exs.Count = 0 Then
            If oDelivery Is Nothing Then
                MsgBox("No hi ha cap sortida amb aquest número", MsgBoxStyle.Exclamation, "Servei Tècnic")
            Else
                If oDelivery.cod <> 4 Then
                    MsgBox("este número no corresponde al taller!", MsgBoxStyle.Exclamation, "Servei Tècnic")
                End If
            End If

            root.PrintDelivery(oDelivery)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ButtonPrntSpvOutEtq_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonPrntSpvOutEtq.Click
        Dim IntYea As Integer = NumericUpDownYea.Value
        Dim LngId As Long

        If IsNumeric(TextBoxSpv.Text) Then
            LngId = TextBoxSpv.Text
        Else
            MsgBox("falta el número", MsgBoxStyle.Exclamation, "Servei Tècnic")
            Exit Sub
        End If

        Me.Cursor = Cursors.AppStarting
        Dim exs As New List(Of Exception)
        Dim oDelivery As DTODelivery = Await FEB.Delivery.FromNum(GlobalVariables.Emp, IntYea, LngId, exs)
        If exs.Count = 0 Then
            root.PrintDeliveryLabel(oDelivery)
            Me.Cursor = Cursors.Default
        Else
            Me.Cursor = Cursors.Default
            UIHelper.WarnError(exs)
        End If
    End Sub



    Private Sub ButtonNewSpvOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonNewSpvOut.Click
        root.ShowNewSpvOut()
    End Sub


    Private Sub ToolStripMenuItemLabelPrinterSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemLabelPrinterSelection.Click
        Frm_LabelPrinterNotFound.Show()
    End Sub

    Private Async Sub DesconnectarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DesconnectarToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If Await FEB.Session.Close(exs, Current.Session) Then
            SaveSetting("MatSoft", "MAT.NET", DTOSession.CookiePersistName, "0")
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub SetPrintPreview()
        Dim sBool = GetSetting("MatSoft", "SpvCli", "PrintPreviews")
        If sBool > "" Then
            PRINT_PREVIEWS = sBool
            PrintPreviewToolStripMenuItem.Checked = PRINT_PREVIEWS
        End If
    End Sub

    Private Sub PrintPreviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PrintPreviewToolStripMenuItem.Click
        PRINT_PREVIEWS = PrintPreviewToolStripMenuItem.Checked
        SaveSetting("MatSoft", "SpvCli", "PrintPreviews", PRINT_PREVIEWS)
    End Sub

    Private Sub ApiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ApiToolStripMenuItem.Click
        FEB.Api.UseLocalApi(ApiToolStripMenuItem.Checked)
    End Sub
End Class
