<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PdcConfirm
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
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox()
        Me.Xl_Contact1 = New Winforms.Xl_Contact()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DateTimePickerFch = New System.Windows.Forms.DateTimePicker()
        Me.TextBoxRef = New System.Windows.Forms.TextBox()
        Me.DateTimePickerETD = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePickerETA = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBoxObs = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBoxUsr = New System.Windows.Forms.TextBox()
        Me.Xl_DocFile1 = New Winforms.Xl_DocFile()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 1032)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(2496, 74)
        Me.Panel1.TabIndex = 45
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(1912, 10)
        Me.ButtonCancel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(2208, 10)
        Me.ButtonOk.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(277, 57)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonDel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonDel.Enabled = False
        Me.ButtonDel.Location = New System.Drawing.Point(16, 10)
        Me.ButtonDel.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(277, 57)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxLogo.Location = New System.Drawing.Point(2056, 24)
        Me.PictureBoxLogo.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(400, 114)
        Me.PictureBoxLogo.TabIndex = 1
        Me.PictureBoxLogo.TabStop = False
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(1208, 160)
        Me.Xl_Contact1.Margin = New System.Windows.Forms.Padding(21, 17, 21, 17)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.ReadOnly = False
        Me.Xl_Contact1.Size = New System.Drawing.Size(1248, 38)
        Me.Xl_Contact1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1019, 160)
        Me.Label1.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 32)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "proveidor"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1019, 38)
        Me.Label2.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 32)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "data"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1019, 98)
        Me.Label3.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(111, 32)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "numero"
        '
        'DateTimePickerFch
        '
        Me.DateTimePickerFch.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerFch.Location = New System.Drawing.Point(1208, 24)
        Me.DateTimePickerFch.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.DateTimePickerFch.Name = "DateTimePickerFch"
        Me.DateTimePickerFch.Size = New System.Drawing.Size(257, 38)
        Me.DateTimePickerFch.TabIndex = 6
        '
        'TextBoxRef
        '
        Me.TextBoxRef.Location = New System.Drawing.Point(1208, 91)
        Me.TextBoxRef.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxRef.Name = "TextBoxRef"
        Me.TextBoxRef.Size = New System.Drawing.Size(257, 38)
        Me.TextBoxRef.TabIndex = 7
        '
        'DateTimePickerETD
        '
        Me.DateTimePickerETD.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerETD.Location = New System.Drawing.Point(1208, 229)
        Me.DateTimePickerETD.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.DateTimePickerETD.Name = "DateTimePickerETD"
        Me.DateTimePickerETD.Size = New System.Drawing.Size(257, 38)
        Me.DateTimePickerETD.TabIndex = 47
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1019, 243)
        Me.Label4.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 32)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "ETD"
        '
        'DateTimePickerETA
        '
        Me.DateTimePickerETA.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePickerETA.Location = New System.Drawing.Point(1208, 291)
        Me.DateTimePickerETA.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.DateTimePickerETA.Name = "DateTimePickerETA"
        Me.DateTimePickerETA.Size = New System.Drawing.Size(257, 38)
        Me.DateTimePickerETA.TabIndex = 49
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1019, 305)
        Me.Label5.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 32)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "ETA"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(1208, 353)
        Me.TextBoxObs.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(1241, 104)
        Me.TextBoxObs.TabIndex = 51
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1019, 360)
        Me.Label6.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(183, 32)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "observacions"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(1528, 243)
        Me.Label7.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(324, 32)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "(estimated delivery time)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(1528, 305)
        Me.Label8.Margin = New System.Windows.Forms.Padding(8, 0, 8, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(303, 32)
        Me.Label8.TabIndex = 53
        Me.Label8.Text = "(estimated arrival time)"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(1027, 508)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(1429, 446)
        Me.DataGridView1.TabIndex = 54
        '
        'TextBoxUsr
        '
        Me.TextBoxUsr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxUsr.Location = New System.Drawing.Point(1027, 956)
        Me.TextBoxUsr.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.TextBoxUsr.Name = "TextBoxUsr"
        Me.TextBoxUsr.ReadOnly = True
        Me.TextBoxUsr.Size = New System.Drawing.Size(1423, 38)
        Me.TextBoxUsr.TabIndex = 55
        '
        'Xl_DocFile1
        '
        Me.Xl_DocFile1.IsDirty = False
        Me.Xl_DocFile1.Location = New System.Drawing.Point(16, 13)
        Me.Xl_DocFile1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_DocFile1.Name = "Xl_DocFile1"
        Me.Xl_DocFile1.Size = New System.Drawing.Size(933, 1002)
        Me.Xl_DocFile1.TabIndex = 56
        '
        'Frm_PdcConfirm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(2496, 1106)
        Me.Controls.Add(Me.Xl_DocFile1)
        Me.Controls.Add(Me.TextBoxUsr)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.DateTimePickerETA)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DateTimePickerETD)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxRef)
        Me.Controls.Add(Me.DateTimePickerFch)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.MinimumSize = New System.Drawing.Size(1915, 1070)
        Me.Name = "Frm_PdcConfirm"
        Me.Text = "CONFIRMACIO DE COMANDA"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents DateTimePickerFch As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents TextBoxRef As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePickerETD As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerETA As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBoxUsr As System.Windows.Forms.TextBox
    Friend WithEvents Xl_DocFile1 As Xl_DocFile
End Class
