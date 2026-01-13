

Public Class Xl_Art_Logistics
    Private mTarget As Object
    Private mEmp as DTOEmp
    Private mDimensions As ArtDimensions
    Private mCnap As DTOCnap
    Private mCodiMercancia As maxisrvr.CodiMercancia
    Private mParent As ArtDimensions
    Private mIsDirty As Boolean = False
    Private mAllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public WriteOnly Property Target() As Object
        Set(ByVal value As Object)
            mTarget = value
            If TypeOf (mTarget) Is Art Then
                Dim oArt As Art = CType(mTarget, Art)
                mEmp = oArt.Emp
                mDimensions = oArt.Dimensions
                CheckBoxHeredaCodiMercancia.Checked = oArt.CodiMercancia.IsEmpty
                Xl_CodiMercancia1.CodiMercancia = oArt.CodiMercanciaSelfOrInherited
                ButtonFraccionarTemporalment.Visible = True
            ElseIf TypeOf (mTarget) Is Stp Then
                Dim oStp As Stp = CType(mTarget, Stp)
                mEmp = oStp.Tpa.emp
                mDimensions = oStp.Dimensions
                CheckBoxHeredaCodiMercancia.Checked = oStp.CodiMercancia.IsEmpty
                Xl_CodiMercancia1.CodiMercancia = oStp.CodiMercanciaSelfOrInherited
                CheckBoxHeredaDimensions.Visible = False
            Else
                Exit Property
            End If

            refrescaDimensions()
            EnableDimensions()
            EnableFraccionarPackaging()
            mAllowEvents = True
        End Set
    End Property

    Public ReadOnly Property CodiMercancia() As maxisrvr.CodiMercancia
        Get
            Dim oCodi As maxisrvr.CodiMercancia = maxisrvr.CodiMercancia.Empty
            If Not CheckBoxNoDimensions.Checked Then
                If Not CheckBoxHeredaCodiMercancia.Checked Then
                    oCodi = Xl_CodiMercancia1.CodiMercancia
                End If
            End If
            Return oCodi
        End Get
    End Property

    Public ReadOnly Property Dimensions() As ArtDimensions
        Get
            If CheckBoxNoDimensions.Checked Then
                mDimensions = ArtDimensions.DimensionLess()
            Else
                mDimensions = New ArtDimensions
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


    Private Sub LoadDimensions(ByVal oDimensions As ArtDimensions)
        With oDimensions
            If .NoDimensions Then
                CheckBoxNoDimensions.Checked = True
                ResetDimensions()
            Else
                CheckBoxNoDimensions.Checked = False
                Xl_TextBoxNumKgNet.Value = .KgNet
                Xl_TextBoxNumKgBrut.Value = .KgBrut
                Xl_TextBoxNumM3.Value = .M3
                Xl_TextBoxNumDimL.Value = .DimensionLargo
                Xl_TextBoxNumDimW.Value = .DimensionAncho
                Xl_TextBoxNumDimH.Value = .DimensionAlto
                Xl_TextBoxNumInnerPack.Value = .InnerPack
                Xl_TextBoxNumOuterPack.Value = .OuterPack
                CheckBoxForzarInnerPack.Checked = .ForzarInnerPack
            End If
            CheckWarnM3()
        End With
    End Sub

    Private Sub refrescaDimensions()
        Dim BlHereda As Boolean = mDimensions.Hereda

        If BlHereda Then
            LoadDimensions(mDimensions.Parent)
        Else
            LoadDimensions(mDimensions)
        End If

        CheckBoxHeredaDimensions.Checked = BlHereda
    End Sub

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
        CheckBoxHeredaCodiMercancia.CheckedChanged, _
        Xl_CodiMercancia1.AfterUpdate, _
        CheckBoxNoDimensions.CheckedChanged, _
        Xl_TextBoxNumKgBrut.AfterUpdate, _
        Xl_TextBoxNumKgNet.AfterUpdate, _
        Xl_TextBoxNumOuterPack.AfterUpdate

        If mAllowEvents Then
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
            If CheckBoxHeredaDimensions.Checked Then
                LoadDimensions(mDimensions.Parent)
            Else
                LoadDimensions(mDimensions)
            End If
            EnableDimensions()
            mIsDirty = True
            RaiseEvent AfterUpdate(sender, e)
        End If
    End Sub

    Private Sub ButtonFraccionarTemporalment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonFraccionarTemporalment.Click
        Dim oProduct As DTOProduct = Nothing
        If TypeOf mDimensions.Target Is Art Then
            oProduct = New DTOProductSku(mDimensions.TargetGuid)
        ElseIf TypeOf mDimensions.Target Is Stp Then
            oProduct = New DTOProductCategory(mDimensions.TargetGuid)
        Else
            MsgBox("error al asignar Xl_Art_Logistics.dimensions.target")
        End If
        Dim oUser As DTOUser = BLL.BLLSession.Current.User
        Dim exs As New List(Of Exception)
        If BLL.BLLProduct.FraccionarTemporalment(oProduct, oUser, exs) Then
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


    Private Sub Control_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
 _
     Xl_CodiMercancia1.AfterUpdate

        mIsDirty = True
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
