<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Pdc_Proveidor
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonAlb = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.PanelHost = New System.Windows.Forms.Panel()
        Me.Xl_Pdc_LineItems_Proveidor1 = New Mat.NET.Xl_Pdc_LineItems_Proveidor()
        Me.ButtonExcel = New System.Windows.Forms.Button()
        Me.ToolStripButtonView = New System.Windows.Forms.ToolStripButton()
        Me.PanelHostParent = New System.Windows.Forms.Panel()
        Me.StatusStripObs = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelObs = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelCustDoc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TextBoxPdd = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Xl_Contact_Logo1 = New Mat.NET.Xl_Contact_Logo()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonObs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFpg = New System.Windows.Forms.ToolStripButton()
        Me.Xl_Cur1 = New Mat.NET.Xl_Cur()
        Me.ComboBoxDeliverTo = New System.Windows.Forms.ComboBox()
        Me.Xl_ContactMgz = New Mat.NET.Xl_Contact()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_ContactDeliverTo = New Mat.NET.Xl_Contact()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_PdcSrc1 = New Mat.NET.Xl_PdcSrc()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel1.SuspendLayout()
        Me.PanelHost.SuspendLayout()
        Me.PanelHostParent.SuspendLayout()
        Me.StatusStripObs.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonAlb)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 563)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(921, 31)
        Me.Panel1.TabIndex = 502
        '
        'ButtonAlb
        '
        Me.ButtonAlb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAlb.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonAlb.Enabled = False
        Me.ButtonAlb.Location = New System.Drawing.Point(813, 4)
        Me.ButtonAlb.Name = "ButtonAlb"
        Me.ButtonAlb.Size = New System.Drawing.Size(104, 24)
        Me.ButtonAlb.TabIndex = 101
        Me.ButtonAlb.Text = "ALBARA"
        Me.ButtonAlb.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(593, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(703, 4)
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
        'PanelHost
        '
        Me.PanelHost.Controls.Add(Me.Xl_Pdc_LineItems_Proveidor1)
        Me.PanelHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelHost.Location = New System.Drawing.Point(0, 22)
        Me.PanelHost.Name = "PanelHost"
        Me.PanelHost.Size = New System.Drawing.Size(763, 482)
        Me.PanelHost.TabIndex = 109
        '
        'Xl_Pdc_LineItems_Proveidor1
        '
        Me.Xl_Pdc_LineItems_Proveidor1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Pdc_LineItems_Proveidor1.Location = New System.Drawing.Point(0, 3)
        Me.Xl_Pdc_LineItems_Proveidor1.Name = "Xl_Pdc_LineItems_Proveidor1"
        Me.Xl_Pdc_LineItems_Proveidor1.Size = New System.Drawing.Size(763, 479)
        Me.Xl_Pdc_LineItems_Proveidor1.TabIndex = 0
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Location = New System.Drawing.Point(732, 13)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(18, 18)
        Me.ButtonExcel.TabIndex = 116
        Me.ButtonExcel.UseVisualStyleBackColor = True
        '
        'ToolStripButtonView
        '
        Me.ToolStripButtonView.CheckOnClick = True
        Me.ToolStripButtonView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonView.Name = "ToolStripButtonView"
        Me.ToolStripButtonView.Size = New System.Drawing.Size(153, 19)
        Me.ToolStripButtonView.Text = "vista sortides"
        '
        'PanelHostParent
        '
        Me.PanelHostParent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelHostParent.Controls.Add(Me.PanelHost)
        Me.PanelHostParent.Controls.Add(Me.StatusStripObs)
        Me.PanelHostParent.Location = New System.Drawing.Point(-1, 59)
        Me.PanelHostParent.Name = "PanelHostParent"
        Me.PanelHostParent.Size = New System.Drawing.Size(763, 504)
        Me.PanelHostParent.TabIndex = 108
        '
        'StatusStripObs
        '
        Me.StatusStripObs.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.StatusStripObs.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStripObs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelObs, Me.ToolStripStatusLabelCustDoc})
        Me.StatusStripObs.Location = New System.Drawing.Point(0, 0)
        Me.StatusStripObs.Name = "StatusStripObs"
        Me.StatusStripObs.Size = New System.Drawing.Size(763, 22)
        Me.StatusStripObs.TabIndex = 110
        Me.StatusStripObs.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelObs
        '
        Me.ToolStripStatusLabelObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatusLabelObs.Name = "ToolStripStatusLabelObs"
        Me.ToolStripStatusLabelObs.Size = New System.Drawing.Size(748, 17)
        Me.ToolStripStatusLabelObs.Spring = True
        Me.ToolStripStatusLabelObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabelCustDoc
        '
        Me.ToolStripStatusLabelCustDoc.Name = "ToolStripStatusLabelCustDoc"
        Me.ToolStripStatusLabelCustDoc.Size = New System.Drawing.Size(0, 17)
        '
        'TextBoxPdd
        '
        Me.TextBoxPdd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPdd.Location = New System.Drawing.Point(76, 13)
        Me.TextBoxPdd.MaxLength = 60
        Me.TextBoxPdd.Name = "TextBoxPdd"
        Me.TextBoxPdd.Size = New System.Drawing.Size(464, 20)
        Me.TextBoxPdd.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 111
        Me.Label1.Text = "&concepte:"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_DocFile1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_Contact_Logo1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
        Me.SplitContainer1.Panel1MinSize = 155
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_Cur1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ComboBoxDeliverTo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ContactMgz)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_ContactDeliverTo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ButtonExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelHostParent)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_PdcSrc1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxPdd)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateTimePicker1)
        Me.SplitContainer1.Size = New System.Drawing.Size(921, 563)
        Me.SplitContainer1.SplitterDistance = 155
        Me.SplitContainer1.TabIndex = 501
        Me.SplitContainer1.TabStop = False
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(3, 59)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(149, 182)
        Me.Xl_DocFile1.TabIndex = 115
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(2, 2)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(153, 48)
        Me.Xl_Contact_Logo1.TabIndex = 113
        Me.Xl_Contact_Logo1.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonObs, Me.ToolStripButtonFpg, Me.ToolStripButtonView})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 486)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(155, 77)
        Me.ToolStrip1.TabIndex = 107
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonObs
        '
        Me.ToolStripButtonObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonObs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonObs.Name = "ToolStripButtonObs"
        Me.ToolStripButtonObs.Size = New System.Drawing.Size(153, 19)
        Me.ToolStripButtonObs.Text = "observacions"
        Me.ToolStripButtonObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonFpg
        '
        Me.ToolStripButtonFpg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFpg.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFpg.Name = "ToolStripButtonFpg"
        Me.ToolStripButtonFpg.Size = New System.Drawing.Size(153, 19)
        Me.ToolStripButtonFpg.Text = "pagament especial"
        Me.ToolStripButtonFpg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Xl_Cur1
        '
        Me.Xl_Cur1.Cur = Nothing
        Me.Xl_Cur1.Location = New System.Drawing.Point(594, 12)
        Me.Xl_Cur1.Name = "Xl_Cur1"
        Me.Xl_Cur1.Size = New System.Drawing.Size(30, 20)
        Me.Xl_Cur1.TabIndex = 122
        '
        'ComboBoxDeliverTo
        '
        Me.ComboBoxDeliverTo.FormattingEnabled = True
        Me.ComboBoxDeliverTo.Items.AddRange(New Object() {"directament al client", "als nostres magatzems"})
        Me.ComboBoxDeliverTo.Location = New System.Drawing.Point(74, 35)
        Me.ComboBoxDeliverTo.Name = "ComboBoxDeliverTo"
        Me.ComboBoxDeliverTo.Size = New System.Drawing.Size(179, 21)
        Me.ComboBoxDeliverTo.TabIndex = 121
        '
        'Xl_ContactMgz
        '
        Me.Xl_ContactMgz.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactMgz.Contact = Nothing
        Me.Xl_ContactMgz.Location = New System.Drawing.Point(539, 36)
        Me.Xl_ContactMgz.Name = "Xl_ContactMgz"
        Me.Xl_ContactMgz.ReadOnly = False
        Me.Xl_ContactMgz.Size = New System.Drawing.Size(211, 20)
        Me.Xl_ContactMgz.TabIndex = 120
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(482, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(58, 13)
        Me.Label3.TabIndex = 119
        Me.Label3.Text = "magatzem:"
        '
        'Xl_ContactDeliverTo
        '
        Me.Xl_ContactDeliverTo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ContactDeliverTo.Contact = Nothing
        Me.Xl_ContactDeliverTo.Location = New System.Drawing.Point(259, 36)
        Me.Xl_ContactDeliverTo.Name = "Xl_ContactDeliverTo"
        Me.Xl_ContactDeliverTo.ReadOnly = False
        Me.Xl_ContactDeliverTo.Size = New System.Drawing.Size(219, 20)
        Me.Xl_ContactDeliverTo.TabIndex = 118
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 117
        Me.Label2.Text = "a entregar:"
        '
        'Xl_PdcSrc1
        '
        Me.Xl_PdcSrc1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PdcSrc1.Location = New System.Drawing.Point(546, 13)
        Me.Xl_PdcSrc1.Name = "Xl_PdcSrc1"
        Me.Xl_PdcSrc1.Size = New System.Drawing.Size(18, 16)
        Me.Xl_PdcSrc1.TabIndex = 115
        Me.Xl_PdcSrc1.TabStop = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(630, 12)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 112
        Me.DateTimePicker1.TabStop = False
        '
        'Frm_Pdc_Proveidor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 594)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Pdc_Proveidor"
        Me.Text = "Comanda a proveidor"
        Me.Panel1.ResumeLayout(False)
        Me.PanelHost.ResumeLayout(False)
        Me.PanelHostParent.ResumeLayout(False)
        Me.PanelHostParent.PerformLayout()
        Me.StatusStripObs.ResumeLayout(False)
        Me.StatusStripObs.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonAlb As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents PanelHost As System.Windows.Forms.Panel
    Friend WithEvents Xl_Pdc_LineItems_Proveidor1 As Xl_Pdc_LineItems_Proveidor
    Friend WithEvents ButtonExcel As System.Windows.Forms.Button
    Friend WithEvents ToolStripButtonView As System.Windows.Forms.ToolStripButton
    Friend WithEvents PanelHostParent As System.Windows.Forms.Panel
    Friend WithEvents StatusStripObs As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelObs As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelCustDoc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Xl_PdcSrc1 As Xl_PdcSrc
    Friend WithEvents TextBoxPdd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonObs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonFpg As System.Windows.Forms.ToolStripButton
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Xl_ContactDeliverTo As Xl_Contact
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxDeliverTo As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_ContactMgz As Xl_Contact
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_Cur1 As Xl_Cur
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
End Class
