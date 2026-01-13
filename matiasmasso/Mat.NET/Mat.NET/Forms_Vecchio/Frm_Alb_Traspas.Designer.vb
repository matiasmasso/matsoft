<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Alb_Traspas
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.LabelFch = New System.Windows.Forms.Label
        Me.LabelMgzTo = New System.Windows.Forms.Label
        Me.LabelMgzFrom = New System.Windows.Forms.Label
        Me.LabelObs = New System.Windows.Forms.Label
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelFch
        '
        Me.LabelFch.AutoSize = True
        Me.LabelFch.Location = New System.Drawing.Point(6, 49)
        Me.LabelFch.Name = "LabelFch"
        Me.LabelFch.Size = New System.Drawing.Size(28, 13)
        Me.LabelFch.TabIndex = 10
        Me.LabelFch.Text = "data"
        '
        'LabelMgzTo
        '
        Me.LabelMgzTo.AutoSize = True
        Me.LabelMgzTo.Location = New System.Drawing.Point(6, 29)
        Me.LabelMgzTo.Name = "LabelMgzTo"
        Me.LabelMgzTo.Size = New System.Drawing.Size(40, 13)
        Me.LabelMgzTo.TabIndex = 9
        Me.LabelMgzTo.Text = "MgzTo"
        '
        'LabelMgzFrom
        '
        Me.LabelMgzFrom.AutoSize = True
        Me.LabelMgzFrom.Location = New System.Drawing.Point(6, 6)
        Me.LabelMgzFrom.Name = "LabelMgzFrom"
        Me.LabelMgzFrom.Size = New System.Drawing.Size(50, 13)
        Me.LabelMgzFrom.TabIndex = 8
        Me.LabelMgzFrom.Text = "MgzFrom"
        '
        'LabelObs
        '
        Me.LabelObs.AutoSize = True
        Me.LabelObs.Location = New System.Drawing.Point(2, 70)
        Me.LabelObs.Name = "LabelObs"
        Me.LabelObs.Size = New System.Drawing.Size(75, 13)
        Me.LabelObs.TabIndex = 7
        Me.LabelObs.Text = "Observacions:"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(0, 113)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.Size = New System.Drawing.Size(380, 274)
        Me.DataGridView1.TabIndex = 11
        '
        'Frm_Alb_Traspas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 387)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelFch)
        Me.Controls.Add(Me.LabelMgzTo)
        Me.Controls.Add(Me.LabelMgzFrom)
        Me.Controls.Add(Me.LabelObs)
        Me.Name = "Frm_Alb_Traspas"
        Me.Text = "TRASPAS DE MAGATZEM"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelFch As System.Windows.Forms.Label
    Friend WithEvents LabelMgzTo As System.Windows.Forms.Label
    Friend WithEvents LabelMgzFrom As System.Windows.Forms.Label
    Friend WithEvents LabelObs As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
End Class
