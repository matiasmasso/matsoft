<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Ccx
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Ccx))
        Me.Xl_Contact_Logo1 = New Winforms.Xl_Contact_Logo()
        Me.Xl_TextboxSearch1 = New Winforms.Xl_TextboxSearch()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonCentres = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonPncs = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonSellOut = New System.Windows.Forms.ToolStripButton()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Xl_Contact_Logo1
        '
        Me.Xl_Contact_Logo1.AllowDrop = True
        Me.Xl_Contact_Logo1.Location = New System.Drawing.Point(0, 0)
        Me.Xl_Contact_Logo1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_Contact_Logo1.Name = "Xl_Contact_Logo1"
        Me.Xl_Contact_Logo1.Size = New System.Drawing.Size(150, 48)
        Me.Xl_Contact_Logo1.TabIndex = 0
        '
        'Xl_TextboxSearch1
        '
        Me.Xl_TextboxSearch1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_TextboxSearch1.Location = New System.Drawing.Point(480, 28)
        Me.Xl_TextboxSearch1.Margin = New System.Windows.Forms.Padding(8, 7, 8, 7)
        Me.Xl_TextboxSearch1.Name = "Xl_TextboxSearch1"
        Me.Xl_TextboxSearch1.Size = New System.Drawing.Size(155, 20)
        Me.Xl_TextboxSearch1.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 54)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ToolStrip1)
        Me.SplitContainer1.Size = New System.Drawing.Size(634, 274)
        Me.SplitContainer1.SplitterDistance = 148
        Me.SplitContainer1.TabIndex = 2
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(40, 40)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonCentres, Me.ToolStripButtonPncs, Me.ToolStripButtonSellOut})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(148, 77)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonCentres
        '
        Me.ToolStripButtonCentres.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonCentres.Image = CType(resources.GetObject("ToolStripButtonCentres.Image"), System.Drawing.Image)
        Me.ToolStripButtonCentres.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCentres.Name = "ToolStripButtonCentres"
        Me.ToolStripButtonCentres.Size = New System.Drawing.Size(147, 19)
        Me.ToolStripButtonCentres.Text = "Centres"
        Me.ToolStripButtonCentres.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonPncs
        '
        Me.ToolStripButtonPncs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonPncs.Image = CType(resources.GetObject("ToolStripButtonPncs.Image"), System.Drawing.Image)
        Me.ToolStripButtonPncs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPncs.Name = "ToolStripButtonPncs"
        Me.ToolStripButtonPncs.Size = New System.Drawing.Size(147, 19)
        Me.ToolStripButtonPncs.Text = "Pendents d'entrega"
        Me.ToolStripButtonPncs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButtonSellOut
        '
        Me.ToolStripButtonSellOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButtonSellOut.Image = CType(resources.GetObject("ToolStripButtonSellOut.Image"), System.Drawing.Image)
        Me.ToolStripButtonSellOut.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonSellOut.Name = "ToolStripButtonSellOut"
        Me.ToolStripButtonSellOut.Size = New System.Drawing.Size(147, 19)
        Me.ToolStripButtonSellOut.Text = "Sellout"
        Me.ToolStripButtonSellOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Frm_Ccx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 328)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Xl_TextboxSearch1)
        Me.Controls.Add(Me.Xl_Contact_Logo1)
        Me.Name = "Frm_Ccx"
        Me.Text = "Frm_Ccx"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Xl_Contact_Logo1 As Xl_Contact_Logo
    Friend WithEvents Xl_TextboxSearch1 As Xl_TextboxSearch
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents ToolStripButtonCentres As ToolStripButton
    Friend WithEvents ToolStripButtonPncs As ToolStripButton
    Friend WithEvents ToolStripButtonSellOut As ToolStripButton
End Class
