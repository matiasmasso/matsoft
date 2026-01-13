<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_ExerciciApertura
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
        Me.ButtonApertura = New System.Windows.Forms.Button()
        Me.ButtonTancament = New System.Windows.Forms.Button()
        Me.ButtonTancamentUndo = New System.Windows.Forms.Button()
        Me.ButtonCcaRenum = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_ProgressBar1 = New Winforms.Xl_ProgressBar()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ButtonCcaRenum2 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonApertura
        '
        Me.ButtonApertura.Location = New System.Drawing.Point(12, 57)
        Me.ButtonApertura.Name = "ButtonApertura"
        Me.ButtonApertura.Size = New System.Drawing.Size(190, 23)
        Me.ButtonApertura.TabIndex = 2
        Me.ButtonApertura.Text = "Apertura exercici actual"
        Me.ButtonApertura.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonApertura.UseVisualStyleBackColor = True
        '
        'ButtonTancament
        '
        Me.ButtonTancament.Location = New System.Drawing.Point(12, 86)
        Me.ButtonTancament.Name = "ButtonTancament"
        Me.ButtonTancament.Size = New System.Drawing.Size(190, 23)
        Me.ButtonTancament.TabIndex = 3
        Me.ButtonTancament.Text = "Tancament exercici anterior"
        Me.ButtonTancament.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonTancament.UseVisualStyleBackColor = True
        '
        'ButtonTancamentUndo
        '
        Me.ButtonTancamentUndo.Location = New System.Drawing.Point(12, 115)
        Me.ButtonTancamentUndo.Name = "ButtonTancamentUndo"
        Me.ButtonTancamentUndo.Size = New System.Drawing.Size(190, 23)
        Me.ButtonTancamentUndo.TabIndex = 4
        Me.ButtonTancamentUndo.Text = "Retrocedir tancament"
        Me.ButtonTancamentUndo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonTancamentUndo.UseVisualStyleBackColor = True
        '
        'ButtonCcaRenum
        '
        Me.ButtonCcaRenum.Location = New System.Drawing.Point(12, 163)
        Me.ButtonCcaRenum.Name = "ButtonCcaRenum"
        Me.ButtonCcaRenum.Size = New System.Drawing.Size(190, 23)
        Me.ButtonCcaRenum.TabIndex = 5
        Me.ButtonCcaRenum.Text = "Renumera assentaments"
        Me.ButtonCcaRenum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCcaRenum.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(252, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(43, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "exercici"
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(0, 244)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(362, 30)
        Me.Xl_ProgressBar1.TabIndex = 0
        Me.Xl_ProgressBar1.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 221)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(362, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 7
        Me.ProgressBar1.Visible = False
        '
        'ButtonCcaRenum2
        '
        Me.ButtonCcaRenum2.Location = New System.Drawing.Point(12, 192)
        Me.ButtonCcaRenum2.Name = "ButtonCcaRenum2"
        Me.ButtonCcaRenum2.Size = New System.Drawing.Size(190, 23)
        Me.ButtonCcaRenum2.TabIndex = 8
        Me.ButtonCcaRenum2.Text = "Renumera assentaments"
        Me.ButtonCcaRenum2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ButtonCcaRenum2.UseVisualStyleBackColor = True
        '
        'Frm_ExerciciApertura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 274)
        Me.Controls.Add(Me.ButtonCcaRenum2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ButtonCcaRenum)
        Me.Controls.Add(Me.ButtonTancamentUndo)
        Me.Controls.Add(Me.ButtonTancament)
        Me.Controls.Add(Me.ButtonApertura)
        Me.Controls.Add(Me.Xl_ProgressBar1)
        Me.Name = "Frm_ExerciciApertura"
        Me.Text = "Apertura i tancament d'exercici"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
    Friend WithEvents ButtonApertura As Button
    Friend WithEvents ButtonTancament As Button
    Friend WithEvents ButtonTancamentUndo As Button
    Friend WithEvents ButtonCcaRenum As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ButtonCcaRenum2 As Button
End Class
