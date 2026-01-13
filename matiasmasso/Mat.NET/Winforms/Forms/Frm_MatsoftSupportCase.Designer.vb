<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_MatsoftSupportCase
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
        Me.TextBoxDsc = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelFch = New System.Windows.Forms.Label()
        Me.ComboBoxProduct = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_Screenshot1 = New Winforms.Xl_Screenshot()
        Me.TextBoxAnswer = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LabelFchValue = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Screenshot1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxDsc
        '
        Me.TextBoxDsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDsc.Location = New System.Drawing.Point(13, 131)
        Me.TextBoxDsc.Multiline = True
        Me.TextBoxDsc.Name = "TextBoxDsc"
        Me.TextBoxDsc.Size = New System.Drawing.Size(613, 118)
        Me.TextBoxDsc.TabIndex = 58
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 412)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(626, 31)
        Me.Panel1.TabIndex = 56
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(407, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(518, 4)
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
        Me.Label1.Location = New System.Drawing.Point(11, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(448, 13)
        Me.Label1.TabIndex = 57
        Me.Label1.Text = "Descripció (passos per reproduir el problema, incloent dades concretes com client" &
    " o producte)"
        '
        'LabelFch
        '
        Me.LabelFch.AutoSize = True
        Me.LabelFch.Location = New System.Drawing.Point(16, 42)
        Me.LabelFch.Name = "LabelFch"
        Me.LabelFch.Size = New System.Drawing.Size(28, 13)
        Me.LabelFch.TabIndex = 60
        Me.LabelFch.Text = "data"
        '
        'ComboBoxProduct
        '
        Me.ComboBoxProduct.FormattingEnabled = True
        Me.ComboBoxProduct.Items.AddRange(New Object() {"", "Mat.Net", "Web", "Taller", "Altres"})
        Me.ComboBoxProduct.Location = New System.Drawing.Point(73, 10)
        Me.ComboBoxProduct.Margin = New System.Windows.Forms.Padding(1)
        Me.ComboBoxProduct.Name = "ComboBoxProduct"
        Me.ComboBoxProduct.Size = New System.Drawing.Size(80, 21)
        Me.ComboBoxProduct.TabIndex = 61
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 13)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 62
        Me.Label3.Text = "software"
        '
        'Xl_Screenshot1
        '
        Me.Xl_Screenshot1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Screenshot1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Xl_Screenshot1.image = Nothing
        Me.Xl_Screenshot1.Location = New System.Drawing.Point(422, 10)
        Me.Xl_Screenshot1.Margin = New System.Windows.Forms.Padding(1)
        Me.Xl_Screenshot1.Name = "Xl_Screenshot1"
        Me.Xl_Screenshot1.Size = New System.Drawing.Size(204, 88)
        Me.Xl_Screenshot1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Xl_Screenshot1.TabIndex = 63
        Me.Xl_Screenshot1.TabStop = False
        '
        'TextBoxAnswer
        '
        Me.TextBoxAnswer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxAnswer.Location = New System.Drawing.Point(13, 266)
        Me.TextBoxAnswer.Multiline = True
        Me.TextBoxAnswer.Name = "TextBoxAnswer"
        Me.TextBoxAnswer.Size = New System.Drawing.Size(613, 140)
        Me.TextBoxAnswer.TabIndex = 65
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 250)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(52, 13)
        Me.Label4.TabIndex = 64
        Me.Label4.Text = "Resposta"
        '
        'LabelFchValue
        '
        Me.LabelFchValue.AutoSize = True
        Me.LabelFchValue.Location = New System.Drawing.Point(70, 42)
        Me.LabelFchValue.Name = "LabelFchValue"
        Me.LabelFchValue.Size = New System.Drawing.Size(93, 13)
        Me.LabelFchValue.TabIndex = 66
        Me.LabelFchValue.Text = "dd/mm/yy HH:mm"
        '
        'Frm_MatsoftSupportCase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(626, 443)
        Me.Controls.Add(Me.LabelFchValue)
        Me.Controls.Add(Me.TextBoxAnswer)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_Screenshot1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ComboBoxProduct)
        Me.Controls.Add(Me.LabelFch)
        Me.Controls.Add(Me.TextBoxDsc)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Margin = New System.Windows.Forms.Padding(1)
        Me.Name = "Frm_MatsoftSupportCase"
        Me.Text = "Incidencia de Software"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Screenshot1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxDsc As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ButtonDel As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents LabelFch As Label
    Friend WithEvents ComboBoxProduct As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Xl_Screenshot1 As Xl_Screenshot
    Friend WithEvents TextBoxAnswer As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents LabelFchValue As Label
End Class
