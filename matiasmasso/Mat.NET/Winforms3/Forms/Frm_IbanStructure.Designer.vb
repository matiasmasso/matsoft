<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_IbanStructure
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TextBoxCountry = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.NumericUpDownBankPos = New System.Windows.Forms.NumericUpDown()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.RadioButtonBankNum = New System.Windows.Forms.RadioButton()
        Me.RadioButtonBankAlfa = New System.Windows.Forms.RadioButton()
        Me.NumericUpDownBankLen = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownBranchLen = New System.Windows.Forms.NumericUpDown()
        Me.RadioButtonBranchAlfa = New System.Windows.Forms.RadioButton()
        Me.RadioButtonBranchNum = New System.Windows.Forms.RadioButton()
        Me.NumericUpDownBranchPos = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownDcLen = New System.Windows.Forms.NumericUpDown()
        Me.RadioButtonDcAlfa = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDcNum = New System.Windows.Forms.RadioButton()
        Me.NumericUpDownDcPos = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownCccLen = New System.Windows.Forms.NumericUpDown()
        Me.RadioButtonCccAlfa = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCccNum = New System.Windows.Forms.RadioButton()
        Me.NumericUpDownCccPos = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDownOverallLength = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDownBankPos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownBankLen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownBranchLen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownBranchPos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownDcLen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownDcPos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownCccLen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownCccPos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDownOverallLength, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 45
        Me.Label1.Text = "Pais"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 292)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(490, 31)
        Me.Panel1.TabIndex = 44
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(271, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(382, 4)
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
        'TextBoxCountry
        '
        Me.TextBoxCountry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCountry.Enabled = False
        Me.TextBoxCountry.Location = New System.Drawing.Point(76, 29)
        Me.TextBoxCountry.Name = "TextBoxCountry"
        Me.TextBoxCountry.Size = New System.Drawing.Size(390, 20)
        Me.TextBoxCountry.TabIndex = 46
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(73, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 13)
        Me.Label2.TabIndex = 47
        Me.Label2.Text = "Entitat bancària"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(73, 131)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 13)
        Me.Label3.TabIndex = 48
        Me.Label3.Text = "Oficina bancària"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(73, 159)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 49
        Me.Label4.Text = "Digits de control"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(73, 188)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "Número de Compte"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(194, 77)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Posició"
        '
        'NumericUpDownBankPos
        '
        Me.NumericUpDownBankPos.Location = New System.Drawing.Point(197, 101)
        Me.NumericUpDownBankPos.Name = "NumericUpDownBankPos"
        Me.NumericUpDownBankPos.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownBankPos.TabIndex = 53
        Me.NumericUpDownBankPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(277, 77)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Longitut"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(363, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "Numeric"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(410, 77)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 13)
        Me.Label9.TabIndex = 56
        Me.Label9.Text = "Alfaumeric"
        '
        'RadioButtonBankNum
        '
        Me.RadioButtonBankNum.AutoSize = True
        Me.RadioButtonBankNum.Location = New System.Drawing.Point(19, 10)
        Me.RadioButtonBankNum.Name = "RadioButtonBankNum"
        Me.RadioButtonBankNum.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonBankNum.TabIndex = 57
        Me.RadioButtonBankNum.TabStop = True
        Me.RadioButtonBankNum.UseVisualStyleBackColor = True
        '
        'RadioButtonBankAlfa
        '
        Me.RadioButtonBankAlfa.AutoSize = True
        Me.RadioButtonBankAlfa.Location = New System.Drawing.Point(68, 10)
        Me.RadioButtonBankAlfa.Name = "RadioButtonBankAlfa"
        Me.RadioButtonBankAlfa.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonBankAlfa.TabIndex = 58
        Me.RadioButtonBankAlfa.TabStop = True
        Me.RadioButtonBankAlfa.UseVisualStyleBackColor = True
        '
        'NumericUpDownBankLen
        '
        Me.NumericUpDownBankLen.Location = New System.Drawing.Point(277, 101)
        Me.NumericUpDownBankLen.Name = "NumericUpDownBankLen"
        Me.NumericUpDownBankLen.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownBankLen.TabIndex = 59
        Me.NumericUpDownBankLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NumericUpDownBranchLen
        '
        Me.NumericUpDownBranchLen.Location = New System.Drawing.Point(277, 129)
        Me.NumericUpDownBranchLen.Name = "NumericUpDownBranchLen"
        Me.NumericUpDownBranchLen.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownBranchLen.TabIndex = 63
        Me.NumericUpDownBranchLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadioButtonBranchAlfa
        '
        Me.RadioButtonBranchAlfa.AutoSize = True
        Me.RadioButtonBranchAlfa.Location = New System.Drawing.Point(68, 10)
        Me.RadioButtonBranchAlfa.Name = "RadioButtonBranchAlfa"
        Me.RadioButtonBranchAlfa.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonBranchAlfa.TabIndex = 62
        Me.RadioButtonBranchAlfa.TabStop = True
        Me.RadioButtonBranchAlfa.UseVisualStyleBackColor = True
        '
        'RadioButtonBranchNum
        '
        Me.RadioButtonBranchNum.AutoSize = True
        Me.RadioButtonBranchNum.Location = New System.Drawing.Point(19, 10)
        Me.RadioButtonBranchNum.Name = "RadioButtonBranchNum"
        Me.RadioButtonBranchNum.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonBranchNum.TabIndex = 61
        Me.RadioButtonBranchNum.TabStop = True
        Me.RadioButtonBranchNum.UseVisualStyleBackColor = True
        '
        'NumericUpDownBranchPos
        '
        Me.NumericUpDownBranchPos.Location = New System.Drawing.Point(197, 129)
        Me.NumericUpDownBranchPos.Name = "NumericUpDownBranchPos"
        Me.NumericUpDownBranchPos.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownBranchPos.TabIndex = 60
        Me.NumericUpDownBranchPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NumericUpDownDcLen
        '
        Me.NumericUpDownDcLen.Location = New System.Drawing.Point(277, 157)
        Me.NumericUpDownDcLen.Name = "NumericUpDownDcLen"
        Me.NumericUpDownDcLen.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownDcLen.TabIndex = 67
        Me.NumericUpDownDcLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadioButtonDcAlfa
        '
        Me.RadioButtonDcAlfa.AutoSize = True
        Me.RadioButtonDcAlfa.Location = New System.Drawing.Point(68, 10)
        Me.RadioButtonDcAlfa.Name = "RadioButtonDcAlfa"
        Me.RadioButtonDcAlfa.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonDcAlfa.TabIndex = 66
        Me.RadioButtonDcAlfa.TabStop = True
        Me.RadioButtonDcAlfa.UseVisualStyleBackColor = True
        '
        'RadioButtonDcNum
        '
        Me.RadioButtonDcNum.AutoSize = True
        Me.RadioButtonDcNum.Location = New System.Drawing.Point(19, 10)
        Me.RadioButtonDcNum.Name = "RadioButtonDcNum"
        Me.RadioButtonDcNum.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonDcNum.TabIndex = 65
        Me.RadioButtonDcNum.TabStop = True
        Me.RadioButtonDcNum.UseVisualStyleBackColor = True
        '
        'NumericUpDownDcPos
        '
        Me.NumericUpDownDcPos.Location = New System.Drawing.Point(197, 157)
        Me.NumericUpDownDcPos.Name = "NumericUpDownDcPos"
        Me.NumericUpDownDcPos.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownDcPos.TabIndex = 64
        Me.NumericUpDownDcPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NumericUpDownCccLen
        '
        Me.NumericUpDownCccLen.Location = New System.Drawing.Point(277, 186)
        Me.NumericUpDownCccLen.Name = "NumericUpDownCccLen"
        Me.NumericUpDownCccLen.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownCccLen.TabIndex = 71
        Me.NumericUpDownCccLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RadioButtonCccAlfa
        '
        Me.RadioButtonCccAlfa.AutoSize = True
        Me.RadioButtonCccAlfa.Location = New System.Drawing.Point(68, 11)
        Me.RadioButtonCccAlfa.Name = "RadioButtonCccAlfa"
        Me.RadioButtonCccAlfa.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonCccAlfa.TabIndex = 70
        Me.RadioButtonCccAlfa.TabStop = True
        Me.RadioButtonCccAlfa.UseVisualStyleBackColor = True
        '
        'RadioButtonCccNum
        '
        Me.RadioButtonCccNum.AutoSize = True
        Me.RadioButtonCccNum.Location = New System.Drawing.Point(19, 11)
        Me.RadioButtonCccNum.Name = "RadioButtonCccNum"
        Me.RadioButtonCccNum.Size = New System.Drawing.Size(14, 13)
        Me.RadioButtonCccNum.TabIndex = 69
        Me.RadioButtonCccNum.TabStop = True
        Me.RadioButtonCccNum.UseVisualStyleBackColor = True
        '
        'NumericUpDownCccPos
        '
        Me.NumericUpDownCccPos.Location = New System.Drawing.Point(197, 186)
        Me.NumericUpDownCccPos.Name = "NumericUpDownCccPos"
        Me.NumericUpDownCccPos.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownCccPos.TabIndex = 68
        Me.NumericUpDownCccPos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'NumericUpDownOverallLength
        '
        Me.NumericUpDownOverallLength.Location = New System.Drawing.Point(277, 232)
        Me.NumericUpDownOverallLength.Name = "NumericUpDownOverallLength"
        Me.NumericUpDownOverallLength.Size = New System.Drawing.Size(45, 20)
        Me.NumericUpDownOverallLength.TabIndex = 73
        Me.NumericUpDownOverallLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(73, 234)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(95, 13)
        Me.Label10.TabIndex = 72
        Me.Label10.Text = "Total longitud Iban"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonCccNum)
        Me.GroupBox1.Controls.Add(Me.RadioButtonCccAlfa)
        Me.GroupBox1.Location = New System.Drawing.Point(362, 177)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(104, 28)
        Me.GroupBox1.TabIndex = 74
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButtonDcAlfa)
        Me.GroupBox2.Controls.Add(Me.RadioButtonDcNum)
        Me.GroupBox2.Location = New System.Drawing.Point(362, 149)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(104, 28)
        Me.GroupBox2.TabIndex = 75
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadioButtonBranchAlfa)
        Me.GroupBox3.Controls.Add(Me.RadioButtonBranchNum)
        Me.GroupBox3.Location = New System.Drawing.Point(362, 121)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(104, 28)
        Me.GroupBox3.TabIndex = 76
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.RadioButtonBankNum)
        Me.GroupBox4.Controls.Add(Me.RadioButtonBankAlfa)
        Me.GroupBox4.Location = New System.Drawing.Point(362, 93)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(104, 28)
        Me.GroupBox4.TabIndex = 77
        Me.GroupBox4.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(73, 264)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(146, 13)
        Me.Label11.TabIndex = 78
        Me.Label11.Text = "(les posicions comencen al 0)"
        '
        'Frm_IbanStructure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 323)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.NumericUpDownOverallLength)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.NumericUpDownCccLen)
        Me.Controls.Add(Me.NumericUpDownCccPos)
        Me.Controls.Add(Me.NumericUpDownDcLen)
        Me.Controls.Add(Me.NumericUpDownDcPos)
        Me.Controls.Add(Me.NumericUpDownBranchLen)
        Me.Controls.Add(Me.NumericUpDownBranchPos)
        Me.Controls.Add(Me.NumericUpDownBankLen)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.NumericUpDownBankPos)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxCountry)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_IbanStructure"
        Me.Text = "Estructura Iban"
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDownBankPos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownBankLen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownBranchLen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownBranchPos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownDcLen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownDcPos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownCccLen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownCccPos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDownOverallLength, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TextBoxCountry As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownBankPos As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents RadioButtonBankNum As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonBankAlfa As System.Windows.Forms.RadioButton
    Friend WithEvents NumericUpDownBankLen As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownBranchLen As System.Windows.Forms.NumericUpDown
    Friend WithEvents RadioButtonBranchAlfa As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonBranchNum As System.Windows.Forms.RadioButton
    Friend WithEvents NumericUpDownBranchPos As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownDcLen As System.Windows.Forms.NumericUpDown
    Friend WithEvents RadioButtonDcAlfa As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonDcNum As System.Windows.Forms.RadioButton
    Friend WithEvents NumericUpDownDcPos As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownCccLen As System.Windows.Forms.NumericUpDown
    Friend WithEvents RadioButtonCccAlfa As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonCccNum As System.Windows.Forms.RadioButton
    Friend WithEvents NumericUpDownCccPos As System.Windows.Forms.NumericUpDown
    Friend WithEvents NumericUpDownOverallLength As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
