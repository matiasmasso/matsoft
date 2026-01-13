Public Class Xl_Art_Gral
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents LabelNom As System.Windows.Forms.Label
    Friend WithEvents PictureBoxImg As System.Windows.Forms.PictureBox
    Friend WithEvents LabelNomCurt As System.Windows.Forms.Label
    Friend WithEvents LabelConsumidor As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents LabelStk As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents LabelPn2 As System.Windows.Forms.Label
    Friend WithEvents LabelPot As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents LabelPn1 As System.Windows.Forms.Label
    Friend WithEvents LabelTpa As System.Windows.Forms.Label
    Friend WithEvents LabelStp As System.Windows.Forms.Label
    Friend WithEvents ComboBoxMgz As System.Windows.Forms.ComboBox
    Friend WithEvents PanelForzarInnerPack As System.Windows.Forms.Panel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxForzarInnerPack As System.Windows.Forms.TextBox
    Friend WithEvents PictureBoxEan As System.Windows.Forms.PictureBox
    Friend WithEvents LabelNoEan As System.Windows.Forms.Label
    Friend WithEvents LabelDsc As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStripUserManual As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents VisualitzarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EliminarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCopyLink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelCnap As System.Windows.Forms.Label
    Friend WithEvents Xl_ProductChannels1 As Xl_ProductChannels
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents GroupBoxWarning As GroupBox
    Friend WithEvents LabelWarning As Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LabelNom = New System.Windows.Forms.Label()
        Me.PictureBoxImg = New System.Windows.Forms.PictureBox()
        Me.LabelNomCurt = New System.Windows.Forms.Label()
        Me.LabelConsumidor = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LabelStk = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.LabelPn2 = New System.Windows.Forms.Label()
        Me.LabelPot = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.LabelPn1 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.LabelTpa = New System.Windows.Forms.Label()
        Me.LabelStp = New System.Windows.Forms.Label()
        Me.ComboBoxMgz = New System.Windows.Forms.ComboBox()
        Me.PanelForzarInnerPack = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxForzarInnerPack = New System.Windows.Forms.TextBox()
        Me.PictureBoxEan = New System.Windows.Forms.PictureBox()
        Me.LabelNoEan = New System.Windows.Forms.Label()
        Me.LabelDsc = New System.Windows.Forms.Label()
        Me.ContextMenuStripUserManual = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.VisualitzarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EliminarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemCopyLink = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelCnap = New System.Windows.Forms.Label()
        Me.Xl_ProductChannels1 = New Mat.Net.Xl_ProductChannels()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.GroupBoxWarning = New System.Windows.Forms.GroupBox()
        Me.LabelWarning = New System.Windows.Forms.Label()
        CType(Me.PictureBoxImg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelForzarInnerPack.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBoxEan, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripUserManual.SuspendLayout()
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBoxWarning.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelNom
        '
        Me.LabelNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelNom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelNom.Location = New System.Drawing.Point(14, 48)
        Me.LabelNom.Name = "LabelNom"
        Me.LabelNom.Size = New System.Drawing.Size(317, 18)
        Me.LabelNom.TabIndex = 1
        '
        'PictureBoxImg
        '
        Me.PictureBoxImg.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxImg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxImg.Location = New System.Drawing.Point(379, 0)
        Me.PictureBoxImg.Name = "PictureBoxImg"
        Me.PictureBoxImg.Size = New System.Drawing.Size(350, 396)
        Me.PictureBoxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBoxImg.TabIndex = 5
        Me.PictureBoxImg.TabStop = False
        '
        'LabelNomCurt
        '
        Me.LabelNomCurt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelNomCurt.Location = New System.Drawing.Point(14, 28)
        Me.LabelNomCurt.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.LabelNomCurt.Name = "LabelNomCurt"
        Me.LabelNomCurt.Size = New System.Drawing.Size(317, 18)
        Me.LabelNomCurt.TabIndex = 7
        '
        'LabelConsumidor
        '
        Me.LabelConsumidor.BackColor = System.Drawing.Color.DarkSeaGreen
        Me.LabelConsumidor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelConsumidor.Location = New System.Drawing.Point(261, 307)
        Me.LabelConsumidor.Name = "LabelConsumidor"
        Me.LabelConsumidor.Size = New System.Drawing.Size(70, 18)
        Me.LabelConsumidor.TabIndex = 10
        Me.LabelConsumidor.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(258, 291)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 16)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "PVP:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(14, 345)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Magatzem:"
        '
        'LabelStk
        '
        Me.LabelStk.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelStk.Location = New System.Drawing.Point(170, 363)
        Me.LabelStk.Name = "LabelStk"
        Me.LabelStk.Size = New System.Drawing.Size(40, 18)
        Me.LabelStk.TabIndex = 18
        Me.LabelStk.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(154, 347)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 16)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Stock:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(194, 347)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(64, 16)
        Me.Label10.TabIndex = 21
        Me.Label10.Text = "Clients:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelPn2
        '
        Me.LabelPn2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelPn2.Location = New System.Drawing.Point(210, 363)
        Me.LabelPn2.Name = "LabelPn2"
        Me.LabelPn2.Size = New System.Drawing.Size(40, 18)
        Me.LabelPn2.TabIndex = 20
        Me.LabelPn2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelPot
        '
        Me.LabelPot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelPot.Location = New System.Drawing.Point(251, 363)
        Me.LabelPot.Name = "LabelPot"
        Me.LabelPot.Size = New System.Drawing.Size(40, 18)
        Me.LabelPot.TabIndex = 22
        Me.LabelPot.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(267, 347)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(32, 16)
        Me.Label13.TabIndex = 23
        Me.Label13.Text = "Pot:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelPn1
        '
        Me.LabelPn1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelPn1.Location = New System.Drawing.Point(291, 363)
        Me.LabelPn1.Name = "LabelPn1"
        Me.LabelPn1.Size = New System.Drawing.Size(40, 18)
        Me.LabelPn1.TabIndex = 24
        Me.LabelPn1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(267, 347)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(64, 16)
        Me.Label15.TabIndex = 25
        Me.Label15.Text = "Prov:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelTpa
        '
        Me.LabelTpa.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelTpa.Location = New System.Drawing.Point(14, 7)
        Me.LabelTpa.Margin = New System.Windows.Forms.Padding(3, 3, 3, 1)
        Me.LabelTpa.Name = "LabelTpa"
        Me.LabelTpa.Size = New System.Drawing.Size(88, 18)
        Me.LabelTpa.TabIndex = 29
        '
        'LabelStp
        '
        Me.LabelStp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LabelStp.Location = New System.Drawing.Point(102, 7)
        Me.LabelStp.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.LabelStp.Name = "LabelStp"
        Me.LabelStp.Size = New System.Drawing.Size(229, 18)
        Me.LabelStp.TabIndex = 31
        '
        'ComboBoxMgz
        '
        Me.ComboBoxMgz.FormattingEnabled = True
        Me.ComboBoxMgz.Location = New System.Drawing.Point(14, 361)
        Me.ComboBoxMgz.Name = "ComboBoxMgz"
        Me.ComboBoxMgz.Size = New System.Drawing.Size(150, 21)
        Me.ComboBoxMgz.TabIndex = 32
        '
        'PanelForzarInnerPack
        '
        Me.PanelForzarInnerPack.Controls.Add(Me.PictureBox1)
        Me.PanelForzarInnerPack.Controls.Add(Me.Label1)
        Me.PanelForzarInnerPack.Controls.Add(Me.TextBoxForzarInnerPack)
        Me.PanelForzarInnerPack.Location = New System.Drawing.Point(14, 411)
        Me.PanelForzarInnerPack.Name = "PanelForzarInnerPack"
        Me.PanelForzarInnerPack.Size = New System.Drawing.Size(239, 25)
        Me.PanelForzarInnerPack.TabIndex = 38
        Me.PanelForzarInnerPack.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Mat.Net.My.Resources.Resources.package
        Me.PictureBox1.Location = New System.Drawing.Point(4, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.TabIndex = 40
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(59, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(165, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "unitats per caixa. No fraccionable"
        '
        'TextBoxForzarInnerPack
        '
        Me.TextBoxForzarInnerPack.Location = New System.Drawing.Point(29, 1)
        Me.TextBoxForzarInnerPack.Name = "TextBoxForzarInnerPack"
        Me.TextBoxForzarInnerPack.ReadOnly = True
        Me.TextBoxForzarInnerPack.Size = New System.Drawing.Size(24, 20)
        Me.TextBoxForzarInnerPack.TabIndex = 38
        Me.TextBoxForzarInnerPack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PictureBoxEan
        '
        Me.PictureBoxEan.Location = New System.Drawing.Point(18, 112)
        Me.PictureBoxEan.Name = "PictureBoxEan"
        Me.PictureBoxEan.Size = New System.Drawing.Size(184, 57)
        Me.PictureBoxEan.TabIndex = 39
        Me.PictureBoxEan.TabStop = False
        '
        'LabelNoEan
        '
        Me.LabelNoEan.AutoSize = True
        Me.LabelNoEan.Location = New System.Drawing.Point(40, 135)
        Me.LabelNoEan.Name = "LabelNoEan"
        Me.LabelNoEan.Size = New System.Drawing.Size(145, 13)
        Me.LabelNoEan.TabIndex = 40
        Me.LabelNoEan.Text = "(codi de barres no disponible)"
        '
        'LabelDsc
        '
        Me.LabelDsc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelDsc.AutoSize = True
        Me.LabelDsc.Location = New System.Drawing.Point(11, 185)
        Me.LabelDsc.MaximumSize = New System.Drawing.Size(353, 74)
        Me.LabelDsc.Name = "LabelDsc"
        Me.LabelDsc.Size = New System.Drawing.Size(70, 13)
        Me.LabelDsc.TabIndex = 41
        Me.LabelDsc.Text = "(descripció...)"
        '
        'ContextMenuStripUserManual
        '
        Me.ContextMenuStripUserManual.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VisualitzarToolStripMenuItem, Me.ImportarToolStripMenuItem, Me.EliminarToolStripMenuItem, Me.ToolStripMenuItemCopyLink})
        Me.ContextMenuStripUserManual.Name = "ContextMenuStripUserManual"
        Me.ContextMenuStripUserManual.Size = New System.Drawing.Size(142, 92)
        '
        'VisualitzarToolStripMenuItem
        '
        Me.VisualitzarToolStripMenuItem.Name = "VisualitzarToolStripMenuItem"
        Me.VisualitzarToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.VisualitzarToolStripMenuItem.Text = "visualitzar"
        '
        'ImportarToolStripMenuItem
        '
        Me.ImportarToolStripMenuItem.Name = "ImportarToolStripMenuItem"
        Me.ImportarToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.ImportarToolStripMenuItem.Text = "importar"
        '
        'EliminarToolStripMenuItem
        '
        Me.EliminarToolStripMenuItem.Name = "EliminarToolStripMenuItem"
        Me.EliminarToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.EliminarToolStripMenuItem.Text = "eliminar"
        '
        'ToolStripMenuItemCopyLink
        '
        Me.ToolStripMenuItemCopyLink.Name = "ToolStripMenuItemCopyLink"
        Me.ToolStripMenuItemCopyLink.Size = New System.Drawing.Size(141, 22)
        Me.ToolStripMenuItemCopyLink.Text = "copiar enllaç"
        '
        'LabelCnap
        '
        Me.LabelCnap.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCnap.AutoSize = True
        Me.LabelCnap.Location = New System.Drawing.Point(15, 82)
        Me.LabelCnap.MaximumSize = New System.Drawing.Size(353, 74)
        Me.LabelCnap.Name = "LabelCnap"
        Me.LabelCnap.Size = New System.Drawing.Size(62, 13)
        Me.LabelCnap.TabIndex = 83
        Me.LabelCnap.Text = "codi CNAP:"
        '
        'Xl_ProductChannels1
        '
        Me.Xl_ProductChannels1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Xl_ProductChannels1.DisplayObsolets = False
        Me.Xl_ProductChannels1.Location = New System.Drawing.Point(208, 69)
        Me.Xl_ProductChannels1.MouseIsDown = False
        Me.Xl_ProductChannels1.Name = "Xl_ProductChannels1"
        Me.Xl_ProductChannels1.Size = New System.Drawing.Size(123, 100)
        Me.Xl_ProductChannels1.TabIndex = 84
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.Mat.Net.My.Resources.Resources.warn_16
        Me.PictureBox2.Location = New System.Drawing.Point(3, 12)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox2.TabIndex = 85
        Me.PictureBox2.TabStop = False
        '
        'GroupBoxWarning
        '
        Me.GroupBoxWarning.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxWarning.Controls.Add(Me.LabelWarning)
        Me.GroupBoxWarning.Controls.Add(Me.PictureBox2)
        Me.GroupBoxWarning.Location = New System.Drawing.Point(14, 245)
        Me.GroupBoxWarning.Name = "GroupBoxWarning"
        Me.GroupBoxWarning.Size = New System.Drawing.Size(359, 43)
        Me.GroupBoxWarning.TabIndex = 86
        Me.GroupBoxWarning.TabStop = False
        Me.GroupBoxWarning.Visible = False
        '
        'LabelWarning
        '
        Me.LabelWarning.AutoSize = True
        Me.LabelWarning.ForeColor = System.Drawing.Color.Firebrick
        Me.LabelWarning.Location = New System.Drawing.Point(22, 15)
        Me.LabelWarning.Name = "LabelWarning"
        Me.LabelWarning.Size = New System.Drawing.Size(56, 13)
        Me.LabelWarning.TabIndex = 86
        Me.LabelWarning.Text = "Warning..."
        '
        'Xl_Art_Gral
        '
        Me.Controls.Add(Me.GroupBoxWarning)
        Me.Controls.Add(Me.Xl_ProductChannels1)
        Me.Controls.Add(Me.LabelCnap)
        Me.Controls.Add(Me.LabelDsc)
        Me.Controls.Add(Me.LabelNoEan)
        Me.Controls.Add(Me.PictureBoxEan)
        Me.Controls.Add(Me.PanelForzarInnerPack)
        Me.Controls.Add(Me.ComboBoxMgz)
        Me.Controls.Add(Me.LabelStp)
        Me.Controls.Add(Me.LabelTpa)
        Me.Controls.Add(Me.LabelPn1)
        Me.Controls.Add(Me.LabelPot)
        Me.Controls.Add(Me.LabelPn2)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.LabelStk)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.LabelConsumidor)
        Me.Controls.Add(Me.LabelNomCurt)
        Me.Controls.Add(Me.PictureBoxImg)
        Me.Controls.Add(Me.LabelNom)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label15)
        Me.Name = "Xl_Art_Gral"
        Me.Size = New System.Drawing.Size(730, 459)
        CType(Me.PictureBoxImg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelForzarInnerPack.ResumeLayout(False)
        Me.PanelForzarInnerPack.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBoxEan, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripUserManual.ResumeLayout(False)
        CType(Me.Xl_ProductChannels1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBoxWarning.ResumeLayout(False)
        Me.GroupBoxWarning.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private _Sku As DTOProductSku
    Private mAllowEvents As Boolean

    Public Shadows Async Function Load(oSku As DTOProductSku) As Task
        Dim exs As New List(Of Exception)
        _Sku = oSku
        If _Sku IsNot Nothing Then
            With _Sku
                LabelTpa.Text = .category.brand.nom.Tradueix(Current.Session.Lang)
                LabelStp.Text = .category.nom.Tradueix(Current.Session.Lang)
                LabelNom.Text = .nomLlarg.Tradueix(Current.Session.Lang)
                LabelNomCurt.Text = .nom.Tradueix(Current.Session.Lang)
                Await LoadMgzs()
                LabelCnap.Text = "Codi CNAP: " & DTOProductSku.SelfOrInheritedCnapFullNom(oSku, Current.Session.Lang)

                If _Sku.RRPP IsNot Nothing Then
                    LabelConsumidor.Text = DTOAmt.CurFormatted(_Sku.RRPP)
                Else
                    LabelConsumidor.Text = ""
                End If

                Dim sDsc As String = _Sku.nomLlarg.Tradueix(Current.Session.Lang)
                If sDsc > "" Then
                    LabelDsc.Text = sDsc
                    LabelDsc.AutoEllipsis = True
                End If

                SetWarning()

                LabelNoEan.Visible = True
                PictureBoxEan.Visible = False
                If .Ean13 IsNot Nothing Then
                    If .Ean13.Value > "" Then
                        LabelNoEan.Visible = False
                        PictureBoxEan.Image = LegacyHelper.Ean13Helper.Bitmap(.ean13, iHeight:=PictureBoxEan.Height)
                        PictureBoxEan.Visible = True
                    End If
                End If

                If Not .IsNew Then SetMgz()

                If exs.Count = 0 Then
                    Dim oImage As Image = Nothing
                    If _Sku.Image IsNot Nothing Then
                        oImage = LegacyHelper.ImageHelper.FromBytes(_Sku.Image)
                        If oImage.Height > PictureBoxImg.Height Or oImage.Width > PictureBoxImg.Width Then
                            PictureBoxImg.Cursor = Cursors.Hand
                            PictureBoxImg.SizeMode = PictureBoxSizeMode.StretchImage
                            AddHandler PictureBoxImg.Click, AddressOf onImgZoomRequest
                        End If
                    End If
                    PictureBoxImg.Image = oImage
                Else
                    UIHelper.WarnError(exs)
                End If



                PanelForzarInnerPack.Visible = DTOProductSku.SelfOrInheritedForzarInnerPack(_Sku)
                TextBoxForzarInnerPack.Text = _Sku.InnerPack

                'EnableUserManualMenuItems()
            End With


            SetMgz()

            mAllowEvents = True
            Await RefrescaChannels()
        End If

    End Function

    Private Sub SetWarning()
        If _Sku.Availability > DTO.GlobalVariables.Today() Then
            LabelWarning.Text = "No disponible fins " & _Sku.Availability.ToShortDateString
            GroupBoxWarning.Visible = True
        End If
    End Sub

    Public Sub UpdateAvailability(fch As DateTime)
        _Sku.Availability = fch
        SetWarning()
    End Sub



    Private Sub onImgZoomRequest(sender As Object, e As System.EventArgs)
        Dim oFrm As New Frm_Img(LegacyHelper.ImageHelper.FromBytes(_Sku.Image), _Sku.NomLlarg.Tradueix(Current.Session.Lang))
        oFrm.Show()
    End Sub



    Private Sub SetMgz()
        Dim oMgz As DTOMgz = ComboBoxMgz.SelectedItem

        Dim sTmp As String = ""
        LabelStk.BackColor = DTOProductSku.BackColor(_Sku)
        LabelStk.Text = _Sku.Stock
        LabelPn2.Text = _Sku.Clients - _Sku.ClientsAlPot - _Sku.ClientsEnProgramacio
        LabelPot.Text = _Sku.ClientsAlPot + _Sku.ClientsEnProgramacio
        LabelPn1.Text = _Sku.Proveidors

    End Sub


    Private Sub PictureBoxThumbnail_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxImg.DoubleClick
        Dim oFrm As New Frm_Img(LegacyHelper.ImageHelper.Converter(_Sku.image), _Sku.nomLlarg.Tradueix(Current.Session.Lang))
        oFrm.Show()
    End Sub

    Private Async Function LoadMgzs() As Task
        Dim exs As New List(Of Exception)
        Dim oMgzs = Await FEB.Mgzs.All(_Sku, exs)
        If exs.Count = 0 Then
            With ComboBoxMgz
                .DisplayMember = "nom"
                .DataSource = oMgzs ' oDs.Tables(0)
                .SelectedItem = oMgzs.Find(Function(x) x.Equals(Current.Session.Emp.Mgz))
                If .SelectedIndex < 0 Then
                    If .Items.Count > 0 Then
                        .SelectedIndex = 0
                    End If
                End If
            End With
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub ComboBoxMgz_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxMgz.SelectedIndexChanged
        If mAllowEvents Then
            SetMgz()
        End If
    End Sub

    Public Sub forzarInnerPackChanged(ByVal blForzar As Boolean, ByVal iInnerPack As Integer)
        PanelForzarInnerPack.Visible = blForzar
        TextBoxForzarInnerPack.Text = iInnerPack
    End Sub

    Private Sub LabelStp_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelStp.DoubleClick
        Dim oFrm As New Frm_Stp(_Sku.Category, DTO.Defaults.SelectionModes.Browse)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestStp
        oFrm.Show()
    End Sub

    Private Sub RefreshRequestStp(ByVal sender As System.Object, ByVal e As MatEventArgs)
        Dim oCategory As DTOProductCategory = e.Argument
        LabelStp.Text = oCategory.nom.Tradueix(Current.Session.Lang)
    End Sub

    Private Sub LabelTpa_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LabelTpa.DoubleClick
        Dim oFrm As New Frm_Tpa(_Sku.Category.Brand)
        oFrm.Show()
    End Sub



    Public Sub SetDsc(ByVal sText As String)
        'trigered by Xl_Art_Edit through Frm_Art
        LabelDsc.Text = sText
        LabelDsc.AutoEllipsis = True

    End Sub


    Private Sub Xl_ProductChannels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToAddNew
        Dim item As New DTOProductChannel
        With item
            .Product = _Sku
            .Cod = DTOProductChannel.Cods.Inclou
        End With
        Dim oFrm As New Frm_ProductChannel(item)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaChannels
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductChannels1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductChannels1.RequestToRefresh
        Await RefrescaChannels()
    End Sub

    Private Async Sub RefrescaChannels(sender As Object, e As MatEventArgs)
        Await RefrescaChannels()
    End Sub
    Private Async Function RefrescaChannels() As Task
        Dim exs As New List(Of Exception)
        Dim oProductChannels = Await FEB.ProductChannels.All(exs, _Sku)
        If exs.Count = 0 Then
            Xl_ProductChannels1.Load(oProductChannels, Xl_ProductChannels.Modes.ChannelsPerProduct)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class
