<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Contact2_Banc
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.CheckBoxModeCcaImpags = New System.Windows.Forms.CheckBox
        Me.PictureBoxCuadraDisposat = New System.Windows.Forms.PictureBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Xl_AmtCurDisposat = New Xl_AmountCur
        Me.TextBoxSufixe = New System.Windows.Forms.TextBox
        Me.TextBoxCedent = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Xl_IBAN1 = New Xl_Iban
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Xl_AmtCurClassificacio = New Xl_AmountCur
        CType(Me.PictureBoxCuadraDisposat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBoxModeCcaImpags
        '
        Me.CheckBoxModeCcaImpags.AutoSize = True
        Me.CheckBoxModeCcaImpags.Location = New System.Drawing.Point(35, 121)
        Me.CheckBoxModeCcaImpags.Name = "CheckBoxModeCcaImpags"
        Me.CheckBoxModeCcaImpags.Size = New System.Drawing.Size(217, 17)
        Me.CheckBoxModeCcaImpags.TabIndex = 14
        Me.CheckBoxModeCcaImpags.Text = "separar nominal de despeses a impagats"
        Me.CheckBoxModeCcaImpags.UseVisualStyleBackColor = True
        '
        'PictureBoxCuadraDisposat
        '
        Me.PictureBoxCuadraDisposat.Image = My.Resources.Resources.warn
        Me.PictureBoxCuadraDisposat.Location = New System.Drawing.Point(85, 91)
        Me.PictureBoxCuadraDisposat.Name = "PictureBoxCuadraDisposat"
        Me.PictureBoxCuadraDisposat.Size = New System.Drawing.Size(17, 18)
        Me.PictureBoxCuadraDisposat.TabIndex = 13
        Me.PictureBoxCuadraDisposat.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(32, 89)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Disposat:"
        '
        'Xl_AmtCurDisposat
        '
        Me.Xl_AmtCurDisposat.Amt = Nothing
        Me.Xl_AmtCurDisposat.Location = New System.Drawing.Point(110, 89)
        Me.Xl_AmtCurDisposat.Name = "Xl_AmtCurDisposat"
        Me.Xl_AmtCurDisposat.Size = New System.Drawing.Size(125, 20)
        Me.Xl_AmtCurDisposat.TabIndex = 11
        '
        'TextBoxSufixe
        '
        Me.TextBoxSufixe.Location = New System.Drawing.Point(211, 36)
        Me.TextBoxSufixe.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxSufixe.Name = "TextBoxSufixe"
        Me.TextBoxSufixe.Size = New System.Drawing.Size(25, 20)
        Me.TextBoxSufixe.TabIndex = 10
        '
        'TextBoxCedent
        '
        Me.TextBoxCedent.Location = New System.Drawing.Point(32, 36)
        Me.TextBoxCedent.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.TextBoxCedent.Name = "TextBoxCedent"
        Me.TextBoxCedent.Size = New System.Drawing.Size(172, 20)
        Me.TextBoxCedent.TabIndex = 6
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(205, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Sufixe:"
        '
        'Xl_IBAN1
        '
        Me.Xl_IBAN1.Location = New System.Drawing.Point(59, 262)
        Me.Xl_IBAN1.Name = "Xl_IBAN1"
        Me.Xl_IBAN1.Size = New System.Drawing.Size(250, 50)
        Me.Xl_IBAN1.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(44, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Cedent:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Classificació:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CheckBoxModeCcaImpags)
        Me.GroupBox1.Controls.Add(Me.PictureBoxCuadraDisposat)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtCurDisposat)
        Me.GroupBox1.Controls.Add(Me.TextBoxSufixe)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TextBoxCedent)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Xl_AmtCurClassificacio)
        Me.GroupBox1.Location = New System.Drawing.Point(59, 81)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(250, 153)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Norma 58"
        '
        'Xl_AmtCurClassificacio
        '
        Me.Xl_AmtCurClassificacio.Amt = Nothing
        Me.Xl_AmtCurClassificacio.Location = New System.Drawing.Point(110, 63)
        Me.Xl_AmtCurClassificacio.Name = "Xl_AmtCurClassificacio"
        Me.Xl_AmtCurClassificacio.Size = New System.Drawing.Size(125, 20)
        Me.Xl_AmtCurClassificacio.TabIndex = 3
        '
        'Xl_Contact2_Banc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Xl_IBAN1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Xl_Contact2_Banc"
        Me.Size = New System.Drawing.Size(368, 414)
        CType(Me.PictureBoxCuadraDisposat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CheckBoxModeCcaImpags As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBoxCuadraDisposat As System.Windows.Forms.PictureBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_AmtCurDisposat As Xl_AmountCur
    Friend WithEvents TextBoxSufixe As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxCedent As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Xl_IBAN1 As Xl_Iban
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Xl_AmtCurClassificacio As Xl_AmountCur

End Class
