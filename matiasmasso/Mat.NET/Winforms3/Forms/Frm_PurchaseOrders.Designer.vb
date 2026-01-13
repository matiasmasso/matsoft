<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PurchaseOrders
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
        Me.Xl_PurchaseOrders1 = New Xl_PurchaseOrders()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.Xl_Years1 = New Xl_Years()
        Me.Xl_TextboxSearch1 = New Xl_TextboxSearch()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.Xl_PurchaseOrders1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_PurchaseOrders1
        '
        Me.Xl_PurchaseOrders1.DisplayObsolets = False
        Me.Xl_PurchaseOrders1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PurchaseOrders1.Filter = Nothing
        Me.Xl_PurchaseOrders1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PurchaseOrders1.MouseIsDown = False
        Me.Xl_PurchaseOrders1.Name = "Xl_PurchaseOrders1"
        Me.Xl_PurchaseOrders1.Size = New System.Drawing.Size(744, 281)
        Me.Xl_PurchaseOrders1.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(745, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'Xl_Years1
        '
        Me.Xl_Years1.Location = New System.Drawing.Point(13, 1)
        Me.Xl_Years1.Name = "Xl_Years1"
        Me.Xl_Years1.Size = New System.Drawing.Size(163, 23)
        Me.Xl_Years1.TabIndex = 5
        Me.Xl_Years1.Value = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(559, 5)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(185, 20)
        Me.Xl_TextboxSearch1.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Xl_PurchaseOrders1)
        Me.Panel1.Controls.Add(Me.ProgressBar1)
        Me.Panel1.Location = New System.Drawing.Point(0, 31)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(744, 304)
        Me.Panel1.TabIndex = 7
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 281)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(744, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 0
        '
        'Frm_PurchaseOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(745, 336)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_Years1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "Frm_PurchaseOrders"
        Me.Text = "Comandes de clients"
        CType(Me.Xl_PurchaseOrders1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Xl_PurchaseOrders1 As Xl_PurchaseOrders
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents Xl_Years1 As Xl_Years
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
