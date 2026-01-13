Public Class Frm_RepProduct
    Private _RepProducts As List(Of DTORepProduct)
    Private _RepProduct As DTORepProduct
    Private _Mode As Modes
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Modes
        SingleItem
        MultipleItems
    End Enum


    Public Sub New(ByVal oRepProducts As List(Of DTORepProduct))
        MyBase.New()
        Me.InitializeComponent()
        If oRepProducts.Count = 1 Then
            _Mode = Modes.SingleItem
        Else
            _Mode = Modes.MultipleItems
        End If
        _RepProducts = oRepProducts
        _RepProduct = oRepProducts(0)
        Refresca()
        CheckConflict()
        _AllowEvents = True
    End Sub

    Private Sub Refresca()

        With _RepProduct
            If _Mode = Modes.MultipleItems Then
                Xl_LookupRep1.Enabled = False
                Xl_LookupProduct1.Enabled = False
                Xl_LookupArea1.Enabled = False
            Else
                BLL.BLLRepProduct.Load(_RepProduct)
                Xl_LookupRep1.Rep = .Rep
                Xl_LookupProduct1.Product = .Product
                Xl_LookupArea1.Area = .Area
            End If

            If _Mode = Modes.SingleItem Then
                Xl_LookupRep1.Rep = .Rep
                Xl_LookupProduct1.Product = .Product
                Xl_LookupArea1.Area = .Area
            Else
                Xl_LookupRep1.Enabled = False
                Xl_LookupProduct1.Enabled = False
                Xl_LookupArea1.Enabled = False
            End If

            If .Cod = DTORepProduct.Cods.Included Then
                RadioButtonIncluded.Checked = True
                Xl_PercentComStd.Value = .ComStd
                Xl_PercentComRed.Value = .ComRed
            Else
                RadioButtonExcluded.Checked = True
                GroupBoxComisions.Visible = False
            End If

            DateTimePickerFrom.Value = .FchFrom
            If .FchTo <> Nothing Then
                CheckBoxFchTo.Checked = True
                DateTimePickerTo.Visible = True
                DateTimePickerTo.Value = .FchTo
            End If
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
            Xl_PercentComStd.AfterUpdate, _
             Xl_PercentComRed.AfterUpdate

        If _AllowEvents Then
            enableButtons()
        End If
    End Sub

    Private Sub Xl_LookUp_Rep1_AfterUpdate(sender As Object, e As MatEventArgs)
        If _AllowEvents Then
            Dim oRep As Rep = e.Argument
            Xl_PercentComStd.Value = oRep.ComisionStandard
            Xl_PercentComRed.Value = oRep.ComisionReducida
            CheckConflict()
            enableButtons()
        End If
    End Sub

    Private Sub MainControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        RadioButtonIncluded.CheckedChanged, _
         RadioButtonExcluded.CheckedChanged, _
          DateTimePickerFrom.ValueChanged, _
           DateTimePickerTo.ValueChanged, _
            Xl_PercentComStd.AfterUpdate, _
             Xl_PercentComRed.AfterUpdate

        If _AllowEvents Then
            If _Mode = Modes.SingleItem Then
                'TODO: arreglar checkitem també per MultipleItem
                CheckConflict()
            End If
            enableButtons()
        End If
    End Sub



    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Select Case _Mode
            Case Modes.SingleItem
                With _RepProduct
                    .Rep = Xl_LookupRep1.Rep
                    .Product = Xl_LookupProduct1.Product
                    .Area = Xl_LookupArea1.Area
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
                Dim exs As New List(Of Exception)
                If BLL.BLLRepProduct.Update(_RepProduct, exs) Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepProduct))
                    Me.Close()
                Else
                    UIHelper.WarnError(exs, "error al desar la zona")
                End If

            Case Modes.MultipleItems
                For Each item As DTORepProduct In _RepProducts
                    With item
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

                    Dim exs As New List(Of Exception)
                    If BLL.BLLRepProduct.Update(item, exs) Then
                        RaiseEvent AfterUpdate(Me, New MatEventArgs(item))
                        Me.Close()
                    Else
                        UIHelper.WarnError(exs, "error al desar la zona " & BLL.BLLArea.Nom(item.Area))
                    End If
                Next

        End Select
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        If BLL.BLLRepProduct.Delete(_RepProduct, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_RepProduct))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al eliminar zona de representant")
        End If
    End Sub

    Private Sub CheckBoxFchTo_CheckedChanged(sender As Object, e As System.EventArgs) Handles CheckBoxFchTo.CheckedChanged
        If _AllowEvents Then
            DateTimePickerTo.Visible = CheckBoxFchTo.CheckAlign
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub RadioButtonExcluded_Click(sender As Object, e As System.EventArgs) Handles RadioButtonExcluded.Click
        If _AllowEvents Then
            GroupBoxComisions.Visible = False
            ButtonOk.Enabled = True
        End If
    End Sub


    Private Function CheckConflict() As Boolean
        Dim retval As Boolean
        Dim oProduct As DTOProduct = Xl_LookupProduct1.Product
        Dim oArea As DTOArea = Xl_LookupArea1.Area
        Dim DtFch As Date = DateTimePickerFrom.Value

        If oArea IsNot Nothing And oProduct IsNot Nothing Then
            Dim oRepProduct As DTORepProduct = BLL.BLLRepProduct.GetRepProduct(oArea, oProduct, DtFch)
            Dim BlSameProduct As Boolean
            If oRepProduct Is Nothing Then
                ButtonOk.Image = Nothing
            Else
                BlSameProduct = oRepProduct.Guid.Equals(_RepProduct.Guid)
                If BlSameProduct Then
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
        End If

        Return retval
    End Function

    Private Sub enableButtons()
        If _Mode = Modes.SingleItem Then
            If Xl_LookupProduct1.Product Is Nothing Then
                ButtonOk.Enabled = False
            ElseIf Xl_LookupArea1.Area Is Nothing Then
                ButtonOk.Enabled = False
            Else
                If CheckConflict() Then
                    ButtonOk.Image = My.Resources.warning
                    ButtonOk.Enabled = False
                Else
                    ButtonOk.Image = Nothing
                    ButtonOk.Enabled = True
                End If
            End If
        Else
            If CheckConflict() Then
                ButtonOk.Image = My.Resources.warning
                ButtonOk.Enabled = False
            Else
                ButtonOk.Image = Nothing
                ButtonOk.Enabled = True
            End If
        End If
    End Sub


    Private Sub Xl_LookupRep1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupRep1.AfterUpdate
        Dim oRep As DTORep = e.Argument
        BLL.BLLRep.Load(oRep)
        With oRep
            Xl_PercentComRed.Value = oRep.ComisionReducida
            Xl_PercentComStd.Value = oRep.ComisionStandard
        End With

        ButtonOk.Enabled = True
    End Sub

    Private Sub Xl_LookupArea1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupArea1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub
End Class