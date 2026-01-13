<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CcaDescuadres
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
        Me.Label_exercici = New System.Windows.Forms.Label()
        Me.Xl_Years1 = New Mat.Net.Xl_Years()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Xl_Ccas1 = New Mat.Net.Xl_Ccas()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Panel1.SuspendLayout()
        CType(Me.Xl_Ccas1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label_exercici
        '
        Me.Label_exercici.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label_exercici.AutoSize = True
        Me.Label_exercici.Location = New System.Drawing.Point(585, 7)
        Me.Label_exercici.Name = "Label_exercici"
        Me.Label_exercici.Size = New System.Drawing.Size(44, 13)
        Me.Label_exercici.TabIndex = 1
        Me.Label_exercici.Text = "Exercici"
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Years1.Location = New System.Drawing.Point(635, 3)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 0
        Me.Xl_Years1.Value = 0
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_Ccas1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(1, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(796, 417)
        Me.Panel1.TabIndex = 2
        '
        'Xl_Ccas1
        '
        Me.Xl_Ccas1.AllowUserToAddRows = False
        Me.Xl_Ccas1.AllowUserToDeleteRows = False
        Me.Xl_Ccas1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_Ccas1.DisplayObsolets = False
        Me.Xl_Ccas1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Ccas1.Filter = Nothing
        Me.Xl_Ccas1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Ccas1.MouseIsDown = False
        Me.Xl_Ccas1.Name = "Xl_Ccas1"
        Me.Xl_Ccas1.ReadOnly = True
        Me.Xl_Ccas1.Size = New System.Drawing.Size(796, 394)
        Me.Xl_Ccas1.TabIndex = 1
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 394)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(796, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_CcaDescuadres
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label_exercici)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Name = "Frm_CcaDescuadres"
        Me.Text = "Relació de assentaments que no cuadren el debe amb l'haver"
        Me.Panel1.ResumeLayout(False)
        CType(Me.Xl_Ccas1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Label_exercici As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Xl_Ccas1 As Xl_Ccas
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
