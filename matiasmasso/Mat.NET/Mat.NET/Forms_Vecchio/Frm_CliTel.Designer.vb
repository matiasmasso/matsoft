<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class Frm_CliTel
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Xl_Contact1 = New Xl_Contact
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBoxTel = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TextBoxObs = New System.Windows.Forms.TextBox
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.PictureBoxIco = New System.Windows.Forms.PictureBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioButtonTel = New System.Windows.Forms.RadioButton
        Me.RadioButtonFax = New System.Windows.Forms.RadioButton
        Me.RadioButtonMovil = New System.Windows.Forms.RadioButton
        CType(Me.PictureBoxIco, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Location = New System.Drawing.Point(35, 34)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(426, 20)
        Me.Xl_Contact1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(34, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Contacte"
        '
        'TextBoxTel
        '
        Me.TextBoxTel.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxTel.Location = New System.Drawing.Point(57, 84)
        Me.TextBoxTel.Name = "TextBoxTel"
        Me.TextBoxTel.Size = New System.Drawing.Size(198, 20)
        Me.TextBoxTel.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(34, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Numero de telefon"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(34, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Observació"
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Location = New System.Drawing.Point(35, 135)
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(220, 20)
        Me.TextBoxObs.TabIndex = 4
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.AutoSize = True
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(35, 173)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(48, 17)
        Me.CheckBoxPrivat.TabIndex = 6
        Me.CheckBoxPrivat.Text = "privat"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(7, 233)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(119, 26)
        Me.ButtonCancel.TabIndex = 7
        Me.ButtonCancel.TabStop = False
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(389, 233)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(119, 26)
        Me.ButtonOk.TabIndex = 8
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'PictureBoxIco
        '
        Me.PictureBoxIco.Location = New System.Drawing.Point(35, 88)
        Me.PictureBoxIco.Name = "PictureBoxIco"
        Me.PictureBoxIco.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxIco.TabIndex = 9
        Me.PictureBoxIco.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonMovil)
        Me.GroupBox1.Controls.Add(Me.RadioButtonFax)
        Me.GroupBox1.Controls.Add(Me.RadioButtonTel)
        Me.GroupBox1.Location = New System.Drawing.Point(381, 78)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(79, 95)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "linia"
        '
        'RadioButtonTel
        '
        Me.RadioButtonTel.AutoSize = True
        Me.RadioButtonTel.Location = New System.Drawing.Point(7, 22)
        Me.RadioButtonTel.Name = "RadioButtonTel"
        Me.RadioButtonTel.Size = New System.Drawing.Size(53, 17)
        Me.RadioButtonTel.TabIndex = 0
        Me.RadioButtonTel.Text = "telefon"
        '
        'RadioButtonFax
        '
        Me.RadioButtonFax.AutoSize = True
        Me.RadioButtonFax.Location = New System.Drawing.Point(7, 39)
        Me.RadioButtonFax.Name = "RadioButtonFax"
        Me.RadioButtonFax.Size = New System.Drawing.Size(35, 17)
        Me.RadioButtonFax.TabIndex = 1
        Me.RadioButtonFax.Text = "fax"
        '
        'RadioButtonMovil
        '
        Me.RadioButtonMovil.AutoSize = True
        Me.RadioButtonMovil.Location = New System.Drawing.Point(7, 57)
        Me.RadioButtonMovil.Name = "RadioButtonMovil"
        Me.RadioButtonMovil.Size = New System.Drawing.Size(45, 17)
        Me.RadioButtonMovil.TabIndex = 2
        Me.RadioButtonMovil.Text = "movil"
        '
        'Frm_CliTel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(510, 266)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBoxIco)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.CheckBoxPrivat)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBoxTel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Name = "Frm_CliTel"
        Me.Text = "NUMERO DE TELEFON"
        CType(Me.PictureBoxIco, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTel As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxPrivat As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents PictureBoxIco As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonMovil As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonFax As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonTel As System.Windows.Forms.RadioButton
End Class
