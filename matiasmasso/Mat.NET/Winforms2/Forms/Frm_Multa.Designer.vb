<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Multa
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Xl_Amount1 = New Xl_Amount()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxPagat = New System.Windows.Forms.CheckBox()
        Me.DateTimePickerPagat = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePickerVto = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxExpedient = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_Contact2Emisor = New Xl_Contact2()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Xl_DocfileSrcs1 = New Xl_DocfileSrcs()
        Me.Xl_LookupVehicle1 = New Xl_LookupVehicle()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.Xl_DocfileSrcs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 371)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(423, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(204, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 9
        Me.ButtonCancel.Text = "Cancel.lar"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(315, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 8
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
        Me.Label1.Location = New System.Drawing.Point(32, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Entitat emissora"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(0, 31)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(423, 338)
        Me.TabControl1.TabIndex = 60
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Xl_LookupVehicle1)
        Me.TabPage1.Controls.Add(Me.Xl_Amount1)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.CheckBoxPagat)
        Me.TabPage1.Controls.Add(Me.DateTimePickerPagat)
        Me.TabPage1.Controls.Add(Me.DateTimePickerVto)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.TextBoxExpedient)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.DateTimePickerFch)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Xl_Contact2Emisor)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(415, 312)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Xl_Amount1
        '
        Me.Xl_Amount1.Amt = Nothing
        Me.Xl_Amount1.Location = New System.Drawing.Point(132, 240)
        Me.Xl_Amount1.Name = "Xl_Amount1"
        Me.Xl_Amount1.ReadOnly = False
        Me.Xl_Amount1.Size = New System.Drawing.Size(108, 20)
        Me.Xl_Amount1.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(32, 242)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 69
        Me.Label6.Text = "Import"
        '
        'CheckBoxPagat
        '
        Me.CheckBoxPagat.AutoSize = True
        Me.CheckBoxPagat.Location = New System.Drawing.Point(36, 209)
        Me.CheckBoxPagat.Name = "CheckBoxPagat"
        Me.CheckBoxPagat.Size = New System.Drawing.Size(74, 17)
        Me.CheckBoxPagat.TabIndex = 5
        Me.CheckBoxPagat.Text = "Pagament"
        Me.CheckBoxPagat.UseVisualStyleBackColor = True
        '
        'DateTimePickerPagat
        '
        Me.DateTimePickerPagat.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerPagat.Location = New System.Drawing.Point(132, 207)
        Me.DateTimePickerPagat.Name = "DateTimePickerPagat"
        Me.DateTimePickerPagat.Size = New System.Drawing.Size(108, 20)
        Me.DateTimePickerPagat.TabIndex = 6
        Me.DateTimePickerPagat.Visible = False
        '
        'DateTimePickerVto
        '
        Me.DateTimePickerVto.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerVto.Location = New System.Drawing.Point(132, 181)
        Me.DateTimePickerVto.Name = "DateTimePickerVto"
        Me.DateTimePickerVto.Size = New System.Drawing.Size(108, 20)
        Me.DateTimePickerVto.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(33, 185)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 64
        Me.Label5.Text = "Venciment"
        '
        'TextBoxExpedient
        '
        Me.TextBoxExpedient.Location = New System.Drawing.Point(132, 81)
        Me.TextBoxExpedient.Name = "TextBoxExpedient"
        Me.TextBoxExpedient.Size = New System.Drawing.Size(253, 20)
        Me.TextBoxExpedient.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 13)
        Me.Label4.TabIndex = 62
        Me.Label4.Text = "Expedient"
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(132, 155)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(108, 20)
        Me.DateTimePickerFch.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 159)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 60
        Me.Label3.Text = "Data infracció"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 112)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 59
        Me.Label2.Text = "Vehicle"
        '
        'Xl_Contact2Emisor
        '
        Me.Xl_Contact2Emisor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact2Emisor.Contact = Nothing
        Me.Xl_Contact2Emisor.Location = New System.Drawing.Point(132, 55)
        Me.Xl_Contact2Emisor.Name = "Xl_Contact2Emisor"
        Me.Xl_Contact2Emisor.ReadOnly = False
        Me.Xl_Contact2Emisor.Size = New System.Drawing.Size(253, 20)
        Me.Xl_Contact2Emisor.TabIndex = 0
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Xl_DocfileSrcs1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(415, 300)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Documents"
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
        Me.Xl_DocfileSrcs1.Size = New System.Drawing.Size(409, 294)
        Me.Xl_DocfileSrcs1.TabIndex = 0
        '
        'Xl_LookupVehicle1
        '
        Me.Xl_LookupVehicle1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupVehicle1.IsDirty = False
        Me.Xl_LookupVehicle1.Location = New System.Drawing.Point(132, 108)
        Me.Xl_LookupVehicle1.Name = "Xl_LookupVehicle1"
        Me.Xl_LookupVehicle1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupVehicle1.Size = New System.Drawing.Size(253, 20)
        Me.Xl_LookupVehicle1.TabIndex = 2
        Me.Xl_LookupVehicle1.Value = Nothing
        Me.Xl_LookupVehicle1.Vehicle = Nothing
        '
        'Frm_Multa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 402)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Multa"
        Me.Text = "Multa"
        Me.Panel1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.Xl_DocfileSrcs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents Xl_Contact2Emisor As Xl_Contact2
    Friend WithEvents Xl_DocfileSrcs1 As Xl_DocfileSrcs
    Friend WithEvents CheckBoxPagat As CheckBox
    Friend WithEvents DateTimePickerPagat As DateTimePicker
    Friend WithEvents DateTimePickerVto As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents TextBoxExpedient As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents DateTimePickerFch As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_Amount1 As Xl_Amount
    Friend WithEvents Label6 As Label
    Friend WithEvents Xl_LookupVehicle1 As Xl_LookupVehicle
End Class
