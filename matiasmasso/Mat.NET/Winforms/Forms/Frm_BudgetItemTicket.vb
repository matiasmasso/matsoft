Public Class Frm_BudgetItemTicket

    Private _BudgetItemTicket As DTOBudgetItemTicket
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOBudgetItemTicket)
        MyBase.New()
        Me.InitializeComponent()
        _BudgetItemTicket = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.BudgetItemTicket.Load(_BudgetItemTicket, exs) Then
            With _BudgetItemTicket
                Xl_LookupBudgetItem1.BudgetItem = _BudgetItemTicket.BudgetItem
                Xl_Amount1.Amt = .Amt

                Select Case _BudgetItemTicket.Cod
                    Case DTOBudgetItemTicket.Cods.Factura, DTOBudgetItemTicket.Cods.NotSet
                        RadioButtonFra.Checked = True
                        Xl_LookupBookFra1.Visible = True
                        Xl_DocFile1.Visible = False
                        Xl_LookupBookFra1.BookFra = _BudgetItemTicket.BookFra
                    Case DTOBudgetItemTicket.Cods.Altres
                        RadioButtonTicket.Checked = True
                        Xl_LookupBookFra1.Visible = False
                        Xl_DocFile1.Visible = True
                        Xl_DocFile1.Load(_BudgetItemTicket.Docfile)
                End Select

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
        With _BudgetItemTicket
            .BudgetItem = Xl_LookupBudgetItem1.BudgetItem
            If RadioButtonFra.Checked Then
                .BookFra = Xl_LookupBookFra1.BookFra
            Else
                .Docfile = Xl_DocFile1.Value
            End If
            .Amt = Xl_Amount1.Amt
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.BudgetItemTicket.Update(_BudgetItemTicket, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_BudgetItemTicket))
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
            If Await FEB2.BudgetItemTicket.Delete(_BudgetItemTicket, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_BudgetItemTicket))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Async Sub Xl_LookupBookFra1_Request(sender As Object, e As EventArgs) Handles Xl_LookupBookFra1.onLookUpRequest
        Dim exs As New List(Of Exception)
        Dim oContact As DTOContact = _BudgetItemTicket.BudgetItem.Budget.Contact
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
        If Xl_Amount1.Amt.IsNotZero Then
            Xl_Amount1.Amt = oBookFra.BaseDevengada
        End If
    End Sub

    Private Sub Xl_LookupBudgetItem1_onLookUpRequest(sender As Object, e As EventArgs) Handles Xl_LookupBudgetItem1.onLookUpRequest
        Dim oFrm As New Frm_Budgets(, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.itemSelected, AddressOf onItemSelected
        oFrm.Show()
    End Sub

    Private Sub onItemSelected(sender As Object, e As MatEventArgs)
        Dim oItem As DTOBudget.Item = e.Argument
        Xl_LookupBudgetItem1.BudgetItem = oItem
        If Xl_Amount1.Amt.IsNotZero Then
            Xl_Amount1.Amt = oItem.Amt
        End If
    End Sub


    Private Sub RadioButtonFra_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonFra.CheckedChanged
        Xl_LookupBookFra1.Visible = RadioButtonFra.Checked
        Xl_DocFile1.Visible = RadioButtonTicket.Checked
    End Sub
End Class


