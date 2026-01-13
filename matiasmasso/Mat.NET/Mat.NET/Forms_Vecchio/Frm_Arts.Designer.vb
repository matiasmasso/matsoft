<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frm_Arts
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
        Me.PictureBoxTpaLogo = New System.Windows.Forms.PictureBox()
        Me.PictureBoxArt = New System.Windows.Forms.PictureBox()
        Me.LabelPack = New System.Windows.Forms.Label()
        Me.TextBoxPack = New System.Windows.Forms.TextBox()
        Me.PictureBoxForzarInnerPack = New System.Windows.Forms.PictureBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButtonRefresca = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonBusca = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonExcel = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonIncentius = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonCdImg = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButtonPromos = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripComboBoxMgz = New System.Windows.Forms.ToolStripComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxPvp = New System.Windows.Forms.TextBox()
        Me.LabelMyd = New System.Windows.Forms.Label()
        Me.Xl_StpArts1 = New Mat.NET.Xl_StpArts()
        Me.Xl_ProductBrands1 = New Mat.NET.Xl_ProductBrands()
        Me.Xl_ProductCategories1 = New Mat.NET.Xl_ProductCategories()
        CType(Me.PictureBoxTpaLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxArt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxForzarInnerPack, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBoxTpaLogo
        '
        Me.PictureBoxTpaLogo.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxTpaLogo.BackColor = System.Drawing.Color.White
        Me.PictureBoxTpaLogo.Location = New System.Drawing.Point(-1, 249)
        Me.PictureBoxTpaLogo.Name = "PictureBoxTpaLogo"
        Me.PictureBoxTpaLogo.Size = New System.Drawing.Size(170, 76)
        Me.PictureBoxTpaLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxTpaLogo.TabIndex = 0
        Me.PictureBoxTpaLogo.TabStop = False
        '
        'PictureBoxArt
        '
        Me.PictureBoxArt.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxArt.BackColor = System.Drawing.Color.White
        Me.PictureBoxArt.Location = New System.Drawing.Point(605, 249)
        Me.PictureBoxArt.Name = "PictureBoxArt"
        Me.PictureBoxArt.Size = New System.Drawing.Size(66, 76)
        Me.PictureBoxArt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBoxArt.TabIndex = 4
        Me.PictureBoxArt.TabStop = False
        '
        'LabelPack
        '
        Me.LabelPack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelPack.AutoSize = True
        Me.LabelPack.Location = New System.Drawing.Point(502, 262)
        Me.LabelPack.Name = "LabelPack"
        Me.LabelPack.Size = New System.Drawing.Size(32, 13)
        Me.LabelPack.TabIndex = 5
        Me.LabelPack.Text = "Pack"
        '
        'TextBoxPack
        '
        Me.TextBoxPack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPack.Location = New System.Drawing.Point(540, 258)
        Me.TextBoxPack.Name = "TextBoxPack"
        Me.TextBoxPack.ReadOnly = True
        Me.TextBoxPack.Size = New System.Drawing.Size(26, 20)
        Me.TextBoxPack.TabIndex = 6
        Me.TextBoxPack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PictureBoxForzarInnerPack
        '
        Me.PictureBoxForzarInnerPack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxForzarInnerPack.Image = Global.Mat.NET.My.Resources.Resources.candau
        Me.PictureBoxForzarInnerPack.Location = New System.Drawing.Point(572, 257)
        Me.PictureBoxForzarInnerPack.Name = "PictureBoxForzarInnerPack"
        Me.PictureBoxForzarInnerPack.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxForzarInnerPack.TabIndex = 7
        Me.PictureBoxForzarInnerPack.TabStop = False
        Me.PictureBoxForzarInnerPack.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButtonRefresca, Me.ToolStripButtonBusca, Me.ToolStripButtonExcel, Me.ToolStripButtonIncentius, Me.ToolStripButtonCdImg, Me.ToolStripButtonPromos, Me.ToolStripComboBoxMgz})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(671, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButtonRefresca
        '
        Me.ToolStripButtonRefresca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonRefresca.Image = Global.Mat.NET.My.Resources.Resources.refresca
        Me.ToolStripButtonRefresca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonRefresca.Name = "ToolStripButtonRefresca"
        Me.ToolStripButtonRefresca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonRefresca.Text = "ToolStripButton1"
        '
        'ToolStripButtonBusca
        '
        Me.ToolStripButtonBusca.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonBusca.Image = Global.Mat.NET.My.Resources.Resources.search_16
        Me.ToolStripButtonBusca.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonBusca.Name = "ToolStripButtonBusca"
        Me.ToolStripButtonBusca.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonBusca.Text = "Busca"
        '
        'ToolStripButtonExcel
        '
        Me.ToolStripButtonExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonExcel.Image = Global.Mat.NET.My.Resources.Resources.Excel
        Me.ToolStripButtonExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonExcel.Name = "ToolStripButtonExcel"
        Me.ToolStripButtonExcel.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonExcel.Text = "Excel"
        '
        'ToolStripButtonIncentius
        '
        Me.ToolStripButtonIncentius.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonIncentius.Image = Global.Mat.NET.My.Resources.Resources.star
        Me.ToolStripButtonIncentius.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonIncentius.Name = "ToolStripButtonIncentius"
        Me.ToolStripButtonIncentius.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonIncentius.Text = "incentius"
        Me.ToolStripButtonIncentius.ToolTipText = "tables de incentius"
        '
        'ToolStripButtonCdImg
        '
        Me.ToolStripButtonCdImg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonCdImg.Image = Global.Mat.NET.My.Resources.Resources.img_16
        Me.ToolStripButtonCdImg.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonCdImg.Name = "ToolStripButtonCdImg"
        Me.ToolStripButtonCdImg.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonCdImg.Text = "CD imatges"
        '
        'ToolStripButtonPromos
        '
        Me.ToolStripButtonPromos.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButtonPromos.Image = Global.Mat.NET.My.Resources.Resources.package
        Me.ToolStripButtonPromos.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButtonPromos.Name = "ToolStripButtonPromos"
        Me.ToolStripButtonPromos.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButtonPromos.Text = "Packs promocionals"
        '
        'ToolStripComboBoxMgz
        '
        Me.ToolStripComboBoxMgz.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripComboBoxMgz.Name = "ToolStripComboBoxMgz"
        Me.ToolStripComboBoxMgz.Size = New System.Drawing.Size(300, 25)
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(502, 287)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(28, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "PVP"
        '
        'TextBoxPvp
        '
        Me.TextBoxPvp.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxPvp.Location = New System.Drawing.Point(540, 284)
        Me.TextBoxPvp.Name = "TextBoxPvp"
        Me.TextBoxPvp.ReadOnly = True
        Me.TextBoxPvp.Size = New System.Drawing.Size(48, 20)
        Me.TextBoxPvp.TabIndex = 12
        Me.TextBoxPvp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelMyd
        '
        Me.LabelMyd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelMyd.AutoSize = True
        Me.LabelMyd.Location = New System.Drawing.Point(296, 261)
        Me.LabelMyd.MaximumSize = New System.Drawing.Size(150, 48)
        Me.LabelMyd.Name = "LabelMyd"
        Me.LabelMyd.Size = New System.Drawing.Size(119, 13)
        Me.LabelMyd.TabIndex = 13
        Me.LabelMyd.Text = "(nombre del producto...)"
        '
        'Xl_StpArts1
        '
        Me.Xl_StpArts1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_StpArts1.Location = New System.Drawing.Point(262, 25)
        Me.Xl_StpArts1.Name = "Xl_StpArts1"
        Me.Xl_StpArts1.SelectionMode = BLL.Defaults.SelectionModes.Browse
        Me.Xl_StpArts1.Size = New System.Drawing.Size(409, 224)
        Me.Xl_StpArts1.TabIndex = 16
        '
        'Xl_ProductBrands1
        '
        Me.Xl_ProductBrands1.Location = New System.Drawing.Point(0, 25)
        Me.Xl_ProductBrands1.Name = "Xl_ProductBrands1"
        Me.Xl_ProductBrands1.Size = New System.Drawing.Size(120, 224)
        Me.Xl_ProductBrands1.TabIndex = 17
        '
        'Xl_ProductCategories1
        '
        Me.Xl_ProductCategories1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_ProductCategories1.Location = New System.Drawing.Point(122, 25)
        Me.Xl_ProductCategories1.Name = "Xl_ProductCategories1"
        Me.Xl_ProductCategories1.Size = New System.Drawing.Size(139, 224)
        Me.Xl_ProductCategories1.TabIndex = 18
        '
        'Frm_Arts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(671, 325)
        Me.Controls.Add(Me.Xl_ProductCategories1)
        Me.Controls.Add(Me.Xl_ProductBrands1)
        Me.Controls.Add(Me.Xl_StpArts1)
        Me.Controls.Add(Me.LabelMyd)
        Me.Controls.Add(Me.TextBoxPvp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.PictureBoxForzarInnerPack)
        Me.Controls.Add(Me.TextBoxPack)
        Me.Controls.Add(Me.LabelPack)
        Me.Controls.Add(Me.PictureBoxArt)
        Me.Controls.Add(Me.PictureBoxTpaLogo)
        Me.Name = "Frm_Arts"
        Me.Text = "ARTICLES"
        CType(Me.PictureBoxTpaLogo,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBoxArt,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.PictureBoxForzarInnerPack,System.ComponentModel.ISupportInitialize).EndInit
        Me.ToolStrip1.ResumeLayout(false)
        Me.ToolStrip1.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents PictureBoxTpaLogo As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBoxArt As System.Windows.Forms.PictureBox
    Friend WithEvents LabelPack As System.Windows.Forms.Label
    Friend WithEvents TextBoxPack As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxForzarInnerPack As System.Windows.Forms.PictureBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButtonRefresca As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxPvp As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripButtonBusca As System.Windows.Forms.ToolStripButton
    Friend WithEvents LabelMyd As System.Windows.Forms.Label
    Friend WithEvents ToolStripButtonExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonIncentius As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonCdImg As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButtonPromos As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripComboBoxMgz As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Xl_StpArts1 As Mat.NET.Xl_StpArts
    Friend WithEvents Xl_ProductBrands1 As Mat.NET.Xl_ProductBrands
    Friend WithEvents Xl_ProductCategories1 As Mat.NET.Xl_ProductCategories
End Class
