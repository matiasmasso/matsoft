<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Alb_Recollida
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxMotiu = New System.Windows.Forms.TextBox()
        Me.ComboBoxEstat = New System.Windows.Forms.ComboBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxOrigenContact = New System.Windows.Forms.TextBox()
        Me.TextBoxBultos = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TextBoxOrigenTel = New System.Windows.Forms.TextBox()
        Me.Xl_CitOrigen = New Xl_Zip2()
        Me.TextBoxOrigenAdr = New System.Windows.Forms.TextBox()
        Me.TextBoxOrigenNom = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.ComboBoxAccio = New System.Windows.Forms.ComboBox()
        Me.ComboBoxCarrec = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.TextBoxDestiContact = New System.Windows.Forms.TextBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.TextBoxDestiTel = New System.Windows.Forms.TextBox()
        Me.Xl_CitDesti = New Xl_Zip2()
        Me.TextBoxDestiAdr = New System.Windows.Forms.TextBox()
        Me.TextBoxDestiNom = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Panel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 472)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(888, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(669, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(780, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 207)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(888, 263)
        Me.DataGridView1.TabIndex = 42
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TextBoxMotiu)
        Me.GroupBox1.Controls.Add(Me.ComboBoxEstat)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBoxOrigenContact)
        Me.GroupBox1.Controls.Add(Me.TextBoxBultos)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Controls.Add(Me.TextBoxOrigenTel)
        Me.GroupBox1.Controls.Add(Me.Xl_CitOrigen)
        Me.GroupBox1.Controls.Add(Me.TextBoxOrigenAdr)
        Me.GroupBox1.Controls.Add(Me.TextBoxOrigenNom)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(423, 161)
        Me.GroupBox1.TabIndex = 46
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Origen"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(168, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(30, 13)
        Me.Label3.TabIndex = 55
        Me.Label3.Text = "estat"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 54
        Me.Label2.Text = "motiu"
        '
        'TextBoxMotiu
        '
        Me.TextBoxMotiu.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxMotiu.Location = New System.Drawing.Point(82, 119)
        Me.TextBoxMotiu.Multiline = True
        Me.TextBoxMotiu.Name = "TextBoxMotiu"
        Me.TextBoxMotiu.Size = New System.Drawing.Size(333, 36)
        Me.TextBoxMotiu.TabIndex = 53
        '
        'ComboBoxEstat
        '
        Me.ComboBoxEstat.FormattingEnabled = True
        Me.ComboBoxEstat.Location = New System.Drawing.Point(200, 98)
        Me.ComboBoxEstat.Name = "ComboBoxEstat"
        Me.ComboBoxEstat.Size = New System.Drawing.Size(215, 21)
        Me.ComboBoxEstat.TabIndex = 48
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = My.Resources.Resources.People_Blue
        Me.PictureBox2.Location = New System.Drawing.Point(178, 79)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 52
        Me.PictureBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 13)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "bultos"
        '
        'TextBoxOrigenContact
        '
        Me.TextBoxOrigenContact.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOrigenContact.Location = New System.Drawing.Point(200, 77)
        Me.TextBoxOrigenContact.Name = "TextBoxOrigenContact"
        Me.TextBoxOrigenContact.Size = New System.Drawing.Size(215, 20)
        Me.TextBoxOrigenContact.TabIndex = 51
        '
        'TextBoxBultos
        '
        Me.TextBoxBultos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxBultos.Location = New System.Drawing.Point(82, 99)
        Me.TextBoxBultos.Name = "TextBoxBultos"
        Me.TextBoxBultos.Size = New System.Drawing.Size(47, 20)
        Me.TextBoxBultos.TabIndex = 46
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = My.Resources.Resources.tel
        Me.PictureBox1.Location = New System.Drawing.Point(36, 79)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 50
        Me.PictureBox1.TabStop = False
        '
        'TextBoxOrigenTel
        '
        Me.TextBoxOrigenTel.Location = New System.Drawing.Point(58, 77)
        Me.TextBoxOrigenTel.Name = "TextBoxOrigenTel"
        Me.TextBoxOrigenTel.Size = New System.Drawing.Size(106, 20)
        Me.TextBoxOrigenTel.TabIndex = 49
        '
        'Xl_CitOrigen
        '
        Me.Xl_CitOrigen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CitOrigen.Location = New System.Drawing.Point(34, 57)
        Me.Xl_CitOrigen.Name = "Xl_CitOrigen"
        Me.Xl_CitOrigen.Size = New System.Drawing.Size(381, 20)
        Me.Xl_CitOrigen.TabIndex = 48
        '
        'TextBoxOrigenAdr
        '
        Me.TextBoxOrigenAdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOrigenAdr.Location = New System.Drawing.Point(34, 37)
        Me.TextBoxOrigenAdr.Name = "TextBoxOrigenAdr"
        Me.TextBoxOrigenAdr.Size = New System.Drawing.Size(381, 20)
        Me.TextBoxOrigenAdr.TabIndex = 47
        '
        'TextBoxOrigenNom
        '
        Me.TextBoxOrigenNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxOrigenNom.Location = New System.Drawing.Point(34, 17)
        Me.TextBoxOrigenNom.Name = "TextBoxOrigenNom"
        Me.TextBoxOrigenNom.Size = New System.Drawing.Size(381, 20)
        Me.TextBoxOrigenNom.TabIndex = 46
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ComboBoxAccio)
        Me.GroupBox2.Controls.Add(Me.ComboBoxCarrec)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.PictureBox3)
        Me.GroupBox2.Controls.Add(Me.TextBoxDestiContact)
        Me.GroupBox2.Controls.Add(Me.PictureBox4)
        Me.GroupBox2.Controls.Add(Me.TextBoxDestiTel)
        Me.GroupBox2.Controls.Add(Me.Xl_CitDesti)
        Me.GroupBox2.Controls.Add(Me.TextBoxDestiAdr)
        Me.GroupBox2.Controls.Add(Me.TextBoxDestiNom)
        Me.GroupBox2.Location = New System.Drawing.Point(457, 30)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(423, 161)
        Me.GroupBox2.TabIndex = 49
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Destinació"
        '
        'ComboBoxAccio
        '
        Me.ComboBoxAccio.FormattingEnabled = True
        Me.ComboBoxAccio.Location = New System.Drawing.Point(87, 120)
        Me.ComboBoxAccio.Name = "ComboBoxAccio"
        Me.ComboBoxAccio.Size = New System.Drawing.Size(328, 21)
        Me.ComboBoxAccio.TabIndex = 59
        '
        'ComboBoxCarrec
        '
        Me.ComboBoxCarrec.FormattingEnabled = True
        Me.ComboBoxCarrec.Location = New System.Drawing.Point(87, 98)
        Me.ComboBoxCarrec.Name = "ComboBoxCarrec"
        Me.ComboBoxCarrec.Size = New System.Drawing.Size(328, 21)
        Me.ComboBoxCarrec.TabIndex = 58
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(33, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "acció"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(33, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 56
        Me.Label4.Text = "carrec"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = My.Resources.Resources.People_Blue
        Me.PictureBox3.Location = New System.Drawing.Point(178, 79)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox3.TabIndex = 52
        Me.PictureBox3.TabStop = False
        '
        'TextBoxDestiContact
        '
        Me.TextBoxDestiContact.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDestiContact.Location = New System.Drawing.Point(200, 77)
        Me.TextBoxDestiContact.Name = "TextBoxDestiContact"
        Me.TextBoxDestiContact.Size = New System.Drawing.Size(215, 20)
        Me.TextBoxDestiContact.TabIndex = 51
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = My.Resources.Resources.tel
        Me.PictureBox4.Location = New System.Drawing.Point(36, 79)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox4.TabIndex = 50
        Me.PictureBox4.TabStop = False
        '
        'TextBoxDestiTel
        '
        Me.TextBoxDestiTel.Location = New System.Drawing.Point(58, 77)
        Me.TextBoxDestiTel.Name = "TextBoxDestiTel"
        Me.TextBoxDestiTel.Size = New System.Drawing.Size(106, 20)
        Me.TextBoxDestiTel.TabIndex = 49
        '
        'Xl_CitDesti
        '
        Me.Xl_CitDesti.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CitDesti.Location = New System.Drawing.Point(34, 57)
        Me.Xl_CitDesti.Name = "Xl_CitDesti"
        Me.Xl_CitDesti.Size = New System.Drawing.Size(381, 20)
        Me.Xl_CitDesti.TabIndex = 48
        '
        'TextBoxDestiAdr
        '
        Me.TextBoxDestiAdr.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDestiAdr.Location = New System.Drawing.Point(34, 37)
        Me.TextBoxDestiAdr.Name = "TextBoxDestiAdr"
        Me.TextBoxDestiAdr.Size = New System.Drawing.Size(381, 20)
        Me.TextBoxDestiAdr.TabIndex = 47
        '
        'TextBoxDestiNom
        '
        Me.TextBoxDestiNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDestiNom.Location = New System.Drawing.Point(34, 17)
        Me.TextBoxDestiNom.Name = "TextBoxDestiNom"
        Me.TextBoxDestiNom.Size = New System.Drawing.Size(381, 20)
        Me.TextBoxDestiNom.TabIndex = 46
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(790, 9)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(90, 20)
        Me.DateTimePicker1.TabIndex = 50
        '
        'Frm_Alb_Recollida
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(888, 503)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Alb_Recollida"
        Me.Text = "Recollida"
        Me.Panel1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxOrigenContact As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxOrigenTel As System.Windows.Forms.TextBox
    Friend WithEvents Xl_CitOrigen As Xl_Zip2
    Friend WithEvents TextBoxOrigenAdr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxOrigenNom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxBultos As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxDestiContact As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxDestiTel As System.Windows.Forms.TextBox
    Friend WithEvents Xl_CitDesti As Xl_Zip2
    Friend WithEvents TextBoxDestiAdr As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDestiNom As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxMotiu As System.Windows.Forms.TextBox
    Friend WithEvents ComboBoxEstat As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxAccio As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxCarrec As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
