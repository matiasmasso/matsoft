<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CondicioCapitol
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
        Me.TextBoxCaptionEsp = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxTextEsp = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxCondicioEsp = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TextBoxTextCat = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxCondicioCat = New System.Windows.Forms.TextBox()
        Me.TextBoxCaptionCat = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.TextBoxTextEng = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCondicioEng = New System.Windows.Forms.TextBox()
        Me.TextBoxCaptionEng = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.TextBoxTextPor = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxCondicioPor = New System.Windows.Forms.TextBox()
        Me.TextBoxCaptionPor = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Xl_UsrLog1 = New Xl_UsrLog()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBoxCaptionEsp
        '
        Me.TextBoxCaptionEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCaptionEsp.Location = New System.Drawing.Point(11, 84)
        Me.TextBoxCaptionEsp.Name = "TextBoxCaptionEsp"
        Me.TextBoxCaptionEsp.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCaptionEsp.TabIndex = 57
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 427)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(522, 31)
        Me.Panel1.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(303, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(414, 4)
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
        Me.Label1.Location = New System.Drawing.Point(8, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Encapçalament"
        '
        'TextBoxTextEsp
        '
        Me.TextBoxTextEsp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTextEsp.Location = New System.Drawing.Point(11, 134)
        Me.TextBoxTextEsp.Multiline = True
        Me.TextBoxTextEsp.Name = "TextBoxTextEsp"
        Me.TextBoxTextEsp.Size = New System.Drawing.Size(495, 208)
        Me.TextBoxTextEsp.TabIndex = 59
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 118)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Paragraf"
        '
        'TextBoxCondicioEsp
        '
        Me.TextBoxCondicioEsp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCondicioEsp.Location = New System.Drawing.Point(11, 36)
        Me.TextBoxCondicioEsp.Name = "TextBoxCondicioEsp"
        Me.TextBoxCondicioEsp.ReadOnly = True
        Me.TextBoxCondicioEsp.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCondicioEsp.TabIndex = 61
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Condicions"
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
        Me.TabControl1.Location = New System.Drawing.Point(0, 23)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(522, 371)
        Me.TabControl1.TabIndex = 63
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxTextEsp)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.TextBoxCondicioEsp)
        Me.TabPage1.Controls.Add(Me.TextBoxCaptionEsp)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(514, 345)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Espanyol"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.TextBoxTextCat)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.TextBoxCondicioCat)
        Me.TabPage2.Controls.Add(Me.TextBoxCaptionCat)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(514, 345)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Català"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TextBoxTextCat
        '
        Me.TextBoxTextCat.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTextCat.Location = New System.Drawing.Point(11, 125)
        Me.TextBoxTextCat.Multiline = True
        Me.TextBoxTextCat.Name = "TextBoxTextCat"
        Me.TextBoxTextCat.Size = New System.Drawing.Size(495, 208)
        Me.TextBoxTextCat.TabIndex = 65
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 13)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Encapçalament"
        '
        'TextBoxCondicioCat
        '
        Me.TextBoxCondicioCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCondicioCat.Location = New System.Drawing.Point(11, 27)
        Me.TextBoxCondicioCat.Name = "TextBoxCondicioCat"
        Me.TextBoxCondicioCat.ReadOnly = True
        Me.TextBoxCondicioCat.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCondicioCat.TabIndex = 67
        '
        'TextBoxCaptionCat
        '
        Me.TextBoxCaptionCat.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCaptionCat.Location = New System.Drawing.Point(11, 75)
        Me.TextBoxCaptionCat.Name = "TextBoxCaptionCat"
        Me.TextBoxCaptionCat.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCaptionCat.TabIndex = 63
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 66
        Me.Label5.Text = "Condicions"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 109)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 64
        Me.Label6.Text = "Paragraf"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.TextBoxTextEng)
        Me.TabPage3.Controls.Add(Me.Label7)
        Me.TabPage3.Controls.Add(Me.TextBoxCondicioEng)
        Me.TabPage3.Controls.Add(Me.TextBoxCaptionEng)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.Label9)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(514, 345)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Anglès"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'TextBoxTextEng
        '
        Me.TextBoxTextEng.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTextEng.Location = New System.Drawing.Point(11, 125)
        Me.TextBoxTextEng.Multiline = True
        Me.TextBoxTextEng.Name = "TextBoxTextEng"
        Me.TextBoxTextEng.Size = New System.Drawing.Size(495, 208)
        Me.TextBoxTextEng.TabIndex = 65
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 59)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 13)
        Me.Label7.TabIndex = 62
        Me.Label7.Text = "Encapçalament"
        '
        'TextBoxCondicioEng
        '
        Me.TextBoxCondicioEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCondicioEng.Location = New System.Drawing.Point(11, 27)
        Me.TextBoxCondicioEng.Name = "TextBoxCondicioEng"
        Me.TextBoxCondicioEng.ReadOnly = True
        Me.TextBoxCondicioEng.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCondicioEng.TabIndex = 67
        '
        'TextBoxCaptionEng
        '
        Me.TextBoxCaptionEng.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCaptionEng.Location = New System.Drawing.Point(11, 75)
        Me.TextBoxCaptionEng.Name = "TextBoxCaptionEng"
        Me.TextBoxCaptionEng.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCaptionEng.TabIndex = 63
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(8, 11)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 66
        Me.Label8.Text = "Condicions"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(47, 13)
        Me.Label9.TabIndex = 64
        Me.Label9.Text = "Paragraf"
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.TextBoxTextPor)
        Me.TabPage4.Controls.Add(Me.Label10)
        Me.TabPage4.Controls.Add(Me.TextBoxCondicioPor)
        Me.TabPage4.Controls.Add(Me.TextBoxCaptionPor)
        Me.TabPage4.Controls.Add(Me.Label11)
        Me.TabPage4.Controls.Add(Me.Label12)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(514, 345)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Portuguès"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'TextBoxTextPor
        '
        Me.TextBoxTextPor.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTextPor.Location = New System.Drawing.Point(11, 125)
        Me.TextBoxTextPor.Multiline = True
        Me.TextBoxTextPor.Name = "TextBoxTextPor"
        Me.TextBoxTextPor.Size = New System.Drawing.Size(495, 208)
        Me.TextBoxTextPor.TabIndex = 65
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 59)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 13)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "Encapçalament"
        '
        'TextBoxCondicioPor
        '
        Me.TextBoxCondicioPor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCondicioPor.Location = New System.Drawing.Point(11, 27)
        Me.TextBoxCondicioPor.Name = "TextBoxCondicioPor"
        Me.TextBoxCondicioPor.ReadOnly = True
        Me.TextBoxCondicioPor.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCondicioPor.TabIndex = 67
        '
        'TextBoxCaptionPor
        '
        Me.TextBoxCaptionPor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCaptionPor.Location = New System.Drawing.Point(11, 75)
        Me.TextBoxCaptionPor.Name = "TextBoxCaptionPor"
        Me.TextBoxCaptionPor.Size = New System.Drawing.Size(495, 20)
        Me.TextBoxCaptionPor.TabIndex = 63
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(8, 11)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 13)
        Me.Label11.TabIndex = 66
        Me.Label11.Text = "Condicions"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 109)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(47, 13)
        Me.Label12.TabIndex = 64
        Me.Label12.Text = "Paragraf"
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(6, 401)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.ReadOnly = True
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(512, 20)
        Me.Xl_UsrLog1.TabIndex = 64
        '
        'Frm_CondicioCapitol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 458)
        Me.Controls.Add(Me.Xl_UsrLog1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_CondicioCapitol"
        Me.Text = "Paragraf de Condicions"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxCaptionEsp As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxTextEsp As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxCondicioEsp As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TextBoxTextCat As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxCondicioCat As TextBox
    Friend WithEvents TextBoxCaptionCat As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TextBoxTextEng As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxCondicioEng As TextBox
    Friend WithEvents TextBoxCaptionEng As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TextBoxTextPor As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents TextBoxCondicioPor As TextBox
    Friend WithEvents TextBoxCaptionPor As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
End Class
