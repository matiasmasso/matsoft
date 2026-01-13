<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_VisaOrgs
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
        Me.Xl_VisaOrgs1 = New Mat.NET.Xl_VisaOrgs()
        CType(Me.Xl_VisaOrgs1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_VisaOrgs1
        '
        Me.Xl_VisaOrgs1.AllowUserToAddRows = False
        Me.Xl_VisaOrgs1.AllowUserToDeleteRows = False
        Me.Xl_VisaOrgs1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_VisaOrgs1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_VisaOrgs1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_VisaOrgs1.Name = "Xl_VisaOrgs1"
        Me.Xl_VisaOrgs1.ReadOnly = True
        Me.Xl_VisaOrgs1.Size = New System.Drawing.Size(350, 261)
        Me.Xl_VisaOrgs1.TabIndex = 0
        '
        'Frm_VisaOrgs
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(350, 261)
        Me.Controls.Add(Me.Xl_VisaOrgs1)
        Me.Name = "Frm_VisaOrgs"
        Me.Text = "Emisors de tarjes de crèdit"
        CType(Me.Xl_VisaOrgs1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Xl_VisaOrgs1 As Mat.NET.Xl_VisaOrgs
End Class
