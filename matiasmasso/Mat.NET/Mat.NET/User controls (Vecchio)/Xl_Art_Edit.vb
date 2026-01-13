Imports System.Drawing

Public Class Xl_Art_Edit
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
    Friend WithEvents PictureBoxThumbnail As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomCurt As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNom As System.Windows.Forms.TextBox
    Friend WithEvents Xl_Image1 As Xl_Image
    Friend WithEvents CheckBoxObsoleto As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoMgz As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoWeb As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxNoStk As System.Windows.Forms.CheckBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Xl_EAN1 As Xl_EAN
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNomPrv As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents TextBoxDscESP As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDscCAT As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxDscENG As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxDscInheritFromParent As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxLastProduction As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxTransportMobles As System.Windows.Forms.CheckBox
    Friend WithEvents Xl_DropdownList_Ivas1 As Xl_DropdownList_Ivas
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNoTarifa As System.Windows.Forms.CheckBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Xl_CnapLookup1 As Xl_CnapLookup
    Friend WithEvents ButtonTarifesCustom As System.Windows.Forms.Button
    Friend WithEvents LabelTarifesCustom As System.Windows.Forms.Label
    Friend WithEvents CheckBoxVirtual As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxCodiProveedor As System.Windows.Forms.TextBox
    Friend WithEvents Xl_PercentCostDto As Mat.Net.Xl_Percent
    Friend WithEvents Xl_PercentRetail As Mat.Net.Xl_Percent
    Friend WithEvents Xl_AmtCurRetail As Mat.Net.Xl_AmountCur
    Friend WithEvents Xl_AmtCurCost As Mat.Net.Xl_AmountCur
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents LabelCost As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNoPro As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonTarifaCost As System.Windows.Forms.Button
    Friend WithEvents Xl_LookupProduct1 As Mat.NET.Xl_LookupProduct
    Friend WithEvents TextBoxKeys As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Xl_Art_Edit))
        Me.PictureBoxThumbnail = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNomCurt = New System.Windows.Forms.TextBox()
        Me.TextBoxNom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CheckBoxObsoleto = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoStk = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoMgz = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoWeb = New System.Windows.Forms.CheckBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TextBoxKeys = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBoxNomPrv = New System.Windows.Forms.TextBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.TextBoxDscESP = New System.Windows.Forms.TextBox()
        Me.TextBoxDscCAT = New System.Windows.Forms.TextBox()
        Me.TextBoxDscENG = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CheckBoxDscInheritFromParent = New System.Windows.Forms.CheckBox()
        Me.CheckBoxLastProduction = New System.Windows.Forms.CheckBox()
        Me.CheckBoxTransportMobles = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CheckBoxNoTarifa = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.ButtonTarifesCustom = New System.Windows.Forms.Button()
        Me.LabelTarifesCustom = New System.Windows.Forms.Label()
        Me.CheckBoxVirtual = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxCodiProveedor = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.LabelCost = New System.Windows.Forms.Label()
        Me.Xl_PercentCostDto = New Mat.NET.Xl_Percent()
        Me.Xl_PercentRetail = New Mat.NET.Xl_Percent()
        Me.Xl_AmtCurRetail = New Mat.NET.Xl_AmountCur()
        Me.Xl_AmtCurCost = New Mat.NET.Xl_AmountCur()
        Me.Xl_CnapLookup1 = New Mat.NET.Xl_CnapLookup()
        Me.Xl_DropdownList_Ivas1 = New Mat.NET.Xl_DropdownList_Ivas()
        Me.Xl_EAN1 = New Mat.NET.Xl_EAN()
        Me.Xl_Image1 = New Mat.NET.Xl_Image()
        Me.CheckBoxNoPro = New System.Windows.Forms.CheckBox()
        Me.ButtonTarifaCost = New System.Windows.Forms.Button()
        Me.Xl_LookupProduct1 = New Mat.NET.Xl_LookupProduct()
        CType(Me.PictureBoxThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBoxThumbnail
        '
        Me.PictureBoxThumbnail.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PictureBoxThumbnail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.PictureBoxThumbnail.Location = New System.Drawing.Point(730, 8)
        Me.PictureBoxThumbnail.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.PictureBoxThumbnail.Name = "PictureBoxThumbnail"
        Me.PictureBoxThumbnail.Size = New System.Drawing.Size(48, 48)
        Me.PictureBoxThumbnail.TabIndex = 29
        Me.PictureBoxThumbnail.TabStop = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(0, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Classif.:"
        '
        'TextBoxNomCurt
        '
        Me.TextBoxNomCurt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomCurt.Location = New System.Drawing.Point(64, 31)
        Me.TextBoxNomCurt.MaxLength = 50
        Me.TextBoxNomCurt.Name = "TextBoxNomCurt"
        Me.TextBoxNomCurt.Size = New System.Drawing.Size(154, 20)
        Me.TextBoxNomCurt.TabIndex = 32
        '
        'TextBoxNom
        '
        Me.TextBoxNom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNom.Location = New System.Drawing.Point(64, 51)
        Me.TextBoxNom.MaxLength = 60
        Me.TextBoxNom.Name = "TextBoxNom"
        Me.TextBoxNom.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNom.TabIndex = 33
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(0, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 16)
        Me.Label1.TabIndex = 34
        Me.Label1.Text = "Nom curt:"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(0, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 16)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "Nom:"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(380, 408)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 38
        Me.Label5.Text = "C.Barres:"
        '
        'CheckBoxObsoleto
        '
        Me.CheckBoxObsoleto.Location = New System.Drawing.Point(281, 322)
        Me.CheckBoxObsoleto.Name = "CheckBoxObsoleto"
        Me.CheckBoxObsoleto.Size = New System.Drawing.Size(72, 16)
        Me.CheckBoxObsoleto.TabIndex = 50
        Me.CheckBoxObsoleto.Text = "Obsolet"
        '
        'CheckBoxNoStk
        '
        Me.CheckBoxNoStk.Location = New System.Drawing.Point(281, 340)
        Me.CheckBoxNoStk.Name = "CheckBoxNoStk"
        Me.CheckBoxNoStk.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxNoStk.TabIndex = 51
        Me.CheckBoxNoStk.Text = "No Picking"
        '
        'CheckBoxNoMgz
        '
        Me.CheckBoxNoMgz.Location = New System.Drawing.Point(281, 356)
        Me.CheckBoxNoMgz.Name = "CheckBoxNoMgz"
        Me.CheckBoxNoMgz.Size = New System.Drawing.Size(104, 16)
        Me.CheckBoxNoMgz.TabIndex = 52
        Me.CheckBoxNoMgz.Text = "No Magatzem"
        '
        'CheckBoxNoWeb
        '
        Me.CheckBoxNoWeb.Location = New System.Drawing.Point(281, 372)
        Me.CheckBoxNoWeb.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxNoWeb.Name = "CheckBoxNoWeb"
        Me.CheckBoxNoWeb.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxNoWeb.TabIndex = 53
        Me.CheckBoxNoWeb.Text = "No web"
        '
        'Label13
        '
        Me.Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.Location = New System.Drawing.Point(380, 430)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 16)
        Me.Label13.TabIndex = 54
        Me.Label13.Text = "Claus:"
        '
        'TextBoxKeys
        '
        Me.TextBoxKeys.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxKeys.Location = New System.Drawing.Point(444, 427)
        Me.TextBoxKeys.Name = "TextBoxKeys"
        Me.TextBoxKeys.Size = New System.Drawing.Size(283, 20)
        Me.TextBoxKeys.TabIndex = 55
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(0, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 16)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Nom prov:"
        '
        'TextBoxNomPrv
        '
        Me.TextBoxNomPrv.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxNomPrv.Location = New System.Drawing.Point(64, 94)
        Me.TextBoxNomPrv.MaxLength = 60
        Me.TextBoxNomPrv.Name = "TextBoxNomPrv"
        Me.TextBoxNomPrv.Size = New System.Drawing.Size(301, 20)
        Me.TextBoxNomPrv.TabIndex = 59
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(15, 27)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(47, 39)
        Me.PictureBox1.TabIndex = 66
        Me.PictureBox1.TabStop = False
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(15, 63)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(47, 39)
        Me.PictureBox2.TabIndex = 68
        Me.PictureBox2.TabStop = False
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(15, 99)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(47, 39)
        Me.PictureBox3.TabIndex = 70
        Me.PictureBox3.TabStop = False
        '
        'TextBoxDscESP
        '
        Me.TextBoxDscESP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDscESP.Location = New System.Drawing.Point(57, 27)
        Me.TextBoxDscESP.Multiline = True
        Me.TextBoxDscESP.Name = "TextBoxDscESP"
        Me.TextBoxDscESP.Size = New System.Drawing.Size(301, 39)
        Me.TextBoxDscESP.TabIndex = 61
        '
        'TextBoxDscCAT
        '
        Me.TextBoxDscCAT.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDscCAT.Location = New System.Drawing.Point(57, 63)
        Me.TextBoxDscCAT.Multiline = True
        Me.TextBoxDscCAT.Name = "TextBoxDscCAT"
        Me.TextBoxDscCAT.Size = New System.Drawing.Size(301, 39)
        Me.TextBoxDscCAT.TabIndex = 67
        '
        'TextBoxDscENG
        '
        Me.TextBoxDscENG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxDscENG.Location = New System.Drawing.Point(57, 99)
        Me.TextBoxDscENG.Multiline = True
        Me.TextBoxDscENG.Name = "TextBoxDscENG"
        Me.TextBoxDscENG.Size = New System.Drawing.Size(301, 39)
        Me.TextBoxDscENG.TabIndex = 69
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.CheckBoxDscInheritFromParent)
        Me.GroupBox1.Controls.Add(Me.TextBoxDscENG)
        Me.GroupBox1.Controls.Add(Me.TextBoxDscCAT)
        Me.GroupBox1.Controls.Add(Me.TextBoxDscESP)
        Me.GroupBox1.Controls.Add(Me.PictureBox3)
        Me.GroupBox1.Controls.Add(Me.PictureBox2)
        Me.GroupBox1.Controls.Add(Me.PictureBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 156)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(367, 143)
        Me.GroupBox1.TabIndex = 71
        Me.GroupBox1.TabStop = False
        '
        'CheckBoxDscInheritFromParent
        '
        Me.CheckBoxDscInheritFromParent.AutoSize = True
        Me.CheckBoxDscInheritFromParent.Location = New System.Drawing.Point(6, -2)
        Me.CheckBoxDscInheritFromParent.Name = "CheckBoxDscInheritFromParent"
        Me.CheckBoxDscInheritFromParent.Size = New System.Drawing.Size(123, 17)
        Me.CheckBoxDscInheritFromParent.TabIndex = 71
        Me.CheckBoxDscInheritFromParent.Text = "Hereda descripcions"
        Me.CheckBoxDscInheritFromParent.UseVisualStyleBackColor = True
        '
        'CheckBoxLastProduction
        '
        Me.CheckBoxLastProduction.Location = New System.Drawing.Point(281, 305)
        Me.CheckBoxLastProduction.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxLastProduction.Name = "CheckBoxLastProduction"
        Me.CheckBoxLastProduction.Size = New System.Drawing.Size(104, 16)
        Me.CheckBoxLastProduction.TabIndex = 75
        Me.CheckBoxLastProduction.Text = "Ultimes unitats"
        '
        'CheckBoxTransportMobles
        '
        Me.CheckBoxTransportMobles.Location = New System.Drawing.Point(281, 419)
        Me.CheckBoxTransportMobles.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxTransportMobles.Name = "CheckBoxTransportMobles"
        Me.CheckBoxTransportMobles.Size = New System.Drawing.Size(91, 16)
        Me.CheckBoxTransportMobles.TabIndex = 76
        Me.CheckBoxTransportMobles.Text = "transp.mobles"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(572, 408)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 16)
        Me.Label6.TabIndex = 103
        Me.Label6.Text = "IVA:"
        '
        'CheckBoxNoTarifa
        '
        Me.CheckBoxNoTarifa.Location = New System.Drawing.Point(281, 403)
        Me.CheckBoxNoTarifa.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxNoTarifa.Name = "CheckBoxNoTarifa"
        Me.CheckBoxNoTarifa.Size = New System.Drawing.Size(91, 16)
        Me.CheckBoxNoTarifa.TabIndex = 106
        Me.CheckBoxNoTarifa.Text = "no tarifa"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(2, 121)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 13)
        Me.Label11.TabIndex = 108
        Me.Label11.Text = "Cnap:"
        '
        'ButtonTarifesCustom
        '
        Me.ButtonTarifesCustom.Location = New System.Drawing.Point(8, 421)
        Me.ButtonTarifesCustom.Name = "ButtonTarifesCustom"
        Me.ButtonTarifesCustom.Size = New System.Drawing.Size(81, 23)
        Me.ButtonTarifesCustom.TabIndex = 110
        Me.ButtonTarifesCustom.Text = "tarifa custom"
        Me.ButtonTarifesCustom.UseVisualStyleBackColor = True
        '
        'LabelTarifesCustom
        '
        Me.LabelTarifesCustom.AutoSize = True
        Me.LabelTarifesCustom.Location = New System.Drawing.Point(94, 426)
        Me.LabelTarifesCustom.Name = "LabelTarifesCustom"
        Me.LabelTarifesCustom.Size = New System.Drawing.Size(181, 13)
        Me.LabelTarifesCustom.TabIndex = 109
        Me.LabelTarifesCustom.Text = "(no hi han clients amb tarifes custom)"
        Me.LabelTarifesCustom.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'CheckBoxVirtual
        '
        Me.CheckBoxVirtual.AutoSize = True
        Me.CheckBoxVirtual.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxVirtual.Location = New System.Drawing.Point(270, 33)
        Me.CheckBoxVirtual.Name = "CheckBoxVirtual"
        Me.CheckBoxVirtual.Size = New System.Drawing.Size(104, 17)
        Me.CheckBoxVirtual.TabIndex = 111
        Me.CheckBoxVirtual.Text = "referencia virtual"
        Me.CheckBoxVirtual.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(0, 76)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 16)
        Me.Label7.TabIndex = 113
        Me.Label7.Text = "Codi prov.:"
        '
        'TextBoxCodiProveedor
        '
        Me.TextBoxCodiProveedor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCodiProveedor.Location = New System.Drawing.Point(64, 74)
        Me.TextBoxCodiProveedor.MaxLength = 50
        Me.TextBoxCodiProveedor.Name = "TextBoxCodiProveedor"
        Me.TextBoxCodiProveedor.Size = New System.Drawing.Size(154, 20)
        Me.TextBoxCodiProveedor.TabIndex = 112
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(16, 330)
        Me.Label9.Margin = New System.Windows.Forms.Padding(3, 3, 2, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 16)
        Me.Label9.TabIndex = 121
        Me.Label9.Text = "Public:"
        '
        'LabelCost
        '
        Me.LabelCost.Location = New System.Drawing.Point(16, 306)
        Me.LabelCost.Name = "LabelCost"
        Me.LabelCost.Size = New System.Drawing.Size(64, 16)
        Me.LabelCost.TabIndex = 118
        Me.LabelCost.Text = "Cost:"
        '
        'Xl_PercentCostDto
        '
        Me.Xl_PercentCostDto.BackColor = System.Drawing.Color.White
        Me.Xl_PercentCostDto.Enabled = False
        Me.Xl_PercentCostDto.Location = New System.Drawing.Point(171, 305)
        Me.Xl_PercentCostDto.Name = "Xl_PercentCostDto"
        Me.Xl_PercentCostDto.Size = New System.Drawing.Size(47, 20)
        Me.Xl_PercentCostDto.TabIndex = 125
        Me.Xl_PercentCostDto.Text = "0,00 %"
        Me.Xl_PercentCostDto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentCostDto.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_PercentRetail
        '
        Me.Xl_PercentRetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_PercentRetail.Enabled = False
        Me.Xl_PercentRetail.Location = New System.Drawing.Point(171, 330)
        Me.Xl_PercentRetail.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.Xl_PercentRetail.Name = "Xl_PercentRetail"
        Me.Xl_PercentRetail.Size = New System.Drawing.Size(47, 20)
        Me.Xl_PercentRetail.TabIndex = 124
        Me.Xl_PercentRetail.TabStop = False
        Me.Xl_PercentRetail.Text = "0,00 %"
        Me.Xl_PercentRetail.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.Xl_PercentRetail.Value = New Decimal(New Integer() {0, 0, 0, 131072})
        '
        'Xl_AmtCurRetail
        '
        Me.Xl_AmtCurRetail.Amt = Nothing
        Me.Xl_AmtCurRetail.Amt2 = Nothing
        Me.Xl_AmtCurRetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmtCurRetail.Enabled = False
        Me.Xl_AmtCurRetail.Location = New System.Drawing.Point(67, 329)
        Me.Xl_AmtCurRetail.Margin = New System.Windows.Forms.Padding(1, 2, 3, 3)
        Me.Xl_AmtCurRetail.Name = "Xl_AmtCurRetail"
        Me.Xl_AmtCurRetail.Size = New System.Drawing.Size(97, 20)
        Me.Xl_AmtCurRetail.TabIndex = 117
        '
        'Xl_AmtCurCost
        '
        Me.Xl_AmtCurCost.Amt = Nothing
        Me.Xl_AmtCurCost.Amt2 = Nothing
        Me.Xl_AmtCurCost.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_AmtCurCost.Enabled = False
        Me.Xl_AmtCurCost.Location = New System.Drawing.Point(67, 305)
        Me.Xl_AmtCurCost.Margin = New System.Windows.Forms.Padding(3, 1, 3, 2)
        Me.Xl_AmtCurCost.Name = "Xl_AmtCurCost"
        Me.Xl_AmtCurCost.Size = New System.Drawing.Size(97, 20)
        Me.Xl_AmtCurCost.TabIndex = 114
        '
        'Xl_CnapLookup1
        '
        Me.Xl_CnapLookup1.Cnap = Nothing
        Me.Xl_CnapLookup1.IsDirty = False
        Me.Xl_CnapLookup1.Location = New System.Drawing.Point(64, 119)
        Me.Xl_CnapLookup1.Name = "Xl_CnapLookup1"
        Me.Xl_CnapLookup1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_CnapLookup1.Size = New System.Drawing.Size(310, 20)
        Me.Xl_CnapLookup1.TabIndex = 107
        Me.Xl_CnapLookup1.Value = Nothing
        '
        'Xl_DropdownList_Ivas1
        '
        Me.Xl_DropdownList_Ivas1.IvaCod = DTO.DTOTax.Codis.Iva_Standard
        Me.Xl_DropdownList_Ivas1.Location = New System.Drawing.Point(613, 406)
        Me.Xl_DropdownList_Ivas1.Name = "Xl_DropdownList_Ivas1"
        Me.Xl_DropdownList_Ivas1.Size = New System.Drawing.Size(114, 21)
        Me.Xl_DropdownList_Ivas1.TabIndex = 102
        '
        'Xl_EAN1
        '
        Me.Xl_EAN1.Location = New System.Drawing.Point(444, 405)
        Me.Xl_EAN1.Name = "Xl_EAN1"
        Me.Xl_EAN1.Size = New System.Drawing.Size(117, 20)
        Me.Xl_EAN1.TabIndex = 58
        '
        'Xl_Image1
        '
        Me.Xl_Image1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_Image1.Bitmap = Nothing
        Me.Xl_Image1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Xl_Image1.EmptyImageLabelText = ""
        Me.Xl_Image1.IsDirty = False
        Me.Xl_Image1.Location = New System.Drawing.Point(379, 0)
        Me.Xl_Image1.Margin = New System.Windows.Forms.Padding(2, 3, 0, 3)
        Me.Xl_Image1.MaxHeight = 0
        Me.Xl_Image1.MaxWidth = 0
        Me.Xl_Image1.Name = "Xl_Image1"
        Me.Xl_Image1.Size = New System.Drawing.Size(350, 400)
        Me.Xl_Image1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.Xl_Image1.TabIndex = 49
        Me.Xl_Image1.ZipStream = Nothing
        '
        'CheckBoxNoPro
        '
        Me.CheckBoxNoPro.Location = New System.Drawing.Point(281, 388)
        Me.CheckBoxNoPro.Margin = New System.Windows.Forms.Padding(3, 3, 1, 3)
        Me.CheckBoxNoPro.Name = "CheckBoxNoPro"
        Me.CheckBoxNoPro.Size = New System.Drawing.Size(80, 16)
        Me.CheckBoxNoPro.TabIndex = 126
        Me.CheckBoxNoPro.Text = "No Pro"
        '
        'ButtonTarifaCost
        '
        Me.ButtonTarifaCost.Location = New System.Drawing.Point(222, 305)
        Me.ButtonTarifaCost.Name = "ButtonTarifaCost"
        Me.ButtonTarifaCost.Size = New System.Drawing.Size(28, 20)
        Me.ButtonTarifaCost.TabIndex = 128
        Me.ButtonTarifaCost.Text = "..."
        Me.ButtonTarifaCost.UseVisualStyleBackColor = True
        '
        'Xl_LookupProduct1
        '
        Me.Xl_LookupProduct1.IsDirty = False
        Me.Xl_LookupProduct1.Location = New System.Drawing.Point(64, 5)
        Me.Xl_LookupProduct1.Name = "Xl_LookupProduct1"
        Me.Xl_LookupProduct1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupProduct1.Product = Nothing
        Me.Xl_LookupProduct1.Size = New System.Drawing.Size(310, 20)
        Me.Xl_LookupProduct1.TabIndex = 129
        Me.Xl_LookupProduct1.Value = Nothing
        '
        'Xl_Art_Edit
        '
        Me.Controls.Add(Me.Xl_LookupProduct1)
        Me.Controls.Add(Me.ButtonTarifaCost)
        Me.Controls.Add(Me.CheckBoxNoPro)
        Me.Controls.Add(Me.Xl_PercentCostDto)
        Me.Controls.Add(Me.Xl_PercentRetail)
        Me.Controls.Add(Me.Xl_AmtCurRetail)
        Me.Controls.Add(Me.Xl_AmtCurCost)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.LabelCost)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxCodiProveedor)
        Me.Controls.Add(Me.CheckBoxVirtual)
        Me.Controls.Add(Me.ButtonTarifesCustom)
        Me.Controls.Add(Me.LabelTarifesCustom)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.CheckBoxNoTarifa)
        Me.Controls.Add(Me.Xl_CnapLookup1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Xl_DropdownList_Ivas1)
        Me.Controls.Add(Me.CheckBoxTransportMobles)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxNomPrv)
        Me.Controls.Add(Me.Xl_EAN1)
        Me.Controls.Add(Me.CheckBoxNoWeb)
        Me.Controls.Add(Me.CheckBoxNoStk)
        Me.Controls.Add(Me.CheckBoxObsoleto)
        Me.Controls.Add(Me.TextBoxKeys)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Xl_Image1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxNom)
        Me.Controls.Add(Me.TextBoxNomCurt)
        Me.Controls.Add(Me.PictureBoxThumbnail)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CheckBoxLastProduction)
        Me.Controls.Add(Me.CheckBoxNoMgz)
        Me.Name = "Xl_Art_Edit"
        Me.Size = New System.Drawing.Size(730, 461)
        CType(Me.PictureBoxThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private mArt As Art
    Private mAllowEvents As Boolean

    Public Event AfterUpdate()
    Public Event AfterImageUpdate()
    Public Event ForzarInnerPackChanged(ByVal BlForzar As Boolean, ByVal iInnerPack As Integer)
    Public Event DscInheritanceChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Art() As Art
        Get
            If mArt IsNot Nothing Then
                With mArt
                    .NomCurt = TextBoxNomCurt.Text
                    .Nom_ESP = TextBoxNom.Text
                    .NomPrv = TextBoxNomPrv.Text
                    .CodPrv = TextBoxCodiProveedor.Text
                    .Virtual = CheckBoxVirtual.Checked
                    .Cnap = Xl_CnapLookup1.Cnap
                    If CheckBoxDscInheritFromParent.Checked Then
                        .Hereda = True
                        .Dsc_ESP = ""
                        .Dsc_CAT = ""
                        .Dsc_ENG = ""
                    Else
                        .Hereda = False
                        .Dsc_ESP = TextBoxDscESP.Text
                        .Dsc_CAT = TextBoxDscCAT.Text
                        .Dsc_ENG = TextBoxDscENG.Text
                    End If
                    .Cbar = New MaxiSrvr.Ean13(Xl_EAN1.Text)
                    .LastProduction = CheckBoxLastProduction.Checked
                    .Obsoleto = CheckBoxObsoleto.Checked
                    .NoStk = CheckBoxNoStk.Checked
                    .NoMgz = CheckBoxNoMgz.Checked
                    .NoWeb = CheckBoxNoWeb.Checked
                    .NoPro = CheckBoxNoPro.Checked
                    .NoTarifa = CheckBoxNoTarifa.Checked
                    .TransportMobles = CheckBoxTransportMobles.Checked
                    .IVAcod = Xl_DropdownList_Ivas1.IvaCod ' IIf(CheckBoxIVAred.Checked, Art.IVAcods.reduit, Art.IVAcods.standard)
                    .Keys = New ArrayList
                    For Each sKey As String In MaxiSrvr.GetArrayListFromSplitCharSeparatedString(TextBoxKeys.Text)
                        .Keys.Add(sKey)
                    Next
                End With
            End If
            Return mArt
        End Get
        Set(ByVal Value As Art)
            If Not Value Is Nothing Then
                mArt = Value
                With mArt
                    Dim oCategory As DTOProductCategory = BLL.BLLProductCategory.Find(.Stp.Guid)
                    Xl_LookupProduct1.Load(oCategory, Frm_Products.SelModes.SelectProductCategory)
                    CheckBoxVirtual.Checked = .Virtual

                    Xl_Image1.Bitmap = .Image
                    Xl_Image1.Title = .FullNom
                    TextBoxNomCurt.Text = .NomCurt
                    TextBoxNom.Text = .Nom_ESP
                    TextBoxNomPrv.Text = .NomPrv
                    TextBoxCodiProveedor.Text = .CodPrv

                    Xl_CnapLookup1.Cnap = .Cnap

                    If .Hereda Then
                        CheckBoxDscInheritFromParent.Checked = True
                        TextBoxDscESP.Text = mArt.Stp.Dsc_ESP
                        TextBoxDscCAT.Text = mArt.Stp.Dsc_CAT
                        TextBoxDscENG.Text = mArt.Stp.Dsc_ENG
                        TextBoxDscESP.Enabled = False
                        TextBoxDscCAT.Enabled = False
                        TextBoxDscENG.Enabled = False
                    Else
                        CheckBoxDscInheritFromParent.Checked = False
                        TextBoxDscESP.Text = mArt.Dsc_ESP
                        TextBoxDscCAT.Text = mArt.Dsc_CAT
                        TextBoxDscENG.Text = mArt.Dsc_ENG
                    End If

                    Xl_EAN1.Text = .Cbar.Value
                    RefreshTarifa()
                    Select Case BLL.BLLSession.Current.User.Rol.Id
                        Case Rol.Ids.SuperUser, Rol.Ids.Admin
                        Case Else
                            LabelCost.Visible = False
                            Xl_AmtCurCost.Visible = False
                            Xl_PercentCostDto.Visible = False
                    End Select

                    SetHereda()
                    CheckBoxLastProduction.Checked = .LastProduction
                    CheckBoxObsoleto.Checked = .Obsoleto
                    CheckBoxNoStk.Checked = .NoStk
                    CheckBoxNoMgz.Checked = .NoMgz
                    CheckBoxNoWeb.Checked = .NoWeb
                    CheckBoxNoPro.Checked = .NoPro
                    CheckBoxNoTarifa.Checked = .NoTarifa
                    CheckBoxTransportMobles.Checked = .TransportMobles
                    Xl_DropdownList_Ivas1.IvaCod = .IVAcod
                    TextBoxKeys.Text = MaxiSrvr.GetSplitCharSeparatedStringFromArrayList(.Keys)
                End With
                UpdateTarifasCustom()

                mAllowEvents = True
            End If
        End Set
    End Property

    Public Sub SetHereda()
    End Sub


    Private Sub Xl_Image1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Image1.AfterUpdate
        If Xl_Image1.Bitmap Is Nothing Then
            mArt.DeleteImage()
        Else
            mArt.UpdateBigImg(Xl_Image1.Bitmap)
        End If
        RaiseEvent AfterImageUpdate()
    End Sub

    Private Sub TextBoxNomCurt_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxNomCurt.KeyPress
        Select Case e.KeyChar
            Case "&", ":", "/", "?", "'", "."
                e.KeyChar = "-"
        End Select
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxNom.TextChanged, TextBoxNomCurt.TextChanged, TextBoxNomPrv.TextChanged, TextBoxCodiProveedor.TextChanged, TextBoxDscESP.TextChanged, TextBoxDscCAT.TextChanged, TextBoxDscENG.TextChanged, CheckBoxLastProduction.CheckedChanged, CheckBoxObsoleto.CheckedChanged, CheckBoxNoStk.CheckedChanged, CheckBoxNoMgz.CheckedChanged, CheckBoxNoWeb.CheckedChanged, CheckBoxNoPro.CheckedChanged, CheckBoxVirtual.CheckedChanged, CheckBoxNoTarifa.CheckedChanged, CheckBoxTransportMobles.CheckedChanged, TextBoxKeys.TextChanged, TextBoxDscCAT.TextChanged

        If mAllowEvents Then
            SetDirty()
        End If
    End Sub

    Private Sub CheckBoxHeredaDsc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxDscInheritFromParent.CheckedChanged
        If mAllowEvents Then
            If CheckBoxDscInheritFromParent.Checked Then
                TextBoxDscESP.Text = mArt.Stp.Dsc_ESP
                TextBoxDscCAT.Text = mArt.Stp.Dsc_CAT
                TextBoxDscENG.Text = mArt.Stp.Dsc_ENG
                TextBoxDscESP.Enabled = False
                TextBoxDscCAT.Enabled = False
                TextBoxDscENG.Enabled = False
            Else
                TextBoxDscESP.Enabled = True
                TextBoxDscCAT.Enabled = True
                TextBoxDscENG.Enabled = True
            End If

            SetDirty()
            RaiseEvent DscInheritanceChanged(TextBoxDscESP.Text, EventArgs.Empty)
        End If
    End Sub

    Private Sub ControlAfterUpdateValue(ByVal oAmt As MaxiSrvr.Amt)
        SetDirty()
    End Sub

    Private Sub ControlAfterUpdateCur(ByVal oCur As MaxiSrvr.Cur)
        SetDirty()
    End Sub

    Private Sub SetDirty()
        If mAllowEvents Then
            RaiseEvent AfterUpdate()
        End If
    End Sub

    Private Sub Xl_EAN1_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_EAN1.Changed
        SetDirty()
    End Sub


    Private Sub Xl_DropdownList_Ivas1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_DropdownList_Ivas1.AfterUpdate
        SetDirty()
    End Sub

    Private Sub ButtonTarifesCustom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonTarifesCustom.Click
        Dim oFrm As New Frm_CliPreusXProduct(New Product(mArt))
        AddHandler oFrm.AfterUpdate, AddressOf UpdateTarifasCustom
        oFrm.Show()
    End Sub

    Private Sub UpdateTarifasCustom()
        Dim s As String = ""
        Dim iCount As Integer = mArt.Product.TarifesCustomCount
        Select Case iCount
            Case 0
                s = "(no hi han clients amb tarifa custom)"
            Case 1
                s = "1 client amb tarifa custom"
            Case Else
                s = iCount.ToString & " clients amb tarifa custom"
        End Select

        LabelTarifesCustom.Text = s
    End Sub


    Private Sub onTarifasCostUpdated()
        Dim oItem As DTOPricelistItemCustomer = mArt.DTOPricelistItemCustomer()
        If oItem IsNot Nothing Then
            With mArt
                '.TarifaA = New Amt(oItem.TarifaA.Eur)
                '.TarifaB = New Amt(oItem.TarifaB.Eur)
                .Pvp = New Amt(oItem.Retail.Eur)
            End With
        End If

        RefreshTarifa()
    End Sub

    Private Sub onTarifasVendaUpdated()
        Dim oItem As DTOPricelistItemCustomer = mArt.DTOPricelistItemCustomer()
        If oItem IsNot Nothing Then
            With mArt
                '.TarifaA = New Amt(oItem.TarifaA.Eur)
                '.TarifaB = New Amt(oItem.TarifaB.Eur)
                .Pvp = New Amt(oItem.Retail.Eur)
            End With
        End If

        RefreshTarifa()
    End Sub

    Private Sub RefreshTarifa()
        With mArt
            Xl_AmtCurCost.Amt = .Cost
            Xl_PercentCostDto.Value = .CostDto_OnInvoice
            'Xl_AmtCurTarifaA.Amt = .TarifaA
            'Xl_AmtCurTarifaB.Amt = .TarifaB
            Xl_AmtCurRetail.Amt = .Pvp

            Dim DcCostDtoEur As Decimal
            Dim DcCostNet As Decimal

            If .Cost IsNot Nothing Then
                DcCostDtoEur = Math.Round(.Cost.Eur * (.CostDto_OnInvoice + .CostDto_OffInvoice) / 100, 2)
                DcCostNet = .Cost.Eur - DcCostDtoEur
            End If
        End With
    End Sub

    Private Sub ButtonTarifaVenda_Click(sender As Object, e As EventArgs)
        Dim oDTOPricelistItemCustomer As DTOPricelistItemCustomer = mArt.DTOPricelistItemCustomer()
        Dim oFrm As New Frm_PricelistItemCustomer(oDTOPricelistItemCustomer)
        AddHandler oFrm.AfterUpdate, AddressOf onTarifasVendaUpdated
        oFrm.Show()
    End Sub

    Private Sub ButtonTarifaCost_Click(sender As Object, e As EventArgs) Handles ButtonTarifaCost.Click
        Dim oPriceListItem_Supplier As DTOPriceListItem_Supplier = mArt.PriceListItem_Supplier
        Dim oFrm As New Frm_PriceListItem_Supplier(oPriceListItem_Supplier)
        AddHandler oFrm.AfterUpdate, AddressOf onTarifasCostUpdated
        oFrm.Show()
    End Sub

    Private Sub Xl_AmtCurRetail_MouseDown(sender As Object, e As MouseEventArgs) Handles Xl_AmtCurRetail.MouseDown
        MsgBox("Xl_AmtCurRetail_MouseDown event")
    End Sub


    Private Sub Xl_LookupProduct1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupProduct1.AfterUpdate
        If mAllowEvents Then
            Dim oCategory As DTOProductCategory = e.Argument
            mArt.Stp = New Stp(oCategory.Guid)
            RaiseEvent AfterUpdate()
        End If
    End Sub
End Class
