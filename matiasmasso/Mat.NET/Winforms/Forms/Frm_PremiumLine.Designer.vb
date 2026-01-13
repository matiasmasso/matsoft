<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PremiumLine
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
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_ProductCategories1 = New Winforms.Xl_ProductCategories()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_PremiumCustomers1 = New Winforms.Xl_PremiumCustomers()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.LabelSummary = New System.Windows.Forms.Label()
        Me.ButtonExcelMailing = New System.Windows.Forms.Button()
        Me.Xl_LookupCustomersxChannel1 = New Winforms.Xl_LookupCustomersxChannel()
        Me.CheckBoxFilterChannel = New System.Windows.Forms.CheckBox()
        Me.CheckboxFilterArea = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupAtlas1 = New Winforms.Xl_LookupAtlas()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_PremiumCustomers1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(326, 24)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePicker1.TabIndex = 59
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(42, 24)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(281, 20)
        Me.TextBoxNom.TabIndex = 58
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 419)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(413, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(193, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(305, 4)
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
        Me.Label1.Location = New System.Drawing.Point(7, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Nom"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(323, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(0, 13)
        Me.Label2.TabIndex = 60
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(1, 71)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(411, 344)
        Me.TabControl1.TabIndex = 63
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_ProductCategories1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage1.Size = New System.Drawing.Size(403, 318)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Productes"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.AllowRemoveOnContextMenu = False
        Me.Xl_ProductCategories1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(1, 1)
        Me.Xl_ProductCategories1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.ShowObsolets = False
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(401, 316)
        Me.Xl_ProductCategories1.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_TextboxSearch1)
        Me.TabPage2.Controls.Add(Me.Xl_PremiumCustomers1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(1)
        Me.TabPage2.Size = New System.Drawing.Size(403, 201)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Clients"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(221, 4)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(180, 20)
        Me.Xl_TextboxSearch1.TabIndex = 2
        '
        'Xl_PremiumCustomers1
        '
        Me.Xl_PremiumCustomers1.AllowUserToAddRows = False
        Me.Xl_PremiumCustomers1.AllowUserToDeleteRows = False
        Me.Xl_PremiumCustomers1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PremiumCustomers1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PremiumCustomers1.DisplayObsolets = False
        Me.Xl_PremiumCustomers1.Filter = Nothing
        Me.Xl_PremiumCustomers1.Location = New System.Drawing.Point(5, 28)
        Me.Xl_PremiumCustomers1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_PremiumCustomers1.MouseIsDown = False
        Me.Xl_PremiumCustomers1.Name = "Xl_PremiumCustomers1"
        Me.Xl_PremiumCustomers1.ReadOnly = True
        Me.Xl_PremiumCustomers1.RowTemplate.Height = 40
        Me.Xl_PremiumCustomers1.Size = New System.Drawing.Size(397, 171)
        Me.Xl_PremiumCustomers1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.LabelSummary)
        Me.TabPage3.Controls.Add(Me.ButtonExcelMailing)
        Me.TabPage3.Controls.Add(Me.Xl_LookupCustomersxChannel1)
        Me.TabPage3.Controls.Add(Me.CheckBoxFilterChannel)
        Me.TabPage3.Controls.Add(Me.CheckboxFilterArea)
        Me.TabPage3.Controls.Add(Me.Xl_LookupAtlas1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(403, 201)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Circulars"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'LabelSummary
        '
        Me.LabelSummary.AutoSize = True
        Me.LabelSummary.Location = New System.Drawing.Point(111, 112)
        Me.LabelSummary.Name = "LabelSummary"
        Me.LabelSummary.Size = New System.Drawing.Size(139, 13)
        Me.LabelSummary.TabIndex = 5
        Me.LabelSummary.Text = "(cap destinatari seleccionat)"
        '
        'ButtonExcelMailing
        '
        Me.ButtonExcelMailing.Location = New System.Drawing.Point(111, 145)
        Me.ButtonExcelMailing.Name = "ButtonExcelMailing"
        Me.ButtonExcelMailing.Size = New System.Drawing.Size(140, 23)
        Me.ButtonExcelMailing.TabIndex = 4
        Me.ButtonExcelMailing.Text = "Excel emails"
        Me.ButtonExcelMailing.UseVisualStyleBackColor = True
        '
        'Xl_LookupCustomersxChannel1
        '
        Me.Xl_LookupCustomersxChannel1.IsDirty = False
        Me.Xl_LookupCustomersxChannel1.Location = New System.Drawing.Point(111, 71)
        Me.Xl_LookupCustomersxChannel1.Name = "Xl_LookupCustomersxChannel1"
        Me.Xl_LookupCustomersxChannel1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCustomersxChannel1.ReadOnlyLookup = False
        Me.Xl_LookupCustomersxChannel1.Size = New System.Drawing.Size(285, 20)
        Me.Xl_LookupCustomersxChannel1.TabIndex = 3
        Me.Xl_LookupCustomersxChannel1.Value = Nothing
        Me.Xl_LookupCustomersxChannel1.Visible = False
        '
        'CheckBoxFilterChannel
        '
        Me.CheckBoxFilterChannel.AutoSize = True
        Me.CheckBoxFilterChannel.Location = New System.Drawing.Point(7, 71)
        Me.CheckBoxFilterChannel.Name = "CheckBoxFilterChannel"
        Me.CheckBoxFilterChannel.Size = New System.Drawing.Size(98, 17)
        Me.CheckBoxFilterChannel.TabIndex = 2
        Me.CheckBoxFilterChannel.Text = "Filtrar per canal"
        Me.CheckBoxFilterChannel.UseVisualStyleBackColor = True
        '
        'CheckboxFilterArea
        '
        Me.CheckboxFilterArea.AutoSize = True
        Me.CheckboxFilterArea.Location = New System.Drawing.Point(8, 37)
        Me.CheckboxFilterArea.Name = "CheckboxFilterArea"
        Me.CheckboxFilterArea.Size = New System.Drawing.Size(87, 17)
        Me.CheckboxFilterArea.TabIndex = 1
        Me.CheckboxFilterArea.Text = "Filtre de area"
        Me.CheckboxFilterArea.UseVisualStyleBackColor = True
        '
        'Xl_LookupAtlas1
        '
        Me.Xl_LookupAtlas1.IsDirty = False
        Me.Xl_LookupAtlas1.Location = New System.Drawing.Point(111, 36)
        Me.Xl_LookupAtlas1.Name = "Xl_LookupAtlas1"
        Me.Xl_LookupAtlas1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupAtlas1.ReadOnlyLookup = False
        Me.Xl_LookupAtlas1.Size = New System.Drawing.Size(285, 20)
        Me.Xl_LookupAtlas1.TabIndex = 0
        Me.Xl_LookupAtlas1.Value = Nothing
        Me.Xl_LookupAtlas1.Visible = False
        '
        'Frm_PremiumLine
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "Frm_PremiumLine"
        Me.Text = "Gama Premium"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_PremiumCustomers1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Xl_ProductCategories1 As Xl_ProductCategories
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_PremiumCustomers1 As Xl_PremiumCustomers
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents CheckboxFilterArea As CheckBox
    Friend WithEvents Xl_LookupAtlas1 As Xl_LookupAtlas
    Friend WithEvents ButtonExcelMailing As Button
    Friend WithEvents Xl_LookupCustomersxChannel1 As Xl_LookupCustomersxChannel
    Friend WithEvents CheckBoxFilterChannel As CheckBox
    Friend WithEvents LabelSummary As Label
End Class
