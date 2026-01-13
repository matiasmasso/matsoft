<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Content
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
        Me.Xl_LookupTextboxButtonUrl = New Xl_LookupTextboxButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxVisitCount = New System.Windows.Forms.TextBox()
        Me.CheckBoxVisible = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextTitol = New Xl_LangsText()
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextExcerpt = New Xl_LangsText()
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextContingut = New Xl_LangsText()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_LangsTextUrl = New Xl_LangsText()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage7)
        Me.TabControl1.Controls.Add(Me.TabPage8)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 22)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(797, 454)
        Me.TabControl1.TabIndex = 73
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_LookupTextboxButtonUrl)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxVisitCount)
        Me.TabPage1.Controls.Add(Me.CheckBoxVisible)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(789, 428)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_LookupTextboxButtonUrl
        '
        Me.Xl_LookupTextboxButtonUrl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupTextboxButtonUrl.IsDirty = False
        Me.Xl_LookupTextboxButtonUrl.Location = New System.Drawing.Point(121, 55)
        Me.Xl_LookupTextboxButtonUrl.Name = "Xl_LookupTextboxButtonUrl"
        Me.Xl_LookupTextboxButtonUrl.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupTextboxButtonUrl.ReadOnlyLookup = False
        Me.Xl_LookupTextboxButtonUrl.Size = New System.Drawing.Size(660, 20)
        Me.Xl_LookupTextboxButtonUrl.TabIndex = 75
        Me.Xl_LookupTextboxButtonUrl.Value = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "Visites"
        '
        'TextBoxVisitCount
        '
        Me.TextBoxVisitCount.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxVisitCount.Enabled = False
        Me.TextBoxVisitCount.Location = New System.Drawing.Point(121, 81)
        Me.TextBoxVisitCount.Name = "TextBoxVisitCount"
        Me.TextBoxVisitCount.ReadOnly = True
        Me.TextBoxVisitCount.Size = New System.Drawing.Size(202, 20)
        Me.TextBoxVisitCount.TabIndex = 74
        Me.TextBoxVisitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxVisible
        '
        Me.CheckBoxVisible.AutoSize = True
        Me.CheckBoxVisible.Checked = True
        Me.CheckBoxVisible.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxVisible.Location = New System.Drawing.Point(14, 115)
        Me.CheckBoxVisible.Name = "CheckBoxVisible"
        Me.CheckBoxVisible.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxVisible.TabIndex = 49
        Me.CheckBoxVisible.Text = "Visible"
        Me.CheckBoxVisible.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(20, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Url"
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Xl_LangsTextTitol)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(789, 428)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Titol"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextTitol
        '
        Me.Xl_LangsTextTitol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextTitol.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextTitol.Name = "Xl_LangsTextTitol"
        Me.Xl_LangsTextTitol.Size = New System.Drawing.Size(789, 428)
        Me.Xl_LangsTextTitol.TabIndex = 0
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.Xl_LangsTextExcerpt)
        Me.TabPage7.Location = New System.Drawing.Point(4, 22)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Size = New System.Drawing.Size(789, 428)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "Extracte"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextExcerpt
        '
        Me.Xl_LangsTextExcerpt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextExcerpt.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextExcerpt.Name = "Xl_LangsTextExcerpt"
        Me.Xl_LangsTextExcerpt.Size = New System.Drawing.Size(789, 428)
        Me.Xl_LangsTextExcerpt.TabIndex = 0
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.Xl_LangsTextContingut)
        Me.TabPage8.Location = New System.Drawing.Point(4, 22)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(789, 428)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "Contingut"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextContingut
        '
        Me.Xl_LangsTextContingut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextContingut.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextContingut.Name = "Xl_LangsTextContingut"
        Me.Xl_LangsTextContingut.Size = New System.Drawing.Size(789, 428)
        Me.Xl_LangsTextContingut.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_LangsTextUrl)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(789, 428)
        Me.TabPage2.TabIndex = 8
        Me.TabPage2.Text = "Url"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 478)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(800, 31)
        Me.Panel1.TabIndex = 74
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(581, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(692, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
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
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Xl_LangsTextUrl
        '
        Me.Xl_LangsTextUrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextUrl.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextUrl.Name = "Xl_LangsTextUrl"
        Me.Xl_LangsTextUrl.Size = New System.Drawing.Size(789, 428)
        Me.Xl_LangsTextUrl.TabIndex = 2
        '
        'Frm_Content
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 509)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_Content"
        Me.Text = "Contingut"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents Xl_LangsTextTitol As Xl_LangsText
    Friend WithEvents TabPage7 As TabPage
    Friend WithEvents Xl_LangsTextExcerpt As Xl_LangsText
    Friend WithEvents TabPage8 As TabPage
    Friend WithEvents Xl_LangsTextContingut As Xl_LangsText
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxVisitCount As TextBox
    Friend WithEvents CheckBoxVisible As CheckBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Xl_LookupTextboxButtonUrl As Xl_LookupTextboxButton
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_LangsTextUrl As Xl_LangsText
End Class
