<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_PlatformsNewAlb
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOk = New System.Windows.Forms.Button()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.SplitContainerLeft = New System.Windows.Forms.SplitContainer()
        Me.ComboBoxDept = New System.Windows.Forms.ComboBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArxiuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AgrupacionsPerTransmisióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PreviewAlbaranesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarSeleccióAnteriorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelSumary = New System.Windows.Forms.Label()
        Me.Xl_PurchaseOrderItemsForPlatforms1 = New Xl_PurchaseOrderItemsForPlatforms()
        Me.Xl_StocksAvailable1 = New Xl_StockAvailable()
        Me.Xl_EciPlatformDeliveries1 = New Xl_EciPlatformDeliveries()
        Me.Xl_ProgressBar1 = New Xl_ProgressBar()
        Me.Panel1.SuspendLayout()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        CType(Me.SplitContainerLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerLeft.Panel1.SuspendLayout()
        Me.SplitContainerLeft.Panel2.SuspendLayout()
        Me.SplitContainerLeft.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Xl_StocksAvailable1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.Panel1.Controls.Add(Me.Xl_ProgressBar1)
        Me.Panel1.Controls.Add(Me.ButtonCancel)
        Me.Panel1.Controls.Add(Me.ButtonOk)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 550)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(951, 31)
        Me.Panel1.TabIndex = 41
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonCancel.Location = New System.Drawing.Point(732, 4)
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
        Me.ButtonOk.Location = New System.Drawing.Point(843, 4)
        Me.ButtonOk.Name = "ButtonOk"
        Me.ButtonOk.Size = New System.Drawing.Size(104, 24)
        Me.ButtonOk.TabIndex = 11
        Me.ButtonOk.Text = "ACCEPTAR"
        Me.ButtonOk.UseVisualStyleBackColor = False
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.Controls.Add(Me.SplitContainerLeft)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.Xl_EciPlatformDeliveries1)
        Me.SplitContainerMain.Panel2.Controls.Add(Me.ComboBoxDept)
        Me.SplitContainerMain.Size = New System.Drawing.Size(951, 526)
        Me.SplitContainerMain.SplitterDistance = 642
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
        Me.SplitContainerLeft.Size = New System.Drawing.Size(642, 526)
        Me.SplitContainerLeft.SplitterDistance = 257
        Me.SplitContainerLeft.TabIndex = 44
        '
        'ComboBoxDept
        '
        Me.ComboBoxDept.Dock = System.Windows.Forms.DockStyle.Top
        Me.ComboBoxDept.FormattingEnabled = True
        Me.ComboBoxDept.Location = New System.Drawing.Point(0, 0)
        Me.ComboBoxDept.Name = "ComboBoxDept"
        Me.ComboBoxDept.Size = New System.Drawing.Size(305, 21)
        Me.ComboBoxDept.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArxiuToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(951, 24)
        Me.MenuStrip1.TabIndex = 45
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArxiuToolStripMenuItem
        '
        Me.ArxiuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AgrupacionsPerTransmisióToolStripMenuItem, Me.PreviewAlbaranesToolStripMenuItem, Me.ImportarSeleccióAnteriorToolStripMenuItem})
        Me.ArxiuToolStripMenuItem.Name = "ArxiuToolStripMenuItem"
        Me.ArxiuToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.ArxiuToolStripMenuItem.Text = "Arxiu"
        '
        'AgrupacionsPerTransmisióToolStripMenuItem
        '
        Me.AgrupacionsPerTransmisióToolStripMenuItem.Name = "AgrupacionsPerTransmisióToolStripMenuItem"
        Me.AgrupacionsPerTransmisióToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.AgrupacionsPerTransmisióToolStripMenuItem.Text = "Agrupacions per transmisió"
        '
        'PreviewAlbaranesToolStripMenuItem
        '
        Me.PreviewAlbaranesToolStripMenuItem.Image = Global.Mat.Net.My.Resources.Resources.pdf
        Me.PreviewAlbaranesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.PreviewAlbaranesToolStripMenuItem.Name = "PreviewAlbaranesToolStripMenuItem"
        Me.PreviewAlbaranesToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.PreviewAlbaranesToolStripMenuItem.Text = "Preview albaranes"
        '
        'ImportarSeleccióAnteriorToolStripMenuItem
        '
        Me.ImportarSeleccióAnteriorToolStripMenuItem.Name = "ImportarSeleccióAnteriorToolStripMenuItem"
        Me.ImportarSeleccióAnteriorToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ImportarSeleccióAnteriorToolStripMenuItem.Text = "Importar selecció anterior"
        '
        'LabelSumary
        '
        Me.LabelSumary.AutoSize = True
        Me.LabelSumary.Location = New System.Drawing.Point(109, 5)
        Me.LabelSumary.Name = "LabelSumary"
        Me.LabelSumary.Size = New System.Drawing.Size(31, 13)
        Me.LabelSumary.TabIndex = 46
        Me.LabelSumary.Text = "Total"
        Me.LabelSumary.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Xl_PurchaseOrderItemsForPlatforms1
        '
        Me.Xl_PurchaseOrderItemsForPlatforms1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_PurchaseOrderItemsForPlatforms1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_PurchaseOrderItemsForPlatforms1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_PurchaseOrderItemsForPlatforms1.Name = "Xl_PurchaseOrderItemsForPlatforms1"
        Me.Xl_PurchaseOrderItemsForPlatforms1.Size = New System.Drawing.Size(642, 257)
        Me.Xl_PurchaseOrderItemsForPlatforms1.TabIndex = 43
        '
        'Xl_StocksAvailable1
        '
        Me.Xl_StocksAvailable1.DisplayObsolets = False
        Me.Xl_StocksAvailable1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_StocksAvailable1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_StocksAvailable1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_StocksAvailable1.MouseIsDown = False
        Me.Xl_StocksAvailable1.Name = "Xl_StocksAvailable1"
        Me.Xl_StocksAvailable1.Size = New System.Drawing.Size(642, 265)
        Me.Xl_StocksAvailable1.TabIndex = 1
        '
        'Xl_EciPlatformDeliveries1
        '
        Me.Xl_EciPlatformDeliveries1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Xl_EciPlatformDeliveries1.Location = New System.Drawing.Point(0, 21)
        Me.Xl_EciPlatformDeliveries1.Name = "Xl_EciPlatformDeliveries1"
        Me.Xl_EciPlatformDeliveries1.Size = New System.Drawing.Size(305, 505)
        Me.Xl_EciPlatformDeliveries1.TabIndex = 3
        '
        'Xl_ProgressBar1
        '
        Me.Xl_ProgressBar1.Location = New System.Drawing.Point(0, 1)
        Me.Xl_ProgressBar1.Name = "Xl_ProgressBar1"
        Me.Xl_ProgressBar1.Size = New System.Drawing.Size(726, 30)
        Me.Xl_ProgressBar1.TabIndex = 13
        Me.Xl_ProgressBar1.Visible = False
        '
        'Frm_PlatformsNewAlb
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(951, 581)
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Controls.Add(Me.LabelSumary)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.MainMenuStrip = Me.MenuStrip1
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
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Xl_StocksAvailable1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOk As System.Windows.Forms.Button
    Friend WithEvents SplitContainerMain As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerLeft As System.Windows.Forms.SplitContainer
    Friend WithEvents Xl_PurchaseOrderItemsForPlatforms1 As Xl_PurchaseOrderItemsForPlatforms
    Friend WithEvents Xl_StocksAvailable1 As Xl_StockAvailable
    Friend WithEvents ComboBoxDept As ComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ArxiuToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AgrupacionsPerTransmisióToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PreviewAlbaranesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Xl_ProgressBar1 As Xl_ProgressBar
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Xl_EciPlatformDeliveries1 As Xl_EciPlatformDeliveries
    Friend WithEvents ImportarSeleccióAnteriorToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LabelSumary As Label
End Class
