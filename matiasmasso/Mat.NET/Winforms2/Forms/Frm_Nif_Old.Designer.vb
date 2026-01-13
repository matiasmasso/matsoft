<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Nif_Old
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
        Me.ComboBoxCountry = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelNif1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.LabelNif2 = New System.Windows.Forms.Label()
        Me.PictureBoxNif1 = New System.Windows.Forms.PictureBox()
        Me.PictureBoxNif2 = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBoxNif1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxNif2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 178)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(325, 31)
        Me.Panel1.TabIndex = 42
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(106, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(217, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "Acceptar"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'ComboBoxCountry
        '
        Me.ComboBoxCountry.FormattingEnabled = True
        Me.ComboBoxCountry.Location = New System.Drawing.Point(125, 43)
        Me.ComboBoxCountry.Name = "ComboBoxCountry"
        Me.ComboBoxCountry.Size = New System.Drawing.Size(157, 21)
        Me.ComboBoxCountry.TabIndex = 43
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Pais"
        '
        'LabelNif1
        '
        Me.LabelNif1.AutoSize = True
        Me.LabelNif1.Location = New System.Drawing.Point(31, 93)
        Me.LabelNif1.Name = "LabelNif1"
        Me.LabelNif1.Size = New System.Drawing.Size(26, 13)
        Me.LabelNif1.TabIndex = 45
        Me.LabelNif1.Text = "Nif1"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(125, 90)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(157, 20)
        Me.TextBox1.TabIndex = 46
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(125, 116)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(157, 20)
        Me.TextBox2.TabIndex = 48
        '
        'LabelNif2
        '
        Me.LabelNif2.AutoSize = True
        Me.LabelNif2.Location = New System.Drawing.Point(31, 119)
        Me.LabelNif2.Name = "LabelNif2"
        Me.LabelNif2.Size = New System.Drawing.Size(26, 13)
        Me.LabelNif2.TabIndex = 47
        Me.LabelNif2.Text = "Nif2"
        '
        'PictureBoxNif1
        '
        Me.PictureBoxNif1.Location = New System.Drawing.Point(289, 93)
        Me.PictureBoxNif1.Name = "PictureBoxNif1"
        Me.PictureBoxNif1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxNif1.TabIndex = 49
        Me.PictureBoxNif1.TabStop = False
        '
        'PictureBoxNif2
        '
        Me.PictureBoxNif2.Location = New System.Drawing.Point(289, 119)
        Me.PictureBoxNif2.Name = "PictureBoxNif2"
        Me.PictureBoxNif2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxNif2.TabIndex = 50
        Me.PictureBoxNif2.TabStop = False
        '
        'Frm_Nif
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(325, 209)
        Me.Controls.Add(Me.PictureBoxNif2)
        Me.Controls.Add(Me.PictureBoxNif1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.LabelNif2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.LabelNif1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxCountry)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_Nif"
        Me.Text = "Numeros de Identificació Fiscal"
        Me.Panel1.ResumeLayout(False)
        CType(Me.PictureBoxNif1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxNif2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents ButtonCancel As Button
    Friend WithEvents ButtonOk As Button
    Friend WithEvents ComboBoxCountry As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents LabelNif1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents LabelNif2 As Label
    Friend WithEvents PictureBoxNif1 As PictureBox
    Friend WithEvents PictureBoxNif2 As PictureBox
End Class
