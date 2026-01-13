<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PncRepCom
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
        Me.ComboBoxRep = New System.Windows.Forms.ComboBox
        Me.TextBoxCom = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TextBoxCliNom = New System.Windows.Forms.TextBox
        Me.PictureBoxArt = New System.Windows.Forms.PictureBox
        Me.TextBoxArtNom = New System.Windows.Forms.TextBox
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.PictureBoxTpa = New System.Windows.Forms.PictureBox
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxTpa, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ComboBoxRep
        '
        Me.ComboBoxRep.FormattingEnabled = True
        Me.ComboBoxRep.Location = New System.Drawing.Point(3, 156)
        Me.ComboBoxRep.Name = "ComboBoxRep"
        Me.ComboBoxRep.Size = New System.Drawing.Size(253, 21)
        Me.ComboBoxRep.TabIndex = 14
        '
        'TextBoxCom
        '
        Me.TextBoxCom.Location = New System.Drawing.Point(287, 157)
        Me.TextBoxCom.Name = "TextBoxCom"
        Me.TextBoxCom.Size = New System.Drawing.Size(38, 20)
        Me.TextBoxCom.TabIndex = 15
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 140)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Rep"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(284, 141)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Comisió:"
        '
        'TextBoxCliNom
        '
        Me.TextBoxCliNom.Location = New System.Drawing.Point(3, 2)
        Me.TextBoxCliNom.Name = "TextBoxCliNom"
        Me.TextBoxCliNom.ReadOnly = True
        Me.TextBoxCliNom.Size = New System.Drawing.Size(329, 20)
        Me.TextBoxCliNom.TabIndex = 18
        '
        'PictureBoxArt
        '
        Me.PictureBoxArt.Location = New System.Drawing.Point(262, 28)
        Me.PictureBoxArt.Name = "PictureBoxArt"
        Me.PictureBoxArt.Size = New System.Drawing.Size(70, 80)
        Me.PictureBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxArt.TabIndex = 19
        Me.PictureBoxArt.TabStop = False
        '
        'TextBoxArtNom
        '
        Me.TextBoxArtNom.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxArtNom.Location = New System.Drawing.Point(2, 88)
        Me.TextBoxArtNom.Name = "TextBoxArtNom"
        Me.TextBoxArtNom.ReadOnly = True
        Me.TextBoxArtNom.Size = New System.Drawing.Size(253, 20)
        Me.TextBoxArtNom.TabIndex = 22
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(118, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 195)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(337, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(229, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'PictureBoxTpa
        '
        Me.PictureBoxTpa.Location = New System.Drawing.Point(3, 28)
        Me.PictureBoxTpa.Name = "PictureBoxTpa"
        Me.PictureBoxTpa.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxTpa.TabIndex = 42
        Me.PictureBoxTpa.TabStop = False
        '
        'Frm_PncRepCom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 226)
        Me.Controls.Add(Me.PictureBoxTpa)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxArtNom)
        Me.Controls.Add(Me.PictureBoxArt)
        Me.Controls.Add(Me.TextBoxCliNom)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxCom)
        Me.Controls.Add(Me.ComboBoxRep)
        Me.Name = "Frm_PncRepCom"
        Me.Text = "LINIA DE COMANDA"
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxTpa, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboBoxRep As System.Windows.Forms.ComboBox
    Friend WithEvents TextBoxCom As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCliNom As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxArt As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxArtNom As System.Windows.Forms.TextBox
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents PictureBoxTpa As System.Windows.Forms.PictureBox
End Class
