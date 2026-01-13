Public Class Frm_BudgetPartida

    Private _Budget As DTOBudget
    Private _DefaultValue As DTOBudget.Item
    Private _SelectionMode As DTOBudget.SelectionModes = DTOBudget.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Enum Tabs
        General
        Orders
        Invoices
        Downloads
    End Enum

    Public Sub New(value As DTOBudget, Optional oDefaultValue As DTOBudget.Item = Nothing, Optional oSelectionMode As DTOBudget.SelectionModes = DTOBudget.SelectionModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Budget = value

    End Sub

    Private Async Sub Frm_BudgetPartida_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Budget.Load(_Budget, exs) Then
            With _Budget
                TextBoxBudget.Text = DTOBudget.ReverseNom(_Budget)
                TextBoxNom.Text = .Nom
                Xl_Contact21.Contact = .Contact
                Await refrescaItems()
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged,
          Xl_Contact21.AfterUpdate

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Budget
            .nom = TextBoxNom.Text
            .Contact = Xl_Contact21.Contact
        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Budget.Update(_Budget, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Budget))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub Xl_BudgetItems1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_BudgetItems1.RequestToAddNew
        Dim item As New DTOBudget.Item
        With item
            .Budget = _Budget
            .MonthFrom = New DTOYearMonth(_Budget.Exercici.Year, Today.Month)
            .MonthTo = .MonthFrom
        End With

        Dim oFrm As New Frm_BudgetItem(item)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaItems
        oFrm.Show()
    End Sub

    Private Async Sub refrescaItems(sender As Object, e As MatEventArgs)
        Await refrescaItems()
    End Sub
    Private Async Function refrescaItems() As Task
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.BudgetItems.All(exs, _Budget)
        If exs.Count = 0 Then
            Xl_BudgetItems1.Load(items, _DefaultValue, IIf(_SelectionMode = DTOBudget.SelectionModes.Item, DTO.Defaults.SelectionModes.Selection, DTO.Defaults.SelectionModes.Browse))
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_BudgetItems1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BudgetItems1.RequestToRefresh
        Await refrescaItems()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Invoices
            Case Tabs.Downloads
                Static done As Boolean
                If Not done Then
                    refrescaDocfiles()
                    done = True
                End If
        End Select
    End Sub

    Private Sub Xl_DocfileSrcs1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_DocfileSrcs1.RequestToAddNew
        Dim oDocfileSrc As New DTODocFileSrc()
        With oDocfileSrc
            .Cod = DTODocFile.Cods.Download
            .Src = _Budget
        End With
        Dim oFrm As New Frm_DocfileSrc(oDocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaDocfiles
        oFrm.Show()
    End Sub

    Private Async Sub refrescaDocfiles()
        Dim exs As New List(Of Exception)
        Dim oDocFileSrcs = Await FEB2.DocFileSrcs.All(_Budget, exs)
        If exs.Count = 0 Then
            Xl_DocfileSrcs1.Load(oDocFileSrcs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Xl_BudgetItems1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_BudgetItems1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB2.Budget.Delete(_Budget, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Budget))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If

    End Sub
End Class


