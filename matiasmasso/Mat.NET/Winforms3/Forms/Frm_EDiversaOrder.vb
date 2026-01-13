

Public Class Frm_EDiversaOrder
    Private _EdiversaOrder As DTOEdiversaOrder
    Private _loadedFile As Boolean
    Private _AllowEvents As Boolean

    Public Enum Tabs
        General
        File
        Errors
    End Enum

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOEdiversaOrder)
        MyBase.New()
        Me.InitializeComponent()
        _EdiversaOrder = value
    End Sub

    Private Async Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB.EdiversaOrder.Load(_EdiversaOrder, exs) Then
            Await refresca()
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        _AllowEvents = False
        With _EdiversaOrder
            DateTimePickerDoc.Value = .FchDoc
            If .FchDeliveryMin <> Nothing Then
                DateTimePickerDelivery.Value = .FchDeliveryMin
            End If
            If .FchDeliveryMax > DateTimePickerLast.MinDate Then
                DateTimePickerLast.Value = .FchDeliveryMax
            End If
            TextBoxOrderNum.Text = .DocNum
            Xl_EANComprador.Load(.CompradorEAN)
            Xl_EANReceptorMercancia.Load(.ReceptorMercanciaEAN)
            Xl_EANFacturarA.Load(.FacturarAEAN)
            Await Xl_Contact2Comprador.Load(exs, .Comprador)
            Await Xl_Contact2ReceptorMercancia.Load(exs, .ReceptorMercancia)
            Await Xl_Contact2FacturarA.Load(exs, .FacturarA)
            If .Cur Is Nothing Then
                TextBoxCur.Clear()
            Else
                TextBoxCur.Text = .Cur.Tag
            End If
            Xl_EDiversaOrderItems1.Load(.Items)
            TextBoxObs.Text = .Obs
            TextBoxDepto.Text = .Departamento
            If .Exceptions.Count = 0 Then
                TabControl1.TabPages.Remove(TabPageErrors)
            Else
                Await Xl_EdiversaExceptions1.Load(.Exceptions)
            End If

            Select Case .EdiversaFile.Result
                Case DTOEdiversaFile.Results.pending
                    LabelStatus.Text = "pendent"
                Case DTOEdiversaFile.Results.processed
                    LabelStatus.Text = "processat"
                Case DTOEdiversaFile.Results.deleted
                    LabelStatus.Text = "descartat"
            End Select

            ButtonOk.Enabled = False
            ButtonDel.Enabled = False
        End With
        _AllowEvents = True

    End Function

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        DateTimePickerDoc.ValueChanged,
        DateTimePickerDelivery.ValueChanged,
         DateTimePickerLast.ValueChanged,
          TextBoxObs.TextChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Await Save()
    End Sub

    Private Async Function Save() As Task
        UIHelper.ToggleProggressBar(Panel1, True)
        With _EdiversaOrder
            .FchDoc = DateTimePickerDoc.Value
            .FchDeliveryMin = DateTimePickerDelivery.Value
            .FchDeliveryMax = DateTimePickerLast.Value
            .DocNum = TextBoxOrderNum.Text
            .Comprador = Xl_Contact2Comprador.Contact
            .ReceptorMercancia = Xl_Contact2ReceptorMercancia.Contact
            .FacturarA = Xl_Contact2FacturarA.Contact
            .FacturarAEAN = Xl_EANFacturarA.EAN13
            .Cur = DTOCur.Factory(TextBoxCur.Text)
            .Items = Xl_EDiversaOrderItems1.Values
            .Obs = TextBoxObs.Text
        End With

        Dim exs As New List(Of Exception)
        If Await FEB.EdiversaOrder.Update(_EdiversaOrder, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_EdiversaOrder))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la comanda")
        End If
    End Function

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.EdiversaOrder.Delete(_EdiversaOrder, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_EdiversaOrder))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Xl_EDiversaOrderItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EDiversaOrderItems1.RequestToRefresh
        Dim exs As New List(Of Exception)
        _EdiversaOrder.IsLoaded = False
        Xl_EDiversaOrderItems1.Load(_EdiversaOrder.Items)
        ButtonOk.Enabled = True
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Dim exs As New List(Of Exception)
        Select Case TabControl1.SelectedIndex
            Case Tabs.File
                If Not _loadedFile Then
                    Dim oFile = _EdiversaOrder.EdiversaFile
                    If oFile Is Nothing And Not _EdiversaOrder.IsNew Then
                        oFile = Await FEB.EdiversaFile.Find(exs, _EdiversaOrder.Guid)
                    End If

                    If exs.Count = 0 Then
                        TextBoxGuid.Text = oFile.Guid.ToString
                        UIHelper.LoadComboFromEnum(ComboBoxResultCod, GetType(DTOEdiversaFile.Results),, oFile.Result)
                        If oFile.ResultBaseGuid IsNot Nothing Then
                            TextBoxResult.Text = oFile.ResultBaseGuid.Guid.ToString
                        End If
                        TextBoxSrc.Text = oFile.Stream
                        _loadedFile = True
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Sub

    Private Async Sub Xl_EdiversaExceptions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_EdiversaExceptions1.RequestToRefresh
        If _AllowEvents Then
            Dim exs As New List(Of Exception)
            If TypeOf e.Argument Is DTOEdiversaOrder Then
                _EdiversaOrder = Await FEB.EdiversaOrder.Find(_EdiversaOrder.Guid, exs)
                If exs.Count = 0 Then
                    Await refresca()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_EDiversaOrderItems1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_EDiversaOrderItems1.AfterUpdate
        ButtonOk.Enabled = True
    End Sub

    Private Sub ReassignarComandaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReassignarComandaToolStripMenuItem.Click
        Dim oFrm As New Frm_PurchaseOrders(Defaults.SelectionModes.selection)
        AddHandler oFrm.onItemSelected, AddressOf ReassignComanda
        oFrm.Show()
    End Sub

    Private Async Sub ReassignComanda(sender As Object, e As MatEventArgs)
        _EdiversaOrder.Result = e.Argument
        Await Save()
    End Sub
End Class


