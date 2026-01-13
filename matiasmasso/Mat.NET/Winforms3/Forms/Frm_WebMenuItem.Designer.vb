<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_WebMenuItem
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxPor = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxActiu = New System.Windows.Forms.CheckBox()
        Me.TextBoxEng = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxCat = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxEsp = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LabelGroup = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_RolsAllowed1 = New Mat.Net.Xl_RolsAllowed()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupboxUrl = New System.Windows.Forms.GroupBox()
        Me.TextBoxUrlEsp = New System.Windows.Forms.TextBox()
        Me.TextBoxUrlPor = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxUrlCat = New System.Windows.Forms.TextBox()
        Me.TextBoxUrlEng = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_RolsAllowed1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupboxUrl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(6, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(390, 373)
        Me.TabControl1.TabIndex = 50
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupboxUrl)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.CheckBoxActiu)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.LabelGroup)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(382, 347)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        '
        'TextBoxPor
        '
        Me.TextBoxPor.Location = New System.Drawing.Point(86, 89)
        Me.TextBoxPor.Name = "TextBoxPor"
        Me.TextBoxPor.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxPor.TabIndex = 14
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(30, 89)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 16)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Por:"
        '
        'CheckBoxActiu
        '
        Me.CheckBoxActiu.AutoSize = True
        Me.CheckBoxActiu.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxActiu.Location = New System.Drawing.Point(322, 325)
        Me.CheckBoxActiu.Margin = New System.Windows.Forms.Padding(1)
        Me.CheckBoxActiu.Name = "CheckBoxActiu"
        Me.CheckBoxActiu.Size = New System.Drawing.Size(50, 17)
        Me.CheckBoxActiu.TabIndex = 12
        Me.CheckBoxActiu.Text = "Actiu"
        Me.CheckBoxActiu.UseVisualStyleBackColor = True
        '
        'TextBoxEng
        '
        Me.TextBoxEng.Location = New System.Drawing.Point(86, 68)
        Me.TextBoxEng.Name = "TextBoxEng"
        Me.TextBoxEng.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxEng.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(30, 68)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Eng:"
        '
        'TextBoxCat
        '
        Me.TextBoxCat.Location = New System.Drawing.Point(86, 46)
        Me.TextBoxCat.Name = "TextBoxCat"
        Me.TextBoxCat.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxCat.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(30, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Cat:"
        '
        'TextBoxEsp
        '
        Me.TextBoxEsp.Location = New System.Drawing.Point(86, 25)
        Me.TextBoxEsp.Name = "TextBoxEsp"
        Me.TextBoxEsp.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxEsp.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(30, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Esp:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 16)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Grup:"
        '
        'LabelGroup
        '
        Me.LabelGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelGroup.Location = New System.Drawing.Point(64, 8)
        Me.LabelGroup.Name = "LabelGroup"
        Me.LabelGroup.Size = New System.Drawing.Size(301, 18)
        Me.LabelGroup.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_RolsAllowed1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(382, 187)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Rols"
        '
        'Xl_RolsAllowed1
        '
        Me.Xl_RolsAllowed1.AllowUserToAddRows = False
        Me.Xl_RolsAllowed1.AllowUserToDeleteRows = False
        Me.Xl_RolsAllowed1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_RolsAllowed1.DisplayObsolets = False
        Me.Xl_RolsAllowed1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_RolsAllowed1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_RolsAllowed1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_RolsAllowed1.MouseIsDown = False
        Me.Xl_RolsAllowed1.Name = "Xl_RolsAllowed1"
        Me.Xl_RolsAllowed1.Size = New System.Drawing.Size(382, 187)
        Me.Xl_RolsAllowed1.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 386)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(400, 31)
        Me.Panel1.TabIndex = 51
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(181, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(292, 4)
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBoxEsp)
        Me.GroupBox1.Controls.Add(Me.TextBoxPor)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TextBoxCat)
        Me.GroupBox1.Controls.Add(Me.TextBoxEng)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(367, 132)
        Me.GroupBox1.TabIndex = 15
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Nom"
        '
        'GroupboxUrl
        '
        Me.GroupboxUrl.Controls.Add(Me.TextBoxUrlEsp)
        Me.GroupboxUrl.Controls.Add(Me.TextBoxUrlPor)
        Me.GroupboxUrl.Controls.Add(Me.Label7)
        Me.GroupboxUrl.Controls.Add(Me.Label8)
        Me.GroupboxUrl.Controls.Add(Me.Label9)
        Me.GroupboxUrl.Controls.Add(Me.TextBoxUrlCat)
        Me.GroupboxUrl.Controls.Add(Me.TextBoxUrlEng)
        Me.GroupboxUrl.Controls.Add(Me.Label10)
        Me.GroupboxUrl.Location = New System.Drawing.Point(11, 189)
        Me.GroupboxUrl.Name = "GroupboxUrl"
        Me.GroupboxUrl.Size = New System.Drawing.Size(367, 132)
        Me.GroupboxUrl.TabIndex = 16
        Me.GroupboxUrl.TabStop = False
        Me.GroupboxUrl.Text = "Url"
        '
        'TextBoxUrlEsp
        '
        Me.TextBoxUrlEsp.Location = New System.Drawing.Point(86, 25)
        Me.TextBoxUrlEsp.Name = "TextBoxUrlEsp"
        Me.TextBoxUrlEsp.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxUrlEsp.TabIndex = 5
        '
        'TextBoxUrlPor
        '
        Me.TextBoxUrlPor.Location = New System.Drawing.Point(86, 89)
        Me.TextBoxUrlPor.Name = "TextBoxUrlPor"
        Me.TextBoxUrlPor.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxUrlPor.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(30, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 16)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "Esp:"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(30, 89)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 16)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Por:"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(30, 46)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 16)
        Me.Label9.TabIndex = 8
        Me.Label9.Text = "Cat:"
        '
        'TextBoxUrlCat
        '
        Me.TextBoxUrlCat.Location = New System.Drawing.Point(86, 46)
        Me.TextBoxUrlCat.Name = "TextBoxUrlCat"
        Me.TextBoxUrlCat.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxUrlCat.TabIndex = 9
        '
        'TextBoxUrlEng
        '
        Me.TextBoxUrlEng.Location = New System.Drawing.Point(86, 68)
        Me.TextBoxUrlEng.Name = "TextBoxUrlEng"
        Me.TextBoxUrlEng.Size = New System.Drawing.Size(275, 20)
        Me.TextBoxUrlEng.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(30, 68)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 16)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "Eng:"
        '
        'Frm_WebMenuItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 417)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "Frm_WebMenuItem"
        Me.Text = "Menu web"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_RolsAllowed1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupboxUrl.ResumeLayout(False)
        Me.GroupboxUrl.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TextBoxEsp As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LabelGroup As Label
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents TextBoxEng As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxCat As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents CheckBoxActiu As CheckBox
    Friend WithEvents Xl_RolsAllowed1 As Xl_RolsAllowed
    Friend WithEvents TextBoxPor As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents GroupboxUrl As GroupBox
    Friend WithEvents TextBoxUrlEsp As TextBox
    Friend WithEvents TextBoxUrlPor As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxUrlCat As TextBox
    Friend WithEvents TextBoxUrlEng As TextBox
    Friend WithEvents Label10 As Label
    Friend WithEvents GroupBox1 As GroupBox
End Class
