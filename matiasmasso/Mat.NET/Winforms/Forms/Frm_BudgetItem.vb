Public Class Frm_BudgetItem
    Private _Value As DTOBudget.Item
    Private _doneInvoices As Boolean
    Private _doneDocFiles As Boolean
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Invoices
        Downloads
    End Enum

    Public Sub New(value As DTOBudget.Item)
        MyBase.New()
        Me.InitializeComponent()
        _Value = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.BudgetItem.Load(_Value, exs) Then
            With _Value
                TextBoxBudget.Text = DTOBudget.ReverseNom(_Value.Budget)
                TextBoxYear.Text = .MonthFrom.Year
                Xl_MonthFrom.Load(Current.Session.Lang)
                Xl_MonthFrom.Month = .MonthFrom.Month
                Xl_MonthTo.Load(Current.Session.Lang)
                Xl_MonthTo.Month = .MonthTo.Month
                Xl_AmountAssignat.Amt = .Amt
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        Xl_MonthFrom.AfterUpdate,
         Xl_MonthTo.AfterUpdate,
          Xl_AmountAssignat.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Value
            .MonthFrom = New DTOYearMonth(_Value.Budget.Exercici.Year, Xl_MonthFrom.Month)
            .MonthTo = New DTOYearMonth(_Value.Budget.Exercici.Year, Xl_MonthTo.Month)
            .Amt = Xl_AmountAssignat.Amt
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.BudgetItem.Update(_Value, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
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
        Dim rc As MsgBoxResult = MsgBox("eliminem aquesta acció?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.BudgetItem.Delete(_Value, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Value))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar la acció")
            End If
        End If
    End Sub


    Private Async Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Invoices
                If Not _doneInvoices Then
                    Await refrescaInvoices()
                    _doneInvoices = True
                End If
            Case Tabs.Downloads
                If Not _doneDocFiles Then
                    refrescaDocfiles()
                    _doneDocFiles = True
                End If
        End Select
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToAddNew
        Dim oDocfileSrc As New DTODocFileSrc()
        With oDocfileSrc
            .Cod = DTODocFile.Cods.Download
            .Src = _Value
        End With
        Dim oFrm As New Frm_DocfileSrc(oDocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaDocfiles
        oFrm.Show()
    End Sub

    Private Async Sub refrescaDocfiles()
        Dim exs As New List(Of Exception)
        Dim oDocFileSrcs = Await FEB2.DocFileSrcs.All(_Value, exs)
        If exs.Count = 0 Then
            Xl_DocfileSrcs1.Load(oDocFileSrcs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_BudgetItemTickets1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BudgetItemTickets1.RequestToAddNew
        Dim oBudgetItemTicket As New DTOBudgetItemTicket
        With oBudgetItemTicket
            .BudgetItem = _Value
        End With
        Dim oFrm As New Frm_BudgetItemTicket(oBudgetItemTicket)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaInvoices
        oFrm.Show()
    End Sub


    Private Async Sub refrescaInvoices(sender As Object, e As MatEventArgs)
        Await refrescaInvoices()
    End Sub

    Private Async Function refrescaInvoices() As Task
        Dim exs As New List(Of Exception)
        Dim oBudgetItemTickets = Await FEB2.BudgetItemTickets.All(exs, _Value)
        If exs.Count = 0 Then
            Xl_BudgetItemTickets1.Load(oBudgetItemTickets)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_BudgetItemTickets1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BudgetItemTickets1.RequestToRefresh
        Await refrescaInvoices()
    End Sub
End Class