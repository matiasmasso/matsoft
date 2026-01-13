<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Banc_Conciliacions2
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DataGridViewBancs = New System.Windows.Forms.DataGridView
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.DataGridViewQ43 = New System.Windows.Forms.DataGridView
        Me.Xl_Ccd_Extracte1 = New Xl_Ccd_Extracte
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridViewBancs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DataGridViewQ43, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridViewBancs)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(882, 748)
        Me.SplitContainer1.SplitterDistance = 250
        Me.SplitContainer1.TabIndex = 0
        '
        'DataGridViewBancs
        '
        Me.DataGridViewBancs.AllowUserToAddRows = False
        Me.DataGridViewBancs.AllowUserToDeleteRows = False
        Me.DataGridViewBancs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewBancs.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewBancs.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewBancs.Name = "DataGridViewBancs"
        Me.DataGridViewBancs.ReadOnly = True
        Me.DataGridViewBancs.Size = New System.Drawing.Size(250, 748)
        Me.DataGridViewBancs.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DataGridViewQ43)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Xl_Ccd_Extracte1)
        Me.SplitContainer2.Size = New System.Drawing.Size(628, 748)
        Me.SplitContainer2.SplitterDistance = 376
        Me.SplitContainer2.TabIndex = 0
        '
        'DataGridViewQ43
        '
        Me.DataGridViewQ43.AllowUserToAddRows = False
        Me.DataGridViewQ43.AllowUserToDeleteRows = False
        Me.DataGridViewQ43.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewQ43.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewQ43.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewQ43.Name = "DataGridViewQ43"
        Me.DataGridViewQ43.ReadOnly = True
        Me.DataGridViewQ43.Size = New System.Drawing.Size(628, 376)
        Me.DataGridViewQ43.TabIndex = 1
        '
        'Xl_Ccd_Extracte1
        '
        Me.Xl_Ccd_Extracte1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_Ccd_Extracte1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Ccd_Extracte1.Name = "Xl_Ccd_Extracte1"
        Me.Xl_Ccd_Extracte1.Size = New System.Drawing.Size(628, 368)
        Me.Xl_Ccd_Extracte1.TabIndex = 0
        '
        'Frm_Banc_Conciliacions2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(882, 748)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_Banc_Conciliacions2"
        Me.Text = "CONCILIACIONS BANCARIES AMB EL QUADERN 43 (ENTITATS NACIONALS)"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridViewBancs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DataGridViewQ43, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewBancs As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridViewQ43 As System.Windows.Forms.DataGridView
    Friend WithEvents Xl_Ccd_Extracte1 As Xl_Ccd_Extracte
End Class
