<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Xl_Art_Logistics
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.GroupBoxCodiMercancia = New System.Windows.Forms.GroupBox()
        Me.Xl_LookupCodiMercancia1 = New Xl_LookupCodiMercancia()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CheckBoxHeredaCodiMercancia = New System.Windows.Forms.CheckBox()
        Me.GroupBoxDimensions = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumKgNet = New Xl_TextBoxNum()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumOuterPack = New Xl_TextBoxNum()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Xl_Ean131 = New Xl_Ean13()
        Me.Xl_TextBoxNumInnerPack = New Xl_TextBoxNum()
        Me.ButtonFraccionarTemporalment = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CheckBoxForzarInnerPack = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.PictureBoxWarn = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumKgBrut = New Xl_TextBoxNum()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumDimL = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumDimW = New Xl_TextBoxNum()
        Me.Xl_TextBoxNumDimH = New Xl_TextBoxNum()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Xl_TextBoxNumM3 = New Xl_TextBoxNum()
        Me.CheckBoxHeredaDimensions = New System.Windows.Forms.CheckBox()
        Me.CheckBoxNoDimensions = New System.Windows.Forms.CheckBox()
        Me.Xl_LookupCountryMadeIn = New Xl_LookupCountry()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBoxCodiMercancia.SuspendLayout()
        Me.GroupBoxDimensions.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBoxCodiMercancia
        '
        Me.GroupBoxCodiMercancia.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxCodiMercancia.Controls.Add(Me.Xl_LookupCodiMercancia1)
        Me.GroupBoxCodiMercancia.Controls.Add(Me.Label10)
        Me.GroupBoxCodiMercancia.Controls.Add(Me.CheckBoxHeredaCodiMercancia)
        Me.GroupBoxCodiMercancia.Location = New System.Drawing.Point(6, 27)
        Me.GroupBoxCodiMercancia.Name = "GroupBoxCodiMercancia"
        Me.GroupBoxCodiMercancia.Size = New System.Drawing.Size(581, 63)
        Me.GroupBoxCodiMercancia.TabIndex = 1
        Me.GroupBoxCodiMercancia.TabStop = False
        Me.GroupBoxCodiMercancia.Text = "Codi mercancia"
        '
        'Xl_LookupCodiMercancia1
        '
        Me.Xl_LookupCodiMercancia1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Xl_LookupCodiMercancia1.CodiMercancia = Nothing
        Me.Xl_LookupCodiMercancia1.IsDirty = False
        Me.Xl_LookupCodiMercancia1.Location = New System.Drawing.Point(68, 32)
        Me.Xl_LookupCodiMercancia1.Name = "Xl_LookupCodiMercancia1"
        Me.Xl_LookupCodiMercancia1.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCodiMercancia1.ReadOnlyLookup = False
        Me.Xl_LookupCodiMercancia1.Size = New System.Drawing.Size(502, 20)
        Me.Xl_LookupCodiMercancia1.TabIndex = 6
        Me.Xl_LookupCodiMercancia1.Value = Nothing
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 35)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(56, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "arancelari:"
        '
        'CheckBoxHeredaCodiMercancia
        '
        Me.CheckBoxHeredaCodiMercancia.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxHeredaCodiMercancia.AutoSize = True
        Me.CheckBoxHeredaCodiMercancia.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxHeredaCodiMercancia.Location = New System.Drawing.Point(510, 10)
        Me.CheckBoxHeredaCodiMercancia.Name = "CheckBoxHeredaCodiMercancia"
        Me.CheckBoxHeredaCodiMercancia.Size = New System.Drawing.Size(61, 17)
        Me.CheckBoxHeredaCodiMercancia.TabIndex = 1
        Me.CheckBoxHeredaCodiMercancia.Text = "Hereda"
        Me.CheckBoxHeredaCodiMercancia.UseVisualStyleBackColor = True
        '
        'GroupBoxDimensions
        '
        Me.GroupBoxDimensions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxDimensions.Controls.Add(Me.GroupBox2)
        Me.GroupBoxDimensions.Controls.Add(Me.GroupBox1)
        Me.GroupBoxDimensions.Controls.Add(Me.CheckBoxHeredaDimensions)
        Me.GroupBoxDimensions.Location = New System.Drawing.Point(6, 97)
        Me.GroupBoxDimensions.Name = "GroupBoxDimensions"
        Me.GroupBoxDimensions.Size = New System.Drawing.Size(581, 285)
        Me.GroupBoxDimensions.TabIndex = 2
        Me.GroupBoxDimensions.TabStop = False
        Me.GroupBoxDimensions.Text = "Dimensions i packaging"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Xl_LookupCountryMadeIn)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Xl_TextBoxNumKgNet)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Xl_TextBoxNumOuterPack)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 36)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(281, 243)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Producte"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(129, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 13)
        Me.Label4.TabIndex = 27
        Me.Label4.Text = "(per comandes a proveidor)"
        '
        'Xl_TextBoxNumKgNet
        '
        Me.Xl_TextBoxNumKgNet.Location = New System.Drawing.Point(86, 27)
        Me.Xl_TextBoxNumKgNet.Mat_CustomFormat = Xl_TextBoxNum.Formats.Kg
        Me.Xl_TextBoxNumKgNet.Mat_FormatString = ""
        Me.Xl_TextBoxNumKgNet.Name = "Xl_TextBoxNumKgNet"
        Me.Xl_TextBoxNumKgNet.ReadOnly = False
        Me.Xl_TextBoxNumKgNet.Size = New System.Drawing.Size(64, 20)
        Me.Xl_TextBoxNumKgNet.TabIndex = 4
        Me.Xl_TextBoxNumKgNet.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Pes net:"
        '
        'Xl_TextBoxNumOuterPack
        '
        Me.Xl_TextBoxNumOuterPack.Location = New System.Drawing.Point(86, 53)
        Me.Xl_TextBoxNumOuterPack.Mat_CustomFormat = Xl_TextBoxNum.Formats.Numeric
        Me.Xl_TextBoxNumOuterPack.Mat_FormatString = ""
        Me.Xl_TextBoxNumOuterPack.Name = "Xl_TextBoxNumOuterPack"
        Me.Xl_TextBoxNumOuterPack.ReadOnly = False
        Me.Xl_TextBoxNumOuterPack.Size = New System.Drawing.Size(41, 20)
        Me.Xl_TextBoxNumOuterPack.TabIndex = 5
        Me.Xl_TextBoxNumOuterPack.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(30, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(56, 13)
        Me.Label9.TabIndex = 24
        Me.Label9.Text = "Unitats/lot"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Xl_Ean131)
        Me.GroupBox1.Controls.Add(Me.Xl_TextBoxNumInnerPack)
        Me.GroupBox1.Controls.Add(Me.ButtonFraccionarTemporalment)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.CheckBoxForzarInnerPack)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.PictureBoxWarn)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Xl_TextBoxNumKgBrut)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Xl_TextBoxNumDimL)
        Me.GroupBox1.Controls.Add(Me.Xl_TextBoxNumDimW)
        Me.GroupBox1.Controls.Add(Me.Xl_TextBoxNumDimH)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Xl_TextBoxNumM3)
        Me.GroupBox1.Location = New System.Drawing.Point(331, 36)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 243)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Embalatge"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 89)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(26, 13)
        Me.Label11.TabIndex = 25
        Me.Label11.Text = "Ean"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Xl_Ean131
        '
        Me.Xl_Ean131.Ean = Nothing
        Me.Xl_Ean131.Location = New System.Drawing.Point(55, 86)
        Me.Xl_Ean131.Name = "Xl_Ean131"
        Me.Xl_Ean131.Size = New System.Drawing.Size(86, 20)
        Me.Xl_Ean131.TabIndex = 24
        '
        'Xl_TextBoxNumInnerPack
        '
        Me.Xl_TextBoxNumInnerPack.Location = New System.Drawing.Point(55, 24)
        Me.Xl_TextBoxNumInnerPack.Mat_CustomFormat = Xl_TextBoxNum.Formats.Numeric
        Me.Xl_TextBoxNumInnerPack.Mat_FormatString = ""
        Me.Xl_TextBoxNumInnerPack.Name = "Xl_TextBoxNumInnerPack"
        Me.Xl_TextBoxNumInnerPack.ReadOnly = False
        Me.Xl_TextBoxNumInnerPack.Size = New System.Drawing.Size(31, 20)
        Me.Xl_TextBoxNumInnerPack.TabIndex = 6
        Me.Xl_TextBoxNumInnerPack.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ButtonFraccionarTemporalment
        '
        Me.ButtonFraccionarTemporalment.Enabled = False
        Me.ButtonFraccionarTemporalment.Location = New System.Drawing.Point(165, 24)
        Me.ButtonFraccionarTemporalment.Name = "ButtonFraccionarTemporalment"
        Me.ButtonFraccionarTemporalment.Size = New System.Drawing.Size(68, 43)
        Me.ButtonFraccionarTemporalment.TabIndex = 11
        Me.ButtonFraccionarTemporalment.Text = "fraccionar 2 minuts"
        Me.ButtonFraccionarTemporalment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.ButtonFraccionarTemporalment.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(13, 28)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "unitats"
        '
        'CheckBoxForzarInnerPack
        '
        Me.CheckBoxForzarInnerPack.AutoSize = True
        Me.CheckBoxForzarInnerPack.Enabled = False
        Me.CheckBoxForzarInnerPack.Location = New System.Drawing.Point(55, 50)
        Me.CheckBoxForzarInnerPack.Name = "CheckBoxForzarInnerPack"
        Me.CheckBoxForzarInnerPack.Size = New System.Drawing.Size(104, 17)
        Me.CheckBoxForzarInnerPack.TabIndex = 7
        Me.CheckBoxForzarInnerPack.Text = "no fraccionables"
        Me.CheckBoxForzarInnerPack.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 169)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "alçada"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PictureBoxWarn
        '
        Me.PictureBoxWarn.Image = Global.Mat.Net.My.Resources.Resources.warn
        Me.PictureBoxWarn.Location = New System.Drawing.Point(125, 190)
        Me.PictureBoxWarn.Name = "PictureBoxWarn"
        Me.PictureBoxWarn.Size = New System.Drawing.Size(16, 16)
        Me.PictureBoxWarn.TabIndex = 18
        Me.PictureBoxWarn.TabStop = False
        Me.PictureBoxWarn.Tag = "el volum no quadra. Doble clic per recalcular"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 143)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "amplada"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Xl_TextBoxNumKgBrut
        '
        Me.Xl_TextBoxNumKgBrut.Location = New System.Drawing.Point(55, 215)
        Me.Xl_TextBoxNumKgBrut.Mat_CustomFormat = Xl_TextBoxNum.Formats.Kg
        Me.Xl_TextBoxNumKgBrut.Mat_FormatString = ""
        Me.Xl_TextBoxNumKgBrut.Name = "Xl_TextBoxNumKgBrut"
        Me.Xl_TextBoxNumKgBrut.ReadOnly = False
        Me.Xl_TextBoxNumKgBrut.Size = New System.Drawing.Size(64, 20)
        Me.Xl_TextBoxNumKgBrut.TabIndex = 12
        Me.Xl_TextBoxNumKgBrut.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 117)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "longitud"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 219)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Pes brut:"
        '
        'Xl_TextBoxNumDimL
        '
        Me.Xl_TextBoxNumDimL.Location = New System.Drawing.Point(55, 112)
        Me.Xl_TextBoxNumDimL.Mat_CustomFormat = Xl_TextBoxNum.Formats.mm
        Me.Xl_TextBoxNumDimL.Mat_FormatString = ""
        Me.Xl_TextBoxNumDimL.Name = "Xl_TextBoxNumDimL"
        Me.Xl_TextBoxNumDimL.ReadOnly = False
        Me.Xl_TextBoxNumDimL.Size = New System.Drawing.Size(64, 20)
        Me.Xl_TextBoxNumDimL.TabIndex = 8
        Me.Xl_TextBoxNumDimL.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumDimW
        '
        Me.Xl_TextBoxNumDimW.Location = New System.Drawing.Point(55, 138)
        Me.Xl_TextBoxNumDimW.Mat_CustomFormat = Xl_TextBoxNum.Formats.mm
        Me.Xl_TextBoxNumDimW.Mat_FormatString = ""
        Me.Xl_TextBoxNumDimW.Name = "Xl_TextBoxNumDimW"
        Me.Xl_TextBoxNumDimW.ReadOnly = False
        Me.Xl_TextBoxNumDimW.Size = New System.Drawing.Size(64, 20)
        Me.Xl_TextBoxNumDimW.TabIndex = 9
        Me.Xl_TextBoxNumDimW.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Xl_TextBoxNumDimH
        '
        Me.Xl_TextBoxNumDimH.Location = New System.Drawing.Point(55, 164)
        Me.Xl_TextBoxNumDimH.Mat_CustomFormat = Xl_TextBoxNum.Formats.mm
        Me.Xl_TextBoxNumDimH.Mat_FormatString = ""
        Me.Xl_TextBoxNumDimH.Name = "Xl_TextBoxNumDimH"
        Me.Xl_TextBoxNumDimH.ReadOnly = False
        Me.Xl_TextBoxNumDimH.Size = New System.Drawing.Size(64, 20)
        Me.Xl_TextBoxNumDimH.TabIndex = 10
        Me.Xl_TextBoxNumDimH.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 193)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "volum:"
        '
        'Xl_TextBoxNumM3
        '
        Me.Xl_TextBoxNumM3.Location = New System.Drawing.Point(55, 188)
        Me.Xl_TextBoxNumM3.Mat_CustomFormat = Xl_TextBoxNum.Formats.M3
        Me.Xl_TextBoxNumM3.Mat_FormatString = ""
        Me.Xl_TextBoxNumM3.Name = "Xl_TextBoxNumM3"
        Me.Xl_TextBoxNumM3.ReadOnly = False
        Me.Xl_TextBoxNumM3.Size = New System.Drawing.Size(64, 20)
        Me.Xl_TextBoxNumM3.TabIndex = 11
        Me.Xl_TextBoxNumM3.Value = New Decimal(New Integer() {0, 0, 0, 0})
        '
        'CheckBoxHeredaDimensions
        '
        Me.CheckBoxHeredaDimensions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxHeredaDimensions.AutoSize = True
        Me.CheckBoxHeredaDimensions.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckBoxHeredaDimensions.Location = New System.Drawing.Point(510, 13)
        Me.CheckBoxHeredaDimensions.Name = "CheckBoxHeredaDimensions"
        Me.CheckBoxHeredaDimensions.Size = New System.Drawing.Size(61, 17)
        Me.CheckBoxHeredaDimensions.TabIndex = 3
        Me.CheckBoxHeredaDimensions.Text = "Hereda"
        Me.CheckBoxHeredaDimensions.UseVisualStyleBackColor = True
        '
        'CheckBoxNoDimensions
        '
        Me.CheckBoxNoDimensions.AutoSize = True
        Me.CheckBoxNoDimensions.Location = New System.Drawing.Point(15, 3)
        Me.CheckBoxNoDimensions.Name = "CheckBoxNoDimensions"
        Me.CheckBoxNoDimensions.Size = New System.Drawing.Size(85, 17)
        Me.CheckBoxNoDimensions.TabIndex = 0
        Me.CheckBoxNoDimensions.Text = "No aplicable"
        Me.CheckBoxNoDimensions.UseVisualStyleBackColor = True
        '
        'Xl_LookupCountryMadeIn
        '
        Me.Xl_LookupCountryMadeIn.Country = Nothing
        Me.Xl_LookupCountryMadeIn.IsDirty = False
        Me.Xl_LookupCountryMadeIn.Location = New System.Drawing.Point(86, 79)
        Me.Xl_LookupCountryMadeIn.Name = "Xl_LookupCountryMadeIn"
        Me.Xl_LookupCountryMadeIn.PasswordChar = "" & Global.Microsoft.VisualBasic.ChrW(0)
        Me.Xl_LookupCountryMadeIn.ReadOnlyLookup = False
        Me.Xl_LookupCountryMadeIn.Size = New System.Drawing.Size(183, 20)
        Me.Xl_LookupCountryMadeIn.TabIndex = 133
        Me.Xl_LookupCountryMadeIn.Value = Nothing
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(30, 80)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(58, 16)
        Me.Label15.TabIndex = 132
        Me.Label15.Text = "Made In:"
        '
        'Xl_Art_Logistics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxNoDimensions)
        Me.Controls.Add(Me.GroupBoxDimensions)
        Me.Controls.Add(Me.GroupBoxCodiMercancia)
        Me.Name = "Xl_Art_Logistics"
        Me.Size = New System.Drawing.Size(596, 385)
        Me.GroupBoxCodiMercancia.ResumeLayout(False)
        Me.GroupBoxCodiMercancia.PerformLayout()
        Me.GroupBoxDimensions.ResumeLayout(False)
        Me.GroupBoxDimensions.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.PictureBoxWarn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBoxCodiMercancia As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxHeredaCodiMercancia As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxDimensions As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxHeredaDimensions As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumDimH As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumM3 As Xl_TextBoxNum
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumKgBrut As Xl_TextBoxNum
    Friend WithEvents PictureBoxWarn As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumDimL As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumDimW As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumInnerPack As Xl_TextBoxNum
    Friend WithEvents Xl_TextBoxNumOuterPack As Xl_TextBoxNum
    Friend WithEvents ButtonFraccionarTemporalment As System.Windows.Forms.Button
    Friend WithEvents CheckBoxForzarInnerPack As System.Windows.Forms.CheckBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CheckBoxNoDimensions As System.Windows.Forms.CheckBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Xl_TextBoxNumKgNet As Xl_TextBoxNum
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Xl_Ean131 As Xl_Ean13
    Friend WithEvents Xl_LookupCodiMercancia1 As Xl_LookupCodiMercancia
    Friend WithEvents Xl_LookupCountryMadeIn As Xl_LookupCountry
    Friend WithEvents Label15 As Label
End Class
