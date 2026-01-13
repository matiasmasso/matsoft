Public Class Frm_RepProduct
    Private _RepProducts As List(Of DTORepProduct)
    Private _RepProduct As DTORepProduct
    Private _Mode As Modes
    Private _AllowEvents As Boolean = False
    Private _AllowPersist As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Modes
        SingleItem
        MultipleItems
    End Enum


    Public Sub New(ByVal oRepProducts As List(Of DTORepProduct), Optional AllowPersist As Boolean = True)
        MyBase.New()
        Me.InitializeComponent()
        _RepProducts = oRepProducts
        If oRepProducts.Count = 0 Then
            _Mode = Modes.SingleItem
            _RepProduct = New DTORepProduct()
        ElseIf oRepProducts.Count = 1 Then
            _Mode = Modes.SingleItem
            _RepProduct = oRepProducts.First
        Else
            _Mode = Modes.MultipleItems
            _RepProduct = oRepProducts.First
        End If

        _AllowPersist = AllowPersist
    End Sub

    Private Async Sub Frm_RepProduct_Load(sender As Object, e As EventArgs) Handles Me.Load
        Refresca()
        Await CheckConflict()
        _AllowEvents = True
    End Sub


    Private Sub Refresca()
        Dim exs As New List(Of Exception)
        With _RepProduct
            If _Mode = Modes.MultipleItems Then
                Dim oFirstRep As DTORep = _RepProducts.First.Rep
                If _RepProducts.Exists(Function(x) x.Rep.UnEquals(_RepProducts.First.Rep)) Then
                    Xl_LookupRep1.Enabled = False
                Else
                    Xl_LookupRep1.Rep = oFirstRep
                End If

                Dim oFirstProduct As DTOProduct = _RepProducts.First.Product
                If _RepProducts.Exists(Function(x) x.Product.UnEquals(_RepProducts.First.Product)) Then
                    Xl_LookupProduct1.Enabled = False
                Else
                    Dim oProducts As New List(Of DTOProduct)
                    If oFirstProduct IsNot Nothing Then oProducts.Add(oFirstProduct)
                    Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                End If

                Dim oFirstArea As DTOArea = _RepProducts.First.Area
                If oFirstArea Is Nothing Then
                    If _RepProducts.Exists(Function(x) x.Area IsNot Nothing) Then
                        UIHelper.WarnError(New Exception("Detectats alguns valors sense area de distribució"))
                        Me.Close()
                    Else
                        Xl_LookupArea1.Load(oFirstArea)
                    End If

                Else
                    If _RepProducts.Exists(Function(x) x.Area.UnEquals(_RepProducts.First.Area)) Then
                        Xl_LookupArea1.Enabled = False
                    Else
                        Xl_LookupArea1.Load(oFirstArea)
                    End If
                End If

                Dim oFirstDistributionChannel As DTODistributionChannel = _RepProducts.First.DistributionChannel
                If oFirstDistributionChannel Is Nothing Then
                    If _RepProducts.Exists(Function(x) x.DistributionChannel IsNot Nothing) Then
                        UIHelper.WarnError(New Exception("Detectats alguns valors sense canal de distribució"))
                        Me.Close()
                    Else
                        Xl_LookupDistributionChannel1.DistributionChannel = oFirstDistributionChannel
                    End If
                Else
                    If _RepProducts.Exists(Function(x) x.DistributionChannel.UnEquals(_RepProducts.First.DistributionChannel)) Then
                        Xl_LookupDistributionChannel1.Enabled = False
                    Else
                        Xl_LookupDistributionChannel1.DistributionChannel = oFirstDistributionChannel
                    End If
                End If

            Else
                If FEB.RepProduct.Load(exs, _RepProduct) Then
                    Xl_LookupRep1.Rep = .Rep
                    Dim oProducts As New List(Of DTOProduct)
                    If .Product IsNot Nothing Then oProducts.Add(.Product)
                    Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectAny)
                    Xl_LookupArea1.Load(.Area)
                    Xl_LookupDistributionChannel1.DistributionChannel = .DistributionChannel
                Else
                    UIHelper.WarnError(exs)
                End If
            End If

            If .Cod = DTORepProduct.Cods.Included Then
                RadioButtonIncluded.Checked = True
                Xl_PercentComStd.Value = .ComStd
                Xl_PercentComRed.Value = .ComRed
            Else
                RadioButtonExcluded.Checked = True
                GroupBoxComisions.Visible = False
            End If

            If .FchFrom <> Nothing Then
                DateTimePickerFrom.Value = .FchFrom
            End If

            If .FchTo <> Nothing Then
                CheckBoxFchTo.Checked = True
                DateTimePickerTo.Visible = True
                DateTimePickerTo.Value = .FchTo
            End If
        End With
    End Sub

    Private Async Sub ControlChanged(ByVal sender As Object, ByVal e As MatEventArgs) Handles _
            Xl_PercentComStd.AfterUpdate,
             Xl_PercentComRed.AfterUpdate,
               Xl_LookupProduct1.AfterUpdate,
                Xl_LookupArea1.AfterUpdate,
                 Xl_LookupDistributionChannel1.AfterUpdate

        If _AllowEvents Then
            Await enableButtons()
        End If
    End Sub

    Private Async Sub Xl_LookUp_Rep1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupRep1.AfterUpdate
        If _AllowEvents Then
            Dim oRep As DTORep = e.Argument
            Xl_PercentComStd.Value = oRep.ComisionStandard
            Xl_PercentComRed.Value = oRep.ComisionReducida
            Await CheckConflict()
            Await enableButtons()
        End If
    End Sub

    Private Async Sub MainControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
          DateTimePickerFrom.ValueChanged,
           DateTimePickerTo.ValueChanged,
            Xl_PercentComStd.AfterUpdate,
             Xl_PercentComRed.AfterUpdate

        If _AllowEvents Then
            If _Mode = Modes.SingleItem Then
                'TODO: arreglar checkitem també per MultipleItem
                Await CheckConflict()
            End If
            Await enableButtons()
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Select Case _Mode
            Case Modes.SingleItem
                With _RepProduct
                    .Rep = Xl_LookupRep1.Rep
                    .Product = Xl_LookupProduct1.Product
                    .Area = Xl_LookupArea1.Area
                    .DistributionChannel = Xl_LookupDistributionChannel1.DistributionChannel
                    .Cod = IIf(RadioButtonExcluded.Checked, DTORepProduct.Cods.Excluded, DTORepProduct.Cods.Included)
                    .FchFrom = DateTimePickerFrom.Value
                    If CheckBoxFchTo.Checked Then
                        .FchTo = DateTimePickerTo.Value
                    Else
                        .FchTo = Nothing
                    End If
                    If RadioButtonExcluded.Checked Then
                        .ComStd = 0
                        .ComRed = 0
                    Else
                        .ComStd = Xl_PercentComStd.Value
                        .ComRed = Xl_PercentComRed.Value
                    End If
                End With

                If _AllowPersist Then
                    Dim exs As New List(Of Exception)
                    UIHelper.ToggleProggressBar(Panel1, True)
                    If Await FEB.RepProduct.Update(exs, _RepProduct) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepProduct))
                        Me.Close()
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs, "error al desar la zona")
                    End If
                Else
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepProducts))
                    Me.Close()
                End If


            Case Modes.MultipleItems
                For Each item As DTORepProduct In _RepProducts
                    With item
                        If Xl_LookupRep1.Enabled Then .rep = Xl_LookupRep1.Rep
                        If Xl_LookupProduct1.Enabled Then .product = Xl_LookupProduct1.Product
                        If Xl_LookupArea1.Enabled Then .area = Xl_LookupArea1.Area
                        If Xl_LookupDistributionChannel1.Enabled Then .distributionChannel = Xl_LookupDistributionChannel1.DistributionChannel

                        .cod = IIf(RadioButtonExcluded.Checked, DTORepProduct.Cods.excluded, DTORepProduct.Cods.included)
                        .fchFrom = DateTimePickerFrom.Value
                        If CheckBoxFchTo.Checked Then
                            .fchTo = DateTimePickerTo.Value
                        Else
                            .fchTo = Nothing
                        End If
                        If RadioButtonExcluded.Checked Then
                            .comStd = 0
                            .comRed = 0
                        Else
                            .comStd = Xl_PercentComStd.Value
                            .comRed = Xl_PercentComRed.Value
                        End If
                    End With

                Next

                If _AllowPersist Then
                    Dim exs As New List(Of Exception)
                    UIHelper.ToggleProggressBar(Panel1, True)
                    If Await FEB.RepProducts.Update(exs, _RepProducts) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepProducts))
                        Me.Close()
                    Else
                        UIHelper.ToggleProggressBar(Panel1, False)
                        UIHelper.WarnError(exs, "error al desar la zona")
                    End If
                Else
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepProducts))
                    Me.Close()
                End If

        End Select

    End Sub

    Private Async Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB.RepProducts.Delete(exs, _RepProducts) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepProduct))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al eliminar zona de representant")
        End If
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If _AllowEvents Then
            DateTimePickerTo.Visible = CheckBoxFchTo.CheckAlign
            ButtonOk.Enabled = True
        End If
    End Sub




    Private Async Function CheckConflict() As Task(Of Boolean)
        Dim retval As Boolean
        Dim exs As New List(Of Exception)
        Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
        Dim oArea As DTOArea = Xl_LookupArea1.Area
        Dim oDistributionChannel As DTODistributionChannel = Xl_LookupDistributionChannel1.DistributionChannel
        Dim DtFch As Date = DateTimePickerFrom.Value

        If oDistributionChannel IsNot Nothing And oArea IsNot Nothing And oProduct IsNot Nothing Then
            Dim oRepProduct = Await FEB.RepProduct.Find(exs, oArea, oProduct, oDistributionChannel, DtFch)
            If exs.Count = 0 Then
                Dim BlSameRepProduct As Boolean
                If oRepProduct Is Nothing Then
                    ButtonOk.Image = Nothing
                Else
                    BlSameRepProduct = oRepProduct.Guid.Equals(_RepProduct.Guid)
                    If BlSameRepProduct Then
                        retval = False
                    Else
                        If RadioButtonIncluded.Checked Then
                            ButtonOk.Image = My.Resources.warning
                            retval = True
                        Else
                            If Not oRepProduct.Rep.Equals(Xl_LookupRep1.Rep) Then
                                retval = True
                            End If
                        End If
                    End If
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        End If

        Return retval
    End Function

    Private Async Function enableButtons() As Task
        If _Mode = Modes.SingleItem Then
            If Xl_LookupProduct1.Product Is Nothing Then
                ButtonOk.Enabled = False
            ElseIf Xl_LookupArea1.Area Is Nothing Then
                ButtonOk.Enabled = False
            Else
                If Await CheckConflict() Then
                    ButtonOk.Image = My.Resources.warning
                    ButtonOk.Enabled = False
                Else
                    ButtonOk.Image = Nothing
                    ButtonOk.Enabled = True
                End If
            End If
        Else
            If Await CheckConflict() Then
                ButtonOk.Image = My.Resources.warning
                ButtonOk.Enabled = False
            Else
                ButtonOk.Image = Nothing
                ButtonOk.Enabled = True
            End If
        End If
    End Function


    Private Sub Xl_LookupRep1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupRep1.AfterUpdate
        Dim exs As New List(Of Exception)
        Dim oRep As DTORep = e.Argument
        If FEB.Rep.Load(exs, oRep) Then
            With oRep
                Xl_PercentComRed.Value = oRep.ComisionReducida
                Xl_PercentComStd.Value = oRep.ComisionStandard
            End With

            ButtonOk.Enabled = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_LookupArea1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupArea1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub RadioButton_CheckedChanged(sender As Object, e As EventArgs) Handles _
        RadioButtonExcluded.CheckedChanged,
         RadioButtonIncluded.CheckedChanged

        If _AllowEvents Then
            GroupBoxComisions.Visible = RadioButtonIncluded.Checked
            ButtonOk.Enabled = True
        End If

    End Sub

    Private Sub Xl_LookupArea1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupArea1.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectAny, Xl_LookupArea1.Area)
        AddHandler oFrm.onItemSelected, AddressOf onAreaSelected
        oFrm.Show()
    End Sub

    Private Sub onAreaSelected(sender As Object, e As MatEventArgs)
        Xl_LookupArea1.Load(e.Argument)
    End Sub
End Class