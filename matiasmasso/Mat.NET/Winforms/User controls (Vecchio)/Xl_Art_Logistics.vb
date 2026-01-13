

Public Class Xl_Art_Logistics
    Private mTarget As Object
    Private mDimensions As DTOProductDimensions
    Private mCnap As DTOCnap
    Private mCodiMercancia As DTOCodiMercancia
    Private mParent As DTOProductDimensions
    Private mIsDirty As Boolean = False
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Target() As Object
        Set(ByVal value As Object)
            mTarget = value
            If TypeOf (mTarget) Is DTOProductSku Then
                CheckBoxHeredaDimensions.Checked = DirectCast(mTarget, DTOProductSku).HeredaDimensions
                CheckBoxHeredaCodiMercancia.Checked = DirectCast(mTarget, DTOProductSku).CodiMercancia IsNot Nothing
            ElseIf TypeOf (mTarget) Is DTOProductCategory Then
                CheckBoxHeredaDimensions.Visible = False
            Else
                Exit Property
            End If
            refresca()
            mAllowEvents = True
        End Set
    End Property

    Private Sub refresca()
        Dim exs As New List(Of Exception)
        mAllowEvents = False
        If TypeOf (mTarget) Is DTOProductSku Then
            Dim oSku = DirectCast(mTarget, DTOProductSku)


            If CheckBoxHeredaDimensions.Checked Then
                If FEB2.ProductCategory.Load(oSku.Category, exs) Then
                    With oSku.Category
                        If .NoDimensions Then
                            CheckBoxNoDimensions.Checked = True
                            ResetDimensions()
                        Else
                            CheckBoxNoDimensions.Checked = False
                            Xl_TextBoxNumKgNet.Value = .KgNet
                            Xl_TextBoxNumKgBrut.Value = .KgBrut
                            Xl_TextBoxNumM3.Value = .VolumeM3
                            Xl_TextBoxNumDimL.Value = .DimensionL
                            Xl_TextBoxNumDimW.Value = .DimensionW
                            Xl_TextBoxNumDimH.Value = .DimensionH
                            Xl_TextBoxNumInnerPack.Value = .InnerPack
                            Xl_TextBoxNumOuterPack.Value = .OuterPack
                            CheckBoxForzarInnerPack.Checked = .ForzarInnerPack
                            Xl_Ean131.Ean = .PackageEan
                        End If
                    End With
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                With oSku
                    If .NoDimensions Then
                        CheckBoxNoDimensions.Checked = True
                        ResetDimensions()
                    Else
                        CheckBoxNoDimensions.Checked = False
                        Xl_TextBoxNumKgNet.Value = .KgNet
                        Xl_TextBoxNumKgBrut.Value = .KgBrut
                        Xl_TextBoxNumM3.Value = .VolumeM3
                        Xl_TextBoxNumDimL.Value = .DimensionL
                        Xl_TextBoxNumDimW.Value = .DimensionW
                        Xl_TextBoxNumDimH.Value = .DimensionH
                        Xl_TextBoxNumInnerPack.Value = .InnerPack
                        Xl_TextBoxNumOuterPack.Value = .OuterPack
                        CheckBoxForzarInnerPack.Checked = .ForzarInnerPack
                        Xl_Ean131.Ean = .PackageEan
                    End If
                End With
            End If

            CheckBoxHeredaCodiMercancia.Checked = mTarget.CodiMercancia Is Nothing
            Xl_LookupCodiMercancia1.CodiMercancia = DTOProduct.CodiMercancia(mTarget)
            Xl_LookupCodiMercancia1.ReadOnlyLookup = CheckBoxHeredaCodiMercancia.Checked
            CheckWarnM3()

            ButtonFraccionarTemporalment.Visible = True

        ElseIf TypeOf (mTarget) Is DTOProductCategory Then
            Dim oCategory As DTOProductCategory = mTarget
            With oCategory
                If .NoDimensions Then
                    CheckBoxNoDimensions.Checked = True
                    ResetDimensions()
                Else
                    CheckBoxNoDimensions.Checked = False
                    Xl_TextBoxNumKgNet.Value = .KgNet
                    Xl_TextBoxNumKgBrut.Value = .KgBrut
                    Xl_TextBoxNumM3.Value = .VolumeM3
                    Xl_TextBoxNumDimL.Value = .DimensionL
                    Xl_TextBoxNumDimW.Value = .DimensionW
                    Xl_TextBoxNumDimH.Value = .DimensionH
                    Xl_TextBoxNumInnerPack.Value = .InnerPack
                    Xl_TextBoxNumOuterPack.Value = .OuterPack
                    CheckBoxForzarInnerPack.Checked = .ForzarInnerPack
                    Xl_Ean131.Ean = .PackageEan
                End If
            End With

            CheckBoxHeredaCodiMercancia.Checked = mTarget.CodiMercancia Is Nothing
            Xl_LookupCodiMercancia1.ReadOnlyLookup = CheckBoxHeredaCodiMercancia.Checked
            Xl_LookupCodiMercancia1.CodiMercancia = DTOProduct.CodiMercancia(mTarget)
            CheckWarnM3()

        Else
            mAllowEvents = True
            Exit Sub
        End If

        EnableDimensions()
        EnableFraccionarPackaging()
        mAllowEvents = True
    End Sub

    Public ReadOnly Property PackageEan() As DTOEan
        Get
            Return Xl_Ean131.Ean
        End Get
    End Property


    Public ReadOnly Property Dimensions() As DTOProductDimensions
        Get
            If CheckBoxNoDimensions.Checked Then
                mDimensions = DTOProductDimensions.DimensionLess()
            Else
                mDimensions = New DTOProductDimensions
                With mDimensions
                    If CheckBoxHeredaDimensions.Checked Then
                        .Hereda = True
                        .KgNet = 0
                        .KgBrut = 0
                        .M3 = 0
                        .DimensionLargo = 0
                        .DimensionAncho = 0
                        .DimensionAlto = 0
                        .InnerPack = 0
                        .OuterPack = 0
                        .ForzarInnerPack = False
                    Else
                        .Hereda = False
                        .KgBrut = Xl_TextBoxNumKgBrut.Value
                        .KgNet = Xl_TextBoxNumKgNet.Value
                        .M3 = Xl_TextBoxNumM3.Value
                        .DimensionLargo = Xl_TextBoxNumDimL.Value
                        .DimensionAncho = Xl_TextBoxNumDimW.Value
                        .DimensionAlto = Xl_TextBoxNumDimH.Value
                        .InnerPack = Xl_TextBoxNumInnerPack.Value
                        .OuterPack = Xl_TextBoxNumOuterPack.Value
                        .ForzarInnerPack = CheckBoxForzarInnerPack.Checked
                    End If
                    .PackageEan = Xl_Ean131.Ean
                    If CheckBoxHeredaCodiMercancia.Checked Then
                        .CodiMercancia = Nothing
                    Else
                        .CodiMercancia = Xl_LookupCodiMercancia1.CodiMercancia
                    End If

                End With
            End If
            Return mDimensions
        End Get
    End Property

    Public ReadOnly Property IsDirty() As Boolean
        Get
            Return mIsDirty
        End Get
    End Property



    Private Sub EnableDimensions()
        Dim BlHereda As Boolean = CheckBoxHeredaDimensions.Checked
        For Each oControl As Control In GroupBoxDimensions.Controls
            If oControl Is CheckBoxHeredaDimensions Then
                oControl.Enabled = True
            ElseIf oControl Is ButtonFraccionarTemporalment Then
                oControl.Enabled = CheckBoxForzarInnerPack.Checked
            Else
                oControl.Enabled = Not BlHereda
            End If
        Next

    End Sub

    Private Sub EnableFraccionarPackaging()
        If Xl_TextBoxNumInnerPack.Value > 1 Then
            CheckBoxForzarInnerPack.Enabled = True
            ButtonFraccionarTemporalment.Enabled = True
        Else
            CheckBoxForzarInnerPack.Enabled = False
            ButtonFraccionarTemporalment.Enabled = False
        End If
    End Sub

    Private Sub ResetDimensions()
        Xl_TextBoxNumKgNet.Value = 0
        Xl_TextBoxNumKgBrut.Value = 0
        Xl_TextBoxNumM3.Value = 0
        Xl_TextBoxNumDimL.Value = 0
        Xl_TextBoxNumDimW.Value = 0
        Xl_TextBoxNumDimH.Value = 0
        Xl_TextBoxNumInnerPack.Value = 0
        Xl_TextBoxNumOuterPack.Value = 0
        CheckBoxForzarInnerPack.Checked = False
    End Sub

    Private Sub Control_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxNoDimensions.CheckedChanged,
        Xl_TextBoxNumKgBrut.AfterUpdate,
        Xl_TextBoxNumKgNet.AfterUpdate,
        Xl_TextBoxNumOuterPack.AfterUpdate,
         Xl_Ean131.AfterUpdate,
          Xl_LookupCodiMercancia1.AfterUpdate

        If mAllowEvents Then
            mIsDirty = True
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Sub CheckBoxHeredaCodiMercancia_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxHeredaCodiMercancia.CheckedChanged
        If mAllowEvents Then
            If CheckBoxHeredaCodiMercancia.Checked Then
                Xl_LookupCodiMercancia1.ReadOnlyLookup = True
                If TypeOf mTarget Is DTOProductCategory Then
                    Dim oCategory As DTOProductCategory = mTarget
                    Xl_LookupCodiMercancia1.CodiMercancia = oCategory.Brand.CodiMercancia
                ElseIf TypeOf mTarget Is DTOProductSku Then
                    Dim oSku As DTOProductSku = mTarget
                    Xl_LookupCodiMercancia1.CodiMercancia = DTOProductCategory.CodiMercanciaOrInherited(oSku.Category)
                End If
            Else
                Xl_LookupCodiMercancia1.ReadOnlyLookup = False
                Xl_LookupCodiMercancia1.CodiMercancia = DTOProduct.CodiMercancia(mTarget)
            End If

            mIsDirty = True
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub


    Private Sub Fraccionar_Changed(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        CheckBoxForzarInnerPack.CheckedChanged, _
         Xl_TextBoxNumInnerPack.AfterUpdate

        If mAllowEvents Then
            EnableFraccionarPackaging()
            mIsDirty = True
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Sub Xl_TextBoxNumInnerPack_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_TextBoxNumInnerPack.TextChanged
        If mAllowEvents Then
            EnableFraccionarPackaging()
        End If
    End Sub

    Private Sub Volume_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        Xl_TextBoxNumM3.AfterUpdate, _
          Xl_TextBoxNumDimL.AfterUpdate, _
           Xl_TextBoxNumDimW.AfterUpdate, _
            Xl_TextBoxNumDimH.AfterUpdate

        If mAllowEvents Then
            mIsDirty = True
            RaiseEvent AfterUpdate(sender, e)
            CheckWarnM3()
        End If
    End Sub


    Private Function VolumeCalculated() As Decimal
        Dim RetVal As Decimal = 0
        Dim DimL As Integer = Xl_TextBoxNumDimL.Value
        Dim DimW As Integer = Xl_TextBoxNumDimW.Value
        Dim DimH As Integer = Xl_TextBoxNumDimH.Value
        If Not (DimL = 0 Or DimW = 0 Or DimH = 0) Then
            RetVal = Math.Round(DimL * DimW * DimH / 1000000000, 3, MidpointRounding.ToEven)
        End If
        Return RetVal
    End Function

    Private Sub CheckWarnM3()
        Dim DecVolCalculated As Decimal = VolumeCalculated()
        Dim DecVolExisting As Decimal = Xl_TextBoxNumM3.Value
        If DecVolExisting = 0 And DecVolCalculated <> 0 Then
            DecVolExisting = DecVolCalculated
            Xl_TextBoxNumM3.Value = DecVolCalculated
        End If
        PictureBoxWarn.Visible = (DecVolCalculated <> DecVolExisting)
    End Sub

    Private Sub PictureBoxWarn_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBoxWarn.DoubleClick
        Xl_TextBoxNumM3.Value = VolumeCalculated()
        CheckWarnM3()
        mIsDirty = True
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Private Sub CheckBoxHeredaDimensions_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxHeredaDimensions.CheckedChanged
        If mAllowEvents Then
            refresca()
            mIsDirty = True
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Async Sub ButtonFraccionarTemporalment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFraccionarTemporalment.Click
        Dim oProduct As DTOProduct = mTarget
        Dim oUser As DTOUser = Current.Session.User
        Dim exs As New List(Of Exception)
        If Await FEB2.Product.FraccionarTemporalment(exs, oProduct, oUser) Then
            With ButtonFraccionarTemporalment
                .Image = My.Resources.crono
                .Text = "fraccionable 2 minuts"
                .Enabled = False
            End With
            Dim oTimer As New Timer
            oTimer.Interval = 20 * 1000
            AddHandler oTimer.Tick, AddressOf OnTempFraccTick
            oTimer.Enabled = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub OnTempFraccTick(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oTimer As Timer = sender
        oTimer.Enabled = False
        With ButtonFraccionarTemporalment
            .Image = Nothing
            .Text = "fraccionar 2 minuts"
            .Enabled = True
        End With
    End Sub


End Class
