<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PromosPerRep
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
        Me.Xl_PromosPerRep1 = New Winforms.Xl_PromosPerRep()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Xl_PurchaseOrders1 = New Winforms.Xl_PurchaseOrders()
        CType(Me.Xl_PromosPerRep1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.Xl_PurchaseOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Xl_PromosPerRep1
        '
        Me.Xl_PromosPerRep1.AllowUserToAddRows = False
        Me.Xl_PromosPerRep1.AllowUserToDeleteRows = False
        Me.Xl_PromosPerRep1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PromosPerRep1.DisplayObsolets = False
        Me.Xl_PromosPerRep1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PromosPerRep1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PromosPerRep1.Name = "Xl_PromosPerRep1"
        Me.Xl_PromosPerRep1.ReadOnly = True
        Me.Xl_PromosPerRep1.Size = New System.Drawing.Size(576, 250)
        Me.Xl_PromosPerRep1.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Xl_PromosPerRep1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Xl_PurchaseOrders1)
        Me.SplitContainer1.Size = New System.Drawing.Size(576, 503)
        Me.SplitContainer1.SplitterDistance = 250
        Me.SplitContainer1.TabIndex = 1
        '
        'Xl_PurchaseOrders1
        '
        Me.Xl_PurchaseOrders1.AllowUserToAddRows = False
        Me.Xl_PurchaseOrders1.AllowUserToDeleteRows = False
        Me.Xl_PurchaseOrders1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_PurchaseOrders1.DisplayObsolets = False
        Me.Xl_PurchaseOrders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PurchaseOrders1.Filter = Nothing
        Me.Xl_PurchaseOrders1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PurchaseOrders1.Name = "Xl_PurchaseOrders1"
        Me.Xl_PurchaseOrders1.ReadOnly = True
        Me.Xl_PurchaseOrders1.Size = New System.Drawing.Size(576, 249)
        Me.Xl_PurchaseOrders1.TabIndex = 0
        '
        'Frm_PromosPerRep
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(576, 503)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Frm_PromosPerRep"
        Me.Text = "Comandes promo"
        CType(Me.Xl_PromosPerRep1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.Xl_PurchaseOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_PromosPerRep1 As Xl_PromosPerRep
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Xl_PurchaseOrders1 As Xl_PurchaseOrders
End Class
