<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_IncidenciesCods
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
        Me.Xl_IncidenciesCods1 = New Winforms.Xl_IncidenciesCods()
        CType(Me.Xl_IncidenciesCods1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_IncidenciesCods1
        '
        Me.Xl_IncidenciesCods1.AllowUserToAddRows = False
        Me.Xl_IncidenciesCods1.AllowUserToDeleteRows = False
        Me.Xl_IncidenciesCods1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_IncidenciesCods1.DisplayObsolets = False
        Me.Xl_IncidenciesCods1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_IncidenciesCods1.Filter = Nothing
        Me.Xl_IncidenciesCods1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_IncidenciesCods1.MouseIsDown = False
        Me.Xl_IncidenciesCods1.Name = "Xl_IncidenciesCods1"
        Me.Xl_IncidenciesCods1.ReadOnly = True
        Me.Xl_IncidenciesCods1.Size = New System.Drawing.Size(338, 264)
        Me.Xl_IncidenciesCods1.TabIndex = 0
        '
        'Frm_IncidenciesCods
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 264)
        Me.Controls.Add(Me.Xl_IncidenciesCods1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "Frm_IncidenciesCods"
        Me.Text = "SELECCIONAR CODI INCIDENCIA"
        CType(Me.Xl_IncidenciesCods1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_IncidenciesCods1 As Xl_IncidenciesCods
End Class
