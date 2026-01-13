<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RepCom
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.Xl_LookupRep1 = New Winforms.Xl_LookupRep()
        Me.TextBoxCaption = New System.Windows.Forms.TextBox()
        Me.LabelCom = New System.Windows.Forms.Label()
        Me.Xl_Percent1 = New Winforms.Xl_Percent()
        Me.CheckBoxRepCom = New System.Windows.Forms.CheckBox()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 249)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(494, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(275, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(386, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'Xl_LookupRep1
        '
        Me.Xl_LookupRep1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupRep1.Enabled = False
        Me.Xl_LookupRep1.IsDirty = False
        Me.Xl_LookupRep1.Location = New System.Drawing.Point(120, 163)
        Me.Xl_LookupRep1.Name = "Xl_LookupRep1"
        Me.Xl_LookupRep1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupRep1.ReadOnlyLookup = False
        Me.Xl_LookupRep1.Rep = Nothing
        Me.Xl_LookupRep1.Size = New System.Drawing.Size(370, 20)
        Me.Xl_LookupRep1.TabIndex = 43
        Me.Xl_LookupRep1.Value = Nothing
        '
        'TextBoxCaption
        '
        Me.TextBoxCaption.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCaption.Location = New System.Drawing.Point(13, 32)
        Me.TextBoxCaption.Multiline = True
        Me.TextBoxCaption.Name = "TextBoxCaption"
        Me.TextBoxCaption.ReadOnly = True
        Me.TextBoxCaption.Size = New System.Drawing.Size(469, 99)
        Me.TextBoxCaption.TabIndex = 44
        '
        'LabelCom
        '
        Me.LabelCom.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelCom.AutoSize = True
        Me.LabelCom.Enabled = False
        Me.LabelCom.Location = New System.Drawing.Point(31, 192)
        Me.LabelCom.Name = "LabelCom"
        Me.LabelCom.Size = New System.Drawing.Size(43, 13)
        Me.LabelCom.TabIndex = 46
        Me.LabelCom.Text = "Comisió"
        '
        'Xl_Percent1
        '
        Me.Xl_Percent1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Xl_Percent1.Enabled = False
        Me.Xl_Percent1.Location = New System.Drawing.Point(120, 189)
        Me.Xl_Percent1.Name = "Xl_Percent1"
        Me.Xl_Percent1.Size = New System.Drawing.Size(57, 20)
        Me.Xl_Percent1.TabIndex = 47
        Me.Xl_Percent1.Text = "0,00 %"
        Me.Xl_Percent1.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'CheckBoxRepCom
        '
        Me.CheckBoxRepCom.AutoSize = True
        Me.CheckBoxRepCom.Location = New System.Drawing.Point(13, 164)
        Me.CheckBoxRepCom.Name = "CheckBoxRepCom"
        Me.CheckBoxRepCom.Size = New System.Drawing.Size(90, 17)
        Me.CheckBoxRepCom.TabIndex = 48
        Me.CheckBoxRepCom.Text = "Representant"
        Me.CheckBoxRepCom.UseVisualStyleBackColor = True
        '
        'Frm_RepCom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 280)
        Me.Controls.Add(Me.CheckBoxRepCom)
        Me.Controls.Add(Me.Xl_Percent1)
        Me.Controls.Add(Me.LabelCom)
        Me.Controls.Add(Me.TextBoxCaption)
        Me.Controls.Add(Me.Xl_LookupRep1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_RepCom"
        Me.Text = "Comisió representant"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents Xl_LookupRep1 As Xl_LookupRep
    Friend WithEvents TextBoxCaption As TextBox
    Friend WithEvents LabelCom As Label
    Friend WithEvents Xl_Percent1 As Xl_Percent
    Friend WithEvents CheckBoxRepCom As CheckBox
End Class
