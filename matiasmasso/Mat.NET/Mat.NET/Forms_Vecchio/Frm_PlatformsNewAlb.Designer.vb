<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_PlatformsNewAlb
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerLeft = New System.Windows.Forms.SplitContainer()
        Me.Xl_PurchaseOrderItemsForPlatforms1 = New Mat.NET.Xl_PurchaseOrderItemsForPlatforms()
        Me.Xl_StocksAvailable1 = New Mat.NET.Xl_StocksAvailable()
        Me.SplitContainerRight = New System.Windows.Forms.SplitContainer()
        Me.Xl_PlatformAlbsSumary1 = New Mat.NET.Xl_PlatformAlbsSumary()
        Me.Xl_PlatformCentersSumary1 = New Mat.NET.Xl_PlatformCentersSumary()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        CType(Me.SplitContainerLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerLeft.Panel1.SuspendLayout()
        Me.SplitContainerLeft.Panel2.SuspendLayout()
        Me.SplitContainerLeft.SuspendLayout()
        CType(Me.SplitContainerRight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerRight.Panel1.SuspendLayout()
        Me.SplitContainerRight.Panel2.SuspendLayout()
        Me.SplitContainerRight.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 692)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1200, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(981, 4)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(104, 24)
        Me.ButtonCancel.TabIndex = 12
        Me.ButtonCancel.Text = "CANCELAR"
        Me.ButtonCancel.UseVisualStyleBackColor = False
        '
        'ButtonOk
        '
        Me.ButtonOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOk.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonOk.Enabled = False
        Me.ButtonOk.Location = New System.Drawing.Point(1092, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.Controls.Add(Me.SplitContainerLeft)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.SplitContainerRight)
        Me.SplitContainerMain.Size = New System.Drawing.Size(1200, 692)
        Me.SplitContainerMain.SplitterDistance = 814
        Me.SplitContainerMain.TabIndex = 44
        '
        'SplitContainerLeft
        '
        Me.SplitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerLeft.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerLeft.Name = "SplitContainerLeft"
        Me.SplitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerLeft.Panel1
        '
        Me.SplitContainerLeft.Panel1.Controls.Add(Me.Xl_PurchaseOrderItemsForPlatforms1)
        '
        'SplitContainerLeft.Panel2
        '
        Me.SplitContainerLeft.Panel2.Controls.Add(Me.Xl_StocksAvailable1)
        Me.SplitContainerLeft.Size = New System.Drawing.Size(814, 692)
        Me.SplitContainerLeft.SplitterDistance = 346
        Me.SplitContainerLeft.TabIndex = 44
        '
        'Xl_PurchaseOrderItemsForPlatforms1
        '
        Me.Xl_PurchaseOrderItemsForPlatforms1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PurchaseOrderItemsForPlatforms1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PurchaseOrderItemsForPlatforms1.Name = "Xl_PurchaseOrderItemsForPlatforms1"
        Me.Xl_PurchaseOrderItemsForPlatforms1.Size = New System.Drawing.Size(814, 346)
        Me.Xl_PurchaseOrderItemsForPlatforms1.TabIndex = 43
        '
        'Xl_StocksAvailable1
        '
        Me.Xl_StocksAvailable1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StocksAvailable1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StocksAvailable1.Name = "Xl_StocksAvailable1"
        Me.Xl_StocksAvailable1.Size = New System.Drawing.Size(814, 342)
        Me.Xl_StocksAvailable1.TabIndex = 1
        '
        'SplitContainerRight
        '
        Me.SplitContainerRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerRight.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerRight.Name = "SplitContainerRight"
        Me.SplitContainerRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerRight.Panel1
        '
        Me.SplitContainerRight.Panel1.Controls.Add(Me.Xl_PlatformAlbsSumary1)
        '
        'SplitContainerRight.Panel2
        '
        Me.SplitContainerRight.Panel2.Controls.Add(Me.Xl_PlatformCentersSumary1)
        Me.SplitContainerRight.Size = New System.Drawing.Size(382, 692)
        Me.SplitContainerRight.SplitterDistance = 115
        Me.SplitContainerRight.TabIndex = 0
        '
        'Xl_PlatformAlbsSumary1
        '
        Me.Xl_PlatformAlbsSumary1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PlatformAlbsSumary1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PlatformAlbsSumary1.Name = "Xl_PlatformAlbsSumary1"
        Me.Xl_PlatformAlbsSumary1.Size = New System.Drawing.Size(382, 115)
        Me.Xl_PlatformAlbsSumary1.TabIndex = 1
        '
        'Xl_PlatformCentersSumary1
        '
        Me.Xl_PlatformCentersSumary1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PlatformCentersSumary1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PlatformCentersSumary1.Name = "Xl_PlatformCentersSumary1"
        Me.Xl_PlatformCentersSumary1.Size = New System.Drawing.Size(382, 573)
        Me.Xl_PlatformCentersSumary1.TabIndex = 0
        '
        'Frm_PlatformsNewAlb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 723)
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Frm_PlatformsNewAlb"
        Me.Text = "Sortida per plataformes"
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.SplitContainerLeft.Panel1.ResumeLayout(False)
        Me.SplitContainerLeft.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerLeft, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerLeft.ResumeLayout(False)
        Me.SplitContainerRight.Panel1.ResumeLayout(False)
        Me.SplitContainerRight.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerRight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerRight.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerMain As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerLeft As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_PurchaseOrderItemsForPlatforms1 As Mat.NET.Xl_PurchaseOrderItemsForPlatforms
    Friend WithEvents Xl_StocksAvailable1 As Mat.NET.Xl_StocksAvailable
    Friend WithEvents SplitContainerRight As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_PlatformAlbsSumary1 As Mat.NET.Xl_PlatformAlbsSumary
    Friend WithEvents Xl_PlatformCentersSumary1 As Mat.NET.Xl_PlatformCentersSumary
End Class
