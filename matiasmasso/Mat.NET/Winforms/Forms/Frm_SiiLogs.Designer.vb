<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_SiiLogs
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_SiiLogs1 = New Winforms.Xl_SiiLogs()
        Me.Xl_YearMonth1 = New Winforms.Xl_YearMonth()
        CType(Me.Xl_SiiLogs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(484, 16)
        Me.Xl_TextboxSearch1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(400, 48)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Xl_SiiLogs1
        '
        Me.Xl_SiiLogs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_SiiLogs1.DisplayObsolets = False
        Me.Xl_SiiLogs1.Filter = Nothing
        Me.Xl_SiiLogs1.Location = New System.Drawing.Point(13, 75)
        Me.Xl_SiiLogs1.MouseIsDown = False
        Me.Xl_SiiLogs1.Name = "Xl_SiiLogs1"
        Me.Xl_SiiLogs1.RowTemplate.Height = 40
        Me.Xl_SiiLogs1.Size = New System.Drawing.Size(1063, 535)
        Me.Xl_SiiLogs1.TabIndex = 1
        '
        'Xl_YearMonth1
        '
        Me.Xl_YearMonth1.Location = New System.Drawing.Point(17, 16)
        Me.Xl_YearMonth1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_YearMonth1.Name = "Xl_YearMonth1"
        Me.Xl_YearMonth1.Size = New System.Drawing.Size(400, 45)
        Me.Xl_YearMonth1.TabIndex = 2
        '
        'Frm_SiiLogs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(16.0!, 31.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1088, 622)
        Me.Controls.Add(Me.Xl_YearMonth1)
        Me.Controls.Add(Me.Xl_SiiLogs1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Name = "Frm_SiiLogs"
        Me.Text = "SII (Subministre Immediat de Informació a Hisenda)"
        CType(Me.Xl_SiiLogs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_SiiLogs1 As Xl_SiiLogs
    Friend WithEvents Xl_YearMonth1 As Xl_YearMonth
End Class
