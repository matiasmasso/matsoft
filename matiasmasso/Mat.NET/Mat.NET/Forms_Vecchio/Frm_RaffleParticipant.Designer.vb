<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_RaffleParticipant
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TextBoxFch = New System.Windows.Forms.TextBox()
        Me.TextBoxRaffle = New System.Windows.Forms.TextBox()
        Me.TextBoxUsuari = New System.Windows.Forms.TextBox()
        Me.ButtonRaffle = New System.Windows.Forms.Button()
        Me.ButtonUsuari = New System.Windows.Forms.Button()
        Me.Xl_Contact2Distributor = New Mat.NET.Xl_Contact2()
        Me.PictureBoxRightAnswer = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.ButtonDel = New System.Windows.Forms.Button()
        Me.ComboBoxAnswer = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBoxRightAnswer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "Data:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "Sorteig:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Usuari:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 100)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 13)
        Me.Label8.TabIndex = 58
        Me.Label8.Text = "Resposta"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 126)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 13)
        Me.Label10.TabIndex = 60
        Me.Label10.Text = "Distribuidor"
        '
        'TextBoxFch
        '
        Me.TextBoxFch.Enabled = False
        Me.TextBoxFch.Location = New System.Drawing.Point(81, 19)
        Me.TextBoxFch.Name = "TextBoxFch"
        Me.TextBoxFch.Size = New System.Drawing.Size(162, 20)
        Me.TextBoxFch.TabIndex = 61
        '
        'TextBoxRaffle
        '
        Me.TextBoxRaffle.Enabled = False
        Me.TextBoxRaffle.Location = New System.Drawing.Point(81, 45)
        Me.TextBoxRaffle.Name = "TextBoxRaffle"
        Me.TextBoxRaffle.Size = New System.Drawing.Size(347, 20)
        Me.TextBoxRaffle.TabIndex = 62
        '
        'TextBoxUsuari
        '
        Me.TextBoxUsuari.Enabled = False
        Me.TextBoxUsuari.Location = New System.Drawing.Point(81, 71)
        Me.TextBoxUsuari.Name = "TextBoxUsuari"
        Me.TextBoxUsuari.Size = New System.Drawing.Size(347, 20)
        Me.TextBoxUsuari.TabIndex = 63
        '
        'ButtonRaffle
        '
        Me.ButtonRaffle.Location = New System.Drawing.Point(434, 45)
        Me.ButtonRaffle.Name = "ButtonRaffle"
        Me.ButtonRaffle.Size = New System.Drawing.Size(36, 20)
        Me.ButtonRaffle.TabIndex = 66
        Me.ButtonRaffle.Text = "..."
        Me.ButtonRaffle.UseVisualStyleBackColor = True
        '
        'ButtonUsuari
        '
        Me.ButtonUsuari.Location = New System.Drawing.Point(434, 71)
        Me.ButtonUsuari.Name = "ButtonUsuari"
        Me.ButtonUsuari.Size = New System.Drawing.Size(36, 20)
        Me.ButtonUsuari.TabIndex = 67
        Me.ButtonUsuari.Text = "..."
        Me.ButtonUsuari.UseVisualStyleBackColor = True
        '
        'Xl_Contact2Distributor
        '
        Me.Xl_Contact2Distributor.Contact = Nothing
        Me.Xl_Contact2Distributor.Location = New System.Drawing.Point(81, 124)
        Me.Xl_Contact2Distributor.Name = "Xl_Contact2Distributor"
        Me.Xl_Contact2Distributor.ReadOnly = False
        Me.Xl_Contact2Distributor.Size = New System.Drawing.Size(347, 20)
        Me.Xl_Contact2Distributor.TabIndex = 68
        '
        'PictureBoxRightAnswer
        '
        Me.PictureBoxRightAnswer.Location = New System.Drawing.Point(434, 99)
        Me.PictureBoxRightAnswer.Name = "PictureBoxRightAnswer"
        Me.PictureBoxRightAnswer.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxRightAnswer.TabIndex = 69
        Me.PictureBoxRightAnswer.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 167)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(475, 31)
        Me.Panel1.TabIndex = 70
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(256, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(367, 4)
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
        Me.ButtonDel.Location = New System.Drawing.Point(6, 4)
        Me.ButtonDel.Name = "ButtonDel"
        Me.ButtonDel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "Eliminar"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'ComboBoxAnswer
        '
        Me.ComboBoxAnswer.FormattingEnabled = True
        Me.ComboBoxAnswer.Location = New System.Drawing.Point(81, 97)
        Me.ComboBoxAnswer.Name = "ComboBoxAnswer"
        Me.ComboBoxAnswer.Size = New System.Drawing.Size(347, 21)
        Me.ComboBoxAnswer.TabIndex = 71
        '
        'Frm_RaffleParticipant
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(475, 198)
        Me.Controls.Add(Me.ComboBoxAnswer)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBoxRightAnswer)
        Me.Controls.Add(Me.Xl_Contact2Distributor)
        Me.Controls.Add(Me.ButtonUsuari)
        Me.Controls.Add(Me.ButtonRaffle)
        Me.Controls.Add(Me.TextBoxUsuari)
        Me.Controls.Add(Me.TextBoxRaffle)
        Me.Controls.Add(Me.TextBoxFch)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Frm_RaffleParticipant"
        Me.Text = "Participant en Sorteig"
        CType(Me.PictureBoxRightAnswer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBoxFch As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxRaffle As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUsuari As System.Windows.Forms.TextBox
    Friend WithEvents ButtonRaffle As System.Windows.Forms.Button
    Friend WithEvents ButtonUsuari As System.Windows.Forms.Button
    Friend WithEvents Xl_Contact2Distributor As Mat.NET.Xl_Contact2
    Friend WithEvents PictureBoxRightAnswer As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
    Friend WithEvents ComboBoxAnswer As System.Windows.Forms.ComboBox
End Class
