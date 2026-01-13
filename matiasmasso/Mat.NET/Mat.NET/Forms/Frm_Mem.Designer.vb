<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Mem
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
        Me.ComboBoxCod = New System.Windows.Forms.ComboBox()
        Me.PictureBoxLocked = New System.Windows.Forms.PictureBox()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_Contact1 = New Mat.NET.Xl_Contact2()
        Me.Xl_User1 = New Mat.NET.Xl_User()
        CType(Me.PictureBoxLocked, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxCod
        '
        Me.ComboBoxCod.FormattingEnabled = True
        Me.ComboBoxCod.Items.AddRange(New Object() {"Despatx", "Comercial", "Impagos"})
        Me.ComboBoxCod.Location = New System.Drawing.Point(170, 56)
        Me.ComboBoxCod.Name = "ComboBoxCod"
        Me.ComboBoxCod.Size = New System.Drawing.Size(172, 21)
        Me.ComboBoxCod.TabIndex = 22
        '
        'PictureBoxLocked
        '
        Me.PictureBoxLocked.Image = Global.Mat.NET.My.Resources.Resources.candado
        Me.PictureBoxLocked.Location = New System.Drawing.Point(422, 30)
        Me.PictureBoxLocked.Name = "PictureBoxLocked"
        Me.PictureBoxLocked.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxLocked.TabIndex = 20
        Me.PictureBoxLocked.TabStop = False
        Me.PictureBoxLocked.Visible = False
        '
        'ButtonDel
        '
        Me.ButtonDel.Location = New System.Drawing.Point(6, 346)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(96, 24)
        Me.ButtonDel.TabIndex = 19
        Me.ButtonDel.Text = "ELIMINAR"
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(238, 346)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(96, 24)
        Me.ButtonCancel.TabIndex = 18
        Me.ButtonCancel.Text = "CANCELAR"
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(342, 346)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(96, 24)
        Me.ButtonOk.TabIndex = 17
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(70, 96)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(368, 231)
        Me.TextBox1.TabIndex = 16
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(70, 27)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(88, 20)
        Me.DateTimePicker1.TabIndex = 15
        Me.DateTimePicker1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 27)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 16)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "data:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(6, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "nom:"
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Contact = Nothing
        Me.Xl_Contact1.Location = New System.Drawing.Point(70, 3)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.ReadOnly = False
        Me.Xl_Contact1.Size = New System.Drawing.Size(368, 20)
        Me.Xl_Contact1.TabIndex = 12
        Me.Xl_Contact1.TabStop = False
        '
        'Xl_User1
        '
        Me.Xl_User1.Location = New System.Drawing.Point(165, 27)
        Me.Xl_User1.Name = "Xl_User1"
        Me.Xl_User1.Size = New System.Drawing.Size(251, 20)
        Me.Xl_User1.TabIndex = 23
        '
        'Frm_Mem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(445, 373)
        Me.Controls.Add(Me.Xl_User1)
        Me.Controls.Add(Me.ComboBoxCod)
        Me.Controls.Add(Me.PictureBoxLocked)
        Me.Controls.Add(Me.ButtonDel)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Name = "Frm_Mem"
        Me.Text = "Frm_Mem"
        CType(Me.PictureBoxLocked, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxCod As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBoxLocked As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact1 As Mat.NET.Xl_Contact2
    Friend WithEvents Xl_User1 As Mat.NET.Xl_User
End Class
