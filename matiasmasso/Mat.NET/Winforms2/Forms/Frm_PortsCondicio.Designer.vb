<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PortsCondicio
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
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioButtonPagats = New System.Windows.Forms.RadioButton()
        Me.RadioButtonCarrec = New System.Windows.Forms.RadioButton()
        Me.RadioButtonDeguts = New System.Windows.Forms.RadioButton()
        Me.RadioButtonReculliran = New System.Windows.Forms.RadioButton()
        Me.CheckBoxMOQ = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBoxMoq = New System.Windows.Forms.GroupBox()
        Me.PanelMoq = New System.Windows.Forms.Panel()
        Me.Xl_AmountFee = New Mat.Net.Xl_Amount()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_AmountMOQ = New Mat.Net.Xl_Amount()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        Me.Xl_Contacts1 = New Mat.Net.Xl_Contacts()
        Me.PanelButtons.SuspendLayout()
        Me.GroupBoxMoq.SuspendLayout()
        Me.PanelMoq.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(69, 23)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(372, 20)
        Me.TextBoxNom.TabIndex = 57
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(0, 379)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(459, 31)
        Me.PanelButtons.TabIndex = 55
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(240, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(351, 4)
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
        Me.Label1.Location = New System.Drawing.Point(6, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "Nom"
        '
        'RadioButtonPagats
        '
        Me.RadioButtonPagats.AutoSize = True
        Me.RadioButtonPagats.Location = New System.Drawing.Point(69, 65)
        Me.RadioButtonPagats.Name = "RadioButtonPagats"
        Me.RadioButtonPagats.Size = New System.Drawing.Size(84, 17)
        Me.RadioButtonPagats.TabIndex = 58
        Me.RadioButtonPagats.TabStop = True
        Me.RadioButtonPagats.Text = "Ports pagats"
        Me.RadioButtonPagats.UseVisualStyleBackColor = True
        '
        'RadioButtonCarrec
        '
        Me.RadioButtonCarrec.AutoSize = True
        Me.RadioButtonCarrec.Location = New System.Drawing.Point(69, 88)
        Me.RadioButtonCarrec.Name = "RadioButtonCarrec"
        Me.RadioButtonCarrec.Size = New System.Drawing.Size(107, 17)
        Me.RadioButtonCarrec.TabIndex = 59
        Me.RadioButtonCarrec.TabStop = True
        Me.RadioButtonCarrec.Text = "Càrrec en factura"
        Me.RadioButtonCarrec.UseVisualStyleBackColor = True
        '
        'RadioButtonDeguts
        '
        Me.RadioButtonDeguts.AutoSize = True
        Me.RadioButtonDeguts.Location = New System.Drawing.Point(69, 111)
        Me.RadioButtonDeguts.Name = "RadioButtonDeguts"
        Me.RadioButtonDeguts.Size = New System.Drawing.Size(84, 17)
        Me.RadioButtonDeguts.TabIndex = 60
        Me.RadioButtonDeguts.TabStop = True
        Me.RadioButtonDeguts.Text = "Ports deguts"
        Me.RadioButtonDeguts.UseVisualStyleBackColor = True
        '
        'RadioButtonReculliran
        '
        Me.RadioButtonReculliran.AutoSize = True
        Me.RadioButtonReculliran.Location = New System.Drawing.Point(69, 134)
        Me.RadioButtonReculliran.Name = "RadioButtonReculliran"
        Me.RadioButtonReculliran.Size = New System.Drawing.Size(72, 17)
        Me.RadioButtonReculliran.TabIndex = 61
        Me.RadioButtonReculliran.TabStop = True
        Me.RadioButtonReculliran.Text = "Reculliran"
        Me.RadioButtonReculliran.UseVisualStyleBackColor = True
        '
        'CheckBoxMOQ
        '
        Me.CheckBoxMOQ.AutoSize = True
        Me.CheckBoxMOQ.Location = New System.Drawing.Point(6, 0)
        Me.CheckBoxMOQ.Name = "CheckBoxMOQ"
        Me.CheckBoxMOQ.Size = New System.Drawing.Size(176, 17)
        Me.CheckBoxMOQ.TabIndex = 62
        Me.CheckBoxMOQ.Text = "subjecte a minims de enviament"
        Me.CheckBoxMOQ.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(105, 13)
        Me.Label2.TabIndex = 66
        Me.Label2.Text = "Si no arriba, carregar"
        '
        'GroupBoxMoq
        '
        Me.GroupBoxMoq.Controls.Add(Me.PanelMoq)
        Me.GroupBoxMoq.Controls.Add(Me.CheckBoxMOQ)
        Me.GroupBoxMoq.Location = New System.Drawing.Point(64, 179)
        Me.GroupBoxMoq.Name = "GroupBoxMoq"
        Me.GroupBoxMoq.Size = New System.Drawing.Size(221, 86)
        Me.GroupBoxMoq.TabIndex = 67
        Me.GroupBoxMoq.TabStop = False
        '
        'PanelMoq
        '
        Me.PanelMoq.Controls.Add(Me.Xl_AmountFee)
        Me.PanelMoq.Controls.Add(Me.Label3)
        Me.PanelMoq.Controls.Add(Me.Xl_AmountMOQ)
        Me.PanelMoq.Controls.Add(Me.Label2)
        Me.PanelMoq.Enabled = False
        Me.PanelMoq.Location = New System.Drawing.Point(6, 25)
        Me.PanelMoq.Name = "PanelMoq"
        Me.PanelMoq.Size = New System.Drawing.Size(212, 61)
        Me.PanelMoq.TabIndex = 68
        '
        'Xl_AmountFee
        '
        Me.Xl_AmountFee.Amt = Nothing
        Me.Xl_AmountFee.Location = New System.Drawing.Point(131, 30)
        Me.Xl_AmountFee.Name = "Xl_AmountFee"
        Me.Xl_AmountFee.ReadOnly = False
        Me.Xl_AmountFee.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmountFee.TabIndex = 65
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 13)
        Me.Label3.TabIndex = 67
        Me.Label3.Text = "Enviament minim"
        '
        'Xl_AmountMOQ
        '
        Me.Xl_AmountMOQ.Amt = Nothing
        Me.Xl_AmountMOQ.Location = New System.Drawing.Point(131, 7)
        Me.Xl_AmountMOQ.Name = "Xl_AmountMOQ"
        Me.Xl_AmountMOQ.ReadOnly = False
        Me.Xl_AmountMOQ.Size = New System.Drawing.Size(78, 20)
        Me.Xl_AmountMOQ.TabIndex = 63
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1, 34)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(455, 343)
        Me.TabControl1.TabIndex = 68
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.GroupBoxMoq)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.RadioButtonReculliran)
        Me.TabPage1.Controls.Add(Me.RadioButtonPagats)
        Me.TabPage1.Controls.Add(Me.RadioButtonDeguts)
        Me.TabPage1.Controls.Add(Me.RadioButtonCarrec)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(447, 317)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Contacts1)
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearch1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(447, 317)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Clients"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(291, 6)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Xl_Contacts1
        '
        Me.Xl_Contacts1.AllowUserToAddRows = False
        Me.Xl_Contacts1.AllowUserToDeleteRows = False
        Me.Xl_Contacts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contacts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Contacts1.DisplayObsolets = False
        Me.Xl_Contacts1.Location = New System.Drawing.Point(3, 32)
        Me.Xl_Contacts1.MouseIsDown = False
        Me.Xl_Contacts1.Name = "Xl_Contacts1"
        Me.Xl_Contacts1.ReadOnly = True
        Me.Xl_Contacts1.Size = New System.Drawing.Size(441, 282)
        Me.Xl_Contacts1.TabIndex = 1
        '
        'Frm_PortsCondicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(459, 410)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PanelButtons)
        Me.Name = "Frm_PortsCondicio"
        Me.Text = "Condicions de transport"
        Me.PanelButtons.ResumeLayout(False)
        Me.GroupBoxMoq.ResumeLayout(False)
        Me.GroupBoxMoq.PerformLayout()
        Me.PanelMoq.ResumeLayout(False)
        Me.PanelMoq.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_Contacts1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents RadioButtonPagats As RadioButton
    Friend WithEvents RadioButtonCarrec As RadioButton
    Friend WithEvents RadioButtonDeguts As RadioButton
    Friend WithEvents RadioButtonReculliran As RadioButton
    Friend WithEvents CheckBoxMOQ As CheckBox
    Friend WithEvents Xl_AmountMOQ As Xl_Amount
    Friend WithEvents Xl_AmountFee As Xl_Amount
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBoxMoq As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PanelMoq As Panel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_Contacts1 As Xl_Contacts
End Class
