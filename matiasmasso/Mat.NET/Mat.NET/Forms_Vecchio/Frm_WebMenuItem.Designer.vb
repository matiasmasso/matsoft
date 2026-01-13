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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_Lookup_WebmenuGroup1 = New Xl_Lookup_WebmenuGroup()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxNom_ENG = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxNom_CAT = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBoxNom_ESP = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_Rols1 = New Xl_Rols_Allowed()
        Me.Xl_Lookup_WebRoute1 = New Xl_Lookup_WebRoute()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(483, 382)
        Me.TabControl1.TabIndex = 48
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_Lookup_WebRoute1)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Xl_Lookup_WebmenuGroup1)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.TextBoxNom_ENG)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBoxNom_CAT)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.TextBoxNom_ESP)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(475, 356)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(29, 20)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 55
        Me.Label5.Text = "Grup:"
        '
        'Xl_Lookup_WebmenuGroup1
        '
        Me.Xl_Lookup_WebmenuGroup1.Group = Nothing
        Me.Xl_Lookup_WebmenuGroup1.IsDirty = False
        Me.Xl_Lookup_WebmenuGroup1.Location = New System.Drawing.Point(78, 17)
        Me.Xl_Lookup_WebmenuGroup1.Name = "Xl_Lookup_WebmenuGroup1"
        Me.Xl_Lookup_WebmenuGroup1.Size = New System.Drawing.Size(367, 20)
        Me.Xl_Lookup_WebmenuGroup1.TabIndex = 0
        Me.Xl_Lookup_WebmenuGroup1.Value = Nothing
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(29, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 53
        Me.Label4.Text = "Ruta:"
        '
        'TextBoxNom_ENG
        '
        Me.TextBoxNom_ENG.Location = New System.Drawing.Point(78, 123)
        Me.TextBoxNom_ENG.Name = "TextBoxNom_ENG"
        Me.TextBoxNom_ENG.Size = New System.Drawing.Size(367, 20)
        Me.TextBoxNom_ENG.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(29, 126)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Eng:"
        '
        'TextBoxNom_CAT
        '
        Me.TextBoxNom_CAT.Location = New System.Drawing.Point(78, 97)
        Me.TextBoxNom_CAT.Name = "TextBoxNom_CAT"
        Me.TextBoxNom_CAT.Size = New System.Drawing.Size(367, 20)
        Me.TextBoxNom_CAT.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(29, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "Cat:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(3, 322)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(469, 31)
        Me.Panel1.TabIndex = 47
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(250, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(361, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'TextBoxNom_ESP
        '
        Me.TextBoxNom_ESP.Location = New System.Drawing.Point(78, 71)
        Me.TextBoxNom_ESP.Name = "TextBoxNom_ESP"
        Me.TextBoxNom_ESP.Size = New System.Drawing.Size(367, 20)
        Me.TextBoxNom_ESP.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(29, 74)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(28, 13)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Esp:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_Rols1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(475, 356)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Rols"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_Rols1
        '
        Me.Xl_Rols1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Rols1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_Rols1.Name = "Xl_Rols1"
        Me.Xl_Rols1.Size = New System.Drawing.Size(469, 350)
        Me.Xl_Rols1.TabIndex = 0
        '
        'Xl_Lookup_WebRoute1
        '
        Me.Xl_Lookup_WebRoute1.IsDirty = False
        Me.Xl_Lookup_WebRoute1.Location = New System.Drawing.Point(78, 178)
        Me.Xl_Lookup_WebRoute1.Name = "Xl_Lookup_WebRoute1"
        Me.Xl_Lookup_WebRoute1.Size = New System.Drawing.Size(367, 20)
        Me.Xl_Lookup_WebRoute1.TabIndex = 56
        Me.Xl_Lookup_WebRoute1.Value = Nothing
        Me.Xl_Lookup_WebRoute1.WebPage = Nothing
        '
        'Frm_WebMenuItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 394)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Frm_WebMenuItem"
        Me.Text = "WEB MENUITEM"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBoxNom_ESP As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom_ENG As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom_CAT As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_Lookup_WebmenuGroup1 As Xl_Lookup_WebmenuGroup
    Friend WithEvents Xl_Rols1 As Xl_Rols_Allowed
    Friend WithEvents Xl_Lookup_WebRoute1 As Xl_Lookup_WebRoute
End Class
