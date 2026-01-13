<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EDiversaOrder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_EDiversaOrder))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageGeneral = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.LabelStatus = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_EDiversaOrderItems1 = New Winforms.Xl_EDiversaOrderItems()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePickerLast = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerDelivery = New System.Windows.Forms.DateTimePicker()
        Me.Xl_Contact2Comprador = New Winforms.Xl_Contact2()
        Me.Xl_Contact2ReceptorMercancia = New Winforms.Xl_Contact2()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCur = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_EANComprador = New Winforms.Xl_EAN()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxOrderNum = New System.Windows.Forms.TextBox()
        Me.Xl_EANReceptorMercancia = New Winforms.Xl_EAN()
        Me.DateTimePickerDoc = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPageFile = New System.Windows.Forms.TabPage()
        Me.ButtonReloadFromEdiFile = New System.Windows.Forms.Button()
        Me.ComboBoxResultCod = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxResult = New System.Windows.Forms.TextBox()
        Me.TextBoxSrc = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxGuid = New System.Windows.Forms.TextBox()
        Me.TabPageErrors = New System.Windows.Forms.TabPage()
        Me.Xl_EdiversaExceptions1 = New Winforms.Xl_EdiversaExceptions()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPageGeneral.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_EDiversaOrderItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageFile.SuspendLayout()
        Me.TabPageErrors.SuspendLayout()
        CType(Me.Xl_EdiversaExceptions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 487)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(746, 31)
        Me.Panel1.TabIndex = 49
        '
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPageGeneral)
        Me.TabControl1.Controls.Add(Me.TabPageFile)
        Me.TabControl1.Controls.Add(Me.TabPageErrors)
        Me.TabControl1.ImageList = Me.ImageList1
        Me.TabControl1.Location = New System.Drawing.Point(0, 29)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(746, 489)
        Me.TabControl1.TabIndex = 50
        '
        'TabPageGeneral
        '
        Me.TabPageGeneral.Controls.Add(Me.Panel2)
        Me.TabPageGeneral.Controls.Add(Me.LabelStatus)
        Me.TabPageGeneral.Controls.Add(Me.SplitContainer1)
        Me.TabPageGeneral.Controls.Add(Me.Label5)
        Me.TabPageGeneral.Controls.Add(Me.DateTimePickerLast)
        Me.TabPageGeneral.Controls.Add(Me.Label4)
        Me.TabPageGeneral.Controls.Add(Me.DateTimePickerDelivery)
        Me.TabPageGeneral.Controls.Add(Me.Xl_Contact2Comprador)
        Me.TabPageGeneral.Controls.Add(Me.Xl_Contact2ReceptorMercancia)
        Me.TabPageGeneral.Controls.Add(Me.Label7)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxCur)
        Me.TabPageGeneral.Controls.Add(Me.Label6)
        Me.TabPageGeneral.Controls.Add(Me.Xl_EANComprador)
        Me.TabPageGeneral.Controls.Add(Me.Label3)
        Me.TabPageGeneral.Controls.Add(Me.Label2)
        Me.TabPageGeneral.Controls.Add(Me.TextBoxOrderNum)
        Me.TabPageGeneral.Controls.Add(Me.Xl_EANReceptorMercancia)
        Me.TabPageGeneral.Controls.Add(Me.DateTimePickerDoc)
        Me.TabPageGeneral.Controls.Add(Me.Label1)
        Me.TabPageGeneral.Location = New System.Drawing.Point(4, 23)
        Me.TabPageGeneral.Name = "TabPageGeneral"
        Me.TabPageGeneral.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageGeneral.Size = New System.Drawing.Size(738, 462)
        Me.TabPageGeneral.TabIndex = 0
        Me.TabPageGeneral.Text = "General"
        Me.TabPageGeneral.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel2.Controls.Add(Me.ButtonCancel)
        Me.Panel2.Controls.Add(Me.ButtonOk)
        Me.Panel2.Controls.Add(Me.ButtonDel)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(3, 428)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(732, 31)
        Me.Panel2.TabIndex = 117
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(513, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(624, 4)
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
        'LabelStatus
        '
        Me.LabelStatus.AutoSize = True
        Me.LabelStatus.Location = New System.Drawing.Point(261, 72)
        Me.LabelStatus.Name = "LabelStatus"
        Me.LabelStatus.Size = New System.Drawing.Size(37, 13)
        Me.LabelStatus.TabIndex = 116
        Me.LabelStatus.Text = "Status"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 94)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_EDiversaOrderItems1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxObs)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer1.Size = New System.Drawing.Size(736, 332)
        Me.SplitContainer1.SplitterDistance = 205
        Me.SplitContainer1.TabIndex = 115
        '
        'Xl_EDiversaOrderItems1
        '
        Me.Xl_EDiversaOrderItems1.AllowUserToAddRows = False
        Me.Xl_EDiversaOrderItems1.AllowUserToDeleteRows = False
        Me.Xl_EDiversaOrderItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EDiversaOrderItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EDiversaOrderItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_EDiversaOrderItems1.Name = "Xl_EDiversaOrderItems1"
        Me.Xl_EDiversaOrderItems1.ReadOnly = True
        Me.Xl_EDiversaOrderItems1.Size = New System.Drawing.Size(736, 205)
        Me.Xl_EDiversaOrderItems1.TabIndex = 55
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(1, 26)
        Me.TextBoxObs.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(734, 93)
        Me.TextBoxObs.TabIndex = 73
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(3, 9)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(75, 13)
        Me.Label8.TabIndex = 74
        Me.Label8.Text = "Observacions:"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(572, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 114
        Me.Label5.Text = "Data Limit"
        '
        'DateTimePickerLast
        '
        Me.DateTimePickerLast.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerLast.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerLast.Location = New System.Drawing.Point(652, 66)
        Me.DateTimePickerLast.Name = "DateTimePickerLast"
        Me.DateTimePickerLast.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerLast.TabIndex = 113
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(572, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 112
        Me.Label4.Text = "Data Servei"
        '
        'DateTimePickerDelivery
        '
        Me.DateTimePickerDelivery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerDelivery.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDelivery.Location = New System.Drawing.Point(652, 42)
        Me.DateTimePickerDelivery.Name = "DateTimePickerDelivery"
        Me.DateTimePickerDelivery.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerDelivery.TabIndex = 111
        '
        'Xl_Contact2Comprador
        '
        Me.Xl_Contact2Comprador.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2Comprador.Contact = Nothing
        Me.Xl_Contact2Comprador.Emp = Nothing
        Me.Xl_Contact2Comprador.Location = New System.Drawing.Point(167, 16)
        Me.Xl_Contact2Comprador.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Contact2Comprador.Name = "Xl_Contact2Comprador"
        Me.Xl_Contact2Comprador.ReadOnly = False
        Me.Xl_Contact2Comprador.Size = New System.Drawing.Size(395, 20)
        Me.Xl_Contact2Comprador.TabIndex = 110
        '
        'Xl_Contact2ReceptorMercancia
        '
        Me.Xl_Contact2ReceptorMercancia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2ReceptorMercancia.Contact = Nothing
        Me.Xl_Contact2ReceptorMercancia.Emp = Nothing
        Me.Xl_Contact2ReceptorMercancia.Location = New System.Drawing.Point(167, 42)
        Me.Xl_Contact2ReceptorMercancia.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Contact2ReceptorMercancia.Name = "Xl_Contact2ReceptorMercancia"
        Me.Xl_Contact2ReceptorMercancia.ReadOnly = False
        Me.Xl_Contact2ReceptorMercancia.Size = New System.Drawing.Size(395, 20)
        Me.Xl_Contact2ReceptorMercancia.TabIndex = 109
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(167, 72)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(46, 13)
        Me.Label7.TabIndex = 108
        Me.Label7.Text = "Moneda"
        '
        'TextBoxCur
        '
        Me.TextBoxCur.Location = New System.Drawing.Point(217, 68)
        Me.TextBoxCur.Margin = New System.Windows.Forms.Padding(1)
        Me.TextBoxCur.Name = "TextBoxCur"
        Me.TextBoxCur.Size = New System.Drawing.Size(40, 20)
        Me.TextBoxCur.TabIndex = 107
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(572, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(78, 13)
        Me.Label6.TabIndex = 106
        Me.Label6.Text = "Data Comanda"
        '
        'Xl_EANComprador
        '
        Me.Xl_EANComprador.Location = New System.Drawing.Point(61, 16)
        Me.Xl_EANComprador.Name = "Xl_EANComprador"
        Me.Xl_EANComprador.Size = New System.Drawing.Size(100, 20)
        Me.Xl_EANComprador.TabIndex = 105
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 104
        Me.Label3.Text = "Comprador"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(2, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 103
        Me.Label2.Text = "Comanda"
        '
        'TextBoxOrderNum
        '
        Me.TextBoxOrderNum.Enabled = False
        Me.TextBoxOrderNum.Location = New System.Drawing.Point(61, 68)
        Me.TextBoxOrderNum.Name = "TextBoxOrderNum"
        Me.TextBoxOrderNum.Size = New System.Drawing.Size(101, 20)
        Me.TextBoxOrderNum.TabIndex = 102
        '
        'Xl_EANReceptorMercancia
        '
        Me.Xl_EANReceptorMercancia.Location = New System.Drawing.Point(61, 42)
        Me.Xl_EANReceptorMercancia.Name = "Xl_EANReceptorMercancia"
        Me.Xl_EANReceptorMercancia.Size = New System.Drawing.Size(100, 20)
        Me.Xl_EANReceptorMercancia.TabIndex = 101
        '
        'DateTimePickerDoc
        '
        Me.DateTimePickerDoc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerDoc.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDoc.Location = New System.Drawing.Point(652, 16)
        Me.DateTimePickerDoc.Name = "DateTimePickerDoc"
        Me.DateTimePickerDoc.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerDoc.TabIndex = 100
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 13)
        Me.Label1.TabIndex = 99
        Me.Label1.Text = "Destinació"
        '
        'TabPageFile
        '
        Me.TabPageFile.Controls.Add(Me.ButtonReloadFromEdiFile)
        Me.TabPageFile.Controls.Add(Me.ComboBoxResultCod)
        Me.TabPageFile.Controls.Add(Me.Label10)
        Me.TabPageFile.Controls.Add(Me.TextBoxResult)
        Me.TabPageFile.Controls.Add(Me.TextBoxSrc)
        Me.TabPageFile.Controls.Add(Me.Label9)
        Me.TabPageFile.Controls.Add(Me.TextBoxGuid)
        Me.TabPageFile.Location = New System.Drawing.Point(4, 23)
        Me.TabPageFile.Name = "TabPageFile"
        Me.TabPageFile.Size = New System.Drawing.Size(738, 462)
        Me.TabPageFile.TabIndex = 2
        Me.TabPageFile.Text = "Fichero"
        Me.TabPageFile.UseVisualStyleBackColor = True
        '
        'ButtonReloadFromEdiFile
        '
        Me.ButtonReloadFromEdiFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonReloadFromEdiFile.Location = New System.Drawing.Point(441, 45)
        Me.ButtonReloadFromEdiFile.Name = "ButtonReloadFromEdiFile"
        Me.ButtonReloadFromEdiFile.Size = New System.Drawing.Size(75, 23)
        Me.ButtonReloadFromEdiFile.TabIndex = 6
        Me.ButtonReloadFromEdiFile.Text = "reload"
        Me.ButtonReloadFromEdiFile.UseVisualStyleBackColor = True
        '
        'ComboBoxResultCod
        '
        Me.ComboBoxResultCod.FormattingEnabled = True
        Me.ComboBoxResultCod.Location = New System.Drawing.Point(63, 47)
        Me.ComboBoxResultCod.Name = "ComboBoxResultCod"
        Me.ComboBoxResultCod.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxResultCod.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 50)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(46, 13)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Resultat"
        '
        'TextBoxResult
        '
        Me.TextBoxResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxResult.Location = New System.Drawing.Point(197, 47)
        Me.TextBoxResult.Name = "TextBoxResult"
        Me.TextBoxResult.Size = New System.Drawing.Size(238, 20)
        Me.TextBoxResult.TabIndex = 3
        '
        'TextBoxSrc
        '
        Me.TextBoxSrc.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSrc.Location = New System.Drawing.Point(12, 92)
        Me.TextBoxSrc.Multiline = True
        Me.TextBoxSrc.Name = "TextBoxSrc"
        Me.TextBoxSrc.Size = New System.Drawing.Size(504, 282)
        Me.TextBoxSrc.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 24)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Guid"
        '
        'TextBoxGuid
        '
        Me.TextBoxGuid.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxGuid.Location = New System.Drawing.Point(63, 21)
        Me.TextBoxGuid.Name = "TextBoxGuid"
        Me.TextBoxGuid.Size = New System.Drawing.Size(453, 20)
        Me.TextBoxGuid.TabIndex = 0
        '
        'TabPageErrors
        '
        Me.TabPageErrors.Controls.Add(Me.Xl_EdiversaExceptions1)
        Me.TabPageErrors.ImageIndex = 0
        Me.TabPageErrors.Location = New System.Drawing.Point(4, 23)
        Me.TabPageErrors.Name = "TabPageErrors"
        Me.TabPageErrors.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageErrors.Size = New System.Drawing.Size(738, 462)
        Me.TabPageErrors.TabIndex = 1
        Me.TabPageErrors.Text = "Errors"
        Me.TabPageErrors.UseVisualStyleBackColor = True
        '
        'Xl_EdiversaExceptions1
        '
        Me.Xl_EdiversaExceptions1.AllowUserToAddRows = False
        Me.Xl_EdiversaExceptions1.AllowUserToDeleteRows = False
        Me.Xl_EdiversaExceptions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiversaExceptions1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EdiversaExceptions1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_EdiversaExceptions1.Name = "Xl_EdiversaExceptions1"
        Me.Xl_EdiversaExceptions1.ReadOnly = True
        Me.Xl_EdiversaExceptions1.Size = New System.Drawing.Size(732, 456)
        Me.Xl_EdiversaExceptions1.TabIndex = 0
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "WarnRed16.png")
        '
        'Frm_EDiversaOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 518)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_EDiversaOrder"
        Me.Text = "Comanda EDI"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPageGeneral.ResumeLayout(False)
        Me.TabPageGeneral.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_EDiversaOrderItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageFile.ResumeLayout(False)
        Me.TabPageFile.PerformLayout()
        Me.TabPageErrors.ResumeLayout(False)
        CType(Me.Xl_EdiversaExceptions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageGeneral As TabPage
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_EDiversaOrderItems1 As Xl_EDiversaOrderItems
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents DateTimePickerLast As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents DateTimePickerDelivery As DateTimePicker
    Friend WithEvents Xl_Contact2Comprador As Xl_Contact2
    Friend WithEvents Xl_Contact2ReceptorMercancia As Xl_Contact2
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxCur As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents Xl_EANComprador As Xl_EAN
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxOrderNum As TextBox
    Friend WithEvents Xl_EANReceptorMercancia As Xl_EAN
    Friend WithEvents DateTimePickerDoc As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents TabPageErrors As TabPage
    Friend WithEvents Xl_EdiversaExceptions1 As Xl_EdiversaExceptions
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents TabPageFile As TabPage
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxGuid As TextBox
    Friend WithEvents TextBoxSrc As TextBox
    Friend WithEvents ComboBoxResultCod As ComboBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBoxResult As TextBox
    Friend WithEvents ButtonReloadFromEdiFile As Button
    Friend WithEvents LabelStatus As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
End Class
