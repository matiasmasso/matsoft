<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_EdiExceptions
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
        Me.Xl_EdiExceptions1 = New Mat.Net.Xl_EdiExceptions()
        Me.Xl_TextboxSearch1 = New Mat.Net.Xl_TextboxSearch()
        CType(Me.Xl_EdiExceptions1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_EdiExceptions1
        '
        Me.Xl_EdiExceptions1.AllowUserToAddRows = False
        Me.Xl_EdiExceptions1.AllowUserToDeleteRows = False
        Me.Xl_EdiExceptions1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_EdiExceptions1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_EdiExceptions1.DisplayObsolets = False
        Me.Xl_EdiExceptions1.Location = New System.Drawing.Point(1, 46)
        Me.Xl_EdiExceptions1.MouseIsDown = False
        Me.Xl_EdiExceptions1.Name = "Xl_EdiExceptions1"
        Me.Xl_EdiExceptions1.ReadOnly = True
        Me.Xl_EdiExceptions1.Size = New System.Drawing.Size(592, 204)
        Me.Xl_EdiExceptions1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(443, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'Frm_EdiExceptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 251)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_EdiExceptions1)
        Me.Name = "Frm_EdiExceptions"
        Me.Text = "Incidencies Edi per resoldre"
        CType(Me.Xl_EdiExceptions1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_EdiExceptions1 As Xl_EdiExceptions
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
End Class
