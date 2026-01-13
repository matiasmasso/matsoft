<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Facturacio_Old
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Facturacio_Old))
        Me.Splitter1 = New System.Windows.Forms.Splitter()
        Me.CheckBoxCash = New System.Windows.Forms.CheckBox()
        Me.CheckBoxCredit = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerLast = New System.Windows.Forms.DateTimePicker()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonEnd = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.PanelFrxNew = New System.Windows.Forms.Panel()
        Me.CheckBoxSmallVolume = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFraPerMes = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPreVto = New System.Windows.Forms.CheckBox()
        Me.CheckBoxFacturarTot = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNegatives = New System.Windows.Forms.CheckBox()
        Me.CheckBoxExport = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerFirst = New System.Windows.Forms.DateTimePicker()
        Me.LabelLastFra = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DataGridViewWarn = New System.Windows.Forms.DataGridView()
        Me.RadioButtonFrxNew = New System.Windows.Forms.RadioButton()
        Me.TabPageCheck = New System.Windows.Forms.TabPage()
        Me.ProgressBarDist = New System.Windows.Forms.ProgressBar()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageFch = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TabPageDist = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridViewClis = New System.Windows.Forms.DataGridView()
        Me.CheckBoxReq = New System.Windows.Forms.CheckBox()
        Me.CheckBoxIva = New System.Windows.Forms.CheckBox()
        Me.LabelJoinAlbs = New System.Windows.Forms.Label()
        Me.TextBoxOb3 = New System.Windows.Forms.TextBox()
        Me.TextBoxOb2 = New System.Windows.Forms.TextBox()
        Me.TextBoxOb1 = New System.Windows.Forms.TextBox()
        Me.TextBoxFpg = New System.Windows.Forms.TextBox()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.ComboBoxCfp = New System.Windows.Forms.ComboBox()
        Me.LabelFpg = New System.Windows.Forms.Label()
        Me.TreeViewFras = New System.Windows.Forms.TreeView()
        Me.ImageListFolders = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPageEnd = New System.Windows.Forms.TabPage()
        Me.LabelStatusSave = New System.Windows.Forms.Label()
        Me.ProgressBarSave = New System.Windows.Forms.ProgressBar()
        Me.ContextMenuClis = New System.Windows.Forms.ContextMenu()
        Me.MenuItemTvFrasSetNum = New System.Windows.Forms.MenuItem()
        Me.MenuItemTvFrasNewFra = New System.Windows.Forms.MenuItem()
        Me.MenuItemTvFrasFacturarEn = New System.Windows.Forms.MenuItem()
        Me.ContextMenuTvFras = New System.Windows.Forms.ContextMenu()
        Me.MenuItemTvFrasZoom = New System.Windows.Forms.MenuItem()
        Me.MenuItemTvFrasRemove = New System.Windows.Forms.MenuItem()
        Me.PanelFrxNew.SuspendLayout()
        CType(Me.DataGridViewWarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageCheck.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageFch.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPageDist.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewClis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageEnd.SuspendLayout()
        Me.SuspendLayout()
        '
        'Splitter1
        '
        Me.Splitter1.Location = New System.Drawing.Point(0, 0)
        Me.Splitter1.Name = "Splitter1"
        Me.Splitter1.Size = New System.Drawing.Size(3, 529)
        Me.Splitter1.TabIndex = 3
        Me.Splitter1.TabStop = False
        '
        'CheckBoxCash
        '
        Me.CheckBoxCash.Location = New System.Drawing.Point(186, 135)
        Me.CheckBoxCash.Name = "CheckBoxCash"
        Me.CheckBoxCash.Size = New System.Drawing.Size(88, 16)
        Me.CheckBoxCash.TabIndex = 34
        Me.CheckBoxCash.Text = "cash"
        '
        'CheckBoxCredit
        '
        Me.CheckBoxCredit.Checked = True
        Me.CheckBoxCredit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxCredit.Location = New System.Drawing.Point(186, 119)
        Me.CheckBoxCredit.Name = "CheckBoxCredit"
        Me.CheckBoxCredit.Size = New System.Drawing.Size(88, 16)
        Me.CheckBoxCredit.TabIndex = 33
        Me.CheckBoxCredit.Text = "Credit"
        '
        'DateTimePickerLast
        '
        Me.DateTimePickerLast.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerLast.Location = New System.Drawing.Point(168, 56)
        Me.DateTimePickerLast.Name = "DateTimePickerLast"
        Me.DateTimePickerLast.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerLast.TabIndex = 32
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.Location = New System.Drawing.Point(12, 567)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(86, 23)
        Me.ButtonCancel.TabIndex = 16
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonEnd
        '
        Me.ButtonEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonEnd.Location = New System.Drawing.Point(708, 568)
        Me.ButtonEnd.Name = "ButtonEnd"
        Me.ButtonEnd.Size = New System.Drawing.Size(86, 23)
        Me.ButtonEnd.TabIndex = 15
        Me.ButtonEnd.Text = "FINALIZAR >>"
        '
        'ButtonNext
        '
        Me.ButtonNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonNext.Location = New System.Drawing.Point(612, 568)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(86, 23)
        Me.ButtonNext.TabIndex = 14
        Me.ButtonNext.Text = "SIGUIENTE >"
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonPrevious.Enabled = False
        Me.ButtonPrevious.Location = New System.Drawing.Point(516, 567)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(86, 23)
        Me.ButtonPrevious.TabIndex = 13
        Me.ButtonPrevious.Text = "< ATRAS"
        '
        'PanelFrxNew
        '
        Me.PanelFrxNew.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxSmallVolume)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxFraPerMes)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxPreVto)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxFacturarTot)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxNegatives)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxExport)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxCash)
        Me.PanelFrxNew.Controls.Add(Me.CheckBoxCredit)
        Me.PanelFrxNew.Controls.Add(Me.DateTimePickerLast)
        Me.PanelFrxNew.Controls.Add(Me.DateTimePickerFirst)
        Me.PanelFrxNew.Controls.Add(Me.LabelLastFra)
        Me.PanelFrxNew.Controls.Add(Me.Label3)
        Me.PanelFrxNew.Controls.Add(Me.Label2)
        Me.PanelFrxNew.Controls.Add(Me.Label1)
        Me.PanelFrxNew.Location = New System.Drawing.Point(16, 40)
        Me.PanelFrxNew.Name = "PanelFrxNew"
        Me.PanelFrxNew.Size = New System.Drawing.Size(720, 395)
        Me.PanelFrxNew.TabIndex = 18
        '
        'CheckBoxSmallVolume
        '
        Me.CheckBoxSmallVolume.Location = New System.Drawing.Point(186, 220)
        Me.CheckBoxSmallVolume.Name = "CheckBoxSmallVolume"
        Me.CheckBoxSmallVolume.Size = New System.Drawing.Size(179, 16)
        Me.CheckBoxSmallVolume.TabIndex = 40
        Me.CheckBoxSmallVolume.Text = "petits imports (100,00 €)"
        '
        'CheckBoxFraPerMes
        '
        Me.CheckBoxFraPerMes.Location = New System.Drawing.Point(186, 202)
        Me.CheckBoxFraPerMes.Name = "CheckBoxFraPerMes"
        Me.CheckBoxFraPerMes.Size = New System.Drawing.Size(179, 16)
        Me.CheckBoxFraPerMes.TabIndex = 39
        Me.CheckBoxFraPerMes.Text = "una sola factura mensual"
        '
        'CheckBoxPreVto
        '
        Me.CheckBoxPreVto.Location = New System.Drawing.Point(186, 184)
        Me.CheckBoxPreVto.Name = "CheckBoxPreVto"
        Me.CheckBoxPreVto.Size = New System.Drawing.Size(213, 16)
        Me.CheckBoxPreVto.TabIndex = 38
        Me.CheckBoxPreVto.Text = "encara que no calgui per el venciment"
        '
        'CheckBoxFacturarTot
        '
        Me.CheckBoxFacturarTot.Location = New System.Drawing.Point(168, 97)
        Me.CheckBoxFacturarTot.Name = "CheckBoxFacturarTot"
        Me.CheckBoxFacturarTot.Size = New System.Drawing.Size(136, 16)
        Me.CheckBoxFacturarTot.TabIndex = 37
        Me.CheckBoxFacturarTot.Text = "facturar-ho tot"
        '
        'CheckBoxNegatives
        '
        Me.CheckBoxNegatives.Location = New System.Drawing.Point(186, 167)
        Me.CheckBoxNegatives.Name = "CheckBoxNegatives"
        Me.CheckBoxNegatives.Size = New System.Drawing.Size(88, 16)
        Me.CheckBoxNegatives.TabIndex = 36
        Me.CheckBoxNegatives.Text = "negatives"
        '
        'CheckBoxExport
        '
        Me.CheckBoxExport.Location = New System.Drawing.Point(186, 151)
        Me.CheckBoxExport.Name = "CheckBoxExport"
        Me.CheckBoxExport.Size = New System.Drawing.Size(88, 16)
        Me.CheckBoxExport.TabIndex = 35
        Me.CheckBoxExport.Text = "export"
        '
        'DateTimePickerFirst
        '
        Me.DateTimePickerFirst.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFirst.Location = New System.Drawing.Point(168, 32)
        Me.DateTimePickerFirst.Name = "DateTimePickerFirst"
        Me.DateTimePickerFirst.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePickerFirst.TabIndex = 31
        '
        'LabelLastFra
        '
        Me.LabelLastFra.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelLastFra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelLastFra.Location = New System.Drawing.Point(168, 8)
        Me.LabelLastFra.Name = "LabelLastFra"
        Me.LabelLastFra.Size = New System.Drawing.Size(533, 20)
        Me.LabelLastFra.TabIndex = 30
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(16, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(144, 16)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "Darrera data de facturació:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(144, 16)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Propera data de facturació:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(16, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Última factura registrada:"
        '
        'DataGridViewWarn
        '
        Me.DataGridViewWarn.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewWarn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewWarn.Location = New System.Drawing.Point(4, 93)
        Me.DataGridViewWarn.Name = "DataGridViewWarn"
        Me.DataGridViewWarn.ReadOnly = True
        Me.DataGridViewWarn.Size = New System.Drawing.Size(776, 433)
        Me.DataGridViewWarn.TabIndex = 37
        '
        'RadioButtonFrxNew
        '
        Me.RadioButtonFrxNew.Checked = True
        Me.RadioButtonFrxNew.Location = New System.Drawing.Point(8, 16)
        Me.RadioButtonFrxNew.Name = "RadioButtonFrxNew"
        Me.RadioButtonFrxNew.Size = New System.Drawing.Size(112, 16)
        Me.RadioButtonFrxNew.TabIndex = 0
        Me.RadioButtonFrxNew.TabStop = True
        Me.RadioButtonFrxNew.Text = "Nova facturació"
        '
        'TabPageCheck
        '
        Me.TabPageCheck.Controls.Add(Me.ProgressBarDist)
        Me.TabPageCheck.Controls.Add(Me.DataGridViewWarn)
        Me.TabPageCheck.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCheck.Name = "TabPageCheck"
        Me.TabPageCheck.Size = New System.Drawing.Size(784, 529)
        Me.TabPageCheck.TabIndex = 3
        Me.TabPageCheck.Text = "Control"
        '
        'ProgressBarDist
        '
        Me.ProgressBarDist.Location = New System.Drawing.Point(3, 41)
        Me.ProgressBarDist.Name = "ProgressBarDist"
        Me.ProgressBarDist.Size = New System.Drawing.Size(781, 23)
        Me.ProgressBarDist.TabIndex = 38
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageFch)
        Me.TabControl1.Controls.Add(Me.TabPageCheck)
        Me.TabControl1.Controls.Add(Me.TabPageDist)
        Me.TabControl1.Controls.Add(Me.TabPageEnd)
        Me.TabControl1.Location = New System.Drawing.Point(4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(792, 555)
        Me.TabControl1.TabIndex = 12
        '
        'TabPageFch
        '
        Me.TabPageFch.Controls.Add(Me.GroupBox1)
        Me.TabPageFch.Location = New System.Drawing.Point(4, 22)
        Me.TabPageFch.Name = "TabPageFch"
        Me.TabPageFch.Size = New System.Drawing.Size(784, 529)
        Me.TabPageFch.TabIndex = 0
        Me.TabPageFch.Text = "Dates"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.PanelFrxNew)
        Me.GroupBox1.Controls.Add(Me.RadioButtonFrxNew)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(760, 384)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        '
        'TabPageDist
        '
        Me.TabPageDist.Controls.Add(Me.SplitContainer1)
        Me.TabPageDist.Controls.Add(Me.Splitter1)
        Me.TabPageDist.Location = New System.Drawing.Point(4, 22)
        Me.TabPageDist.Name = "TabPageDist"
        Me.TabPageDist.Size = New System.Drawing.Size(784, 529)
        Me.TabPageDist.TabIndex = 1
        Me.TabPageDist.Text = "Distribució"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewClis)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxReq)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxIva)
        Me.SplitContainer1.Panel2.Controls.Add(Me.LabelJoinAlbs)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxOb3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxOb2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxOb1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxFpg)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateTimePickerVto)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboBoxCfp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.LabelFpg)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TreeViewFras)
        Me.SplitContainer1.Size = New System.Drawing.Size(781, 529)
        Me.SplitContainer1.SplitterDistance = 379
        Me.SplitContainer1.TabIndex = 4
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'DataGridViewClis
        '
        Me.DataGridViewClis.AllowUserToAddRows = False
        Me.DataGridViewClis.AllowUserToDeleteRows = False
        Me.DataGridViewClis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewClis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewClis.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewClis.Name = "DataGridViewClis"
        Me.DataGridViewClis.ReadOnly = True
        Me.DataGridViewClis.Size = New System.Drawing.Size(379, 529)
        Me.DataGridViewClis.TabIndex = 0
        '
        'CheckBoxReq
        '
        Me.CheckBoxReq.AutoSize = True
        Me.CheckBoxReq.Enabled = False
        Me.CheckBoxReq.Location = New System.Drawing.Point(74, 406)
        Me.CheckBoxReq.Name = "CheckBoxReq"
        Me.CheckBoxReq.Size = New System.Drawing.Size(134, 17)
        Me.CheckBoxReq.TabIndex = 34
        Me.CheckBoxReq.Text = "Recarrec Equivalencia"
        Me.CheckBoxReq.UseVisualStyleBackColor = True
        '
        'CheckBoxIva
        '
        Me.CheckBoxIva.AutoSize = True
        Me.CheckBoxIva.Enabled = False
        Me.CheckBoxIva.Location = New System.Drawing.Point(9, 407)
        Me.CheckBoxIva.Name = "CheckBoxIva"
        Me.CheckBoxIva.Size = New System.Drawing.Size(43, 17)
        Me.CheckBoxIva.TabIndex = 33
        Me.CheckBoxIva.Text = "IVA"
        Me.CheckBoxIva.UseVisualStyleBackColor = True
        '
        'LabelJoinAlbs
        '
        Me.LabelJoinAlbs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelJoinAlbs.Location = New System.Drawing.Point(12, 425)
        Me.LabelJoinAlbs.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.LabelJoinAlbs.Name = "LabelJoinAlbs"
        Me.LabelJoinAlbs.Size = New System.Drawing.Size(292, 16)
        Me.LabelJoinAlbs.TabIndex = 32
        '
        'TextBoxOb3
        '
        Me.TextBoxOb3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOb3.Location = New System.Drawing.Point(12, 506)
        Me.TextBoxOb3.MaxLength = 56
        Me.TextBoxOb3.Name = "TextBoxOb3"
        Me.TextBoxOb3.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxOb3.TabIndex = 31
        '
        'TextBoxOb2
        '
        Me.TextBoxOb2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOb2.Location = New System.Drawing.Point(12, 486)
        Me.TextBoxOb2.MaxLength = 56
        Me.TextBoxOb2.Name = "TextBoxOb2"
        Me.TextBoxOb2.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxOb2.TabIndex = 30
        '
        'TextBoxOb1
        '
        Me.TextBoxOb1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOb1.Location = New System.Drawing.Point(12, 466)
        Me.TextBoxOb1.MaxLength = 56
        Me.TextBoxOb1.Name = "TextBoxOb1"
        Me.TextBoxOb1.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxOb1.TabIndex = 29
        '
        'TextBoxFpg
        '
        Me.TextBoxFpg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFpg.Location = New System.Drawing.Point(12, 446)
        Me.TextBoxFpg.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxFpg.MaxLength = 56
        Me.TextBoxFpg.Name = "TextBoxFpg"
        Me.TextBoxFpg.Size = New System.Drawing.Size(383, 20)
        Me.TextBoxFpg.TabIndex = 28
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(305, 380)
        Me.DateTimePickerVto.Margin = New System.Windows.Forms.Padding(0, 3, 3, 3)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(87, 20)
        Me.DateTimePickerVto.TabIndex = 21
        '
        'ComboBoxCfp
        '
        Me.ComboBoxCfp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxCfp.DisplayMember = "nom"
        Me.ComboBoxCfp.FormattingEnabled = True
        Me.ComboBoxCfp.Location = New System.Drawing.Point(10, 380)
        Me.ComboBoxCfp.Margin = New System.Windows.Forms.Padding(3, 3, 1, 2)
        Me.ComboBoxCfp.Name = "ComboBoxCfp"
        Me.ComboBoxCfp.Size = New System.Drawing.Size(294, 21)
        Me.ComboBoxCfp.TabIndex = 20
        Me.ComboBoxCfp.ValueMember = "Id"
        '
        'LabelFpg
        '
        Me.LabelFpg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelFpg.Location = New System.Drawing.Point(4, 312)
        Me.LabelFpg.Name = "LabelFpg"
        Me.LabelFpg.Size = New System.Drawing.Size(386, 61)
        Me.LabelFpg.TabIndex = 5
        '
        'TreeViewFras
        '
        Me.TreeViewFras.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeViewFras.ImageIndex = 0
        Me.TreeViewFras.ImageList = Me.ImageListFolders
        Me.TreeViewFras.Location = New System.Drawing.Point(1, 0)
        Me.TreeViewFras.Name = "TreeViewFras"
        Me.TreeViewFras.SelectedImageIndex = 0
        Me.TreeViewFras.Size = New System.Drawing.Size(391, 305)
        Me.TreeViewFras.TabIndex = 4
        '
        'ImageListFolders
        '
        Me.ImageListFolders.ImageStream = CType(resources.GetObject("ImageListFolders.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListFolders.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListFolders.Images.SetKeyName(0, "FolderClosedGray.gif")
        Me.ImageListFolders.Images.SetKeyName(1, "FolderClosedYellow.gif")
        Me.ImageListFolders.Images.SetKeyName(2, "FolderClosedRed.gif")
        Me.ImageListFolders.Images.SetKeyName(3, "FolderClosedBlue.gif")
        Me.ImageListFolders.Images.SetKeyName(4, "")
        Me.ImageListFolders.Images.SetKeyName(5, "")
        Me.ImageListFolders.Images.SetKeyName(6, "")
        Me.ImageListFolders.Images.SetKeyName(7, "")
        Me.ImageListFolders.Images.SetKeyName(8, "Doc.gif")
        Me.ImageListFolders.Images.SetKeyName(9, "DocPink.gif")
        Me.ImageListFolders.Images.SetKeyName(10, "DocBlue.gif")
        Me.ImageListFolders.Images.SetKeyName(11, "DocYellow.gif")
        '
        'TabPageEnd
        '
        Me.TabPageEnd.Controls.Add(Me.LabelStatusSave)
        Me.TabPageEnd.Controls.Add(Me.ProgressBarSave)
        Me.TabPageEnd.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEnd.Name = "TabPageEnd"
        Me.TabPageEnd.Size = New System.Drawing.Size(784, 529)
        Me.TabPageEnd.TabIndex = 2
        Me.TabPageEnd.Text = "Finalització"
        '
        'LabelStatusSave
        '
        Me.LabelStatusSave.AutoSize = True
        Me.LabelStatusSave.Location = New System.Drawing.Point(13, 359)
        Me.LabelStatusSave.Name = "LabelStatusSave"
        Me.LabelStatusSave.Size = New System.Drawing.Size(94, 13)
        Me.LabelStatusSave.TabIndex = 1
        Me.LabelStatusSave.Text = "(LabelStatusSave)"
        '
        'ProgressBarSave
        '
        Me.ProgressBarSave.Location = New System.Drawing.Point(13, 375)
        Me.ProgressBarSave.Name = "ProgressBarSave"
        Me.ProgressBarSave.Size = New System.Drawing.Size(755, 23)
        Me.ProgressBarSave.TabIndex = 0
        '
        'MenuItemTvFrasSetNum
        '
        Me.MenuItemTvFrasSetNum.Index = 3
        Me.MenuItemTvFrasSetNum.Text = "num i data..."
        '
        'MenuItemTvFrasNewFra
        '
        Me.MenuItemTvFrasNewFra.Index = 0
        Me.MenuItemTvFrasNewFra.Text = "Nueva factura"
        '
        'MenuItemTvFrasFacturarEn
        '
        Me.MenuItemTvFrasFacturarEn.Index = 2
        Me.MenuItemTvFrasFacturarEn.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemTvFrasNewFra})
        Me.MenuItemTvFrasFacturarEn.Text = "Facturar en..."
        '
        'ContextMenuTvFras
        '
        Me.ContextMenuTvFras.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItemTvFrasZoom, Me.MenuItemTvFrasRemove, Me.MenuItemTvFrasFacturarEn, Me.MenuItemTvFrasSetNum})
        '
        'MenuItemTvFrasZoom
        '
        Me.MenuItemTvFrasZoom.Enabled = False
        Me.MenuItemTvFrasZoom.Index = 0
        Me.MenuItemTvFrasZoom.Text = "zoom"
        '
        'MenuItemTvFrasRemove
        '
        Me.MenuItemTvFrasRemove.Enabled = False
        Me.MenuItemTvFrasRemove.Index = 1
        Me.MenuItemTvFrasRemove.Text = "Anular"
        '
        'Frm_Facturacio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 594)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonEnd)
        Me.Controls.Add(Me.ButtonNext)
        Me.Controls.Add(Me.ButtonPrevious)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Facturacio"
        Me.Text = "FACTURACIO"
        Me.PanelFrxNew.ResumeLayout(False)
        CType(Me.DataGridViewWarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageCheck.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageFch.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPageDist.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewClis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageEnd.ResumeLayout(False)
        Me.TabPageEnd.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
    Friend WithEvents CheckBoxCash As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCredit As System.Windows.Forms.CheckBox
    Friend WithEvents DateTimePickerLast As System.Windows.Forms.DateTimePicker
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonEnd As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents PanelFrxNew As System.Windows.Forms.Panel
    Friend WithEvents DateTimePickerFirst As System.Windows.Forms.DateTimePicker
    Friend WithEvents LabelLastFra As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewWarn As System.Windows.Forms.DataGridView
    Friend WithEvents RadioButtonFrxNew As System.Windows.Forms.RadioButton
    Friend WithEvents TabPageCheck As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPageFch As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TabPageDist As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewClis As System.Windows.Forms.DataGridView
    Friend WithEvents LabelJoinAlbs As System.Windows.Forms.Label
    Friend WithEvents TextBoxOb3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxOb2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxOb1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxFpg As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerVto As System.Windows.Forms.DateTimePicker
    Friend WithEvents ComboBoxCfp As System.Windows.Forms.ComboBox
    Friend WithEvents LabelFpg As System.Windows.Forms.Label
    Friend WithEvents TreeViewFras As System.Windows.Forms.TreeView
    Friend WithEvents ImageListFolders As System.Windows.Forms.ImageList
    Friend WithEvents TabPageEnd As System.Windows.Forms.TabPage
    Friend WithEvents ContextMenuClis As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemTvFrasSetNum As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTvFrasNewFra As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTvFrasFacturarEn As System.Windows.Forms.MenuItem
    Friend WithEvents ContextMenuTvFras As System.Windows.Forms.ContextMenu
    Friend WithEvents MenuItemTvFrasZoom As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItemTvFrasRemove As System.Windows.Forms.MenuItem
    Friend WithEvents CheckBoxNegatives As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxExport As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFacturarTot As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPreVto As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxFraPerMes As System.Windows.Forms.CheckBox
    Friend WithEvents LabelStatusSave As System.Windows.Forms.Label
    Friend WithEvents ProgressBarSave As System.Windows.Forms.ProgressBar
    Friend WithEvents ProgressBarDist As System.Windows.Forms.ProgressBar
    Friend WithEvents CheckBoxSmallVolume As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxReq As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxIva As System.Windows.Forms.CheckBox
End Class
