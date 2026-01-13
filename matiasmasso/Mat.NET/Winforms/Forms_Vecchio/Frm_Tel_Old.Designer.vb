<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Tel_Old
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
        Me.components = New System.ComponentModel.Container
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        Me.CheckBoxPrivat = New System.Windows.Forms.CheckBox
        Me.TextBoxObs = New System.Windows.Forms.TextBox
        Me.TextBoxNum = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.LabelNum = New System.Windows.Forms.Label
        Me.LabelPais = New System.Windows.Forms.Label
        Me.Xl_Pais1 = New Xl_Pais
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 231)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(389, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(170, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 8
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(281, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 7
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
        Me.ButtonDel.TabIndex = 9
        Me.ButtonDel.Text = "RETROCEDIR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'CheckBoxPrivat
        '
        Me.CheckBoxPrivat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxPrivat.Location = New System.Drawing.Point(233, 181)
        Me.CheckBoxPrivat.Name = "CheckBoxPrivat"
        Me.CheckBoxPrivat.Size = New System.Drawing.Size(144, 16)
        Me.CheckBoxPrivat.TabIndex = 6
        Me.CheckBoxPrivat.TabStop = False
        Me.CheckBoxPrivat.Text = "Privat (No publicar)"
        Me.CheckBoxPrivat.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Location = New System.Drawing.Point(135, 143)
        Me.TextBoxObs.MaxLength = 50
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.Size = New System.Drawing.Size(242, 20)
        Me.TextBoxObs.TabIndex = 5
        '
        'TextBoxNum
        '
        Me.TextBoxNum.Location = New System.Drawing.Point(135, 118)
        Me.TextBoxNum.MaxLength = 16
        Me.TextBoxNum.Name = "TextBoxNum"
        Me.TextBoxNum.Size = New System.Drawing.Size(242, 20)
        Me.TextBoxNum.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(55, 145)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(80, 16)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Observacions:"
        '
        'LabelNum
        '
        Me.LabelNum.Location = New System.Drawing.Point(55, 121)
        Me.LabelNum.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.LabelNum.Name = "LabelNum"
        Me.LabelNum.Size = New System.Drawing.Size(80, 16)
        Me.LabelNum.TabIndex = 2
        Me.LabelNum.Text = "Numero:"
        '
        'LabelPais
        '
        Me.LabelPais.Location = New System.Drawing.Point(55, 97)
        Me.LabelPais.Name = "LabelPais"
        Me.LabelPais.Size = New System.Drawing.Size(40, 16)
        Me.LabelPais.TabIndex = 0
        Me.LabelPais.Text = "Pais:"
        '
        'Xl_Pais1
        '
        Me.Xl_Pais1.FlagVisible = True
        Me.Xl_Pais1.Location = New System.Drawing.Point(135, 96)
        Me.Xl_Pais1.Name = "Xl_Pais1"
        Me.Xl_Pais1.Country = Nothing
        Me.Xl_Pais1.Size = New System.Drawing.Size(60, 15)
        Me.Xl_Pais1.TabIndex = 1
        Me.Xl_Pais1.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.Control
        Me.PictureBox1.Location = New System.Drawing.Point(313, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(64, 64)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 47
        Me.PictureBox1.TabStop = False
        '
        'Frm_Tel2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(389, 262)
        Me.Controls.Add(Me.CheckBoxPrivat)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.TextBoxNum)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabelNum)
        Me.Controls.Add(Me.LabelPais)
        Me.Controls.Add(Me.Xl_Pais1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Tel2"
        Me.Text = "NUMERO DE TELEFON"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents CheckBoxPrivat As System.Windows.Forms.CheckBox
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelNum As System.Windows.Forms.Label
    Friend WithEvents LabelPais As System.Windows.Forms.Label
    Friend WithEvents Xl_Pais1 As Xl_Pais
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
End Class
