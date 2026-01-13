<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_CfpPnds
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_CfpPndsMaster1 = New Winforms.Xl_CfpPndsMaster()
        Me.Xl_CfpPndsDetail1 = New Winforms.Xl_CfpPndsDetail()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_CfpPndsMaster1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Xl_CfpPndsDetail1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_CfpPndsMaster1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_CfpPndsDetail1)
        Me.SplitContainer1.Size = New System.Drawing.Size(834, 261)
        Me.SplitContainer1.SplitterDistance = 386
        Me.SplitContainer1.TabIndex = 0
        '
        'Xl_CfpPndsMaster1
        '
        Me.Xl_CfpPndsMaster1.AllowUserToAddRows = False
        Me.Xl_CfpPndsMaster1.AllowUserToDeleteRows = False
        Me.Xl_CfpPndsMaster1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CfpPndsMaster1.DisplayObsolets = False
        Me.Xl_CfpPndsMaster1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CfpPndsMaster1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CfpPndsMaster1.Name = "Xl_CfpPndsMaster1"
        Me.Xl_CfpPndsMaster1.ReadOnly = True
        Me.Xl_CfpPndsMaster1.Size = New System.Drawing.Size(386, 261)
        Me.Xl_CfpPndsMaster1.TabIndex = 0
        '
        'Xl_CfpPndsDetail1
        '
        Me.Xl_CfpPndsDetail1.AllowUserToAddRows = False
        Me.Xl_CfpPndsDetail1.AllowUserToDeleteRows = False
        Me.Xl_CfpPndsDetail1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_CfpPndsDetail1.DisplayObsolets = False
        Me.Xl_CfpPndsDetail1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_CfpPndsDetail1.Filter = Nothing
        Me.Xl_CfpPndsDetail1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_CfpPndsDetail1.Name = "Xl_CfpPndsDetail1"
        Me.Xl_CfpPndsDetail1.ReadOnly = True
        Me.Xl_CfpPndsDetail1.Size = New System.Drawing.Size(444, 261)
        Me.Xl_CfpPndsDetail1.TabIndex = 0
        '
        'Frm_CfpPnds
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 261)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_CfpPnds"
        Me.Text = "Deutors per forma de pago"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_CfpPndsMaster1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Xl_CfpPndsDetail1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_CfpPndsMaster1 As Xl_CfpPndsMaster
    Friend WithEvents Xl_CfpPndsDetail1 As Xl_CfpPndsDetail
End Class
