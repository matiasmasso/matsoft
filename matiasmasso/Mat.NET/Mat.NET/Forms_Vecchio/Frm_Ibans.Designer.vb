<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ibans
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
        Me.Xl_Ibans1 = New Mat.NET.Xl_Ibans()
        Me.ComboBoxFormat = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelCount = New System.Windows.Forms.Label()
        Me.ComboBoxStatus = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Xl_Ibans1
        '
        Me.Xl_Ibans1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Ibans1.Location = New System.Drawing.Point(-1, 39)
        Me.Xl_Ibans1.Name = "Xl_Ibans1"
        Me.Xl_Ibans1.Size = New System.Drawing.Size(890, 378)
        Me.Xl_Ibans1.TabIndex = 0
        '
        'ComboBoxFormat
        '
        Me.ComboBoxFormat.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFormat.FormattingEnabled = True
        Me.ComboBoxFormat.Location = New System.Drawing.Point(768, 12)
        Me.ComboBoxFormat.Name = "ComboBoxFormat"
        Me.ComboBoxFormat.Size = New System.Drawing.Size(121, 21)
        Me.ComboBoxFormat.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(726, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "format"
        '
        'LabelCount
        '
        Me.LabelCount.AutoSize = True
        Me.LabelCount.Location = New System.Drawing.Point(505, 20)
        Me.LabelCount.Name = "LabelCount"
        Me.LabelCount.Size = New System.Drawing.Size(0, 13)
        Me.LabelCount.TabIndex = 7
        '
        'ComboBoxStatus
        '
        Me.ComboBoxStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxStatus.FormattingEnabled = True
        Me.ComboBoxStatus.Items.AddRange(New Object() {"(tots)", "pendents de descarregar", "pendents de signar", "pendents de validar", "descarregats", "signats", "validats", "denegats"})
        Me.ComboBoxStatus.Location = New System.Drawing.Point(12, 12)
        Me.ComboBoxStatus.Name = "ComboBoxStatus"
        Me.ComboBoxStatus.Size = New System.Drawing.Size(263, 21)
        Me.ComboBoxStatus.TabIndex = 9
        '
        'Frm_Ibans
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(890, 417)
        Me.Controls.Add(Me.ComboBoxStatus)
        Me.Controls.Add(Me.LabelCount)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxFormat)
        Me.Controls.Add(Me.Xl_Ibans1)
        Me.Name = "Frm_Ibans"
        Me.Text = "Domiciliacions bancàries"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_Ibans1 As Mat.NET.Xl_Ibans
    Friend WithEvents ComboBoxFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelCount As System.Windows.Forms.Label
    Friend WithEvents ComboBoxStatus As System.Windows.Forms.ComboBox
End Class
