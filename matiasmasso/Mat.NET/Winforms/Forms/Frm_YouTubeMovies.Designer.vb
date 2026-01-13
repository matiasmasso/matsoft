<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_YouTubeMovies
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
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.Xl_YouTubeMovies1 = New Winforms.Xl_YouTubeMovies()
        CType(Me.Xl_YouTubeMovies1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(373, 12)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(150, 20)
        Me.Xl_TextboxSearch1.TabIndex = 0
        '
        'Xl_YouTubeMovies1
        '
        Me.Xl_YouTubeMovies1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_YouTubeMovies1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_YouTubeMovies1.DisplayObsolets = False
        Me.Xl_YouTubeMovies1.Filter = Nothing
        Me.Xl_YouTubeMovies1.Location = New System.Drawing.Point(1, 38)
        Me.Xl_YouTubeMovies1.MouseIsDown = False
        Me.Xl_YouTubeMovies1.Name = "Xl_YouTubeMovies1"
        Me.Xl_YouTubeMovies1.Size = New System.Drawing.Size(522, 315)
        Me.Xl_YouTubeMovies1.TabIndex = 1
        '
        'Frm_YouTubeMovies
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 356)
        Me.Controls.Add(Me.Xl_YouTubeMovies1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Name = "Frm_YouTubeMovies"
        Me.Text = "Videos YouTube"
        CType(Me.Xl_YouTubeMovies1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Xl_YouTubeMovies1 As Xl_YouTubeMovies
End Class
