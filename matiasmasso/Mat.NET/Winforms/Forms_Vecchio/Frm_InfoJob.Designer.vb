<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_InfoJob
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
        Me.Xl_Image1 = New Xl_Image
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxTit = New System.Windows.Forms.TextBox
        Me.TextBoxDsc = New System.Windows.Forms.TextBox
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.CheckBoxObsolet = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Image1.Location = New System.Drawing.Point(419, 12)
        Me.Xl_Image1.MaxHeight = 0
        Me.Xl_Image1.MaxWidth = 0
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(64, 64)
        Me.Xl_Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal
        Me.Xl_Image1.TabIndex = 0
        Me.Xl_Image1.ZipStream = Nothing
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Títol:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 121)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Descripció:"
        '
        'TextBoxTit
        '
        Me.TextBoxTit.Location = New System.Drawing.Point(17, 86)
        Me.TextBoxTit.MaxLength = 100
        Me.TextBoxTit.Name = "TextBoxTit"
        Me.TextBoxTit.Size = New System.Drawing.Size(466, 20)
        Me.TextBoxTit.TabIndex = 2
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Location = New System.Drawing.Point(18, 137)
        Me.TextBoxDsc.Multiline = True
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(465, 150)
        Me.TextBoxDsc.TabIndex = 4
        '
        'ButtonOk
        '
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(408, 317)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOk.TabIndex = 6
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = True
        '
        'CheckBoxObsolet
        '
        Me.CheckBoxObsolet.AutoSize = True
        Me.CheckBoxObsolet.Location = New System.Drawing.Point(18, 293)
        Me.CheckBoxObsolet.Name = "CheckBoxObsolet"
        Me.CheckBoxObsolet.Size = New System.Drawing.Size(68, 17)
        Me.CheckBoxObsolet.TabIndex = 5
        Me.CheckBoxObsolet.Text = "Obsoleto"
        Me.CheckBoxObsolet.UseVisualStyleBackColor = True
        '
        'Frm_InfoJob
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(495, 341)
        Me.Controls.Add(Me.CheckBoxObsolet)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.TextBoxTit)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Name = "Frm_InfoJob"
        Me.Text = "OFERTA DE EMPLEO"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTit As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDsc As System.Windows.Forms.TextBox
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents CheckBoxObsolet As System.Windows.Forms.CheckBox
End Class
