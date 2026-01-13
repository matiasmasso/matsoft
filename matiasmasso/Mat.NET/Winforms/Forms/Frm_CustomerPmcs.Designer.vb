<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CustomerPmcs
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
        Me.Xl_CustomerPmcs1 = New Winforms.Xl_CustomerPmcs()
        Me.Xl_Contact21 = New Winforms.Xl_Contact2()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.Xl_CustomerPmcs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_CustomerPmcs1
        '
        Me.Xl_CustomerPmcs1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_CustomerPmcs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CustomerPmcs1.DisplayObsolets = False
        Me.Xl_CustomerPmcs1.Location = New System.Drawing.Point(3, 38)
        Me.Xl_CustomerPmcs1.Name = "Xl_CustomerPmcs1"
        Me.Xl_CustomerPmcs1.Size = New System.Drawing.Size(748, 221)
        Me.Xl_CustomerPmcs1.TabIndex = 0
        '
        'Xl_Contact21
        '
        Me.Xl_Contact21.Contact = Nothing
        Me.Xl_Contact21.Location = New System.Drawing.Point(42, 12)
        Me.Xl_Contact21.Name = "Xl_Contact21"
        Me.Xl_Contact21.ReadOnly = False
        Me.Xl_Contact21.Size = New System.Drawing.Size(357, 20)
        Me.Xl_Contact21.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(33, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Client"
        '
        'Frm_CustomerPmcs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 261)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Xl_Contact21)
        Me.Controls.Add(Me.Xl_CustomerPmcs1)
        Me.Name = "Frm_CustomerPmcs"
        Me.Text = "Marges comercials per compte client"
        CType(Me.Xl_CustomerPmcs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Xl_CustomerPmcs1 As Xl_CustomerPmcs
    Friend WithEvents Xl_Contact21 As Xl_Contact2
    Friend WithEvents Label1 As Label
End Class
