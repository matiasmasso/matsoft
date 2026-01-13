Public Partial Class Frm_Csa_NewAndorra
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.Xl_Gir_SelEfts1 = New Xl_Gir_SelEfts
        Me.ButtonOk = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Xl_Gir_SelEfts1
        '
        Me.Xl_Gir_SelEfts1.Location = New System.Drawing.Point(13, 13)
        Me.Xl_Gir_SelEfts1.Name = "Xl_Gir_SelEfts1"
        Me.Xl_Gir_SelEfts1.Size = New System.Drawing.Size(616, 218)
        Me.Xl_Gir_SelEfts1.TabIndex = 0
        '
        'ButtonOk
        '
        Me.ButtonOk.Location = New System.Drawing.Point(529, 238)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(100, 26)
        Me.ButtonOk.TabIndex = 1
        Me.ButtonOk.Text = "ACCEPTAR"
        '
        'Frm_Csa_NewAndorra
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(641, 266)
        Me.Controls.Add(Me.ButtonOk)
        Me.Controls.Add(Me.Xl_Gir_SelEfts1)
        Me.Name = "Frm_Csa_NewAndorra"
        Me.Text = "Frm_Csa_NewAndorra"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_Gir_SelEfts1 As Xl_Gir_SelEfts
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
End Class
