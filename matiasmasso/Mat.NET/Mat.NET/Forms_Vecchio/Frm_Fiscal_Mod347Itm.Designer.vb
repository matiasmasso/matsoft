<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Fiscal_Mod347Itm
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
        Me.PictureBoxLogo = New System.Windows.Forms.PictureBox
        Me.ComboBoxOp = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Xl_Contact1 = New Xl_Contact
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Xl_NIF1 = New Xl_NIF
        Me.Label4 = New System.Windows.Forms.Label
        Me.TextBoxNom = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Xl_Provincia1 = New Xl_Provincia
        Me.Label6 = New System.Windows.Forms.Label
        Me.Xl_AmtCur1 = New Xl_AmountCur
        Me.TextBoxObs = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ButtonCancel = New System.Windows.Forms.Button
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.ButtonDel = New System.Windows.Forms.Button
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBoxLogo
        '
        Me.PictureBoxLogo.Location = New System.Drawing.Point(303, 3)
        Me.PictureBoxLogo.Name = "PictureBoxLogo"
        Me.PictureBoxLogo.Size = New System.Drawing.Size(150, 48)
        Me.PictureBoxLogo.TabIndex = 0
        Me.PictureBoxLogo.TabStop = False
        '
        'ComboBoxOp
        '
        Me.ComboBoxOp.FormattingEnabled = True
        Me.ComboBoxOp.Location = New System.Drawing.Point(115, 57)
        Me.ComboBoxOp.Name = "ComboBoxOp"
        Me.ComboBoxOp.Size = New System.Drawing.Size(127, 21)
        Me.ComboBoxOp.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Codi operació"
        '
        'Xl_Contact1
        '
        Me.Xl_Contact1.Location = New System.Drawing.Point(115, 84)
        Me.Xl_Contact1.Name = "Xl_Contact1"
        Me.Xl_Contact1.Size = New System.Drawing.Size(344, 20)
        Me.Xl_Contact1.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(50, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Declarat:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Nom:"
        '
        'Xl_NIF1
        '
        Me.Xl_NIF1.Location = New System.Drawing.Point(115, 137)
        Me.Xl_NIF1.Name = "Xl_NIF1"
        Me.Xl_NIF1.Size = New System.Drawing.Size(144, 20)
        Me.Xl_NIF1.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 144)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "NIF:"
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Location = New System.Drawing.Point(115, 111)
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(344, 20)
        Me.TextBoxNom.TabIndex = 8
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 168)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Provincia:"
        '
        'Xl_Provincia1
        '
        Me.Xl_Provincia1.Location = New System.Drawing.Point(115, 164)
        Me.Xl_Provincia1.Name = "Xl_Provincia1"
        Me.Xl_Provincia1.Provincia = Nothing
        Me.Xl_Provincia1.Size = New System.Drawing.Size(214, 20)
        Me.Xl_Provincia1.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(39, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Import:"
        '
        'Xl_AmtCur1
        '
        Me.Xl_AmtCur1.Amt = Nothing
        Me.Xl_AmtCur1.Location = New System.Drawing.Point(115, 191)
        Me.Xl_AmtCur1.Name = "Xl_AmtCur1"
        Me.Xl_AmtCur1.Size = New System.Drawing.Size(157, 20)
        Me.Xl_AmtCur1.TabIndex = 12
        '
        'TextBoxObs
        '
        Me.TextBoxObs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxObs.Location = New System.Drawing.Point(16, 278)
        Me.TextBoxObs.Multiline = True
        Me.TextBoxObs.Name = "TextBoxObs"
        Me.TextBoxObs.ReadOnly = True
        Me.TextBoxObs.Size = New System.Drawing.Size(428, 144)
        Me.TextBoxObs.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(13, 262)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Observacions del client:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Controls.Add(Me.ButtonDel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 428)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(456, 31)
        Me.Panel1.TabIndex = 44
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(237, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(348, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
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
        Me.ButtonDel.TabIndex = 14
        Me.ButtonDel.Text = "ELIMINAR"
        Me.ButtonDel.UseVisualStyleBackColor = False
        '
        'Frm_Fiscal_Mod347Itm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 459)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBoxObs)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Xl_AmtCur1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_Provincia1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Xl_NIF1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Xl_Contact1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxOp)
        Me.Controls.Add(Me.PictureBoxLogo)
        Me.Name = "Frm_Fiscal_Mod347Itm"
        Me.Text = "MODEL 347"
        CType(Me.PictureBoxLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBoxLogo As System.Windows.Forms.PictureBox
    Friend WithEvents ComboBoxOp As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_Contact1 As Xl_Contact
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_NIF1 As Xl_NIF
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_Provincia1 As Xl_Provincia
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCur1 As Xl_AmountCur
    Friend WithEvents TextBoxObs As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents ButtonDel As System.Windows.Forms.Button
End Class
