<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ean_ECI
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
        Me.TextBoxDepto = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.LabelFamilia = New System.Windows.Forms.Label
        Me.TextBoxFamilia = New System.Windows.Forms.TextBox
        Me.LabelBarra = New System.Windows.Forms.Label
        Me.TextBoxBarra = New System.Windows.Forms.TextBox
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.ButtonCopy = New System.Windows.Forms.Button
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxDepto
        '
        Me.TextBoxDepto.Location = New System.Drawing.Point(103, 12)
        Me.TextBoxDepto.MaxLength = 3
        Me.TextBoxDepto.Name = "TextBoxDepto"
        Me.TextBoxDepto.Size = New System.Drawing.Size(27, 20)
        Me.TextBoxDepto.TabIndex = 1
        Me.TextBoxDepto.TabStop = False
        Me.TextBoxDepto.Text = "053"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Departamento"
        '
        'LabelFamilia
        '
        Me.LabelFamilia.AutoSize = True
        Me.LabelFamilia.Location = New System.Drawing.Point(14, 39)
        Me.LabelFamilia.Name = "LabelFamilia"
        Me.LabelFamilia.Size = New System.Drawing.Size(39, 13)
        Me.LabelFamilia.TabIndex = 2
        Me.LabelFamilia.Text = "Categoría"
        '
        'TextBoxFamilia
        '
        Me.TextBoxFamilia.Location = New System.Drawing.Point(103, 38)
        Me.TextBoxFamilia.MaxLength = 3
        Me.TextBoxFamilia.Name = "TextBoxFamilia"
        Me.TextBoxFamilia.Size = New System.Drawing.Size(27, 20)
        Me.TextBoxFamilia.TabIndex = 3
        Me.TextBoxFamilia.Text = "225"
        '
        'LabelBarra
        '
        Me.LabelBarra.AutoSize = True
        Me.LabelBarra.Location = New System.Drawing.Point(14, 65)
        Me.LabelBarra.Name = "LabelBarra"
        Me.LabelBarra.Size = New System.Drawing.Size(32, 13)
        Me.LabelBarra.TabIndex = 4
        Me.LabelBarra.Text = "Barra"
        '
        'TextBoxBarra
        '
        Me.TextBoxBarra.Location = New System.Drawing.Point(103, 64)
        Me.TextBoxBarra.MaxLength = 5
        Me.TextBoxBarra.Name = "TextBoxBarra"
        Me.TextBoxBarra.Size = New System.Drawing.Size(42, 20)
        Me.TextBoxBarra.TabIndex = 5
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(165, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(200, 60)
        Me.PictureBox1.TabIndex = 6
        Me.PictureBox1.TabStop = False
        '
        'ButtonCopy
        '
        Me.ButtonCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCopy.Image = My.Resources.Resources.Copy
        Me.ButtonCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCopy.Location = New System.Drawing.Point(303, 80)
        Me.ButtonCopy.Name = "ButtonCopy"
        Me.ButtonCopy.Size = New System.Drawing.Size(65, 21)
        Me.ButtonCopy.TabIndex = 7
        Me.ButtonCopy.Text = "Copiar"
        Me.ButtonCopy.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ButtonCopy.UseVisualStyleBackColor = True
        '
        'Frm_Ean_ECI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 113)
        Me.Controls.Add(Me.ButtonCopy)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LabelBarra)
        Me.Controls.Add(Me.TextBoxBarra)
        Me.Controls.Add(Me.LabelFamilia)
        Me.Controls.Add(Me.TextBoxFamilia)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxDepto)
        Me.Name = "Frm_Ean_ECI"
        Me.Text = "CODIS DE BARRES EL CORTE INGLES"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxDepto As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelFamilia As System.Windows.Forms.Label
    Friend WithEvents TextBoxFamilia As System.Windows.Forms.TextBox
    Friend WithEvents LabelBarra As System.Windows.Forms.Label
    Friend WithEvents TextBoxBarra As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents ButtonCopy As System.Windows.Forms.Button
End Class
