<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_AlbTrps
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.TextBoxM3 = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.ComboBoxZonas = New System.Windows.Forms.ComboBox
        Me.Xl_Pais1 = New Xl_Pais
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBoxM3
        '
        Me.TextBoxM3.Location = New System.Drawing.Point(76, 36)
        Me.TextBoxM3.Name = "TextBoxM3"
        Me.TextBoxM3.Size = New System.Drawing.Size(56, 20)
        Me.TextBoxM3.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(4, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Volumen m3:"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(4, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 16)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Zona:"
        '
        'ComboBoxZonas
        '
        Me.ComboBoxZonas.FormattingEnabled = True
        Me.ComboBoxZonas.Location = New System.Drawing.Point(108, 12)
        Me.ComboBoxZonas.Name = "ComboBoxZonas"
        Me.ComboBoxZonas.Size = New System.Drawing.Size(320, 21)
        Me.ComboBoxZonas.TabIndex = 6
        '
        'Xl_Pais1
        '
        Me.Xl_Pais1.Location = New System.Drawing.Point(42, 15)
        Me.Xl_Pais1.Name = "Xl_Pais1"
        Me.Xl_Pais1.Country = Nothing
        Me.Xl_Pais1.Size = New System.Drawing.Size(60, 15)
        Me.Xl_Pais1.TabIndex = 5
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 62)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(438, 203)
        Me.DataGridView1.TabIndex = 10
        '
        'Frm_AlbTrps
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 266)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.TextBoxM3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxZonas)
        Me.Controls.Add(Me.Xl_Pais1)
        Me.Name = "Frm_AlbTrps"
        Me.Text = "COMPARATIVA TRANSPORTISTES"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBoxM3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxZonas As System.Windows.Forms.ComboBox
    Friend WithEvents Xl_Pais1 As Xl_Pais
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
