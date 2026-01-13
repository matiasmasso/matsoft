<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Location
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
        Me.LabelComarca = New System.Windows.Forms.Label()
        Me.TextBoxLocation = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Xl_Comarcas1 = New Winforms.Xl_Comarcas()
        Me.Xl_LookupArea1 = New Winforms.Xl_LookupArea()
        Me.Xl_AreaContacts1 = New Winforms.Xl_AreaContacts()
        Me.Xl_AreaDeliveries1 = New Winforms.Xl_AreaDeliveries()
        Me.Xl_LocationBankBranches1 = New Winforms.Xl_LocationBankBranches()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.Xl_AreaContacts1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_AreaDeliveries1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_LocationBankBranches1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.TabControl1.Location = New System.Drawing.Point(0, 6)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(500, 260)
        Me.TabControl1.TabIndex = 65
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LabelComarca)
        Me.TabPage1.Controls.Add(Me.Xl_Comarcas1)
        Me.TabPage1.Controls.Add(Me.Xl_LookupArea1)
        Me.TabPage1.Controls.Add(Me.TextBoxLocation)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(492, 234)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'LabelComarca
        '
        Me.LabelComarca.AutoSize = True
        Me.LabelComarca.Location = New System.Drawing.Point(9, 42)
        Me.LabelComarca.Name = "LabelComarca"
        Me.LabelComarca.Size = New System.Drawing.Size(52, 13)
        Me.LabelComarca.TabIndex = 64
        Me.LabelComarca.Text = "Comarca:"
        Me.LabelComarca.Visible = False
        '
        'TextBoxLocation
        '
        Me.TextBoxLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxLocation.Location = New System.Drawing.Point(90, 66)
        Me.TextBoxLocation.Name = "TextBoxLocation"
        Me.TextBoxLocation.Size = New System.Drawing.Size(308, 20)
        Me.TextBoxLocation.TabIndex = 61
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Població:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 13)
        Me.Label2.TabIndex = 58
        Me.Label2.Text = "Zona:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_AreaContacts1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(492, 234)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Contactes"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Xl_AreaDeliveries1)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(492, 234)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Albarans"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 274)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 31)
        Me.Panel1.TabIndex = 64
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(281, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(392, 4)
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
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Xl_LocationBankBranches1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(492, 234)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Oficines bancàries"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Xl_Comarcas1
        '
        Me.Xl_Comarcas1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Comarcas1.FormattingEnabled = True
        Me.Xl_Comarcas1.Location = New System.Drawing.Point(90, 39)
        Me.Xl_Comarcas1.Name = "Xl_Comarcas1"
        Me.Xl_Comarcas1.Size = New System.Drawing.Size(308, 21)
        Me.Xl_Comarcas1.TabIndex = 63
        Me.Xl_Comarcas1.Visible = False
        '
        'Xl_LookupArea1
        '
        Me.Xl_LookupArea1.IsDirty = False
        Me.Xl_LookupArea1.Location = New System.Drawing.Point(90, 13)
        Me.Xl_LookupArea1.Name = "Xl_LookupArea1"
        Me.Xl_LookupArea1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupArea1.Size = New System.Drawing.Size(308, 20)
        Me.Xl_LookupArea1.TabIndex = 62
        Me.Xl_LookupArea1.Value = Nothing
        '
        'Xl_AreaContacts1
        '
        Me.Xl_AreaContacts1.AllowUserToAddRows = False
        Me.Xl_AreaContacts1.AllowUserToDeleteRows = False
        Me.Xl_AreaContacts1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_AreaContacts1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AreaContacts1.Filter = Nothing
        Me.Xl_AreaContacts1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_AreaContacts1.Name = "Xl_AreaContacts1"
        Me.Xl_AreaContacts1.ReadOnly = True
        Me.Xl_AreaContacts1.Size = New System.Drawing.Size(486, 228)
        Me.Xl_AreaContacts1.TabIndex = 0
        '
        'Xl_AreaDeliveries1
        '
        Me.Xl_AreaDeliveries1.AllowUserToAddRows = False
        Me.Xl_AreaDeliveries1.AllowUserToDeleteRows = False
        Me.Xl_AreaDeliveries1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_AreaDeliveries1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_AreaDeliveries1.Filter = Nothing
        Me.Xl_AreaDeliveries1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_AreaDeliveries1.Name = "Xl_AreaDeliveries1"
        Me.Xl_AreaDeliveries1.ReadOnly = True
        Me.Xl_AreaDeliveries1.Size = New System.Drawing.Size(492, 234)
        Me.Xl_AreaDeliveries1.TabIndex = 0
        '
        'Xl_LocationBankBranches1
        '
        Me.Xl_LocationBankBranches1.AllowUserToAddRows = False
        Me.Xl_LocationBankBranches1.AllowUserToDeleteRows = False
        Me.Xl_LocationBankBranches1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_LocationBankBranches1.DisplayObsolets = False
        Me.Xl_LocationBankBranches1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_LocationBankBranches1.Filter = Nothing
        Me.Xl_LocationBankBranches1.Location = New System.Drawing.Point(3, 3)
        Me.Xl_LocationBankBranches1.MouseIsDown = False
        Me.Xl_LocationBankBranches1.Name = "Xl_LocationBankBranches1"
        Me.Xl_LocationBankBranches1.ReadOnly = True
        Me.Xl_LocationBankBranches1.Size = New System.Drawing.Size(486, 228)
        Me.Xl_LocationBankBranches1.TabIndex = 0
        '
        'Frm_Location
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(500, 305)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Location"
        Me.Text = "Població"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.Xl_AreaContacts1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_AreaDeliveries1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_LocationBankBranches1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxLocation As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents Xl_AreaDeliveries1 As Xl_AreaDeliveries
    Friend WithEvents Xl_AreaContacts1 As Xl_AreaContacts
    Friend WithEvents Xl_LookupArea1 As Xl_LookupArea
    Friend WithEvents LabelComarca As Label
    Friend WithEvents Xl_Comarcas1 As Xl_Comarcas
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents Xl_LocationBankBranches1 As Xl_LocationBankBranches
End Class
