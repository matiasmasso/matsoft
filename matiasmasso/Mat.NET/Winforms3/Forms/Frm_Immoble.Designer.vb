<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Immoble
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
        Me.DateTimePickerFchFrom = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePickerFchTo = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxCadastre = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxAdr = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Xl_LookupZip1 = New Mat.Net.Xl_LookupZip()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_TextBoxNumSuperficie = New Mat.Net.Xl_TextBoxNum()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Xl_Percent1 = New Mat.Net.Xl_Percent()
        Me.ComboBoxTitularitat = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBoxVenda = New System.Windows.Forms.CheckBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_DocfileSrcs1 = New Mat.Net.Xl_DocfileSrcs()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Xl_InventariItems1 = New Mat.Net.Xl_InventariItems()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_DocfileSrcs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.Xl_InventariItems1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateTimePickerFchFrom
        '
        Me.DateTimePickerFchFrom.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchFrom.Location = New System.Drawing.Point(108, 187)
        Me.DateTimePickerFchFrom.Name = "DateTimePickerFchFrom"
        Me.DateTimePickerFchFrom.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchFrom.TabIndex = 6
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(108, 29)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(393, 20)
        Me.TextBoxNom.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 319)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(529, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(310, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 10
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(421, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 9
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
        Me.ButtonDel.TabIndex = 11
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Nom"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 190)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 13)
        Me.Label2.TabIndex = 60
        Me.Label2.Text = "Data adquisició"
        '
        'DateTimePickerFchTo
        '
        Me.DateTimePickerFchTo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePickerFchTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFchTo.Location = New System.Drawing.Point(108, 213)
        Me.DateTimePickerFchTo.Name = "DateTimePickerFchTo"
        Me.DateTimePickerFchTo.Size = New System.Drawing.Size(80, 20)
        Me.DateTimePickerFchTo.TabIndex = 8
        Me.DateTimePickerFchTo.Visible = False
        '
        'TextBoxCadastre
        '
        Me.TextBoxCadastre.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCadastre.Location = New System.Drawing.Point(108, 134)
        Me.TextBoxCadastre.Name = "TextBoxCadastre"
        Me.TextBoxCadastre.Size = New System.Drawing.Size(196, 20)
        Me.TextBoxCadastre.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 137)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 63
        Me.Label4.Text = "Ref.Cadastral"
        '
        'TextBoxAdr
        '
        Me.TextBoxAdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAdr.Location = New System.Drawing.Point(108, 55)
        Me.TextBoxAdr.Name = "TextBoxAdr"
        Me.TextBoxAdr.Size = New System.Drawing.Size(393, 20)
        Me.TextBoxAdr.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(22, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 65
        Me.Label5.Text = "adreça"
        '
        'Xl_LookupZip1
        '
        Me.Xl_LookupZip1.IsDirty = False
        Me.Xl_LookupZip1.Location = New System.Drawing.Point(108, 81)
        Me.Xl_LookupZip1.Name = "Xl_LookupZip1"
        Me.Xl_LookupZip1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupZip1.ReadOnlyLookup = False
        Me.Xl_LookupZip1.Size = New System.Drawing.Size(393, 20)
        Me.Xl_LookupZip1.TabIndex = 2
        Me.Xl_LookupZip1.Value = Nothing
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(22, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 68
        Me.Label6.Text = "població"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(0, 33)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(529, 284)
        Me.TabControl1.TabIndex = 69
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_TextBoxNumSuperficie)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Xl_Percent1)
        Me.TabPage1.Controls.Add(Me.ComboBoxTitularitat)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.CheckBoxVenda)
        Me.TabPage1.Controls.Add(Me.TextBoxNom)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Xl_LookupZip1)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchFrom)
        Me.TabPage1.Controls.Add(Me.TextBoxAdr)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFchTo)
        Me.TabPage1.Controls.Add(Me.TextBoxCadastre)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(521, 258)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_TextBoxNumSuperficie
        '
        Me.Xl_TextBoxNumSuperficie.Location = New System.Drawing.Point(108, 161)
        Me.Xl_TextBoxNumSuperficie.Mat_CustomFormat = Mat.Net.Xl_TextBoxNum.Formats.M2
        Me.Xl_TextBoxNumSuperficie.Mat_FormatString = ""
        Me.Xl_TextBoxNumSuperficie.Name = "Xl_TextBoxNumSuperficie"
        Me.Xl_TextBoxNumSuperficie.ReadOnly = False
        Me.Xl_TextBoxNumSuperficie.Size = New System.Drawing.Size(80, 20)
        Me.Xl_TextBoxNumSuperficie.TabIndex = 73
        Me.Xl_TextBoxNumSuperficie.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(22, 165)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 72
        Me.Label7.Text = "Superficie"
        '
        'Xl_Percent1
        '
        Me.Xl_Percent1.Location = New System.Drawing.Point(310, 107)
        Me.Xl_Percent1.Name = "Xl_Percent1"
        Me.Xl_Percent1.Size = New System.Drawing.Size(60, 20)
        Me.Xl_Percent1.TabIndex = 4
        Me.Xl_Percent1.Text = "100 %"
        Me.Xl_Percent1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_Percent1.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'ComboBoxTitularitat
        '
        Me.ComboBoxTitularitat.FormattingEnabled = True
        Me.ComboBoxTitularitat.ItemHeight = 13
        Me.ComboBoxTitularitat.Items.AddRange(New Object() {"Ple domini", "Nua propietat", "Usdefruit"})
        Me.ComboBoxTitularitat.Location = New System.Drawing.Point(108, 107)
        Me.ComboBoxTitularitat.Name = "ComboBoxTitularitat"
        Me.ComboBoxTitularitat.Size = New System.Drawing.Size(196, 21)
        Me.ComboBoxTitularitat.TabIndex = 71
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(22, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 70
        Me.Label3.Text = "Titularitat"
        '
        'CheckBoxVenda
        '
        Me.CheckBoxVenda.AutoSize = True
        Me.CheckBoxVenda.Location = New System.Drawing.Point(25, 215)
        Me.CheckBoxVenda.Name = "CheckBoxVenda"
        Me.CheckBoxVenda.Size = New System.Drawing.Size(57, 17)
        Me.CheckBoxVenda.TabIndex = 7
        Me.CheckBoxVenda.Text = "Venda"
        Me.CheckBoxVenda.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_DocfileSrcs1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(521, 258)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Descarregues"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Xl_DocfileSrcs1
        '
        Me.Xl_DocfileSrcs1.AllowUserToAddRows = False
        Me.Xl_DocfileSrcs1.AllowUserToDeleteRows = False
        Me.Xl_DocfileSrcs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_DocfileSrcs1.DisplayObsolets = False
        Me.Xl_DocfileSrcs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_DocfileSrcs1.Filter = Nothing
        Me.Xl_DocfileSrcs1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_DocfileSrcs1.MouseIsDown = False
        Me.Xl_DocfileSrcs1.Name = "Xl_DocfileSrcs1"
        Me.Xl_DocfileSrcs1.ReadOnly = True
        Me.Xl_DocfileSrcs1.Size = New System.Drawing.Size(515, 252)
        Me.Xl_DocfileSrcs1.TabIndex = 0
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_InventariItems1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(521, 258)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Inventari"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Xl_InventariItems1
        '
        Me.Xl_InventariItems1.AllowUserToAddRows = False
        Me.Xl_InventariItems1.AllowUserToDeleteRows = False
        Me.Xl_InventariItems1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_InventariItems1.DisplayObsolets = False
        Me.Xl_InventariItems1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_InventariItems1.Filter = Nothing
        Me.Xl_InventariItems1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_InventariItems1.MouseIsDown = False
        Me.Xl_InventariItems1.Name = "Xl_InventariItems1"
        Me.Xl_InventariItems1.ReadOnly = True
        Me.Xl_InventariItems1.Size = New System.Drawing.Size(515, 252)
        Me.Xl_InventariItems1.TabIndex = 0
        '
        'Frm_Immoble
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 350)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Immoble"
        Me.Text = "Immoble"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_DocfileSrcs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.Xl_InventariItems1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DateTimePickerFchFrom As DateTimePicker
    Friend WithEvents TextBoxNom As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DateTimePickerFchTo As DateTimePicker
    Friend WithEvents TextBoxCadastre As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBoxAdr As TextBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Xl_LookupZip1 As Xl_LookupZip
    Friend WithEvents Label6 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Xl_DocfileSrcs1 As Xl_DocfileSrcs
    Friend WithEvents CheckBoxVenda As CheckBox
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_InventariItems1 As Xl_InventariItems
    Friend WithEvents Xl_Percent1 As Xl_Percent
    Friend WithEvents ComboBoxTitularitat As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_TextBoxNumSuperficie As Xl_TextBoxNum
    Friend WithEvents Label7 As Label
End Class
