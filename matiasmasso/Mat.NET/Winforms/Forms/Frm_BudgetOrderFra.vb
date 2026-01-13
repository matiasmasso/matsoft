Public Class Frm_BudgetOrderFra

    Private _BudgetOrderFra As DTOBudgetOrderFra
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBudgetOrderFra)
        MyBase.New()
        Me.InitializeComponent()
        _BudgetOrderFra = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.BudgetOrderFra.Load(_BudgetOrderFra, exs) Then
            With _BudgetOrderFra
                Xl_LookupBudgetOrder1.BudgetOrder = _BudgetOrderFra.BudgetOrder
                Xl_LookupBookFra1.BookFra = _BudgetOrderFra.BookFra
                Xl_Amount1.Amt = .Amt
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_Amount1.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _BudgetOrderFra
            .BudgetOrder = Xl_LookupBudgetOrder1.BudgetOrder
            .BookFra = Xl_LookupBookFra1.BookFra
            .Amt = Xl_Amount1.Amt
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.BudgetOrderFra.Update(_BudgetOrderFra, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BudgetOrderFra))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.BudgetOrderFra.Delete(_BudgetOrderFra, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BudgetOrderFra))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub Xl_LookupBookFra1_Request(sender As Object, e As EventArgs) Handles Xl_LookupBookFra1.onLookUpRequest
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = _BudgetOrderFra.BudgetOrder.Contact
        Dim oBookFras = Await FEB2.Bookfras.BudgetFree(exs, oContact)
        If exs.Count = 0 Then
            Dim oDefaultFra As DTOBookFra = Xl_LookupBookFra1.BookFra
            Dim oFrm As New Frm_BookFrasCompact(oBookFras, oDefaultFra, DTO.Defaults.SelectionModes.Selection)
            AddHandler oFrm.itemSelected, AddressOf onBookFraSelected
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub onBookFraSelected(sender As Object, e As MatEventArgs)
        Dim oBookFra As DTOBookFra = e.Argument
        Xl_LookupBookFra1.BookFra = oBookFra
        Xl_Amount1.Amt = oBookFra.BaseDevengada
    End Sub
End Class


