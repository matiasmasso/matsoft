<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Xl_Gir_SelEfts
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Xl_Gir_SelEfts))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxTot = New System.Windows.Forms.TextBox()
        Me.ToolStripButtonAddBanc = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonDelBanc = New System.Windows.Forms.ToolStripButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RefrescaToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonMinVtos = New System.Windows.Forms.ToolStripButton()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TextBoxSel = New System.Windows.Forms.TextBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(0, 290)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 16)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Tot:"
        '
        'TextBoxTot
        '
        Me.TextBoxTot.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxTot.Location = New System.Drawing.Point(26, 290)
        Me.TextBoxTot.Name = "TextBoxTot"
        Me.TextBoxTot.ReadOnly = True
        Me.TextBoxTot.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxTot.TabIndex = 8
        Me.TextBoxTot.TabStop = False
        Me.TextBoxTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ToolStripButtonAddBanc
        '
        Me.ToolStripButtonAddBanc.Image = CType(resources.GetObject("ToolStripButtonAddBanc.Image"), System.Drawing.Image)
        Me.ToolStripButtonAddBanc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonAddBanc.Name = "ToolStripButtonAddBanc"
        Me.ToolStripButtonAddBanc.Size = New System.Drawing.Size(86, 22)
        Me.ToolStripButtonAddBanc.Text = "afegir banc"
        Me.ToolStripButtonAddBanc.ToolTipText = "seleccionar tots els efectes de un banc concret"
        '
        'ToolStripButtonDelBanc
        '
        Me.ToolStripButtonDelBanc.Image = CType(resources.GetObject("ToolStripButtonDelBanc.Image"), System.Drawing.Image)
        Me.ToolStripButtonDelBanc.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonDelBanc.Name = "ToolStripButtonDelBanc"
        Me.ToolStripButtonDelBanc.Size = New System.Drawing.Size(87, 22)
        Me.ToolStripButtonDelBanc.Text = "retirar banc"
        Me.ToolStripButtonDelBanc.ToolTipText = "seleccionar tots els efectes de un banc concret"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(144, 290)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 16)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Seleccionar:"
        '
        'RefrescaToolStripButton
        '
        Me.RefrescaToolStripButton.Image = Global.Mat.Net.My.Resources.Resources.refresca
        Me.RefrescaToolStripButton.Name = "RefrescaToolStripButton"
        Me.RefrescaToolStripButton.Size = New System.Drawing.Size(68, 22)
        Me.RefrescaToolStripButton.Text = "refresca"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefrescaToolStripButton, Me.ToolStripButtonAddBanc, Me.ToolStripButtonDelBanc, Me.ToolStripButtonMinVtos})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(312, 25)
        Me.ToolStrip1.TabIndex = 6
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonMinVtos
        '
        Me.ToolStripButtonMinVtos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonMinVtos.Image = Global.Mat.Net.My.Resources.Resources.component
        Me.ToolStripButtonMinVtos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonMinVtos.Name = "ToolStripButtonMinVtos"
        Me.ToolStripButtonMinVtos.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonMinVtos.Text = "arreglar vtos.minims"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(2, 25)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(308, 262)
        Me.DataGridView1.TabIndex = 11
        '
        'TextBoxSel
        '
        Me.TextBoxSel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxSel.Location = New System.Drawing.Point(215, 290)
        Me.TextBoxSel.Name = "TextBoxSel"
        Me.TextBoxSel.Size = New System.Drawing.Size(92, 20)
        Me.TextBoxSel.TabIndex = 12
        Me.TextBoxSel.TabStop = False
        Me.TextBoxSel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Top
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 25)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(312, 10)
        Me.ProgressBar1.TabIndex = 13
        '
        'Xl_Gir_SelEfts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.TextBoxSel)
        Me.Controls.Add(Me.TextBoxTot)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "Xl_Gir_SelEfts"
        Me.Size = New System.Drawing.Size(312, 311)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxTot As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripButtonAddBanc As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonDelBanc As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RefrescaToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBoxSel As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripButtonMinVtos As System.Windows.Forms.ToolStripButton
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar

End Class
