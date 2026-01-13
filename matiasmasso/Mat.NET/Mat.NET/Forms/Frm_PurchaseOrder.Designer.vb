<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrder
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonAlb = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_DocFile1 = New Mat.NET.Xl_DocFile()
        Me.Xl_Contact_Logo1 = New Mat.NET.Xl_Contact_Logo()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonObs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFpg = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonFchMin = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonServirTotJunt = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonPot = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonPromos = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonCustomDoc = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonView = New System.Windows.Forms.ToolStripButton()
        Me.TextBoxTotal = New System.Windows.Forms.TextBox()
        Me.PanelItems = New System.Windows.Forms.Panel()
        Me.Xl_PurchaseOrderItems1 = New Mat.NET.Xl_PurchaseOrderItems()
        Me.StatusStripObs = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelObs = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelCustDoc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TextBoxStatus = New System.Windows.Forms.TextBox()
        Me.ButtonExcel = New System.Windows.Forms.Button()
        Me.Xl_PdcSrc1 = New Mat.NET.Xl_PdcSrc()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.CheckBoxPromo = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupPromo1 = New Mat.NET.Xl_LookupPromo()
        Me.TextBoxConcept = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.PanelItems.SuspendLayout()
        CType(Me.Xl_PurchaseOrderItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStripObs.SuspendLayout()
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
        Me.Panel1.Location = New System.Drawing.Point(0, 464)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(921, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonAlb
        '
        Me.ButtonAlb.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonAlb.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonAlb.Enabled = False
        Me.ButtonAlb.Location = New System.Drawing.Point(814, 3)
        Me.ButtonAlb.Name = "ButtonAlb"
        Me.ButtonAlb.Size = New System.Drawing.Size(104, 24)
        Me.ButtonAlb.TabIndex = 3
        Me.ButtonAlb.Text = "Albarà"
        Me.ButtonAlb.UseVisualStyleBackColor = False
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(594, 3)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.TabStop = False
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(704, 3)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 3
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(3, 3)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.TabStop = False
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxTotal)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelItems)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxStatus)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ButtonExcel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_PdcSrc1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DateTimePicker1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBoxPromo)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_LookupPromo1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TextBoxConcept)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Size = New System.Drawing.Size(921, 464)
        Me.SplitContainer1.SplitterDistance = 155
        Me.SplitContainer1.TabIndex = 0
        Me.SplitContainer1.TabStop = False
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(2, 56)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(149, 182)
        Me.Xl_DocFile1.TabIndex = 116
        Me.Xl_DocFile1.TabStop = False
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Contact = Nothing
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(2, 2)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(153, 48)
        Me.Xl_Contact_Logo1.TabIndex = 115
        Me.Xl_Contact_Logo1.TabStop = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonObs, Me.ToolStripButtonFpg, Me.ToolStripButtonFchMin, Me.ToolStripButtonServirTotJunt, Me.ToolStripButtonPot, Me.ToolStripButtonPromos, Me.ToolStripButtonCustomDoc, Me.ToolStripButtonView})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 269)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(155, 195)
        Me.ToolStrip1.TabIndex = 108
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonObs
        '
        Me.ToolStripButtonObs.Image = Global.Mat.NET.My.Resources.Resources.info
        Me.ToolStripButtonObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonObs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonObs.Name = "ToolStripButtonObs"
        Me.ToolStripButtonObs.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonObs.Text = "observacions"
        Me.ToolStripButtonObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonFpg
        '
        Me.ToolStripButtonFpg.Image = Global.Mat.NET.My.Resources.Resources.info
        Me.ToolStripButtonFpg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFpg.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFpg.Name = "ToolStripButtonFpg"
        Me.ToolStripButtonFpg.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonFpg.Text = "pagament especial"
        Me.ToolStripButtonFpg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonFchMin
        '
        Me.ToolStripButtonFchMin.Image = Global.Mat.NET.My.Resources.Resources.Outlook_16
        Me.ToolStripButtonFchMin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonFchMin.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonFchMin.Name = "ToolStripButtonFchMin"
        Me.ToolStripButtonFchMin.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonFchMin.Text = "servei inmediat"
        '
        'ToolStripButtonServirTotJunt
        '
        Me.ToolStripButtonServirTotJunt.CheckOnClick = True
        Me.ToolStripButtonServirTotJunt.DoubleClickEnabled = True
        Me.ToolStripButtonServirTotJunt.Image = Global.Mat.NET.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonServirTotJunt.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonServirTotJunt.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonServirTotJunt.Name = "ToolStripButtonServirTotJunt"
        Me.ToolStripButtonServirTotJunt.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonServirTotJunt.Text = "servir tot junt"
        '
        'ToolStripButtonPot
        '
        Me.ToolStripButtonPot.CheckOnClick = True
        Me.ToolStripButtonPot.DoubleClickEnabled = True
        Me.ToolStripButtonPot.Image = Global.Mat.NET.My.Resources.Resources.UnChecked13
        Me.ToolStripButtonPot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonPot.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPot.Name = "ToolStripButtonPot"
        Me.ToolStripButtonPot.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonPot.Text = "pot"
        '
        'ToolStripButtonPromos
        '
        Me.ToolStripButtonPromos.Image = Global.Mat.NET.My.Resources.Resources.star
        Me.ToolStripButtonPromos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonPromos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPromos.Name = "ToolStripButtonPromos"
        Me.ToolStripButtonPromos.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonPromos.Text = "promocions"
        '
        'ToolStripButtonCustomDoc
        '
        Me.ToolStripButtonCustomDoc.Image = Global.Mat.NET.My.Resources.Resources.iExplorer
        Me.ToolStripButtonCustomDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonCustomDoc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCustomDoc.Name = "ToolStripButtonCustomDoc"
        Me.ToolStripButtonCustomDoc.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonCustomDoc.Text = "doc.consumidor"
        '
        'ToolStripButtonView
        '
        Me.ToolStripButtonView.CheckOnClick = True
        Me.ToolStripButtonView.Image = Global.Mat.NET.My.Resources.Resources.prismatics
        Me.ToolStripButtonView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButtonView.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonView.Name = "ToolStripButtonView"
        Me.ToolStripButtonView.Size = New System.Drawing.Size(153, 20)
        Me.ToolStripButtonView.Text = "vista sortides"
        '
        'TextBoxTotal
        '
        Me.TextBoxTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTotal.Location = New System.Drawing.Point(655, 444)
        Me.TextBoxTotal.Name = "TextBoxTotal"
        Me.TextBoxTotal.ReadOnly = True
        Me.TextBoxTotal.Size = New System.Drawing.Size(101, 20)
        Me.TextBoxTotal.TabIndex = 127
        Me.TextBoxTotal.TabStop = False
        Me.TextBoxTotal.Text = "Total:"
        '
        'PanelItems
        '
        Me.PanelItems.Controls.Add(Me.Xl_PurchaseOrderItems1)
        Me.PanelItems.Controls.Add(Me.StatusStripObs)
        Me.PanelItems.Location = New System.Drawing.Point(3, 57)
        Me.PanelItems.Name = "PanelItems"
        Me.PanelItems.Size = New System.Drawing.Size(753, 388)
        Me.PanelItems.TabIndex = 126
        '
        'Xl_PurchaseOrderItems1
        '
        Me.Xl_PurchaseOrderItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PurchaseOrderItems1.Fch = New Date(CType(0, Long))
        Me.Xl_PurchaseOrderItems1.Location = New System.Drawing.Point(0, 22)
        Me.Xl_PurchaseOrderItems1.Name = "Xl_PurchaseOrderItems1"
        Me.Xl_PurchaseOrderItems1.Size = New System.Drawing.Size(753, 366)
        Me.Xl_PurchaseOrderItems1.TabIndex = 2
        '
        'StatusStripObs
        '
        Me.StatusStripObs.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.StatusStripObs.Dock = System.Windows.Forms.DockStyle.Top
        Me.StatusStripObs.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelObs, Me.ToolStripStatusLabelCustDoc})
        Me.StatusStripObs.Location = New System.Drawing.Point(0, 0)
        Me.StatusStripObs.Name = "StatusStripObs"
        Me.StatusStripObs.Size = New System.Drawing.Size(753, 22)
        Me.StatusStripObs.TabIndex = 111
        Me.StatusStripObs.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelObs
        '
        Me.ToolStripStatusLabelObs.Image = Global.Mat.NET.My.Resources.Resources.info
        Me.ToolStripStatusLabelObs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripStatusLabelObs.Name = "ToolStripStatusLabelObs"
        Me.ToolStripStatusLabelObs.Size = New System.Drawing.Size(722, 17)
        Me.ToolStripStatusLabelObs.Spring = True
        Me.ToolStripStatusLabelObs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripStatusLabelCustDoc
        '
        Me.ToolStripStatusLabelCustDoc.Image = Global.Mat.NET.My.Resources.Resources.iExplorer
        Me.ToolStripStatusLabelCustDoc.Name = "ToolStripStatusLabelCustDoc"
        Me.ToolStripStatusLabelCustDoc.Size = New System.Drawing.Size(16, 17)
        '
        'TextBoxStatus
        '
        Me.TextBoxStatus.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxStatus.Location = New System.Drawing.Point(0, 444)
        Me.TextBoxStatus.Name = "TextBoxStatus"
        Me.TextBoxStatus.ReadOnly = True
        Me.TextBoxStatus.Size = New System.Drawing.Size(649, 20)
        Me.TextBoxStatus.TabIndex = 125
        Me.TextBoxStatus.TabStop = False
        '
        'ButtonExcel
        '
        Me.ButtonExcel.Image = Global.Mat.NET.My.Resources.Resources.Excel
        Me.ButtonExcel.Location = New System.Drawing.Point(738, 10)
        Me.ButtonExcel.Name = "ButtonExcel"
        Me.ButtonExcel.Size = New System.Drawing.Size(18, 18)
        Me.ButtonExcel.TabIndex = 124
        Me.ButtonExcel.TabStop = False
        Me.ButtonExcel.UseVisualStyleBackColor = True
        '
        'Xl_PdcSrc1
        '
        Me.Xl_PdcSrc1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PdcSrc1.Location = New System.Drawing.Point(601, 10)
        Me.Xl_PdcSrc1.Name = "Xl_PdcSrc1"
        Me.Xl_PdcSrc1.Size = New System.Drawing.Size(18, 16)
        Me.Xl_PdcSrc1.TabIndex = 123
        Me.Xl_PdcSrc1.TabStop = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(636, 9)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(96, 20)
        Me.DateTimePicker1.TabIndex = 122
        Me.DateTimePicker1.TabStop = False
        '
        'CheckBoxPromo
        '
        Me.CheckBoxPromo.AutoSize = True
        Me.CheckBoxPromo.Location = New System.Drawing.Point(67, 33)
        Me.CheckBoxPromo.Name = "CheckBoxPromo"
        Me.CheckBoxPromo.Size = New System.Drawing.Size(55, 17)
        Me.CheckBoxPromo.TabIndex = 121
        Me.CheckBoxPromo.TabStop = False
        Me.CheckBoxPromo.Text = "promo"
        Me.CheckBoxPromo.UseVisualStyleBackColor = True
        '
        'Xl_LookupPromo1
        '
        Me.Xl_LookupPromo1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupPromo1.IsDirty = False
        Me.Xl_LookupPromo1.Location = New System.Drawing.Point(129, 31)
        Me.Xl_LookupPromo1.Name = "Xl_LookupPromo1"
        Me.Xl_LookupPromo1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPromo1.Promo = Nothing
        Me.Xl_LookupPromo1.Size = New System.Drawing.Size(454, 20)
        Me.Xl_LookupPromo1.TabIndex = 120
        Me.Xl_LookupPromo1.TabStop = False
        Me.Xl_LookupPromo1.Value = Nothing
        Me.Xl_LookupPromo1.Visible = False
        '
        'TextBoxConcept
        '
        Me.TextBoxConcept.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxConcept.Location = New System.Drawing.Point(67, 9)
        Me.TextBoxConcept.MaxLength = 60
        Me.TextBoxConcept.Name = "TextBoxConcept"
        Me.TextBoxConcept.Size = New System.Drawing.Size(515, 20)
        Me.TextBoxConcept.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "&concepte:"
        '
        'Frm_PurchaseOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(921, 495)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PurchaseOrder"
        Me.Text = "Comanda"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.PanelItems.ResumeLayout(False)
        Me.PanelItems.PerformLayout()
        CType(Me.Xl_PurchaseOrderItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStripObs.ResumeLayout(False)
        Me.StatusStripObs.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents CheckBoxPromo As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_LookupPromo1 As Mat.NET.Xl_LookupPromo
    Friend WithEvents TextBoxConcept As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ButtonExcel As System.Windows.Forms.Button
    Friend WithEvents Xl_PdcSrc1 As Mat.NET.Xl_PdcSrc
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonObs As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonFpg As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonFchMin As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonServirTotJunt As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonPot As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonPromos As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonCustomDoc As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonView As System.Windows.Forms.ToolStripButton
    Friend WithEvents Xl_DocFile1 As Mat.NET.Xl_DocFile
    Friend WithEvents Xl_Contact_Logo1 As Mat.NET.Xl_Contact_Logo
    Friend WithEvents ButtonAlb As System.Windows.Forms.Button
    Friend WithEvents TextBoxStatus As System.Windows.Forms.TextBox
    Friend WithEvents PanelItems As System.Windows.Forms.Panel
    Friend WithEvents Xl_PurchaseOrderItems1 As Mat.NET.Xl_PurchaseOrderItems
    Friend WithEvents StatusStripObs As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelObs As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelCustDoc As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TextBoxTotal As System.Windows.Forms.TextBox
End Class
