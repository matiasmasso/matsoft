<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_BlogPost
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
        Me.CheckBoxVisible = New System.Windows.Forms.CheckBox()
        Me.TextBoxSharesCount = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.PanelButtons = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Xl_Image1 = New Xl_Image()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCommentCount = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxVisitCount = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxUrl = New System.Windows.Forms.TextBox()
        Me.TextBoxContingut = New System.Windows.Forms.TextBox()
        Me.TextBoxExcerpt = New System.Windows.Forms.TextBox()
        Me.TextBoxTitol = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextTitol = New Xl_LangsText()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextExcerpt = New Xl_LangsText()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextContingut = New Xl_LangsText()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Xl_LangsTextUrl = New Xl_LangsText()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BrowseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyLinkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Xl_UsrLog1 = New Xl_UsrLog()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.PanelButtons.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 51)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(665, 408)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_UsrLog1)
        Me.TabPage1.Controls.Add(Me.CheckBoxVisible)
        Me.TabPage1.Controls.Add(Me.TextBoxSharesCount)
        Me.TabPage1.Controls.Add(Me.Label9)
        Me.TabPage1.Controls.Add(Me.PanelButtons)
        Me.TabPage1.Controls.Add(Me.Xl_Image1)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.TextBoxCommentCount)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxVisitCount)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.DateTimePicker1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.TextBoxUrl)
        Me.TabPage1.Controls.Add(Me.TextBoxContingut)
        Me.TabPage1.Controls.Add(Me.TextBoxExcerpt)
        Me.TabPage1.Controls.Add(Me.TextBoxTitol)
        Me.TabPage1.Controls.Add(Me.Label8)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(657, 382)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'CheckBoxVisible
        '
        Me.CheckBoxVisible.AutoSize = True
        Me.CheckBoxVisible.Location = New System.Drawing.Point(28, 231)
        Me.CheckBoxVisible.Name = "CheckBoxVisible"
        Me.CheckBoxVisible.Size = New System.Drawing.Size(56, 17)
        Me.CheckBoxVisible.TabIndex = 66
        Me.CheckBoxVisible.Text = "Visible"
        Me.CheckBoxVisible.UseVisualStyleBackColor = True
        '
        'TextBoxSharesCount
        '
        Me.TextBoxSharesCount.Enabled = False
        Me.TextBoxSharesCount.Location = New System.Drawing.Point(89, 197)
        Me.TextBoxSharesCount.Name = "TextBoxSharesCount"
        Me.TextBoxSharesCount.Size = New System.Drawing.Size(97, 20)
        Me.TextBoxSharesCount.TabIndex = 65
        Me.TextBoxSharesCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(25, 200)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(40, 13)
        Me.Label9.TabIndex = 64
        Me.Label9.Text = "Shares"
        '
        'PanelButtons
        '
        Me.PanelButtons.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.PanelButtons.Controls.Add(Me.ButtonCancel)
        Me.PanelButtons.Controls.Add(Me.ButtonOk)
        Me.PanelButtons.Controls.Add(Me.ButtonDel)
        Me.PanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelButtons.Location = New System.Drawing.Point(3, 348)
        Me.PanelButtons.Name = "PanelButtons"
        Me.PanelButtons.Size = New System.Drawing.Size(651, 31)
        Me.PanelButtons.TabIndex = 63
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(432, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(543, 4)
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
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(385, 162)
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(265, 150)
        Me.Xl_Image1.TabIndex = 61
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(384, 148)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(88, 13)
        Me.Label7.TabIndex = 62
        Me.Label7.Text = "imatge (265x150)"
        '
        'TextBoxCommentCount
        '
        Me.TextBoxCommentCount.Enabled = False
        Me.TextBoxCommentCount.Location = New System.Drawing.Point(89, 171)
        Me.TextBoxCommentCount.Name = "TextBoxCommentCount"
        Me.TextBoxCommentCount.Size = New System.Drawing.Size(97, 20)
        Me.TextBoxCommentCount.TabIndex = 16
        Me.TextBoxCommentCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(25, 174)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Comentaris"
        '
        'TextBoxVisitCount
        '
        Me.TextBoxVisitCount.Enabled = False
        Me.TextBoxVisitCount.Location = New System.Drawing.Point(89, 145)
        Me.TextBoxVisitCount.Name = "TextBoxVisitCount"
        Me.TextBoxVisitCount.Size = New System.Drawing.Size(97, 20)
        Me.TextBoxVisitCount.TabIndex = 14
        Me.TextBoxVisitCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Visites"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(89, 15)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(97, 20)
        Me.DateTimePicker1.TabIndex = 12
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 21)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(30, 13)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "Data"
        '
        'TextBoxUrl
        '
        Me.TextBoxUrl.Enabled = False
        Me.TextBoxUrl.Location = New System.Drawing.Point(89, 119)
        Me.TextBoxUrl.Name = "TextBoxUrl"
        Me.TextBoxUrl.Size = New System.Drawing.Size(561, 20)
        Me.TextBoxUrl.TabIndex = 10
        '
        'TextBoxContingut
        '
        Me.TextBoxContingut.Enabled = False
        Me.TextBoxContingut.Location = New System.Drawing.Point(89, 92)
        Me.TextBoxContingut.Name = "TextBoxContingut"
        Me.TextBoxContingut.Size = New System.Drawing.Size(561, 20)
        Me.TextBoxContingut.TabIndex = 9
        '
        'TextBoxExcerpt
        '
        Me.TextBoxExcerpt.Enabled = False
        Me.TextBoxExcerpt.Location = New System.Drawing.Point(89, 66)
        Me.TextBoxExcerpt.Name = "TextBoxExcerpt"
        Me.TextBoxExcerpt.Size = New System.Drawing.Size(561, 20)
        Me.TextBoxExcerpt.TabIndex = 8
        '
        'TextBoxTitol
        '
        Me.TextBoxTitol.Enabled = False
        Me.TextBoxTitol.Location = New System.Drawing.Point(89, 40)
        Me.TextBoxTitol.Name = "TextBoxTitol"
        Me.TextBoxTitol.Size = New System.Drawing.Size(561, 20)
        Me.TextBoxTitol.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(25, 122)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(20, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Url"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(25, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Contingut"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Extracte"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Titol"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_LangsTextTitol)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(657, 371)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Titol"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextTitol
        '
        Me.Xl_LangsTextTitol.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextTitol.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextTitol.Name = "Xl_LangsTextTitol"
        Me.Xl_LangsTextTitol.Size = New System.Drawing.Size(657, 371)
        Me.Xl_LangsTextTitol.TabIndex = 1
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_LangsTextExcerpt)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(657, 371)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Extracte"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextExcerpt
        '
        Me.Xl_LangsTextExcerpt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextExcerpt.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextExcerpt.Name = "Xl_LangsTextExcerpt"
        Me.Xl_LangsTextExcerpt.Size = New System.Drawing.Size(657, 371)
        Me.Xl_LangsTextExcerpt.TabIndex = 1
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Xl_LangsTextContingut)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(657, 371)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Contingut"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextContingut
        '
        Me.Xl_LangsTextContingut.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextContingut.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextContingut.Name = "Xl_LangsTextContingut"
        Me.Xl_LangsTextContingut.Size = New System.Drawing.Size(657, 371)
        Me.Xl_LangsTextContingut.TabIndex = 2
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Xl_LangsTextUrl)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(657, 371)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "Url"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Xl_LangsTextUrl
        '
        Me.Xl_LangsTextUrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LangsTextUrl.Location = New System.Drawing.Point(0, 0)
        Me.Xl_LangsTextUrl.Name = "Xl_LangsTextUrl"
        Me.Xl_LangsTextUrl.Size = New System.Drawing.Size(657, 371)
        Me.Xl_LangsTextUrl.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(657, 371)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Comentaris"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(666, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BrowseToolStripMenuItem, Me.CopyLinkToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'BrowseToolStripMenuItem
        '
        Me.BrowseToolStripMenuItem.Name = "BrowseToolStripMenuItem"
        Me.BrowseToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.BrowseToolStripMenuItem.Text = "Navegar"
        '
        'CopyLinkToolStripMenuItem
        '
        Me.CopyLinkToolStripMenuItem.Name = "CopyLinkToolStripMenuItem"
        Me.CopyLinkToolStripMenuItem.Size = New System.Drawing.Size(143, 22)
        Me.CopyLinkToolStripMenuItem.Text = "Copiar enllaç"
        '
        'Xl_UsrLog1
        '
        Me.Xl_UsrLog1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_UsrLog1.Location = New System.Drawing.Point(3, 322)
        Me.Xl_UsrLog1.Name = "Xl_UsrLog1"
        Me.Xl_UsrLog1.Size = New System.Drawing.Size(647, 20)
        Me.Xl_UsrLog1.TabIndex = 76
        '
        'Frm_BlogPost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(666, 461)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Frm_BlogPost"
        Me.Text = "Post del blog"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.PanelButtons.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents TabPage5 As TabPage
    Friend WithEvents TabPage6 As TabPage
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxUrl As TextBox
    Friend WithEvents TextBoxContingut As TextBox
    Friend WithEvents TextBoxExcerpt As TextBox
    Friend WithEvents TextBoxTitol As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxCommentCount As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxVisitCount As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Label7 As Label
    Friend WithEvents PanelButtons As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Xl_LangsTextTitol As Xl_LangsText
    Friend WithEvents Xl_LangsTextExcerpt As Xl_LangsText
    Friend WithEvents Xl_LangsTextContingut As Xl_LangsText
    Friend WithEvents Xl_LangsTextUrl As Xl_LangsText
    Friend WithEvents TextBoxSharesCount As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents CheckBoxVisible As CheckBox
    Friend WithEvents BrowseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CopyLinkToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_UsrLog1 As Xl_UsrLog
End Class
