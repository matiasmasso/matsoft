<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CreditsClients
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.LabelMandatos = New System.Windows.Forms.Label()
        Me.LabelCredits = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 67)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(618, 284)
        Me.DataGridView1.TabIndex = 0
        '
        'LabelMandatos
        '
        Me.LabelMandatos.AutoSize = True
        Me.LabelMandatos.Location = New System.Drawing.Point(12, 32)
        Me.LabelMandatos.Name = "LabelMandatos"
        Me.LabelMandatos.Size = New System.Drawing.Size(56, 13)
        Me.LabelMandatos.TabIndex = 1
        Me.LabelMandatos.Text = "mandatos:"
        '
        'LabelCredits
        '
        Me.LabelCredits.AutoSize = True
        Me.LabelCredits.Location = New System.Drawing.Point(12, 9)
        Me.LabelCredits.Name = "LabelCredits"
        Me.LabelCredits.Size = New System.Drawing.Size(41, 13)
        Me.LabelCredits.TabIndex = 2
        Me.LabelCredits.Text = "crèdits:"
        '
        'Frm_CreditsClients
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 351)
        Me.Controls.Add(Me.LabelCredits)
        Me.Controls.Add(Me.LabelMandatos)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Frm_CreditsClients"
        Me.Text = "CREDITS CONCEDITS"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents LabelMandatos As System.Windows.Forms.Label
    Friend WithEvents LabelCredits As System.Windows.Forms.Label
End Class
