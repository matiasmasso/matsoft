

Public Class Frm_Bancs_Old
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents Xl_Bancs_Select1 As Xl_Bancs_Select
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Xl_Bancs_Select1 = New Xl_Bancs_Select
        Me.SuspendLayout()
        '
        'Xl_Bancs_Select1
        '
        Me.Xl_Bancs_Select1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Bancs_Select1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Bancs_Select1.Name = "Xl_Bancs_Select1"
        Me.Xl_Bancs_Select1.Size = New System.Drawing.Size(292, 273)
        Me.Xl_Bancs_Select1.TabIndex = 0
        '
        'Frm_Bancs
        '
        Me.AutoScaleDimensions = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(292, 273)
        Me.Controls.Add(Me.Xl_Bancs_Select1)
        Me.Name = "Frm_Bancs"
        Me.Text = "BANCS"
        Me.ResumeLayout(False)

    End Sub

#End Region


End Class
