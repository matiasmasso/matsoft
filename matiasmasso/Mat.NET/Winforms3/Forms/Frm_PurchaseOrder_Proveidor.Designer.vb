<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrder_Proveidor
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
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerDelivery = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxFchDelivery = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_PdcSrc1 = New Xl_PdcSrc()
        Me.TextBoxPdd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_PurchaseOrderItems1 = New Xl_PurchaseOrderItems()
        Me.Xl_UsrLog1 = New Xl_UsrLog()
        Me.Xl_ContactDeliverTo = New Xl_Contact2()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DivisaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EURToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.USDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.GBPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ALTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TextBoxTotals = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.CheckBoxHide = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_PurchaseOrderItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(624, 32)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerFch.TabIndex = 128
        Me.DateTimePickerFch.TabStop = False
        '
        'DateTimePickerDelivery
        '
        Me.DateTimePickerDelivery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerDelivery.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerDelivery.Location = New System.Drawing.Point(624, 57)
        Me.DateTimePickerDelivery.Name = "DateTimePickerDelivery"
        Me.DateTimePickerDelivery.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePickerDelivery.TabIndex = 138
        Me.DateTimePickerDelivery.TabStop = False
        Me.DateTimePickerDelivery.Visible = False
        '
        'CheckBoxFchDelivery
        '
        Me.CheckBoxFchDelivery.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxFchDelivery.AutoSize = True
        Me.CheckBoxFchDelivery.Location = New System.Drawing.Point(527, 58)
        Me.CheckBoxFchDelivery.Name = "CheckBoxFchDelivery"
        Me.CheckBoxFchDelivery.Size = New System.Drawing.Size(94, 17)
        Me.CheckBoxFchDelivery.TabIndex = 139
        Me.CheckBoxFchDelivery.Text = "data lliurament"
        Me.CheckBoxFchDelivery.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(593, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(28, 13)
        Me.Label4.TabIndex = 137
        Me.Label4.Text = "data"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(74, 13)
        Me.Label2.TabIndex = 131
        Me.Label2.Text = "A entregar en:"
        '
        'Xl_PdcSrc1
        '
        Me.Xl_PdcSrc1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PdcSrc1.Location = New System.Drawing.Point(527, 36)
        Me.Xl_PdcSrc1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_PdcSrc1.Name = "Xl_PdcSrc1"
        Me.Xl_PdcSrc1.Size = New System.Drawing.Size(18, 16)
        Me.Xl_PdcSrc1.TabIndex = 129
        Me.Xl_PdcSrc1.TabStop = False
        '
        'TextBoxPdd
        '
        Me.TextBoxPdd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPdd.Location = New System.Drawing.Point(91, 32)
        Me.TextBoxPdd.MaxLength = 60
        Me.TextBoxPdd.Name = "TextBoxPdd"
        Me.TextBoxPdd.Size = New System.Drawing.Size(411, 20)
        Me.TextBoxPdd.TabIndex = 126
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 127
        Me.Label1.Text = "&Concepte:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 414)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(722, 31)
        Me.Panel1.TabIndex = 503
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(505, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 102
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(615, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 103
        Me.ButtonOk.Text = "ACCEPTAR"
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
        Me.ButtonDel.TabIndex = 104
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_PurchaseOrderItems1
        '
        Me.Xl_PurchaseOrderItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PurchaseOrderItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PurchaseOrderItems1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PurchaseOrderItems1.Name = "Xl_PurchaseOrderItems1"
        Me.Xl_PurchaseOrderItems1.Size = New System.Drawing.Size(722, 257)
        Me.Xl_PurchaseOrderItems1.TabIndex = 504
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(0, 257)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(722, 20)
        Me.Xl_UsrLog1.TabIndex = 505
        '
        'Xl_ContactDeliverTo
        '
        Me.Xl_ContactDeliverTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactDeliverTo.Contact = Nothing
        Me.Xl_ContactDeliverTo.Emp = Nothing
        Me.Xl_ContactDeliverTo.Location = New System.Drawing.Point(91, 55)
        Me.Xl_ContactDeliverTo.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ContactDeliverTo.Name = "Xl_ContactDeliverTo"
        Me.Xl_ContactDeliverTo.ReadOnly = False
        Me.Xl_ContactDeliverTo.Size = New System.Drawing.Size(411, 20)
        Me.Xl_ContactDeliverTo.TabIndex = 506
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(75, 13)
        Me.Label3.TabIndex = 507
        Me.Label3.Text = "Observacions:"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(91, 78)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(411, 20)
        Me.TextBoxObs.TabIndex = 508
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(722, 24)
        Me.MenuStrip1.TabIndex = 509
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DivisaToolStripMenuItem, Me.ExcelToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'DivisaToolStripMenuItem
        '
        Me.DivisaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EURToolStripMenuItem, Me.USDToolStripMenuItem, Me.GBPToolStripMenuItem, Me.ALTToolStripMenuItem})
        Me.DivisaToolStripMenuItem.Name = "DivisaToolStripMenuItem"
        Me.DivisaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.DivisaToolStripMenuItem.Text = "Divisa"
        '
        'EURToolStripMenuItem
        '
        Me.EURToolStripMenuItem.CheckOnClick = True
        Me.EURToolStripMenuItem.Name = "EURToolStripMenuItem"
        Me.EURToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.EURToolStripMenuItem.Text = "EUR"
        '
        'USDToolStripMenuItem
        '
        Me.USDToolStripMenuItem.CheckOnClick = True
        Me.USDToolStripMenuItem.Name = "USDToolStripMenuItem"
        Me.USDToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.USDToolStripMenuItem.Text = "USD"
        '
        'GBPToolStripMenuItem
        '
        Me.GBPToolStripMenuItem.CheckOnClick = True
        Me.GBPToolStripMenuItem.Name = "GBPToolStripMenuItem"
        Me.GBPToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.GBPToolStripMenuItem.Text = "GBP"
        '
        'ALTToolStripMenuItem
        '
        Me.ALTToolStripMenuItem.Name = "ALTToolStripMenuItem"
        Me.ALTToolStripMenuItem.Size = New System.Drawing.Size(96, 22)
        Me.ALTToolStripMenuItem.Text = "ALT"
        Me.ALTToolStripMenuItem.Visible = False
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ExcelToolStripMenuItem.Text = "Excel"
        '
        'TextBoxTotals
        '
        Me.TextBoxTotals.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TextBoxTotals.Location = New System.Drawing.Point(0, 277)
        Me.TextBoxTotals.Name = "TextBoxTotals"
        Me.TextBoxTotals.ReadOnly = True
        Me.TextBoxTotals.Size = New System.Drawing.Size(722, 20)
        Me.TextBoxTotals.TabIndex = 510
        Me.TextBoxTotals.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.Xl_PurchaseOrderItems1)
        Me.Panel2.Controls.Add(Me.Xl_UsrLog1)
        Me.Panel2.Controls.Add(Me.TextBoxTotals)
        Me.Panel2.Location = New System.Drawing.Point(0, 115)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(722, 297)
        Me.Panel2.TabIndex = 511
        '
        'CheckBoxHide
        '
        Me.CheckBoxHide.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxHide.AutoSize = True
        Me.CheckBoxHide.Location = New System.Drawing.Point(527, 80)
        Me.CheckBoxHide.Name = "CheckBoxHide"
        Me.CheckBoxHide.Size = New System.Drawing.Size(108, 17)
        Me.CheckBoxHide.TabIndex = 512
        Me.CheckBoxHide.Text = "ocultar a l'exterior"
        Me.CheckBoxHide.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxHide.UseVisualStyleBackColor = True
        '
        'Frm_PurchaseOrder_Proveidor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(722, 445)
        Me.Controls.Add(Me.CheckBoxHide)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_ContactDeliverTo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.DateTimePickerFch)
        Me.Controls.Add(Me.DateTimePickerDelivery)
        Me.Controls.Add(Me.CheckBoxFchDelivery)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_PdcSrc1)
        Me.Controls.Add(Me.TextBoxPdd)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_PurchaseOrder_Proveidor"
        Me.Text = "Comanda a Proveidor"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_PurchaseOrderItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePickerFch As DateTimePicker
    Friend WithEvents DateTimePickerDelivery As DateTimePicker
    Friend WithEvents CheckBoxFchDelivery As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_PdcSrc1 As Xl_PdcSrc
    Friend WithEvents TextBoxPdd As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_PurchaseOrderItems1 As Xl_PurchaseOrderItems
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
    Friend WithEvents Xl_ContactDeliverTo As Xl_Contact2
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DivisaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EURToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents USDToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents GBPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ALTToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBoxTotals As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents CheckBoxHide As CheckBox
End Class
