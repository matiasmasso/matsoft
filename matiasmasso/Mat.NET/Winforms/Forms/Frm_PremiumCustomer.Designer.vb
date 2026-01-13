<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_PremiumCustomer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_UsrLog1 = New Winforms.Xl_UsrLog()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonExclos = New System.Windows.Forms.RadioButton()
        Me.RadioButtonInclos = New System.Windows.Forms.RadioButton()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_LookupPremiumLine1 = New Winforms.Xl_LookupPremiumLine()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile_Old()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 443)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(648, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(429, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(540, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 4
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
        Me.Label1.Location = New System.Drawing.Point(10, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Client"
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(4, 423)
        Me.Xl_UsrLog1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(640, 20)
        Me.Xl_UsrLog1.TabIndex = 59
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(70, 21)
        Me.Xl_Contact21.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(213, 20)
        Me.Xl_Contact21.TabIndex = 60
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonExclos)
        Me.GroupBox1.Controls.Add(Me.RadioButtonInclos)
        Me.GroupBox1.Location = New System.Drawing.Point(70, 68)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.GroupBox1.Size = New System.Drawing.Size(213, 64)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'RadioButtonExclos
        '
        Me.RadioButtonExclos.AutoSize = True
        Me.RadioButtonExclos.Location = New System.Drawing.Point(29, 34)
        Me.RadioButtonExclos.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.RadioButtonExclos.Name = "RadioButtonExclos"
        Me.RadioButtonExclos.Size = New System.Drawing.Size(56, 17)
        Me.RadioButtonExclos.TabIndex = 1
        Me.RadioButtonExclos.TabStop = True
        Me.RadioButtonExclos.Text = "Exclos"
        Me.RadioButtonExclos.UseVisualStyleBackColor = True
        '
        'RadioButtonInclos
        '
        Me.RadioButtonInclos.AutoSize = True
        Me.RadioButtonInclos.Location = New System.Drawing.Point(29, 15)
        Me.RadioButtonInclos.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.RadioButtonInclos.Name = "RadioButtonInclos"
        Me.RadioButtonInclos.Size = New System.Drawing.Size(53, 17)
        Me.RadioButtonInclos.TabIndex = 0
        Me.RadioButtonInclos.TabStop = True
        Me.RadioButtonInclos.Text = "Inclos"
        Me.RadioButtonInclos.UseVisualStyleBackColor = True
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(4, 162)
        Me.TextBoxObs.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(282, 241)
        Me.TextBoxObs.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 146)
        Me.Label2.Margin = New System.Windows.Forms.Padding(1, 0, 1, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 63
        Me.Label2.Text = "Observacions"
        '
        'Xl_LookupPremiumLine1
        '
        Me.Xl_LookupPremiumLine1.IsDirty = False
        Me.Xl_LookupPremiumLine1.Location = New System.Drawing.Point(70, 43)
        Me.Xl_LookupPremiumLine1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_LookupPremiumLine1.Name = "Xl_LookupPremiumLine1"
        Me.Xl_LookupPremiumLine1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupPremiumLine1.PremiumLine = Nothing
        Me.Xl_LookupPremiumLine1.Size = New System.Drawing.Size(213, 20)
        Me.Xl_LookupPremiumLine1.TabIndex = 1
        Me.Xl_LookupPremiumLine1.TabStop = False
        Me.Xl_LookupPremiumLine1.Value = Nothing
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 65
        Me.Label3.Text = "Productes"
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(294, 1)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(350, 420)
        Me.Xl_DocFile1.TabIndex = 66
        '
        'Frm_PremiumCustomer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 474)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Xl_LookupPremiumLine1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.Xl_UsrLog1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(1, 1, 1, 1)
        Me.Name = "Frm_PremiumCustomer"
        Me.Text = "Client Premium"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButtonExclos As RadioButton
    Friend WithEvents RadioButtonInclos As RadioButton
    Friend WithEvents TextBoxObs As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_LookupPremiumLine1 As Xl_LookupPremiumLine
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_DocFile1 As Xl_DocFile_Old
End Class
